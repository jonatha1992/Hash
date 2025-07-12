from django.contrib import admin
from django.utils.html import format_html
from django.urls import reverse
from django.utils.safestring import mark_safe
from .models import (Jerarquia, Destino, Oficial, TipoProcedimiento, FormularioHash, 
                     Archivo, FormularioCustodia, PersonalCustodia)


@admin.register(Jerarquia)
class JerarquiaAdmin(admin.ModelAdmin):
    list_display = ['abreviatura', 'nombre', 'orden']
    list_editable = ['orden']
    search_fields = ['nombre', 'abreviatura']
    ordering = ['orden']


@admin.register(Destino)
class DestinoAdmin(admin.ModelAdmin):
    list_display = ['nombre', 'activo', 'total_oficiales']
    list_filter = ['activo']
    search_fields = ['nombre']
    list_editable = ['activo']
    ordering = ['nombre']
    
    def total_oficiales(self, obj):
        return obj.oficiales.count()
    total_oficiales.short_description = 'Total Oficiales'


@admin.register(Oficial)
class OficialAdmin(admin.ModelAdmin):
    list_display = ['legajo', 'nombre', 'jerarquia', 'destino', 'activo', 'total_formularios']
    list_filter = ['jerarquia', 'activo', 'destino']
    search_fields = ['legajo', 'nombre', 'destino__nombre']
    list_editable = ['activo']
    ordering = ['jerarquia__orden', 'nombre']
    
    fieldsets = (
        ('Información Personal', {
            'fields': ('legajo', 'nombre', 'jerarquia', 'destino')
        }),
        ('Información Adicional', {
            'fields': ('fecha_ingreso', 'activo')
        }),
        ('Usuario del Sistema', {
            'fields': ('user',),
            'classes': ('collapse',)
        }),
    )
    
    def total_formularios(self, obj):
        total_entregados = obj.formularios_entregados.count()
        total_recibidos = obj.formularios_recibidos.count()
        return format_html(
            '<span style="color: blue;">E: {}</span> | <span style="color: green;">R: {}</span>',
            total_entregados, total_recibidos
        )
    total_formularios.short_description = 'Formularios (E/R)'


class PersonalCustodiaInline(admin.TabularInline):
    model = PersonalCustodia
    extra = 1
    fields = ['oficial', 'funcion', 'descripcion', 'orden', 'observaciones']


@admin.register(FormularioCustodia)
class FormularioCustodiaAdmin(admin.ModelAdmin):
    list_display = [
        'nro_custodia', 'nro_hash_link', 'caratula_truncada', 'estado',
        'fecha_hora_incidente', 'total_personal', 'fecha_creacion'
    ]
    list_filter = ['estado', 'fecha_hora_incidente', 'fecha_creacion']
    search_fields = ['nro_custodia', 'caratula', 'sumario', 'nro_hash__nro_hash']
    readonly_fields = ['fecha_creacion', 'fecha_modificacion']
    
    fieldsets = (
        ('Información Principal', {
            'fields': ('nro_custodia', 'nro_hash', 'estado')
        }),
        ('Información del Caso', {
            'fields': ('caratula', 'sumario', 'juzgado_fiscalia', 'secretaria')
        }),
        ('Fechas y Control', {
            'fields': (
                ('fecha_hora_incidente', 'fecha_hora_custodia'),
                ('nro_control', 'nro_orden')
            )
        }),
        ('Información Adicional', {
            'fields': ('otra_informacion', 'identificacion_material', 'breve_descripcion'),
            'classes': ('collapse',)
        }),
        ('Sistema', {
            'fields': ('creado_por', 'fecha_creacion', 'fecha_modificacion'),
            'classes': ('collapse',)
        }),
    )
    
    inlines = [PersonalCustodiaInline]
    
    def caratula_truncada(self, obj):
        if len(obj.caratula) > 50:
            return obj.caratula[:47] + "..."
        return obj.caratula
    caratula_truncada.short_description = 'Carátula'
    
    def nro_hash_link(self, obj):
        url = reverse('admin:core_formulariohash_change', args=[obj.nro_hash.pk])
        return format_html('<a href="{}">Hash #{}</a>', url, obj.nro_hash.nro_hash)
    nro_hash_link.short_description = 'Hash'
    
    def total_personal(self, obj):
        return obj.personal.count()
    total_personal.short_description = 'Personal'
    
    def save_model(self, request, obj, form, change):
        if not change:  # Si es un nuevo objeto
            obj.creado_por = request.user
        super().save_model(request, obj, form, change)


@admin.register(PersonalCustodia)
class PersonalCustodiaAdmin(admin.ModelAdmin):
    list_display = [
        'formulario_custodia', 'oficial', 'funcion', 'orden', 'fecha_hora_intervencion'
    ]
    list_filter = ['funcion', 'fecha_hora_intervencion', 'oficial__jerarquia']
    search_fields = [
        'formulario_custodia__nro_custodia', 'oficial__nombre', 
        'funcion', 'descripcion'
    ]
    
    fieldsets = (
        ('Información Principal', {
            'fields': ('formulario_custodia', 'oficial', 'orden')
        }),
        ('Función y Descripción', {
            'fields': ('funcion', 'descripcion')
        }),
        ('Información Adicional', {
            'fields': ('fecha_hora_intervencion', 'observaciones')
        }),
    )




