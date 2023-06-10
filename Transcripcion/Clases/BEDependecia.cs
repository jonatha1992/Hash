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

            XDocument doc = XDocument.Load("datos.xml");


            bool existeDependencia = doc.Descendants("Dependencias").Any(o => (string)o.Element("Dependencia").Value == NDependencia);

            if (!existeDependencia)
            {

                XElement nuevoDependencia = new XElement("Dependencia", NDependencia);

                doc.Root.Element("Dependencias").Add(nuevoDependencia);
                doc.Save("datos.xml");
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
