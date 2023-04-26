using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Transcripcion
{
    public partial class Trancripciones : Form
    {
        List<string> ArchivosMP3 = new List<string>();
        List<string> RutasArchivosMP3 = new List<string>();

        public Trancripciones()
        {
            InitializeComponent();
        }


        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = ("Archivos de audio |*.wma;*.mp3;*.wav");
                var result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {

                    if (openFileDialog.CheckFileExists == true)
                    {
                        ArchivosMP3.AddRange(openFileDialog.SafeFileNames.ToList());
                        RutasArchivosMP3.AddRange(openFileDialog.FileNames.ToList());
                        listaCanciones.DataSource = null;
                        listaCanciones.DataSource = ArchivosMP3;
                        Reproductor.URL = RutasArchivosMP3[0];
                        listaCanciones.SelectedIndex = 0;
                        MessageBox.Show("Audio cargado Correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void buttonEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                if (listaCanciones.SelectedItem != null)
                {
                    foreach (string cancion in listaCanciones.SelectedItems)
                    {
                        ArchivosMP3.Remove(ArchivosMP3.Find(x => x.Contains(cancion.ToString())));
                        RutasArchivosMP3.Remove(RutasArchivosMP3.Find(x => x.Contains(cancion.ToString())));
                    }

                    listaCanciones.DataSource = null;
                    listaCanciones.DataSource = ArchivosMP3;
                    Reproductor.URL = null;
                }
                else
                {
                    MessageBox.Show("Seleccione el Audio a eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reproductor_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (Reproductor.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                Reproductor.Ctlcontrols.currentPosition -= 2;
            }

        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 || e.KeyCode == Keys.F3)
            {
                if (Reproductor.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    Reproductor.Ctlcontrols.play();
                }
                else
                {
                    Reproductor.Ctlcontrols.pause();
                }
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
        private void listaCanciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaCanciones.SelectedItem != null)
            {
                Reproductor.URL = RutasArchivosMP3[listaCanciones.SelectedIndex];
            }
        }
        private void listaCanciones_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] datos = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (var archivoMp3 in datos)
                {
                    ArchivosMP3.Add(Path.GetFileName(archivoMp3));
                    RutasArchivosMP3.Add(archivoMp3);
                }

                listaCanciones.DataSource = null;
                listaCanciones.DataSource = ArchivosMP3;
                Reproductor.URL = RutasArchivosMP3[0];
                MessageBox.Show("Audio cargado Correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                if (radioButtonA.Checked)
                {
                    listBoxConversacion.Items.Add("A:" + textBox1.Text);
                    radioButtonB.Checked = true;
                    textBox1.Text = "";
                }
                else
                {
                    listBoxConversacion.Items.Add("B:" + textBox1.Text);
                    radioButtonA.Checked = true;
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No hay nada para agregar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAgregar_Click(null, null);
                e.Handled = true;
                this.listBoxConversacion.SelectedIndex = this.listBoxConversacion.Items.Count - 1;
            }
        }
    }
}

