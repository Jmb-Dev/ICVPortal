using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTanner.Models
{
    public class ColaboradresJerarquia
    {
        public string ICNUM { get; set; }
        public string VORNA { get; set; }
        public string NACHN { get; set; }
        public string NACH2 { get; set; }
        public string ZTEXTUO { get; set; }
        public string ZTEXTPLANS { get; set; }


        public List<ColaboradresJerarquia> Colaboradores { get; set; }
    }
}