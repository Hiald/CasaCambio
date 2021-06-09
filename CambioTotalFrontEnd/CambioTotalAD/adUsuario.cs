﻿using MySql.Data.MySqlClient;
using CambioTotalED;
using System;
using System.Data;

namespace CambioTotalAD
{
    public class adUsuario : ad_aglobal
    {
        public adUsuario(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public edUsuario adObtenerUsuario(string adcorreo, string adclave)
        {
            try
            {
                edUsuario senUsuario = null;
                using (MySqlCommand cmd = new MySqlCommand("sp_obtener_usuario", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.VarChar, 100).Value = adcorreo;
                    cmd.Parameters.Add("_clave", MySqlDbType.VarChar, 500).Value = adclave;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidos = mdrd.GetOrdinal("v_apellidos");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_vimagenruta = mdrd.GetOrdinal("v_imagen_ruta");
                            int pos_vcelular1 = mdrd.GetOrdinal("v_celular1");
                            int pos_vtelefono = mdrd.GetOrdinal("v_telefono");
                            int pos_itipodocumento = mdrd.GetOrdinal("i_tipodocumento");
                            int pos_vdocumento = mdrd.GetOrdinal("v_documento");
                            int pos_vcorreo = mdrd.GetOrdinal("v_correo");
                            int pos_itipousuario = mdrd.GetOrdinal("i_tipo_usuario");
                            int pos_resultado = mdrd.GetOrdinal("_resultado");

                            while (mdrd.Read())
                            {
                                senUsuario = new edUsuario();
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.snombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                senUsuario.sapellidos = (mdrd.IsDBNull(pos_vapellidos) ? "-" : mdrd.GetString(pos_vapellidos));
                                senUsuario.simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                senUsuario.simagenRuta = (mdrd.IsDBNull(pos_vimagenruta) ? "/img/vacio.png" : mdrd.GetString(pos_vimagenruta));
                                senUsuario.scelular = (mdrd.IsDBNull(pos_vcelular1) ? "-" : mdrd.GetString(pos_vcelular1));
                                senUsuario.stelefono = (mdrd.IsDBNull(pos_vtelefono) ? "-" : mdrd.GetString(pos_vtelefono));
                                senUsuario.itipodocumento = (mdrd.IsDBNull(pos_itipodocumento) ? 0 : mdrd.GetInt32(pos_itipodocumento));
                                senUsuario.sdocumento = (mdrd.IsDBNull(pos_vdocumento) ? "-" : mdrd.GetString(pos_vdocumento));
                                senUsuario.scorreo = (mdrd.IsDBNull(pos_vcorreo) ? "-" : mdrd.GetString(pos_vcorreo));
                                senUsuario.itipousuario = (mdrd.IsDBNull(pos_itipousuario) ? 0 : mdrd.GetInt32(pos_itipousuario));
                                senUsuario._resultado = (mdrd.IsDBNull(pos_resultado) ? 0 : mdrd.GetInt32(pos_resultado));
                            }
                        }
                    }
                    return senUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public int adInsertarUsuario(string adnombres, string adapellidos, int adtipodocumento, string addocumento,
            string adfecharegistro, string adhoraregistro, int adtipousuario, string adcorreo, string adclave, string adtoken)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_usuario", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_v_nombres", MySqlDbType.VarChar, 50).Value = adnombres;
                cmd.Parameters.Add("_v_apellidos", MySqlDbType.VarChar, 45).Value = adapellidos;
                cmd.Parameters.Add("_i_tipodocumento", MySqlDbType.Int32).Value = adtipodocumento;
                cmd.Parameters.Add("_v_documento", MySqlDbType.VarChar, 20).Value = addocumento;
                cmd.Parameters.Add("_dt_fecharegistro", MySqlDbType.VarChar, 25).Value = adfecharegistro;
                cmd.Parameters.Add("_v_horaregistro", MySqlDbType.VarChar, 25).Value = adhoraregistro;
                cmd.Parameters.Add("_i_tipo_usuario", MySqlDbType.Int32).Value = adtipousuario;
                cmd.Parameters.Add("_v_correo", MySqlDbType.VarChar, 100).Value = adcorreo;
                cmd.Parameters.Add("_v_clave", MySqlDbType.VarChar, 500).Value = adclave;
                cmd.Parameters.Add("_v_token", MySqlDbType.VarChar, 500).Value = adtoken;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        //adtipoactualizar: 1 par actualizar, 2 para deshabilitar la cuenta
        public int adActualizarAcceso(int adtipoactualizar, int adidusuario, string adcorreo, string adclave)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_acceso", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_tipoactualizar", MySqlDbType.Int32).Value = adtipoactualizar;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_v_correo", MySqlDbType.VarChar, 100).Value = adcorreo;
                cmd.Parameters.Add("_v_clave", MySqlDbType.VarChar, 500).Value = adclave;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adActualizarClave(int adidusuario, string adnuevaclave)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_clave", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_nuevaclave", MySqlDbType.VarChar, 500).Value = adnuevaclave;
                result = Convert.ToInt32(cmd.ExecuteNonQuery());
                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adActualizarCuenta(int adidusuario, int adidprovincia, int adidciudad, int adiddistrito, string addireccion, string adnombres,
                                    string adapellidos, int adigenero, string adimagenruta, string adimagendniruta1, string adimagendniruta2, string adcelular1,
                                    string adcelular2, string adtelefono, int aditipodocumento, string addocumento, string adubigeo, string adfechanacimiento,
                                    string adfechamodificacion, string adhoramodificacion, int adidusuariomod)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_usuario", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_idprovincia", MySqlDbType.Int32).Value = adidprovincia;
                cmd.Parameters.Add("_idciudad", MySqlDbType.Int32).Value = adidciudad;
                cmd.Parameters.Add("_iddistrito", MySqlDbType.Int32).Value = adiddistrito;
                cmd.Parameters.Add("_v_direccion", MySqlDbType.VarChar, 50).Value = addireccion;
                cmd.Parameters.Add("_v_nombres", MySqlDbType.VarChar, 50).Value = adnombres;
                cmd.Parameters.Add("_v_apellidos", MySqlDbType.VarChar, 45).Value = adapellidos;
                cmd.Parameters.Add("_i_genero", MySqlDbType.VarChar, 1).Value = adigenero;
                cmd.Parameters.Add("_v_imagen_ruta", MySqlDbType.VarChar, 500).Value = adimagenruta;
                cmd.Parameters.Add("_v_imagen_dni_ruta_1", MySqlDbType.VarChar, 500).Value = adimagendniruta1;
                cmd.Parameters.Add("_v_imagen_dni_ruta_2", MySqlDbType.VarChar, 500).Value = adimagendniruta2;
                cmd.Parameters.Add("_v_celular1", MySqlDbType.VarChar, 20).Value = adcelular1;
                cmd.Parameters.Add("_v_celular2", MySqlDbType.VarChar, 20).Value = adcelular2;
                cmd.Parameters.Add("_v_telefono", MySqlDbType.VarChar, 10).Value = adtelefono;
                cmd.Parameters.Add("_i_tipodocumento", MySqlDbType.Int32).Value = aditipodocumento;
                cmd.Parameters.Add("_v_documento", MySqlDbType.VarChar, 20).Value = addocumento;
                cmd.Parameters.Add("_v_ubigeo", MySqlDbType.VarChar, 20).Value = adubigeo;
                cmd.Parameters.Add("_d_fechanac", MySqlDbType.VarChar, 20).Value = adfechanacimiento;
                cmd.Parameters.Add("_dt_fechamodificacion", MySqlDbType.VarChar, 20).Value = adfechamodificacion;
                cmd.Parameters.Add("_v_horamodificacion", MySqlDbType.VarChar, 25).Value = adhoramodificacion;
                cmd.Parameters.Add("_idusuario_Modificacion", MySqlDbType.Int32).Value = adidusuariomod;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.ventaAD, UtlConstantes.LogNamespace_ventaAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public edUsuario adFiltrarUsuario(string adusuario)
        {
            try
            {
                edUsuario senUsuario = null;
                using (MySqlCommand cmd = new MySqlCommand("sp_filtrar_usuario", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adusuario;
                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_idprovincia = mdrd.GetOrdinal("idprovincia");
                            int pos_idciudad = mdrd.GetOrdinal("idciudad");
                            int pos_iddistrito = mdrd.GetOrdinal("iddistrito");
                            int pos_vdireccion = mdrd.GetOrdinal("v_direccion");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidos = mdrd.GetOrdinal("v_apellidos");
                            int pos_igenero = mdrd.GetOrdinal("i_genero");
                            int pos_vimagenruta = mdrd.GetOrdinal("v_imagen_ruta");
                            int pos_vimagendniruta1 = mdrd.GetOrdinal("v_imagen_dni_ruta_1");
                            int pos_vimagendniruta2 = mdrd.GetOrdinal("v_imagen_dni_ruta_2");
                            int pos_vcelular1 = mdrd.GetOrdinal("v_celular1");
                            int pos_vcelular2 = mdrd.GetOrdinal("v_celular2");
                            int pos_vtelefono = mdrd.GetOrdinal("v_telefono");
                            int pos_itipodocumento = mdrd.GetOrdinal("i_tipodocumento");
                            int pos_vdocumento = mdrd.GetOrdinal("v_documento");
                            int pos_dfechanac = mdrd.GetOrdinal("d_fechanac");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            int pos_vhoraregistro = mdrd.GetOrdinal("v_horaregistro");

                            while (mdrd.Read())
                            {
                                senUsuario = new edUsuario();
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.idprovincia = (mdrd.IsDBNull(pos_idprovincia) ? 0 : mdrd.GetInt32(pos_idprovincia));
                                senUsuario.idciudad = (mdrd.IsDBNull(pos_idciudad) ? 0 : mdrd.GetInt32(pos_idciudad));
                                senUsuario.iddistrito = (mdrd.IsDBNull(pos_iddistrito) ? 0 : mdrd.GetInt32(pos_iddistrito));
                                senUsuario.sdireccion = (mdrd.IsDBNull(pos_vdireccion) ? "-" : mdrd.GetString(pos_vdireccion));
                                senUsuario.snombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                senUsuario.sapellidos = (mdrd.IsDBNull(pos_vapellidos) ? "-" : mdrd.GetString(pos_vapellidos));
                                senUsuario.igenero = (mdrd.IsDBNull(pos_igenero) ? 0 : mdrd.GetInt32(pos_igenero));
                                senUsuario.simagenRuta = (mdrd.IsDBNull(pos_vimagenruta) ? "/img/vacio.png" : mdrd.GetString(pos_vimagenruta));
                                senUsuario.simagendni1 = (mdrd.IsDBNull(pos_vimagendniruta1) ? "/img/vacio.png" : mdrd.GetString(pos_vimagendniruta1));
                                senUsuario.simagendni2 = (mdrd.IsDBNull(pos_vimagendniruta2) ? "/img/vacio.png" : mdrd.GetString(pos_vimagendniruta2));
                                senUsuario.scelular = (mdrd.IsDBNull(pos_vcelular1) ? "-" : mdrd.GetString(pos_vcelular1));
                                senUsuario.scelular2 = (mdrd.IsDBNull(pos_vcelular2) ? "-" : mdrd.GetString(pos_vcelular2));
                                senUsuario.stelefono = (mdrd.IsDBNull(pos_vtelefono) ? "-" : mdrd.GetString(pos_vtelefono));
                                senUsuario.itipodocumento = (mdrd.IsDBNull(pos_itipodocumento) ? 0 : mdrd.GetInt32(pos_itipodocumento));
                                senUsuario.sdocumento = (mdrd.IsDBNull(pos_vdocumento) ? "-" : mdrd.GetString(pos_vdocumento));
                                senUsuario.sfechanacimiento = (mdrd.IsDBNull(pos_dfechanac) ? "-" : mdrd.GetString(pos_dfechanac));
                                senUsuario.iestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt32(pos_bestado));
                                senUsuario.sfecharegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                senUsuario.shoraregistro = (mdrd.IsDBNull(pos_vhoraregistro) ? "-" : mdrd.GetString(pos_vhoraregistro));
                            }
                        }
                    }
                    return senUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }
        
        #region POR AGREGAR
        
        public int test(int adidalumno, string addireccionip, string addireccionmac, int adtipoconexion)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("spalumno_registro", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@_idalumno", MySqlDbType.Int32).Value = adidalumno;
                cmd.Parameters.Add("@_direccionip", MySqlDbType.VarChar, 150).Value = addireccionip;
                cmd.Parameters.Add("@_direccionmac", MySqlDbType.VarChar, 150).Value = addireccionmac;
                cmd.Parameters.Add("@_tipoconexion", MySqlDbType.Int32).Value = adtipoconexion;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        #endregion
    }
}
