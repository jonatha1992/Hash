using System;
using System.Collections.Generic;
using System.Linq;

namespace Hash
{
    public class Formulario_Custodia
    {
        public int Nro_Hash { get; set; }
        public string Tipo { get; set; }
        public string Cartula{ get; set; }
        public string Juzgado{ get; set; }
        public string Procedimiento { get; set; }
        public string Sumario { get; set; }
        public string Secretaria { get; set; }
        public string Lugar_Recoleccion { get; set; }
        public string Utilidad { get; set; }
        public string Identificacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime Hora { get; set; }
        public BEOficial OfEntrega { get; set; }
        public BEOficial OfRecibe { get; set; }

    }
}
