using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Hash
{
    public class BEAeropuerto
    {

        public string Nombre { get; set; }
        public string Unidad { get; set; }
       

        static public List<BEAeropuerto> ObtenerAeropuertos()
        {

            List<BEAeropuerto> aeropuertos= new List<BEAeropuerto>() ;
            XDocument doc = XDocument.Load("datos.xml");

             aeropuertos = (from x in doc.Descendants("Aeropuerto")
                            select new BEAeropuerto()
                            {
                                Nombre = x.Element("Nombre").Value,
                                Unidad = x.Element("Unidad").Value
                            }).ToList();
            return aeropuertos;
        }

        public override string ToString()
        {
            return Unidad;
        }

    }
}
