﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Hash
{
    public class BEJerarquia : ICloneable
    {
        public string jerarquia { get; set; }
        public string Abreviatura { get; set; }

        public BEJerarquia() { }
        public BEJerarquia(string jerar, string Abre)
        {
            jerarquia = jerar;
            Abreviatura = Abre;
        }

        public static List<BEJerarquia> ObtenerJerarquias()
        {
            try
            {


                XDocument doc = XDocument.Load("datos.xml");

                var jerarquias = from jerar in doc.Descendants("Jerarquia")
                                 select new BEJerarquia
                                 {
                                     jerarquia = jerar.Element("jerarquia").Value,
                                     Abreviatura = jerar.Element("Abreviatura").Value
                                 };
                return jerarquias.ToList();
            }
            catch (Exception )
            {

                throw new Exception("Error al obtener la base");
            }
        }
        public override string ToString()
        {
            return jerarquia;
        }

        public object Clone()
        {
            return new BEJerarquia
            {
                jerarquia = this.jerarquia,
                Abreviatura = this.Abreviatura
            };
        }
    }

}
