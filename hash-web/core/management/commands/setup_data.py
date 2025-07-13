from django.core.management.base import BaseCommand
from core.models import Jerarquia, Destino, Oficial, TipoProcedimiento, FormularioHash, Archivo
from django.contrib.auth.models import User

class Command(BaseCommand):
    help = 'Crea datos de prueba para la aplicación'

    def handle(self, *args, **options):
        self.stdout.write('Creando datos de prueba...')
        
        # Crear jerarquías si no existen
        jerarquias_data = [
            {'nombre': 'Comisario General', 'abreviatura': 'CRIO. GRAL.', 'orden': 1},
            {'nombre': 'Comisario', 'abreviatura': 'CRIO.', 'orden': 2},
            {'nombre': 'Subcomisario', 'abreviatura': 'SUBCRIO.', 'orden': 3},
            {'nombre': 'Principal', 'abreviatura': 'PPAL.', 'orden': 4},
            {'nombre': 'Sargento', 'abreviatura': 'SGTO.', 'orden': 5},
            {'nombre': 'Cabo', 'abreviatura': 'CBO.', 'orden': 6},
            {'nombre': 'Agente', 'abreviatura': 'AGT.', 'orden': 7},
        ]
        
        for data in jerarquias_data:
            jerarquia, created = Jerarquia.objects.get_or_create(
                nombre=data['nombre'],
                defaults=data
            )
            if created:
                self.stdout.write(f"Jerarquía creada: {jerarquia}")
        
        # Crear destinos si no existen
        destinos_data = [
            'División Criminalística',
            'Comisaría Primera',
            'Unidad Regional I',
            'División Investigaciones',
            'Departamento Informática',
        ]
        
        for nombre in destinos_data:
            destino, created = Destino.objects.get_or_create(nombre=nombre)
            if created:
                self.stdout.write(f"Destino creado: {destino}")
        
        # Crear oficiales de prueba si no existen suficientes
        if Oficial.objects.count() < 2:
            jerarquia_agente = Jerarquia.objects.get(nombre='Agente')
            jerarquia_sargento = Jerarquia.objects.get(nombre='Sargento')
            destino_criminalistica = Destino.objects.get(nombre='División Criminalística')
            
            oficiales_data = [
                {
                    'legajo': '12345',
                    'nombre': 'Juan Carlos Pérez',
                    'jerarquia': jerarquia_sargento,
                    'destino': destino_criminalistica,
                },
                {
                    'legajo': '12346',
                    'nombre': 'María Elena González',
                    'jerarquia': jerarquia_agente,
                    'destino': destino_criminalistica,
                },
                {
                    'legajo': '12347',
                    'nombre': 'Carlos Alberto Rodríguez',
                    'jerarquia': jerarquia_sargento,
                    'destino': destino_criminalistica,
                },
            ]
            
            for data in oficiales_data:
                # Usamos update_or_create para evitar errores por duplicados
                oficial, created = Oficial.objects.update_or_create(
                    legajo=data['legajo'],
                    defaults={
                        'nombre': data['nombre'],
                        'jerarquia': data['jerarquia'],
                        'destino': data['destino']
                    }
                )
                if created:
                    self.stdout.write(f"Oficial creado: {oficial}")
                else:
                    self.stdout.write(f"Oficial actualizado: {oficial}")
        
        # Crear tipos de procedimiento si no existen
        tipos_data = [
            {'nombre': 'Allanamiento', 'descripcion': 'Procedimiento de allanamiento'},
            {'nombre': 'Secuestro de elementos', 'descripcion': 'Secuestro de elementos de prueba'},
            {'nombre': 'Inspección técnica', 'descripcion': 'Inspección técnica del lugar del hecho'},
            {'nombre': 'Análisis de dispositivos', 'descripcion': 'Análisis de dispositivos digitales'},
        ]
        
        for data in tipos_data:
            tipo, created = TipoProcedimiento.objects.get_or_create(
                nombre=data['nombre'],
                defaults=data
            )
            if created:
                self.stdout.write(f"Tipo de procedimiento creado: {tipo}")
        
        # Crear un formulario de ejemplo si no existe ninguno
        if FormularioHash.objects.count() == 0:
            oficiales = list(Oficial.objects.all())
            if len(oficiales) >= 2:
                # Crear formulario
                formulario = FormularioHash.objects.create(
                    procedimiento='Formulario de prueba',
                    tipo_procedimiento=TipoProcedimiento.objects.first(),
                    oficial_entrega=oficiales[0],
                    oficial_recibe=oficiales[1],
                    observaciones='Este es un formulario creado para pruebas'
                )
                
                # Crear archivos de ejemplo
                archivos = [
                    {
                        'nro_orden': 1,
                        'nombre': 'imagen_ejemplo.jpg',
                        'extension': '.jpg',
                        'peso': 1024 * 1024 * 2,  # 2MB
                        'hash_sha256': 'A' * 64
                    },
                    {
                        'nro_orden': 2,
                        'nombre': 'documento_ejemplo.pdf',
                        'extension': '.pdf',
                        'peso': 1024 * 512,  # 0.5MB
                        'hash_sha256': 'B' * 64
                    },
                    {
                        'nro_orden': 3,
                        'nombre': 'video_ejemplo.mp4',
                        'extension': '.mp4',
                        'peso': 1024 * 1024 * 15,  # 15MB
                        'hash_sha256': 'C' * 64
                    }
                ]
                
                for archivo_data in archivos:
                    Archivo.objects.create(
                        formulario=formulario,
                        **archivo_data
                    )
                
                # Actualizar contadores
                formulario.calcular_contadores()
                self.stdout.write(f"Formulario creado con ID {formulario.id} y {len(archivos)} archivos")
            else:
                self.stdout.write(self.style.WARNING("No hay suficientes oficiales para crear un formulario"))
        
        # Mostrar resumen de datos creados
        self.stdout.write(self.style.SUCCESS(f"\n=== RESUMEN DE DATOS ==="))
        self.stdout.write(f"Jerarquías: {Jerarquia.objects.count()}")
        self.stdout.write(f"Destinos: {Destino.objects.count()}")
        self.stdout.write(f"Oficiales: {Oficial.objects.count()}")
        self.stdout.write(f"Tipos de procedimiento: {TipoProcedimiento.objects.count()}")
        self.stdout.write(f"Formularios hash: {FormularioHash.objects.count()}")
        self.stdout.write(f"Archivos: {Archivo.objects.count()}")
