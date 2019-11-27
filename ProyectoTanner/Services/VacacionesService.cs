using ProyectoTanner.Models;
using System;
using System.Collections.Generic;
//using SAP.Middleware.Connector;

namespace ProyectoTanner.Services
{
    public class VacacionesService
    {

        public List<ListaVacaciones> ObjSalida = new List<ListaVacaciones>();
        public List<Tablamensaje> Objmensaje = new List<Tablamensaje>();
        public List<_VACACIONES> ObjTipo = new List<_VACACIONES>();
        public List<ListadosolicitudesPendientesAprobacion> ObjPendAprob = new List<ListadosolicitudesPendientesAprobacion>();
        public List<ListadoSolicitudesHistoricoVigente> ListaVacacionesHistorico = new List<ListadoSolicitudesHistoricoVigente>();
        public List<PermisoAdministrativo> ObjPerAdmi = new List<PermisoAdministrativo>();
        public void ObtenerVacaciones(string RUT, string usuario, string contrasena)
        {
            ObjTipo.Clear();
            ObjSalida.Clear();
            Objmensaje.Clear();

            //IRfcTable lt_T_OUTPUT;

            //IRfcTable lt_T_MENSAJE;

            //IRfcTable lt_T_VACACIONES;

            //ListaVacaciones  SalidaVacaciones;

            //Tablamensaje SalidaMensajes;

            //TipoVacaciones SalidaTipoVacaciones;

            //ConexionController conexion = new ConexionController();

            //List<ListaVacaciones> ListaVacaciones = new List<ListaVacaciones>();
            //List<Tablamensaje> ListaMensajes = new List<Tablamensaje>();
            //List<TipoVacaciones> listaTipoVacaciones = new List<TipoVacaciones>();


            string retorno = "ERROR";

            try
            {
                if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                    //retorno = conexion.connectionsSAP(usuario, contrasena);

                    if (string.IsNullOrEmpty(retorno))
                    {
                        //RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                        //RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                        //IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_GET_CONT");

                        //BapiGetUser.SetValue("I_RUT", RUT);

                        //BapiGetUser.Invoke(SapRfcDestination);

                        //lt_T_OUTPUT = BapiGetUser.GetTable("T_OUTPUT");

                        //for (int i = 0; i < lt_T_OUTPUT.RowCount; i++)
                        //{
                        //    SalidaVacaciones = new ListaVacaciones();

                        //    SalidaVacaciones.ANZHL = lt_T_OUTPUT[i].GetString("ANZHL");
                        //    SalidaVacaciones.KVERB = lt_T_OUTPUT[i].GetString("KVERB");
                        //    SalidaVacaciones.DISPO = lt_T_OUTPUT[i].GetString("DISPO");
                        //    SalidaVacaciones.KTART = lt_T_OUTPUT[i].GetString("KTART");
                        //    SalidaVacaciones.KTEXT = lt_T_OUTPUT[i].GetString("KTEXT");
                        //    ObjSalida.Add(SalidaVacaciones);
                        //}

                        //lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");

                        //for (int j = 0; j < lt_T_MENSAJE.RowCount; j++)
                        //{
                        //    SalidaMensajes = new Tablamensaje();

                        //    SalidaMensajes.codigo = lt_T_MENSAJE[j].GetString("CODIGO");
                        //    SalidaMensajes.mensaje = lt_T_MENSAJE[j].GetString("MENSAJE");
                        //    Objmensaje.Add(SalidaMensajes);
                        //}

                        //lt_T_VACACIONES = BapiGetUser.GetTable("T_VACACIONES");

                        //SalidaTipoVacaciones = new TipoVacaciones();
                        //SalidaTipoVacaciones.COD_TIP = "01";
                        //SalidaTipoVacaciones.DES_TIP = "Seleccioné";
                        //ObjTipo.Add(SalidaTipoVacaciones);

                        //for (int k = 0; k < lt_T_VACACIONES.RowCount; k++)
                        //{
                        //    SalidaTipoVacaciones = new TipoVacaciones();
                        //    if (lt_T_VACACIONES[k].GetString("COD_TIP") != "7001")
                        //    {
                        //        SalidaTipoVacaciones.COD_TIP = lt_T_VACACIONES[k].GetString("COD_TIP");
                        //        SalidaTipoVacaciones.DES_TIP = lt_T_VACACIONES[k].GetString("DES_TIP");
                        //        ObjTipo.Add(SalidaTipoVacaciones);
                        //    }    
                        //}
                    }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //lt_T_OUTPUT = null;
                //SalidaVacaciones = null;
                //SalidaMensajes = null;
                //SalidaTipoVacaciones = null;
                //conexion = null;
            }
        }

