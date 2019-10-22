using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProyectoTanner.Services;
using ProyectoTanner.Models;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProyectoTanner.Controllers
{
    public class HomeController : Controller
    {
        string fecUltSoli;
        DateTime fec_1;
        DateTime fec_2;
        string[] results;

        public ActionResult Login(string usuario, string password)
        {
            

            //try
            //{
            //    if (usuario != null && password != null)
            //    {
            //        Session["usuarioSap"] = ConfigurationManager.AppSettings["usuario"];
            //        Session["contrasenaSap"] = ConfigurationManager.AppSettings["password"];

            //        var InformColaborador = new InformacionColaborador();

            //        var model = InformColaborador.ObtenerDatos(usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //        if (model.Count > 0)
            //        {
            //            Session["SessionActiva"] = "X";
            //            Session["NombreUsuario"] = string.Concat(string.Concat(model[0].VORNA + " " + model[0].NACH2));
            //            Session["Usuario"] = usuario;

            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            return View();
          // return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(string usuario)
        {
            //try
            //{
            //    if (usuario != null)
            //    {
            //        Session["usuarioSap"] = ConfigurationManager.AppSettings["usuario"];
            //        Session["contrasenaSap"] = ConfigurationManager.AppSettings["password"];

            //        var InformColaborador = new InformacionColaborador();

            //        var model = InformColaborador.ObtenerDatos(usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //        if (model.Count > 0)
            //        {
            //            Session["SessionActiva"] = "X";
            //            Session["NombreUsuario"] = string.Concat(string.Concat(model[0].VORNA), model[0].NACH2);
            //            Session["Usuario"] = usuario;

            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
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

            //return RedirectToAction("Login", "LOGIN");
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
                //var VacacionesService = new VacacionesService();
                //VacacionesService.ObtenerVacacionesHistorico(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
                //ViewBag.ObtenerVacHistorico = VacacionesService.ListaVacacionesHistorico;
                //if (Mensaje != null)
                //{
                //    ViewBag.Mostrar = true;
                //    ViewBag.Mensaje = Mensaje;
                //}
            }
            catch (Exception ex)
            {

            }

            return View();
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

            string url = "http://164.77.177.179:5055/API/login/GetToken?Username=QHRPORTAL&Password=1234";

            string apikey = new WebClient().DownloadString(url);

            string url_Principal2 = "http://164.77.177.179:5055/API/ZHR_GET_CONT?RUT=";
            var rut = "12279395-8";
            var url_2 = url_Principal2 + rut;
            string key2 = "Bearer " + apikey;

            var json2 = new WebClient();
            json2.Headers.Add("Authorization", key2);
            var Retorno2 = json2.DownloadString(url_2);
            Retorno2.Remove(0,2);
            Retorno2 = Retorno2.Remove(Retorno2.Length - 1);


            var Convert = JsonConvert.DeserializeObject<RootObject>(Retorno2);            

            JObject json = JObject.Parse(Retorno2);
            List<JToken> dataa = json.Children().ToList();

            List<_CONTINGENTE> lista = new List<_CONTINGENTE>();
            List<TipoVacaciones> ListaVaca = new List<TipoVacaciones>();

            _CONTINGENTE table = new _CONTINGENTE();
            TipoVacaciones tableTipVac = new TipoVacaciones();

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


            //Mostrar Div Vacaciones 
            ViewBag.ocultar = true;
            //Ocultar Div Vacaciones sin uso de días
            ViewBag.ocultar2 = false;

            return View();
        }

        [HttpGet]
        public ActionResult SolicitudMiEquipo2()
        {
            //try
            //{
            //    VacacionesService VacacionesService = new VacacionesService();

            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);



            //    var Mensaje = TempData["Mensaje"].ToString();
            //    var RutColabor = TempData["RutColabor"].ToString();
            //    var NombreColabor = TempData["NombreColabor"].ToString();

            //    if (Mensaje != null)
            //    {
            //        ViewBag.Mensaje = Mensaje;
            //        //Mostrar Div Mensaje 
            //        ViewBag.Mostrar = true;
            //        //Ocultar Div Vacaciones 
            //        ViewBag.ocultar = true;
            //        //Mostrar Div Vacaciones sin uso de días
            //        ViewBag.ocultar2 = false;

            //        ViewBag.RutColabor = RutColabor;
            //        ViewBag.NombreColabor = NombreColabor;

            //    }
            //    else
            //    {
            //        //Mostrar Div Vacaciones 
            //        ViewBag.ocultar = true;
            //        //Ocultar Div Vacaciones sin uso de días
            //        ViewBag.ocultar2 = false;

            //        ViewBag.RutColabor = RutColabor;
            //        ViewBag.NombreColabor = NombreColabor;

            //    }


            //    VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    ViewBag.Milistado = VacacionesService.ObjSalida;
            //    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //    ViewBag.Milistado3 = VacacionesService.ObjTipo;

            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();

            //    //Obtiene Dias Para Permisos Administrativos
            //    VacacionesService.ObtieneDiasPermi();
            //    ViewBag.MiPermi = VacacionesService.ObjPerAdmi;

            //}
            //catch (Exception ex)
            //{

            //}
            return View();
        }

        [HttpPost]
        public ActionResult Solicitud(string FecDesde, string FecHasta, string TipoVaca, FormCollection fomr)
        {
            //VacacionesService SolicitudVaca = new VacacionesService();
            //VacacionesService VacacionesService = new VacacionesService();
            //VacacionesService VacacionesService2 = new VacacionesService();

            //if (verificaSesion() == true)
            //{
            //    return RedirectToAction("Login", "LOGIN", new { });
            //}

            //DateTime fechHoy = DateTime.Now;

            //string fechadia = Convert.ToString(fechHoy);

            //string Value = Request.Form["DropSoli"];

            //string NombreUsuario = (string)(Session["NombreUsuario"]);
            //string Usuario = (string)(Session["usuario"]);
            //string Usuario2 = string.Empty;

            //var dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            //var date = DateTime.ParseExact(FecDesde, "dd/MM/yyyy", dateTimeFormat);
            //var date2 = DateTime.ParseExact(FecHasta, "dd/MM/yyyy", dateTimeFormat);

            //string fech_d = string.Format("{0:d/M/yyyy}", fechadia);

            //DateTime fec_1 = Convert.ToDateTime(date);
            //DateTime fec_2 = Convert.ToDateTime(date2);
            //DateTime fec_3 = Convert.ToDateTime(fech_d);

            //if (Value == "01")
            //{
                
            //    Value = Request.Form["Namedrop"];

            //    VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    ViewBag.Milistado = VacacionesService.ObjSalida;
            //    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //    ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //    ViewBag.DropPerm = VacacionesService.ObjPerAdmi;

            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();
            //    VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    ViewBag.DropSoli2 = VacacionesService2.ObjTipo;
            //    VacacionesService2.ObtieneDiasPermi();
            //    ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;

            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();

            //    ViewBag.Mensaje = "Debe Seleccionar un tipo de Vacaciones y/o Permiso.";
            //    ViewBag.Mostrar = true;
            //    ViewBag.ocultar = true;
            //    ViewBag.ocultar2 = false; //uso de dias
            //}
            //else
            //{
            //    if (Value == "7000") /*Absentismo Vacaciones*Add Jaime Marchant/*/
            //    {
            //        try
            //        {
            //            List<SolicitudVacaciones> Solicit = new List<SolicitudVacaciones>();
            //            SolicitudVacaciones p_Solicit;

                        

            //            //Llena Estructura 
            //            p_Solicit = new SolicitudVacaciones();
            //            p_Solicit.ICNUM1 = Usuario;
            //            p_Solicit.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //            p_Solicit.AWART = Value;
            //            p_Solicit.BEGDA = fec_1.ToString("dd/MM/yyyy"); ;
            //            p_Solicit.ENDDA = fec_2.ToString("dd/MM/yyyy"); ;
            //            p_Solicit.ICNUM2 = Usuario;
            //            p_Solicit.SPRPS = "X";
            //            Solicit.Add(p_Solicit);

            //            if (fec_2 >= fec_1)
            //            {

            //                //Validacion Cant Dias 05.09.2019 Jaime Marchant B
            //                DateTime fechaUno = Convert.ToDateTime(fec_1);
            //                DateTime fechaDos = Convert.ToDateTime(fec_2);

            //                //Validacion entre Meses Marzo a Noviembre 05.09.2019 Jaime Marchant B
            //                string cadena = fec_1.ToString("ddMMyyyy");
            //                string cadena2 = fec_2.ToString("ddMMyyyy");

            //                int tam_var = cadena.Length;
            //                int Var_Sub = Convert.ToInt32(cadena.Substring((tam_var - 6), 2));

            //                int tam_var2 = cadena2.Length;
            //                int Var_Sub2 = Convert.ToInt32(cadena2.Substring((tam_var2 - 6), 2));


            //                double dias = CalcularDiasDeDiferencia(fechaUno, fechaDos);
            //                int dmax = 10;
            //                if (dias >= dmax)
            //                {
            //                    if (Var_Sub >= 03 && Var_Sub2 <= 11)
            //                    {
            //                        SolicitudVaca.SolictudVacaciones(Solicit, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        if (SolicitudVaca.Objmensaje.Count > 0)
            //                        {
            //                            for (int i = 0; i < SolicitudVaca.Objmensaje.Count; i++)
            //                            {
            //                                if (SolicitudVaca.Objmensaje[i].codigo == "I")
            //                                {

            //                                    ViewBag.Mensaje  = SolicitudVaca.Objmensaje[i].mensaje;
            //                                    ViewBag.Mostrar  = true;
            //                                    ViewBag.ocultar2 = true; //uso de dias
            //                                    ViewBag.ocultar  = false;
                                                

            //                                    VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                    ViewBag.Milistado3 = VacacionesService.ObjTipo;

            //                                    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                    ViewBag.dias = VacacionesService.ObtieneDias();
            //                                    VacacionesService.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.DropSoli2 = VacacionesService.ObjTipo;
            //                                    VacacionesService.ObtieneDiasPermi();
            //                                    ViewBag.MiPermi = VacacionesService.ObjPerAdmi;

            //                                    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                    ViewBag.dias = VacacionesService.ObtieneDias();
            //                                }
            //                                else
            //                                {
            //                                    ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                    ViewBag.Mostrar = true;
            //                                    ViewBag.ocultar = true;
            //                                    ViewBag.ocultar2 = false;

            //                                    VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                    ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                                    VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.DropSoli2 = VacacionesService2.ObtieneDias();
            //                                    VacacionesService2.ObtieneDiasPermi();
            //                                    ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;


            //                                    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                    ViewBag.dias = VacacionesService.ObtieneDias();
            //                                }
                                        
            //                            }
            //                        }
            //                        else
            //                        {
            //                            VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                            ViewBag.Milistado = VacacionesService.ObjSalida;
            //                            ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                            ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                            VacacionesService.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                            ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                            VacacionesService.ObtieneDiasPermi();
            //                            ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                            ViewBag.ocultar = false;


            //                            //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                            ViewBag.dias = VacacionesService.ObtieneDias();

            //                        }
            //                    }
            //                    else
            //                    {
            //                        ViewBag.Mensaje = "la Fecha Seleccionadas Deben Estar entre los meses de Marzo y Noviembre";
            //                        ViewBag.Mostrar = true;
            //                    }

            //                }
            //                else
            //                {
            //                    SolicitudVaca.SolictudVacaciones(Solicit, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                    if (SolicitudVaca.Objmensaje.Count > 0)
            //                    {
            //                        for (int i = 0; i < SolicitudVaca.Objmensaje.Count; i++)
            //                        {
            //                            if (SolicitudVaca.Objmensaje[i].codigo == "I")
            //                            {

            //                                ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                ViewBag.Mostrar = true;
            //                                ViewBag.ocultar2 = false; //uso de dias
            //                                ViewBag.ocultar = true;


            //                                VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                ViewBag.Milistado3 = VacacionesService.ObjTipo;

            //                                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                ViewBag.dias = VacacionesService.ObtieneDias();
            //                                VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                ViewBag.DropSoli2 = VacacionesService2.ObjTipo;
            //                                VacacionesService2.ObtieneDiasPermi();
            //                                ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;

            //                                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                ViewBag.dias = VacacionesService.ObtieneDias();
                          
            //                            }
            //                            else
            //                            {
            //                                ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                ViewBag.Mostrar = true;
            //                                ViewBag.ocultar = true;
            //                                ViewBag.ocultar2 = false;

            //                                VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                                VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                //ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                                VacacionesService2.ObtieneDiasPermi();
            //                                ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;


            //                                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                ViewBag.dias = VacacionesService.ObtieneDias();
            //                            }

            //                        }
            //                    }
            //                    else
            //                    {
            //                        VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.Milistado = VacacionesService.ObjSalida;
            //                        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                        VacacionesService.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                        VacacionesService.ObtieneDiasPermi();
            //                        ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                        ViewBag.ocultar = false;


            //                        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                        ViewBag.dias = VacacionesService.ObtieneDias();

            //                    }
            //                }
            //            }
            //            else
            //            {
            //                VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                VacacionesService.ObtieneDiasPermi();
            //                ViewBag.MiPermi = VacacionesService.ObjPerAdmi;


            //                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                ViewBag.dias = VacacionesService.ObtieneDias();
            //                ViewBag.Mensaje = "Fecha inicial de vacaciones debe ser mayor a la fecha hasta.";
            //                ViewBag.Mostrar = true;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }

            //    }
            //    else /* Add Jaime Marchant 26.07.2019*/
            //    {
            //        List<SolicitudPermisos> SolicitPermi = new List<SolicitudPermisos>();
            //        SolicitudPermisos p_SolicitPermi;

            //        NombreUsuario = (string)(Session["NombreUsuario"]);
            //        Usuario = (string)(Session["usuario"]);
            //        Usuario2 = string.Empty;

            //        dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            //        date = DateTime.ParseExact(FecDesde, "dd/MM/yyyy", dateTimeFormat);
            //        date2 = DateTime.ParseExact(FecHasta, "dd/MM/yyyy", dateTimeFormat);

            //        fech_d = string.Format("{0:d/M/yyyy}", fechadia);

            //        fec_1 = Convert.ToDateTime(date);
            //        fec_2 = Convert.ToDateTime(date2);
            //        fec_3 = Convert.ToDateTime(fech_d);

            //        VacacionesService SolicitudPermi = new VacacionesService();

            //        if (Value == "3010")
            //        {
            //            //Obtiene codigo Solicitud Permiso 3010-3012
            //            string ValueServ = Request.Form["DropPerm"];
            //            //Llena Estructura 

            //            p_SolicitPermi = new SolicitudPermisos();
            //            p_SolicitPermi.BEGDA = fec_1.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.ENDDA = fec_2.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.SUBTY = ValueServ;
            //            p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.RUT_CREA = Usuario;
            //            p_SolicitPermi.RUT_EMPLEA = Usuario;
            //            SolicitPermi.Add(p_SolicitPermi);
            //        }
            //        else
            //        {
            //            //Llena Estructura 
            //            p_SolicitPermi = new SolicitudPermisos();
            //            p_SolicitPermi.BEGDA = fec_1.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.ENDDA = fec_2.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.SUBTY = Value;
            //            p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.RUT_CREA = Usuario;
            //            p_SolicitPermi.RUT_EMPLEA = Usuario;
            //            SolicitPermi.Add(p_SolicitPermi);
            //        }

            //        if (fec_2 >= fec_1)
            //        {
                  
            //            SolicitudPermi.SolictudPermisos(SolicitPermi, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //            if (SolicitudPermi.Objmensaje.Count > 0)
            //            {
            //                for (int i = 0; i < SolicitudPermi.Objmensaje.Count; i++)
            //                {
            //                    ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //                    //Code Error E = 001
            //                    if (SolicitudPermi.Objmensaje[i].codigo == "E")
            //                    {
            //                        ViewBag.Mostrar = true;
            //                        ViewBag.ocultar2 = false;
            //                        ViewBag.ocultar = true;


            //                        VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.Milistado = VacacionesService.ObjSalida;
            //                        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                        ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                        VacacionesService.ObtieneDiasPermi();
            //                        ViewBag.MiPermi = VacacionesService.ObjPerAdmi;

            //                        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                        ViewBag.dias = VacacionesService.ObtieneDias();
            //                    }
            //                    else
            //                    {
            //                        ViewBag.Mostrar = true;
            //                        ViewBag.ocultar2 = false;
            //                        ViewBag.ocultar = true;


            //                        VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.Milistado = VacacionesService.ObjSalida;
            //                        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                        ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                        VacacionesService.ObtieneDiasPermi();
            //                        ViewBag.MiPermi = VacacionesService.ObjPerAdmi;

            //                        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                        ViewBag.dias = VacacionesService.ObtieneDias();

            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //            ViewBag.Milistado = VacacionesService.ObjSalida;
            //            ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //            ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //            ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //            VacacionesService.ObtieneDiasPermi();
            //            ViewBag.MiPermi = VacacionesService.ObjPerAdmi;

            //            //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //            ViewBag.dias = VacacionesService.ObtieneDias();

            //            ViewBag.Mensaje = "Fecha inicial de vacaciones debe ser mayor a la fecha hasta.";
            //            ViewBag.Mostrar = true;
            //        }
            //    }
            //}
            return View("Solicitud");
        }

        [HttpGet]
        public ActionResult MiEquipo()
        {
            //try
            //{
            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    VacacionesService Aprobaciones = new VacacionesService();
            //    Aprobaciones.ObtenerListaPendientesAprob(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    ViewBag.MilistadoAProb = Aprobaciones.ObjPendAprob;

            //    if (Aprobaciones.ObjPendAprob.Count > 0)
            //    {
            //        ViewBag.CountList = "1";
            //    }
            //    else
            //    {
            //        ViewBag.CountList = "2";
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            return View();
        }

        [HttpPost]
        public ActionResult MiEquipo(FormCollection collection)
        {
            try { }
            //{
            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    string Colaborador2 = string.Empty;
            //    string Colaborador = string.Empty;
            //    string Aprueba = "3";
            //    string Rechaza = "4";
            //    VacacionesService Aprobaciones = new VacacionesService();
            //    string DOCNR = Request.Form["DOCNR"];
            //    string Aprobar = Request.Form["APROBAR"];
            //    string Rechazar = Request.Form["RECHAZAR"];

            //    string[] parts = DOCNR.Split('-');

            //    DOCNR = parts[0];
            //    Colaborador2 = parts[1].Replace(".", "") + parts[2];
            //    Colaborador = Colaborador2;


            //    if (Aprobar == "X")
            //    {
            //        Aprobaciones.AprobarRechazarVacaciones(Usuario, Aprueba, DOCNR, Colaborador, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //        ViewBag.ListaMensaje = Aprobaciones.Objmensaje;
            //        if (Aprobaciones.Objmensaje.Count > 0)
            //        {
            //            for (int i = 0; i < Aprobaciones.Objmensaje.Count; i++)
            //            {
            //                ViewBag.Mensaje = Aprobaciones.Objmensaje[i].mensaje;
            //            }

            //            ViewBag.Mostrar = true;

            //            Aprobaciones.ObtenerListaPendientesAprob(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //            ViewBag.MilistadoAProb = Aprobaciones.ObjPendAprob;

            //            if (Aprobaciones.ObjPendAprob.Count > 0)
            //            {
            //                ViewBag.CountList = "1";
            //            }
            //            else
            //            {
            //                ViewBag.CountList = "2";
            //            }
            //        }
            //        else
            //        {
            //            Aprobaciones.ObtenerListaPendientesAprob(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //            ViewBag.MilistadoAProb = Aprobaciones.ObjPendAprob;
            //            ViewBag.ListaMensaje = Aprobaciones.ObjSalida;

            //            if (Aprobaciones.ObjPendAprob.Count > 0)
            //            {
            //                ViewBag.CountList = "1";
            //            }
            //            else
            //            {
            //                ViewBag.CountList = "2";
            //            }
            //        }
            //    }
            //    else if (Rechazar == "X")
            //    {
            //        Aprobaciones.AprobarRechazarVacaciones(Usuario, Rechaza, DOCNR, Colaborador, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //        ViewBag.ListaMensaje = Aprobaciones.Objmensaje;
            //        if (Aprobaciones.Objmensaje.Count > 0)
            //        {
            //            for (int i = 0; i < Aprobaciones.Objmensaje.Count; i++)
            //            {
            //                ViewBag.Mensaje = Aprobaciones.Objmensaje[i].mensaje;
            //            }

            //            ViewBag.Mostrar = true;

            //            Aprobaciones.ObtenerListaPendientesAprob(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //            ViewBag.MilistadoAProb = Aprobaciones.ObjPendAprob;

            //            if (Aprobaciones.ObjPendAprob.Count > 0)
            //            {
            //                ViewBag.CountList = "1";
            //            }
            //            else
            //            {
            //                ViewBag.CountList = "2";
            //            }
            //        }
            //        else
            //        {
            //            Aprobaciones.ObtenerListaPendientesAprob(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //            ViewBag.MilistadoAProb = Aprobaciones.ObjPendAprob;
            //            ViewBag.ListaMensaje = Aprobaciones.ObjSalida;

            //            if (Aprobaciones.ObjPendAprob.Count > 0)
            //            {
            //                ViewBag.CountList = "1";
            //            }
            //            else
            //            {
            //                ViewBag.CountList = "2";
            //            }
            //        }

            //    }
            //}
            catch (Exception ex) { }

            return View();
        }



        [HttpGet]
        public ActionResult Anular(string Docnr)
        {
            //try
            //{

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    string Rechaza = "6";

            //    VacacionesService Aprobaciones = new VacacionesService();

            //    Aprobaciones.AprobarRechazarVacaciones(Usuario, Rechaza, Docnr, Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //    if (Aprobaciones.Objmensaje.Count > 0)
            //    {
            //        for (int i = 0; i < Aprobaciones.Objmensaje.Count; i++)
            //        {
            //            ViewBag.Mensaje = Aprobaciones.Objmensaje[i].mensaje;
            //        }

            //        ViewBag.Mostrar = true;
            //    }
            //    else
            //    {
            //        Aprobaciones.ObtenerListaPendientesAprob(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //        ViewBag.MilistadoAProb = Aprobaciones.ObjPendAprob;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
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

                //ViewBag.liquidacion = "../Certificados/FileLiquidacion.ashx?Rut=" + Usuario + "&Mes=" + MES + "&Anio=" + ANIO + "&Usuario=" + Session["usuarioSap"].ToString() + "&Clave=" + Session["contrasenaSap"].ToString() + "";
                ViewBag.liquidacion   = "../Certificados/FileLiq.ashx?Rut=" + Usuario
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
            //try
            //{
            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    VacacionesService VacacionesService = new VacacionesService();
            //    InformacionColaborador Colaboradores = new InformacionColaborador();
            //    Colaboradores.ObtenerDatosJerarquia(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());


            //    ViewBag.ListaColaborades = Colaboradores.ObjSalida;

            //    for (int i = 0; i < Colaboradores.ObjSalida.Count; i++)
            //    {
            //        ViewBag.RutColaborador = Colaboradores.ObjSalida[i].ICNUM;
            //        ViewBag.NombreColaborador = Colaboradores.ObjSalida[i].VORNA;
            //    }

            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();
            //}
            //catch (Exception ex) { }

            return View();
        }

        [HttpPost]
        public ActionResult SolicitudMiEquipo(string mensaje,FormCollection fomr)
        {

            //VacacionesService VacacionesService = new VacacionesService();
            //VacacionesService VacacionesService2 = new VacacionesService();
            //try
            //{
            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);

            //    string Value = Request.Form["Namedrop"];

            //    InformacionColaborador Colaboradores = new InformacionColaborador();
            //    Colaboradores.ObtenerDatosJerarquia(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //    ViewBag.ListaColaborades = Colaboradores.ObjSalida;

            //    for (int i = 0; i < Colaboradores.ObjSalida.Count; i++)
            //    {
            //        if (Colaboradores.ObjSalida[i].ICNUM == Value)
            //        {
            //            ViewBag.RutColabor = Colaboradores.ObjSalida[i].ICNUM;
            //            ViewBag.NombreColabor = Colaboradores.ObjSalida[i].VORNA;
            //        }
            //    }

            //    string Colaborador = ViewBag.RutColabor;
                

            //    if (mensaje != null)
            //    {
            //        //ocultar div
            //        ViewBag.ocultar = true;
            //        ViewBag.ocultar2 = false;
            //        ViewBag.Mensaje = mensaje;
            //        ViewBag.Mostrar = true;

            //        VacacionesService.ObtenerVacaciones7001(Colaborador, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //        ViewBag.Milistado = VacacionesService.ObjSalida;
            //        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //    }
            //    else
            //    {
            //        //ocultar div
            //        ViewBag.ocultar = true;
            //        ViewBag.ocultar2 = false;

            //        VacacionesService.ObtenerVacaciones(Colaborador, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //        ViewBag.Milistado = VacacionesService.ObjSalida;
            //        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //        ViewBag.Milistado3 = VacacionesService.ObjTipo;

            //        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //        ViewBag.dias = VacacionesService.ObtieneDias();
            //        VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //        ViewBag.DropSoli2 = VacacionesService2.ObjTipo;
            //        VacacionesService2.ObtieneDiasPermi();
            //        ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;

            //        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //        ViewBag.dias = VacacionesService.ObtieneDias();


            //    }
            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();
            //}
            //catch (Exception ex) { }
            return View("SolicitudMiEquipo2");
        }


        [HttpPost]
        public ActionResult SolicitudMiEquipo2(string FecDesde, string FecHasta, string RutColabor, string NombreColabor, FormCollection fomr)
        {

            //VacacionesService SolicitudVaca = new VacacionesService();
            //VacacionesService VacacionesService = new VacacionesService();
            //VacacionesService VacacionesService2 = new VacacionesService();

            //if (verificaSesion() == true)
            //{
            //    return RedirectToAction("Login", "LOGIN", new { });
            //}

            //string ValueColaborador= Request.Form["Namedrop"];

            //DateTime fechHoy = DateTime.Now;

            //string fechadia = Convert.ToString(fechHoy);

            //string Value = Request.Form["DropSoli"];

            //string NombreUsuario = (string)(Session["NombreUsuario"]);
            //string Usuario = (string)(Session["usuario"]);
            //string Usuario2 = string.Empty;

          
            //string fech_d = string.Format("{0:d/M/yyyy}", fechadia);

           

            //if (Value == "01")
            //{

            //    Value = Request.Form["Namedrop"];

            //    VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    ViewBag.Milistado = VacacionesService.ObjSalida;
            //    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //    ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //    ViewBag.DropPerm = VacacionesService.ObjPerAdmi;

            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();
            //    VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    ViewBag.DropSoli2 = VacacionesService2.ObjTipo;
            //    VacacionesService2.ObtieneDiasPermi();
            //    ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;

            //    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //    ViewBag.dias = VacacionesService.ObtieneDias();

            //    ViewBag.Mensaje = "Debe Seleccionar un tipo de Vacaciones y/o Permiso.";
            //    ViewBag.Mostrar = true;
            //    ViewBag.ocultar = true;
            //    ViewBag.ocultar2 = false; //uso de dias
            //}
            //else
            //{
            //    if (Value == "7000") /*Absentismo Vacaciones*Add Jaime Marchant/*/
            //    {

            //        var dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            //        var date = DateTime.ParseExact(FecDesde, "dd/MM/yyyy", dateTimeFormat);
            //        var date2 = DateTime.ParseExact(FecHasta, "dd/MM/yyyy", dateTimeFormat);

            //        DateTime fec_1 = Convert.ToDateTime(date);
            //        DateTime fec_2 = Convert.ToDateTime(date2);
            //        DateTime fec_3 = Convert.ToDateTime(fech_d);

            //        try
            //        {
            //            List<SolicitudVacaciones> Solicit = new List<SolicitudVacaciones>();
            //            SolicitudVacaciones p_Solicit;
            //            //Llena Estructura 
            //            p_Solicit = new SolicitudVacaciones();
            //            p_Solicit.ICNUM1 = Usuario;
            //            p_Solicit.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //            p_Solicit.AWART = Value;
            //            p_Solicit.BEGDA = fec_1.ToString("dd/MM/yyyy"); ;
            //            p_Solicit.ENDDA = fec_2.ToString("dd/MM/yyyy"); ;
            //            p_Solicit.ICNUM2 = RutColabor;
            //            p_Solicit.SPRPS = "X";
            //            Solicit.Add(p_Solicit);

            //            if (fec_2 >= fec_1)
            //            {

            //                //Validacion Cant Dias 05.09.2019 Jaime Marchant B
            //                DateTime fechaUno = Convert.ToDateTime(fec_1);
            //                DateTime fechaDos = Convert.ToDateTime(fec_2);

            //                //Validacion entre Meses Marzo a Noviembre 05.09.2019 Jaime Marchant B
            //                string cadena = fec_1.ToString("ddMMyyyy");
            //                string cadena2 = fec_2.ToString("ddMMyyyy");

            //                int tam_var = cadena.Length;
            //                int Var_Sub = Convert.ToInt32(cadena.Substring((tam_var - 6), 2));

            //                int tam_var2 = cadena2.Length;
            //                int Var_Sub2 = Convert.ToInt32(cadena2.Substring((tam_var2 - 6), 2));


            //                double dias = CalcularDiasDeDiferencia(fechaUno, fechaDos);
            //                int dmax = 10;
            //                if (dias >= dmax)
            //                {
            //                    if (Var_Sub >= 03 && Var_Sub2 <= 11)
            //                    {
            //                        SolicitudVaca.SolictudVacaciones(Solicit, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        if (SolicitudVaca.Objmensaje.Count > 0)
            //                        {
            //                            for (int i = 0; i < SolicitudVaca.Objmensaje.Count; i++)
            //                            {
            //                                if (SolicitudVaca.Objmensaje[i].codigo == "I")
            //                                {

            //                                    ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                    ViewBag.Mostrar = true;
            //                                    ViewBag.ocultar2 = true; //uso de dias
            //                                    ViewBag.ocultar = false;
                                                                                            
            //                                    VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                    ViewBag.Milistado3 = VacacionesService.ObjTipo;

            //                                    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                    ViewBag.dias = VacacionesService.ObtieneDias();
            //                                    VacacionesService.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.DropSoli2 = VacacionesService.ObjTipo;
            //                                    VacacionesService.ObtieneDiasPermi();
            //                                    ViewBag.MiPermi = VacacionesService.ObjPerAdmi;

            //                                    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                    ViewBag.dias = VacacionesService.ObtieneDias();
            //                                    ViewBag.NombreColabor = NombreColabor;

            //                                    TempData["Mensaje"] = ViewBag.Mensaje;
            //                                    TempData["RutColabor"] = RutColabor;
            //                                    TempData["NombreColabor"] = NombreColabor;
            //                                }
            //                                else
            //                                {
            //                                    ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                    ViewBag.Mostrar = true;
            //                                    ViewBag.ocultar = true;
            //                                    ViewBag.ocultar2 = false;

            //                                    VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                    ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                    ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                                    VacacionesService2.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                    ViewBag.DropSoli2 = VacacionesService2.ObtieneDias();
            //                                    VacacionesService2.ObtieneDiasPermi();
            //                                    ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;
            //                                    ViewBag.NombreColabor = NombreColabor;


            //                                    //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                    ViewBag.dias = VacacionesService.ObtieneDias();
            //                                }

            //                            }
            //                        }
            //                        else
            //                        {
            //                            VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                            ViewBag.Milistado = VacacionesService.ObjSalida;
            //                            ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                            ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                            VacacionesService.ObtenerVacaciones7001(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                            ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                            VacacionesService.ObtieneDiasPermi();
            //                            ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                            ViewBag.ocultar = false;
            //                            ViewBag.NombreColabor = NombreColabor;


            //                            //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                            ViewBag.dias = VacacionesService.ObtieneDias();

            //                        }
            //                    }
            //                    else
            //                    {
            //                        ViewBag.Mensaje = "la Fecha Seleccionadas Deben Estar entre los meses de Marzo y Noviembre";
            //                        ViewBag.Mostrar = true;
            //                    }

            //                }
            //                else
            //                {
            //                    SolicitudVaca.SolictudVacaciones(Solicit, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                    if (SolicitudVaca.Objmensaje.Count > 0)
            //                    {
            //                        for (int i = 0; i < SolicitudVaca.Objmensaje.Count; i++)
            //                        {
            //                            if (SolicitudVaca.Objmensaje[i].codigo == "I")
            //                            {

            //                                ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                ViewBag.Mostrar = true;
            //                                ViewBag.ocultar2 = false; //uso de dias
            //                                ViewBag.ocultar = true;


            //                                VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                ViewBag.Milistado3 = VacacionesService.ObjTipo;

            //                                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                ViewBag.dias = VacacionesService.ObtieneDias();
            //                                VacacionesService2.ObtenerVacaciones7001(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                ViewBag.DropSoli2 = VacacionesService2.ObjTipo;
            //                                VacacionesService2.ObtieneDiasPermi();
            //                                ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;
            //                                ViewBag.NombreColabor = NombreColabor;

            //                                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                ViewBag.dias = VacacionesService.ObtieneDias();

            //                            }
            //                            else
            //                            {
            //                                ViewBag.Mensaje = SolicitudVaca.Objmensaje[i].mensaje;
            //                                ViewBag.Mostrar = true;
            //                                ViewBag.ocultar = true;
            //                                ViewBag.ocultar2 = false;

            //                                VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                                VacacionesService2.ObtenerVacaciones7001(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                                //ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                                VacacionesService2.ObtieneDiasPermi();
            //                                ViewBag.MiPermi = VacacionesService2.ObjPerAdmi;
            //                                ViewBag.NombreColabor = NombreColabor;


            //                                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                                ViewBag.dias = VacacionesService.ObtieneDias();
            //                            }

            //                        }
            //                    }
            //                    else
            //                    {
            //                        VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.Milistado = VacacionesService.ObjSalida;
            //                        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                        VacacionesService.ObtenerVacaciones7001(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                        VacacionesService.ObtieneDiasPermi();
            //                        ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                        ViewBag.NombreColabor = NombreColabor;
            //                        ViewBag.ocultar = false;


            //                        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                        ViewBag.dias = VacacionesService.ObtieneDias();

            //                    }
            //                }
            //            }
            //            else
            //            {
            //                VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                VacacionesService.ObtieneDiasPermi();
            //                ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                ViewBag.NombreColabor = NombreColabor;


            //                //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                ViewBag.dias = VacacionesService.ObtieneDias();
            //                ViewBag.Mensaje = "Fecha inicial de vacaciones debe ser mayor a la fecha hasta.";
            //                ViewBag.Mostrar = true;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }

            //    }
            //    else /* Add Jaime Marchant 26.07.2019*/
            //    {

            //        List<SolicitudPermisos> SolicitPermi = new List<SolicitudPermisos>();
            //        SolicitudPermisos p_SolicitPermi;

            //        VacacionesService SolicitudPermi = new VacacionesService();

            //        NombreUsuario = (string)(Session["NombreUsuario"]);
            //        Usuario = (string)(Session["usuario"]);
            //        Usuario2 = string.Empty;


            //        var dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            //        var date = DateTime.ParseExact(FecDesde, "dd/MM/yyyy", dateTimeFormat);
            //        var date2 = DateTime.ParseExact(FecHasta, "dd/MM/yyyy", dateTimeFormat);

            //        DateTime fec_1 = Convert.ToDateTime(date);
            //        DateTime fec_2 = Convert.ToDateTime(date2);
            //        DateTime fec_3 = Convert.ToDateTime(fech_d);


            //        fech_d = string.Format("{0:d/M/yyyy}", fechadia);


            //        if (Value == "3010")
            //        {
            //            //Obtiene codigo Solicitud Permiso 3010-3012
            //            string ValueServ = Request.Form["DropPerm"];
            //            //Llena Estructura 

            //            p_SolicitPermi = new SolicitudPermisos();
            //            p_SolicitPermi.BEGDA = fec_1.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.ENDDA = fec_2.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.SUBTY = ValueServ;
            //            p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.RUT_CREA = Usuario;
            //            p_SolicitPermi.RUT_EMPLEA = RutColabor;
            //            SolicitPermi.Add(p_SolicitPermi);
            //        }
            //        else
            //        {
            //            //Llena Estructura 
            //            p_SolicitPermi = new SolicitudPermisos();
            //            p_SolicitPermi.BEGDA = fec_1.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.ENDDA = fec_2.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.SUBTY = Value;
            //            p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //            p_SolicitPermi.RUT_CREA = Usuario;
            //            p_SolicitPermi.RUT_EMPLEA = RutColabor;
            //            SolicitPermi.Add(p_SolicitPermi);
            //        }

            //        if (fec_2 >= fec_1)
            //        {

            //            SolicitudPermi.SolictudPermisos(SolicitPermi, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //            if (SolicitudPermi.Objmensaje.Count > 0)
            //            {
            //                for (int i = 0; i < SolicitudPermi.Objmensaje.Count; i++)
            //                {
            //                    ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //                    //Code Error E = 001
            //                    if (SolicitudPermi.Objmensaje[i].codigo == "E")
            //                    {
            //                        ViewBag.Mostrar = true;
            //                        ViewBag.ocultar2 = false;
            //                        ViewBag.ocultar = true;


            //                        VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.Milistado = VacacionesService.ObjSalida;
            //                        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                        ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                        VacacionesService.ObtieneDiasPermi();
            //                        ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                        ViewBag.NombreColabor = NombreColabor;

            //                        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                        ViewBag.dias = VacacionesService.ObtieneDias();

            //                        TempData["Mensaje"] = ViewBag.Mensaje;
            //                        TempData["RutColabor"] = RutColabor;
            //                        TempData["NombreColabor"] = NombreColabor;
            //                    }
            //                    else
            //                    {
            //                        ViewBag.Mostrar = true;
            //                        ViewBag.ocultar2 = false;
            //                        ViewBag.ocultar = true;


            //                        VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                        ViewBag.Milistado = VacacionesService.ObjSalida;
            //                        ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                        ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //                        ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //                        VacacionesService.ObtieneDiasPermi();
            //                        ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //                        ViewBag.NombreColabor = NombreColabor;

            //                        //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //                        ViewBag.dias = VacacionesService.ObtieneDias();


            //                        TempData["Mensaje"] = ViewBag.Mensaje;
            //                        TempData["RutColabor"] = RutColabor;
            //                        TempData["NombreColabor"] = NombreColabor;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //            ViewBag.Milistado = VacacionesService.ObjSalida;
            //            ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //            ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //            ViewBag.DropSoli2 = VacacionesService.ObtieneDias();
            //            VacacionesService.ObtieneDiasPermi();
            //            ViewBag.MiPermi = VacacionesService.ObjPerAdmi;
            //            ViewBag.NombreColabor = NombreColabor;

            //            //Obtiene Dias Para vacaciones sin uso de dias Abs 2100
            //            ViewBag.dias = VacacionesService.ObtieneDias();

            //            ViewBag.Mensaje = "Fecha inicial de vacaciones debe ser mayor a la fecha hasta.";
            //            ViewBag.Mostrar = true;
            //        }
            //    }
            //}
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

                var InformColaborador = new InformacionColaborador();
                List<string> lista = new List<string>();
                lista = InformColaborador.ListaCertificado();

                ViewBag.Tipos = lista;
                ViewBag.control = "0";
                ViewBag.mensaje = "";

                List<string> lista2 = new List<string>();
                lista2 = InformColaborador.ListaMotivo();

                ViewBag.Motivos = lista2;
                ViewBag.control = "0";
                ViewBag.mensaje = "";

                //ViewBag.SelectedValue = Value;


            }
            catch (Exception ex) { }
            return View();
        }


        public ActionResult Antiguedad(FormCollection form)
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

                //ViewBag.liquidacion = "../Certificados/FileLiquidacion.ashx?Rut=" + Usuario + "&Mes=" + MES + "&Anio=" + ANIO + "&Usuario=" + Session["usuarioSap"].ToString() + "&Clave=" + Session["contrasenaSap"].ToString() + "";
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
        public ActionResult VerSolicitud(string Id, string Fec1, string Fec2, string Dias)
        {
            //try
            //{
            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    Usuario = formatearRut(Usuario);

            //    string dir = Server.MapPath("/");
            //    string newDoc = "SolicitudVacaciones_" + Usuario + ".pdf";
            //    string newFile = dir + @"\Certificados\" + newDoc;

            //    // Create a Document object
            //    var document = new Document(PageSize.A4, 60, 60, 50, 25);

            //    // Create a new PdfWriter object, specifying the output stream
            //    var output = new FileStream(newFile, FileMode.Create);
            //    var writer = PdfWriter.GetInstance(document, output);

            //    // Open the Document for writing
            //    document.Open();

            //    string nombre_logo = "logo.png";
            //    string logo_emp = dir + @"\images\" + nombre_logo;
            //    var logo = iTextSharp.text.Image.GetInstance(logo_emp);
            //    logo.ScalePercent(50f);
            //    logo.SetAbsolutePosition(60, 760);



            //    document.Add(logo);

            //    var titleFont = FontFactory.GetFont("Arial", 13, Font.BOLD);

            //    string txtTipoSolicitud = string.Empty;

            //    txtTipoSolicitud = "FERIADO LEGAL";



            //    PdfContentByte cb = writer.DirectContent;
            //    cb.Rectangle(50f, 450f, 500f, 350f);
            //    Paragraph titulo = new Paragraph("COMPROBANTE DE " + txtTipoSolicitud, titleFont);
            //    titulo.IndentationLeft = 130;
            //    document.Add(titulo);


            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));

            //    Font arial = FontFactory.GetFont("Arial", 13f);
            //    Font arial2 = FontFactory.GetFont("Arial", 11f);

            //    Chunk beginning = new Chunk("Nombre : " + NombreUsuario, arial);
            //    Phrase p1 = new Phrase(beginning);

            //    Paragraph p = new Paragraph();
            //    p.Alignment = Element.ALIGN_LEFT;
            //    p.Add(p1);
            //    document.Add(p);

            //    string line2 = "Rut : " + Usuario;
            //    Chunk beginning2 = new Chunk(line2, arial);


            //    string line3;

            //    line3 = "";


            //    Chunk beginning3 = new Chunk(line3, arial);
            //    Phrase p3 = new Phrase(beginning3);
            //    Phrase p2 = new Phrase(beginning2);
            //    Paragraph p_2 = new Paragraph();
            //    p_2.Alignment = Element.ALIGN_LEFT;
            //    p_2.Add(p2);
            //    p_2.Add(p3);
            //    document.Add(p_2);

            //    document.Add(new Paragraph("\n"));

            //    Chunk beginning4;

            //    beginning4 = new Chunk("Declaro que a partir de las fechas indicadas, haré uso de mi período de feriado legal", arial2);


            //    Phrase p4 = new Phrase(beginning4);
            //    Paragraph p_4 = new Paragraph();
            //    p_4.Alignment = Element.ALIGN_LEFT;
            //    p_4.SpacingAfter = Element.ALIGN_MIDDLE;
            //    p_4.SetLeading(3, 1);
            //    p_4.Add(p4);
            //    document.Add(p_4);

            //    string fec1 = string.Format("{0:d/M/yyyy}", Convert.ToDateTime(Fec1));
            //    string fec2 = string.Format("{0:d/M/yyyy}", Convert.ToDateTime(Fec2));


            //    string line4;
            //    line4 = "Inicio :" + fec1;

            //    Chunk beginning5 = new Chunk(line4, arial);

            //    string line5 = "";
            //    line5 = "                          Término :" + fec2;


            //    Chunk beginning6 = new Chunk(line5, arial);
            //    Phrase p5 = new Phrase(beginning5);
            //    Phrase p6 = new Phrase(beginning6);
            //    Paragraph p_5 = new Paragraph();
            //    p_5.Alignment = Element.ALIGN_LEFT;
            //    p_5.Add(p5);
            //    p_5.Add(p6);
            //    document.Add(p_5);

            //    document.Add(new Paragraph("\n"));
            //    Chunk beginning7 = new Chunk("Cantidad de días" + " :" + Dias, arial);
            //    Phrase p7 = new Phrase(beginning7);
            //    Paragraph p_6 = new Paragraph();
            //    p_6.Alignment = Element.ALIGN_LEFT;
            //    p_6.Add(p7);
            //    document.Add(p_6);

            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));

            //    Chunk beginning8 = new Chunk("  ________________                                ________________", arial);
            //    Phrase p8 = new Phrase(beginning8);
            //    Paragraph p_7 = new Paragraph();
            //    p_7.Alignment = Element.ALIGN_CENTER;
            //    p_7.Add(p8);
            //    document.Add(p_7);

            //    Chunk beginning9 = new Chunk("   Firma Jefatura                                         Firma Trabajador", arial);
            //    Phrase p9 = new Phrase(beginning9);
            //    Paragraph p_8 = new Paragraph();
            //    p_8.Alignment = Element.ALIGN_CENTER;
            //    p_8.Add(p9);
            //    document.Add(p_8);

            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    document.Add(new Paragraph("\n"));
            //    cb.Stroke();

            //    document.Close();

            //    ViewBag.vercomprobante = "../Certificados/FileCS.ashx?Id=" + newFile;

            //}
            //catch (Exception ex)
            //{

            //    ViewBag.Mensaje = ex.Message.ToString();
            //    ViewBag.Mostrar = true;
            //}
            return PartialView("VerSolicitud");

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
            //try
            //{
            //    if (verificaSesion() == true)
            //    {
            //        return RedirectToAction("Login", "LOGIN", new { });
            //    }

            //    string Usuario = (string)(Session["usuario"]);
            //    Usuario = formatearRut(Usuario);

            //    string dir = Server.MapPath("/");
            //    string newDoc = "SolicitudVacaciones_" + Usuario + ".pdf";
            //    string newFile = dir + @"\Certificados\" + newDoc;

            //    if (System.IO.File.Exists(newFile))
            //    {
            //        try
            //        {
            //            System.IO.File.Delete(newFile);
            //        }
            //        catch (System.IO.IOException e)
            //        {
            //            Console.WriteLine(e.Message);
            //        }
            //    }
            //}
            //catch (Exception ex) { }
            return RedirectToAction("MisSolicitudes", "HOME");
        }

        [HttpGet]  //Solictud Revision Mi Equipo 01.08.2019
        public ActionResult SolicitudRevisionEquipo()
        {

        //    string NombreUsuario =  (string)(Session["NombreUsuario"]);
        //    string Usuario = (string)(Session["usuario"]);

        //    InformacionColaborador Colaboradores = new InformacionColaborador();
        //    Colaboradores.ObtenerDatosJerarquia(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

        //    ViewBag.ListaColaborades = Colaboradores.ObjSalida;

        //    for (int i = 0; i < Colaboradores.ObjSalida.Count; i++)
        //    {
        //        ViewBag.RutColabor = Colaboradores.ObjSalida[i].ICNUM;
        //        ViewBag.NombreColabor = Colaboradores.ObjSalida[i].VORNA;
        //    }

        //if (Request["Mensaje"] != null)
        //    {
        //        string Mensaje = (Request["Mensaje"]);
        //        ViewBag.Mensaje = Mensaje;
        //        //Mostrar Div Mensaje 
        //       ViewBag.Mostrar = true;
        //    }
    
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

            //InformacionColaborador ObtenerReporte = new InformacionColaborador();
            //ViewBag.Milistado = ObtenerReporte.ObtenerDatosAbsentismo(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            return View();
        }


        [HttpPost]
        public ActionResult SolitsinusodiasMiEquipo(string RutColabor, string NombreColabor, FormCollection fomr)
        {
            //try
            //{
            //    List<SolicitudPermisos> SolicitPermi = new List<SolicitudPermisos>();
            //    SolicitudPermisos p_SolicitPermi;

            //    var VacacionesService = new VacacionesService();
            //    VacacionesService Solicitudhist = new VacacionesService();

            //    DateTime fechHoy = DateTime.Now;
               
            //    string fechadia = Convert.ToString(fechHoy);

            //    string Value = Request.Form["DropSoli2"];
            //    string valueDias = Request.Form["DropDias"];

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    string Usuario2 = string.Empty;

            //    string fech_d = string.Format("{0:d/M/yyyy}", fechadia);
            //    DateTime fec_3 = Convert.ToDateTime(fech_d);

            //    //Obtiene ultima fecha de vacaciones
            //    if (RutColabor != null)
            //    {
            //        VacacionesService.ObtenerVacacionesHistorico(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    }
            //    else
            //    {
            //        VacacionesService.ObtenerVacacionesHistorico(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    }
               
             
            //    for (int i = 0; i < VacacionesService.ListaVacacionesHistorico.Count; i++)
            //    {
            //        if (i==0)
            //        {
            //             fecUltSoli = VacacionesService.ListaVacacionesHistorico[i].ENDDA;
            //        }                       
            //    }
                
            //    var dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;

            //    if (valueDias == "1")
            //    {
            //        fec_1 = Convert.ToDateTime(fecUltSoli).AddDays(0);

            //        //Llena Estructura 
            //        p_SolicitPermi = new SolicitudPermisos();
            //        p_SolicitPermi.BEGDA = fecUltSoli.Replace("-", "/");
            //        p_SolicitPermi.ENDDA = fec_1.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.SUBTY = Value;
            //        p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.RUT_CREA = Usuario;
            //        p_SolicitPermi.RUT_EMPLEA = RutColabor;
            //        SolicitPermi.Add(p_SolicitPermi);
            //    }
            //    else
            //    {
            //        //var date2 = DateTime.ParseExact(fecUltSoli, "dd/MM/yyyy", dateTimeFormat);
            //        fec_2 = Convert.ToDateTime(fecUltSoli).AddDays(1);
            //        //Llena Estructura 
            //        p_SolicitPermi = new SolicitudPermisos();
            //        p_SolicitPermi.BEGDA = fecUltSoli.Replace("-", "/");
            //        p_SolicitPermi.ENDDA = fec_2.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.SUBTY = Value;
            //        p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.RUT_CREA = Usuario;
            //        p_SolicitPermi.RUT_EMPLEA = RutColabor;
            //        SolicitPermi.Add(p_SolicitPermi);

            //    }

            //    VacacionesService SolicitudPermi = new VacacionesService();

            //    SolicitudPermi.SolictudPermisos(SolicitPermi, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //    if (SolicitudPermi.Objmensaje.Count > 0)
            //    {
            //        for (int i = 0; i < SolicitudPermi.Objmensaje.Count; i++)
            //        {
            //            ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //            //Code Error E = 001
            //            if (SolicitudPermi.Objmensaje[i].codigo == "E")
            //            {
            //                ViewBag.Mostrar = true;
            //                ViewBag.ocultar2 = true; //uso de dias

            //                ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //                VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //            }
            //            else
            //            {
            //                ViewBag.ocultar2 = false; //uso de dias
            //                ViewBag.Mostrar = true;

            //                ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //                VacacionesService.ObtenerVacaciones(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}



            TempData["Mensaje"] = ViewBag.Mensaje;
            TempData["RutColabor"] = RutColabor;
            TempData["NombreColabor"] = NombreColabor;
            return RedirectToAction("SolicitudMiEquipo2", "HOME");
            //return RedirectToAction("SolicitudMiEquipo2", "HOME", new { Mensaje = ViewBag.Mensaje });
        }

        [HttpPost]
        public ActionResult Solitsinusodias(string RutColabor, FormCollection fomr)
        {
            //try
            //{
            //    List<SolicitudPermisos> SolicitPermi = new List<SolicitudPermisos>();
            //    SolicitudPermisos p_SolicitPermi;

            //    var VacacionesService = new VacacionesService();
            //    VacacionesService Solicitudhist = new VacacionesService();

            //    DateTime fechHoy = DateTime.Now;

            //    string fechadia = Convert.ToString(fechHoy);

            //    string Value = Request.Form["DropSoli2"];
            //    string valueDias = Request.Form["DropDias"];

            //    string NombreUsuario = (string)(Session["NombreUsuario"]);
            //    string Usuario = (string)(Session["usuario"]);
            //    string Usuario2 = string.Empty;

            //    string fech_d = string.Format("{0:d/M/yyyy}", fechadia);
            //    DateTime fec_3 = Convert.ToDateTime(fech_d);

            //    //Obtiene ultima fecha de vacaciones
            //    if (RutColabor != null)
            //    {
            //        VacacionesService.ObtenerVacacionesHistorico(RutColabor, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    }
            //    else
            //    {
            //        VacacionesService.ObtenerVacacionesHistorico(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //    }


            //    for (int i = 0; i < VacacionesService.ListaVacacionesHistorico.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            fecUltSoli = VacacionesService.ListaVacacionesHistorico[i].ENDDA;
            //        }
            //    }

            //    var dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;

            //    if (valueDias == "1")
            //    {
            //        fec_1 = Convert.ToDateTime(fecUltSoli).AddDays(0);

            //        //Llena Estructura 
            //        p_SolicitPermi = new SolicitudPermisos();
            //        p_SolicitPermi.BEGDA = fecUltSoli.Replace("-", "/");
            //        p_SolicitPermi.ENDDA = fec_1.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.SUBTY = Value;
            //        p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.RUT_CREA = Usuario;
            //        p_SolicitPermi.RUT_EMPLEA = Usuario;
            //        SolicitPermi.Add(p_SolicitPermi);
            //    }
            //    else
            //    {
            //        //var date2 = DateTime.ParseExact(fecUltSoli, "dd/MM/yyyy", dateTimeFormat);
            //        fec_2 = Convert.ToDateTime(fecUltSoli).AddDays(1);
            //        //Llena Estructura 
            //        p_SolicitPermi = new SolicitudPermisos();
            //        p_SolicitPermi.BEGDA = fecUltSoli.Replace("-", "/");
            //        p_SolicitPermi.ENDDA = fec_2.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.SUBTY = Value;
            //        p_SolicitPermi.AEDTM = fec_3.ToString("dd/MM/yyyy");
            //        p_SolicitPermi.RUT_CREA = Usuario;
            //        p_SolicitPermi.RUT_EMPLEA = Usuario;
            //        SolicitPermi.Add(p_SolicitPermi);

            //    }

            //    VacacionesService SolicitudPermi = new VacacionesService();

            //    SolicitudPermi.SolictudPermisos(SolicitPermi, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

            //    if (SolicitudPermi.Objmensaje.Count > 0)
            //    {
            //        for (int i = 0; i < SolicitudPermi.Objmensaje.Count; i++)
            //        {
            //            ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //            //Code Error E = 001
            //            if (SolicitudPermi.Objmensaje[i].codigo == "E")
            //            {
            //                ViewBag.Mostrar = true;
            //                ViewBag.ocultar2 = true; //uso de dias

            //                ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;

            //                VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //            }
            //            else
            //            {
            //                ViewBag.ocultar2 = false; //uso de dias
            //                ViewBag.Mostrar = true;

            //                ViewBag.Mensaje = SolicitudPermi.Objmensaje[i].mensaje;
             
            //                VacacionesService.ObtenerVacaciones(Usuario, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());
            //                ViewBag.Milistado = VacacionesService.ObjSalida;
            //                ViewBag.Milistado2 = VacacionesService.Objmensaje;
            //                ViewBag.Milistado3 = VacacionesService.ObjTipo;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            TempData["Mensaje"] = ViewBag.Mensaje;
            return RedirectToAction("Solicitud", "HOME");
        }

        [HttpPost]
        public ActionResult Enviar(string RutColabor, string ACCION, FormCollection fomr)
        {
          //  try
          //  {
                
          //      //string Mensajefinal = Mensaje;
          //      string NombreUsuario = (string)(Session["NombreUsuario"]);
          //      string Usuario = (string)(Session["usuario"]);
          //      string Value = "9999";

          //      DateTime fechHoy = DateTime.Now;

          //      string fechadia = Convert.ToString(fechHoy);
          //      string fech_d = string.Format("{0:d/M/yyyy}", fechadia);

          //      DateTime fec_3 = Convert.ToDateTime(fech_d);

          //      // Formateo RUT Colaborador
          ////      string Colaborador = RutColabor.Replace(".", "");

          //      VacacionesService RevisionEquipo = new VacacionesService();

          //      List<EnvioMailRevisionEquipo> SolicitRevision = new List<EnvioMailRevisionEquipo>();
          //      List<ZEHR045> SolicitColaborador = new List<ZEHR045>();
          //      EnvioMailRevisionEquipo p_SolicitRevision;
          //      ZEHR045 p_colaboradores;

          //      NombreUsuario = (string)(Session["NombreUsuario"]);
          //      Usuario = (string)(Session["usuario"]);

          //      VacacionesService EnvioMail = new VacacionesService();

          //      string ValueMensaje = Request.Form["Mensaje"];
          //      string RUT = Request.Form["RUT"];


          //      //Llena Estructura 
          //      p_SolicitRevision = new EnvioMailRevisionEquipo();
          //      p_SolicitRevision.AEDTM = fec_3.ToString("dd/MM/yyyy");
          //      p_SolicitRevision.RUT_CREA = Usuario;
          //      p_SolicitRevision.ACTION = ACCION;
          //      p_SolicitRevision.ZTEXTO = ValueMensaje;
          //      SolicitRevision.Add(p_SolicitRevision);

                

          //      //Completa Estructura cuando es Agregar personal
          //      if (RutColabor != null)
          //      {
          //          p_colaboradores = new ZEHR045();
          //          p_colaboradores.RUT_EMPLEA = RutColabor;
          //          p_colaboradores.PERNR = "";
          //          p_colaboradores.ENAME = "";
          //          SolicitColaborador.Add(p_colaboradores);
          //      }
          //      else
          //      {
          //          //Completa Estructura cuando es mas de un colaborador
          //          foreach (var key in fomr.AllKeys)
          //          {
          //              if (key == "check")
          //              {
          //                  var value = fomr[key.ToString()];
          //                  results = value.Split(',');
          //              }
          //          }
          //          foreach (string valor in results)
          //          {
          //              if (valor != "false")
          //              {
          //                  p_colaboradores = new ZEHR045();
          //                  p_colaboradores.RUT_EMPLEA = valor;
          //                  p_colaboradores.PERNR = "";
          //                  p_colaboradores.ENAME = "";
          //                  SolicitColaborador.Add(p_colaboradores);
          //              }
          //          }
          //      }

          //      EnvioMail.EnvioMailRevisionEquipo(SolicitRevision, SolicitColaborador, Session["usuarioSap"].ToString(), Session["contrasenaSap"].ToString());

          //      if (EnvioMail.Objmensaje.Count > 0)
          //      {
          //          for (int i = 0; i < EnvioMail.Objmensaje.Count; i++)
          //          {
          //              ViewBag.Mensaje = EnvioMail.Objmensaje[i].mensaje;
          //          }

          //          ViewBag.Mostrar = true;
          //      }
          //      else
          //      {

          //      }
          //  }
          //  catch (Exception ex)
          //  {

          //  }
            return RedirectToAction("SolicitudRevisionEquipo", "HOME", new { Mensaje = ViewBag.Mensaje });
        }

    }
}