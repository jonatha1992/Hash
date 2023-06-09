﻿using Hash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace Transcripcion
{
    public partial class Hash : Form
    {
        List<string> NombreArchivos = new List<string>();
        List<string> RutaArchivos = new List<string>();
        Formulario_Hash formulario = new Formulario_Hash();
        List<BEOficial> listaOficiales = new List<BEOficial>();
        List<BEJerarquia> listaJerarquias = BEJerarquia.ObtenerJerarquias();
        Formulario_Custodia formulario_Custodia = new Formulario_Custodia();

        string CarpetaDestino;
        string carpetaSeleccionada;

        List<string> sugerencias = new List<string>()
        {
    "KLM 702",
    "MP 6912",
    "ODT",
        };


        public Hash()
        {
            InitializeComponent();

            try
            {


                comboBoxRecibe.DataSource = listaJerarquias;
                comboBoxEntrega.DataSource = listaJerarquias?.ConvertAll(item => (BEJerarquia)item.Clone());
                listaOficiales = BEOficial.ObtenerOficiales();



                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                foreach (BEOficial oficial in listaOficiales)
                {
                    source.Add(oficial.NombreCompleto);
                }
                textBoxNomEntrega.AutoCompleteCustomSource = source;
                textBoxNomEntrega.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBoxNomEntrega.AutoCompleteSource = AutoCompleteSource.CustomSource;

                textBoxNomOfRecibe.AutoCompleteCustomSource = source;
                textBoxNomOfRecibe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBoxNomOfRecibe.AutoCompleteSource = AutoCompleteSource.CustomSource;



                AutoCompleteStringCollection source2 = new AutoCompleteStringCollection();
                source2.AddRange(sugerencias.ToArray());
                textBoxProcedimiento.AutoCompleteCustomSource = source2;
                textBoxProcedimiento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBoxProcedimiento.AutoCompleteSource = AutoCompleteSource.CustomSource;

                labelPesoTotal.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message} ", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }



        private string calcularHash(string rutaArchivo)
        {
            string hash = "";
            long fileSize = new FileInfo(rutaArchivo).Length;
            long totalBytesRead = 0;
            using (var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1048576, FileOptions.SequentialScan))
            {
                using (var sha = SHA256.Create())
                {
                    byte[] buffer = new byte[1048576];
                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        sha.TransformBlock(buffer, 0, bytesRead, buffer, 0);

                        totalBytesRead += bytesRead;
                    }
                    sha.TransformFinalBlock(buffer, 0, 0);
                    byte[] hashBytes = sha.Hash;
                    hash = BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
            return hash;
        }


        private void buttonCarpetaSeleccionada_Click(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog dialogoSeleccionCarpeta = new FolderBrowserDialog();
                dialogoSeleccionCarpeta.RootFolder = Environment.SpecialFolder.MyComputer;

                DialogResult resultado = dialogoSeleccionCarpeta.ShowDialog();

                // Verificar si el usuario seleccionó una carpeta
                if (resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(dialogoSeleccionCarpeta.SelectedPath))
                {
                    // La carpeta seleccionada por el usuario
                    carpetaSeleccionada = dialogoSeleccionCarpeta.SelectedPath;

                    MostrarSpriner();


                    RutaArchivos.AddRange(Directory.GetFiles(carpetaSeleccionada, "*", SearchOption.AllDirectories));

                    int totalArchivos = RutaArchivos.Count;
                    circularProgressBar1.Maximum = totalArchivos;
                    int archivosProcesados = 0;

                    // Mostrar los nombres de los archivos en la consola
                    foreach (string archivo in RutaArchivos)
                    {
                        BEArchivo archivo1 = new BEArchivo(archivo);
                        archivo1.Hash = calcularHash(archivo);

                        archivo1.Nro_Orden = formulario.ListaArchivos.Count + 1;
                        formulario.ListaArchivos.Add(archivo1);
                        NombreArchivos.Add(archivo1.Nombre);

                        archivosProcesados++;
                        circularProgressBar1.Value = archivosProcesados;
                        circularProgressBar1.Text = $"{(int)((archivosProcesados / (double)totalArchivos) * 100)}%";
                    }
                    OcultarSpriner();
                    Actualizar();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void MostrarSpriner()
        {
            circularProgressBar1.Text = "0%";
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Visible = true;

        }

        public void OcultarSpriner()
        {
            circularProgressBar1.Visible = false;

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
            DgvElementos.Columns["PesoArchivo"].HeaderText = "Peso";
            DgvElementos.Columns["SI"].Visible = false;
            DgvElementos.Columns["Extension"].HeaderText = "Ext.";
            DgvElementos.Columns["Nro_Orden"].Width = 30;
            DgvElementos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DgvElementos.Columns["Extension"].Width = 35;
            DgvElementos.Columns["PesoArchivo"].Width = 65;
            DgvElementos.Columns["Peso"].Visible= false;
            //DgvElementos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            labelPesoTotal.Text = formulario.pesototal;

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
                    string carpetaRecord = "Record";
                    int contador = 1;

                    // Verificar si la carpeta "Record" ya existe en la carpeta destino
                    while (Directory.Exists(Path.Combine(CarpetaDestino, carpetaRecord)))
                    {
                        carpetaRecord = $"Record{contador}";
                        contador++;
                    }

                    // Crear la carpeta "Record" dentro de la carpeta destino
                    string rutaCarpetaRecord = Path.Combine(CarpetaDestino, carpetaRecord);
                    Directory.CreateDirectory(rutaCarpetaRecord);

                    // La carpeta seleccionada por el usuario
                    MostrarSpriner();
                    circularProgressBar1.Maximum = RutaArchivos.Count;
                    foreach (var ruta in RutaArchivos)
                    {
                        string rutaArchivoOrigen = ruta;
                        string nombreArchivoDestino = Path.GetFileName(rutaArchivoOrigen);
                        string rutaArchivoDestino = Path.Combine(rutaCarpetaRecord, nombreArchivoDestino);
                        File.Copy(rutaArchivoOrigen, rutaArchivoDestino, true);
                        if (!carpetaAbierta)
                        {
                            // Abre el explorador de archivos y navega a la carpeta deseada
                            System.Diagnostics.Process.Start(rutaCarpetaRecord);

                            // Establece la variable carpetaAbierta en true para indicar que ya se ha abierto la carpeta
                            carpetaAbierta = true;

                        }
                        circularProgressBar1.Value++;
                        circularProgressBar1.Text = $"{(circularProgressBar1.Value * 100) / circularProgressBar1.Maximum}%";
                    }
                    OcultarSpriner();
                    MessageBox.Show($"Archivos copiados exitosamente en la carpeta\n\n{rutaCarpetaRecord} ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                formulario.OfEntrega = new BEOficial(Convert.ToInt32(textBoxControlOfEntrega.Text), textBoxNomEntrega.Text);
                formulario.OfEntrega.jerarquia = (BEJerarquia)comboBoxEntrega.SelectedItem;
                formulario.OfRecibe = new BEOficial(Convert.ToInt32(textBoxConOfRecibe.Text), textBoxNomOfRecibe.Text);
                formulario.OfRecibe.jerarquia = (BEJerarquia)comboBoxRecibe.SelectedItem;

                BEOficial.AgregarOficial(formulario.OfEntrega);
                BEOficial.AgregarOficial(formulario.OfRecibe);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImpCustodia_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCustodia();
                if (VerificarObjetoCompleto(formulario_Custodia))
                {
                    Form_Impresion form_Impresion = new Form_Impresion(formulario_Custodia);
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
        private void CargarCustodia()
        {
            try
            {
                formulario_Custodia.Nro_Hash = (int)numericUpDownHash.Value;
                formulario_Custodia.Hora = dateTimePicker1.Value;
                formulario_Custodia.Tipo = rbtnVuelo.Checked == true ? "VUELO" : "PROCEDIMIENTO";
                formulario_Custodia.Procedimiento = textBoxProcedimiento.Text;
                formulario_Custodia.OfEntrega = new BEOficial(Convert.ToInt32(textBoxControlOfEntrega.Text), textBoxNomEntrega.Text);
                formulario_Custodia.OfEntrega.jerarquia = (BEJerarquia)comboBoxEntrega.SelectedItem;
                formulario_Custodia.OfRecibe = new BEOficial(Convert.ToInt32(textBoxConOfRecibe.Text), textBoxNomOfRecibe.Text);
                formulario_Custodia.OfRecibe.jerarquia = (BEJerarquia)comboBoxRecibe.SelectedItem;
                //formulario_Custodia.ListaArchivos = formulario.ListaArchivos;
                BEOficial.AgregarOficial(formulario_Custodia.OfEntrega);
                BEOficial.AgregarOficial(formulario_Custodia.OfRecibe);
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

                var oficial = BuscarOficial(Convert.ToInt32(textBoxControlOfEntrega.Text));
                if (oficial != null)
                {
                    textBoxNomEntrega.Text = oficial.NombreCompleto;
                    comboBoxEntrega.Text = oficial.jerarquia.jerarquia;
                }
                else
                {
                    MessageBox.Show("El Oficial no se encuentra registrado cuando se imprima el Hash se guarda los datos del Ofical para el futuros Hash", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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

                var oficial = BuscarOficial(Convert.ToInt32(textBoxConOfRecibe.Text));
                if (oficial != null)
                {
                    textBoxNomOfRecibe.Text = oficial.NombreCompleto;
                    comboBoxRecibe.Text = oficial.jerarquia.jerarquia;
                }
            }
        }

        public BEOficial BuscarOficial(int legajo)
        {
            return listaOficiales.Find(o => o.Legajo == legajo);

        }
        private void listBoxArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxArchivos.SelectedIndex >= 0) // Si hay algún elemento seleccionado
            {
                buttonEliminar.Visible = true; // Mostrar el botón de eliminar
            }
            else // Si no hay ningún elemento seleccionado
            {
                buttonEliminar.Visible = false; // Ocultar el botón de eliminar
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void LimpiarControles(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                else if (c is NumericUpDown)
                {
                    ((NumericUpDown)c).Value = 0;
                }
                else if (c.HasChildren)
                {
                    LimpiarControles(c);
                }
            }
        }

        private void textBoxNomEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void textBoxNomOfRecibe_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);

        }

        private void textBoxProcedimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);

        }


        public BEOficial BuscarOficial(int? legajo, string nombre)
        {
            return listaOficiales.Find(o => o.NombreCompleto == nombre);

        }

        private void textBoxNomEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                var oficial = BuscarOficial(null, textBoxNomEntrega.Text);
                if (oficial != null)
                {
                    textBoxControlOfEntrega.Text = oficial.Legajo.ToString();
                    comboBoxEntrega.Text = oficial.jerarquia.jerarquia;
                }
            }
        }

        private void textBoxNomOfRecibe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                var oficial = BuscarOficial(null, textBoxNomOfRecibe.Text);
                if (oficial != null)
                {
                    textBoxConOfRecibe.Text = oficial.Legajo.ToString();
                    comboBoxRecibe.Text = oficial.jerarquia.jerarquia;
                }

            }
        }

        private void rdbtnProcedimiento_CheckedChanged(object sender, EventArgs e)
        {
            buttonImpCustodia.Visible = false;
        }

        private void rbtnVuelo_CheckedChanged(object sender, EventArgs e)
        {
            buttonImpCustodia.Visible = true;
        }


    }
}



