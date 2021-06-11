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
    public class tdOperacion:td_aglobal
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
                        iRespuesta = iadOperacion.adOperacionCuentaBancaria(tdtipoOperacion,tdidCuentBanc, tdidusuario, tditipocuenta, tdimoneda
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

    }
}
