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
        public BEJerarquia jerarquia { get; set; }
        public string Dependencia { get; set; }


        public BEOficial(int legaj, string nombrecompleto)
        {
            this.Legajo = legaj;
            this.NombreCompleto = nombrecompleto;
        }

        public BEOficial() { }


        static public List<BEOficial> ObtenerOficiales()
        {

            List<BEJerarquia> JerarquiaList = BEJerarquia.ObtenerJerarquias();
            List<BEOficial> OficialList = new List<BEOficial>();
            XDocument doc = XDocument.Load("datos.xml");

            var oficiales = from oficial in doc.Descendants("Oficial")
                            select new BEOficial()
                            {
                                Legajo = Convert.ToInt32(oficial.Element("Legajo").Value),
                                NombreCompleto = oficial.Element("Nombre").Value,
                                jerarquia = JerarquiaList.Find(x => x.jerarquia == oficial.Element("jerarquia").Value)
                            };
            OficialList = oficiales.ToList();
            return OficialList;
        }

        static public void AgregarOficial(BEOficial poficial)
        {
            XDocument doc = XDocument.Load("datos.xml");

            if (poficial.NombreCompleto != "" && poficial.Legajo > 0)
            {

                bool existeLegajo = doc.Descendants("Oficial").Any(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());

                if (!existeLegajo)
                {
                    XElement nuevoOficial = new XElement("Oficial",
                    new XElement("Legajo", poficial.Legajo),
                    new XElement("Nombre", poficial.NombreCompleto),
                    new XElement("jerarquia", poficial.jerarquia.jerarquia));

                    doc.Root.Element("Oficiales").Add(nuevoOficial);
                    doc.Save("datos.xml");
                }
                else
                {
                    XElement oficialExistente = doc.Descendants("Oficial").FirstOrDefault(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());
                    if (oficialExistente != null)
                    {
                        oficialExistente.Element("jerarquia").Value = poficial.jerarquia.jerarquia;
                        doc.Save("datos.xml");
                    }
                }
            }

        }
        public override string ToString()
        {
            return jerarquia.Abreviatura + " " + NombreCompleto;
        }
    }


}