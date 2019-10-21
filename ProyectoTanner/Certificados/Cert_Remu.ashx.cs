using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTanner.Certificados
{
    /// <summary>
    /// Descripción breve de Cert_Remu
    /// </summary>
    public class Cert_Remu : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hola a todos");
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