        public void ObtenerVacacionesHistorico(string RUT, string usuario, string contrasena)
        {
            //IRfcTable lt_T_OUTPUT;

            //ListadoSolicitudesHistoricoVigente SalidaVacacionesHistorico;

            //ConexionController conexion = new ConexionController();

            //string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_GET_ABS");

                //    BapiGetUser.SetValue("I_RUT", RUT);

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_OUTPUT = BapiGetUser.GetTable("T_OUTPUT");

                //    for (int i = 0; i < lt_T_OUTPUT.RowCount; i++)
                //    {
                //        SalidaVacacionesHistorico = new ListadoSolicitudesHistoricoVigente();

                //       //SalidaVacacionesHistorico.BEGDA = lt_T_OUTPUT[i].GetString("BEGDA");
                //        DateTime fec_BEGDA = Convert.ToDateTime(lt_T_OUTPUT[i].GetString("BEGDA"));
                //        SalidaVacacionesHistorico.BEGDA = fec_BEGDA.ToString("dd/MM/yyyy");

                //        DateTime fec_ENDA = Convert.ToDateTime(lt_T_OUTPUT[i].GetString("ENDDA"));
                //        SalidaVacacionesHistorico.ENDDA = fec_ENDA.ToString("dd/MM/yyyy");

                //        //SalidaVacacionesHistorico.ENDDA = lt_T_OUTPUT[i].GetString("ENDDA");
                //        //SalidaVacacionesHistorico.AEDTM = lt_T_OUTPUT[i].GetString("AEDTM");

                //        DateTime fec_AEDTM = Convert.ToDateTime(lt_T_OUTPUT[i].GetString("AEDTM"));
                //        SalidaVacacionesHistorico.AEDTM = fec_AEDTM.ToString("dd/MM/yyyy");

                //        SalidaVacacionesHistorico.AWART = lt_T_OUTPUT[i].GetString("AWART");
                //        SalidaVacacionesHistorico.ABRTG = lt_T_OUTPUT[i].GetString("ABRTG");
                //        SalidaVacacionesHistorico.SPRPS = lt_T_OUTPUT[i].GetString("SPRPS");
                //        SalidaVacacionesHistorico.DOCNR = lt_T_OUTPUT[i].GetString("DOCNR");
                //        SalidaVacacionesHistorico.ATEXT = lt_T_OUTPUT[i].GetString("ATEXT");
                //        SalidaVacacionesHistorico.STATU = lt_T_OUTPUT[i].GetString("STATU");

                //        ListaVacacionesHistorico.Add(SalidaVacacionesHistorico);
                //    }
                //}

                //return ListaVacacionesHistorico;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //lt_T_OUTPUT = null;
                //SalidaVacacionesHistorico = null;
                //conexion = null;
            }
        }

