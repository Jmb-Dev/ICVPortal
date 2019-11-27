using Newtonsoft.Json.Linq;
using ProyectoTanner.Controllers;
using ProyectoTanner.Models;
using System;
using System.Linq;
using System.Net;
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
            ////var rut = context.Request.QueryString["Rut"];
            ////var rem = context.Request.QueryString["Rem"];
            ////var mot = "XXXXXX";
            string Token = "Bearer  " + context.Request.QueryString["Tok"];
            var id = context.Request.QueryString["ID"];
            ConexionDBA db = new ConexionDBA();
            db.Conexion();
            MdDetCertificado obj = new MdDetCertificado();
            obj = db.GetCertificado(id);

            string tipo = "";
            if ((string)obj.tipo_certificado == "1")
            {
                tipo = "";
            }
            else
            {
                tipo = "X";
            }
            string motivo = null;
            switch ((string)obj.tipo_motivo)
            {
                case "1":
                    motivo = "CCAA los Andes";
                    break;
                case "2":
                    motivo = "Instituciones Bancarias";
                    break;
                case "3":
                    motivo = "Cooperativas";
                    break;


            }

            var url = "http://164.77.177.179:5055/api/ZHR_MF_REN_ANT?RUT=" + obj.rut + "&REM=" + tipo + "&MOT=" + motivo + "";

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