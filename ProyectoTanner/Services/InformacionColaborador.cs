using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using SAP.Middleware.Connector;
using ProyectoTanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ProyectoTanner.Services
{
    public class InformacionColaborador
    {
        //    //public List<ColaboradresJerarquia> ObjSalida = new List<ColaboradresJerarquia>();
        //    //public List<Tablamensaje> Objmensaje = new List<Tablamensaje>();

        public List<PermisoAdministrativo> ObjPerAdmi = new List<PermisoAdministrativo>();
        public List<ListaDespegable> ObjMotivo = new List<ListaDespegable>();
        public List<ListaDespegable> ObjCtas = new List<ListaDespegable>();
        public List<ListaDespegable> ObjCertif = new List<ListaDespegable>();
        //    public List<InformacionColaboradores> ObtenerDatos(string RUT,string usuario, string contrasena)
        //    {
        //        IRfcTable lt_T_MENSAJE;

        //        IRfcStructure lt_E_OUTPUT;

        //        InformacionColaboradores SalidaDatosColab;

        //        ConexionController conexion = new ConexionController();

        //        List<InformacionColaboradores> ListaColaborador = new List<InformacionColaboradores>();

        //        string retorno = "ERROR";

        //        try
        //        {
        //            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
        //                retorno = conexion.connectionsSAP(usuario, contrasena);

        //            if (string.IsNullOrEmpty(retorno))
        //            {
        //                //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
        //                //RfcRepository SapRfcRepository = SapRfcDestination.Repository;

        //                //IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_DAT_MAE");

        //                //BapiGetUser.SetValue("I_RUT", RUT);

        //                //BapiGetUser.Invoke(SapRfcDestination);

        //                //lt_E_OUTPUT = BapiGetUser.GetStructure("E_OUTPUT");

        //                //SalidaDatosColab = new InformacionColaboradores();
        //                //SalidaDatosColab.VORNA = lt_E_OUTPUT[0].GetValue().ToString();
        //                //SalidaDatosColab.NACHN = lt_E_OUTPUT[1].GetValue().ToString();
        //                //SalidaDatosColab.NACH2 = lt_E_OUTPUT[2].GetValue().ToString();
        //                //SalidaDatosColab.JEFE  = lt_E_OUTPUT[9].GetValue().ToString();

        //                //ListaColaborador.Add(SalidaDatosColab);

        //                //for (int i = 0; i < lt_E_OUTPUT.Count(); i++)
        //                //{
        //                //    SalidaDatosColab = new InformacionColaboradores();
        //                //    SalidaDatosColab.VORNA = lt_E_OUTPUT[i].GetValue().ToString();
        //                //    SalidaDatosColab.NACHN = lt_E_OUTPUT[i].GetValue().ToString();
        //                //    SalidaDatosColab.NACH2 = lt_E_OUTPUT[i].GetValue().ToString();
        //                //    ListaColaborador.Add(SalidaDatosColab);
        //                //}
        //            }

        //            return ListaColaborador;
        //        }
        //        catch (Exception e)
        //        {
        //            System.Diagnostics.Debug.Write(e.StackTrace);
        //            throw new Exception();
        //        }
        //        finally
        //        {
        //            lt_T_MENSAJE = null;
        //            SalidaDatosColab = null;
        //            conexion = null;
        //        }
        //}



        //    public void ObtenerDatosJerarquia(string RUT, string usuario, string contrasena)
        //    {
        //        IRfcTable lt_T_MENSAJE;

        //        Tablamensaje T_SALIDA;

        //        IRfcTable lt_T_OUTPUT;

        //        ColaboradresJerarquia SalidaDatosColabJera;

        //        ConexionController conexion = new ConexionController();

        //        List<ColaboradresJerarquia> ListaColaboradorJera = new List<ColaboradresJerarquia>();

        //        List<Tablamensaje> ListaMensaje = new List<Tablamensaje>();

        //        string retorno = "ERROR";

        //        try
        //        {
        //            //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
        //            //    retorno = conexion.connectionsSAP(usuario, contrasena);

        //            if (string.IsNullOrEmpty(retorno))
        //            {
        //                //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
        //                //RfcRepository SapRfcRepository = SapRfcDestination.Repository;

        //                //IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_COL_JER");

        //                //    BapiGetUser.SetValue("I_RUT", RUT);

        //                //    BapiGetUser.Invoke(SapRfcDestination);

        //                //    lt_T_OUTPUT = BapiGetUser.GetTable("T_OUTPUT");

        //                //for (int i = 0; i < lt_T_OUTPUT.RowCount; i++)
        //                //{
        //                //    SalidaDatosColabJera = new ColaboradresJerarquia();
        //                //    SalidaDatosColabJera.ICNUM = lt_T_OUTPUT[i].GetString("ICNUM");
        //                //    SalidaDatosColabJera.VORNA = lt_T_OUTPUT[i].GetString("VORNA") + " " + lt_T_OUTPUT[i].GetString("NACHN") + " " + lt_T_OUTPUT[i].GetString("NACH2");
        //                //    SalidaDatosColabJera.ZTEXTPLANS = lt_T_OUTPUT[i].GetString("ZTEXTPLANS");
        //                //    SalidaDatosColabJera.ZTEXTUO = lt_T_OUTPUT[i].GetString("ZTEXTUO");
        //                //    ObjSalida.Add(SalidaDatosColabJera);
        //                //}


        //                //lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");

        //                //for (int i = 0; i < lt_T_MENSAJE.RowCount; i++)
        //                //{
        //                //    T_SALIDA = new Tablamensaje();
        //                //    T_SALIDA.codigo = lt_T_MENSAJE[i].GetString("CODIGO");
        //                //    T_SALIDA.mensaje = lt_T_MENSAJE[i].GetString("MENSAJE");
        //                //    Objmensaje.Add(T_SALIDA);
        //                //}
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            System.Diagnostics.Debug.Write(e.StackTrace);
        //            throw new Exception();
        //        }
        //        finally
        //        {
        //            lt_T_MENSAJE = null;
        //            SalidaDatosColabJera = null;
        //            ListaMensaje = null;
        //            conexion = null;
        //        }
        //    }

        public List<T_BINARY> ObtenerLiquidacion(string RUT, string MES, string ANIO, string usuario, string contrasena)
        {
            //        IRfcTable lt_T_MENSAJE;
            //        Tablamensaje T_SALIDA;
            //        IRfcTable  lt_E_T_BINARY;
            T_BINARY SalidaBinaria;
            //        ConexionController conexion = new ConexionController();

            List<T_BINARY> Listabinaria = new List<T_BINARY>();
            List<Tablamensaje> ListaMensaje = new List<Tablamensaje>();

            //string retorno = "ERROR";

            try
            {
                //            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //                retorno = conexion.connectionsSAP(usuario, contrasena);

                //            if (string.IsNullOrEmpty(retorno))
                //            {
                //                //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //                //RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //                //IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_LIQ_SUE");

                //                //BapiGetUser.SetValue("I_RUT", RUT);
                //                //BapiGetUser.SetValue("I_MES", MES);
                //                //BapiGetUser.SetValue("I_ANO", ANIO);

                //                //BapiGetUser.Invoke(SapRfcDestination);

                //                //lt_E_T_BINARY = BapiGetUser.GetTable("T_BINARY");

                //                //Byte[] bytes = (Byte[])BapiGetUser.GetValue("PDF_LIQUI");

                //                //for (int i = 0; i < lt_E_T_BINARY.Count; i++)
                //                //{

                //                //    //bytes = (Byte[])lt_E_T_BINARY[i].GetValue(0);
                //                //    //bytes = (Byte[])lt_E_T_BINARY[i].GetValue(0);

                //                //    SalidaBinaria = new T_BINARY();
                //                //    SalidaBinaria.LINE = (Byte[])lt_E_T_BINARY[i].GetValue(0);
                //                //    Listabinaria.Add(SalidaBinaria);
                //                //}


                //                //lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");

                //                //for (int i = 0; i < lt_T_MENSAJE.RowCount; i++)
                //                //{
                //                //    T_SALIDA = new Tablamensaje();
                //                //    T_SALIDA.codigo = lt_T_MENSAJE[i].GetString("CODIGO");
                //                //    T_SALIDA.mensaje = lt_T_MENSAJE[i].GetString("MENSAJE");
                //                //    ListaMensaje.Add(T_SALIDA);
                //                //}
                //            }



                //var url = "http://164.77.177.179:5055/api/ZHR_MF_LIQSUELD?";
                //var rut = "RUT=" + RUT;
                //var mes = "&MES=" + MES;
                //var periodor = "&PERIODO=" + ANIO;

                string url = "http://164.77.177.179:5055/API/login/GetToken?Username=QHRPORTAL&Password=1234";

                string apikey = new WebClient().DownloadString(url);

                string url_Principal2 = "http://164.77.177.179:5055/api/ZHR_MF_LIQSUELD?";
                var rut = "RUT=" + "12279395-8";
                var mes = "&MES=" + MES;
                var periodor = "&PERIODO=" + ANIO;
                var url_2 = url_Principal2 + rut;
                string key2 = "Bearer " + apikey;

                var json2 = new WebClient();
                json2.Headers.Add("Authorization", key2);
                var Retorno2 = json2.DownloadString(url_2);

                var Convert = JsonConvert.DeserializeObject(Retorno2);

                JObject json = JObject.Parse(Retorno2);
                List<JToken> dataa = json.Children().ToList();

                foreach (JProperty item in dataa)
                {
                    item.CreateReader();
                    switch (item.Name)
                    {
                        case "_CONTINGENTE":
                            foreach (JObject msg in item.Values())
                            {
                                //table.anzhl = (string)msg["anzhl"];
                                //table.kverb = (string)msg["kverb"];
                                //table.dispo = (string)msg["dispo"];
                                //table.ktart = (string)msg["ktart"];
                                //table.ktext = (string)msg["ktext"];
                                //miDataTable.Rows.Add(table);
                            }
                            break;

                        default:

                            break;
                    }
                }




                return Listabinaria;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            //finally
            //{
            //    //lt_T_MENSAJE = null;
            //    //lt_E_T_BINARY = null;
            //    //SalidaBinaria = null;
            //    //ListaMensaje = null;
            //    //conexion = null;
            //}
        }

        public List<string> CalculaFecha(int parametro)
        {
            parametro = parametro + 1;
            DateTime fechaActual = DateTime.Now;
            List<DateTime> lista = new List<DateTime>();
            List<string> lista2 = new List<string>();
            for (int i = 1; i <= parametro; i++)
            {
                DateTime nuevaFecha;
                if (lista2.Count == 0)
                {
                    nuevaFecha = fechaActual.AddMonths(i * -1);
                }
                else
                {
                    nuevaFecha = fechaActual.AddMonths(i * -1);
                }

                string Date = string.Format("{0:Y}", Convert.ToDateTime(nuevaFecha));
                lista2.Add(Date.ToUpper());
            }
            return lista2;
        }

        //public List<string> ListaCertificado()
        //{
        //    //List<string> lista2 = new List<string>();
        //    //lista2.Add("Certificado Antiguedad");
        //    //lista2.Add("Certificado de Remuneraciones");
        //    //return lista2;
        //}

        public void ListaCertificado()
        {

            ListaDespegable lista = new ListaDespegable();

            lista = new ListaDespegable();
            lista.COD = "00";
            lista.TEXT = "Seleccionar Certificado";
            ObjCertif.Add(lista);

            lista = new ListaDespegable();
            lista.COD = "1";
            lista.TEXT = "Certificado Antiguedad";
            ObjCertif.Add(lista);

            lista = new ListaDespegable();
            lista.COD = "2";
            lista.TEXT = "Certificado de Remuneraciones";
            ObjCertif.Add(lista);
        }

        public void ListaMotivo()
        {
            ListaDespegable lista = new ListaDespegable();
            lista = new ListaDespegable();
            lista.COD = "00";
            lista.TEXT = "Seleccionar Motivo";
            ObjMotivo.Add(lista);

            lista = new ListaDespegable();
            lista.COD = "1";
            lista.TEXT = "CCAA - Los Andes";
            ObjMotivo.Add(lista);

            lista = new ListaDespegable();
            lista.COD = "2";
            lista.TEXT = "Instituciones Bancarias";
            ObjMotivo.Add(lista);

            lista = new ListaDespegable();
            lista.COD = "3";
            lista.TEXT = "Cooperativas";
            ObjMotivo.Add(lista);

            lista = new ListaDespegable();
            lista.COD = "4";
            lista.TEXT = "Otro";
            ObjMotivo.Add(lista);
        }


        public void ObtieneCtas()
        {
            ListaDespegable lista = new ListaDespegable();
            for (int i = 0; i < 60; i++)
            {
                lista = new ListaDespegable();
                var n = i + 1;
                lista.COD = n.ToString();
                lista.TEXT = "Cuota " + n.ToString();
                ObjCtas.Add(lista);

            }

        }


        //    //Agregado Por Jaime Marchant 05.08.2019
        //    public List<ZHR_ST_SALIDA> ObtenerDatosAbsentismo(string RUT, string usuario, string contrasena)
        //    {
        //        IRfcTable lt_TB_SALIDA;

        //        ZHR_ST_SALIDA SalidaDatosReport;

        //        ConexionController conexion = new ConexionController();

        //        List<ZHR_ST_SALIDA> ListaAbsentismo = new List<ZHR_ST_SALIDA>();

        //        string retorno = "ERROR";

        //        try
        //        {
        //            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
        //                retorno = conexion.connectionsSAP(usuario, contrasena);

        //            if (string.IsNullOrEmpty(retorno))
        //            {
        //                //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
        //                //RfcRepository SapRfcRepository = SapRfcDestination.Repository;

        //                //IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_RP_ABSENTISMO");

        //                //BapiGetUser.SetValue("I_RUT", RUT);

        //                //BapiGetUser.Invoke(SapRfcDestination);

        //                //lt_TB_SALIDA = BapiGetUser.GetTable("TB_SALIDA");

        //                //for (int i = 0; i < lt_TB_SALIDA.RowCount; i++)
        //                //{
        //                //    SalidaDatosReport = new ZHR_ST_SALIDA();
        //                //    //SalidaDatosReport.PERNR = lt_TB_SALIDA[i].GetString("PERNR");
        //                //    SalidaDatosReport.RUT = lt_TB_SALIDA[i].GetString("RUT");
        //                //    SalidaDatosReport.NOMBRE = lt_TB_SALIDA[i].GetString("NOMBRE");
        //                //    SalidaDatosReport.POSICION = lt_TB_SALIDA[i].GetString("POSICION");
        //                //    SalidaDatosReport.T_SOCIEDAD = lt_TB_SALIDA[i].GetString("T_SOCIEDAD");
        //                //    SalidaDatosReport.DIVISION = lt_TB_SALIDA[i].GetString("DIVISION");
        //                //    SalidaDatosReport.T_DIVISION = lt_TB_SALIDA[i].GetString("T_DIVISION");
        //                //    SalidaDatosReport.TIP_AB = lt_TB_SALIDA[i].GetString("TIP_AB");
        //                //    DateTime fec_ini = Convert.ToDateTime(lt_TB_SALIDA[i].GetString("FECINI"));
        //                //    SalidaDatosReport.FECINI = fec_ini.ToString("dd/MM/yyyy");
        //                //    DateTime fec_fin = Convert.ToDateTime(lt_TB_SALIDA[i].GetString("FECFIN"));
        //                //    SalidaDatosReport.FECFIN = fec_fin.ToString("dd/MM/yyyy");
        //                //    SalidaDatosReport.N_DIAS = lt_TB_SALIDA[i].GetString("N_DIAS");
        //                //    ListaAbsentismo.Add(SalidaDatosReport);
        //                //}

        //            }
        //            return ListaAbsentismo;
        //        }
        //        catch (Exception e)
        //        {
        //            System.Diagnostics.Debug.Write(e.StackTrace);
        //            throw new Exception();
        //        }
        //        finally
        //        {
        //            SalidaDatosReport = null;
        //            conexion = null;
        //        }
        //    }



    }
}