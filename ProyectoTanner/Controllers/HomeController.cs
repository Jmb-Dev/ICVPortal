using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProyectoTanner.Models;
using ProyectoTanner.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProyectoTanner.Controllers
{
    public class HomeController : Controller
    {
        string fecUltSoli;
        DateTime fec_1;
        DateTime fec_2;
        string[] results;

        //Urls 
        string UrlSolicitud = "http://164.77.177.179:5055/API/ZHR_GET_CONT?RUT=";

        public ActionResult Login(string usuario, string password)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario)
        {
            return View();
        }
        public ActionResult LogOut()
        {
            try
            {
                Session["SessionActiva"] = "";
                Session["NombreUsuario"] = "";
                Session.Abandon();

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Login", "LOGIN", new { });
        }
        public ActionResult Index()
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }
                string NombreUsuario = (string)(Session["NombreUsuarioNombreUsuario"]);
                string Usuario = (string)(Session["Usuario"]);

                ApiController api = new ApiController();

                //api.obtenerColaboradores(Session["UnidadOrg"].ToString(), Session["Token"].ToString());
            }
            catch (Exception ex) { }
            return View();
        }
        public ActionResult MisSolicitudes(string Mensaje)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string NombreUsuario = (string)(Session["NombreUsuario"]);
                string Usuario = (string)(Session["usuario"]);
                var VacacionesService = new VacacionesService();
                ConexionDBA db = new ConexionDBA();
                db.Conexion();
                //var ListSol = new List<Sol_Vacaciones>();

                var listado = new List<MdSolicitud>();

                //var unid = (string)Session["UnidadOrg"];
                var UserWeb = (string)Session["UserWeb"];
                //var Jefe = "";
                //var tipo = "2";

                //ListSol = db.Get_vacaciones(unid, Jefe, tipo, UserWeb);
                listado = db.VerSolicitudes(UserWeb);
                ViewBag.ObtenerVacHistorico = listado;

                if (Mensaje != null)
                {
                    ViewBag.Mostrar = true;
                    ViewBag.Mensaje = Mensaje;
                }
            }
            catch (Exception ex)
            {
                throw null;
            }

            return View();
        }
        [HttpGet]
        public ActionResult JefeAprobar(string idSolicitud, string idTipo)
        {
            string msn = null;
            string idAprobador = (String)Session["Pernr"];
            try
            {
                ConexionDBA db = new ConexionDBA();
                db.ConexionClose();
                db.Conexion();
                msn = db.JefeAprSoli(idSolicitud, idAprobador);
                db.ConexionClose();
                if (msn == "wf finalizado exitos")
                {
                    string Apikey = "Bearer  " + (string)(Session["Token"]);
                    ApiController ap = new ApiController();
                    switch (idTipo)
                    {
                        case "1":
                            msn = ap.insertAbs(idSolicitud, Apikey);
                            break;
                        case "2":
                            ap.insertCertificado(idSolicitud, Apikey);
                            break;
                        case "3":
                            break;
                    }
                }
                else
                {
                    db.Conexion();
                    string correo = db.Obtener_Mail_sq(idSolicitud);
                    db.ConexionClose();

                    if (correo != null)
                    {
                        string asunto = "";
                        string mensaje = "";
                        switch (idTipo)
                        {
                            case "1":
                                asunto = "Solicitud de Vacaciones";
                                break;
                            case "2":
                                asunto = "Solicitud de Certificado";
                                break;
                            case "3":
                                break;
                        }

                        mensaje = "<p>Usted tiene una solicitud pendiente de aprobación";
                        mensaje += " , con el ID: ";
                        mensaje += idSolicitud;
                        mensaje += ". </p>";
                        string token = Session["Token"].ToString();

                        ApiController api = new ApiController();
                        api.EnvioMail(token, mensaje, asunto, correo);

                    }
                }

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Content(msn);
                //return msn;
            }
            catch (Exception e)
            {
                msn = e.Message.ToString();
                throw;
            }
        }

        [HttpGet]
        public ActionResult JefeRechazar(string idSolicitud, string idTipo, string Nombre)
        {

            ViewBag.id = idSolicitud;
            ViewBag.apr = (String)Session["Pernr"];
            ViewBag.Nombre = Nombre;
            ViewBag.tipo = idTipo;
            return PartialView("VerRechazo");
        }



        public ActionResult RechazarSolicitudJefe(string idSolicitud, string Texto, string Apr, string idTipo)
        {
            string id = idSolicitud;
            try
            {
                ConexionDBA db = new ConexionDBA();
                db.Conexion();
                string msn = db.JefeRechSoli(idSolicitud, Apr, idTipo, Texto);
                db.ConexionClose();
                //return Json(msn);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Content(msn);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult TipoSolicitud(FormCollection fomr)
        {
            string idSolicitud = "40";
            ConexionDBA db = new ConexionDBA();
            db.Conexion();
            var lista = new JsonResult();
            try
            {

                var tipo = db.get_tipo_solicitud(idSolicitud);
                db.ConexionClose();
                switch (tipo)
                {
                    case "1":
                        lista = detAbs(idSolicitud);
                        break;
                    case "2":
                        lista = detCert(idSolicitud);
                        ViewBag.Certificado = "../Certificados/Cert_Remu.ashx?ID=" + idSolicitud
                                                                                   + "&Tok=" + Session["Token"].ToString()
                                                                                   + "";
                        //ViewBag.liquidacion = "../Certificados/FileLiq.ashx";

                        break;

                }

                return Json(lista);

            }
            catch (Exception ex)
            {
                return null;
            }
            return View();
        }
        [HttpPost]
        public JsonResult detCert(string idSolicitud)
        {
            List<MdDetCertificado> lista = new List<MdDetCertificado>();
            try
            {
                ConexionDBA db = new ConexionDBA();
                db.Conexion();

                lista = db.GetDetCertificados(idSolicitud);

                return Json(lista);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpPost]
        public JsonResult detAbs(string idSolicitud)
        {
            List<MdDetAbs> lista = new List<MdDetAbs>();
            try
            {
                ConexionDBA db = new ConexionDBA();
                db.Conexion();

                lista = db.GetDetAbsentismos(idSolicitud);
                return Json(lista);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        public ActionResult Solicitud()
        {
            if (verificaSesion() == true)
            {
                return RedirectToAction("Login", "LOGIN", new { });
            }


            string NombreUsuario = (string)(Session["NombreUsuario"]);
            string Usuario = (string)(Session["usuario"]);
            string Apikey = (string)(Session["Token"]);

            var rut = Usuario;
            var url_2 = UrlSolicitud + rut;
            string key2 = "Bearer " + Apikey;

            var json2 = new WebClient();
            json2.Headers.Add("Authorization", key2);
            var Retorno2 = json2.DownloadString(url_2);


            string retorno3 = Retorno2.Remove(0, 1);
            retorno3 = retorno3.Remove(retorno3.Length - 1);

            var Convert = JsonConvert.DeserializeObject(retorno3);

            JObject json = JObject.Parse(retorno3);
            List<JToken> dataa = json.Children().ToList();

            List<_CONTINGENTE> lista = new List<_CONTINGENTE>();
            List<_VACACIONES> ListaVaca = new List<_VACACIONES>();

            _CONTINGENTE table = new _CONTINGENTE();
            _VACACIONES tableTipVac = new _VACACIONES();

            foreach (JProperty item in dataa)
            {
                item.CreateReader();
                switch (item.Name)
                {
                    case "_CONTINGENTE":
                        foreach (JObject msg in item.Values())
                        {
                            table.anzhl = (string)msg["anzhl"];
                            table.kverb = (string)msg["kverb"];
                            table.dispo = (string)msg["dispo"];
                            table.ktart = (string)msg["ktart"];
                            table.ktext = (string)msg["ktext"];
                            lista.Add(table);
                        }
                        break;
                    case "_VACACIONES":
                        foreach (JObject msg2 in item.Values())
                        {
                            tableTipVac.coD_TIP = (string)msg2["coD_TIP"];
                            tableTipVac.deS_TIP = (string)msg2["deS_TIP"];
                            ListaVaca.Add(tableTipVac);
                        }
                        break;

                    default:

                        break;
                }
            }


            ViewBag.Milistado = lista;
            ViewBag.Milistado3 = ListaVaca;
            ViewBag.ocultar = true;
            ViewBag.ocultar2 = false;

            return View();
        }
        [HttpGet]
        public ActionResult SolicitudMiEquipo2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Solicitud(string FecDesde, string FecHasta, string TipoVaca, FormCollection fomr)
        {

            if (verificaSesion() == true)
            {
                return RedirectToAction("Login", "LOGIN", new { });
            }

            DateTime fechHoy = DateTime.Now;

            string fechadia = Convert.ToString(fechHoy);

            string Value = Request.Form["DropSoli"];
            string Usuario = (string)(Session["usuario"]);
            string fech_d = string.Format("{0:dd/MM/yyyy}", fechHoy.ToShortDateString());


            if (Value == "0111")
            {
                ConexionDBA db = new ConexionDBA();
                db.Conexion();
                string usuario = (string)Session["UserWeb"].ToString();

                string msn = db.Solicitud_pendientes((string)Session["UnidadOrg"], "1", (string)Session["RUT"]);
                db.ConexionClose();

                if (msn == "0")
                {


                    string unidadOrg = (string)Session["UnidadOrg"];
                    string rut = (string)(Session["usuario"]);
                    string nombre = (string)(Session["NombreUsuario"]);
                    string usuarioWeb = (string)Session["UserWeb"];

                    DateTime fechaActual = DateTime.Now;
                    string fechaHoy = string.Format("{0:dd/MM/yyyy}", fechaActual.ToShortDateString());

                    
                    db.Conexion();
                    List<MdAprobador> ListAprob = new List<MdAprobador>();
                    if (Session["UsuarioAprobador"].ToString() == "JEFE")
                    {
                        string pernr = Session["Pernr"].ToString();
                        ListAprob = db.ObtenerAprobadoresJefatura(pernr);
                    }
                    else
                    {
                        ListAprob = db.ObtenerAprobadores(unidadOrg);
                    }
                    db.ConexionClose();

                    if (ListAprob.Count() > 0)
                    {
                        db.Conexion();
                        string id_solicitud = db.insert_solicitud(unidadOrg, usuarioWeb, fechaHoy, "01");
                        db.ConexionClose();

                        db.Conexion();
                        string id_det_abs = db.Insert_det_abs(id_solicitud, Value, rut, nombre, FecDesde, FecHasta);
                        db.ConexionClose();

                        foreach (var item in ListAprob)
                        {
                            db.Conexion();
                            db.insert_estado_aprobacion(id_det_abs, id_solicitud, Value, item.aprobador, item.sequencia);
                            db.ConexionClose();

                            if (item.sequencia == "1")
                            {
                                db.Conexion();
                                string correo = db.Obtener_Mail(item.aprobador);
                                string asunto = "Solicitud de Vacaciones";
                                string mensaje = "<p>se ha enviado una solicitud de vacaciones para el colaborador ";
                                mensaje += nombre;
                                mensaje += " , con el ID: ";
                                mensaje += id_solicitud;
                                mensaje += ". </p>";
                                string token = Session["Token"].ToString();

                                ApiController api = new ApiController();
                                api.EnvioMail(token, mensaje, asunto, correo);
                            }
                        }
                    }

                    msn = "Solicitud ingresada";
                    ViewBag.Mensaje = msn;

                }
                else
                {
                    ViewBag.Mensaje = msn;
                }
                ActionResult actionResult = Solicitud();
                ViewBag.Mostrar = true;
                ViewBag.ocultar = true;
                ViewBag.ocultar2 = false;

            }

            return View("Solicitud");
        }
        [HttpGet]
        public ActionResult MiEquipo()
        {
            //pasa por aqui primero
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string NombreUsuario = (string)(Session["NombreUsuario"]);
                string Usuario = (string)(Session["usuario"]);

                ConexionDBA db = new ConexionDBA();
                db.Conexion();

                var listado = new List<MdSolicitud>();
                var Pernr = (string)Session["Pernr"];

                listado = db.VerSolPendJefe(Pernr);
                ViewBag.MilistadoAProb = listado;
            }

            catch (Exception ex) { }
            return View();
        }
        [HttpPost]
        public ActionResult MiEquipo(FormCollection collection)
        {
            try { }

            catch (Exception ex) { }

            return View();
        }
        [HttpGet]
        public ActionResult Anular(string Docnr)
        {
            return RedirectToAction("MisSolicitudes", "HOME", new { Mensaje = ViewBag.Mensaje });
        }
        public ActionResult VerLiquidacion()
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string NombreUsuario = (string)(Session["NombreUsuario"]);
                string Usuario = (string)(Session["usuario"]);
                var InformColaborador = new InformacionColaborador();
                string meses_atras = "12";

                List<string> lista = new List<string>();
                lista = InformColaborador.CalculaFecha(Int32.Parse(meses_atras));

                ViewBag.Fechas = lista;
                ViewBag.control = "0";
            }
            catch (Exception ex) { }
            return View();
        }
        [HttpPost]
        public ActionResult VerLiquidacion(FormCollection form)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string ANIO = string.Empty;
                string MES = string.Empty;
                string valor = string.Empty;
                string Usuario = string.Empty;

                Usuario = (string)(Session["usuario"]);

                var InformColaborador = new InformacionColaborador();

                string Value = Request.Form["DropMeses"];

                string Date = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(Value));

                valor = string.Format("{0:MM-yyyy}", Convert.ToDateTime(Date));
                string[] split = valor.Split(new Char[] { '-' });
                MES = split[0];
                ANIO = split[1];

                ViewBag.liquidacion = "../Certificados/FileLiq.ashx?Rut=" + Usuario
                                                               + "&Mes=" + MES
                                                               + "&Anio=" + ANIO
                                                               + "&Tok=" + Session["Token"].ToString()
                                                               + "";

                string meses_atras = "12";

                List<string> lista = new List<string>();
                lista = InformColaborador.CalculaFecha(Int32.Parse(meses_atras));

                ViewBag.Fechas = lista;
                ViewBag.control = "1";
                ViewBag.mensaje = "";
                ViewBag.SelectMes = Value;
            }
            catch (Exception ex) { ViewBag.mensaje = ex.Message; }

            return View();
        }
        [HttpGet]
        public ActionResult SolicitudMiEquipo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SolicitudMiEquipo(string mensaje, FormCollection fomr)
        {

            return View("SolicitudMiEquipo2");
        }
        [HttpPost]
        public ActionResult SolicitudMiEquipo2(string FecDesde, string FecHasta, string RutColabor, string NombreColabor, FormCollection fomr)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Antiguedad()
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string NombreUsuario = (string)(Session["NombreUsuario"]);
                string Usuario = (string)(Session["usuario"]);
                ViewBag.Antiguedad = "../Certificados/Cert_Remu.ashx?Rut=" + Usuario
                                                                + "&Tok=" + Session["Token"].ToString()
                                                                + "&Rem=" + "X"
                                                                + "";

                var listCert = new InformacionColaborador();
                listCert.ListaCertificado();
                
                ViewBag.ListCert = listCert.ObjCertif;
               
                var listMotivos = new InformacionColaborador();
                listMotivos.ListaMotivo();
                ViewBag.ListMotivos = listMotivos.ObjMotivo;

                var LtaCtas = new InformacionColaborador();
                LtaCtas.ObtieneCtas();
                ViewBag.LtaCtas = LtaCtas.ObjCtas;
            }
            catch (Exception ex) { }
            return View();
        }
        [HttpPost]
        public ActionResult Antiguedad(FormCollection form)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }
            }
            catch (Exception ex) { ViewBag.mensaje = ex.Message; }

            return View();
        }

        [HttpGet]
        public ActionResult CambioPass()
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

               
            }
            catch (Exception ex) { }
            return View();
        }
        [HttpPost]
        public ActionResult CambioPass(FormCollection form)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }
            }
            catch (Exception ex) { ViewBag.mensaje = ex.Message; }

            return View();
        }
        [HttpPost]
        public ActionResult Sol_Certificado(string otroMotivo, string valorCred, string valorcta, FormCollection form)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string Tipo = Request.Form["ListCert"];
                string motivo = (otroMotivo == null) ? Request.Form["ListMotivos"] : otroMotivo;
                string cta = Request.Form["LtaCtas"];

              

                string unidadOrg = (string)Session["UnidadOrg"];
                string rut = (string)(Session["usuario"]);
                string nombre = (string)(Session["NombreUsuario"]);
                string usuarioWeb = (string)Session["UserWeb"];

                DateTime fechaActual = DateTime.Now;
                string fechaHoy = string.Format("{0:dd/MM/yyyy}", fechaActual.ToShortDateString());

                ConexionDBA db = new ConexionDBA();
                db.Conexion();
                string msn = db.Solicitud_pendientes((string)Session["UnidadOrg"], "2", (string)Session["RUT"]);
                db.ConexionClose();

                if (motivo == "1")
                {
                    ApiController api = new ApiController();
                    ListaDespegable lt = new ListaDespegable();

                     lt = api.ValidarCertificado(rut, valorCred, Session["Token"].ToString());
                    if (lt.COD == "01")
                    {
                        ActionResult actionResult = Antiguedad();

                        ViewBag.Mensaje = lt.TEXT;
                        ViewBag.Mostrar = true;                       
                        return View("Antiguedad");
                    }                  
                   
                }

                if (msn == "0")
                {

                    db.Conexion();
                    List<MdAprobador> ListAprob = new List<MdAprobador>();
                    if (Session["UsuarioAprobador"].ToString() == "JEFE") {
                        string pernr = Session["Pernr"].ToString();
                        ListAprob = db.ObtenerAprobadoresJefatura(pernr);
                    }
                    else
                    {
                        ListAprob = db.ObtenerAprobadores(unidadOrg);
                    }
                   
                    
                    db.ConexionClose();

                    if (ListAprob.Count() > 0)
                    {

                        db.Conexion();
                        string id_solicitud = db.insert_solicitud(unidadOrg, usuarioWeb, fechaHoy, "2");
                        db.ConexionClose();

                        db.Conexion();

                        string id_certificado = db.insert_certificado(id_solicitud, Tipo, motivo, valorCred, cta, valorcta, rut, nombre);
                        db.ConexionClose();

                        foreach (var item in ListAprob)
                        {
                            db.Conexion();
                            db.insert_estado_aprobacion(id_certificado, id_solicitud, Tipo, item.aprobador, item.sequencia);
                            db.ConexionClose();

                            if (item.sequencia == "1")
                            {
                                db.Conexion();
                                string correo = db.Obtener_Mail(item.aprobador);
                                db.ConexionClose();
                                string asunto = "Solicitud de Certificado";
                                string mensaje = "<p>se ha enviado una solicitud de certificado para el colaborador ";
                                mensaje += nombre;
                                mensaje += " , con el ID: ";
                                mensaje += id_solicitud;
                                mensaje += ". </p>";
                                string token = Session["Token"].ToString();

                                ApiController api = new ApiController();
                                api.EnvioMail(token, mensaje, asunto, correo);
                            }

                        }

                        ActionResult actionResult = Antiguedad();
                        ViewBag.Mostrar = true;
                        ViewBag.Mensaje = "Solicitud Ingresada";
                        return View("Antiguedad");

                    }
                }
                else
                {
                    ActionResult actionResult = Antiguedad();
                    ViewBag.Mostrar = true;
                    ViewBag.Mensaje = msn.ToString();
                    return View("Antiguedad");
                }


            }
            catch (Exception ex) { ViewBag.mensaje = ex.Message; }

            return View();
        }
        [HttpGet]
        public ActionResult Remuneracion()
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }

                string NombreUsuario = (string)(Session["NombreUsuario"]);
                string Usuario = (string)(Session["usuario"]);
                ViewBag.Remuneracion = "../Certificados/Cert_Remu.ashx?Rut=" + Usuario
                                                                      + "&Tok=" + Session["Token"].ToString()
                                                                      + "&Rem=" + ""
                                                                      + "";
            }
            catch (Exception ex) { }
            return View();
        }
        public string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }
        [HttpGet]
        public ActionResult VerSolicitud(string idSolicitud, string idTipo, string Estado)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }


                ConexionDBA db = new ConexionDBA();

                //var lista = new JsonResult();

                ViewBag.tipo = idTipo;
                switch (idTipo)
                {
                    case "1":
                        if (Estado == "1")
                        {
                            var archivo = CrearCertAbs(idSolicitud);
                            ViewBag.vercomprobante = "../Certificados/FileCS.ashx?Id=" + archivo;
                        }
                        break;
                    case "2":

                        if (Estado == "1")
                        {
                            ViewBag.Certificado = "../Certificados/Cert_Remu.ashx?ID=" + idSolicitud
                                                                                  + "&Tok=" + Session["Token"].ToString()
                                                                                  + "";
                        }

                        break;

                }

            }
            catch (Exception)
            {
                throw;
            }



            return PartialView("VerSolicitud");

        }

        [HttpGet]
        public ActionResult VerMotRech(string idSolicitud, string idTipo)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }


                ConexionDBA db = new ConexionDBA();
                db.Conexion();

                //var lista = new JsonResult();
                ViewBag.solicitud = idSolicitud;
                //ViewBag.tipo = idTipo;
                ViewBag.msn = db.obtener_motivo_Rechazo(idSolicitud, idTipo);

            }
            catch (Exception)
            {
                throw;
            }



            return PartialView("VerMotRech");

        }


        public Boolean verificaSesion()
        {
            try
            {
                string session_id = (string)(Session["NombreUsuario"]);

                if (session_id == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) { }

            return false;
        }
        public ActionResult CerrarSolicitud()
        {

            return RedirectToAction("MisSolicitudes", "HOME");
        }

        public ActionResult CerrarSolJefe()
        {


            return RedirectToAction("MiEquipo", "HOME");
        }

        [HttpGet]  //Solictud Revision Mi Equipo 01.08.2019
        public ActionResult SolicitudRevisionEquipo()
        {

            return View();
        }
        public double CalcularDiasDeDiferencia(DateTime primerFecha, DateTime segundaFecha)
        {
            TimeSpan diferencia;
            diferencia = segundaFecha - primerFecha;

            return diferencia.Days;
        }
        [HttpGet]  //Solictud Reporte Absentismo JMB 01.08.2019
        public ActionResult ReporteAbsentismo()
        {
            string NombreUsuario = (string)(Session["NombreUsuario"]);
            string Usuario = (string)(Session["usuario"]);

            return View();
        }
        [HttpPost]
        public ActionResult SolitsinusodiasMiEquipo(string RutColabor, string NombreColabor, FormCollection fomr)
        {

            TempData["Mensaje"] = ViewBag.Mensaje;
            TempData["RutColabor"] = RutColabor;
            TempData["NombreColabor"] = NombreColabor;
            return RedirectToAction("SolicitudMiEquipo2", "HOME");
        }
        [HttpPost]
        public ActionResult Solitsinusodias(string RutColabor, FormCollection fomr)
        {

            TempData["Mensaje"] = ViewBag.Mensaje;
            return RedirectToAction("Solicitud", "HOME");
        }
        [HttpPost]
        public ActionResult Enviar(string RutColabor, string ACCION, FormCollection fomr)
        {

            return RedirectToAction("SolicitudRevisionEquipo", "HOME", new { Mensaje = ViewBag.Mensaje });
        }

        [HttpGet]
        public ActionResult CambioDeContra(string Pass)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }
                string correo = Session["Correo"].ToString();
                string pernr = Session["Pernr"].ToString();


                ConexionDBA db = new ConexionDBA();
                db.Conexion();
                db.cambiarPass(correo, pernr, Pass);
                db.ConexionClose();
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Content("OK");

            }
            catch(Exception)
            {
                return null;
            }

         }
        [HttpGet]
        public ActionResult VerSolicitudJefatura(string idSolicitud, string idTipo, string Nombre)
        {
            try
            {
                if (verificaSesion() == true)
                {
                    return RedirectToAction("Login", "LOGIN", new { });
                }


                ConexionDBA db = new ConexionDBA();

                //var lista = new JsonResult();
                ViewBag.Nombre = Nombre;
                ViewBag.id = idSolicitud;
                ViewBag.tipo = idTipo;
                var listas = new InformacionColaborador();
                switch (idTipo)
                {
                    case "1":

                        MdDetAbs abs = new MdDetAbs();
                        db.Conexion();
                        abs = db.verDetalleAbs(idSolicitud);
                        db.ConexionClose();

                        abs.fecha_ini = string.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(abs.fecha_ini));
                        abs.fecha_fin = string.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(abs.fecha_fin));
                        ViewBag.abs = abs;

                        ViewBag.text_tipo = "Vacaciones";



                        break;
                    case "2":

                        MdDetCertificado cert = new MdDetCertificado();
                        db.Conexion();
                        cert = db.GetCertificado(idSolicitud);
                        db.ConexionClose();
                        ViewBag.cert = cert;

                                              
                        listas.ListaCertificado();
                        listas.ListaMotivo();

                        foreach (var item in listas.ObjCertif.Where(n => n.COD == cert.tipo_certificado))
                        {
                            ViewBag.text_tipo = item.TEXT;

                        }

                        foreach (var item in listas.ObjMotivo.Where(n => n.COD == cert.tipo_motivo))
                        {
                            ViewBag.text_motivo = item.TEXT;
                        }


                        break;

                }

            }
            catch (Exception)
            {
                throw;
            }



            return PartialView("VerDetSolicitud");

        }
        public string CrearCertAbs(string idSolicitud)
        {
            try
            {

                MdDetAbs md = new MdDetAbs();
                ConexionDBA db = new ConexionDBA();
                db.Conexion();
                md = db.verDetalleAbs(idSolicitud);


                string NombreUsuario = md.nombre;
                string Usuario = md.rut;
                Usuario = formatearRut(Usuario);

                string dir = Server.MapPath("/");
                string newDoc = "SolicitudVacaciones_" + Usuario + ".pdf";
                string newFile = dir + @"\Certificados\" + newDoc;
                // Create a Document object
                var document = new Document(PageSize.A4, 60, 60, 50, 25);

                // Create a new PdfWriter object, specifying the output stream
                var output = new FileStream(newFile, FileMode.Create);
                var writer = PdfWriter.GetInstance(document, output);

                // Open the Document for writing
                document.Open();

                string nombre_logo = "logo.png";
                string logo_emp = dir + @"\images\" + nombre_logo;
                var logo = iTextSharp.text.Image.GetInstance(logo_emp);
                logo.ScalePercent(25f);
                logo.SetAbsolutePosition(60, 760);


                document.Add(logo);

                document.Add(new Paragraph("\n"));


                var titleFont = FontFactory.GetFont("Arial", 13, Font.BOLD);

                string txtTipoSolicitud = string.Empty;

                txtTipoSolicitud = "FERIADO LEGAL";

                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));


                PdfContentByte cb = writer.DirectContent;
                cb.Rectangle(50f, 400f, 500f, 350f);
                Paragraph titulo = new Paragraph("COMPROBANTE DE " + txtTipoSolicitud, titleFont);
                titulo.IndentationLeft = 130;
                document.Add(titulo);


                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));

                Font arial = FontFactory.GetFont("Arial", 13f);
                Font arial2 = FontFactory.GetFont("Arial", 11f);

                Chunk beginning = new Chunk("Nombre : " + NombreUsuario, arial);
                Phrase p1 = new Phrase(beginning);

                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(p1);
                document.Add(p);

                string line2 = "Rut : " + Usuario;
                Chunk beginning2 = new Chunk(line2, arial);


                string line3;

                line3 = "";


                Chunk beginning3 = new Chunk(line3, arial);
                Phrase p3 = new Phrase(beginning3);
                Phrase p2 = new Phrase(beginning2);
                Paragraph p_2 = new Paragraph();
                p_2.Alignment = Element.ALIGN_LEFT;
                p_2.Add(p2);
                p_2.Add(p3);
                document.Add(p_2);

                document.Add(new Paragraph("\n"));

                Chunk beginning4;

                beginning4 = new Chunk("Declaro que a partir de las fechas indicadas, haré uso de mi período de feriado legal", arial2);


                Phrase p4 = new Phrase(beginning4);
                Paragraph p_4 = new Paragraph();
                p_4.Alignment = Element.ALIGN_LEFT;
                p_4.SpacingAfter = Element.ALIGN_MIDDLE;
                p_4.SetLeading(3, 1);
                p_4.Add(p4);
                document.Add(p_4);

                string fec1 = string.Format("{0:d/M/yyyy}", Convert.ToDateTime(md.fecha_ini));
                string fec2 = string.Format("{0:d/M/yyyy}", Convert.ToDateTime(md.fecha_fin));

                TimeSpan diferencia;
                DateTime ini = DateTime.Parse(md.fecha_ini);
                DateTime fin = DateTime.Parse(md.fecha_fin);
                diferencia = ini - fin;

                double Dias = diferencia.Days;


                string line4;
                line4 = "Inicio :" + fec1;

                Chunk beginning5 = new Chunk(line4, arial);

                string line5 = "";
                line5 = "                          Término :" + fec2;


                Chunk beginning6 = new Chunk(line5, arial);
                Phrase p5 = new Phrase(beginning5);
                Phrase p6 = new Phrase(beginning6);
                Paragraph p_5 = new Paragraph();
                p_5.Alignment = Element.ALIGN_LEFT;
                p_5.Add(p5);
                p_5.Add(p6);
                document.Add(p_5);

                document.Add(new Paragraph("\n"));
                Chunk beginning7 = new Chunk("Cantidad de días" + " :" + Dias, arial);
                Phrase p7 = new Phrase(beginning7);
                Paragraph p_6 = new Paragraph();
                p_6.Alignment = Element.ALIGN_LEFT;
                p_6.Add(p7);
                document.Add(p_6);

                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));

                Chunk beginning8 = new Chunk("  ________________                                ________________", arial);
                Phrase p8 = new Phrase(beginning8);
                Paragraph p_7 = new Paragraph();
                p_7.Alignment = Element.ALIGN_CENTER;
                p_7.Add(p8);
                document.Add(p_7);

                Chunk beginning9 = new Chunk("   Firma Jefatura                                         Firma Trabajador", arial);
                Phrase p9 = new Phrase(beginning9);
                Paragraph p_8 = new Paragraph();
                p_8.Alignment = Element.ALIGN_CENTER;
                p_8.Add(p9);
                document.Add(p_8);

                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));
                cb.Stroke();

                document.Close();

                return newFile;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}