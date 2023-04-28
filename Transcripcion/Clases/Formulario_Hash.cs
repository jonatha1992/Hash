﻿using System.Collections.Generic;
using System.Linq;

namespace Hash
{
    public class Formulario_Hash
    {
        public int Nro_Hash { get; set; }
        public int Imagenes { get; set; }
        public int Clips { get; set; }
        public int Audio { get; set; }
        public int Texto { get; set; }
        public int Varios { get; set; }
        public string Tipo{ get; set; }
        public string Procedimiento { get; set; }
        //public string Nota { get; set; }
        public Oficial OfEntrega { get; set; }
        public Oficial OfRecibe { get; set; }
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
            Texto = ListaArchivos.Count(a => a.Extension == ".pdf" || a.Extension == ".txt" || a.Extension == ".docx" || a.Extension == ".doc");
            Varios = ListaArchivos.Count(a => a.Extension != ".txt" && a.Extension != ".doc" && a.Extension != ".docx" && a.Extension != ".pdf"
            && a.Extension != ".png" && a.Extension != ".jpg" && a.Extension != ".jpeg" && a.Extension != ".gif" && a.Extension != ".bmp"
            && a.Extension != ".mp4" && a.Extension != ".avi" && a.Extension != ".wmv" && a.Extension != ".mov");
        }

    }
}