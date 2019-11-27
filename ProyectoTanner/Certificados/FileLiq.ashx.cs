using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

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
            var Rut = "RUT=" + context.Request.QueryString["Rut"];
            var Mes = "&MES=" + context.Request.QueryString["Mes"];
            var Anio = "&PERIODO=" + context.Request.QueryString["Anio"];
            //string Token = "Bearer  " + context.Request.QueryString["Tok"];

            //para pruebas
            //var Rut = "RUT=" + context.Request.QueryString["Rut"];
            //var Mes = "&MES=07";
            //var Anio = "&PERIODO=2016";
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

            try
            {


                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                //context.Response.End();

            }
            catch (Exception e)
            {


                context.Response.Buffer = true;
                //context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();


                Document document = new Document(PageSize.LETTER, 50, 50, 30, 15);
                MemoryStream output = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                document.Open();

                PdfContentByte cb = writer.DirectContent;
                cb.Rectangle(50f, 450f, 500f, 350f);
                Paragraph titulo = new Paragraph(e.Message);
                titulo.IndentationLeft = 130;
                document.Add(titulo);


                document.Close();


                context.Response.Buffer = true;
                //context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(output.ToArray());
                context.Response.Flush();
                //context.Response.End();
            }
            finally
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