from django.db import models
from django.contrib.auth.models import User
import hashlib
import os


class Jerarquia(models.Model):
    """Modelo para jerarquías militares/policiales"""
    nombre = models.CharField(max_length=100, unique=True)
    abreviatura = models.CharField(max_length=20)
    orden = models.PositiveIntegerField(default=0, help_text="Orden jerárquico (menor = mayor jerarquía)")
    
    class Meta:
        verbose_name = "Jerarquía"
        verbose_name_plural = "Jerarquías"
        ordering = ['orden']
    
    def __str__(self):
        return f"{self.abreviatura} - {self.nombre}"


class Destino(models.Model):
    """Modelo para destinos/unidades"""
    nombre = models.CharField(max_length=200, unique=True)
    activo = models.BooleanField(default=True)
    
    class Meta:
        verbose_name = "Destino"
        verbose_name_plural = "Destinos"
        ordering = ['nombre']
    
    def __str__(self):
        return self.nombre


class Oficial(models.Model):
    """Modelo para oficiales"""
    legajo = models.CharField(max_length=20, unique=True)
    nombre = models.CharField(max_length=200)
    jerarquia = models.ForeignKey(Jerarquia, on_delete=models.CASCADE, related_name='oficiales', null=True, blank=True)
    destino = models.ForeignKey(Destino, on_delete=models.CASCADE, related_name='oficiales')
    activo = models.BooleanField(default=True)
    
    # Campos adicionales opcionales
    user = models.OneToOneField(User, on_delete=models.CASCADE, null=True, blank=True)
    fecha_ingreso = models.DateField(null=True, blank=True)
    
    class Meta:
        verbose_name = "Oficial"
        verbose_name_plural = "Oficiales"
        ordering = ['-jerarquia__orden', 'nombre']  # Nulls last, then by hierarchy order and name
    
    def __str__(self):
        if self.jerarquia:
            return f"{self.jerarquia.abreviatura} {self.nombre}"
        else:
            return f"Civil {self.nombre}"
    
    def get_full_name(self):
        """Retorna el nombre completo del oficial"""
        return self.nombre
    
    @property
    def nombre_completo_formateado(self):
        if self.jerarquia:
            return f"Leg. {self.legajo} - {self.jerarquia.abreviatura} {self.nombre}"
        else:
            return f"Leg. {self.legajo} - Civil {self.nombre}"
    
    @property
    def es_civil(self):
        """Retorna True si es civil (sin jerarquía)"""
        return self.jerarquia is None


class TipoProcedimiento(models.Model):
    """Tipos de procedimientos disponibles"""
    nombre = models.CharField(max_length=100, unique=True)
    descripcion = models.TextField(blank=True)
    activo = models.BooleanField(default=True)
    
    class Meta:
        verbose_name = "Tipo de Procedimiento"
        verbose_name_plural = "Tipos de Procedimientos"
        ordering = ['nombre']
    
    def __str__(self):
        return self.nombre


