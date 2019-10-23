using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;


namespace ProyectoTanner.Controllers
{
    public class ConexionBDA : ApiController
    {
        string conn = ConfigurationManager.ConnectionString["DBA_ICV"].ConnectionString;
        SqlConnection sql = new SqlConnection(conn);
    }



       
       
}