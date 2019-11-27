using System.Web.Mvc;
//using SAP.Middleware.Connector;

namespace ProyectoTanner.Controllers
{
    public class ConexionController : Controller
    {
        public string server { get; set; }
        public string instancia { get; set; }
        public string idSistema { get; set; }
        public string sapRouter { get; set; }
        public string mandante { get; set; }
        public string user { get; set; }
        public string paswr { get; set; }
        public string idioma { get; set; }
        //public RfcConfigParameters connectorConfig { get; set; }
        //Dictionary<string, RfcConfigParameters> availableDestinations;
        //RfcDestinationManager.ConfigurationChangeHandler changeHandler;

        public string ToJson()
        {
            System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer =
                        new System.Web.Script.Serialization.JavaScriptSerializer();

            return jsonSerializer.Serialize(this);
        }


        //public RfcConfigParameters SAPConector(string user, string pass)
        //{
        //    //Globales conn = new Globales();
        //    //string p_user = conn.GetUser();
        //    //string p_pass = conn.GetPass();

        //    //Globales conn = new Globales();
        //    string p_user = user;
        //    string p_pass = pass;

        //    //RfcConfigParameters SapConnector = new RfcConfigParameters();

        //    //SapConnector.Add(RfcConfigParameters.Name, "DES");
        //    //SapConnector.Add(RfcConfigParameters.AppServerHost, server);
        //    //SapConnector.Add(RfcConfigParameters.SAPRouter, sapRouter);
        //    //SapConnector.Add(RfcConfigParameters.SystemNumber, instancia);
        //    //SapConnector.Add(RfcConfigParameters.User, p_user);
        //    //SapConnector.Add(RfcConfigParameters.Password, p_pass);
        //    //SapConnector.Add(RfcConfigParameters.Client, mandante);
        //    //SapConnector.Add(RfcConfigParameters.Language, "ES");
        //    //SapConnector.Add(RfcConfigParameters.PoolSize, "10");
        //    //SapConnector.Add(RfcConfigParameters.IdleTimeout, "10");

        //    //return SapConnector;

        //}


        public string connectionsSAP(string user, string pass)
        {
            string mensaje = null;

            try
            {
                //this.mandante = ConfigurationManager.AppSettings["mandante"];
                //this.server = ConfigurationManager.AppSettings["servidor"];
                //this.sapRouter = ConfigurationManager.AppSettings["saprouter"];
                //this.instancia = ConfigurationManager.AppSettings["num_sist"];
                //this.idSistema = ConfigurationManager.AppSettings["sysid"];
                //this.idioma = ConfigurationManager.AppSettings["lenguaje"];

                //connectorConfig = SAPConector(user, pass);

                //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(connectorConfig);
                //SapRfcDestination.Ping();

            }
            //catch (RfcLogonException ex)
            //{
            //    mensaje = ex.Message;
            //}
            //catch (RfcCommunicationException exp)
            //{
            //    mensaje = exp.Message;
            //}
            finally { }

            return mensaje;
        }


        public void RemoveDestination(string name)
        {
            //if (!string.IsNullOrEmpty(name) && availableDestinations.Remove(name))
            //{
            //    RfcConfigurationEventArgs eventArgs = new RfcConfigurationEventArgs(RfcConfigParameters.EventType.DELETED);
            //    changeHandler(name, eventArgs);
            //}
        }
    }
}