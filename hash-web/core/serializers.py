from rest_framework import serializers
from .models import Jerarquia, Oficial, TipoProcedimiento, FormularioHash, Archivo


class JerarquiaSerializer(serializers.ModelSerializer):
    total_oficiales = serializers.SerializerMethodField()
    
    class Meta:
        model = Jerarquia
        fields = ['id', 'jerarquia', 'abreviatura', 'orden', 'total_oficiales']
    
    def get_total_oficiales(self, obj):
        return obj.oficiales.count()


class OficialSerializer(serializers.ModelSerializer):
    jerarquia_nombre = serializers.CharField(source='jerarquia.jerarquia', read_only=True)
    jerarquia_abrev = serializers.CharField(source='jerarquia.abreviatura', read_only=True)
    nombre_completo_formateado = serializers.CharField(read_only=True)
    destino_nombre = serializers.CharField(source='destino.nombre', read_only=True)
    
    class Meta:
        model = Oficial
        fields = [
            'id', 'legajo', 'nombre', 'jerarquia', 'jerarquia_nombre', 
            'jerarquia_abrev', 'destino', 'destino_nombre', 'activo', 'fecha_ingreso', 
            'nombre_completo_formateado'
        ]


class OficialBasicoSerializer(serializers.ModelSerializer):
    """Serializer simplificado para oficiales en listas"""
    display_name = serializers.SerializerMethodField()
    
    class Meta:
        model = Oficial
        fields = ['id', 'legajo', 'nombre', 'display_name']
    
    def get_display_name(self, obj):
        return str(obj)


class TipoProcedimientoSerializer(serializers.ModelSerializer):
    class Meta:
        model = TipoProcedimiento
        fields = ['id', 'nombre', 'descripcion', 'activo']


class ArchivoSerializer(serializers.ModelSerializer):
    tipo_archivo = serializers.SerializerMethodField()
    
    class Meta:
        model = Archivo
        fields = [
            'id', 'nro_orden', 'nombre', 'extension', 'peso', 'peso_formateado',
            'hash_sha256', 'tipo_mime', 'ruta_original', 'fecha_modificacion_original',
            'es_imagen', 'es_video', 'es_audio', 'es_documento', 'tipo_archivo'
        ]
    
    def get_tipo_archivo(self, obj):
        if obj.es_imagen:
            return "imagen"
        elif obj.es_video:
            return "video"
        elif obj.es_audio:
            return "audio"
        elif obj.es_documento:
            return "documento"
        else:
            return "varios"


class FormularioHashSerializer(serializers.ModelSerializer):
    oficial_entrega_data = OficialBasicoSerializer(source='oficial_entrega', read_only=True)
    oficial_recibe_data = OficialBasicoSerializer(source='oficial_recibe', read_only=True)
    tipo_procedimiento_nombre = serializers.CharField(source='tipo_procedimiento.nombre', read_only=True)
    total_archivos = serializers.ReadOnlyField()
    creado_por_nombre = serializers.CharField(source='creado_por.get_full_name', read_only=True)
    archivos = ArchivoSerializer(many=True, read_only=True)
    
    class Meta:
        model = FormularioHash
        fields = [
            'id', 'nro_hash', 'tipo', 'procedimiento', 'tipo_procedimiento', 'tipo_procedimiento_nombre',
            'oficial_entrega', 'oficial_entrega_data', 'oficial_recibe', 'oficial_recibe_data',
            'imagenes', 'clips', 'audio', 'texto', 'varios', 'total_archivos',
            'peso_total_bytes', 'peso_total_formateado', 'observaciones',
            'fecha_creacion', 'fecha_modificacion', 'creado_por', 'creado_por_nombre',
            'estado', 'archivos'
        ]
        read_only_fields = [
            'imagenes', 'clips', 'audio', 'texto', 'varios', 'total_archivos',
            'peso_total_bytes', 'peso_total_formateado', 'fecha_creacion', 
            'fecha_modificacion', 'creado_por'
        ]


class FormularioHashListSerializer(serializers.ModelSerializer):
    """Serializer simplificado para listas de formularios"""
    oficial_entrega_data = OficialBasicoSerializer(source='oficial_entrega', read_only=True)
    oficial_recibe_data = OficialBasicoSerializer(source='oficial_recibe', read_only=True)
    total_archivos = serializers.ReadOnlyField()
    
    class Meta:
        model = FormularioHash
        fields = [
            'id', 'nro_hash', 'tipo', 'procedimiento', 'oficial_entrega_data', 
            'oficial_recibe_data', 'total_archivos', 'peso_total_formateado',
            'fecha_creacion', 'estado'
        ]


class ArchivoUploadSerializer(serializers.Serializer):
    """Serializer para subida de archivos"""
    archivos = serializers.ListField(
        child=serializers.FileField(),
        write_only=True
    )
    formulario_id = serializers.IntegerField()
    
    def validate_formulario_id(self, value):
        try:
            FormularioHash.objects.get(id=value)
        except FormularioHash.DoesNotExist:
            raise serializers.ValidationError("Formulario no encontrado")
        return value
