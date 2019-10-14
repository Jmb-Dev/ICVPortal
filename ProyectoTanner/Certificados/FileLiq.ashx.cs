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

            var result = json2.DownloadString(url);
            //string retorno3 = result.Remove(0, 1);
            //       retorno3 = retorno3.Remove(retorno3.Length - 1);

            var Convert = JsonConvert.DeserializeObject(result);

            try
            {

                JObject json = JObject.Parse(result);
                List<JToken> dataa = json.Children().ToList();

                foreach (JProperty item in dataa)
                {
                    item.CreateReader();
                    switch (item.Name)
                    {
                        case "LIQUIDACION":
                            foreach (JObject msg in item.Values())
                            {

                                foreach (JProperty child in msg.Children<JProperty>())
                                {
                                    item.CreateReader();
                                    switch (child.Name)
                                    {
                                        case "PDF_LIQUI":
                                            foreach (JObject msg2 in child.Values())
                                            {
                                                Byte[] bytes = (Byte[])System.Text.Encoding.UTF8.GetBytes("PDF_LIQUI"); /*(Byte[])msg2.GetValue("PDF_LIQUI");*/

                                                using (MemoryStream input = new MemoryStream(bytes))
                                                {
                                                   using (MemoryStream output = new MemoryStream())
                                                        {
                                                            string password = Rut;
                                                            PdfReader reader = new PdfReader(input);
                                                            PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                                                            bytes = output.ToArray();
                                                        }                                                }

                                                context.Response.Buffer = true;
                                                context.Response.Charset = "";
                                                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                                context.Response.ContentType = "application/pdf";
                                                //context.Response.BinaryWrite(bytes);
                                                context.Response.Flush();
                                                context.Response.End();

                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {

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
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(output.ToArray());
                context.Response.Flush();
                context.Response.End();
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