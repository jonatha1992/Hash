using Hash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace Hash
{
    public partial class Form_Hash: Form
    {
        List<string> NombreArchivos = new List<string>();
        List<string> RutaArchivos = new List<string>();
        Formulario_Hash formulario_hash = new Formulario_Hash();
        List<BEOficial> listaOficiales = new List<BEOficial>();
        List<BEJerarquia> listaJerarquias = BEJerarquia.ListarJeraquias();

        string carpetaSeleccionada;

        

        public Form_Hash()
        {
            InitializeComponent();

            try
            {

                comboBoxJerarquiaRecibe.DataSource = listaJerarquias;
                comboBoxJerarquiaEntrega.DataSource = listaJerarquias?.ConvertAll(item => (BEJerarquia)item.Clone());
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
                textBoxDescripcion.AutoCompleteCustomSource = source2;
                textBoxDescripcion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBoxDescripcion.AutoCompleteSource = AutoCompleteSource.CustomSource;

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

                        archivo1.Nro_Orden = formulario_hash.ListaArchivos.Count + 1;
                        formulario_hash.ListaArchivos.Add(archivo1);
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
            formulario_hash.Contar();
            lblAudio.Text = formulario_hash.Texto.ToString();
            lblClip.Text = formulario_hash.Clips.ToString();
            lblAudio.Text = formulario_hash.Audio.ToString();
            lblImg.Text = formulario_hash.Imagenes.ToString();
            lblVarios.Text = formulario_hash.Varios.ToString();


            DgvElementos.DataSource = null;
            DgvElementos.DataSource = formulario_hash.ListaArchivos;
            DgvElementos.Columns["Nro_Orden"].HeaderText = "Nro Orden";
            DgvElementos.Columns["PesoArchivo"].HeaderText = "Peso";
            DgvElementos.Columns["Hash"].HeaderText = "Hash SHA1";
            DgvElementos.Columns["SI"].Visible = false;
            DgvElementos.Columns["Extension"].HeaderText = "Ext.";
            DgvElementos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DgvElementos.Columns["Hash"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DgvElementos.Columns["Nro_Orden"].Width = 40;
            DgvElementos.Columns["Extension"].Width = 35;
            DgvElementos.Columns["PesoArchivo"].Width = 65;
            DgvElementos.Columns["Peso"].Visible= false;
            labelPesoTotal.Text = formulario_hash.pesototal;
            //DgvElementos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




       

        private void textBoxNomOfRecibe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                var oficial = listaOficiales.Find(X => X.NombreCompleto == textBoxNomOfRecibe.Text);

                if (oficial != null)
                {
                    numericUpDownRecibe.Value = oficial.Legajo;
                    comboBoxJerarquiaRecibe.Text = oficial.Jerarquia.Nombre;
                    comboBoxDestRecibe.Text = oficial.Destino.Nombre;

                }
            }
        }

           


        private void textBoxNomEntrega_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                var oficial = listaOficiales.Find(X => X.NombreCompleto == textBoxNomEntrega.Text);

                if (oficial != null)
                {
                    numericUpDownEntrega.Value = oficial.Legajo;
                    comboBoxJerarquiaEntrega.Text = oficial.Jerarquia.Nombre;
                    comboBoxDestEntrega.Text = oficial.Destino.Nombre;
                }
            }
        }

        private void numericUpDownEntrega_Enter(object sender, EventArgs e)
        {

            var oficial = listaOficiales.Find(X => X.Legajo == numericUpDownEntrega.Value);

            if (oficial != null)
            {
                textBoxNomEntrega.Text = oficial.NombreCompleto;
                comboBoxJerarquiaEntrega.Text = oficial.Jerarquia.Nombre;
                comboBoxDestEntrega.Text = oficial.Destino.Nombre;

            }
        }

        private void numericUpDownRecibe_Enter(object sender, EventArgs e)
        {
            var oficial = listaOficiales.Find(X => X.Legajo == numericUpDownEntrega.Value);

            if (oficial != null)
            {
                textBoxNomOfRecibe.Text = oficial.NombreCompleto;
                comboBoxJerarquiaRecibe.Text = oficial.Jerarquia.Nombre;
                comboBoxDestRecibe.Text = oficial.Destino.Nombre;
            }
        }
        private bool CargarFormularioHash()
        {
            try
            {
                formulario_hash.Nro_Hash = (int)numericUpDownHash.Value;
                formulario_hash.Descripcion = textBoxDescripcion.Text;
                formulario_hash.OfEntrega = new BEOficial(Convert.ToInt32(numericUpDownEntrega.Value), textBoxNomEntrega.Text);
                formulario_hash.OfEntrega.Jerarquia = (BEJerarquia)comboBoxJerarquiaEntrega.SelectedItem;
                formulario_hash.OfEntrega.Destino = new BEDestino( comboBoxJerarquiaEntrega.Text);
                BEDestino.AgregaDestino(formulario_hash.OfEntrega.Destino);
                BEOficial.AgregarOficial(formulario_hash.OfEntrega);

                if (numericUpDownRecibe.Value == 0  || textBoxNomOfRecibe.Text == "" )
                {
                    formulario_hash.OfRecibe = new BEOficial(Convert.ToInt32(numericUpDownEntrega.Value), textBoxNomOfRecibe.Text);
                    formulario_hash.OfRecibe.Jerarquia = (BEJerarquia)comboBoxJerarquiaRecibe.SelectedItem;
                    formulario_hash.OfRecibe.Destino = new BEDestino(comboBoxJerarquiaRecibe.Text);
                    BEOficial.AgregarOficial(formulario_hash.OfRecibe);
                    BEDestino.AgregaDestino(formulario_hash.OfRecibe.Destino);
                }
                else
                {
                    formulario_hash.OfRecibe = null;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private void buttonImprimirHash_Click(object sender, EventArgs e)
        {
            try
            {
                if (CargarFormularioHash() && DgvElementos.DataSource != null && DgvElementos.Rows?.Count > 0)
                {
                    Form_Impresion form_Impresion = new Form_Impresion(formulario_hash);
                    form_Impresion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Falta informacion para realizar el Hash", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}



