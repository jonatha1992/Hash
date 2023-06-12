using Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace Transcripcion
{
    public partial class Hash : Form
    {
        List<string> NombreArchivos = new List<string>();
        List<string> RutaArchivos = new List<string>();
        Formulario_Hash formularioHash = new Formulario_Hash();
        List<BEOficial> listaOficiales = new List<BEOficial>();
        List<BEAeropuerto> Aeropuertos = BEAeropuerto.ObtenerAeropuertos();
        List<BEJerarquia> listaJerarquias = BEJerarquia.ObtenerJerarquias();
        List<BEProcedimiento> procedimientos = BEProcedimiento.ObtenerProcedimientos();
        List<BEDependencia> Dependencias = BEDependencia.ObtenerDepencias();
        Formulario_Custodia formulario_Custodia = new Formulario_Custodia();

        string carpetaSeleccionada;

        private BackgroundWorker hashWorker;


        public Hash()
        {
            InitializeComponent();

            try
            {

                comboBoxJerarquiaRecibe.DataSource = listaJerarquias;
                comboBoxJerarquiaEntrega.DataSource = listaJerarquias?.ConvertAll(item => (BEJerarquia)item.Clone());
                comboBoxAeropuertos.DataSource = Aeropuertos;
                comboBoxDependeciaEntrega.DataSource = Dependencias;
                comboBoxDependeciaRecibe.DataSource = Dependencias?.ConvertAll(item => (BEDependencia)item.Clone());
                listaOficiales = BEOficial.ObtenerOficiales();




                // Inicializar propiedades de autocompletado para textBoxNomEntrega
                SetAutoComplete(textBoxNomEntrega, listaOficiales, oficial => oficial.NombreCompleto);

                // Inicializar propiedades de autocompletado para textBoxNomOfRecibe
                SetAutoComplete(textBoxNomOfRecibe, listaOficiales, oficial => oficial.NombreCompleto);

                // Inicializar propiedades de autocompletado para textBoxCaratula
                SetAutoComplete(textBoxCaratula, procedimientos, procedimiento => procedimiento.Nombre);

                labelPesoTotal.Text = "0";

                checkBox1_CheckedChanged(null, null);
                comboBoxTipo_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void SetAutoComplete<T>(TextBox textBox, IEnumerable<T> items, Func<T, string> selector)
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(items.Select(selector).ToArray());

            textBox.AutoCompleteCustomSource = source;
            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }



        private void buttonCarpetaSeleccionada_Click2(object sender, EventArgs e)
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

                    // Inicializar el BackgroundWorker
                    hashWorker = new BackgroundWorker();
                    hashWorker.WorkerReportsProgress = true;
                    hashWorker.DoWork += HashWorker_DoWork;
                    hashWorker.ProgressChanged += HashWorker_ProgressChanged;
                    hashWorker.RunWorkerCompleted += HashWorker_RunWorkerCompleted;

                    // Obtener la lista de archivos en la carpeta seleccionada
                    RutaArchivos.AddRange(Directory.GetFiles(carpetaSeleccionada, "*", SearchOption.AllDirectories));

                    // Iniciar el cálculo del hash en segundo plano
                    hashWorker.RunWorkerAsync(RutaArchivos.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void HashWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] archivos = (string[])e.Argument;
            int totalArchivos = archivos.Length;
            int archivosProcesados = 0;

            foreach (string archivo in archivos)
            {
                BEArchivo archivo1 = new BEArchivo(archivo);
                archivo1.Hash = calcularHash2(archivo, hashWorker);
                archivo1.Nro_Orden = formularioHash.ListaArchivos.Count + 1;
                formularioHash.ListaArchivos.Add(archivo1);

                // Actualizar la lista de nombres de archivos en el hilo principal
                Invoke((Action)(() =>
                {
                    NombreArchivos.Add(archivo1.Nombre);
                }));

                archivosProcesados++;

                int porcentajeCompletado = (int)((double)archivosProcesados / totalArchivos * 100);
                hashWorker.ReportProgress(porcentajeCompletado);
                // Informar el progreso actual al BackgroundWorker
                //hashWorker.ReportProgress(archivosProcesados, totalArchivos);
            }
        }

        private void HashWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int archivosProcesados = e.ProgressPercentage;

            Invoke((Action)(() =>
            {
                circularProgressBar1.Value = archivosProcesados;
                circularProgressBar1.Text = $"{archivosProcesados}%";

            }));
        }

        private void HashWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OcultarSpriner();
            Actualizar();
        }
        private string calcularHash2(string rutaArchivo, BackgroundWorker worker)
        {
            string hash = "";
            long fileSize = new FileInfo(rutaArchivo).Length;
            long totalBytesRead = 0;
            using (var stream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1048576, FileOptions.SequentialScan))
            {
                using (var sha = SHA1.Create())
                {
                    byte[] buffer = new byte[1048576];
                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        sha.TransformBlock(buffer, 0, bytesRead, buffer, 0);

                        totalBytesRead += bytesRead;

                        // Informar el progreso actual al BackgroundWorker
                        //worker.ReportProgress((int)((totalBytesRead / (double)fileSize) * 100));
                    }
                    sha.TransformFinalBlock(buffer, 0, 0);
                    byte[] hashBytes = sha.Hash;
                    hash = BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
            return hash;
        }


        public void MostrarSpriner()
        {
            Invoke((Action)(() =>
            {
                circularProgressBar1.Value = 0;
                circularProgressBar1.Minimum = 0;
                circularProgressBar1.Maximum = 100;
                circularProgressBar1.Visible = true;
            }));
        }
        public void OcultarSpriner()
        {

            Invoke((Action)(() =>
            {
                circularProgressBar1.Visible = false;
            }));
        }

        private void Actualizar()
        {
            formularioHash.Contar();
            lblAudio.Text = formularioHash.Texto.ToString();
            lblClip.Text = formularioHash.Clips.ToString();
            lblAudio.Text = formularioHash.Audio.ToString();
            lblImg.Text = formularioHash.Imagenes.ToString();
            lblVarios.Text = formularioHash.Varios.ToString();

            listBoxArchivos.DataSource = null;
            listBoxArchivos.DataSource = NombreArchivos;

            DgvElementos.DataSource = null;
            DgvElementos.DataSource = formularioHash.ListaArchivos;
            DgvElementos.Columns["Nro_Orden"].HeaderText = "Nro Orden";
            DgvElementos.Columns["PesoArchivo"].HeaderText = "Peso";
            DgvElementos.Columns["SI"].Visible = false;
            DgvElementos.Columns["Extension"].HeaderText = "Ext.";
            DgvElementos.Columns["Nro_Orden"].Width = 40;
            DgvElementos.Columns["Nro_Orden"].Width = 40;
            DgvElementos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DgvElementos.Columns["Extension"].Width = 35;
            DgvElementos.Columns["PesoArchivo"].Width = 65;
            DgvElementos.Columns["Peso"].Visible = false;
            DgvElementos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvElementos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //DgvElementos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            labelPesoTotal.Text = formularioHash.pesototal;

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
                        formularioHash.ListaArchivos.Remove(formularioHash.ListaArchivos.Find(x => x.Nombre.Contains(archivo.ToString())));
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


        private void buttonImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (CargarFormularioHash() && DgvElementos.DataSource != null && DgvElementos.Rows?.Count > 0)
                {
                    Form_Impresion form_Impresion = new Form_Impresion(formularioHash);
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

        private bool CargarFormularioHash()
        {
            try
            {
                formularioHash.Nro_Hash = (int)numericUpDownHash.Value;
                formularioHash.Tipo = comboBoxTipo.Text;
                formularioHash.Procedimiento = textBoxCaratula.Text;
                formularioHash.OfEntrega = new BEOficial(Convert.ToInt32(textBoxControlOfEntrega.Text), textBoxNomEntrega.Text);
                formularioHash.OfEntrega.jerarquia = (BEJerarquia)comboBoxJerarquiaEntrega.SelectedItem;
                formularioHash.OfEntrega.Dependencia = comboBoxDependeciaEntrega.Text;
                BEOficial.AgregarOficial(formularioHash.OfEntrega);

                if (checkBoxOficialRecibe.Checked)
                {
                    formularioHash.OfRecibe = new BEOficial(Convert.ToInt32(textBoxConOfRecibe.Text), textBoxNomOfRecibe.Text);
                    formularioHash.OfRecibe.jerarquia = (BEJerarquia)comboBoxJerarquiaRecibe.SelectedItem;
                    formularioHash.OfRecibe.Dependencia = comboBoxDependeciaRecibe.Text;
                    BEOficial.AgregarOficial(formularioHash.OfRecibe);
                }
                else
                {
                    formularioHash.OfRecibe = null;
                }
                return true;

            }
            catch (Exception )
            {
                return false;
            }
        }

        private void buttonImpCustodia_Click(object sender, EventArgs e)
        {
            try
            {

                if (CargarCustodia())
                {
                    Form_Impresion form_Impresion = new Form_Impresion(formulario_Custodia);
                    form_Impresion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Falta informacion para realizar la impresion de la Custodia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CargarCustodia()
        {
            try
            {
                formulario_Custodia.Nro_Hash = (int)numericUpDownHash.Value;
                formulario_Custodia.Hora = dateTimePicker1.Value;
                formulario_Custodia.Cartula = textBoxCaratula.Text;
                formulario_Custodia.Sumario = textBoxSumario.Text;
                formulario_Custodia.Juzgado = textBoxJuzgado.Text;
                formulario_Custodia.Secretaria = textBoxSecretaria.Text;
                formulario_Custodia.Utilidad = textBoxUtilidad.Text;
                formulario_Custodia.Identificacion = textBoxIdentificacion.Text;
                formulario_Custodia.Lugar_Recoleccion = textBoxLugar.Text;
                formulario_Custodia.Descripcion = textBoxDescripcion.Text;
                formulario_Custodia.Tipo = comboBoxTipo.Text;
                formulario_Custodia.Procedimiento = textBoxDescripcion.Text;
                formulario_Custodia.OfEntrega = new BEOficial(Convert.ToInt32(textBoxControlOfEntrega.Text), textBoxNomEntrega.Text);
                formulario_Custodia.OfEntrega.jerarquia = (BEJerarquia)comboBoxJerarquiaEntrega.SelectedItem;
                formulario_Custodia.OfEntrega.Dependencia = comboBoxDependeciaEntrega.Text;

                BEOficial.AgregarOficial(formulario_Custodia.OfEntrega);
                if (checkBoxOficialRecibe.Checked)
                {
                    formulario_Custodia.OfRecibe = new BEOficial(Convert.ToInt32(textBoxConOfRecibe.Text), textBoxNomOfRecibe.Text);
                    formulario_Custodia.OfRecibe.jerarquia = (BEJerarquia)comboBoxJerarquiaRecibe.SelectedItem;
                    formulario_Custodia.OfRecibe.Dependencia = comboBoxDependeciaRecibe.Text;
                    BEOficial.AgregarOficial(formulario_Custodia.OfRecibe);
                }
                else
                {
                    formulario_Custodia.OfRecibe = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha surgido un error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
                    comboBoxJerarquiaEntrega.Text = oficial.jerarquia.jerarquia;
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
                    comboBoxJerarquiaRecibe.Text = oficial.jerarquia.jerarquia;
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

        
        private void ConvertirMayusculas(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
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
                    comboBoxJerarquiaEntrega.Text = oficial.jerarquia.jerarquia;
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
                    comboBoxJerarquiaRecibe.Text = oficial.jerarquia.jerarquia;
                }

            }
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (!checkBoxOficialRecibe.Checked)
            {
                textBoxConOfRecibe.Visible = false;
                textBoxNomOfRecibe.Visible = false;
                comboBoxJerarquiaRecibe.Visible = false;
                comboBoxDependeciaRecibe.Visible = false;
                labelDependenciaRecibe.Visible = false;
                labelJerarquiaRecibe.Visible = false;
                labelControlRecibe.Visible = false;
                labelNombreRecibe.Visible = false;
            }
            else
            {
                textBoxConOfRecibe.Visible = true;
                textBoxNomOfRecibe.Visible = true;
                comboBoxJerarquiaRecibe.Visible = true;
                comboBoxDependeciaRecibe.Visible = true;
                labelDependenciaRecibe.Visible = true;
                labelJerarquiaRecibe.Visible = true;
                labelControlRecibe.Visible = true;
                labelNombreRecibe.Visible = true;
            }
        }

        private void comboBoxAeropuertos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipo.Text.Contains("VUELO"))
            {
                var aeropuerto = comboBoxAeropuertos.SelectedItem as BEAeropuerto;
                textBoxDescripcion.Text = $"REGISTROS FILMICOS EN SECTOR DE PLATAFORMA OPERATIVA {aeropuerto.Nombre}";
                textBoxLugar.Text = aeropuerto.Nombre;
            }
        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipo.Text.Contains("VUELO"))
            {
                labelAeropuerto.Visible = true;
                comboBoxAeropuertos.Visible = true;
                var aeropuerto = comboBoxAeropuertos.SelectedItem as BEAeropuerto;
                textBoxDescripcion.Text = $"REGISTROS FILMICOS EN SECTOR DE PLATAFORMA OPERATIVA {aeropuerto.Nombre}";
                textBoxIdentificacion.Text = $"CONTROL E INSPECCIÓN DE SEGURIDAD - HASH NRO:{numericUpDownHash.Value}";
                textBoxLugar.Text = aeropuerto.Nombre;
            }
            else
            {
                labelAeropuerto.Visible = false;
                comboBoxAeropuertos.Visible = false;
                textBoxDescripcion.Text = "";
                textBoxLugar.Text = "";
                textBoxIdentificacion.Text = "";
            }


        }

        private void numericUpDownHash_ValueChanged(object sender, EventArgs e)
        {
            if (comboBoxTipo.Text.Contains("VUELO"))
            {
                textBoxIdentificacion.Text = $"CONTROL E INSPECCIÓN DE SEGURIDAD - HASH NRO:{numericUpDownHash.Value}";
            }
        }

        private void Hash_Load(object sender, EventArgs e)
        {
            // Suscribir el evento KeyPress para los TextBox
            textBoxNomEntrega.KeyPress += ConvertirMayusculas;
            textBoxNomOfRecibe.KeyPress += ConvertirMayusculas;
            textBoxCaratula.KeyPress += ConvertirMayusculas;
            textBoxSumario.KeyPress += ConvertirMayusculas;
            textBoxJuzgado.KeyPress += ConvertirMayusculas;
            textBoxDescripcion.KeyPress += ConvertirMayusculas;
            textBoxSecretaria.KeyPress += ConvertirMayusculas;
            textBoxUtilidad.KeyPress += ConvertirMayusculas;
            textBoxIdentificacion.KeyPress += ConvertirMayusculas;
            textBoxLugar.KeyPress += ConvertirMayusculas;

            // Suscribir el evento KeyPress para los ComboBox
            comboBoxAeropuertos.KeyPress += ConvertirMayusculas;
            comboBoxDependeciaEntrega.KeyPress += ConvertirMayusculas;
            comboBoxDependeciaRecibe.KeyPress += ConvertirMayusculas;
            comboBoxTipo.KeyPress += ConvertirMayusculas;
            comboBoxDependeciaRecibe.KeyPress += ConvertirMayusculas;
            comboBoxDependeciaRecibe.KeyPress += ConvertirMayusculas;

            dateTimePicker1.Value = DateTime.Now;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}



