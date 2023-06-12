using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Hash
{
    public class BEDependencia
    {

        public string Nombre { get; set; }


        public BEDependencia() { }
        public BEDependencia(string nombre)
        {
            this.Nombre = nombre;
        }
        static public List<BEDependencia> ObtenerDepencias()
        {

            List<BEDependencia> dependencias = new List<BEDependencia>();
            XDocument doc = XDocument.Load("datos.xml");

            dependencias = (from x in doc.Descendants("Dependencia")
                            select new BEDependencia()
                            {
                                Nombre = x.Value,
                            }).ToList();
            return dependencias;
        }

        static public void AgregarDependencia(string NDependencia)
        {
            NDependencia = NDependencia.Trim();
            XDocument doc = XDocument.Load("datos.xml");

            if (NDependencia.Length > 0 && NDependencia != "")
            {
                bool existeDependencia = doc.Descendants("Dependencia").Any(o => o.Value == NDependencia);

                if (!existeDependencia)
                {
                    XElement nuevoDependencia = new XElement("Dependencia", NDependencia);

                    doc.Root.Element("Dependencias").Add(nuevoDependencia);
                    doc.Save("datos.xml");
                }
            }
        } 

        public override string ToString()
        {
            return Nombre;
        }

        public object Clone()
        {
            return new BEDependencia
            {
                Nombre = this.Nombre
            };
        }
    }
}
