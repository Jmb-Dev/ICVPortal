//using SAP.Middleware.Connector;
using ProyectoTanner.Controllers;
using System;
using System.Web;

namespace ProyectoTanner.Certificados
{
    /// <summary>
    /// Descripción breve de Certificados
    /// </summary>
    public class Certificados : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string Rut = context.Request.QueryString["Rut"];
                string Tipo = context.Request.QueryString["Tipo"];

                string usuario = context.Request.QueryString["Usuario"];
                string contrasena = context.Request.QueryString["Clave"];

                string retorno = "ERROR";

                ConexionController conexion = new ConexionController();

                try
                {
                    //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                    //    retorno = conexion.connectionsSAP(usuario, contrasena);

                    if (string.IsNullOrEmpty(retorno))
                    {


                        //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                        //RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                        //IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_REN_ANT");

                        //BapiGetUser.SetValue("I_RUT", Rut);
                        //BapiGetUser.SetValue("I_SUELDO", Tipo);

                        //BapiGetUser.Invoke(SapRfcDestination);

                        //Byte[] bytes = (Byte[])BapiGetUser.GetValue("E_BINARY");

                        //using (MemoryStream input = new MemoryStream(bytes))
                        //{
                        //    using (MemoryStream output = new MemoryStream())
                        //    {
                        //        //string password = Rut;
                        //        PdfReader reader = new PdfReader(input);
                        //        PdfEncryptor.Encrypt(reader, output, false, "","", PdfWriter.ALLOW_SCREENREADERS);
                        //        bytes = output.ToArray();
                        //    }
                        //}
                        //context.Response.Buffer = true;
                        //context.Response.Charset = "";
                        //context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //context.Response.ContentType = "application/pdf";
                        //context.Response.BinaryWrite(bytes);
                        //context.Response.Flush();
                        //context.Response.End();
                    }
                }
                catch (Exception e)
                {

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