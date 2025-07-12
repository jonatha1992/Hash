import xml.etree.ElementTree as ET
from django.core.management.base import BaseCommand
from core.models import Jerarquia, Destino, Oficial, TipoProcedimiento

class Command(BaseCommand):
    help = 'Importa datos desde el archivo datos.xml'

    def add_arguments(self, parser):
        parser.add_argument('--fresh', action='store_true', help='Borra los datos existentes antes de importar.')

    def handle(self, *args, **options):
        if options['fresh']:
            self.stdout.write(self.style.WARNING('Borrando datos existentes...'))
            Jerarquia.objects.all().delete()
            Destino.objects.all().delete()
            Oficial.objects.all().delete()
            TipoProcedimiento.objects.all().delete()

        self.stdout.write(self.style.SUCCESS('Iniciando importación de datos desde datos.xml...'))

        try:
            tree = ET.parse('hash-web/datos.xml')
            root = tree.getroot()
        except FileNotFoundError:
            self.stdout.write(self.style.ERROR('No se encontró el archivo datos.xml. Asegúrate de que esté en la raíz del proyecto.'))
            return
        except ET.ParseError:
            self.stdout.write(self.style.ERROR('Error al parsear el archivo datos.xml. Verifica que el formato XML sea correcto.'))
            return

        # Importar Jerarquías
        self.stdout.write('Importando Jerarquías...')
        jerarquias_creadas = 0
        for jerarquia_node in root.findall('.//Jerarquia'):
            nombre = jerarquia_node.find('Nombre').text
            abreviatura = jerarquia_node.find('Abreviatura').text
            self.stdout.write(f'  Leyendo Jerarquía: {nombre}')
            if nombre and abreviatura:
                jerarquia, created = Jerarquia.objects.get_or_create(
                    nombre=nombre,
                    defaults={'abreviatura': abreviatura}
                )
                if created:
                    jerarquias_creadas += 1
                    self.stdout.write(self.style.SUCCESS(f'    -> Jerarquía creada: {nombre}'))
        self.stdout.write(self.style.SUCCESS(f'Se importaron {jerarquias_creadas} nuevas jerarquías.'))

        # Importar Destinos
        self.stdout.write('Importando Destinos...')
        destinos_creados = 0
        for destino_node in root.findall('.//Destino'):
            nombre = destino_node.find('Nombre').text
            self.stdout.write(f'  Leyendo Destino: {nombre}')
            if nombre:
                destino, created = Destino.objects.get_or_create(nombre=nombre)
                if created:
                    destinos_creados += 1
                    self.stdout.write(self.style.SUCCESS(f'    -> Destino creado: {nombre}'))
        self.stdout.write(self.style.SUCCESS(f'Se importaron {destinos_creados} nuevos destinos.'))

        # Importar Oficiales
        self.stdout.write('Importando Oficiales...')
        oficiales_creados = 0
        for oficial_node in root.findall('.//Oficial'):
            legajo = oficial_node.find('Legajo').text
            nombre_completo = oficial_node.find('Nombre').text
            jerarquia_nombre = oficial_node.find('JerarquiaId').text
            destino_nombre = oficial_node.find('DestinoId').text
            self.stdout.write(f'  Leyendo Oficial: {nombre_completo}')

            if legajo and nombre_completo and jerarquia_nombre and destino_nombre:
                try:
                    jerarquia = Jerarquia.objects.get(nombre=jerarquia_nombre)
                    destino = Destino.objects.get(nombre=destino_nombre)

                    oficial, created = Oficial.objects.get_or_create(
                        legajo=legajo,
                        defaults={
                            'nombre': nombre_completo,
                            'jerarquia': jerarquia,
                            'destino': destino,
                        }
                    )
                    if created:
                        oficiales_creados += 1
                        self.stdout.write(self.style.SUCCESS(f'    -> Oficial creado: {nombre_completo}'))

                except Jerarquia.DoesNotExist:
                    self.stdout.write(self.style.WARNING(f'    -> No se encontró la jerarquía "{jerarquia_nombre}" para el oficial {nombre_completo}.'))
                except Destino.DoesNotExist:
                    self.stdout.write(self.style.WARNING(f'    -> No se encontró el destino "{destino_nombre}" para el oficial {nombre_completo}.'))

        self.stdout.write(self.style.SUCCESS(f'Se importaron {oficiales_creados} nuevos oficiales.'))

        # Opcional: Importar Procedimientos si es necesario
        self.stdout.write('Importando Tipos de Procedimiento...')
        procedimientos_creados = 0
        for procedimiento_node in root.findall('.//Procedimientos/Procedimiento'):
            nombre = procedimiento_node.find('Nombre').text
            self.stdout.write(f'  Leyendo Procedimiento: {nombre}')
            if nombre:
                procedimiento, created = TipoProcedimiento.objects.get_or_create(nombre=nombre)
                if created:
                    procedimientos_creados += 1
                    self.stdout.write(self.style.SUCCESS(f'    -> Tipo de Procedimiento creado: {nombre}'))
        self.stdout.write(self.style.SUCCESS(f'Se importaron {procedimientos_creados} nuevos tipos de procedimiento.'))

        self.stdout.write(self.style.SUCCESS('¡Importación de datos completada!'))
