import hashlib
import os
from django.shortcuts import render, get_object_or_404, redirect
from django.contrib.auth.decorators import login_required
from django.http import JsonResponse, HttpResponse
from django.views.decorators.csrf import csrf_exempt
from django.utils.decorators import method_decorator
from django.core.files.storage import default_storage
from django.conf import settings
from django.db import models
from rest_framework import viewsets, status
from rest_framework.decorators import action, api_view, permission_classes
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated
from rest_framework.parsers import MultiPartParser, FormParser
from .models import (Jerarquia, Destino, Oficial, TipoProcedimiento, FormularioHash, 
                     Archivo, FormularioCustodia, PersonalCustodia)
from .serializers import (
    JerarquiaSerializer, OficialSerializer, TipoProcedimientoSerializer,
    FormularioHashSerializer, FormularioHashListSerializer, ArchivoSerializer,
    ArchivoUploadSerializer
)


# API Views
class JerarquiaViewSet(viewsets.ModelViewSet):
    queryset = Jerarquia.objects.all()
    serializer_class = JerarquiaSerializer
    permission_classes = [IsAuthenticated]


class OficialViewSet(viewsets.ModelViewSet):
    queryset = Oficial.objects.select_related('jerarquia').filter(activo=True)
    serializer_class = OficialSerializer
    permission_classes = [IsAuthenticated]
    
    @action(detail=False, methods=['get'])
    def buscar(self, request):
        """Buscar oficiales por nombre o legajo"""
        termino = request.query_params.get('q', '')
        if len(termino) < 2:
            return Response([])
        
        oficiales = self.queryset.filter(
            models.Q(nombre_completo__icontains=termino) |
            models.Q(legajo__icontains=termino)
        )[:10]
        
        serializer = self.get_serializer(oficiales, many=True)
        return Response(serializer.data)


class TipoProcedimientoViewSet(viewsets.ModelViewSet):
    queryset = TipoProcedimiento.objects.filter(activo=True)
    serializer_class = TipoProcedimientoSerializer
    permission_classes = [IsAuthenticated]


