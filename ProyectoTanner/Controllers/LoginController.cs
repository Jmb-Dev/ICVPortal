using Newtonsoft.Json;
using ProyectoTanner.Models;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;


namespace ProyectoTanner.Controllers
{
    public class LoginController : Controller
    {
        //Declaracion Variables
        string Jefe = "JEFE";//string.Empty;
        string NombreEmpleado = string.Empty;
        string ApellidoEmpleado = string.Empty;
        string urlDomain = "http://localhost:5749/";

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            ConexionDBA db = new ConexionDBA();
            db.Conexion();
            //string pernr = db.get_user(name, password);

            TwoStringValue st = new TwoStringValue();
            st = db.Get_user2(name, password);
           
            if (st.str1 != "Error")
            {
                ApiController api = new ApiController();
                string apikey = api.solicitarToken();

                string url_master = "http://164.77.177.179:5055/api/ZHR_DAT_MAE?RUT=";
                var rut = st.str1;
                var url_2 = url_master + rut;
                string ke2 = "Bearer " + apikey;

                var json2 = new WebClient();
                json2.Headers.Add("Authorization", ke2);
                var n = json2.DownloadString(url_2);
                string jefe2 = "";
                var m = JsonConvert.DeserializeObject<RootObject>(n);
                Session["UserWeb"] = st.str2;
                foreach (var per in m.PERSONALES)
                {
                    var user = per.VORNA + " " + per.NACHN;
                    Session["NombreUsuario"] = user;
                    Session["Usuario"] = st.str1;
                    Session["Token"] = apikey;
                    Session["SessionActiva"] = "X";
                    Session["FechaIng"] = per.FECIN;
                    Session["RUT"] = rut;
                    Session["UnidadOrg"] = per.ORGEH;  //descomentar
                    Session["Pernr"] = per.PERNR;  //descomentar
                    jefe2 = per.TEXT2;
                }
                Session["UsuarioAprobador"] = jefe2.Substring(0, 4);
                var xxx = jefe2.Substring(0, 4);
                var fecha = (String)(Session["FechaIng"]);

                if (Session["UsuarioAprobador"] != "")
                {
                    if (jefe2.Substring(0, 4) != Jefe)
                    {
                        Session["UsuarioAprobador"] = "";
                    }

                }
                Session["Correo"] = name;


            }
            return RedirectToAction("Index", "Home");
        }
    
        [HttpGet]
        public ActionResult StartRecovery()
        {
            Models.ViewModel.RecoveryViewModel model = new Models.ViewModel.RecoveryViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult StartRecovery(Models.ViewModel.RecoveryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = GetSHA256(Guid.NewGuid().ToString());
                using (Models.qhrcl_icvPortalEntities1 db = new Models.qhrcl_icvPortalEntities1())
                {
                    var oUser = db.usuarios.Where(d => d.correo == model.Email).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.token = token;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        EnviarCorreo(oUser.correo, token);
                    }
                }
                return View("Login");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Recovery(string token)
        {
            Models.ViewModel.RecoveryPassViewModel model = new Models.ViewModel.RecoveryPassViewModel();
            model.Token = token;
            using (Models.qhrcl_icvPortalEntities1 db = new Models.qhrcl_icvPortalEntities1())
            {
                if (token == null || token.Trim().Equals(""))
                {
                    ViewBag.Error = "Su Link ha Expirado";
                    return View("Login");
                }
                var oUser = db.usuarios.Where(d => d.token == model.Token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Su Link ha Expirado";
                    return View("Login");
                }
            }
                model.Token = token;
            return View(model);
        }
        [HttpPost]
        public ActionResult Recovery(Models.ViewModel.RecoveryPassViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (Models.qhrcl_icvPortalEntities1 db = new Models.qhrcl_icvPortalEntities1())
                {
                    var oUser = db.usuarios.Where(d => d.token == model.Token).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.clave = model.Password;
                        oUser.token = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(model);
                //throw new Exception(ex.Message);
            }
            ViewBag.Message = "Contraseña modificada con éxito";
            return View("Login");
        }
        #region Helpers

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void EnviarCorreo(string correo, string token)
        {
            ApiController api = new ApiController();
            string Url = urlDomain + "/Login/Recovery/?token="+ token;
            string token1 = api.solicitarToken();
            string mensaje = "<p>Correo para Recuperar Contraseña</p>";
            mensaje += "<br>";
            mensaje += "<a href='";
            mensaje += Url;
            mensaje += "'>Click para Recuperar</a>";
            string asunto = "Recuperar Contraseña PortalAutoservicio ICV";
            api.EnvioMail(token1, mensaje, asunto, correo);
        }
        
        #endregion
    }
}