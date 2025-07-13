from django.core.management.base import BaseCommand
from django.contrib.auth.models import User
from core.models import Jerarquia, Destino, Oficial, TipoProcedimiento, FormularioHash, Archivo
import random
import hashlib
import os


class Command(BaseCommand):
    help = 'Cargar datos de demostración para simular la aplicación de escritorio'

    def handle(self, *args, **options):
        self.stdout.write('Cargando datos de demostración...')
        
        # Crear jerarquías
        jerarquias = self.crear_jerarquias()
        
        # Crear destinos
        destinos = self.crear_destinos()
        
        # Crear oficiales
        oficiales = self.crear_oficiales(jerarquias, destinos)
        
        # Crear tipos de procedimiento
        tipos_procedimiento = self.crear_tipos_procedimiento()
        
        # Crear formularios de ejemplo
        self.crear_formularios_ejemplo(oficiales, tipos_procedimiento)
        
        self.stdout.write(
            self.style.SUCCESS('Datos de demostración cargados exitosamente!')
        )

    def crear_jerarquias(self):
        jerarquias_data = [
            {'nombre': 'Comisario', 'abreviatura': 'COM', 'orden': 1},
            {'nombre': 'Subcomisario', 'abreviatura': 'SUBCOM', 'orden': 2},
            {'nombre': 'Oficial Principal', 'abreviatura': 'OF PRIN', 'orden': 3},
            {'nombre': 'Oficial Inspector', 'abreviatura': 'OF INSP', 'orden': 4},
            {'nombre': 'Suboficial Mayor', 'abreviatura': 'SOF MAY', 'orden': 5},
            {'nombre': 'Suboficial Principal', 'abreviatura': 'SOF PRIN', 'orden': 6},
            {'nombre': 'Sargento Ayudante', 'abreviatura': 'SGTO AY', 'orden': 7},
            {'nombre': 'Sargento Primero', 'abreviatura': 'SGTO 1RO', 'orden': 8},
            {'nombre': 'Sargento', 'abreviatura': 'SGTO', 'orden': 9},
            {'nombre': 'Cabo Primero', 'abreviatura': 'CBO 1RO', 'orden': 10},
            {'nombre': 'Cabo', 'abreviatura': 'CBO', 'orden': 11},
            {'nombre': 'Agente', 'abreviatura': 'AGT', 'orden': 12},
        ]
        
        jerarquias = []
        for data in jerarquias_data:
            jerarquia, created = Jerarquia.objects.get_or_create(
                nombre=data['nombre'],
                defaults=data
            )
            jerarquias.append(jerarquia)
            if created:
                self.stdout.write(f'  - Jerarquía creada: {jerarquia}')
        
        return jerarquias

    def crear_destinos(self):
        destinos_data = [
            'Comisaría 1ra',
            'Comisaría 2da',
            'Comisaría 3ra',
            'Comisaría 4ta',
            'Comisaría 5ta',
            'División Investigaciones',
            'División Operaciones',
            'División Tránsito',
            'División Drogas Peligrosas',
            'División Delitos Económicos',
            'División Delitos contra la Propiedad',
            'División Delitos contra las Personas',
        ]
        
        destinos = []
        for nombre in destinos_data:
            destino, created = Destino.objects.get_or_create(nombre=nombre)
            destinos.append(destino)
            if created:
                self.stdout.write(f'  - Destino creado: {destino}')
        
        return destinos

    def crear_oficiales(self, jerarquias, destinos):
        nombres = [
            'Juan Carlos Pérez',
            'María Elena Rodríguez',
            'Carlos Alberto González',
            'Ana María López',
            'Roberto Daniel Silva',
            'Laura Patricia Martínez',
            'Miguel Ángel Fernández',
            'Silvia Beatriz Torres',
            'Fernando José Ramírez',
            'Carmen Rosa Herrera',
            'Diego Alejandro Morales',
            'Valeria Sofía Castro',
            'Ricardo Nicolás Vargas',
            'Patricia Alejandra Ruiz',
            'Héctor Manuel Jiménez',
        ]
        
        oficiales = []
        for i, nombre in enumerate(nombres):
            legajo = f"LEG{i+1:03d}"
            jerarquia = random.choice(jerarquias)
            destino = random.choice(destinos)
            
            oficial, created = Oficial.objects.get_or_create(
                legajo=legajo,
                defaults={
                    'nombre': nombre,
                    'jerarquia': jerarquia,
                    'destino': destino,
                    'activo': True
                }
            )
            oficiales.append(oficial)
            if created:
                self.stdout.write(f'  - Oficial creado: {oficial}')
        
        return oficiales

    def crear_tipos_procedimiento(self):
        tipos_data = [
            'Allanamiento',
            'Secuestro',
            'Detención',
            'Incautación',
            'Relevamiento',
            'Investigación',
            'Patrullaje',
            'Control de Tránsito',
            'Operativo Especial',
            'Rescate',
        ]
        
        tipos = []
        for nombre in tipos_data:
            tipo, created = TipoProcedimiento.objects.get_or_create(
                nombre=nombre,
                defaults={'descripcion': f'Descripción de {nombre}'}
            )
            tipos.append(tipo)
            if created:
                self.stdout.write(f'  - Tipo de procedimiento creado: {tipo}')
        
        return tipos

    def crear_formularios_ejemplo(self, oficiales, tipos_procedimiento):
        # Crear algunos formularios de ejemplo
        formularios_data = [
            {
                'tipo': 'PROCEDIMIENTO',
                'procedimiento': 'Allanamiento en domicilio - Calle San Martín 123',
                'observaciones': 'Procedimiento realizado en conjunto con la división de investigaciones.'
            },
            {
                'tipo': 'PROCEDIMIENTO',
                'procedimiento': 'Secuestro de vehículo - Ruta Nacional 9',
                'observaciones': 'Vehículo secuestrado por infracción de tránsito.'
            },
            {
                'tipo': 'VUELO',
                'procedimiento': 'Patrullaje aéreo - Zona Norte',
                'observaciones': 'Vuelo de reconocimiento y control de la zona norte.'
            },
        ]
        
        # Nombres de archivos de ejemplo
        archivos_ejemplo = [
            {'nombre': 'foto_evidencia_001.jpg', 'extension': 'jpg', 'peso': 2048576},
            {'nombre': 'video_incidente.mp4', 'extension': 'mp4', 'peso': 15728640},
            {'nombre': 'audio_testimonio.wav', 'extension': 'wav', 'peso': 5242880},
            {'nombre': 'documento_acta.pdf', 'extension': 'pdf', 'peso': 1048576},
            {'nombre': 'planilla_datos.xlsx', 'extension': 'xlsx', 'peso': 512000},
            {'nombre': 'foto_evidencia_002.jpg', 'extension': 'jpg', 'peso': 3072000},
            {'nombre': 'video_seguridad.avi', 'extension': 'avi', 'peso': 20971520},
            {'nombre': 'audio_entrevista.mp3', 'extension': 'mp3', 'peso': 3145728},
            {'nombre': 'informe_final.docx', 'extension': 'docx', 'peso': 786432},
            {'nombre': 'foto_evidencia_003.png', 'extension': 'png', 'peso': 1536000},
        ]
        
        for i, data in enumerate(formularios_data):
            # Crear formulario
            formulario = FormularioHash.objects.create(
                nro_hash=i + 1,
                tipo=data['tipo'],
                procedimiento=data['procedimiento'],
                oficial_entrega=random.choice(oficiales),
                oficial_recibe=random.choice(oficiales),
                observaciones=data['observaciones'],
                estado='FINALIZADO'
            )
            
            # Agregar archivos de ejemplo
            archivos_para_formulario = random.sample(archivos_ejemplo, random.randint(3, 7))
            
            for j, archivo_data in enumerate(archivos_para_formulario):
                # Generar hash simulado
                contenido_simulado = f"{archivo_data['nombre']}{j}{i}".encode()
                hash_sha256 = hashlib.sha256(contenido_simulado).hexdigest().upper()
                
                archivo = Archivo.objects.create(
                    formulario=formulario,
                    nro_orden=j + 1,
                    nombre=archivo_data['nombre'],
                    extension=archivo_data['extension'],
                    peso=archivo_data['peso'],
                    hash_sha256=hash_sha256,
                    tipo_mime='application/octet-stream'
                )
            
            # Recalcular contadores
            formulario.calcular_contadores()
            
            self.stdout.write(f'  - Formulario creado: #{formulario.nro_hash} - {formulario.procedimiento}')
            self.stdout.write(f'    Archivos: {formulario.total_archivos}, Peso: {formulario.peso_total_formateado}') 