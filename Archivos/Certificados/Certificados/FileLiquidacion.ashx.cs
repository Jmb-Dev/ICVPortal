﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using SAP.Middleware.Connector;

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
                    if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                        retorno = conexion.connectionsSAP(usuario, contrasena);

                    if (string.IsNullOrEmpty(retorno))
                    {
                        RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                        RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                        IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_LIQ_SUE");

                        BapiGetUser.SetValue("I_RUT", Rut);
                        BapiGetUser.SetValue("I_MES", Mes);
                        BapiGetUser.SetValue("I_ANO", Anio);


                        BapiGetUser.Invoke(SapRfcDestination);

                        Byte[] bytes = (Byte[])BapiGetUser.GetValue("PDF_LIQUI");

                        using (MemoryStream input = new MemoryStream(bytes))
                        {
                            using (MemoryStream output = new MemoryStream())
                            {
                                string password = Rut;
                                PdfReader reader = new PdfReader(input);
                                PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                                bytes = output.ToArray();
                            }
                        }
                        context.Response.Buffer = true;
                        context.Response.Charset = "";
                        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        context.Response.ContentType = "application/pdf";
                        context.Response.BinaryWrite(bytes);
                        context.Response.Flush();
                        context.Response.End();
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