#!/usr/bin/env python
import os
import sys
import django

# Configure Django settings
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'hash_project.settings')
django.setup()

from core.models import Jerarquia, Destino, Oficial, TipoProcedimiento

def create_test_data():
    print("Creating test data...")
    
    # Create Jerarquias
    jerarquias_data = [
        ("Coronel", "Crl.", 1),
        ("Teniente Coronel", "Tte. Crl.", 2),
        ("Mayor", "My.", 3),
        ("Capitán", "Cap.", 4),
        ("Teniente", "Tte.", 5),
        ("Subteniente", "Subtte.", 6),
        ("Sargento Primero", "Sgto. 1ro.", 7),
        ("Sargento", "Sgto.", 8),
        ("Cabo", "Cabo", 9),
    ]
    
    for nombre, abreviatura, orden in jerarquias_data:
        jerarquia, created = Jerarquia.objects.get_or_create(
            nombre=nombre,
            defaults={'abreviatura': abreviatura, 'orden': orden}
        )
        if created:
            print(f"Created jerarquia: {jerarquia}")
    
    # Create Destinos
    destinos_data = [
        "Comisaría 1ra",
        "Comisaría 2da",
        "Comisaría 3ra",
        "División Criminalística",
        "División Homicidios",
        "División Robos y Hurtos",
        "Comando Radioeléctrico",
        "Dirección General",
    ]
    
    for nombre in destinos_data:
        destino, created = Destino.objects.get_or_create(
            nombre=nombre,
            defaults={'activo': True}
        )
        if created:
            print(f"Created destino: {destino}")
    
    # Create some Oficiales
    oficiales_data = [
        ("12345", "Juan Carlos Pérez", "Teniente", "Comisaría 1ra"),
        ("67890", "María Elena González", "Capitán", "División Criminalística"),
        ("11111", "Roberto Silva", "Sargento", "Comisaría 2da"),
        ("22222", "Ana López", None, "División Homicidios"),  # Civil
        ("33333", "Carlos Rodríguez", "Mayor", "Dirección General"),
    ]
    
    for legajo, nombre, jerarquia_nombre, destino_nombre in oficiales_data:
        destino = Destino.objects.get(nombre=destino_nombre)
        jerarquia = None
        if jerarquia_nombre:
            jerarquia = Jerarquia.objects.get(nombre=jerarquia_nombre)
        
        oficial, created = Oficial.objects.get_or_create(
            legajo=legajo,
            defaults={
                'nombre': nombre,
                'jerarquia': jerarquia,
                'destino': destino,
                'activo': True
            }
        )
        if created:
            print(f"Created oficial: {oficial}")
    
    # Create TipoProcedimiento
    tipos_data = [
        ("Robo", "Procedimientos relacionados con robos"),
        ("Homicidio", "Procedimientos relacionados con homicidios"),
        ("Narcotráfico", "Procedimientos relacionados con drogas"),
        ("Estafa", "Procedimientos relacionados con estafas"),
        ("Violencia Doméstica", "Procedimientos de violencia familiar"),
    ]
    
    for nombre, descripcion in tipos_data:
        tipo, created = TipoProcedimiento.objects.get_or_create(
            nombre=nombre,
            defaults={'descripcion': descripcion, 'activo': True}
        )
        if created:
            print(f"Created tipo procedimiento: {tipo}")
    
    print("Test data creation completed!")
    print(f"Jerarquias: {Jerarquia.objects.count()}")
    print(f"Destinos: {Destino.objects.count()}")
    print(f"Oficiales: {Oficial.objects.count()}")
    print(f"Tipos Procedimiento: {TipoProcedimiento.objects.count()}")

if __name__ == '__main__':
    create_test_data()