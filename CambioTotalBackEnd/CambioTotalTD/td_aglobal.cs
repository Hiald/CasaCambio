using System;
using System.Configuration;

namespace CambioTotalTD
{
    public class td_aglobal
    {
        public String mysqlConexion { get; set; }

        public td_aglobal()
        {
            mysqlConexion = ConfigurationManager.ConnectionStrings["cnMysql"].ConnectionString;
        }
    }
}
