using MySql.Data.MySqlClient;
using CambioTotalED;
using System;
using System.Data;
using System.Collections.Generic;

namespace CambioTotalAD
{
    public class adOperacion : ad_aglobal
    {
        public adOperacion(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adOperacionCuentaBancaria(int adtipoOperacion, int adidCuentBanc, int adidusuario, int aditipocuenta, int adimoneda
                                        , int adibanco, string adbanco, string adnumerocuenta, string adnombrecuenta, int itipodeclaracion
                                        , string adtitular, string adfecReg, string adhoraReg)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_operacion_cuentabancaria", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_itipo_operacion", MySqlDbType.Int32).Value = adtipoOperacion;
                cmd.Parameters.Add("_idcuentabancaria", MySqlDbType.Int32).Value = adidCuentBanc;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_i_tipo_cuenta", MySqlDbType.Int32).Value = aditipocuenta;
                cmd.Parameters.Add("_i_moneda", MySqlDbType.Int32).Value = adimoneda;
                cmd.Parameters.Add("_i_banco", MySqlDbType.Int32).Value = adibanco;
                cmd.Parameters.Add("_v_banco", MySqlDbType.VarChar, 150).Value = adbanco;
                cmd.Parameters.Add("_v_numero_cuenta", MySqlDbType.VarChar, 150).Value = adnumerocuenta;
                cmd.Parameters.Add("_v_nombre_cuenta", MySqlDbType.VarChar, 150).Value = adnombrecuenta;
                cmd.Parameters.Add("_i_tipo_declaracion", MySqlDbType.Int32).Value = itipodeclaracion;
                cmd.Parameters.Add("_v_titular", MySqlDbType.VarChar, 150).Value = adtitular;
                cmd.Parameters.Add("_dt_fecharegistro", MySqlDbType.VarChar, 25).Value = adfecReg;
                cmd.Parameters.Add("_v_horaregistro", MySqlDbType.VarChar, 25).Value = adhoraReg;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edCuentaBancaria> adListarCuentaBancaria(int adidusuario, string adnombres)
        {
            try
            {
                List<edCuentaBancaria> slenUsuario = new List<edCuentaBancaria>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_cuentabancaria", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_nombres", MySqlDbType.VarChar, 50).Value = adnombres;
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edCuentaBancaria enUsuario = null;
                            int pos_idcuentabancaria = mdrd.GetOrdinal("idcuentabancaria");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidos = mdrd.GetOrdinal("v_apellidos");
                            int pos_itipocuenta = mdrd.GetOrdinal("i_tipo_cuenta");
                            int pos_imoneda = mdrd.GetOrdinal("i_moneda");
                            int pos_ibanco = mdrd.GetOrdinal("i_banco");
                            int pos_vbanco = mdrd.GetOrdinal("v_banco");
                            int pos_vnumerocuenta = mdrd.GetOrdinal("v_numero_cuenta");
                            int pos_vnombrecuenta = mdrd.GetOrdinal("v_nombre_cuenta");
                            int pos_itipodeclaracion = mdrd.GetOrdinal("i_tipo_declaracion");
                            int pos_vtitular = mdrd.GetOrdinal("v_titular");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            int pos_vhoraregistro = mdrd.GetOrdinal("v_horaregistro");
                            int pos_idusuarioModificacion = mdrd.GetOrdinal("idusuario_Modificacion");
                            while (mdrd.Read())
                            {
                                enUsuario = new edCuentaBancaria();
                                enUsuario.idcuentabancaria = (mdrd.IsDBNull(pos_idcuentabancaria) ? 0 : mdrd.GetInt32(pos_idcuentabancaria));
                                enUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                enUsuario.vnombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                enUsuario.vpellidos = (mdrd.IsDBNull(pos_vapellidos) ? "-" : mdrd.GetString(pos_vapellidos));
                                enUsuario.itipocuenta = (mdrd.IsDBNull(pos_itipocuenta) ? 0 : mdrd.GetInt32(pos_itipocuenta));
                                enUsuario.imoneda = (mdrd.IsDBNull(pos_imoneda) ? 0 : mdrd.GetInt32(pos_imoneda));
                                enUsuario.ibanco = (mdrd.IsDBNull(pos_ibanco) ? 0 : mdrd.GetInt32(pos_ibanco));
                                enUsuario.vbanco = (mdrd.IsDBNull(pos_vbanco) ? "-" : mdrd.GetString(pos_vbanco));
                                enUsuario.vnumerocuenta = (mdrd.IsDBNull(pos_vnumerocuenta) ? "-" : mdrd.GetString(pos_vnumerocuenta));
                                enUsuario.vnombrecuenta = (mdrd.IsDBNull(pos_vnombrecuenta) ? "-" : mdrd.GetString(pos_vnombrecuenta));
                                enUsuario.itipodeclaracion = (mdrd.IsDBNull(pos_itipodeclaracion) ? 0 : mdrd.GetInt32(pos_itipodeclaracion));
                                enUsuario.vtitular = (mdrd.IsDBNull(pos_vtitular) ? "-" : mdrd.GetString(pos_vtitular));
                                enUsuario.bestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt32(pos_bestado));
                                enUsuario.dtfecharegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                enUsuario.vhoraregistro = (mdrd.IsDBNull(pos_vhoraregistro) ? "-" : mdrd.GetString(pos_vhoraregistro));
                                enUsuario.idusuariomodificacion = (mdrd.IsDBNull(pos_idusuarioModificacion) ? 0 : mdrd.GetInt32(pos_idusuarioModificacion));
                                slenUsuario.Add(enUsuario);
                            }
                        }
                    }
                    return slenUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public int adOperaciondivisa(int adtipoOperacion, int adiddivisa, int adidusuario, int aditipobusqueda,
            decimal addmonto, decimal addsolesventa, decimal addsolescompra, decimal adddolaresventa, decimal adddolarescompra,
            int aditipopromocion, string addtfecha, string advhora, string addtfecharegistro)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_operacion_divisa", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_itipo_operacion", MySqlDbType.Int32).Value = adtipoOperacion;
                cmd.Parameters.Add("_iddivisa", MySqlDbType.Int32).Value = adiddivisa;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_i_tipo_busqueda", MySqlDbType.Int32).Value = aditipobusqueda;
                cmd.Parameters.Add("_d_monto", MySqlDbType.Decimal).Value = addmonto;
                cmd.Parameters.Add("_d_soles_venta", MySqlDbType.Decimal).Value = addsolesventa;
                cmd.Parameters.Add("_d_soles_compra", MySqlDbType.Decimal).Value = addsolescompra;
                cmd.Parameters.Add("_d_dolares_venta", MySqlDbType.Decimal).Value = adddolaresventa;
                cmd.Parameters.Add("_d_dolares_compra", MySqlDbType.Decimal).Value = adddolarescompra;
                cmd.Parameters.Add("_i_tipo_promocion", MySqlDbType.Int32).Value = aditipopromocion;
                cmd.Parameters.Add("_dt_fecha", MySqlDbType.VarChar, 25).Value = addtfecha;
                cmd.Parameters.Add("_v_hora", MySqlDbType.VarChar, 25).Value = advhora;
                cmd.Parameters.Add("_dt_fecharegistro", MySqlDbType.VarChar, 25).Value = addtfecharegistro;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edDivisa> adListardivisa(int adiddivisa, string addtfecha)
        {
            try
            {
                List<edDivisa> slenUsuario = new List<edDivisa>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_divisa", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_iddivisa", MySqlDbType.Int32).Value = adiddivisa;
                    cmd.Parameters.Add("_dt_fecha", MySqlDbType.VarChar, 50).Value = addtfecha;
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edDivisa enUsuario = null;
                            int pos_iddivisa = mdrd.GetOrdinal("iddivisa");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_itipobusqueda = mdrd.GetOrdinal("i_tipo_busqueda");
                            int pos_dmonto = mdrd.GetOrdinal("d_monto");
                            int pos_dsolesventa = mdrd.GetOrdinal("d_soles_venta");
                            int pos_dsolescompra = mdrd.GetOrdinal("d_soles_compra");
                            int pos_ddolaresventa = mdrd.GetOrdinal("d_dolares_venta");
                            int pos_ddolarescompra = mdrd.GetOrdinal("d_dolares_compra");
                            int pos_itipopromocion = mdrd.GetOrdinal("i_tipo_promocion");
                            int pos_dtfecha = mdrd.GetOrdinal("dt_fecha");
                            int pos_vhora = mdrd.GetOrdinal("v_hora");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            int pos_dtfechamodificacion = mdrd.GetOrdinal("dt_fechamodificacion");
                            int pos_vhoramodificacion = mdrd.GetOrdinal("v_horamodificacion");
                            while (mdrd.Read())
                            {
                                enUsuario = new edDivisa();
                                enUsuario.iddivisa = (mdrd.IsDBNull(pos_iddivisa) ? 0 : mdrd.GetInt32(pos_iddivisa));
                                enUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                enUsuario.itipobusqueda = (mdrd.IsDBNull(pos_itipobusqueda) ? 0 : mdrd.GetInt32(pos_itipobusqueda));
                                enUsuario.dmonto = (mdrd.IsDBNull(pos_dmonto) ? 0 : mdrd.GetDecimal(pos_dmonto));
                                enUsuario.dsolesventa = (mdrd.IsDBNull(pos_dsolesventa) ? 0 : mdrd.GetDecimal(pos_dsolesventa));
                                enUsuario.dsolescompra = (mdrd.IsDBNull(pos_dsolescompra) ? 0 : mdrd.GetDecimal(pos_dsolescompra));
                                enUsuario.ddolaresventa = (mdrd.IsDBNull(pos_ddolaresventa) ? 0 : mdrd.GetDecimal(pos_ddolaresventa));
                                enUsuario.ddolarescompra = (mdrd.IsDBNull(pos_ddolarescompra) ? 0 : mdrd.GetDecimal(pos_ddolarescompra));
                                enUsuario.itipopromocion = (mdrd.IsDBNull(pos_itipopromocion) ? 0 : mdrd.GetInt32(pos_itipopromocion));
                                enUsuario.dtfecha = (mdrd.IsDBNull(pos_dtfecha) ? "-" : mdrd.GetString(pos_dtfecha));
                                enUsuario.vhora = (mdrd.IsDBNull(pos_vhora) ? "-" : mdrd.GetString(pos_vhora));
                                enUsuario.dtfecharegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                enUsuario.dtfechamodificacion = (mdrd.IsDBNull(pos_dtfechamodificacion) ? "-" : mdrd.GetString(pos_dtfechamodificacion));
                                enUsuario.horamodificacion = (mdrd.IsDBNull(pos_vhoramodificacion) ? "-" : mdrd.GetString(pos_vhoramodificacion));
                                slenUsuario.Add(enUsuario);
                            }
                        }
                    }
                    return slenUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public int adOperaciontransaccion(int aditipooperacion, int adidtransaccion, int adidusuario, int adiddivisa,
            int adidpromocion, int adidcuentabancaria, int adidusuarioadministrador, string addtfecha, int aditipodivisa,
            decimal adddolaresventa, decimal adddolarescompra, string advhora, string advimagenruta, int aditipoenvio,
            int adiestado, string advoperacion, int adiorigenfondo, decimal addenvio, decimal addrecibo, int aditipocambio,
            int aditipotrasaccion, decimal addigv, string advbancoreceptor, string addtfecharegistro, string addtfechamodificacion,
            int adidusuarioModificacion)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_operacion_transaccion", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_itipo_operacion", MySqlDbType.Int32).Value = aditipooperacion;
                cmd.Parameters.Add("_idtransaccion", MySqlDbType.Int32).Value = adidtransaccion;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_iddivisa", MySqlDbType.Int32).Value = adiddivisa;
                cmd.Parameters.Add("_idpromocion", MySqlDbType.Int32).Value = adidpromocion;
                cmd.Parameters.Add("_idcuentabancaria", MySqlDbType.Int32).Value = adidcuentabancaria;
                cmd.Parameters.Add("_idusuario_administrador", MySqlDbType.Int32).Value = adidusuarioadministrador;
                cmd.Parameters.Add("_dt_fecha", MySqlDbType.VarChar, 25).Value = addtfecha;
                cmd.Parameters.Add("_i_tipo_divisa", MySqlDbType.Int32).Value = aditipodivisa;
                cmd.Parameters.Add("_d_dolares_venta", MySqlDbType.Decimal).Value = adddolaresventa;
                cmd.Parameters.Add("_d_dolares_compra", MySqlDbType.Decimal).Value = adddolarescompra;
                cmd.Parameters.Add("_v_hora", MySqlDbType.VarChar, 25).Value = advhora;
                cmd.Parameters.Add("_v_imagen_ruta", MySqlDbType.VarChar, 500).Value = advimagenruta;
                cmd.Parameters.Add("_i_tipo_envio", MySqlDbType.Int32).Value = aditipoenvio;
                cmd.Parameters.Add("_i_estado", MySqlDbType.Int32).Value = adiestado;
                cmd.Parameters.Add("_v_operacion", MySqlDbType.VarChar, 150).Value = advoperacion;
                cmd.Parameters.Add("_i_origen_fondo", MySqlDbType.Int32).Value = adiorigenfondo;
                cmd.Parameters.Add("_d_envio", MySqlDbType.Decimal).Value = addenvio;
                cmd.Parameters.Add("_d_recibo", MySqlDbType.Decimal).Value = addrecibo;
                cmd.Parameters.Add("_i_tipo_cambio", MySqlDbType.Int32).Value = aditipocambio;
                cmd.Parameters.Add("_i_tipo_trasaccion", MySqlDbType.Int32).Value = aditipotrasaccion;
                cmd.Parameters.Add("_d_igv", MySqlDbType.Decimal).Value = addigv;
                cmd.Parameters.Add("_v_banco_receptor", MySqlDbType.VarChar, 150).Value = advbancoreceptor;
                cmd.Parameters.Add("_dt_fecharegistro", MySqlDbType.VarChar, 25).Value = addtfecharegistro;
                cmd.Parameters.Add("_dt_fechamodificacion", MySqlDbType.VarChar, 25).Value = addtfechamodificacion;
                cmd.Parameters.Add("_idusuario_Modificacion", MySqlDbType.Int32).Value = adidusuarioModificacion;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edTransaccion> adListartransaccion(int adidusuario, string addtfecha, int adestado)
        {
            try
            {
                List<edTransaccion> slenUsuario = new List<edTransaccion>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_transaccion", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_dt_fecha", MySqlDbType.VarChar, 50).Value = addtfecha;
                    cmd.Parameters.Add("_itipo", MySqlDbType.Int32).Value = adestado;
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edTransaccion enUsuario = null;
                            int pos_idtransaccion = mdrd.GetOrdinal("idtransaccion");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidos = mdrd.GetOrdinal("v_apellidos");
                            int pos_iddivisa = mdrd.GetOrdinal("iddivisa");
                            int pos_idpromocion = mdrd.GetOrdinal("idpromocion");
                            int pos_idcuentabancaria = mdrd.GetOrdinal("idcuentabancaria");
                            int pos_idusuarioadministrador = mdrd.GetOrdinal("idusuario_administrador");
                            int pos_dtfecha = mdrd.GetOrdinal("dt_fecha");
                            int pos_itipodivisa = mdrd.GetOrdinal("i_tipo_divisa");
                            int pos_ddolaresventa = mdrd.GetOrdinal("d_dolares_venta");
                            int pos_ddolarescompra = mdrd.GetOrdinal("d_dolares_compra");
                            int pos_vhora = mdrd.GetOrdinal("v_hora");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_vimagenruta = mdrd.GetOrdinal("v_imagen_ruta");
                            int pos_itipoenvio = mdrd.GetOrdinal("i_tipo_envio");
                            int pos_iestado = mdrd.GetOrdinal("i_estado");
                            int pos_voperacion = mdrd.GetOrdinal("v_operacion");
                            int pos_iorigenfondo = mdrd.GetOrdinal("i_origen_fondo");
                            int pos_denvio = mdrd.GetOrdinal("d_envio");
                            int pos_drecibo = mdrd.GetOrdinal("d_recibo");
                            int pos_itipocambio = mdrd.GetOrdinal("i_tipo_cambio");
                            int pos_itipotrasaccion = mdrd.GetOrdinal("i_tipo_trasaccion");
                            int pos_digv = mdrd.GetOrdinal("d_igv");
                            int pos_vbancoreceptor = mdrd.GetOrdinal("v_banco_receptor");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            while (mdrd.Read())
                            {
                                enUsuario = new edTransaccion();
                                enUsuario.idtransaccion = (mdrd.IsDBNull(pos_idtransaccion) ? 0 : mdrd.GetInt32(pos_idtransaccion));
                                enUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                enUsuario.vnombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                enUsuario.vapellidos = (mdrd.IsDBNull(pos_vapellidos) ? "-" : mdrd.GetString(pos_vapellidos));
                                enUsuario.iddivisa = (mdrd.IsDBNull(pos_iddivisa) ? 0 : mdrd.GetInt32(pos_iddivisa));
                                enUsuario.idpromocion = (mdrd.IsDBNull(pos_idpromocion) ? 0 : mdrd.GetInt32(pos_idpromocion));
                                enUsuario.idcuentabancaria = (mdrd.IsDBNull(pos_idcuentabancaria) ? 0 : mdrd.GetInt32(pos_idcuentabancaria));
                                enUsuario.idusuarioadministrador = (mdrd.IsDBNull(pos_idusuarioadministrador) ? 0 : mdrd.GetInt32(pos_idusuarioadministrador));
                                enUsuario.dtfecha = (mdrd.IsDBNull(pos_dtfecha) ? "-" : mdrd.GetString(pos_dtfecha));
                                enUsuario.itipodivisa = (mdrd.IsDBNull(pos_itipodivisa) ? 0 : mdrd.GetInt32(pos_itipodivisa));
                                enUsuario.ddolaresventa = (mdrd.IsDBNull(pos_ddolaresventa) ? 0 : mdrd.GetDecimal(pos_ddolaresventa));
                                enUsuario.ddolarescompra = (mdrd.IsDBNull(pos_ddolarescompra) ? 0 : mdrd.GetDecimal(pos_ddolarescompra));
                                enUsuario.vhora = (mdrd.IsDBNull(pos_vhora) ? "-" : mdrd.GetString(pos_vhora));
                                enUsuario.vimagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                enUsuario.vimagenruta = (mdrd.IsDBNull(pos_vimagenruta) ? "-" : mdrd.GetString(pos_vimagenruta));
                                enUsuario.itipoenvio = (mdrd.IsDBNull(pos_itipoenvio) ? 0 : mdrd.GetInt32(pos_itipoenvio));
                                enUsuario.iestado = (mdrd.IsDBNull(pos_iestado) ? 0 : mdrd.GetInt32(pos_iestado));
                                enUsuario.voperacion = (mdrd.IsDBNull(pos_voperacion) ? "-" : mdrd.GetString(pos_voperacion));
                                enUsuario.iorigenfondo = (mdrd.IsDBNull(pos_iorigenfondo) ? 0 : mdrd.GetInt32(pos_iorigenfondo));
                                enUsuario.denvio = (mdrd.IsDBNull(pos_denvio) ? 0 : mdrd.GetInt32(pos_denvio));
                                enUsuario.drecibo = (mdrd.IsDBNull(pos_drecibo) ? 0 : mdrd.GetInt32(pos_drecibo));
                                enUsuario.itipocambio = (mdrd.IsDBNull(pos_itipocambio) ? 0 : mdrd.GetInt32(pos_itipocambio));
                                enUsuario.itipotrasaccion = (mdrd.IsDBNull(pos_itipotrasaccion) ? 0 : mdrd.GetInt32(pos_itipotrasaccion));
                                enUsuario.digv = (mdrd.IsDBNull(pos_digv) ? 0 : mdrd.GetInt32(pos_digv));
                                enUsuario.vbancoreceptor = (mdrd.IsDBNull(pos_vbancoreceptor) ? "-" : mdrd.GetString(pos_vbancoreceptor));
                                enUsuario.dtfecharegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                slenUsuario.Add(enUsuario);
                            }
                        }
                    }
                    return slenUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }


    }
}
