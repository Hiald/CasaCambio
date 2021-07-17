﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CambioTotalED;
using CambioTotalFrontEnd.Filters;
using CambioTotalTD;
using FrontendUtil;

namespace CambioTotalFrontEnd.Controllers
{
    public class InicioController : Controller
    {
        tdUsuario itdusuario;
        tdOperacion itOperacion;

        public ActionResult Index()
        {
            ViewBag.DirIP = UtlAuditoria.ObtenerDireccionIP();
            return View();
        }

        public ActionResult inicio(int? ivalorsesion, string valorlogin)
        {
            if (ivalorsesion != 1 || ivalorsesion == null)
            {
                ivalorsesion = 0;
            }

            ViewBag.GIvalorError = valorlogin;
            ViewBag.GIvalorSesion = ivalorsesion;
            return View();
        }

        public ActionResult registro()
        {

            return View();
        }

        [ValidadorSesion]
        public ActionResult principal()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            string susuario = UtlAuditoria.ObtenerNombre() + " " + UtlAuditoria.ObtenerApellido();
            int gidusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
            ViewBag.GgradoUsuario = 0;
            ViewBag.GIdusuario = gidusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        [ValidadorSesion]
        public ActionResult miscuentas()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int gidusuario = UtlAuditoria.ObtenerIdUsuario();
            string susuario = UtlAuditoria.ObtenerNombre() + " " + UtlAuditoria.ObtenerApellido();

            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
            ViewBag.GgradoUsuario = 0;
            ViewBag.GIdusuario = gidusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }


