using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    public class Oficial
    {

        public int Legajo { get; set; }
        public string NombreCompleto { get; set; }
        public Jerarquia Jerarquia { get; set; }


        public Oficial(int legaj, string nombrecompleto)
        {
            this.Legajo = legaj;
            this.NombreCompleto = nombrecompleto;
        }

        public override string ToString()
        {
            return Jerarquia.Abreviatura + " " + NombreCompleto;
        }
    }

}
