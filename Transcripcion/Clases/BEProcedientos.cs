using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Hash
{
    public class BEProcedimiento
    {

        public string Nombre { get; set; }
       


        static public List<BEProcedimiento> ObtenerProcedimientos()
        {

            List<BEProcedimiento> procedimientos = new List<BEProcedimiento>() ;
            XDocument doc = XDocument.Load("datos.xml");

             procedimientos = (from x in doc.Descendants("Procedimientos")
                            select new BEProcedimiento()
                            {
                                Nombre = x.Element("Nombre").Value
                            }).ToList();
            return procedimientos;
        }

        //static public void AgregarOficial(BEOficial poficial)
        //{

        //    XDocument doc = XDocument.Load("datos.xml");



        //    bool existeLegajo = doc.Descendants("Oficial").Any(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());

        //    if (!existeLegajo)
        //    {

        //        XElement nuevoOficial = new XElement("Oficial",
        //        new XElement("Legajo", poficial.Legajo),
        //        new XElement("Nombre", poficial.NombreCompleto),
        //        new XElement("jerarquia", poficial.jerarquia.jerarquia));

        //        doc.Root.Element("Oficiales").Add(nuevoOficial);
        //        doc.Save("datos.xml");

        //    }
        //    else
        //    {
        //        XElement oficialExistente = doc.Descendants("Oficial").FirstOrDefault(o => (string)o.Element("Legajo") == poficial.Legajo.ToString());
        //        if (oficialExistente != null)
        //        {
        //            oficialExistente.Element("jerarquia").Value = poficial.jerarquia.jerarquia;
        //            doc.Save("datos.xml");
        //        }

        //    }
        //}





    }


}
