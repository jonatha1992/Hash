from django.test import TestCase
from django.core.files.uploadedfile import SimpleUploadedFile
from core.models import FormularioHash, Archivo, Oficial, Jerarquia, Destino
import hashlib


class FileDuplicationTest(TestCase):
    def setUp(self):
        """Set up test data"""
        # Create required related objects
        self.jerarquia = Jerarquia.objects.create(
            nombre="Sargento", abreviatura="SGTO", orden=1
        )
        self.destino = Destino.objects.create(nombre="Destino Test")
        self.oficial1 = Oficial.objects.create(
            legajo="001", nombre="Oficial Test 1", 
            jerarquia=self.jerarquia, destino=self.destino
        )
        self.oficial2 = Oficial.objects.create(
            legajo="002", nombre="Oficial Test 2", 
            jerarquia=self.jerarquia, destino=self.destino
        )
        
        # Create a test formulario
        self.formulario = FormularioHash.objects.create(
            nro_hash=999,
            procedimiento="Test procedure",
            oficial_entrega=self.oficial1,
            oficial_recibe=self.oficial2
        )

    def test_no_duplicate_files_same_hash(self):
        """Test that files with the same hash are not duplicated"""
        from django.db import IntegrityError, transaction
        
        # Create test file content
        file_content = b"Test file content for deduplication test"
        file_hash = hashlib.sha256(file_content).hexdigest().upper()
        
        # Create first file
        archivo1 = Archivo.objects.create(
            formulario=self.formulario,
            nro_orden=1,
            nombre="test_file.txt",
            extension="txt",
            peso=len(file_content),
            hash_sha256=file_hash
        )
        
        # Verify first file is created
        self.assertEqual(self.formulario.archivos.count(), 1)
        
        # Try to create second file with same hash - should fail due to unique constraint
        with self.assertRaises(IntegrityError):
            with transaction.atomic():
                Archivo.objects.create(
                    formulario=self.formulario,
                    nro_orden=2,
                    nombre="duplicate_file.txt",
                    extension="txt",
                    peso=len(file_content),
                    hash_sha256=file_hash  # Same hash
                )
        
        # Should still have only one file
        self.assertEqual(self.formulario.archivos.count(), 1)

    def test_different_hash_files_allowed(self):
        """Test that files with different hashes are allowed"""
        # Create two different file contents
        file_content1 = b"First file content"
        file_content2 = b"Second file content"
        file_hash1 = hashlib.sha256(file_content1).hexdigest().upper()
        file_hash2 = hashlib.sha256(file_content2).hexdigest().upper()
        
        # Create first file
        archivo1 = Archivo.objects.create(
            formulario=self.formulario,
            nro_orden=1,
            nombre="file1.txt",
            extension="txt",
            peso=len(file_content1),
            hash_sha256=file_hash1
        )
        
        # Create second file with different hash
        archivo2 = Archivo.objects.create(
            formulario=self.formulario,
            nro_orden=2,
            nombre="file2.txt",
            extension="txt",
            peso=len(file_content2),
            hash_sha256=file_hash2
        )
        
        # Should have two files
        self.assertEqual(self.formulario.archivos.count(), 2)

    def test_same_hash_different_formularios_allowed(self):
        """Test that same hash in different formularios is allowed"""
        # Create another formulario
        formulario2 = FormularioHash.objects.create(
            nro_hash=998,
            procedimiento="Another test procedure",
            oficial_entrega=self.oficial1,
            oficial_recibe=self.oficial2
        )
        
        file_content = b"Shared file content"
        file_hash = hashlib.sha256(file_content).hexdigest().upper()
        
        # Create file in first formulario
        archivo1 = Archivo.objects.create(
            formulario=self.formulario,
            nro_orden=1,
            nombre="shared_file.txt",
            extension="txt",
            peso=len(file_content),
            hash_sha256=file_hash
        )
        
        # Create file with same hash in second formulario - should be allowed
        archivo2 = Archivo.objects.create(
            formulario=formulario2,
            nro_orden=1,
            nombre="shared_file.txt",
            extension="txt",
            peso=len(file_content),
            hash_sha256=file_hash
        )
        
        # Each formulario should have one file
        self.assertEqual(self.formulario.archivos.count(), 1)
        self.assertEqual(formulario2.archivos.count(), 1)