@admin.register(TipoProcedimiento)
class TipoProcedimientoAdmin(admin.ModelAdmin):
    list_display = ['nombre', 'descripcion', 'activo', 'total_formularios']
    list_filter = ['activo']
    search_fields = ['nombre', 'descripcion']
    list_editable = ['activo']
    
    def total_formularios(self, obj):
        return obj.formulariohash_set.count()
    total_formularios.short_description = 'Total Formularios'


class ArchivoInline(admin.TabularInline):
    model = Archivo
    extra = 0
    readonly_fields = ['peso_formateado', 'hash_sha256', 'es_imagen', 'es_video', 'es_audio', 'es_documento']
    fields = ['nro_orden', 'nombre', 'extension', 'peso', 'peso_formateado', 'hash_sha256']
    
    def has_add_permission(self, request, obj=None):
        return False


@admin.register(FormularioHash)
class FormularioHashAdmin(admin.ModelAdmin):
    list_display = [
        'nro_hash', 'procedimiento_truncado', 'tipo', 'estado', 
        'oficial_entrega', 'oficial_recibe', 'total_archivos_display', 
        'peso_total_formateado', 'fecha_creacion'
    ]
    list_filter = ['tipo', 'estado', 'tipo_procedimiento', 'fecha_creacion']
    search_fields = ['nro_hash', 'procedimiento', 'oficial_entrega__nombre', 'oficial_recibe__nombre']
    readonly_fields = [
        'imagenes', 'clips', 'audio', 'texto', 'varios', 
        'peso_total_bytes', 'peso_total_formateado', 'total_archivos',
        'fecha_creacion', 'fecha_modificacion'
    ]
    
    fieldsets = (
        ('Información Principal', {
            'fields': ('nro_hash', 'tipo', 'procedimiento', 'tipo_procedimiento', 'estado')
        }),
        ('Oficiales', {
            'fields': ('oficial_entrega', 'oficial_recibe')
        }),
        ('Contadores de Archivos', {
            'fields': (
                ('imagenes', 'clips', 'audio'),
                ('texto', 'varios', 'total_archivos'),
                ('peso_total_bytes', 'peso_total_formateado')
            ),
            'classes': ('collapse',)
        }),
        ('Información Adicional', {
            'fields': ('observaciones', 'creado_por'),
        }),
        ('Timestamps', {
            'fields': ('fecha_creacion', 'fecha_modificacion'),
            'classes': ('collapse',)
        }),
    )
    
    inlines = [ArchivoInline]
    
    def procedimiento_truncado(self, obj):
        if len(obj.procedimiento) > 50:
            return obj.procedimiento[:47] + "..."
        return obj.procedimiento
    procedimiento_truncado.short_description = 'Procedimiento'
    
    def total_archivos_display(self, obj):
        return format_html(
            '<span title="Imágenes: {} | Videos: {} | Audio: {} | Texto: {} | Varios: {}">📁 {}</span>',
            obj.imagenes, obj.clips, obj.audio, obj.texto, obj.varios, obj.total_archivos
        )
    total_archivos_display.short_description = 'Archivos'
    
    def save_model(self, request, obj, form, change):
        if not change:  # Si es un nuevo objeto
            obj.creado_por = request.user
        super().save_model(request, obj, form, change)


@admin.register(Archivo)
class ArchivoAdmin(admin.ModelAdmin):
    list_display = [
        'nro_orden', 'nombre_truncado', 'extension', 'peso_formateado', 
        'tipo_archivo', 'formulario_link', 'hash_truncado'
    ]
    list_filter = ['extension', 'es_imagen', 'es_video', 'es_audio', 'es_documento']
    search_fields = ['nombre', 'hash_sha256', 'formulario__procedimiento']
    readonly_fields = [
        'peso_formateado', 'hash_sha256', 'es_imagen', 'es_video', 
        'es_audio', 'es_documento', 'tipo_mime'
    ]
    
    fieldsets = (
        ('Información del Archivo', {
            'fields': ('formulario', 'nro_orden', 'nombre', 'extension')
        }),
        ('Tamaño y Hash', {
            'fields': ('peso', 'peso_formateado', 'hash_sha256')
        }),
        ('Metadatos', {
            'fields': (
                'tipo_mime', 'ruta_original', 'fecha_modificacion_original',
                ('es_imagen', 'es_video', 'es_audio', 'es_documento')
            ),
            'classes': ('collapse',)
        }),
    )
    
    def nombre_truncado(self, obj):
        if len(obj.nombre) > 40:
            return obj.nombre[:37] + "..."
        return obj.nombre
    nombre_truncado.short_description = 'Nombre'
    
    def formulario_link(self, obj):
        url = reverse('admin:core_formulariohash_change', args=[obj.formulario.pk])
        return format_html('<a href="{}">Hash #{}</a>', url, obj.formulario.nro_hash)
    formulario_link.short_description = 'Formulario'
    
    def hash_truncado(self, obj):
        return format_html(
            '<span title="{}" style="font-family: monospace; font-size: 11px;">{}</span>',
            obj.hash_sha256, obj.hash_sha256[:16] + "..."
        )
    hash_truncado.short_description = 'Hash SHA-256'
    
    def tipo_archivo(self, obj):
        if obj.es_imagen:
            return "🖼️ Imagen"
        elif obj.es_video:
            return "🎥 Video"
        elif obj.es_audio:
            return "🎵 Audio"
        elif obj.es_documento:
            return "📄 Documento"
        else:
            return "📎 Varios"
    tipo_archivo.short_description = 'Tipo'




# Configuración del sitio de administración
admin.site.site_header = "Hash Web - Panel de Administración"
admin.site.site_title = "Hash Web Admin"
admin.site.index_title = "Gestión del Sistema de Hash Forense"
