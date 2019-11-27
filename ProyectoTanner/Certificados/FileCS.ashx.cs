using System;
using System.IO;
using System.Web;

namespace ProyectoTanner.Certificados
{
    /// <summary>
    /// Descripción breve de FileCS
    /// </summary>
    public class FileCS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string file = context.Request.QueryString["Id"];

                string filename = file;
                byte[] bytes = File.ReadAllBytes(filename);

                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {

            }
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