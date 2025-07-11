from django.core.management.base import BaseCommand
from core.models import Jerarquia, Oficial, TipoProcedimiento


class Command(BaseCommand):
    help = 'Carga datos iniciales para la aplicación'

    def handle(self, *args, **options):
        self.stdout.write('Creando datos iniciales...')
        
        # Crear jerarquías
        jerarquias_data = [
            {"jerarquia": "Comisario General", "abreviatura": "CG", "orden": 1},
            {"jerarquia": "Comisario Mayor", "abreviatura": "CM", "orden": 2},
            {"jerarquia": "Comisario", "abreviatura": "CO", "orden": 3},
            {"jerarquia": "Subcomisario", "abreviatura": "SC", "orden": 4},
            {"jerarquia": "Principal", "abreviatura": "PR", "orden": 5},
            {"jerarquia": "Sargento", "abreviatura": "SA", "orden": 6},
            {"jerarquia": "Cabo", "abreviatura": "CB", "orden": 7},
            {"jerarquia": "Agente", "abreviatura": "AG", "orden": 8},
        ]
        
        for j_data in jerarquias_data:
            jerarquia, created = Jerarquia.objects.get_or_create(
                jerarquia=j_data["jerarquia"],
                defaults=j_data
            )
            if created:
                self.stdout.write(f'  ✓ Jerarquía creada: {jerarquia}')
        
        # Crear tipos de procedimiento
        tipos_procedimiento = [
            "Allanamiento",
            "Procedimiento de rutina",
            "Operativo especial",
            "Investigación",
            "Custodia preventiva",
            "Análisis forense",
            "Peritaje técnico",
        ]
        
        for tipo in tipos_procedimiento:
            obj, created = TipoProcedimiento.objects.get_or_create(
                nombre=tipo,
                defaults={"descripcion": f"Procedimiento de tipo {tipo}"}
            )
            if created:
                self.stdout.write(f'  ✓ Tipo de procedimiento creado: {obj}')
        
        # Crear oficiales de ejemplo
        oficiales_data = [
            {"legajo": 12345, "nombre_completo": "Juan Carlos Pérez", "jerarquia": "Comisario"},
            {"legajo": 23456, "nombre_completo": "María Elena González", "jerarquia": "Subcomisario"},
            {"legajo": 34567, "nombre_completo": "Carlos Alberto Rodríguez", "jerarquia": "Principal"},
            {"legajo": 45678, "nombre_completo": "Ana Beatriz Martínez", "jerarquia": "Sargento"},
            {"legajo": 56789, "nombre_completo": "Roberto Luis Silva", "jerarquia": "Cabo"},
            {"legajo": 67890, "nombre_completo": "Lucía Fernanda Torres", "jerarquia": "Agente"},
        ]
        
        for o_data in oficiales_data:
            jerarquia = Jerarquia.objects.get(jerarquia=o_data["jerarquia"])
            oficial, created = Oficial.objects.get_or_create(
                legajo=o_data["legajo"],
                defaults={
                    "nombre_completo": o_data["nombre_completo"],
                    "jerarquia": jerarquia,
                    "unidad": "División Investigaciones",
                    "activo": True
                }
            )
            if created:
                self.stdout.write(f'  ✓ Oficial creado: {oficial}')
        
        self.stdout.write(
            self.style.SUCCESS('✅ Datos iniciales cargados exitosamente!')
        )
