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

             procedimientos = (from x in doc.Descendants("Procedimiento")
                            select new BEProcedimiento()
                            {
                                Nombre = x.Value
                            }).ToList();
            return procedimientos;
        }

        static public void AgregarProcedimiento(BEProcedimiento procedimiento)
        {

            XDocument doc = XDocument.Load("datos.xml");


            bool existeProcedimiento = doc.Descendants("Procedimientos").Any(o => (string)o.Element("Procedimiento").Value == procedimiento.Nombre);

            if (!existeProcedimiento)
            {

                XElement nuevoProcedimiento = new XElement("Procedimiento", procedimiento.Nombre);

                doc.Root.Element("Procedmientos").Add(nuevoProcedimiento);
                doc.Save("datos.xml");

            }
        }

    }
}
