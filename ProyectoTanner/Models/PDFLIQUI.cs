using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTanner.Models
{

    public class PDFLIQUI
    {
        public string pdF_LIQUI { get; set; }
    }

    public class TBINARY
    {
        public string line { get; set; }
    }

    public class RO_PDFLIQUI
    {
        public List<PDFLIQUI> _PDF_LIQUI { get; set; }
        public List<TBINARY> _T_BINARY { get; set; }
        public List<object> _MENSAJES { get; set; }
    }
}