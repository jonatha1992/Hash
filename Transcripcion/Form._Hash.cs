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
        Formulario_Hash formulario = new Formulario_Hash();
        List<BEOficial> listaOficiales = new List<BEOficial>();
        List<BEJerarquia> listaJerarquias = BEJerarquia.ListarJeraquias();

        string carpetaSeleccionada;

        

        public Form_Hash()
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
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvElementos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void textBoxNomEntrega_TextChanged(object sender, EventArgs e)
        {



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

                var oficial = listaOficiales.Find(X => X.NombreCompleto == textBoxNomOfRecibe.Text);       
                
                if (oficial != null)
                {
                    textBoxNomOfRecibe.Text = oficial.NombreCompleto;
                    comboBoxRecibe.Text = oficial.Jerarquia.Nombre;
                }
            }
        }
        private void textBoxNomEntrega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                var oficial = listaOficiales.Find(X => X.NombreCompleto == textBoxNomOfRecibe.Text);

                if (oficial != null)
                {
                    textBoxControlOfEntrega.Text = oficial.Legajo.ToString();
                    comboBoxEntrega.Text = oficial.jerarquia.jerarquia;
                }
            }
        }


    }
}



