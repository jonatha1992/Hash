using Hash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Transcripcion
{
    public partial class Hash : Form
    {
        List<string> NombreArchivos = new List<string>();
        List<string> RutaArchivos = new List<string>();
        Formulario_Hash formulario = new Formulario_Hash();
        string CarpetaDestino;
        string carpetaSeleccionada;

        List<Jerarquia> jerarquias = new List<Jerarquia>() { new Jerarquia("AYUDANTE","OF. AYTE."),
                                                             new Jerarquia("PRINCIPAL","OF. PPAL."),
                                                             new Jerarquia("MAYOR","OF. MY."),
                                                             new Jerarquia("JEFE","OF. JEFE."),
                                                             new Jerarquia("SUBINSPECTOR","SUBINSPECTOR"),
                                                             new Jerarquia("INSPECTOR","INSPECTOR"),
                                                             new Jerarquia("COMISIONADO MAYOR","COM. MY."),
                                                             new Jerarquia("COMISIONADO GENERAL","COM. GRAL."),
                                                              };

        public Hash()
        {
            InitializeComponent();
            comboBox3.DataSource = jerarquias;
            comboBox2.DataSource = jerarquias;

        }


        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog dialogoSeleccionCarpeta = new FolderBrowserDialog();
                DialogResult resultado = dialogoSeleccionCarpeta.ShowDialog();

                // Verificar si el usuario seleccionó una carpeta
                if (resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(dialogoSeleccionCarpeta.SelectedPath))
                {
                    // La carpeta seleccionada por el usuario
                    carpetaSeleccionada = dialogoSeleccionCarpeta.SelectedPath;

                    // Obtener todos los archivos dentro de la carpeta seleccionada y sus subcarpetas
                    //string[] archivos = Directory.GetFiles(carpetaSeleccionada, "*", SearchOption.AllDirectories);
                    RutaArchivos.AddRange(Directory.GetFiles(carpetaSeleccionada, "*", SearchOption.AllDirectories));

                    // Mostrar los nombres de los archivos en la consola
                    foreach (string archivo in RutaArchivos)
                    {
                        Archivo archivo1 = new Archivo(archivo);
                        archivo1.Nro_Orden = formulario.ListaArchivos.Count + 1;
                        formulario.ListaArchivos.Add(archivo1);
                        NombreArchivos.Add(archivo1.Nombre);
                    }

                    Actualizar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Actualizar()
        {
            formulario.Contar();
            lblAudio.Text = formulario.Texto.ToString();
            lblClip.Text = formulario.Clips.ToString();
            lblAudio.Text = formulario.Audio.ToString();
            lblImg.Text = formulario.Imagenes.ToString();
            lblVarios.Text = formulario.Varios.ToString();

            listBoxArchivos.DataSource = null;
            listBoxArchivos.DataSource = NombreArchivos;

            DgvElementos.DataSource = null;
            DgvElementos.DataSource = formulario.ListaArchivos;
            DgvElementos.Columns["Nro_Orden"].HeaderText = "Nro Orden";
            DgvElementos.Columns["SI"].HeaderText = "Reprodu- cible";
            DgvElementos.Columns["Extension"].HeaderText = "Ext.";
            DgvElementos.Columns["Nro_Orden"].Width = 30;
            DgvElementos.Columns["Extension"].Width= 25;
            DgvElementos.Columns["Si"].Width = 25;
            DgvElementos.Columns["Peso"].Width = 60;
            //DgvElementos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                if (listBoxArchivos.SelectedItem != null)
                {
                    foreach (string archivo in listBoxArchivos.SelectedItems)
                    {
                        NombreArchivos.Remove(NombreArchivos.Find(x => x.Contains(archivo.ToString())));
                        formulario.ListaArchivos.Remove(formulario.ListaArchivos.Find(x => x.Nombre.Contains(archivo.ToString())));
                    }

                    Actualizar();

                }
                else
                {
                    MessageBox.Show("Seleccione el Arhivo a eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void listaCanciones_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.All;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listaCanciones_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] datos = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (var archivoMp3 in datos)
                {
                    NombreArchivos.Add(Path.GetFileName(archivoMp3));
                    //ListaArchivos.Add(archivoMp3);
                }

                listBoxArchivos.DataSource = null;
                listBoxArchivos.DataSource = NombreArchivos;
                //Reproductor.URL = RutasArchivos[0];
                MessageBox.Show("Ar cargado Correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void btnCopiar_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialogoSeleccionCarpeta = new FolderBrowserDialog();
                DialogResult resultado = dialogoSeleccionCarpeta.ShowDialog();
                bool carpetaAbierta = false;
                // Verificar si el usuario seleccionó una carpeta
                if (resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(dialogoSeleccionCarpeta.SelectedPath))
                {
                    CarpetaDestino = dialogoSeleccionCarpeta.SelectedPath;
                    // La carpeta seleccionada por el usuario

                    foreach (var ruta in RutaArchivos)
                    {
                        string rutaArchivoOrigen = ruta;
                        string nombreArchivoDestino = Path.GetFileName(rutaArchivoOrigen);
                        string rutaArchivoDestino = Path.Combine(CarpetaDestino, nombreArchivoDestino);
                        File.Copy(rutaArchivoOrigen, rutaArchivoDestino, true);
                        if (!carpetaAbierta)
                        {
                            // Abre el explorador de archivos y navega a la carpeta deseada
                            System.Diagnostics.Process.Start(CarpetaDestino);

                            // Establece la variable carpetaAbierta en true para indicar que ya se ha abierto la carpeta
                            carpetaAbierta = true;
                        }
                    }

                    MessageBox.Show("Archivos copiados exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool VerificarObjetoCompleto(object objeto)
        {
            // Si el objeto es nulo, devuelve false
            if (objeto == null)
            {
                return false;
            }

            // Recorre todas las propiedades del objeto
            foreach (var propiedad in objeto.GetType().GetProperties())
            {
                var valor = propiedad.GetValue(objeto, null);

                // Si la propiedad es nula o vacía, devuelve false
                if (valor == null || (valor is string && string.IsNullOrEmpty((string)valor)))
                {
                    return false;
                }

                // Si la propiedad es otro objeto, llama recursivamente a este método
             
            }

            // Si todas las propiedades del objeto y sus sub-objetos tienen datos, devuelve true
            return true;
        }


        private void buttonImprimir_Click(object sender, EventArgs e)
        {

            try
            {
                CargarFormulario();
                if (VerificarObjetoCompleto(formulario))
                {
                    Form_Impresion form_Impresion = new Form_Impresion(formulario);
                    form_Impresion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Falta informacion para realizar la informacion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarFormulario()
        {

            try
            {
                formulario.Nro_Hash = (int)numericUpDownHash.Value;
                formulario.Tipo = rbtnVuelo.Checked == true ? "VUELO" : "PROCEDIMIENTO";
                formulario.Procedimiento = textBoxProcedimiento.Text;
                formulario.OfEntrega = new Oficial(Convert.ToInt32(textBoxControlOfEntrega.Text), textBoxNomEntrega.Text);
                formulario.OfEntrega.Jerarquia = (Jerarquia)comboBox2.SelectedItem;
                formulario.OfRecibe = new Oficial(Convert.ToInt32(textBoxConOfRecibe.Text), textBoxNomOfRecibe.Text);
                formulario.OfRecibe.Jerarquia = (Jerarquia)comboBox3.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxControlOfEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                MessageBox.Show("Se iria a buscar en la base de datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBoxConOfRecibe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                MessageBox.Show("Se iria a buscar en la base de datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}



