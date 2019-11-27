namespace ProyectoTanner.Models
{
    public class MdDetCertificado
    {
        public string id_detalle { get; set; }
        public string id_solicitud { get; set; }
        public string tipo_certificado { get; set; }
        public string tipo_motivo { get; set; }
        public string valor_credito { get; set; }
        public string valor_cuota { get; set; }
        public string cantidad_cuota { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public string certificado { get; set; }
        public string comentario { get; set; }

    }
}