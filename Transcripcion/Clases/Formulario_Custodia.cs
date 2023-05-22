using System;
using System.Collections.Generic;
using System.Linq;

namespace Hash
{
    public class Formulario_Custodia
    {
        public int Nro_Hash { get; set; }
        public string Tipo { get; set; }
        public string Procedimiento { get; set; }
        public DateTime Hora { get; set; }
        public BEOficial OfEntrega { get; set; }
        public BEOficial OfRecibe { get; set; }
       
        //public List<BEArchivo> ListaArchivos { get; set; }


    }
}
