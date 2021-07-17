using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CambioTotalED;
using CambioTotalTD;
using System.Web.Http;
using Newtonsoft.Json;

namespace CambioTotalAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        tdUsuario itdusuario;

        [HttpGet]
        public string wsObtenerUsuario(string wcorreo, string wclave)
        {
            edUsuario wsenUsuario = new edUsuario();
            try
            {
                itdusuario = new tdUsuario();
                wsenUsuario = itdusuario.tdObtenerUsuario(wcorreo, wclave);
                return JsonConvert.SerializeObject(wsenUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }


        [HttpGet]
        public int wsInsertarUsuario(string wnombres, string wapellidos, int wtipodoc, string wdocumento,
                                    string wfecreg, string whorareg, int wtipousu, string wcorreo, string wclave, string wtoken,
                                    string wruc, string wrazonsoc, string wpep1, string wpep2, string wpep3, string wpep4)
        {
            int iresultado = -1;
            try
            {
                itdusuario = new tdUsuario();
                iresultado = itdusuario.tdInsertarUsuario(wnombres, wapellidos, wtipodoc, wdocumento,
                                                            wfecreg, whorareg, wtipousu, wcorreo, wclave, wtoken,
                                                            wruc, wrazonsoc, wpep1, wpep2, wpep3, wpep4);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsActualizarAcceso(int wtipoact, int widusuario, string wcorreo, string wclave)
        {
            int iresultado = -1;
            try
            {
                itdusuario = new tdUsuario();
                iresultado = itdusuario.tdActualizarAcceso(wtipoact, widusuario, wcorreo, wclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsActualizarClave(int widusuario, string wnuevaclave)
        {
            int iresultado = -1;
            try
            {
                itdusuario = new tdUsuario();
                iresultado = itdusuario.tdActualizarClave(widusuario, wnuevaclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsActualizarCuenta(int widusu, int widprov, int widciudad, int widdist, string wdir, string wnom,
                                    string wape, int wigen, string wimgruta, string wimgdniruta1, string wimgdniruta2,
                                    string wcelular1, string wcelular2, string wtelef, int witipodoc, string wdoc, string wubigeo,
                                    string wfecnac, string wfecmod, string whoramod, int widusumod, string wruc, string wrs)
        {
            int iresultado = -1;
            try
            {
                itdusuario = new tdUsuario();
                iresultado = itdusuario.tdActualizarCuenta(widusu, widprov, widciudad, widdist, wdir, wnom, wape, wigen, wimgruta,
                                    wimgdniruta1, wimgdniruta2, wcelular1, wcelular2, wtelef, witipodoc, wdoc, wubigeo, wfecnac,
                                    wfecmod, whoramod, widusumod, wruc, wrs);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsFiltrarUsuario(int wusuario)
        {
            edUsuario wsenTransaccion = new edUsuario();
            try
            {
                itdusuario = new tdUsuario();
                wsenTransaccion = itdusuario.tdFiltrarUsuario(wusuario);
                return JsonConvert.SerializeObject(wsenTransaccion);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string tdListarUsuario(int wusuario)
        {
            List<edUsuario> wsenTransaccion = new List<edUsuario>();
            try
            {
                itdusuario = new tdUsuario();
                wsenTransaccion = itdusuario.tdListarUsuario(wusuario);
                return JsonConvert.SerializeObject(wsenTransaccion);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}