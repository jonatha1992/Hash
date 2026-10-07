import os
import hashlib
from django.shortcuts import render, get_object_or_404, redirect
from django.contrib.auth.decorators import login_required
from django.db import models

from .models import (
    Jerarquia, Destino, Oficial, TipoProcedimiento, FormularioHash, 
    Archivo, FormularioCustodia, PersonalCustodia
)
from .forms import (
    FormularioHashForm, FormularioCustodiaForm, PersonalCustodiaForm
)

# Re-export API views and viewsets from modularized api_views
from .api_views import (
    _calcular_hash_archivo,
    JerarquiaViewSet,
    OficialViewSet,
    TipoProcedimientoViewSet,
    FormularioHashViewSet,
    ArchivoViewSet,
    api_crear_custodia,
    api_agregar_personal_custodia,
    estadisticas_dashboard,
    procesar_carpeta,
    agregar_archivos_formulario,
    api_obtener_detalles_formulario,
    api_eliminar_formulario,
    api_crear_oficial_rapido,
    api_crear_jerarquia_rapida,
    api_crear_destino_rapido,
    api_obtener_opciones_formulario,
)


# Template Views para Hash
@login_required
def dashboard(request):
    """Dashboard principal"""
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


@login_required
def hash(request, formulario_id=None):
    """Vista unificada del modo escritorio - permite crear y editar formularios"""
    oficiales = Oficial.objects.select_related('jerarquia', 'destino').filter(activo=True)
    jerarquias = Jerarquia.objects.all().order_by('orden')
    destinos = Destino.objects.filter(activo=True).order_by('nombre')
    
    if formulario_id:
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
        formulario = None
        modo = 'creacion'
        ultimo_hash = FormularioHash.objects.aggregate(
            max_hash=models.Max('nro_hash')
        )['max_hash']
        siguiente_hash = (ultimo_hash or 0) + 1
    
    hash_creado_id = request.session.get('hash_creado_id')
    if hash_creado_id and modo == 'creacion':
        try:
            hash_creado = FormularioHash.objects.select_related(
                'oficial_entrega__jerarquia', 'oficial_entrega__destino',
                'oficial_recibe__jerarquia', 'oficial_recibe__destino',
                'tipo_procedimiento'
            ).prefetch_related('archivos').get(id=hash_creado_id)
            modo = 'resultado'
            formulario = hash_creado
            del request.session['hash_creado_id']
        except FormularioHash.DoesNotExist:
            pass
    
    context = {
        'formulario': formulario,
        'oficiales': oficiales,
        'jerarquias': jerarquias,
        'destinos': destinos,
        'siguiente_hash': siguiente_hash,
        'modo': modo,
        'title': f'Hash - {"Editar" if formulario and modo == "edicion" else "Nuevo" if modo == "creacion" else "Resultado"} Hash'
    }
    return render(request, 'core/hash.html', context)


@login_required
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

            return redirect('core:hash_editar', formulario_id=formulario.id)
    else:
        form = FormularioHashForm()

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


@login_required
def eliminar_hash(request, formulario_id):
    """Vista para eliminar un hash"""
    formulario = get_object_or_404(FormularioHash, id=formulario_id)
    
    if request.method == 'POST':
        formulario.delete()
        return redirect('core:lista_hashes')
    
    context = {
        'formulario': formulario,
        'title': f'Eliminar Formulario #{formulario.nro_hash}'
    }
    return render(request, 'core/eliminar_hash.html', context)


@login_required
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


@login_required
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
@login_required
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


@login_required
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


@login_required
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
