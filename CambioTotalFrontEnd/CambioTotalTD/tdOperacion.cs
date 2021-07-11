using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CambioTotalED;
using CambioTotalAD;
using MySql.Data.MySqlClient;

namespace CambioTotalTD
{
    public class tdOperacion : td_aglobal
    {
        adOperacion iadOperacion;

        public int tdOperacionCuentaBancaria(int tdtipoOperacion, int tdidCuentBanc, int tdidusuario, int tditipocuenta, int tdimoneda
                                        , int tdibanco, string tdbanco, string tdnumerocuenta, string tdnombrecuenta, int tditipodeclaracion
                                        , string tdtitular, string tdfecReg, string tdhoraReg)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        iRespuesta = iadOperacion.adOperacionCuentaBancaria(tdtipoOperacion, tdidCuentBanc, tdidusuario, tditipocuenta, tdimoneda
                                        , tdibanco, tdbanco, tdnumerocuenta, tdnombrecuenta, tditipodeclaracion
                                        , tdtitular, tdfecReg, tdhoraReg);
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

        public List<edCuentaBancaria> tdListarCuentaBancaria(int tdidusuario, string tdnombres, int tdimoneda)
        {
            List<edCuentaBancaria> renUsuario = new List<edCuentaBancaria>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        renUsuario = iadOperacion.adListarCuentaBancaria(tdidusuario, tdnombres, tdimoneda);
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

        public int tdOperaciondivisa(int tdtipoOperacion, int tdiddivisa, int tdidusuario, int tditipobusqueda,
            decimal tddmonto, decimal tddsolesventa, decimal tddsolescompra, decimal tdddolaresventa, decimal tdddolarescompra,
            int tditipopromocion, string tddtfecha, string tdvhora, string tddtfecharegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        iRespuesta = iadOperacion.adOperaciondivisa(tdtipoOperacion, tdiddivisa, tdidusuario, tditipobusqueda,
                                                        tddmonto, tddsolesventa, tddsolescompra, tdddolaresventa, tdddolarescompra,
                                                        tditipopromocion, tddtfecha, tdvhora, tddtfecharegistro);
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

        public List<edDivisa> tdListardivisa(int tdiddivisa, string tddtfecha)
        {
            List<edDivisa> renUsuario = new List<edDivisa>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        renUsuario = iadOperacion.adListardivisa(tdiddivisa, tddtfecha);
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

        public int tdOperaciontransaccion(int tditipooperacion, int tdidtransaccion, int tdidusuario, int tdiddivisa,
            int tdidpromocion, int tdidcuentabancaria, int tdidusuarioadministrador, string tddtfecha, int tditipodivisa,
            decimal tdddolaresventa, decimal tdddolarescompra, string tdvhora, string tdvimagenruta, int tditipoenvio,
            int tdiestado, string tdvoperacion, int tdiorigenfondo, decimal tddenvio, decimal tddrecibo, int tditipocambio,
            int tditipotrasaccion, decimal tddigv, string tdvbancoreceptor, string tddtfecharegistro, string tddtfechamodificacion,
            int tdidusuarioModificacion, string voperacionadmin)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        iRespuesta = iadOperacion.adOperaciontransaccion(tditipooperacion, tdidtransaccion, tdidusuario, tdiddivisa,
                                    tdidpromocion, tdidcuentabancaria, tdidusuarioadministrador, tddtfecha, tditipodivisa,
                                    tdddolaresventa, tdddolarescompra, tdvhora, tdvimagenruta, tditipoenvio, tdiestado, tdvoperacion,
                                    tdiorigenfondo, tddenvio, tddrecibo, tditipocambio, tditipotrasaccion, tddigv, tdvbancoreceptor,
                                    tddtfecharegistro, tddtfechamodificacion, tdidusuarioModificacion, voperacionadmin);
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

        public List<edTransaccion> tdListartransaccion(int tdidusuario, string tddtfecha, int tdestado)
        {
            List<edTransaccion> renUsuario = new List<edTransaccion>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        renUsuario = iadOperacion.adListartransaccion(tdidusuario, tddtfecha, tdestado);
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

        public edCuentaBancaria tdFiltrarCuentaBancaria(int tdidcuentabancaria)
        {
            edCuentaBancaria renUsuario = new edCuentaBancaria();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadOperacion = new adOperacion(con);
                        renUsuario = iadOperacion.adFiltrarCuentaBancaria(tdidcuentabancaria);
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
