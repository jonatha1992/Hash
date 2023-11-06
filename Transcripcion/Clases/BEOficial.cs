using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace Hash
{
    public class BEOficial
    {
        public int Legajo { get; set; }
        public string NombreCompleto { get; set; }
        public BEJerarquia Jerarquia { get; set; }
        public BEDestino Destino { get; set; }


        public BEOficial(int legaj, string nombrecompleto)
        {
            this.Legajo = legaj;
            this.NombreCompleto = nombrecompleto;
        }

        public BEOficial() { }


        static public List<BEOficial> ObtenerOficiales()
        {

            List<BEJerarquia> JerarquiaList = BEJerarquia.ListarJeraquias();
            List<BEDestino> DestinoList = BEDestino.ListarDestinos();

            List<BEOficial> OficialList = new List<BEOficial>();
            XDocument doc = XDocument.Load("datos.xml");

            var oficiales = from oficial in doc.Descendants("Oficial")
                            select new BEOficial()
                            {
                                Legajo = Convert.ToInt32(oficial.Element("Legajo").Value),
                                NombreCompleto = oficial.Element("Nombre").Value,
                                Jerarquia = JerarquiaList.Find(x => x.Nombre == oficial.Element("JerarquiaId").Value),
                                Destino = DestinoList.Find(x => x.Nombre == oficial.Element("DestinoId").Value)
                            };
            OficialList = oficiales.ToList();
            return OficialList;
        }

        static public void AgregarOficial(BEOficial poficial)
        {

            XDocument doc = XDocument.Load("datos.xml");


            bool existeLegajo = doc.Descendants("Oficial").Any(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());

            if (!existeLegajo)
            {

                XElement nuevoOficial = new XElement("Oficial",
                new XElement("Legajo", poficial.Legajo),
                new XElement("Nombre", poficial.NombreCompleto),
                new XElement("JerarquiaId", poficial.Jerarquia.Nombre),
                new XElement("DestinoId", poficial.Destino.Nombre)
                );

                doc.Root.Element("Oficiales").Add(nuevoOficial);
                doc.Save("datos.xml");

            }
            else
            {
                XElement oficialExistente = doc.Descendants("Oficial").FirstOrDefault(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());
                if (oficialExistente != null)
                {
                    oficialExistente.Element("JerarquiaId").Value = poficial.Jerarquia.Nombre;
                    oficialExistente.Element("DestinoId").Value = poficial.Destino.Nombre;
                    doc.Save("datos.xml");
                }

            }
        }
        public override string ToString()
        {
            return Jerarquia.Abreviatura + " " + NombreCompleto;
        }
    }


}