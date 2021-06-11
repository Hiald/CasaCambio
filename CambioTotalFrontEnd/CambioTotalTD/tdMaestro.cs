using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CambioTotalAD;
using CambioTotalED;
using MySql.Data.MySqlClient;

namespace CambioTotalTD
{
    public class tdMaestro:td_aglobal
    {
        adMaestro iadMaestro;

        public List<edMaestro> tdListarMaestro(int tdidMaestro)
        {
            List<edMaestro> renUsuario = new List<edMaestro>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadMaestro = new adMaestro(con);
                        renUsuario = iadMaestro.adListarMaestro(tdidMaestro);
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
