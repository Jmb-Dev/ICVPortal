using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using ProyectoTanner.Models;

namespace ProyectoTanner.Certificados
{
    /// <summary>
    /// Descripción breve de FileLiq
    /// </summary>
    public class FileLiq : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            ////funcionando
            //var Rut = "RUT=" + context.Request.QueryString["Rut"];
            //var Mes = "&MES=" + context.Request.QueryString["Mes"];
            //var Anio = "&PERIODO=" + context.Request.QueryString["Anio"];
            //string Token = "Bearer  " + context.Request.QueryString["Tok"];

            //para pruebas
            var Rut = "RUT=" + context.Request.QueryString["Rut"];
            var Mes = "&MES=07";
            var Anio = "&PERIODO=2016";
            string Token = "Bearer  " + context.Request.QueryString["Tok"];

            var url = "http://164.77.177.179:5055/api/ZHR_MF_LIQSUELD?" + Rut + Mes + Anio + "";

            var json2 = new WebClient();
            json2.Headers.Add("Authorization", Token);

            //var result = json2.DownloadData(url);
            
            dynamic result = json2.DownloadString(url);

            var m = JsonConvert.DeserializeObject(result);
            
            JObject json = JObject.Parse(result);
            List<JToken> dataa = json.Children().ToList();
            DataTable miDataTable = new DataTable();
            //String binario = x._PDF_LIQUI;
            
            var linea = "";
            byte[] bytes;


            foreach (JProperty item in dataa)
            {
                item.CreateReader();
                switch (item.Name)
                {
                    case "_PDF_LIQUI":
                        foreach (JObject msg in item.Values())
                        {
                            linea = (string)msg["pdF_LIQUI"];
                            bytes = (Byte[])(msg["pdF_LIQUI"]);

                         
                        }
                        break;
                    default:
                        break;
                }

            }


          


            //Byte[] bytes = Encoding.ASCII.GetBytes(linea);




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