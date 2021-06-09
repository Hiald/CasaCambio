using MySql.Data.MySqlClient;
using CambioTotalED;
using System;
using System.Data;

namespace CambioTotalAD
{
    public class adOperacion : ad_aglobal
    {
        public adOperacion(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adOperacionCuentaBancaria(int adtipoOperacion, int adidCuentBanc, int adidusuario, int aditipocuenta, int adimoneda
                                        , int adibanco, string adbanco, string adnumerocuenta,string adnombrecuenta, int itipodeclaracion
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


    }
}