        public void AprobarRechazarVacaciones(string RUT, string STATUS, string NDOC, string RUTEMPL, string usuario, string contrasena) // Aprobar y rechazar Solicitud de Vacaciones
        {
            //IRfcTable lt_T_tablaMensajes;

            //Tablamensaje T_SALIDA;

            //List<Tablamensaje> Listaprobarechazar = new List<Tablamensaje>();

            //ConexionController conexion = new ConexionController();

            //string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_APR_VAC");

                //    BapiGetUser.SetValue("I_ICNUM1", RUT);
                //    BapiGetUser.SetValue("I_ESTATUS", STATUS);
                //    BapiGetUser.SetValue("I_DOCNR", NDOC);
                //    BapiGetUser.SetValue("I_ICNUM2", RUTEMPL);

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_tablaMensajes = BapiGetUser.GetTable("T_MENSAJE");

                //    for (int i = 0; i < lt_T_tablaMensajes.RowCount; i++)
                //    {
                //        T_SALIDA = new Tablamensaje();
                //        T_SALIDA.codigo = lt_T_tablaMensajes[i].GetString("CODIGO");
                //        T_SALIDA.mensaje = lt_T_tablaMensajes[i].GetString("MENSAJE");
                //        Objmensaje.Add(T_SALIDA);
                //    }
                //}
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //lt_T_tablaMensajes = null;
                //Listaprobarechazar = null;
                //conexion = null;
            }
        }

        public void ObtenerListaPendientesAprob(string RUT, string usuario, string contrasena)
        {
            //IRfcTable lt_T_OUTPUT;

            //IRfcTable lt_T_MENSAJE;

            //Tablamensaje T_SALIDA;

            //ListadosolicitudesPendientesAprobacion SalidaListaPendienteAprob;

            //ConexionController conexion = new ConexionController();

            //List<ListadosolicitudesPendientesAprobacion> ListaPendienteAprob = new List<ListadosolicitudesPendientesAprobacion>();

            //List<Tablamensaje> ListaMensaje = new List<Tablamensaje>();

            //string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_SOL_PEN");

                //    BapiGetUser.SetValue("I_RUT", RUT);

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_OUTPUT = BapiGetUser.GetTable("T_OUTPUT");

                //    for (int i = 0; i < lt_T_OUTPUT.RowCount; i++)
                //    {
                //        SalidaListaPendienteAprob = new ListadosolicitudesPendientesAprobacion();

                //        SalidaListaPendienteAprob.DOCNR = lt_T_OUTPUT[i].GetString("DOCNR");
                //        SalidaListaPendienteAprob.ICNUM = lt_T_OUTPUT[i].GetString("ICNUM");
                //        SalidaListaPendienteAprob.VORNA = lt_T_OUTPUT[i].GetString("VORNA");
                //        SalidaListaPendienteAprob.NACHN = lt_T_OUTPUT[i].GetString("NACHN");
                //        SalidaListaPendienteAprob.NACH2 = lt_T_OUTPUT[i].GetString("NACH2");
                //        DateTime fec_AEDTM = Convert.ToDateTime(lt_T_OUTPUT[i].GetString("AEDTM"));
                //        SalidaListaPendienteAprob.AEDTM = fec_AEDTM.ToString("dd/MM/yyyy");
                //        //SalidaListaPendienteAprob.AEDTM = lt_T_OUTPUT[i].GetString("AEDTM");
                //        SalidaListaPendienteAprob.AWART = lt_T_OUTPUT[i].GetString("AWART");
                //        SalidaListaPendienteAprob.ATEXT = lt_T_OUTPUT[i].GetString("ATEXT");
                //        DateTime fec_BEGDA = Convert.ToDateTime(lt_T_OUTPUT[i].GetString("BEGDA"));
                //        SalidaListaPendienteAprob.BEGDA = fec_BEGDA.ToString("dd/MM/yyyy");
                //        //SalidaListaPendienteAprob.BEGDA = lt_T_OUTPUT[i].GetString("BEGDA");
                //        //SalidaListaPendienteAprob.ENDDA = lt_T_OUTPUT[i].GetString("ENDDA");
                //        DateTime fec_ENDDA = Convert.ToDateTime(lt_T_OUTPUT[i].GetString("ENDDA"));
                //        SalidaListaPendienteAprob.ENDDA = fec_ENDDA.ToString("dd/MM/yyyy");

                //       // SalidaListaPendienteAprob.ABWTG = lt_T_OUTPUT[i].GetString("ABWTG");
                //        SalidaListaPendienteAprob.SPRPS = lt_T_OUTPUT[i].GetString("SPRPS");
                //        ObjPendAprob.Add(SalidaListaPendienteAprob);
                //    }

                //    lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");

                //    for (int j = 0; j < lt_T_MENSAJE.RowCount; j++)
                //    {
                //        T_SALIDA = new Tablamensaje();
                //        T_SALIDA.codigo = lt_T_MENSAJE[j].GetString("CODIGO");
                //        T_SALIDA.mensaje = lt_T_MENSAJE[j].GetString("MENSAJE");
                //        Objmensaje.Add(T_SALIDA);
                //    }
                //}
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //lt_T_OUTPUT = null;
                //SalidaListaPendienteAprob = null;
                //T_SALIDA = null;
                //conexion = null;
            }
        }

        public void SolictudVacaciones(List<SolicitudVacaciones> Solicitud, string usuario, string contrasena)
        {


            //IRfcTable lt_T_MENSAJE;

            //Tablamensaje T_SALIDA;

            //ConexionController conexion = new ConexionController();

            //List<Tablamensaje> ListaMensaje = new List<Tablamensaje>();

            //string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_SOL_VAC");


                //    IRfcStructure Soli = BapiGetUser.GetStructure("I_INPUT");

                //    for (var i = 0; i < Solicitud.Count; i++)
                //    { 
                //        Soli.SetValue("ICNUM1", Solicitud[i].ICNUM1);
                //        Soli.SetValue("AEDTM", Convert.ToDateTime(Solicitud[i].AEDTM));
                //        Soli.SetValue("AWART", Solicitud[i].AWART);
                //        Soli.SetValue("BEGDA", Convert.ToDateTime(Solicitud[i].BEGDA));
                //        Soli.SetValue("ENDDA", Convert.ToDateTime(Solicitud[i].ENDDA));
                //        Soli.SetValue("ICNUM2", Solicitud[i].ICNUM2);
                //        Soli.SetValue("SPRPS", Solicitud[i].SPRPS);
                //    }

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");


                //    for (int i = 0; i < lt_T_MENSAJE.RowCount; i++)
                //    {
                //        T_SALIDA = new Tablamensaje();
                //        T_SALIDA.codigo = lt_T_MENSAJE[i].GetString("CODIGO");
                //        T_SALIDA.mensaje = lt_T_MENSAJE[i].GetString("MENSAJE");
                //        Objmensaje.Add(T_SALIDA);
                //    }
                //}
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //ListaMensaje = null;
                //T_SALIDA = null;
                //conexion = null;
            }
        }

        public void SolictudPermisos(List<SolicitudPermisos> Solicitud, string usuario, string contrasena)
        {

            //IRfcTable lt_T_MENSAJE;

            //Tablamensaje T_SALIDA;

            //ConexionController conexion = new ConexionController();

            //List<Tablamensaje> ListaMensaje = new List<Tablamensaje>();

            //string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_INCL_ABS");


                //    IRfcStructure Soli = BapiGetUser.GetStructure("I_DATA");

                //    for (var i = 0; i < Solicitud.Count; i++)
                //    {
                //        Soli.SetValue("BEGDA", Convert.ToDateTime(Solicitud[i].BEGDA));
                //        Soli.SetValue("ENDDA", Convert.ToDateTime(Solicitud[i].ENDDA));
                //        Soli.SetValue("SUBTY", Solicitud[i].SUBTY);
                //        Soli.SetValue("AEDTM", Convert.ToDateTime(Solicitud[i].BEGDA));
                //        Soli.SetValue("RUT_CREA", Solicitud[i].RUT_CREA);
                //        Soli.SetValue("RUT_EMPLEA", Solicitud[i].RUT_EMPLEA);
                //    }

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");


                //    for (int i = 0; i < lt_T_MENSAJE.RowCount; i++)
                //    {
                //        T_SALIDA = new Tablamensaje();
                //        T_SALIDA.codigo = lt_T_MENSAJE[i].GetString("CODIGO");
                //        T_SALIDA.mensaje = lt_T_MENSAJE[i].GetString("MENSAJE");
                //        Objmensaje.Add(T_SALIDA);
                //    }
                //}
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //ListaMensaje = null;
                //T_SALIDA = null;
                //conexion = null;
            }
        }

        public void EnvioMailRevisionEquipo(List<EnvioMailRevisionEquipo> Solicitud, List<ZEHR045> Solicitud2, string usuario, string contrasena)
        {

            //IRfcTable lt_T_MENSAJE;

            //Tablamensaje T_SALIDA;

            //ConexionController conexion = new ConexionController();

            //List<Tablamensaje> ListaMensaje = new List<Tablamensaje>();

            //string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_MAIL_NOTIF");


                //    IRfcStructure Soli = BapiGetUser.GetStructure("I_DATA");

                //    for (var i = 0; i < Solicitud.Count; i++)
                //    {
                //        Soli.SetValue("AEDTM", Convert.ToDateTime(Solicitud[i].AEDTM));
                //        Soli.SetValue("RUT_CREA", Solicitud[i].RUT_CREA);
                //        Soli.SetValue("ZTEXTO", Solicitud[i].ZTEXTO);
                //        Soli.SetValue("ACTION", Solicitud[i].ACTION);
                //    }

                //    IRfcTable SoliColabo = BapiGetUser.GetTable("T_RUT_EMPLEA");

                //    for (var i = 0; i < Solicitud2.Count; i++)
                //    {
                //        SoliColabo.Append();
                //        SoliColabo.SetValue("RUT_EMPLEA", Solicitud2[i].RUT_EMPLEA);
                //        SoliColabo.SetValue("PERNR", Solicitud2[i].PERNR);
                //        SoliColabo.SetValue("ENAME", Solicitud2[i].ENAME);
                //    }

                //    BapiGetUser.SetValue("T_RUT_EMPLEA", SoliColabo);

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");


                //    for (int i = 0; i < lt_T_MENSAJE.RowCount; i++)
                //    {
                //        T_SALIDA = new Tablamensaje();
                //        T_SALIDA.codigo = lt_T_MENSAJE[i].GetString("CODIGO");
                //        T_SALIDA.mensaje = lt_T_MENSAJE[i].GetString("MENSAJE");
                //        Objmensaje.Add(T_SALIDA);
                //    }
                //}
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //ListaMensaje = null;
                //T_SALIDA = null;
                //conexion = null;
            }
        }

        public List<string> ObtieneDias()
        {
            List<string> lista2 = new List<string>();
            lista2.Add("1");
            lista2.Add("2");
            return lista2;
        }

        public void ObtieneDiasPermi()
        {
            PermisoAdministrativo Permisos = new PermisoAdministrativo();
            Permisos = new PermisoAdministrativo();
            Permisos.COD = "3010";
            Permisos.TEXT = "Permiso 1 dia";
            ObjPerAdmi.Add(Permisos);

            Permisos = new PermisoAdministrativo();
            Permisos.COD = "3012";
            Permisos.TEXT = "Permiso 1/2 dia";
            ObjPerAdmi.Add(Permisos);
        }


        public void ObtenerVacaciones7001(string RUT, string usuario, string contrasena)
        {

            //ObjTipo.Clear();
            //ObjSalida.Clear();
            //Objmensaje.Clear();

            //IRfcTable lt_T_OUTPUT;

            //IRfcTable lt_T_MENSAJE;

            //IRfcTable lt_T_VACACIONES;

            //ListaVacaciones SalidaVacaciones;

            //Tablamensaje SalidaMensajes;

            //TipoVacaciones SalidaTipoVacaciones;

            //ConexionController conexion = new ConexionController();

            //List<ListaVacaciones> ListaVacaciones = new List<ListaVacaciones>();
            //List<Tablamensaje> ListaMensajes = new List<Tablamensaje>();
            //List<TipoVacaciones> listaTipoVacaciones = new List<TipoVacaciones>();

            string retorno = "ERROR";

            try
            {
                //if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
                //    retorno = conexion.connectionsSAP(usuario, contrasena);

                //if (string.IsNullOrEmpty(retorno))
                //{
                //    RfcDestination SapRfcDestination = RfcDestinationManager.GetDestination(conexion.connectorConfig);
                //    RfcRepository SapRfcRepository = SapRfcDestination.Repository;

                //    IRfcFunction BapiGetUser = SapRfcRepository.CreateFunction("ZHR_GET_CONT");

                //    BapiGetUser.SetValue("I_RUT", RUT);

                //    BapiGetUser.Invoke(SapRfcDestination);

                //    lt_T_OUTPUT = BapiGetUser.GetTable("T_OUTPUT");

                //    for (int i = 0; i < lt_T_OUTPUT.RowCount; i++)
                //    {
                //        SalidaVacaciones = new ListaVacaciones();

                //        SalidaVacaciones.ANZHL = lt_T_OUTPUT[i].GetString("ANZHL");
                //        SalidaVacaciones.KVERB = lt_T_OUTPUT[i].GetString("KVERB");
                //        SalidaVacaciones.DISPO = lt_T_OUTPUT[i].GetString("DISPO");
                //        SalidaVacaciones.KTART = lt_T_OUTPUT[i].GetString("KTART");
                //        SalidaVacaciones.KTEXT = lt_T_OUTPUT[i].GetString("KTEXT");
                //        ObjSalida.Add(SalidaVacaciones);
                //    }

                //    lt_T_MENSAJE = BapiGetUser.GetTable("T_MENSAJE");

                //    for (int j = 0; j < lt_T_MENSAJE.RowCount; j++)
                //    {
                //        SalidaMensajes = new Tablamensaje();

                //        SalidaMensajes.codigo = lt_T_MENSAJE[j].GetString("CODIGO");
                //        SalidaMensajes.mensaje = lt_T_MENSAJE[j].GetString("MENSAJE");
                //        Objmensaje.Add(SalidaMensajes);
                //    }

                //    lt_T_VACACIONES = BapiGetUser.GetTable("T_VACACIONES");

                //    for (int k = 0; k < lt_T_VACACIONES.RowCount; k++)
                //    {
                //        SalidaTipoVacaciones = new TipoVacaciones();
                //        if (lt_T_VACACIONES[k].GetString("COD_TIP") == "7001")
                //        {
                //            SalidaTipoVacaciones.COD_TIP = lt_T_VACACIONES[k].GetString("COD_TIP");
                //            SalidaTipoVacaciones.DES_TIP = lt_T_VACACIONES[k].GetString("DES_TIP");
                //            ObjTipo.Add(SalidaTipoVacaciones);
                //        }
                //    }
                //}
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
                throw new Exception();
            }
            finally
            {
                //lt_T_VACACIONES = null;
                //SalidaVacaciones = null;
                //conexion = null;
            }
        }
    }
}