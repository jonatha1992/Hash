using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    public class Jerarquia
    {
        public string Abreviatura { get; set; }
        public string jerarquia { get; set; }


        public Jerarquia(string jerar, string Abre)
        {
            jerarquia = jerar;
            Abreviatura = Abre;
        }
        public override string ToString()
        {
            return jerarquia;
        }
    }

}
