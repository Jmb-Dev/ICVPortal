using System.Collections.Generic;

namespace ProyectoTanner.Models
{
    public class Contingentes
    {
        public string ANZHL { get; set; }
        public string KVERB { get; set; }
        public string DISPO { get; set; }
        public string KTART { get; set; }
        public string KTEXT
        {
            get; set;
        }
    }
    public class _CONTINGENTE
    {
        public string anzhl { get; set; }
        public string kverb { get; set; }
        public string dispo { get; set; }
        public string ktart { get; set; }
        public string ktext
        {
            get; set;
        }
    }
    public class _VACACIONES
    {
        public string coD_TIP { get; set; }
        public string deS_TIP { get; set; }
    }

    public class RootObjectC
    {
        public List<_CONTINGENTE> _CONTINGENTE { get; set; }
    }

    public class RootObjectV
    {
        public List<_VACACIONES> _VACACIONES { get; set; }
    }
}