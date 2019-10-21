﻿using System;
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


            dynamic result = json2.DownloadString(url);
            JObject json = JObject.Parse(result);
            byte[] bytes = null;

            for (int i = 0; i < json["LIQUIDACION"][0]["PDF_LIQUI"].Count(); i++)
            {
                bytes = (Byte[])json["LIQUIDACION"][0]["PDF_LIQUI"][i].SelectToken("PDF_LIQUI");
            }

            context.Response.Buffer = true;
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(bytes);
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