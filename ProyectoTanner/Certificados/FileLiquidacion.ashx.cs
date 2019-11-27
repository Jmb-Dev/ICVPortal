using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;
//using SAP.Middleware.Connector;

namespace ProyectoTanner.Controllers
{
    /// <summary>
    /// Descripción breve de FileLiquidacion
    /// </summary>
    public class FileLiquidacion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string Rut = context.Request.QueryString["Rut"];
                string Mes = context.Request.QueryString["Mes"];
                string Anio = context.Request.QueryString["Anio"];

                string usuario = context.Request.QueryString["Usuario"];
                string contrasena = context.Request.QueryString["Clave"];

                string retorno = "ERROR";

                ConexionController conexion = new ConexionController();
                try
                {
                    //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                    //    retorno = conexion.connectionsSAP(usuario, contrasena);

                    //if (string.IsNullOrEmpty(retorno))
                    //{
                    //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                    //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                    //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_LIQ_SUE");

                    //    BapiGetUser.SetValue("I_RUT", Rut);
                    //    BapiGetUser.SetValue("I_MES", Mes);
                    //    BapiGetUser.SetValue("I_ANO", Anio);


                    //    BapiGetUser.Invoke(SapRfcDestination);

                    //    Byte[] bytes = (Byte[])BapiGetUser.GetValue("PDF_LIQUI");

                    //    using (MemoryStream input = new MemoryStream(bytes))
                    //    {
                    //        using (MemoryStream output = new MemoryStream())
                    //        {
                    //            string password = Rut;
                    //            PdfReader reader = new PdfReader(input);
                    //            PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                    //            bytes = output.ToArray();
                    //        }
                    //    }

                    //obtener datos de api


                    context.Response.Buffer = true;
                    context.Response.Charset = "";
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    context.Response.ContentType = "application/pdf";
                    //context.Response.BinaryWrite(bytes);
                    context.Response.Flush();
                    context.Response.End();
                    //}


                }
                catch (Exception e)
                {

                    Document document = new Document(PageSize.LETTER, 50, 50, 30, 15);
                    MemoryStream output = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(document, output);
                    document.Open();
                    //document.NewPage();

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