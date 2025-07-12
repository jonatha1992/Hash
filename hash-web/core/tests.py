from django.test import TestCase, Client
from django.urls import reverse
from .models import FormularioHash, FormularioCustodia, Oficial, Jerarquia, Destino, TipoProcedimiento, User
from django.core.management import call_command
import os

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
