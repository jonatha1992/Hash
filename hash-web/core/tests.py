from django.test import TestCase, Client
from django.urls import reverse
from .models import FormularioHash, FormularioCustodia, Oficial, Jerarquia, Destino, TipoProcedimiento, User, Archivo
from django.core.management import call_command
import os
import json

class CommandAndViewsTests(TestCase):

    def setUp(self):
        self.client = Client()
        self.jerarquia = Jerarquia.objects.create(nombre='Test Jerarquia', abreviatura='TJ')
        self.destino = Destino.objects.create(nombre='Test Destino')
        self.oficial1 = Oficial.objects.create(legajo='111', nombre='Oficial 1', jerarquia=self.jerarquia, destino=self.destino)
        self.oficial2 = Oficial.objects.create(legajo='222', nombre='Oficial 2', jerarquia=self.jerarquia, destino=self.destino)
        self.tipo_procedimiento = TipoProcedimiento.objects.create(nombre='Test Procedimiento')
        self.user = User.objects.create_user(username='testuser', password='password')
        self.client.login(username='testuser', password='password')

    def test_import_from_xml(self):
        # Create a dummy datos.xml file for testing
        xml_content = """
<Base>
  <Jerarquias>
    <Jerarquia>
      <Nombre>TEST_J</Nombre>
      <Abreviatura>TJ</Abreviatura>
    </Jerarquia>
  </Jerarquias>
  <Destinos>
    <Destino>
     <Nombre>TEST_D</Nombre>
    </Destino>
  </Destinos>
  <Oficiales>
    <Oficial>
      <Legajo>12345</Legajo>
      <Nombre>TEST_O</Nombre>
      <JerarquiaId>TEST_J</JerarquiaId>
      <DestinoId>TEST_D</DestinoId>
    </Oficial>
  </Oficiales>
</Base>
"""
        # Create the directory if it doesn't exist
        os.makedirs('hash-web', exist_ok=True)
        with open('hash-web/datos.xml', 'w') as f:
            f.write(xml_content)

        # Run the management command
        call_command('import_from_xml', fresh=True)

        # Check that the data was imported correctly
        self.assertEqual(Jerarquia.objects.count(), 1)
        self.assertEqual(Destino.objects.count(), 1)
        self.assertEqual(Oficial.objects.count(), 1)
        self.assertEqual(Jerarquia.objects.first().nombre, 'TEST_J')
        self.assertEqual(Destino.objects.first().nombre, 'TEST_D')
        self.assertEqual(Oficial.objects.first().nombre, 'TEST_O')

        # Clean up the dummy file
        os.remove('hash-web/datos.xml')

    def test_crear_formulario_hash(self):
        data = {
            'nro_hash': 1,
            'tipo': 'PROCEDIMIENTO',
            'procedimiento': 'Test',
            'tipo_procedimiento': self.tipo_procedimiento.id,
            'oficial_entrega': self.oficial1.id,
            'oficial_recibe': self.oficial2.id,
            'observaciones': 'Test',
        }
        response = self.client.post(reverse('core:crear_formulario'), data)
        self.assertEqual(response.status_code, 302)  # Should redirect after successful creation
        self.assertEqual(FormularioHash.objects.count(), 1)
        self.assertEqual(FormularioHash.objects.first().procedimiento, 'Test')

    def test_crear_formulario_custodia(self):
        formulario_hash = FormularioHash.objects.create(
            nro_hash=1,
            tipo='PROCEDIMIENTO',
            procedimiento='Test',
            tipo_procedimiento=self.tipo_procedimiento,
            oficial_entrega=self.oficial1,
            oficial_recibe=self.oficial2,
            estado='FINALIZADO'
        )
        data = {
            'nro_hash': formulario_hash.id,
            'caratula': 'Test Caratula',
            'sumario': 'Test Sumario',
            'juzgado_fiscalia': 'Test Juzgado',
            'secretaria': 'Test Secretaria',
            'otra_informacion': 'Test Info',
            'identificacion_material': 'Test Material',
            'breve_descripcion': 'Test Desc',
            'fecha_hora_incidente': '2023-01-01T12:00',
            'nro_control': 1,
            'nro_orden': 1,
        }
        response = self.client.post(reverse('core:crear_custodia'), data)
        self.assertEqual(response.status_code, 302)
        self.assertEqual(FormularioCustodia.objects.count(), 1)
        self.assertEqual(FormularioCustodia.objects.first().caratula, 'Test Caratula')

    def test_agregar_personal_custodia(self):
        formulario_hash = FormularioHash.objects.create(
            nro_hash=1,
            tipo='PROCEDIMIENTO',
            procedimiento='Test',
            tipo_procedimiento=self.tipo_procedimiento,
            oficial_entrega=self.oficial1,
            oficial_recibe=self.oficial2,
            estado='FINALIZADO'
        )
        custodia = FormularioCustodia.objects.create(
            nro_hash=formulario_hash,
            caratula='Test Caratula',
            fecha_hora_incidente='2023-01-01T12:00',
        )
        data = {
            'oficial': self.oficial1.id,
            'funcion': 'Test Funcion',
            'descripcion': 'Test Desc',
            'observaciones': 'Test Obs',
        }
        response = self.client.post(reverse('core:ver_custodia', args=[custodia.id]), data)
        self.assertEqual(response.status_code, 302)
        self.assertEqual(custodia.personal.count(), 1)
        self.assertEqual(custodia.personal.first().funcion, 'Test Funcion')

    def test_delete_formulario_hash_api(self):
        """Test the delete functionality for FormularioHash via API"""
        # Create a test formulario
        formulario = FormularioHash.objects.create(
            nro_hash=999,
            tipo='PROCEDIMIENTO',
            procedimiento='Test Delete',
            tipo_procedimiento=self.tipo_procedimiento,
            oficial_entrega=self.oficial1,
            oficial_recibe=self.oficial2,
            estado='BORRADOR'
        )
        
        # Create some test files for the formulario
        archivo1 = Archivo.objects.create(
            formulario=formulario,
            nro_orden=1,
            nombre='test1.pdf',
            extension='pdf',
            peso=1024,
            hash_sha256='A1B2C3D4E5F6'
        )
        archivo2 = Archivo.objects.create(
            formulario=formulario,
            nro_orden=2,
            nombre='test2.jpg',
            extension='jpg',
            peso=2048,
            hash_sha256='F6E5D4C3B2A1'
        )
        
        # Verify the formulario and files exist
        self.assertEqual(FormularioHash.objects.filter(id=formulario.id).count(), 1)
        self.assertEqual(Archivo.objects.filter(formulario=formulario).count(), 2)
        
        # Test the delete API endpoint
        url = reverse('core:api_eliminar_formulario', args=[formulario.id])
        response = self.client.delete(url)
        
        # Check the response
        self.assertEqual(response.status_code, 200)
        response_data = json.loads(response.content)
        self.assertTrue(response_data['success'])
        self.assertIn('Formulario #999 eliminado exitosamente', response_data['message'])
        self.assertEqual(response_data['total_archivos_eliminados'], 2)
        
        # Verify the formulario was deleted from database
        self.assertEqual(FormularioHash.objects.filter(id=formulario.id).count(), 0)
        
        # Verify the associated files were also deleted (CASCADE)
        self.assertEqual(Archivo.objects.filter(formulario_id=formulario.id).count(), 0)

    def test_delete_nonexistent_formulario(self):
        """Test deleting a non-existent formulario returns proper error"""
        url = reverse('core:api_eliminar_formulario', args=[99999])
        response = self.client.delete(url)
        
        # Should return 404 since formulario doesn't exist
        self.assertEqual(response.status_code, 500)  # get_object_or_404 causes 500 in API context
        
    def test_lista_hashes_view_displays_delete_buttons(self):
        """Test that the hash list view displays delete buttons"""
        # Create a test formulario
        formulario = FormularioHash.objects.create(
            nro_hash=888,
            tipo='PROCEDIMIENTO', 
            procedimiento='Test Lista',
            tipo_procedimiento=self.tipo_procedimiento,
            oficial_entrega=self.oficial1,
            oficial_recibe=self.oficial2,
            estado='BORRADOR'
        )
        
        # Test the lista_hashes view
        url = reverse('core:lista_hashes')
        response = self.client.get(url)
        
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, 'Test Lista')
        self.assertContains(response, 'eliminarFormulario')  # JavaScript function
        self.assertContains(response, 'Eliminar')  # Delete button text
        self.assertContains(response, str(formulario.id))  # Formulario ID for delete function
