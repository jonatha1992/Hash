import hashlib
import os
from django.shortcuts import get_object_or_404
from django.contrib.auth.decorators import login_required
from django.http import JsonResponse, HttpResponse
from django.db import models
from rest_framework import viewsets, status
from rest_framework.decorators import action, api_view, permission_classes
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated
from rest_framework.parsers import MultiPartParser, FormParser

from .models import (
    Jerarquia, Destino, Oficial, TipoProcedimiento, FormularioHash, 
    Archivo, FormularioCustodia, PersonalCustodia
)
from .serializers import (
    JerarquiaSerializer, OficialSerializer, TipoProcedimientoSerializer,
    FormularioHashSerializer, FormularioHashListSerializer, ArchivoSerializer,
    ArchivoUploadSerializer
)
from .forms import (
    QuickOficialForm, QuickJerarquiaForm, QuickDestinoForm
)


def _calcular_hash_archivo(archivo):
    """Calcula el hash SHA-256 de un archivo"""
    hash_sha256 = hashlib.sha256()
    for chunk in archivo.chunks():
        hash_sha256.update(chunk)
    return hash_sha256.hexdigest().upper()


# API ViewSets
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
    permission_classes = [IsAuthenticated]
    
    def get_serializer_class(self):
        if self.action == 'list':
            return FormularioHashListSerializer
        return FormularioHashSerializer
    
    def create(self, request, *args, **kwargs):
        """Crear formulario y procesar archivos en una sola operación"""
        archivos = request.FILES.getlist('archivos')
        data = request.data.copy()
        if 'archivos' in data:
            del data['archivos']
        
        if 'nro_hash' in data and not data['nro_hash']:
            del data['nro_hash']
        
        serializer = self.get_serializer(data=data)
        if not serializer.is_valid():
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
        
        usuario = request.user if request.user.is_authenticated else None
        formulario = serializer.save(creado_por=usuario)
        
        if archivos:
            for archivo in archivos:
                hash_sha256 = _calcular_hash_archivo(archivo)
                if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                    continue
                
                siguiente_orden = formulario.archivos.count() + 1
                extension = os.path.splitext(archivo.name)[1].lower()
                if extension.startswith('.'):
                    extension = extension[1:]
                
                Archivo.objects.create(
                    formulario=formulario,
                    nro_orden=siguiente_orden,
                    nombre=archivo.name,
                    extension=extension,
                    peso=archivo.size,
                    hash_sha256=hash_sha256,
                    tipo_mime=getattr(archivo, 'content_type', '') or ''
                )
        
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
            hash_sha256 = _calcular_hash_archivo(archivo)
            if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                continue
                
            extension = os.path.splitext(archivo.name)[1].lower()
            if extension.startswith('.'):
                extension = extension[1:]
            
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
        
        serializer = ArchivoSerializer(archivos_procesados, many=True)
        return Response(serializer.data, status=status.HTTP_201_CREATED)
    
    @action(detail=True, methods=['get'])
    def exportar_excel(self, request, pk=None):
        """Exportar formulario a Excel"""
        formulario = self.get_object()
        
        try:
            import pandas as pd
            from io import BytesIO
            
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
            
            df_info = pd.DataFrame(datos_formulario)
            df_archivos = pd.DataFrame(archivos_data)
            
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
        
        if formulario.estado != 'BORRADOR':
            return Response(
                {'error': 'Solo se pueden eliminar archivos de formularios en borrador'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        archivo.delete()
        archivos_restantes = formulario.archivos.order_by('nro_orden')
        for i, arch in enumerate(archivos_restantes, 1):
            arch.nro_orden = i
            arch.save()
        
        return Response(status=status.HTTP_204_NO_CONTENT)


# Functional API Views
@login_required
def api_crear_custodia(request):
    """API para crear formulario de custodia con validación de autenticación y CSRF"""
    if request.method == 'POST':
        try:
            data = request.POST
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
                creado_por=request.user
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


@login_required
def api_agregar_personal_custodia(request, custodia_id):
    """API para agregar personal a una custodia con validación de autenticación y CSRF"""
    if request.method == 'POST':
        try:
            custodia = get_object_or_404(FormularioCustodia, id=custodia_id)
            data = request.POST
            ultimo_orden = PersonalCustodia.objects.filter(
                formulario_custodia=custodia
            ).aggregate(max_orden=models.Max('orden'))['max_orden']
            nuevo_orden = (ultimo_orden or 0) + 1
            
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
@permission_classes([IsAuthenticated])
def procesar_carpeta(request):
    """Procesar todos los archivos de una carpeta con autenticación obligatoria"""
    try:
        formulario_data = request.data.copy()
        archivos = request.FILES.getlist('archivos')
        
        if not archivos:
            return Response(
                {'error': 'No se enviaron archivos'}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        if 'nro_hash' in formulario_data and not formulario_data['nro_hash']:
            del formulario_data['nro_hash']
        
        serializer = FormularioHashSerializer(data=formulario_data)
        if not serializer.is_valid():
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
        
        formulario = serializer.save(creado_por=request.user)
        archivos_procesados = []
        
        for archivo in archivos:
            hash_sha256 = _calcular_hash_archivo(archivo)
            if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                continue
            
            siguiente_orden = formulario.archivos.count() + 1
            extension = os.path.splitext(archivo.name)[1].lower()
            if extension.startswith('.'):
                extension = extension[1:]
            
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
        
        formulario.calcular_contadores()
        request.session['hash_creado_id'] = formulario.id
        
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
            'redirect_url': f'/hash/{formulario.id}/'
        }, status=status.HTTP_201_CREATED)
        
    except Exception as e:
        return Response(
            {'error': f'Error al procesar carpeta: {str(e)}'}, 
            status=status.HTTP_500_INTERNAL_SERVER_ERROR
        )


@api_view(['POST'])
@permission_classes([IsAuthenticated])
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
        
        archivos_procesados = []
        siguiente_orden = formulario.archivos.count() + 1
        
        for archivo in archivos:
            hash_sha256 = _calcular_hash_archivo(archivo)
            if formulario.archivos.filter(hash_sha256=hash_sha256).exists():
                continue
                
            extension = os.path.splitext(archivo.name)[1].lower()
            if extension.startswith('.'):
                extension = extension[1:]
            
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
        
        formulario.calcular_contadores()
        
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
@permission_classes([IsAuthenticated])
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


@api_view(['DELETE'])
@permission_classes([IsAuthenticated])
def api_eliminar_formulario(request, formulario_id):
    """API para eliminar un formulario por AJAX"""
    try:
        formulario = get_object_or_404(FormularioHash, id=formulario_id)
        nro_hash = formulario.nro_hash
        total_archivos = formulario.total_archivos
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


@api_view(['POST'])
@permission_classes([IsAuthenticated])
def api_crear_oficial_rapido(request):
    """API para crear un oficial rápidamente desde el formulario hash"""
    try:
        form = QuickOficialForm(request.data)
        if form.is_valid():
            oficial = form.save()
            return Response({
                'success': True,
                'oficial': {
                    'id': oficial.id,
                    'nombre': oficial.nombre,
                    'legajo': oficial.legajo,
                    'nombre_completo_formateado': oficial.nombre_completo_formateado,
                    'es_civil': oficial.es_civil
                },
                'message': 'Oficial creado exitosamente'
            }, status=status.HTTP_201_CREATED)
        else:
            return Response({
                'success': False,
                'errors': form.errors
            }, status=status.HTTP_400_BAD_REQUEST)
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al crear oficial: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['POST'])
@permission_classes([IsAuthenticated])
def api_crear_jerarquia_rapida(request):
    """API para crear una jerarquía rápidamente"""
    try:
        form = QuickJerarquiaForm(request.data)
        if form.is_valid():
            jerarquia = form.save()
            return Response({
                'success': True,
                'jerarquia': {
                    'id': jerarquia.id,
                    'nombre': jerarquia.nombre,
                    'abreviatura': jerarquia.abreviatura,
                    'orden': jerarquia.orden
                },
                'message': 'Jerarquía creada exitosamente'
            }, status=status.HTTP_201_CREATED)
        else:
            return Response({
                'success': False,
                'errors': form.errors
            }, status=status.HTTP_400_BAD_REQUEST)
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al crear jerarquía: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['POST'])
@permission_classes([IsAuthenticated])
def api_crear_destino_rapido(request):
    """API para crear un destino rápidamente"""
    try:
        form = QuickDestinoForm(request.data)
        if form.is_valid():
            destino = form.save()
            return Response({
                'success': True,
                'destino': {
                    'id': destino.id,
                    'nombre': destino.nombre
                },
                'message': 'Destino creado exitosamente'
            }, status=status.HTTP_201_CREATED)
        else:
            return Response({
                'success': False,
                'errors': form.errors
            }, status=status.HTTP_400_BAD_REQUEST)
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al crear destino: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)


