using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTanner.Models
{
    public class PERSONALE
    {
        public string VORNA { get; set; }
        public string NACHN { get; set; }
        public string NACH2 { get; set; }
        public string GBDAT { get; set; }
        public string FECIN { get; set; }
        public string TEXT1 { get; set; }
        public string TEXT2 { get; set; }
        public string SALAR { get; set; }
        public string CTTXT { get; set; }
        public string JEFE { get; set; }
        public string CODIGO { get; set; }
        public string MENSAJE { get; set; }
    }

    public class RootObject
    {
        public List<PERSONALE> PERSONALES { get; set; }
    }
}

