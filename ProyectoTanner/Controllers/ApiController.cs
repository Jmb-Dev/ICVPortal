using Newtonsoft.Json.Linq;
using ProyectoTanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ProyectoTanner.Controllers
{
    public class ApiController
    {
        public List<MdObtenerColab> ListColab = new List<MdObtenerColab>();
        public string insertAbs(string idSolicitud, string token)
        {
            List<MdDetAbs> lista = new List<MdDetAbs>();
            try
            {
                ConexionDBA db = new ConexionDBA();
                db.Conexion();

                lista = db.GetDetAbsentismos(idSolicitud);
                string url = "http://164.77.177.179:5055/api/ZHR_MF_ING_ABS?";
                foreach (var item in lista)
                {
                    string url2 = url;


                    DateTime FechaR = DateTime.Parse(item.fecha_reg);
                    string FechaReg = FechaR.ToString("yyyyMMdd");
                    DateTime FechaI = DateTime.Parse(item.fecha_ini);
                    string FechaIni = FechaI.ToString("yyyyMMdd");
                    DateTime FechaF = DateTime.Parse(item.fecha_fin);
                    string FechaFin = FechaF.ToString("yyyyMMdd");

                    url2 += "ICNUM1=" + item.rut;
                    url2 += "&AEDTM=" + FechaReg;
                    url2 += "&AWART=0" + item.id_tipo_abs;
                    url2 += "&BEGDA=" + FechaIni;
                    url2 += "&ENDDA=" + FechaFin;
                    url2 += "&SPRPS=" + "" + "";


                    var json2 = new WebClient();
                    json2.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    json2.Headers[HttpRequestHeader.ContentType] = "application/json";
                    json2.Headers.Add("Authorization", token);


                    var result = json2.DownloadString(url2);


                    JObject json = JObject.Parse(result);
                    string codigo = null;

                    for (int i = 0; i < json["T_MENSAJE"][0].Count(); i++)
                    {
                        codigo = ((string)json["T_MENSAJE"][i].SelectToken("CODIGO")).Replace("FIELD CODIGO=", "");

                        break;
                    }

                    if (codigo == "001")
                    {
                        //modificar wf 
                    }


                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        public void insertCertificado(string idSolicitud, string token)
        {
            ConexionDBA db = new ConexionDBA();
            List<MdDetCertificado> lt = new List<MdDetCertificado>();
            db.Conexion();
            lt = db.GetDetCertificados(idSolicitud);
            db.ConexionClose();
            string rut = lt.First().rut;
            string rem = "";
            if (lt.First().tipo_certificado.ToString() == "2")
            {
                rem = "X";
            }
            string motivo = lt.First().tipo_motivo;

            string url = "http://164.77.177.179:5055/api/ZHR_MF_REN_ANT?RUT=" + rut + "&REM=" + rem + "&MOT=" + motivo + "";

            var json2 = new WebClient();
            json2.Headers.Add("Authorization", token);
            var result = json2.DownloadString(url);

            JObject json = JObject.Parse(result);

            Byte[] bytes2 = null;

            for (int i = 0; i < json["RENTA"][0]["E_PDF"].Count(); i++)
            {
                bytes2 = (Byte[])json["RENTA"][0]["E_PDF"][i].SelectToken("E_PDF");
            }
            try
            {

                db.Conexion();
                db.ActualizaDetCert(idSolicitud, bytes2);
                db.ConexionClose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EnvioMail(string token, string mensaje, string asunto, string para)
        {
            string url = "http://164.77.177.179:5055/api/ZHR_ENV_MAIL?";
            string subject = "pSUBJECT=" + asunto;
            string body = "&pBODY=" + mensaje;
            string type_doc = "&pTYPE_DOC=";
            string sender = "&pSENDER=PORTAL@ICV.CL";
            string receivers = "&pRECEIVERS=" + para;

            url += subject + body + type_doc + sender + receivers + "";
            string token2 = "Bearer  " + token;
            var json2 = new WebClient();
            json2.Headers.Add("Authorization", token2);
            var result = json2.DownloadString(url);

        }

        public string solicitarToken()
        {
            string url = "http://164.77.177.179:5055/api/login/GetToken?Username=QHRPORTAL&Password=1234";
            string apikey = new WebClient().DownloadString(url);
            return apikey;
        }


        public List<MdObtenerColab> obtenerColaboradores(string unidad, string token)
        {
            try
            {
                string url = "http://164.77.177.179:5055/api/ZHR_MF_GR_EMP?UNIDAD=";
                url += unidad;
                url += "";
                string token2 = "Bearer  " + token;
                var json2 = new WebClient();
                json2.Headers.Add("Authorization", token2);
                var result = json2.DownloadString(url);

                string retorno3 = result.Remove(0, 1);
                retorno3 = retorno3.Remove(retorno3.Length - 1);

                JObject json = JObject.Parse(retorno3);
                List<MdObtenerColab> listColab = new List<MdObtenerColab>();
                for (int i = 0; i < json["e_TB_GR_EMP"][0].Count(); i++)
                {
                    MdObtenerColab md = new MdObtenerColab();
                    md.pernr = (string)json["e_TB_GR_EMP"][i].SelectToken("pernr");
                    md.ename = (string)json["e_TB_GR_EMP"][i].SelectToken("ename");
                    md.stell = (string)json["e_TB_GR_EMP"][i].SelectToken("stell");
                    md.kostl = (string)json["e_TB_GR_EMP"][i].SelectToken("kostl");
                    md.orgeh = (string)json["e_TB_GR_EMP"][i].SelectToken("orgeh");
                    md.begda = (string)json["e_TB_GR_EMP"][i].SelectToken("begda");
                    md.bet01 = (string)json["e_TB_GR_EMP"][i].SelectToken("bet01");
                    md.waers = (string)json["e_TB_GR_EMP"][i].SelectToken("waers");

                    ListColab.Add(md);
                }
                return listColab;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


        public ListaDespegable ValidarCertificado(string rut, string monto, string token)
        {
            try
            {
                DateTime localDate = DateTime.Now;
                

                string fecha_in = localDate.ToString("yyyyMMdd");
                string fecha_fn = fecha_in;
 
                string url = "http://164.77.177.179:5055/api/ZHR_MF_VALCERT?";
                url += "I_RUT=" + rut;
                url += "&I_BEGDA=" + fecha_in;
                url += "&I_ENDDA=" + fecha_fn;
                url += "&I_ENDDA=" + fecha_fn;
                url += "&I_CUOTA=" + monto + "";

                string token2 = "Bearer  " + token;
                var json2 = new WebClient();
                json2.Headers.Add("Authorization", token2);
                var result = json2.DownloadString(url);

                JObject json = JObject.Parse(result);


                ListaDespegable lt = new ListaDespegable();

                for (int i = 0; i < json["VALCERT"].Count(); i++)
                {
                    lt.COD = (string)json["VALCERT"][i].SelectToken("E_CODE");
                    lt.TEXT = (string)json["VALCERT"][i].SelectToken("E_MESSAGE");
                    break;
                }
              
                return lt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

    }
}