class FormularioHash(models.Model):
    """Formulario principal de hash"""
    TIPO_CHOICES = [
        ('PROCEDIMIENTO', 'Procedimiento'),
        ('VUELO', 'Vuelo'),
    ]
    
    ESTADO_CHOICES = [
        ('BORRADOR', 'Borrador'),
        ('FINALIZADO', 'Finalizado'),
        ('ARCHIVADO', 'Archivado'),
    ]
    
    # Campos principales
    nro_hash = models.PositiveIntegerField(unique=True)
    tipo = models.CharField(max_length=20, choices=TIPO_CHOICES, default='PROCEDIMIENTO')
    procedimiento = models.CharField(max_length=300)
    tipo_procedimiento = models.ForeignKey(TipoProcedimiento, on_delete=models.SET_NULL, null=True, blank=True)
    
    # Oficiales
    oficial_entrega = models.ForeignKey(
        Oficial, 
        on_delete=models.CASCADE, 
        related_name='formularios_entregados'
    )
    oficial_recibe = models.ForeignKey(
        Oficial, 
        on_delete=models.CASCADE, 
        related_name='formularios_recibidos'
    )
    
    # Contadores de archivos
    imagenes = models.PositiveIntegerField(default=0)
    clips = models.PositiveIntegerField(default=0)
    audio = models.PositiveIntegerField(default=0)
    texto = models.PositiveIntegerField(default=0)
    varios = models.PositiveIntegerField(default=0)
    
    # Información adicional
    peso_total_bytes = models.BigIntegerField(default=0)
    peso_total_formateado = models.CharField(max_length=50, blank=True)
    observaciones = models.TextField(blank=True)
    
    # Estado
    estado = models.CharField(max_length=20, choices=ESTADO_CHOICES, default='BORRADOR')
    
    # Timestamps
    fecha_creacion = models.DateTimeField(auto_now_add=True)
    fecha_modificacion = models.DateTimeField(auto_now=True)
    creado_por = models.ForeignKey(User, on_delete=models.SET_NULL, related_name='formularios_creados', null=True, blank=True)
    
    class Meta:
        verbose_name = "Formulario Hash"
        verbose_name_plural = "Formularios Hash"
        ordering = ['-fecha_creacion']
    
    def __str__(self):
        return f"Hash #{self.nro_hash} - {self.procedimiento}"
    
    def save(self, *args, **kwargs):
        # Auto-generar número de hash si no existe
        if not self.nro_hash:
            ultimo_hash = FormularioHash.objects.aggregate(
                max_hash=models.Max('nro_hash')
            )['max_hash']
            self.nro_hash = (ultimo_hash or 0) + 1
        
        super().save(*args, **kwargs)
    
    def calcular_contadores(self):
        """Recalcula los contadores de archivos y peso total"""
        archivos = self.archivos.all()
        
        # Resetear contadores
        self.imagenes = 0
        self.clips = 0
        self.audio = 0
        self.texto = 0
        self.varios = 0
        self.peso_total_bytes = 0
        
        # Contar archivos por tipo
        for archivo in archivos:
            self.peso_total_bytes += archivo.peso
            
            if archivo.es_imagen:
                self.imagenes += 1
            elif archivo.es_video:
                self.clips += 1
            elif archivo.es_audio:
                self.audio += 1
            elif archivo.es_documento:
                self.texto += 1
            else:
                self.varios += 1
        
        # Formatear peso total
        self.peso_total_formateado = self._formatear_bytes(self.peso_total_bytes)
        self.save()
    
    def _formatear_bytes(self, bytes_size):
        """Convierte bytes a formato legible"""
        if bytes_size == 0:
            return "0 B"
        
        for unit in ['B', 'KB', 'MB', 'GB', 'TB']:
            if bytes_size < 1024.0:
                return f"{bytes_size:.2f} {unit}"
            bytes_size /= 1024.0
        
        return f"{bytes_size:.2f} PB"
    
    @property
    def total_archivos(self):
        return self.imagenes + self.clips + self.audio + self.texto + self.varios


class Archivo(models.Model):
    """Modelo para archivos procesados"""
    formulario = models.ForeignKey(FormularioHash, on_delete=models.CASCADE, related_name='archivos')
    nro_orden = models.PositiveIntegerField()
    nombre = models.CharField(max_length=500)
    extension = models.CharField(max_length=20)
    peso = models.BigIntegerField()  # En bytes
    peso_formateado = models.CharField(max_length=50, blank=True)
    hash_sha256 = models.CharField(max_length=64, blank=True)
    
    # Información adicional del archivo
    ruta_original = models.TextField(blank=True)
    fecha_modificacion_original = models.DateTimeField(null=True, blank=True)
    
    # Metadatos adicionales
    tipo_mime = models.CharField(max_length=100, blank=True)
    es_imagen = models.BooleanField(default=False)
    es_video = models.BooleanField(default=False)
    es_audio = models.BooleanField(default=False)
    es_documento = models.BooleanField(default=False)
    
    class Meta:
        verbose_name = "Archivo"
        verbose_name_plural = "Archivos"
        ordering = ['formulario', 'nro_orden']
        unique_together = [
            ['formulario', 'nro_orden'],
            ['formulario', 'hash_sha256']  # Prevent duplicate files with same hash in same formulario
        ]
    
    def __str__(self):
        return f"{self.nro_orden}. {self.nombre}"
    
    def save(self, *args, **kwargs):
        # Formatear peso al guardar
        if self.peso:
            self.peso_formateado = self._formatear_bytes(self.peso)
        
        # Determinar tipo de archivo
        if self.extension:
            ext = self.extension.lower()
            self.es_imagen = ext in ['.jpg', '.jpeg', '.png', '.bmp', '.gif', '.tiff', '.webp']
            self.es_video = ext in ['.mp4', '.avi', '.mov', '.wmv', '.flv', '.mkv', '.m4v', '.3gp']
            self.es_audio = ext in ['.mp3', '.wav', '.flac', '.aac', '.ogg', '.wma', '.m4a']
            self.es_documento = ext in ['.pdf', '.txt', '.docx', '.doc', '.xlsx', '.xls', '.pptx', '.ppt', '.rtf']
        
        super().save(*args, **kwargs)
        
        # Recalcular contadores del formulario
        if self.formulario:
            self.formulario.calcular_contadores()
    
    def _formatear_bytes(self, bytes_size):
        """Convierte bytes a formato legible"""
        if bytes_size == 0:
            return "0 B"
        
        for unit in ['B', 'KB', 'MB', 'GB', 'TB']:
            if bytes_size < 1024.0:
                return f"{bytes_size:.2f} {unit}"
            bytes_size /= 1024.0
        
        return f"{bytes_size:.2f} PB"
    
    @staticmethod
    def calcular_hash_archivo(archivo_contenido):
        """Calcula el hash SHA-256 de un archivo desde su contenido"""
        hash_sha256 = hashlib.sha256()
        
        # Si es un archivo de Django (UploadedFile)
        if hasattr(archivo_contenido, 'chunks'):
            for chunk in archivo_contenido.chunks():
                hash_sha256.update(chunk)
        else:
            # Si es contenido directo
            hash_sha256.update(archivo_contenido)
        
        return hash_sha256.hexdigest().upper()


