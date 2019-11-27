using ProyectoTanner.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoTanner.Controllers
{

    public class ConexionDBA
    {

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        public List<MdAprobador> ListAprobadores = new List<MdAprobador>();
        public List<MdDetCertificado> ListDetCertificado = new List<MdDetCertificado>();
        public List<MdDetAbs> ListDetAbsentismos = new List<MdDetAbs>();
        public List<MdSolicitud> ListSolicitudes = new List<MdSolicitud>();
        public List<MdEstadoAprobacion> ListEstApro = new List<MdEstadoAprobacion>();

        string mensaje;
        public string Conexion()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["DBA_ICV"].ConnectionString;
                cn = new SqlConnection(conn);
                cn.Open();

                mensaje = "Conectado";
            }
            catch (Exception ex)
            {
                mensaje = "No se conecto con la base de datos:" + ex.ToString();
            }

            return mensaje;
        }

        public string ConexionClose()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["DBA_ICV"].ConnectionString;
                cn = new SqlConnection(conn);
                cn.Close();

                mensaje = "Cerrado";
            }
            catch (Exception ex)
            {
                mensaje = "se ha cerrado la conexion:" + ex.ToString();
            }

            return mensaje;

        }



        public TwoStringValue Get_user2(string correo, string pass)
        {

            TwoStringValue st = new TwoStringValue();
            try
            {
                SqlCommand cmd = new SqlCommand("get_user", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
                cmd.Parameters.Add("@clave", SqlDbType.VarChar).Value = pass;

                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();

                st.str1 = Convert.ToString(lector[4]);
                st.str2 = Convert.ToString(lector[0]);
                return st;
            }
            catch (Exception)
            {
                st.str1 = "Error";
                return st;
                throw;
            }
        }

        public List<MdSolicitud> VerSolPendJefe(string idAprobador)
        {
            List<MdSolicitud> misSol = new List<MdSolicitud>();
            try
            {
                SqlCommand cmd = new SqlCommand("Jefe_obt_solicitudes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_aprobador", SqlDbType.Int).Value = idAprobador;
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    MdSolicitud Sol = new MdSolicitud();
                    Sol.id_solicitud = Convert.ToString(lector[0]);
                    Sol.id_unidad_org = Convert.ToString(lector[1]);
                    Sol.id_usuario_web = Convert.ToString(lector[2]);

                    DateTime FechaR = DateTime.Parse(Convert.ToString(lector[3]));
                    string FechaReg = FechaR.ToString("dd-MM-yyyy");


                    Sol.fecha_registro = FechaReg;
                    Sol.id_tipo_solicitud = Convert.ToString(lector[4]);
                    Sol.estado_solicitud = Convert.ToString(lector[5]);
                    Sol.nombre = Convert.ToString(lector[6]);

                    misSol.Add(Sol);
                }

                return misSol;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<MdSolicitud> VerSolicitudes(string id_usuario)
        {
            List<MdSolicitud> misSol = new List<MdSolicitud>();
            try
            {
                SqlCommand cmd = new SqlCommand("get_solicitudes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_usuario_web", SqlDbType.Int).Value = id_usuario;
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    MdSolicitud Sol = new MdSolicitud();
                    Sol.id_solicitud = Convert.ToString(lector[0]);
                    Sol.id_unidad_org = Convert.ToString(lector[1]);
                    Sol.id_usuario_web = Convert.ToString(lector[2]);

                    DateTime FechaR = DateTime.Parse(Convert.ToString(lector[3]));
                    string FechaReg = FechaR.ToString("dd-MM-yyyy");


                    Sol.fecha_registro = FechaReg;
                    Sol.id_tipo_solicitud = Convert.ToString(lector[4]);
                    Sol.estado_solicitud = Convert.ToString(lector[5]);
                    misSol.Add(Sol);
                }

                return misSol;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //inserta  solicitud
        public string insert_solicitud(string unidad, string user_web, string fecha, string tipo)
        {
            string msn = null;
            int ms = 0;
            try
            {
                DateTime fechaSol = DateTime.Parse(fecha);
                string sqlFormat = fechaSol.ToString("yyyy-MM-dd");

                SqlCommand cmd = new SqlCommand("insert_solicitud", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@unidad_org", SqlDbType.Int).Value = int.Parse(unidad);
                cmd.Parameters.Add("@usuario_web", SqlDbType.Int).Value = int.Parse(user_web);
                cmd.Parameters.Add("@fecha_reg", SqlDbType.Date).Value = DateTime.Parse(sqlFormat);
                cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = int.Parse(tipo);
                cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();
                ms = lector.GetInt32(0);
                return msn = ms.ToString();

            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }
;
        }

        public string insert_certificado(string IdSolicitud, string TipoCert, string Motivo, string ValorCredito, string ValorCta, string numCta, string Rut, string Nombre)
        {
            string msn = null;
            int ms = 0;

            if (ValorCredito == null)
            {
                ValorCredito = "0";
            }

            if (ValorCta == null)
            {
                ValorCta = "0";
            }

            if (numCta == null)
            {
                numCta = "0";
            }
            try
            {
                SqlCommand cmd = new SqlCommand("insert_det_certificado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = IdSolicitud;
                cmd.Parameters.Add("@tipo_certificado", SqlDbType.Int).Value = TipoCert;
                cmd.Parameters.Add("@tipo_motivo", SqlDbType.Int).Value = Motivo;
                cmd.Parameters.Add("@valor_credito", SqlDbType.Int).Value = ValorCredito;
                cmd.Parameters.Add("@valor_cuota", SqlDbType.Int).Value = ValorCta;
                cmd.Parameters.Add("@cantidad_cuota", SqlDbType.Int).Value = numCta;
                cmd.Parameters.Add("@rut", SqlDbType.VarChar).Value = Rut;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Nombre;
                cmd.Parameters.Add("@id_detalle", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();
                ms = lector.GetInt32(0);
                return msn = ms.ToString();

            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }
;
        }

        public string Insert_det_abs(string id_solicitud, string id_tipo_abs, string rut, string nombre, string fecha_ini, string fecha_fin)
        {
            string msn = null;
            int ms = 0;
            try
            {

                DateTime fe_ini = DateTime.Parse(fecha_ini);
                string f_ini = fe_ini.ToString("yyyy-MM-dd");

                DateTime fe_fin = DateTime.Parse(fecha_fin);
                string f_fin = fe_fin.ToString("yyyy-MM-dd");

                DateTime fe_reg = DateTime.Now;
                string f_reg = fe_reg.ToString("yyyy-MM-dd");

                SqlCommand cmd = new SqlCommand("insert_det_abs", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = id_solicitud;
                cmd.Parameters.Add("@id_tipo_abs", SqlDbType.Int).Value = id_tipo_abs;
                cmd.Parameters.Add("@rut", SqlDbType.VarChar).Value = rut;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                cmd.Parameters.Add("@fecha_reg", SqlDbType.Date).Value = f_reg;
                cmd.Parameters.Add("@fecha_ini", SqlDbType.Date).Value = f_ini;
                cmd.Parameters.Add("@fecha_fin", SqlDbType.Date).Value = f_fin;
                cmd.Parameters.Add("@id_detalle", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();
                ms = lector.GetInt32(0);
                return msn = ms.ToString();
            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }
        }


        public void insert_estado_aprobacion(string IdDetalle, string idSolicitud, string tipo, string id_aprobador, string idSequencia)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert_est_aprobacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_detalle", SqlDbType.Int).Value = IdDetalle;
                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                cmd.Parameters.Add("@id_tipo_solicitud", SqlDbType.Int).Value = tipo;
                cmd.Parameters.Add("@id_aprobador", SqlDbType.VarChar).Value = id_aprobador;
                cmd.Parameters.Add("@id_sequencia", SqlDbType.Int).Value = idSequencia;

                if (idSequencia == "1")
                {
                    cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "X";
                }
                else
                {
                    cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "";
                }


                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<MdAprobador> ObtenerAprobadores(string unidad)
        {
            string msn = null;
            try
            {
                SqlCommand cmd = new SqlCommand("get_aprobadores", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_unidad_org", SqlDbType.Int).Value = unidad;
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    MdAprobador aprob = new MdAprobador();
                    aprob.unidadorg = Convert.ToString(lector[0]);
                    aprob.sequencia = Convert.ToString(lector[1]);
                    aprob.aprobador = Convert.ToString(lector[2]);

                    ListAprobadores.Add(aprob);
                }

                return ListAprobadores;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public List<MdAprobador> ObtenerAprobadoresJefatura(string idpernr)
        {
            string msn = null;
            try
            {
                SqlCommand cmd = new SqlCommand("get_apr_sol_jefes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_pernr", SqlDbType.VarChar).Value = idpernr;
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    MdAprobador aprob = new MdAprobador();
                   
                    aprob.aprobador = Convert.ToString(lector[0]);
                    aprob.sequencia = Convert.ToString(lector[1]);

                    ListAprobadores.Add(aprob);
                }

                return ListAprobadores;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public string JefeRechSoli(string idSolicitud, string idAprobador, string id_tipo, string texto)
        {
            string msn = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Jefe_Actualiza_Rech", cn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                cmd.Parameters.Add("@id_aprobador", SqlDbType.VarChar).Value = idAprobador;
                cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = id_tipo;
                cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = texto;
                cmd.Parameters.Add("@msn", SqlDbType.VarChar).Value = ParameterDirection.Output;


                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();
                return msn = Convert.ToString(lector[0]);
            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }

        }
        public string JefeAprSoli(string idSolicitud, string idAprobador)
        {
            string msn = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Jefe_Actualiza_Apr", cn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                cmd.Parameters.Add("@id_aprobador", SqlDbType.VarChar).Value = idAprobador;
                cmd.Parameters.Add("@msn", SqlDbType.VarChar).Value = ParameterDirection.Output;


                SqlDataReader lector = cmd.ExecuteReader();
                lector.Read();
                return msn = Convert.ToString(lector[0]);
            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }


        }
        public string Solicitud_pendientes(string unidadOrg, string tipoSol, string rut)
        {
            string msn = null;
            try
            {
                SqlCommand cmd = new SqlCommand("solicitudes_pendientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@unidad_org", SqlDbType.Int).Value = unidadOrg;
                cmd.Parameters.Add("@id_tipo_solicitud", SqlDbType.Int).Value = tipoSol;
                cmd.Parameters.Add("@rut", SqlDbType.VarChar).Value = rut;
                cmd.Parameters.Add("@resultado", SqlDbType.VarChar).Value = ParameterDirection.Output;

                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();

                return msn = Convert.ToString(lector[0]);
            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }
        }
        public string ActualizaDetCert(string id_solicitud, byte[] certi)
        {
            string msn = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Actualiza_detalle_Certificado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = id_solicitud;
                cmd.Parameters.Add("@certificado", SqlDbType.Binary).Value = certi;

                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();

                return msn = Convert.ToString(lector[0]);
            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }

        }
        public string get_tipo_solicitud(string id_solicitud)
        {
            string msn = null;
            int ms = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("get_tipo_solicitud", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = id_solicitud;
                cmd.Parameters.Add("@tipo", SqlDbType.Int).Value = ParameterDirection.Output;

                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();
                ms = lector.GetInt32(0);
                return msn = ms.ToString();
            }
            catch (Exception ex)
            {
                return msn = ex.Message;
                throw;
            }
        }

        public List<MdDetCertificado> GetDetCertificados(string idSolicitud)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("get_det_certificado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    MdDetCertificado md = new MdDetCertificado();
                    md.id_detalle = Convert.ToString(lector[0]);
                    md.id_solicitud = Convert.ToString(lector[1]);
                    md.tipo_certificado = Convert.ToString(lector[2]);
                    md.tipo_motivo = Convert.ToString(lector[3]);
                    md.valor_credito = Convert.ToString(lector[4]);
                    md.valor_cuota = Convert.ToString(lector[5]);
                    md.cantidad_cuota = Convert.ToString(lector[6]);
                    md.rut = Convert.ToString(lector[7]);
                    md.nombre = Convert.ToString(lector[8]);
                    md.certificado = Convert.ToString(lector[9]);

                    ListDetCertificado.Add(md);
                }



                return ListDetCertificado;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }


        public MdDetCertificado GetCertificado(string idSolicitud)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("get_det_certificado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                SqlDataReader lector = cmd.ExecuteReader();
                //lector.Read();

                MdDetCertificado md = new MdDetCertificado();

                while (lector.Read())
                {

                    md.id_detalle = Convert.ToString(lector[0]);
                    md.id_solicitud = Convert.ToString(lector[1]);
                    md.tipo_certificado = Convert.ToString(lector[2]);
                    md.tipo_motivo = Convert.ToString(lector[3]);
                    md.valor_credito = Convert.ToString(lector[4]);
                    md.valor_cuota = Convert.ToString(lector[5]);
                    md.cantidad_cuota = Convert.ToString(lector[6]);
                    md.rut = Convert.ToString(lector[7]);
                    md.nombre = Convert.ToString(lector[8]);
                    md.certificado = Convert.ToString(lector[9]);


                }

                return md;



            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public MdDetAbs verDetalleAbs(String idSolicitud)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("get_det_abs", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                SqlDataReader lector = cmd.ExecuteReader();
                MdDetAbs md = new MdDetAbs();
                while (lector.Read())
                {
                    md.id_abs = Convert.ToString(lector[0]);
                    md.id_solicitud = Convert.ToString(lector[1]);
                    md.id_tipo_abs = Convert.ToString(lector[2]);
                    md.rut = Convert.ToString(lector[3]);
                    md.nombre = Convert.ToString(lector[4]);
                    md.fecha_reg = Convert.ToString(lector[5]);
                    md.fecha_ini = Convert.ToString(lector[6]);
                    md.fecha_fin = Convert.ToString(lector[7]);
                    md.comentario = Convert.ToString(lector[8]);
                    md.estatus = Convert.ToString(lector[9]);

                }

                return md;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public List<MdDetAbs> GetDetAbsentismos(string idSolicitud)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("get_det_abs", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = idSolicitud;
                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    MdDetAbs md = new MdDetAbs();
                    md.id_abs = Convert.ToString(lector[0]);
                    md.id_solicitud = Convert.ToString(lector[1]);
                    md.id_tipo_abs = Convert.ToString(lector[2]);
                    md.rut = Convert.ToString(lector[3]);
                    md.nombre = Convert.ToString(lector[4]);
                    md.fecha_reg = Convert.ToString(lector[5]);
                    md.fecha_ini = Convert.ToString(lector[6]);
                    md.fecha_fin = Convert.ToString(lector[7]);
                    md.comentario = Convert.ToString(lector[8]);
                    md.estatus = Convert.ToString(lector[9]);


                    ListDetAbsentismos.Add(md);
                }

                return ListDetAbsentismos;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }

        public string obtener_motivo_Rechazo(string id_solicitud, string tipo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("get_motivo_rechazo", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = id_solicitud;
                cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = tipo;
                cmd.Parameters.Add("@comentario", SqlDbType.VarChar).Value = ParameterDirection.Output;
                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();
                string msn = lector.GetString(0);
                return msn;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }

        public string Obtener_Mail(string id_pernr)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("obtener_mail", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_pernr", SqlDbType.Int).Value = id_pernr;
                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = ParameterDirection.Output;
                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();
                string msn = lector.GetString(0);
                return msn;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public string Obtener_Mail_sq(string id_solicitud)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("obtener_mail_sq", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_solicitud", SqlDbType.Int).Value = id_solicitud;
                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = ParameterDirection.Output;
                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();
                string msn = lector.GetString(0);
                return msn;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public void cambiarPass(string correo, string pernr, string pass)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ChangePass", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_pernr", SqlDbType.VarChar).Value = pernr;
                cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo;
                cmd.Parameters.Add("@clave", SqlDbType.VarChar).Value = pass;
                SqlDataReader lector = cmd.ExecuteReader();

                lector.Read();
                //return "OK";
                
            }
            catch (Exception )
            {
                //return "Error";
                throw;
            }

        }



    }
}
