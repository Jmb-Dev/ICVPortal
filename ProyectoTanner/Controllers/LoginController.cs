using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoTanner.Services;
using System.Configuration;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;
using ProyectoTanner.Models;


namespace ProyectoTanner.Controllers
{
    public class LoginController : Controller
    {
        //Declaracion Variables
        string Jefe = string.Empty;
        string NombreEmpleado = string.Empty;
        string ApellidoEmpleado = string.Empty;

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name , string password)
        {
            string url = "http://164.77.177.179:5055/api/login/GetToken?Username=QHRPORTAL&Password=1234";
            string apikey = new WebClient().DownloadString(url) ;

            string url_master = "http://164.77.177.179:5055/api/ZHR_DAT_MAE?RUT=";
            var rut = name;
            var url_2 = url_master + rut;
            string ke2= "Bearer " + apikey;

            var json2 = new WebClient();
            json2.Headers.Add("Authorization", ke2);
            var n = json2.DownloadString(url_2);
    
            var m = JsonConvert.DeserializeObject<RootObject>(n);

            foreach (var per in m.PERSONALES)
            {
                var user = per.VORNA + " " + per.NACHN;
                Session["NombreUsuario"] = user;
                Session["Usuario"] = name;
                Session["Token"] = apikey;
                Session["SessionActiva"] = "X";
                Session["FechaIng"] = per.FECIN;
                Session["RUT"] = rut;
            }

            var fecha = (String)(Session["FechaIng"]);

            if (Jefe != "") { Session["UsuarioAprobador"] = Jefe; } else { Session["UsuarioAprobador"] = ""; }

            return RedirectToAction("Index", "Home");

        }
    }
}