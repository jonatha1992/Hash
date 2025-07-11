import xml.etree.ElementTree as ET
from django.core.management.base import BaseCommand
from django.contrib.auth.models import User
from core.models import Jerarquia, Destino, Oficial
import os


class Command(BaseCommand):
    help = 'Importa datos desde el archivo XML'

    def add_arguments(self, parser):
        parser.add_argument(
            '--archivo',
            type=str,
            default='datos.xml',
            help='Ruta al archivo XML (por defecto: datos.xml)'
        )

    def handle(self, *args, **options):
        archivo_xml = options['archivo']
        
        # Buscar el archivo en la raíz del proyecto
        if not os.path.isabs(archivo_xml):
            # Si no es ruta absoluta, buscar desde la raíz del proyecto
            base_dir = os.path.dirname(os.path.dirname(os.path.dirname(os.path.dirname(__file__))))
            archivo_xml = os.path.join(base_dir, archivo_xml)
        
        if not os.path.exists(archivo_xml):
            self.stdout.write(
                self.style.ERROR(f'No se encontró el archivo: {archivo_xml}')
            )
            return

        try:
            # Parsear el XML
            tree = ET.parse(archivo_xml)
            root = tree.getroot()
            
            # Importar Jerarquías
            self.importar_jerarquias(root)
            
            # Importar Destinos
            self.importar_destinos(root)
            
            # Importar Oficiales
            self.importar_oficiales(root)
            
            self.stdout.write(
                self.style.SUCCESS('Datos importados exitosamente!')
            )
            
        except ET.ParseError as e:
            self.stdout.write(
                self.style.ERROR(f'Error al parsear XML: {e}')
            )
        except Exception as e:
            self.stdout.write(
                self.style.ERROR(f'Error inesperado: {e}')
            )

    def importar_jerarquias(self, root):
        """Importa las jerarquías desde el XML"""
        jerarquias_xml = root.find('Jerarquias')
        if jerarquias_xml is None:
            self.stdout.write(self.style.WARNING('No se encontraron jerarquías en el XML'))
            return
            
        contador = 0
        for jerarquia_xml in jerarquias_xml.findall('Jerarquia'):
            nombre = jerarquia_xml.find('Nombre').text
            abreviatura = jerarquia_xml.find('Abreviatura').text
            
            jerarquia, created = Jerarquia.objects.get_or_create(
                nombre=nombre,
                defaults={'abreviatura': abreviatura}
            )
            
            if created:
                contador += 1
                self.stdout.write(f'  ✓ Jerarquía creada: {nombre}')
            else:
                # Actualizar abreviatura si es diferente
                if jerarquia.abreviatura != abreviatura:
                    jerarquia.abreviatura = abreviatura
                    jerarquia.save()
                    self.stdout.write(f'  ↻ Jerarquía actualizada: {nombre}')
                else:
                    self.stdout.write(f'  - Jerarquía ya existe: {nombre}')
        
        self.stdout.write(self.style.SUCCESS(f'Jerarquías procesadas: {contador} nuevas'))

    def importar_destinos(self, root):
        """Importa los destinos desde el XML"""
        destinos_xml = root.find('Destinos')
        if destinos_xml is None:
            self.stdout.write(self.style.WARNING('No se encontraron destinos en el XML'))
            return
            
        contador = 0
        for destino_xml in destinos_xml.findall('Destino'):
            nombre = destino_xml.find('Nombre').text
            
            destino, created = Destino.objects.get_or_create(
                nombre=nombre
            )
            
            if created:
                contador += 1
                self.stdout.write(f'  ✓ Destino creado: {nombre}')
            else:
                self.stdout.write(f'  - Destino ya existe: {nombre}')
        
        self.stdout.write(self.style.SUCCESS(f'Destinos procesados: {contador} nuevos'))

    def importar_oficiales(self, root):
        """Importa los oficiales desde el XML"""
        oficiales_xml = root.find('Oficiales')
        if oficiales_xml is None:
            self.stdout.write(self.style.WARNING('No se encontraron oficiales en el XML'))
            return
            
        contador = 0
        for oficial_xml in oficiales_xml.findall('Oficial'):
            legajo = oficial_xml.find('Legajo').text
            nombre_completo = oficial_xml.find('Nombre').text
            jerarquia_nombre = oficial_xml.find('JerarquiaId').text
            destino_nombre = oficial_xml.find('DestinoId').text
            
            # Buscar jerarquía
            try:
                jerarquia = Jerarquia.objects.get(nombre=jerarquia_nombre)
            except Jerarquia.DoesNotExist:
                self.stdout.write(
                    self.style.WARNING(f'Jerarquía no encontrada: {jerarquia_nombre}')
                )
                continue
            
            # Buscar destino
            try:
                destino = Destino.objects.get(nombre=destino_nombre)
            except Destino.DoesNotExist:
                self.stdout.write(
                    self.style.WARNING(f'Destino no encontrado: {destino_nombre}')
                )
                continue
            
            # Separar nombre y apellido (asumir que el último es apellido)
            partes_nombre = nombre_completo.split()
            if len(partes_nombre) >= 2:
                apellido = partes_nombre[-1]
                nombre = ' '.join(partes_nombre[:-1])
            else:
                nombre = nombre_completo
                apellido = ''
            
            # Crear o buscar usuario
            username = f'oficial_{legajo}'
            user, user_created = User.objects.get_or_create(
                username=username,
                defaults={
                    'first_name': nombre,
                    'last_name': apellido,
                    'email': f'{username}@policia.gov.ar'
                }
            )
            
            # Crear o actualizar oficial
            oficial, created = Oficial.objects.get_or_create(
                legajo=legajo,
                defaults={
                    'user': user,
                    'jerarquia': jerarquia,
                    'destino': destino
                }
            )
            
            if created:
                contador += 1
                self.stdout.write(f'  ✓ Oficial creado: {nombre_completo} (Legajo: {legajo})')
            else:
                # Actualizar datos si es necesario
                updated = False
                if oficial.user != user:
                    oficial.user = user
                    updated = True
                if oficial.jerarquia != jerarquia:
                    oficial.jerarquia = jerarquia
                    updated = True
                if oficial.destino != destino:
                    oficial.destino = destino
                    updated = True
                    
                if updated:
                    oficial.save()
                    self.stdout.write(f'  ↻ Oficial actualizado: {nombre_completo}')
                else:
                    self.stdout.write(f'  - Oficial ya existe: {nombre_completo}')
        
        self.stdout.write(self.style.SUCCESS(f'Oficiales procesados: {contador} nuevos'))