class FormularioHashViewSet(viewsets.ModelViewSet):
    queryset = FormularioHash.objects.select_related(
        'oficial_entrega__jerarquia', 'oficial_recibe__jerarquia', 
        'tipo_procedimiento', 'creado_por'
    ).prefetch_related('archivos', 'historial')
    permission_classes = []  # Quitar autenticación temporalmente
    
    def get_serializer_class(self):
        if self.action == 'list':
            return FormularioHashListSerializer
        return FormularioHashSerializer
    
    def create(self, request, *args, **kwargs):
        """Crear formulario y procesar archivos en una sola operación"""
        # Extraer archivos del request
        archivos = request.FILES.getlist('archivos')
        
        # Crear formulario sin archivos primero
        data = request.data.copy()
        if 'archivos' in data:
            del data['archivos']
        
        # Remover nro_hash si está vacío para que se auto-genere
        if 'nro_hash' in data and not data['nro_hash']:
            del data['nro_hash']
        
        serializer = self.get_serializer(data=data)
        if not serializer.is_valid():
            print("Validation errors:", serializer.errors)
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
        
        # Guardar formulario
        usuario = request.user if request.user.is_authenticated else None
        formulario = serializer.save(creado_por=usuario)
        
        # Procesar archivos si los hay
        if archivos:
            archivos_procesados = 0
            for archivo in archivos:
                # Calcular hash SHA-256
                hash_sha256 = _calcular_hash_archivo(archivo)
                
                # Verificar si ya existe un archivo con el mismo hash en este formulario
                if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                    print(f"Archivo duplicado omitido: {archivo.name} (hash ya existe)")
                    continue
                
                # Obtener el siguiente número de orden
                archivos_procesados += 1
                siguiente_orden = formulario.archivos.count() + 1
                
                # Obtener extensión del archivo
                extension = os.path.splitext(archivo.name)[1].lower()
                # Remover el punto inicial si existe
                if extension.startswith('.'):
                    extension = extension[1:]
                
                # Crear objeto Archivo
                Archivo.objects.create(
                    formulario=formulario,
                    nro_orden=siguiente_orden,
                    nombre=archivo.name,
                    extension=extension,
                    peso=archivo.size,
                    hash_sha256=hash_sha256,
                    tipo_mime=getattr(archivo, 'content_type', '') or ''
                )
        
        # Retornar formulario completo
        headers = self.get_success_headers(serializer.data)
        formulario_serializer = FormularioHashSerializer(formulario)
        return Response(formulario_serializer.data, status=status.HTTP_201_CREATED, headers=headers)
    
    @action(detail=True, methods=['post'], parser_classes=[MultiPartParser, FormParser])
    def subir_archivos(self, request, pk=None):
        """Subir archivos a un formulario existente"""
        formulario = self.get_object()
        archivos = request.FILES.getlist('archivos')
        
        if not archivos:
            return Response(
                {'error': 'No se enviaron archivos'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        archivos_procesados = []
        siguiente_orden = formulario.archivos.count() + 1
        
        for archivo in archivos:
            # Calcular hash
            hash_sha256 = _calcular_hash_archivo(archivo)
            
            # Verificar si ya existe un archivo con el mismo hash en este formulario
            if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                print(f"Archivo duplicado omitido: {archivo.name} (hash ya existe)")
                continue
                
            # Obtener extensión del archivo
            extension = os.path.splitext(archivo.name)[1].lower()
            # Remover el punto inicial si existe
            if extension.startswith('.'):
                extension = extension[1:]
            
            # Crear objeto Archivo
            archivo_obj = Archivo.objects.create(
                formulario=formulario,
                nro_orden=siguiente_orden,
                nombre=archivo.name,
                extension=extension,
                peso=archivo.size,
                hash_sha256=hash_sha256,
                tipo_mime=archivo.content_type or ''
            )
            
            archivos_procesados.append(archivo_obj)
            siguiente_orden += 1
        
        
        # Serializar archivos procesados
        serializer = ArchivoSerializer(archivos_procesados, many=True)
        return Response(serializer.data, status=status.HTTP_201_CREATED)
    
    @action(detail=True, methods=['get'])
    def exportar_excel(self, request, pk=None):
        """Exportar formulario a Excel"""
        formulario = self.get_object()
        
        # Crear workbook
        try:
            import pandas as pd
            from io import BytesIO
            
            # Datos del formulario
            datos_formulario = {
                'Información': ['Número Hash', 'Tipo', 'Procedimiento', 'Oficial Entrega', 'Oficial Recibe', 'Fecha Creación', 'Estado'],
                'Valores': [
                    f"#{formulario.nro_hash}",
                    formulario.get_tipo_display(),
                    formulario.procedimiento,
                    str(formulario.oficial_entrega),
                    str(formulario.oficial_recibe),
                    formulario.fecha_creacion.strftime("%d/%m/%Y %H:%M:%S"),
                    formulario.get_estado_display()
                ]
            }
            
            # Datos de archivos
            archivos_data = []
            for archivo in formulario.archivos.all():
                archivos_data.append({
                    'Orden': archivo.nro_orden,
                    'Nombre': archivo.nombre,
                    'Extensión': archivo.extension,
                    'Tamaño': archivo.peso_formateado,
                    'Hash SHA-256': archivo.hash_sha256,
                    'Tipo': 'Imagen' if archivo.es_imagen else 
                            'Video' if archivo.es_video else
                            'Audio' if archivo.es_audio else
                            'Documento' if archivo.es_documento else 'Varios'
                })
            
            # Crear DataFrames
            df_info = pd.DataFrame(datos_formulario)
            df_archivos = pd.DataFrame(archivos_data)
            
            # Crear archivo Excel
            buffer = BytesIO()
            with pd.ExcelWriter(buffer, engine='openpyxl') as writer:
                df_info.to_excel(writer, sheet_name='Información', index=False)
                df_archivos.to_excel(writer, sheet_name='Archivos', index=False)
            
            buffer.seek(0)
            
            response = HttpResponse(
                buffer.getvalue(),
                content_type='application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            )
            response['Content-Disposition'] = f'attachment; filename="formulario_hash_{formulario.nro_hash}.xlsx"'
            return response
            
        except ImportError:
            return Response(
                {'error': 'pandas no está instalado'}, 
                status=status.HTTP_500_INTERNAL_SERVER_ERROR
            )
    
    @action(detail=True, methods=['post'])
    def cambiar_estado(self, request, pk=None):
        """Cambiar estado del formulario"""
        formulario = self.get_object()
        nuevo_estado = request.data.get('estado')
        
        if nuevo_estado not in ['BORRADOR', 'FINALIZADO', 'ARCHIVADO']:
            return Response(
                {'error': 'Estado inválido'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        estado_anterior = formulario.estado
        formulario.estado = nuevo_estado
        formulario.save()
        
        
        return Response({'estado': nuevo_estado})
    



class ArchivoViewSet(viewsets.ReadOnlyModelViewSet):
    queryset = Archivo.objects.select_related('formulario')
    serializer_class = ArchivoSerializer
    permission_classes = [IsAuthenticated]
    
    @action(detail=True, methods=['delete'])
    def eliminar(self, request, pk=None):
        """Eliminar un archivo"""
        archivo = self.get_object()
        formulario = archivo.formulario
        
        # Verificar que el formulario esté en borrador
        if formulario.estado != 'BORRADOR':
            return Response(
                {'error': 'Solo se pueden eliminar archivos de formularios en borrador'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        archivo.delete()
        
        # Reordenar archivos restantes
        archivos_restantes = formulario.archivos.order_by('nro_orden')
        for i, arch in enumerate(archivos_restantes, 1):
            arch.nro_orden = i
            arch.save()
        
        
        return Response(status=status.HTTP_204_NO_CONTENT)


# Template Views para Hash
def dashboard(request):
    """Dashboard principal"""
    # Estadísticas básicas
    total_formularios = FormularioHash.objects.count()
    total_archivos = Archivo.objects.count()
    formularios_recientes = FormularioHash.objects.select_related(
        'oficial_entrega__jerarquia', 'oficial_recibe__jerarquia'
    ).order_by('-fecha_creacion')[:5]
    
    context = {
        'total_formularios': total_formularios,
        'total_archivos': total_archivos,
        'formularios_recientes': formularios_recientes,
        'title': 'Dashboard - Hash Web'
    }
    return render(request, 'core/dashboard.html', context)


def form_hash(request, formulario_id=None):
    """Vista unificada del modo escritorio - permite crear y editar formularios"""
    
    # Obtener datos necesarios
    oficiales = Oficial.objects.select_related('jerarquia', 'destino').filter(activo=True)
    
    if formulario_id:
        # Modo edición - cargar formulario existente
        formulario = get_object_or_404(
            FormularioHash.objects.select_related(
                'oficial_entrega__jerarquia', 'oficial_entrega__destino',
                'oficial_recibe__jerarquia', 'oficial_recibe__destino',
                'tipo_procedimiento'
            ).prefetch_related('archivos'),
            id=formulario_id
        )
        modo = 'edicion'
        siguiente_hash = formulario.nro_hash
    else:
        # Modo creación - nuevo formulario
        formulario = None
        modo = 'creacion'
        # Obtener el siguiente número de hash disponible
        ultimo_hash = FormularioHash.objects.aggregate(
            max_hash=models.Max('nro_hash')
        )['max_hash']
        siguiente_hash = (ultimo_hash or 0) + 1
    
    # Verificar si hay un formulario recién creado en la sesión
    formulario_creado_id = request.session.get('formulario_creado_id')
    if formulario_creado_id and modo == 'creacion':
        try:
            formulario_creado = FormularioHash.objects.select_related(
                'oficial_entrega__jerarquia', 'oficial_entrega__destino',
                'oficial_recibe__jerarquia', 'oficial_recibe__destino',
                'tipo_procedimiento'
            ).prefetch_related('archivos').get(id=formulario_creado_id)
            modo = 'resultado'
            formulario = formulario_creado
            # Limpiar la sesión
            del request.session['formulario_creado_id']
        except FormularioHash.DoesNotExist:
            pass
    
    context = {
        'formulario': formulario,
        'oficiales': oficiales,
        'siguiente_hash': siguiente_hash,
        'modo': modo,
        'title': f'Formulario Hash - {"Editar" if formulario and modo == "edicion" else "Nuevo" if modo == "creacion" else "Resultado"} Formulario'
    }
    return render(request, 'core/form_hash.html', context)


from .forms import FormularioHashForm
import os
import hashlib

def crear_hash(request):
    """Vista para crear nuevo hash"""
    if request.method == 'POST':
        form = FormularioHashForm(request.POST)
        if form.is_valid():
            formulario = form.save(commit=False)
            if request.user.is_authenticated:
                formulario.creado_por = request.user
            formulario.save()

            archivos = request.FILES.getlist('archivos')
            if archivos:
                for i, archivo in enumerate(archivos, 1):
                    hash_sha256 = _calcular_hash_archivo(archivo)
                    extension = os.path.splitext(archivo.name)[1].lower()
                    if extension.startswith('.'):
                        extension = extension[1:]

                    Archivo.objects.create(
                        formulario=formulario,
                        nro_orden=i,
                        nombre=archivo.name,
                        extension=extension,
                        peso=archivo.size,
                        hash_sha256=hash_sha256,
                        tipo_mime=getattr(archivo, 'content_type', '') or ''
                    )


            return redirect('core:form_hash_editar', formulario_id=formulario.id)
    else:
        try:
            form = FormularioHashForm()
        except Exception as e:
            print(f"Error al crear formulario: {e}")
            form = None

    # Obtener datos para debug
    oficiales_count = Oficial.objects.filter(activo=True).count()
    tipos_count = TipoProcedimiento.objects.filter(activo=True).count()
    
    context = {
        'form': form,
        'siguiente_numero': FormularioHash.objects.count() + 1,
        'debug_info': {
            'oficiales_count': oficiales_count,
            'tipos_count': tipos_count,
        }
    }
    return render(request, 'core/crear_hash.html', context)




def eliminar_hash(request, formulario_id):
    """Vista para eliminar un hash"""
    formulario = get_object_or_404(FormularioHash, id=formulario_id)
    
    if request.method == 'POST':
        # Confirmar eliminación
        formulario.delete()
        return redirect('core:lista_hashes')
    
    context = {
        'formulario': formulario,
        'title': f'Eliminar Formulario #{formulario.nro_hash}'
    }
    return render(request, 'core/eliminar_hash.html', context)


def _calcular_hash_archivo(archivo):
    """Calcula el hash SHA-256 de un archivo"""
    hash_sha256 = hashlib.sha256()
    for chunk in archivo.chunks():
        hash_sha256.update(chunk)
    return hash_sha256.hexdigest().upper()


def ver_hash(request, formulario_id):
    """Vista para ver/editar hash"""
    formulario = get_object_or_404(
        FormularioHash.objects.select_related(
            'oficial_entrega__jerarquia', 'oficial_entrega__destino',
            'oficial_recibe__jerarquia', 'oficial_recibe__destino',
            'tipo_procedimiento'
        ).prefetch_related('archivos'),
        id=formulario_id
    )
    
    context = {
        'formulario': formulario,
        'puede_editar': formulario.estado == 'BORRADOR',
    }
    return render(request, 'core/ver_hash.html', context)


def lista_hashes(request):
    """Vista para listar formularios"""
    formularios = FormularioHash.objects.select_related(
        'oficial_entrega__jerarquia', 'oficial_recibe__jerarquia'
    ).order_by('-fecha_creacion')
    
    context = {
        'formularios': formularios,
    }
    return render(request, 'core/lista_hashes.html', context)


# Template Views para Custodia
def lista_custodias(request):
    """Vista para listar todas las custodias"""
    custodias = FormularioCustodia.objects.select_related(
        'nro_hash', 'creado_por'
    ).prefetch_related('personal__oficial').all()
    
    context = {
        'custodias': custodias,
        'title': 'Formularios de Custodia'
    }
    return render(request, 'core/lista_custodias.html', context)


from .forms import FormularioCustodiaForm, PersonalCustodiaForm

def crear_custodia(request):
    """Vista para crear un nuevo formulario de custodia"""
    if request.method == 'POST':
        form = FormularioCustodiaForm(request.POST)
        if form.is_valid():
            custodia = form.save(commit=False)
            if request.user.is_authenticated:
                custodia.creado_por = request.user
            custodia.save()
            return redirect('core:ver_custodia', custodia_id=custodia.id)
    else:
        form = FormularioCustodiaForm()
    
    context = {
        'form': form,
        'title': 'Crear Formulario de Custodia'
    }
    return render(request, 'core/crear_custodia.html', context)


def ver_custodia(request, custodia_id):
    """Vista para ver detalles de una custodia y agregar personal"""
    custodia = get_object_or_404(
        FormularioCustodia.objects.select_related(
            'nro_hash', 'creado_por'
        ).prefetch_related(
            'personal__oficial__jerarquia',
            'personal__oficial__destino'
        ),
        id=custodia_id
    )

    if request.method == 'POST':
        personal_form = PersonalCustodiaForm(request.POST)
        if personal_form.is_valid():
            personal = personal_form.save(commit=False)
            personal.formulario_custodia = custodia
            personal.save()
            return redirect('core:ver_custodia', custodia_id=custodia.id)
    else:
        personal_form = PersonalCustodiaForm()

    context = {
        'custodia': custodia,
        'personal_form': personal_form,
        'title': f'Custodia #{custodia.nro_custodia}'
    }
    return render(request, 'core/ver_custodia.html', context)


@csrf_exempt
def api_crear_custodia(request):
    """API para crear formulario de custodia"""
    if request.method == 'POST':
        try:
            data = request.POST
            
            # Crear el formulario de custodia
            custodia = FormularioCustodia.objects.create(
                nro_hash_id=data.get('nro_hash'),
                caratula=data.get('caratula', ''),
                sumario=data.get('sumario', ''),
                juzgado_fiscalia=data.get('juzgado_fiscalia', ''),
                secretaria=data.get('secretaria', ''),
                otra_informacion=data.get('otra_informacion', ''),
                identificacion_material=data.get('identificacion_material', ''),
                breve_descripcion=data.get('breve_descripcion', ''),
                fecha_hora_incidente=data.get('fecha_hora_incidente'),
                nro_control=data.get('nro_control', 1),
                nro_orden=data.get('nro_orden', 1),
                creado_por=request.user if request.user.is_authenticated else None
            )
            
            return JsonResponse({
                'success': True,
                'custodia_id': custodia.id,
                'nro_custodia': custodia.nro_custodia,
                'message': 'Formulario de custodia creado exitosamente'
            })
            
        except Exception as e:
            return JsonResponse({
                'success': False,
                'error': str(e)
            }, status=400)
    
    return JsonResponse({'error': 'Método no permitido'}, status=405)


@csrf_exempt
def api_agregar_personal_custodia(request, custodia_id):
    """API para agregar personal a una custodia"""
    if request.method == 'POST':
        try:
            custodia = get_object_or_404(FormularioCustodia, id=custodia_id)
            data = request.POST
            
            # Obtener el siguiente número de orden
            ultimo_orden = PersonalCustodia.objects.filter(
                formulario_custodia=custodia
            ).aggregate(max_orden=models.Max('orden'))['max_orden']
            nuevo_orden = (ultimo_orden or 0) + 1
            
            # Crear el personal de custodia
            personal = PersonalCustodia.objects.create(
                formulario_custodia=custodia,
                oficial_id=data.get('oficial_id'),
                funcion=data.get('funcion', ''),
                descripcion=data.get('descripcion', ''),
                orden=nuevo_orden,
                observaciones=data.get('observaciones', '')
            )
            
            return JsonResponse({
                'success': True,
                'personal_id': personal.id,
                'message': 'Personal agregado exitosamente'
            })
            
        except Exception as e:
            return JsonResponse({
                'success': False,
                'error': str(e)
            }, status=400)
    
    return JsonResponse({'error': 'Método no permitido'}, status=405)





# API para AJAX
@api_view(['GET'])
@permission_classes([IsAuthenticated])
def estadisticas_dashboard(request):
    """API para estadísticas del dashboard"""
    data = {
        'formularios_por_estado': {
            estado[0]: FormularioHash.objects.filter(estado=estado[0]).count()
            for estado in FormularioHash._meta.get_field('estado').choices
        },
        'archivos_por_tipo': {
            'imagenes': Archivo.objects.filter(es_imagen=True).count(),
            'videos': Archivo.objects.filter(es_video=True).count(),
            'audios': Archivo.objects.filter(es_audio=True).count(),
            'documentos': Archivo.objects.filter(es_documento=True).count(),
        },
        'formularios_recientes': FormularioHashListSerializer(
            FormularioHash.objects.order_by('-fecha_creacion')[:5],
            many=True
        ).data
    }
    return Response(data)


@api_view(['POST'])
@permission_classes([])  # Quitar autenticación temporalmente
def procesar_carpeta(request):
    """Procesar todos los archivos de una carpeta, similar a la app de escritorio"""
    try:
        # Obtener datos del formulario
        formulario_data = request.data.copy()
        archivos = request.FILES.getlist('archivos')
        
        # Debug: mostrar información recibida
        print(f"Archivos recibidos: {len(archivos)}")
        print(f"Request.FILES keys: {list(request.FILES.keys())}")
        print(f"Request.data keys: {list(request.data.keys())}")
        
        if not archivos:
            return Response(
                {'error': 'No se enviaron archivos'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        # Remover nro_hash si está vacío para que se auto-genere
        if 'nro_hash' in formulario_data and not formulario_data['nro_hash']:
            del formulario_data['nro_hash']
        
        # Crear formulario
        serializer = FormularioHashSerializer(data=formulario_data)
        if not serializer.is_valid():
            print("Validation errors:", serializer.errors)
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
        
        # Manejar usuario autenticado o no
        usuario = request.user if request.user.is_authenticated else None
        formulario = serializer.save(creado_por=usuario)
        
        # Procesar archivos con progreso
        archivos_procesados = []
        total_archivos = len(archivos)
        
        for archivo in archivos:
            # Calcular hash SHA-256 (equivalente a SHA-1 en la app de escritorio)
            hash_sha256 = _calcular_hash_archivo(archivo)
            
            # Verificar si ya existe un archivo con el mismo hash en este formulario
            if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                print(f"Archivo duplicado omitido: {archivo.name} (hash ya existe)")
                continue
            
            # Obtener el siguiente número de orden
            siguiente_orden = formulario.archivos.count() + 1
            
            # Obtener extensión
            extension = os.path.splitext(archivo.name)[1].lower()
            if extension.startswith('.'):
                extension = extension[1:]
            
            # Crear objeto Archivo
            archivo_obj = Archivo.objects.create(
                formulario=formulario,
                nro_orden=siguiente_orden,
                nombre=archivo.name,
                extension=extension,
                peso=archivo.size,
                hash_sha256=hash_sha256,
                tipo_mime=archivo.content_type or ''
            )
            
            archivos_procesados.append(archivo_obj)
        
        # Recalcular contadores del formulario
        formulario.calcular_contadores()
        
        # Guardar ID del formulario en la sesión para mostrar resultados
        request.session['formulario_creado_id'] = formulario.id
        
        # Retornar formulario completo con estadísticas
        formulario_serializer = FormularioHashSerializer(formulario)
        return Response({
            'formulario': formulario_serializer.data,
            'archivos_procesados': len(archivos_procesados),
            'peso_total': formulario.peso_total_formateado,
            'contadores': {
                'imagenes': formulario.imagenes,
                'clips': formulario.clips,
                'audio': formulario.audio,
                'texto': formulario.texto,
                'varios': formulario.varios
            },
            'redirect_url': f'/form_hash/{formulario.id}/'
        }, status=status.HTTP_201_CREATED)
        
    except Exception as e:
        print(f"Error en procesar_carpeta: {str(e)}")
        return Response(
            {'error': f'Error al procesar carpeta: {str(e)}'}, 
            status=status.HTTP_500_INTERNAL_SERVER_ERROR
        )


@api_view(['POST'])
@permission_classes([])  # Quitar autenticación temporalmente
def agregar_archivos_formulario(request, formulario_id):
    """API para agregar archivos a un formulario existente"""
    try:
        formulario = get_object_or_404(FormularioHash, id=formulario_id)
        archivos = request.FILES.getlist('archivos')
        
        if not archivos:
            return Response(
                {'error': 'No se enviaron archivos'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        # Procesar archivos
        archivos_procesados = []
        siguiente_orden = formulario.archivos.count() + 1
        
        for archivo in archivos:
            # Calcular hash SHA-256
            hash_sha256 = _calcular_hash_archivo(archivo)
            
            # Verificar si ya existe un archivo con el mismo hash en este formulario
            if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                print(f"Archivo duplicado omitido: {archivo.name} (hash ya existe)")
                continue
                
            # Obtener extensión del archivo
            extension = os.path.splitext(archivo.name)[1].lower()
            if extension.startswith('.'):
                extension = extension[1:]
            
            # Crear objeto Archivo
            archivo_obj = Archivo.objects.create(
                formulario=formulario,
                nro_orden=siguiente_orden,
                nombre=archivo.name,
                extension=extension,
                peso=archivo.size,
                hash_sha256=hash_sha256,
                tipo_mime=archivo.content_type or ''
            )
            
            archivos_procesados.append(archivo_obj)
            siguiente_orden += 1
        
        # Recalcular contadores del formulario
        formulario.calcular_contadores()
        
        # Retornar respuesta
        return Response({
            'success': True,
            'formulario_id': formulario.id,
            'archivos_procesados': len(archivos_procesados),
            'peso_total': formulario.peso_total_formateado,
            'contadores': {
                'imagenes': formulario.imagenes,
                'clips': formulario.clips,
                'audio': formulario.audio,
                'texto': formulario.texto,
                'varios': formulario.varios
            }
        }, status=status.HTTP_200_OK)
        
    except Exception as e:
        return Response(
            {'error': f'Error al agregar archivos: {str(e)}'}, 
            status=status.HTTP_500_INTERNAL_SERVER_ERROR
        )


@api_view(['GET'])
@permission_classes([])  # Remove authentication for detail view
def api_obtener_detalles_formulario(request, formulario_id):
    """API para obtener detalles completos de un formulario"""
    try:
        formulario = get_object_or_404(
            FormularioHash.objects.select_related(
                'oficial_entrega__jerarquia', 'oficial_recibe__jerarquia',
                'tipo_procedimiento', 'creado_por'
            ).prefetch_related('archivos'),
            id=formulario_id
        )
        
        # Serializar formulario con todos los detalles
        serializer = FormularioHashSerializer(formulario)
        
        return Response({
            'success': True,
            'formulario': serializer.data
        }, status=status.HTTP_200_OK)
        
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al obtener detalles: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['POST'])
@permission_classes([])  # Remove authentication for now
def api_crear_oficial(request):
    """API para crear un nuevo oficial"""
    try:
        data = request.data
        
        # Validate required fields
        required_fields = ['legajo', 'nombre', 'jerarquia', 'destino']
        for field in required_fields:
            if not data.get(field):
                return Response(
                    {'error': f'El campo {field} es requerido'}, 
                    status=status.HTTP_400_BAD_REQUEST
                )
        
        # Check if legajo already exists
        if Oficial.objects.filter(legajo=data['legajo']).exists():
            return Response(
                {'error': 'Ya existe un oficial con ese legajo'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        # Create the official
        oficial = Oficial.objects.create(
            legajo=data['legajo'],
            nombre=data['nombre'],
            jerarquia_id=data['jerarquia'],
            destino_id=data['destino'],
            activo=True
        )
        
        # Return the created official
        serializer = OficialSerializer(oficial)
        return Response({
            'success': True,
            'oficial': serializer.data,
            'message': 'Oficial creado exitosamente'
        }, status=status.HTTP_201_CREATED)
        
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al crear oficial: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['GET'])
@permission_classes([])  # Remove authentication for now
def api_buscar_oficiales(request):
    """API para buscar oficiales con autocompletado"""
    try:
        termino = request.query_params.get('q', '').strip()
        if len(termino) < 2:
            return Response([])
        
        # Search in name, legajo and formatted name
        oficiales = Oficial.objects.select_related('jerarquia', 'destino').filter(
            models.Q(nombre__icontains=termino) |
            models.Q(legajo__icontains=termino),
            activo=True
        )[:10]
        
        # Return formatted data for select2
        results = []
        for oficial in oficiales:
            results.append({
                'id': oficial.id,
                'text': oficial.nombre_completo_formateado,
                'legajo': oficial.legajo,
                'nombre': oficial.nombre,
                'jerarquia': oficial.jerarquia.abreviatura,
                'destino': oficial.destino.nombre
            })
        
        return Response(results)
        
    except Exception as e:
        return Response({
            'error': f'Error al buscar oficiales: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['GET'])
@permission_classes([])  # Remove authentication for now  
def api_obtener_jerarquias_destinos(request):
    """API para obtener jerarquías y destinos para el formulario de oficial"""
    try:
        jerarquias = Jerarquia.objects.all().order_by('orden')
        destinos = Destino.objects.filter(activo=True).order_by('nombre')
        
        return Response({
            'jerarquias': [{'id': j.id, 'nombre': j.nombre, 'abrev': j.abreviatura} for j in jerarquias],
            'destinos': [{'id': d.id, 'nombre': d.nombre} for d in destinos]
        })
        
    except Exception as e:
        return Response({
            'error': f'Error al obtener jerarquías y destinos: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['DELETE'])
@permission_classes([IsAuthenticated])
def api_eliminar_formulario(request, formulario_id):
    """API para eliminar un formulario por AJAX"""
    try:
        formulario = get_object_or_404(FormularioHash, id=formulario_id)
        
        # Obtener información antes de eliminar
        nro_hash = formulario.nro_hash
        total_archivos = formulario.total_archivos
        
        # Eliminar formulario (esto también eliminará los archivos por CASCADE)
        formulario.delete()
        
        return Response({
            'success': True,
            'message': f'Formulario #{nro_hash} eliminado exitosamente',
            'formulario_id': formulario_id,
            'total_archivos_eliminados': total_archivos
        }, status=status.HTTP_200_OK)
        
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al eliminar formulario: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)
