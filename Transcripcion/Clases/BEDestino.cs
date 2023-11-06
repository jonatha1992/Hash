using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace Hash
{
    public class BEDestino
    {
        public string Nombre { get; set; }

        

        public BEDestino() { }


        static public List<BEDestino> ListarDestinos()
        {

            XDocument doc = XDocument.Load("datos.xml");

            var oficiales = from oficial in doc.Descendants("Destinos")
                            select new BEDestino()
                            {
                                Nombre = oficial.Element("Nombre").Value,
                            };
            return oficiales.ToList();
        }

        //Todo: Agregar detino a xml
        static public void AgregaDestino(BEDestino Destino)
        {

            XDocument doc = XDocument.Load("datos.xml");

            //bool existeLegajo = doc.Descendants("Oficial").Any(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());

            //if (!existeLegajo)
            //{

            //    XElement nuevoOficial = new XElement("Oficial",
            //    new XElement("Legajo", poficial.Legajo),
            //    new XElement("Nombre", poficial.NombreCompleto),
            //    new XElement("Nombre", poficial.Nombre.Nombre));

            //    doc.Root.Element("Oficiales").Add(nuevoOficial);
            //    doc.Save("datos.xml");

            //}
            //else
            //{
            //    XElement oficialExistente = doc.Descendants("Oficial").FirstOrDefault(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());
            //    if (oficialExistente != null)
            //    {
            //        oficialExistente.Element("Nombre").Value = poficial.Nombre.Nombre;
            //        doc.Save("datos.xml");
            //    }

            //}
        }
        public override string ToString()
        {
            return Nombre;
        }
    }


}