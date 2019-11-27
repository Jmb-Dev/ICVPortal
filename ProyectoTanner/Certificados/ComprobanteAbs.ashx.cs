using System.Web;

namespace ProyectoTanner.Certificados
{
    /// <summary>
    /// Descripción breve de ComprobanteAbs
    /// </summary>
    public class ComprobanteAbs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}