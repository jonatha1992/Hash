﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
        public Hash()
        {
            InitializeComponent();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                //if (radioButtonA.Checked)
                //{
                //    //listBoxConversacion.Items.Add("A:" + textBox1.Text);
                //    //radioButtonB.Checked = true;
                //    textBox1.Text = "";
                //}
                //else
                //{
                //    //listBoxConversacion.Items.Add("B:" + textBox1.Text);
                //    //radioButtonA.Checked = true;
                //    textBox1.Text = "";
                //}
            }
            else
            {
                MessageBox.Show("No hay nada para agregar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

     

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog dialogoSeleccionCarpeta = new FolderBrowserDialog();
                DialogResult resultado = dialogoSeleccionCarpeta.ShowDialog();

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
                    }

                    MessageBox.Show("Archivos copiados exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}

public class Archivo
{

    public int Nro_Orden { get; set; }
    public string Nombre { get; set; }
    public string Extension { get; set; }
    public int Peso { get; set; }
    public string Hash { get; set; }
    public string Si { get => "SI"; } 
    public Archivo() { }
    public Archivo(string rutaArchivo)
    {
        this.Extension = Path.GetExtension(rutaArchivo);
        this.Peso = (int)new FileInfo(rutaArchivo).Length;
        this.Nombre = Path.GetFileName(rutaArchivo);

        using (var stream = new BufferedStream(File.OpenRead(rutaArchivo), 1200000))
        {
            SHA1 sha =  SHA1.Create();
            byte[] hashBytes = sha.ComputeHash(stream);
            this.Hash = BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}

public class Formulario_Hash
{
    public int Hash { get; set; }
    public int Nro_Control { get; set; }
    public int Imagenes { get; set; }
    public int Clips  { get; set; }
    public int Audio  { get; set; }
    public int Texto  { get; set; }
    public int Varios  { get; set; }
    public string  Procedimiento  { get; set; }
    public List<Archivo> ListaArchivos { get; set; }

    public Formulario_Hash()
    {
        ListaArchivos = new List<Archivo>();
    }
    public void Contar()
    {
        Imagenes = ListaArchivos.Count(x => x.Extension == ".jpg" || x.Extension == ".png" || x.Extension == ".bmp");
        Clips = ListaArchivos.Count(a => a.Extension == ".mp4" || a.Extension == ".avi" || a.Extension == ".mov");
        Audio = ListaArchivos.Count(a => a.Extension == ".mp3" || a.Extension == ".wav" || a.Extension == ".flac");
        Texto= ListaArchivos.Count(a => a.Extension == ".pdf" || a.Extension == ".txt" || a.Extension == ".docx" || a.Extension == ".doc");
        Varios= ListaArchivos.Count(a => a.Extension != ".txt" && a.Extension != ".doc" && a.Extension != ".docx" && a.Extension != ".pdf"
        && a.Extension != ".png" && a.Extension != ".jpg" && a.Extension != ".jpeg" && a.Extension != ".gif" && a.Extension != ".bmp"
        && a.Extension != ".mp4" && a.Extension != ".avi" && a.Extension != ".wmv" && a.Extension != ".mov");
    }

}