class FormularioCustodia(models.Model):
    """Formulario de cadena de custodia"""
    nro_custodia = models.PositiveIntegerField(unique=True)
    nro_hash = models.ForeignKey(FormularioHash, on_delete=models.CASCADE, related_name='custodias')
    
    # Información del caso
    caratula = models.CharField(max_length=500)
    sumario = models.CharField(max_length=300, blank=True)
    juzgado_fiscalia = models.CharField(max_length=300, blank=True)
    secretaria = models.CharField(max_length=300, blank=True)
    otra_informacion = models.TextField(blank=True)
    identificacion_material = models.TextField(blank=True)
    breve_descripcion = models.TextField(blank=True)
    
    # Fechas y horas
    fecha_hora_incidente = models.DateTimeField()
    fecha_hora_custodia = models.DateTimeField(auto_now_add=True)
    
    # Control
    nro_control = models.PositiveIntegerField(default=1)
    nro_orden = models.PositiveIntegerField(default=1)
    
    # Estado
    estado = models.CharField(
        max_length=20,
        choices=[
            ('BORRADOR', 'Borrador'),
            ('FINALIZADO', 'Finalizado'),
            ('ARCHIVADO', 'Archivado'),
        ],
        default='BORRADOR'
    )
    
    # Timestamps
    fecha_creacion = models.DateTimeField(auto_now_add=True)
    fecha_modificacion = models.DateTimeField(auto_now=True)
    creado_por = models.ForeignKey(User, on_delete=models.SET_NULL, null=True, blank=True)
    
    class Meta:
        verbose_name = "Formulario de Custodia"
        verbose_name_plural = "Formularios de Custodia"
        ordering = ['-fecha_creacion']
    
    def __str__(self):
        return f"Custodia #{self.nro_custodia} - Hash #{self.nro_hash.nro_hash}"
    
    def save(self, *args, **kwargs):
        # Auto-generar número de custodia si no existe
        if not self.nro_custodia:
            ultimo_custodia = FormularioCustodia.objects.aggregate(
                max_custodia=models.Max('nro_custodia')
            )['max_custodia']
            self.nro_custodia = (ultimo_custodia or 0) + 1
        
        super().save(*args, **kwargs)


class PersonalCustodia(models.Model):
    """Personal interviniente en la cadena de custodia"""
    formulario_custodia = models.ForeignKey(FormularioCustodia, on_delete=models.CASCADE, related_name='personal')
    oficial = models.ForeignKey(Oficial, on_delete=models.CASCADE)
    
    # Información específica de la custodia
    funcion = models.CharField(max_length=200, help_text="Función del oficial en la custodia")
    descripcion = models.TextField(help_text="Descripción de las actividades realizadas")
    orden = models.PositiveIntegerField(default=1)
    
    # Fechas de intervención
    fecha_hora_intervencion = models.DateTimeField(auto_now_add=True)
    observaciones = models.TextField(blank=True)
    
    class Meta:
        verbose_name = "Personal de Custodia"
        verbose_name_plural = "Personal de Custodia"
        ordering = ['formulario_custodia', 'orden']
        unique_together = ['formulario_custodia', 'orden']
    
    def __str__(self):
        return f"{self.oficial} - {self.funcion}"