@api_view(['GET'])
@permission_classes([IsAuthenticated])
def api_obtener_opciones_formulario(request):
    """API para obtener las opciones actualizadas de oficiales, jerarquías y destinos"""
    try:
        oficiales = Oficial.objects.filter(activo=True).select_related('jerarquia', 'destino')
        jerarquias = Jerarquia.objects.all().order_by('orden')
        destinos = Destino.objects.filter(activo=True).order_by('nombre')
        
        return Response({
            'success': True,
            'oficiales': [
                {
                    'id': oficial.id,
                    'nombre_completo_formateado': oficial.nombre_completo_formateado,
                    'es_civil': oficial.es_civil
                }
                for oficial in oficiales
            ],
            'jerarquias': [
                {
                    'id': jerarquia.id,
                    'nombre': jerarquia.nombre,
                    'abreviatura': jerarquia.abreviatura
                }
                for jerarquia in jerarquias
            ],
            'destinos': [
                {
                    'id': destino.id,
                    'nombre': destino.nombre
                }
                for destino in destinos
            ]
        }, status=status.HTTP_200_OK)
    except Exception as e:
        return Response({
            'success': False,
            'error': f'Error al obtener opciones: {str(e)}'
        }, status=status.HTTP_500_INTERNAL_SERVER_ERROR)
