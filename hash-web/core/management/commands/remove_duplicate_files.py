from django.core.management.base import BaseCommand
from django.db import transaction
from core.models import FormularioHash, Archivo


class Command(BaseCommand):
    help = 'Remove duplicate files from formularios (same hash in same formulario)'

    def add_arguments(self, parser):
        parser.add_argument(
            '--dry-run',
            action='store_true',
            help='Show what would be deleted without actually deleting',
        )

    def handle(self, *args, **options):
        dry_run = options['dry_run']
        total_removed = 0
        
        formularios = FormularioHash.objects.all()
        
        self.stdout.write(f"Analyzing {formularios.count()} formularios...")
        
        for formulario in formularios:
            archivos = formulario.archivos.order_by('id')  # Older files first
            seen_hashes = set()
            duplicates_to_remove = []
            
            for archivo in archivos:
                if archivo.hash_sha256 in seen_hashes:
                    duplicates_to_remove.append(archivo)
                else:
                    seen_hashes.add(archivo.hash_sha256)
            
            if duplicates_to_remove:
                self.stdout.write(
                    f"Formulario #{formulario.nro_hash}: Found {len(duplicates_to_remove)} duplicate files"
                )
                
                for duplicate in duplicates_to_remove:
                    self.stdout.write(
                        f"  - Removing: Order {duplicate.nro_orden}, {duplicate.nombre} "
                        f"(Hash: {duplicate.hash_sha256[:12]}...)"
                    )
                    
                    if not dry_run:
                        duplicate.delete()
                    
                    total_removed += 1
                
                # Renumber remaining files after deletion
                if not dry_run:
                    remaining_files = formulario.archivos.order_by('nro_orden')
                    for i, archivo in enumerate(remaining_files, 1):
                        if archivo.nro_orden != i:
                            archivo.nro_orden = i
                            archivo.save()
                    
                    # Recalculate counters
                    formulario.calcular_contadores()
        
        if dry_run:
            self.stdout.write(
                self.style.WARNING(f"DRY RUN: Would remove {total_removed} duplicate files")
            )
        else:
            self.stdout.write(
                self.style.SUCCESS(f"Successfully removed {total_removed} duplicate files")
            )