using MySql.Data.MySqlClient;
using CambioTotalED;
using System;
using System.Data;
using System.Collections.Generic;

namespace CambioTotalAD
{
    public class adMaestro : ad_aglobal
    {
        public adMaestro(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public List<edMaestro> adListarMaestro(int idmaestro)
        {
            try
            {
                List<edMaestro> slenUsuario = new List<edMaestro>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_maestro", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idmaestro", MySqlDbType.Int32).Value = idmaestro;
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edMaestro enUsuario = null;
                            int pos_idparametro = mdrd.GetOrdinal("idparametro");
                            int pos_idmaestro = mdrd.GetOrdinal("idmaestro");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            while (mdrd.Read())
                            {
                                enUsuario = new edMaestro();
                                enUsuario.idparametro = (mdrd.IsDBNull(pos_idparametro) ? 0 : mdrd.GetInt32(pos_idparametro));
                                enUsuario.idmaestro = (mdrd.IsDBNull(pos_idmaestro) ? 0 : mdrd.GetInt32(pos_idmaestro));
                                enUsuario.snombre = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                enUsuario.sdescripcion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                enUsuario.bestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt32(pos_bestado));
                                enUsuario.sdtfecharegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
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

        //falta completar en la base de datos

    }
}
