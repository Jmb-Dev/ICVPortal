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
            var rut = context.Request.QueryString["Rut"];
            var rem = "";
            var mot = "XXXXXX";
            string Token = "Bearer  " + context.Request.QueryString["Tok"];

            var url = "http://164.77.177.179:5055/api/ZHR_MF_REN_ANT?RUT=" + rut + "&REM=" + rem + "&MOT=" + mot + "";

            var json2 = new WebClient();
            json2.Headers.Add("Authorization", Token);
            var result = json2.DownloadString(url);

            JObject json = JObject.Parse(result);

            Byte[] bytes2 = null;

            for (int i = 0; i < json["RENTA"][0]["E_PDF"].Count(); i++)
            {
                bytes2 = (Byte[])json["RENTA"][0]["E_PDF"][i].SelectToken("E_PDF");
            }

            context.Response.Buffer = true;
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(bytes2);
            context.Response.Flush();

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