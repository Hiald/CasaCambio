using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace FrontendUtil
{
    public class UtlAuditoria
    {
        private const string SESSION_IDUSUARIO = "SESSION_IDUSUARIO";
        private const string SESSION_SNOMBRE = "SESSION_SNOMBRE";
        private const string SESSION_APELLIDO = "SESSION_SAPELLIDO";
        private const string SESSION_SEMAIL = "SESSION_SEMAIL";
        private const string SESSION_ITIPODOC = "SESSION_ITIPODOC";
        private const string SESSION_SDOCUMENTO = "SESSION_SDOCUMENTO";
        private const string SESSION_TIPOUSUARIO = "SESSION_TIPOUSUARIO";
        private const string SESSION_CELULAR = "SESSION_CELULAR";
        private const string SESSION_IMAGENUSUARIO = "SESSION_IMAGENUSUARIO";
        private const string SESSION_SRUC = "SESSION_SRUC";
        private const string SESSION_SRAZONSOCIAL = "SESSION_SRAZONSOCIAL";

        #region "Obtiene Datos del Usuario"

        public static int ObtenerIdUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_IDUSUARIO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDUSUARIO].ToString()));
        }
        public static string ObtenerNombre()
        {
            return ((HttpContext.Current.Session[SESSION_SNOMBRE] == null) ? "-" : HttpContext.Current.Session[SESSION_SNOMBRE].ToString());
        }
        public static string ObtenerApellido()
        {
            return ((HttpContext.Current.Session[SESSION_APELLIDO] == null) ? "-" : HttpContext.Current.Session[SESSION_APELLIDO].ToString());
        }
        public static string ObtenerEmail()
        {
            return ((HttpContext.Current.Session[SESSION_SEMAIL] == null) ? "" : HttpContext.Current.Session[SESSION_SEMAIL].ToString());
        }
        public static int ObtenerTipoDocumento()
        {
            return ((HttpContext.Current.Session[SESSION_ITIPODOC] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_ITIPODOC].ToString()));
        }
        public static string ObtenerDocumento()
        {
            return ((HttpContext.Current.Session[SESSION_SDOCUMENTO] == null) ? "" : HttpContext.Current.Session[SESSION_SDOCUMENTO].ToString());
        }
        public static int ObtenerTipoUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_TIPOUSUARIO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_TIPOUSUARIO].ToString()));
        }
        public static string ObtenerCelular()
        {
            return ((HttpContext.Current.Session[SESSION_CELULAR] == null) ? "" : HttpContext.Current.Session[SESSION_CELULAR].ToString());
        }
        public static string ObtenerImagenUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_IMAGENUSUARIO] == null) ? "/imagenalumno/vacio.png" : HttpContext.Current.Session[SESSION_IMAGENUSUARIO].ToString());
        }
        public static string ObtenerRUC()
        {
            return ((HttpContext.Current.Session[SESSION_SRUC] == null) ? "-" : HttpContext.Current.Session[SESSION_SRUC].ToString());
        }
        public static string ObtenerRazonSocial()
        {
            return ((HttpContext.Current.Session[SESSION_SRAZONSOCIAL] == null) ? "-" : HttpContext.Current.Session[SESSION_SRAZONSOCIAL].ToString());
        }
        public static string ObtenerFechaSistema()
        {
            return DateTime.Now.ToShortDateString();
        }

        public static void SetSessionValues(Dictionary<string, string> DVariables)
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDUSUARIO] = DVariables["IDUSUARIO"];
                HttpContext.Current.Session[SESSION_SNOMBRE] = DVariables["SNOMBRE"];
                HttpContext.Current.Session[SESSION_APELLIDO] = DVariables["APELLIDO"];
                HttpContext.Current.Session[SESSION_SEMAIL] = DVariables["SEMAIL"];
                HttpContext.Current.Session[SESSION_ITIPODOC] = DVariables["ITIPODOC"];
                HttpContext.Current.Session[SESSION_SDOCUMENTO] = DVariables["SDOCUMENTO"];
                HttpContext.Current.Session[SESSION_TIPOUSUARIO] = DVariables["TIPOUSUARIO"];
                HttpContext.Current.Session[SESSION_CELULAR] = DVariables["CELULAR"];
                //HttpContext.Current.Session[SESSION_IMAGENUSUARIO] = DVariables["IMAGENUSUARIO"];
                HttpContext.Current.Session[SESSION_SRUC] = DVariables["SRUC"];
                HttpContext.Current.Session[SESSION_SRAZONSOCIAL] = DVariables["SRAZONSOCIAL"];
                HttpContext.Current.Session.Timeout = 24 * 60;
            }
            catch (ArgumentOutOfRangeException kfe)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "SetSessionValues", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", kfe.StackTrace.ToString(), kfe.Message.ToString());
                throw new ArgumentOutOfRangeException("Se requiere que se tenga todas las llaves :  idUsuario, sCodUsuario , sNombreCompleto, sNombres, sCorreo, sMenu", kfe);
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "SetSessionValues", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        #endregion

        #region "Obtiene la dirección IP del Usuario"

        /// <summary>
        /// Obtiene la dirección IP del cliente, desde donde está conectado al sistema
        /// </summary>
        /// <returns>Devuelve la dirección IP</returns>
        public static string ObtenerDireccionIP()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

            //ADD IPLAN;
            if (ipAddress == "::1")
            {
                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        ipAddress = IPA.ToString();
                        break;
                    }
                }
            }

            return ipAddress;
        }

        #endregion

        #region "Obtiene la dirección MAC del Usuario"

        /// <summary>
        /// Obtiene la dirección MAC del cliente, desde donde está conectado el sistema
        /// </summary>
        /// <returns>Devuelve la dirección MAC</returns>
        public static string ObtenerDireccionMAC()
        {
            string strClientIP = ObtenerDireccionIP();
            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (mac_dest);
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        #endregion

        public static bool ValidarSession()
        {
            try
            {
                if (HttpContext.Current.Session[SESSION_IDUSUARIO] == null ||
                HttpContext.Current.Session[SESSION_SNOMBRE] == null ||
                HttpContext.Current.Session[SESSION_APELLIDO] == null ||
                HttpContext.Current.Session[SESSION_SEMAIL] == null ||
                HttpContext.Current.Session[SESSION_ITIPODOC] == null ||
                HttpContext.Current.Session[SESSION_SDOCUMENTO] == null ||
                HttpContext.Current.Session[SESSION_TIPOUSUARIO] == null ||
                HttpContext.Current.Session[SESSION_CELULAR] == null ||
                HttpContext.Current.Session[SESSION_SRUC] == null ||
                HttpContext.Current.Session[SESSION_SRAZONSOCIAL] == null
                //|| HttpContext.Current.Session[SESSION_IMAGENUSUARIO] == null
                )
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return false;
            }

        }

        public static bool ValidarMenu()
        {
            //string sMenu = UtlAuditoria.ObtenerMenu();
            return true;
        }

        public static bool CerrarSession()
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDUSUARIO] = null;
                HttpContext.Current.Session[SESSION_SNOMBRE] = null;
                HttpContext.Current.Session[SESSION_APELLIDO] = null;
                HttpContext.Current.Session[SESSION_SEMAIL] = null;
                HttpContext.Current.Session[SESSION_ITIPODOC] = null;
                HttpContext.Current.Session[SESSION_SDOCUMENTO] = null;
                HttpContext.Current.Session[SESSION_TIPOUSUARIO] = null;
                HttpContext.Current.Session[SESSION_CELULAR] = null;
                HttpContext.Current.Session[SESSION_SRUC] = null;
                HttpContext.Current.Session[SESSION_SRAZONSOCIAL] = null;
                HttpContext.Current.Session[SESSION_IMAGENUSUARIO] = null;
                return true;
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return false;
            }
        }

        public static int MostrarTiempoSesion()
        {
            try
            {
                var iTiempoSesion = HttpContext.Current.Session.Timeout;
                return iTiempoSesion;
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return 0;
            }

        }

    }
}
