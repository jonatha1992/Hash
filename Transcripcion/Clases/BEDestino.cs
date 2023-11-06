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

            var Destinos = from oficial in doc.Descendants("Destino")
                            select new BEDestino()
                            {
                                Nombre = oficial.Element("Nombre").Value,
                            };
            if (Destinos == null)
            {
                return null;
            }

            return Destinos.ToList();
        }

        //Todo: Agregar detino a xml
        static public void AgregaDestino(BEDestino Destino)
        {

            XDocument doc = XDocument.Load("datos.xml");

            bool existeDestino = doc.Descendants("Destino").Any(o => (string)o.Element("Destino") == Destino.Nombre.ToString());

            if (!existeDestino)
            {

                XElement nuevoDestino = new XElement("Destino",
                new XElement("Nombre", Destino.Nombre));
                doc.Root.Element("Destinos").Add(nuevoDestino);
                doc.Save("datos.xml");

            }
            else
            {
                XElement DestinoExistente = doc.Descendants("Destino").FirstOrDefault(o => (string)o.Element("Destino") == Destino.Nombre.ToString());
                if (DestinoExistente != null)
                {
                    DestinoExistente.Element("Nombre").Value = Destino.Nombre;
                    doc.Save("datos.xml");
                }

            }
        }
        public override string ToString()
        {
            return Nombre;
        }
    }


}