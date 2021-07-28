using CambioTotalAD;
using CambioTotalED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CambioTotalTD
{
    public class tdUsuario : td_aglobal
    {
        adUsuario iadUsuario;

        public edUsuario tdObtenerUsuario(string tdcorreo, string tdclave)
        {
            edUsuario renUsuario = new edUsuario();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        renUsuario = iadUsuario.adObtenerUsuario(tdcorreo, tdclave);
                        scope.Commit();
                    }
                }
                return renUsuario;
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdInsertarUsuario(string tdnombres, string tdapellidos, int tdtipodocumento, string tddocumento,
            string tdfecharegistro, string tdhoraregistro, int tdtipousuario, string tdcorreo, string tdclave, string tdtoken,
            string tdruc, string tdrazonsocial, string tdpep1, string tdpep2, string tdpep3, string tdpep4, string tdcelular)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adInsertarUsuario(tdnombres, tdapellidos, tdtipodocumento, tddocumento,
                                                    tdfecharegistro, tdhoraregistro, tdtipousuario, tdcorreo, tdclave, tdtoken
                                                    , tdruc, tdrazonsocial, tdpep1, tdpep2, tdpep3, tdpep4, tdcelular);
                        scope.Commit();
                    }
                }
                return (iRespuesta);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdActualizarAcceso(int tdtipoactualizar, int tdidusuario, string tdcorreo, string tdclave)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adActualizarAcceso(tdtipoactualizar, tdidusuario, tdcorreo, tdclave);
                        scope.Commit();
                    }
                }
                return (iRespuesta);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdActualizarClave(int tdidusuario, string tdnuevaclave)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adActualizarClave(tdidusuario, tdnuevaclave);
                        scope.Commit();
                    }
                }
                return (iRespuesta);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdActualizarCuenta(int tdidusuario, int tdidprovincia, int tdidciudad, int tdiddistrito, string tddireccion, string tdnombres,
                                    string tdapellidos, int tdigenero, string tdimagenruta, string tdimagendniruta1, string tdimagendniruta2,
                                    string tdcelular1, string tdcelular2, string tdtelefono, int tditipodocumento, string tddocumento, string tdubigeo,
                                    string tdfechanacimiento, string tdfechamodificacion, string tdhoramodificacion, int tdidusuariomod,
                                    string tdruc, string tdrazonsocial)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adActualizarCuenta(tdidusuario, tdidprovincia, tdidciudad, tdiddistrito, tddireccion,
                                    tdnombres, tdapellidos, tdigenero, tdimagenruta, tdimagendniruta1, tdimagendniruta2,
                                    tdcelular1, tdcelular2, tdtelefono, tditipodocumento, tddocumento, tdubigeo,
                                    tdfechanacimiento, tdfechamodificacion, tdhoramodificacion, tdidusuariomod, tdruc, tdrazonsocial);
                        scope.Commit();
                    }
                }
                return (iRespuesta);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public edUsuario tdFiltrarUsuario(int tdusuario)
        {
            edUsuario renUsuario = new edUsuario();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        renUsuario = iadUsuario.adFiltrarUsuario(tdusuario);
                        scope.Commit();
                    }
                }
                return renUsuario;
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public List<edUsuario> tdListarUsuario(int tdpusuario)
        {
            List<edUsuario> renUsuario = new List<edUsuario>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        renUsuario = iadUsuario.adListarUsuario(tdpusuario);
                        scope.Commit();
                    }
                }
                return renUsuario;
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }


    }
}