        [ValidadorSesion]
        public ActionResult GestionTransacciones()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            string susuario = UtlAuditoria.ObtenerNombre() + " " + UtlAuditoria.ObtenerApellido();
            int gidusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
            ViewBag.GgradoUsuario = 0;
            ViewBag.GIdusuario = gidusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        /* LLAMADOS */
        [HttpPost]
        public JsonResult IniciarSesion(string wusuario, string wclave)
        {
            try
            {
                var objResultado = new object();
                edUsuario oedusuario = new edUsuario();
                itdusuario = new tdUsuario();
                oedusuario = itdusuario.tdObtenerUsuario(wusuario, wclave);

                if (oedusuario.scorreo == "0" || oedusuario.snombres == "0" || oedusuario.snombres == "0")
                {
                    objResultado = new
                    {
                        iResultado = -3,
                        iResultadoIns = "Usuario o clave incorrectos"
                    };
                    return Json(objResultado);
                }
                Dictionary<string, string> DVariables = new Dictionary<string, string>();
                DVariables["IDUSUARIO"] = oedusuario.idusuario.ToString();
                DVariables["SNOMBRE"] = oedusuario.snombres.ToString();
                DVariables["APELLIDO"] = oedusuario.sapellidos.ToString();
                DVariables["SEMAIL"] = oedusuario.scorreo.ToString();
                DVariables["ITIPODOC"] = oedusuario.itipodocumento.ToString();
                DVariables["SDOCUMENTO"] = oedusuario.sdocumento.ToString();
                DVariables["TIPOUSUARIO"] = oedusuario.itipousuario.ToString();
                DVariables["CELULAR"] = oedusuario.scelular.ToString();
                //DVariables["IMAGENUSUARIO"] = oedusuario.simagen.ToString();
                DVariables["SRUC"] = oedusuario.sruc.ToString();
                DVariables["SRAZONSOCIAL"] = oedusuario.srazonsocial.ToString();
                UtlAuditoria.SetSessionValues(DVariables);

                objResultado = new
                {
                    iResultado = oedusuario.itipousuario,
                    iResultadoIns = "Inicio/Principal"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        [HttpPost]
        public JsonResult CrearUsuario(string wnombres, string wapellidos, int wtipodocumento, string wdocumento,
            string wfecharegistro, string whoraregistro, int wtipousuario, string wcorreo, string wclave, string wtoken,
            string wruc, string wrazonsocial, string wpep1, string wpep2, string wpep3, string wpep4)
        {
            try
            {
                var objResultado = new object();
                int iresultado = 0;
                itdusuario = new tdUsuario();
                edUsuario oedusuario = new edUsuario();
                iresultado = itdusuario.tdInsertarUsuario(wnombres, wapellidos, wtipodocumento, wdocumento, wfecharegistro,
                                                        whoraregistro, wtipousuario, wcorreo, wclave, wtoken, wruc, wrazonsocial
                                                        , wpep1, wpep2, wpep3, wpep4);
                if (iresultado == 0 || iresultado == -1 || iresultado == -2)
                {
                    objResultado = new
                    {
                        iResultado = -3,
                        iResultadoIns = "Usuario o clave incorrecta"
                    };
                    return Json(objResultado);
                }
                oedusuario = itdusuario.tdFiltrarUsuario(iresultado);
                Dictionary<string, string> DVariables = new Dictionary<string, string>();
                DVariables["IDUSUARIO"] = oedusuario.idusuario.ToString();
                DVariables["SNOMBRE"] = oedusuario.snombres.ToString();
                DVariables["APELLIDO"] = oedusuario.sapellidos.ToString();
                DVariables["SEMAIL"] = oedusuario.scorreo.ToString();
                DVariables["ITIPODOC"] = oedusuario.itipodocumento.ToString();
                DVariables["SDOCUMENTO"] = oedusuario.sdocumento.ToString();
                DVariables["TIPOUSUARIO"] = oedusuario.itipousuario.ToString();
                DVariables["CELULAR"] = oedusuario.scelular.ToString();
                //DVariables["IMAGENUSUARIO"] = oedusuario.simagen.ToString();
                DVariables["SRUC"] = oedusuario.sruc.ToString();
                DVariables["SRAZONSOCIAL"] = oedusuario.srazonsocial.ToString();
                UtlAuditoria.SetSessionValues(DVariables);

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Inicio/Principal"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        [HttpPost]
        public JsonResult cerrarSesion()
        {
            var objResultado = new object();
            try
            {
                bool bResultado = UtlAuditoria.CerrarSession();
                if (bResultado)
                {
                    objResultado = new
                    {
                        iResultado = 1
                    };
                }
                else
                {
                    objResultado = new
                    {
                        iResultado = 2
                    };
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
            return Json(objResultado);
        }

        //registra las tarjetas de la persona
        [HttpPost]
        public JsonResult OperacionCuentaBancaria(int wtipoOperacion, int widCuentBanc, int widusuario, int witipocuenta, int wimoneda
                                        , int wibanco, string wbanco, string wnumerocuenta, string wnombrecuenta, int witipodeclaracion
                                        , string wtitular, string wfecReg, string whoraReg)
        {
            try
            {
                var objResultado = new object();
                int iresultado = 0;
                itOperacion = new tdOperacion();

                iresultado = itOperacion.tdOperacionCuentaBancaria(wtipoOperacion, widCuentBanc, widusuario, witipocuenta, wimoneda
                                        , wibanco, wbanco, wnumerocuenta, wnombrecuenta, witipodeclaracion, wtitular, wfecReg, whoraReg);

                if (iresultado == 0 || iresultado == -1 || iresultado == -2)
                {
                    objResultado = new
                    {
                        iResultado = -3,
                        iResultadoIns = "Usuario o clave incorrecta"
                    };
                    return Json(objResultado);
                }
                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Inicio/Principal"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //lista las transacciones de la persona
        [HttpPost]
        public JsonResult ListarOperacion(int widusuario, string wdtfecha, int westado)
        {
            try
            {
                var objResultado = new object();
                itOperacion = new tdOperacion();
                List<edTransaccion> edtransaccion = new List<edTransaccion>();
                edtransaccion = itOperacion.tdListartransaccion(widusuario, wdtfecha, westado);

                if (edtransaccion.Count == 0)
                {
                    objResultado = new
                    {
                        iResultado = -3,
                        sData = "",
                        sCantidadFilas = 0
                    };
                    return Json(objResultado);
                }

                objResultado = new
                {
                    iResultado = 1,
                    sData = edtransaccion,
                    sCantidadFilas = edtransaccion.Count
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        [HttpPost]
        public JsonResult ListarOperacionAdmin(int widusuario, string wdtfecha, int westado)
        {
            try
            {
                var objResultado = new object();
                itOperacion = new tdOperacion();
                List<edTransaccion> edtransaccion = new List<edTransaccion>();
                edtransaccion = itOperacion.tdListartransaccion(widusuario, wdtfecha, westado);

                if (edtransaccion.Count == 0)
                {
                    objResultado = new
                    {
                        PageStart = 1,
                        pageSize = 100,
                        SearchText = string.Empty,
                        ShowChildren = UtlConstantes.bValorTrue,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 1,
                        aaData = ""
                    };
                    return Json(objResultado);
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = edtransaccion.Count,
                    iTotalDisplayRecords = 1,
                    aaData = edtransaccion
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //lista las tarjetas de la persona
        [HttpPost]
        public JsonResult ListarCuentaBancaria(int widusuario, string wnombres, int wimoneda)
        {
            try
            {
                var objResultado = new object();
                itOperacion = new tdOperacion();
                List<edCuentaBancaria> edtransaccion = new List<edCuentaBancaria>();
                edtransaccion = itOperacion.tdListarCuentaBancaria(widusuario, wnombres, wimoneda);

                if (edtransaccion.Count == 0)
                {
                    objResultado = new
                    {
                        PageStart = 1,
                        pageSize = 100,
                        SearchText = string.Empty,
                        ShowChildren = UtlConstantes.bValorTrue,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 1,
                        aaData = ""
                    };
                    return Json(objResultado);
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = edtransaccion.Count,
                    iTotalDisplayRecords = 1,
                    aaData = edtransaccion
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        [HttpPost]
        public JsonResult OperacionTranscaccion(int itipooperacion, int idtransaccion, int idusuario, int iddivisa,
            int idpromocion, int idcuentabancaria, int idusuarioadministrador, string dtfecha, int itipodivisa,
            decimal dolaresventa, decimal dolarescompra, string hora, string imagenruta, int itipoenvio,
            int iestado, string voperacion, int iorigenfondo, decimal denvio, decimal drecibo, int itipocambio,
            int itipotrasaccion, decimal digv, string vbancoreceptor, string dtfecharegistro, string dtfechamodificacion,
            int idusuarioModificacion, string voperacionadmin)
        {
            try
            {
                var objResultado = new object();
                int iresultado = 0;
                itOperacion = new tdOperacion();
                iresultado = itOperacion.tdOperaciontransaccion(itipooperacion, idtransaccion, idusuario, iddivisa,
                                        idpromocion, idcuentabancaria, idusuarioadministrador, dtfecha, itipodivisa,
                                        dolaresventa, dolarescompra, hora, imagenruta, itipoenvio,
                                        iestado, voperacion, iorigenfondo, denvio, drecibo, itipocambio,
                                        itipotrasaccion, digv, vbancoreceptor, dtfecharegistro, dtfechamodificacion,
                                        idusuarioModificacion, voperacionadmin);
                if (iresultado == 0 || iresultado == -1 || iresultado == -2)
                {
                    objResultado = new
                    {
                        iResultado = -3,
                        iResultadoIns = "Usuario o clave incorrecta"
                    };
                    return Json(objResultado);
                }
                objResultado = new
                {
                    iResultado = iresultado,
                    iResultadoIns = "Inicio/Principal"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        public ActionResult politicadeprivacidad()
        {
            return View();
        }

        public ActionResult terminosycondiciones()
        {
            return View();
        }

        [ValidadorSesion]
        public ActionResult mihistorial()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            string susuario = UtlAuditoria.ObtenerNombre() + " " + UtlAuditoria.ObtenerApellido();
            int gidusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
            ViewBag.GgradoUsuario = 0;
            ViewBag.GIdusuario = gidusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        //lista las transacciones de la persona
        [HttpPost]
        public JsonResult ListarOperacionUsuario(int widusuario, string wdtfecha, int westado)
        {
            try
            {
                var objResultado = new object();
                itOperacion = new tdOperacion();
                List<edTransaccion> edtransaccion = new List<edTransaccion>();
                edtransaccion = itOperacion.tdListartransaccion(widusuario, wdtfecha, westado);

                if (edtransaccion.Count == 0)
                {
                    objResultado = new
                    {
                        PageStart = 1,
                        pageSize = 100,
                        SearchText = string.Empty,
                        ShowChildren = UtlConstantes.bValorTrue,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 1,
                        aaData = ""
                    };
                    return Json(objResultado);
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = edtransaccion.Count,
                    iTotalDisplayRecords = 1,
                    aaData = edtransaccion
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        /* -------------------------------------------------------------------------------------------- */
        [ValidadorSesion]
        public ActionResult divisa()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            string susuario = UtlAuditoria.ObtenerNombre() + " " + UtlAuditoria.ObtenerApellido();
            int gidusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
            ViewBag.GgradoUsuario = 0;
            ViewBag.GIdusuario = gidusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        //lista las divisas de pagina principal
        [HttpPost]
        public JsonResult ListarDivisas(int widdivisa, string wdtfecha)
        {
            try
            {
                var objResultado = new object();
                itOperacion = new tdOperacion();
                List<edDivisa> eddivisa = new List<edDivisa>();
                eddivisa = itOperacion.tdListardivisa(widdivisa, wdtfecha);
                if (eddivisa.Count == 0)
                {
                    objResultado = new
                    {
                        PageStart = 1,
                        pageSize = 100,
                        SearchText = string.Empty,
                        ShowChildren = UtlConstantes.bValorTrue,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 1,
                        aaData = ""
                    };
                    return Json(objResultado);
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = eddivisa.Count,
                    iTotalDisplayRecords = 1,
                    aaData = eddivisa
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //registra la divisa del dia
        [HttpPost]
        public JsonResult OperacionDivisa(int wtOperacion, int widdivisa, int iusuario, int witipobusqueda,
            decimal wmonto, decimal wsolesventa, decimal wsolescompra, decimal wdolaresventa, decimal wdolarescompra,
            int itipopromocion, string wdfecha, string wvhora, string wfechreg)
        {
            try
            {
                var objResultado = new object();
                int iresultado = 0;
                itOperacion = new tdOperacion();
                iresultado = itOperacion.tdOperaciondivisa(wtOperacion, widdivisa, iusuario, witipobusqueda,
                                                        wmonto, wsolesventa, wsolescompra, wdolaresventa, wdolarescompra,
                                                        itipopromocion, wdfecha, wvhora, wfechreg);

                if (iresultado == 0 || iresultado == -1 || iresultado == -2)
                {
                    objResultado = new
                    {
                        iResultado = -3,
                        iResultadoIns = "Error en el servidor"
                    };
                    return Json(objResultado);
                }
                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "OK"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        public ActionResult nosotros()
        {
            return View();
        }

        [ValidadorSesion]
        public ActionResult usuarios()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            string susuario = UtlAuditoria.ObtenerNombre() + " " + UtlAuditoria.ObtenerApellido();
            int gidusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
            ViewBag.GgradoUsuario = 0;
            ViewBag.GIdusuario = gidusuario;
            ViewBag.GImagenUsuario = UtlAuditoria.ObtenerImagenUsuario();
            return View();
        }

        [HttpPost]
        public JsonResult ListarUsuario(int wparamidusuario)
        {
            try
            {
                var objResultado = new object();
                itdusuario = new tdUsuario();
                List<edUsuario> edusuario = new List<edUsuario>();
                edusuario = itdusuario.tdListarUsuario(wparamidusuario);

                if (edusuario.Count == 0)
                {
                    objResultado = new
                    {
                        PageStart = 1,
                        pageSize = 100,
                        SearchText = string.Empty,
                        ShowChildren = UtlConstantes.bValorTrue,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 1,
                        aaData = ""
                    };
                    return Json(objResultado);
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = edusuario.Count,
                    iTotalDisplayRecords = 1,
                    aaData = edusuario
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //lista solo una tarjeta de la persona POR EL ID
        [HttpPost]
        public JsonResult FiltrarCuentaBancaria(int widcuentabancaria)
        {
            try
            {
                var objResultado = new object();
                itOperacion = new tdOperacion();
                edCuentaBancaria edtransaccion = new edCuentaBancaria();
                edtransaccion = itOperacion.tdFiltrarCuentaBancaria(widcuentabancaria);
                objResultado = new
                {
                    aaData = edtransaccion
                };
                return Json(objResultado);

            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }


    }
}