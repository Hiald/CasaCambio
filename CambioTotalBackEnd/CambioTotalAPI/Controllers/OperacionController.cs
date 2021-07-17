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
    public class OperacionController : ApiController
    {
        tdOperacion itdoperacion;

        [HttpGet]
        public int wsOperacionCuentaBancaria(int wtipoOperacion, int widCuentBanc, int widusuario, int witipocuenta, int wimoneda
                                        , int wibanco, string wbanco, string wnucuenta, string wnomcuenta, int witipodec
                                        , string wtitular, string wfecReg, string whoraReg)
        {
            int iresultado = -1;
            try
            {
                itdoperacion = new tdOperacion();
                iresultado = itdoperacion.tdOperacionCuentaBancaria(wtipoOperacion, widCuentBanc, widusuario, witipocuenta
                                        , wimoneda, wibanco, wbanco, wnucuenta, wnomcuenta, witipodec, wtitular, wfecReg, whoraReg);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarCuentaBancaria(int wsidusuario, string wsnombres, int wimoneda)
        {
            List<edCuentaBancaria> wsenCuentaBancaria = new List<edCuentaBancaria>();
            try
            {
                itdoperacion = new tdOperacion();
                wsenCuentaBancaria = itdoperacion.tdListarCuentaBancaria(wsidusuario, wsnombres, wimoneda);
                return JsonConvert.SerializeObject(wsenCuentaBancaria);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsOperaciondivisa(int wtipoOperacion, int widdivisa, int widusuario, int witipobusqueda,
                        decimal wmonto, decimal wsolesventa, decimal wsolescompra, decimal wddolaresventa, decimal wddolarescompra,
                        int witipopromo, string wfecha, string wvhora, string wdtfechareg)
        {
            int iresultado = -1;
            try
            {
                itdoperacion = new tdOperacion();
                iresultado = itdoperacion.tdOperaciondivisa(wtipoOperacion, widdivisa, widusuario, witipobusqueda,
                                                            wmonto, wsolesventa, wsolescompra, wddolaresventa, wddolarescompra,
                                                            witipopromo, wfecha, wvhora, wdtfechareg);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListardivisa(int wsIDdivisa, string wsDTfecha)
        {
            List<edDivisa> wsenDivisa = new List<edDivisa>();
            try
            {
                itdoperacion = new tdOperacion();
                wsenDivisa = itdoperacion.tdListardivisa(wsIDdivisa, wsDTfecha);
                return JsonConvert.SerializeObject(wsenDivisa);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsOperaciontransaccion(int witipope, int widtrans, int widusu, int widdivisa, int widprom, int widcuenbanc,
            int widusuadmin, string wdtfecha, int witipodiv, decimal wdolarven, decimal wdolarcompr, string whora, string wvimgruta,
            int witipoenv, int wiest, string wvoper, int wiorigenfond, decimal wdenvio, decimal wdrecibo, int witipocamb,
            int witipotrans, decimal wdigv, string wvbancorecep, string wdtfecreg, string wdtfecmod, int widusuMod, string voperadmin)
        {
            int iresultado = -1;
            try
            {
                itdoperacion = new tdOperacion();
                iresultado = itdoperacion.tdOperaciontransaccion(witipope, widtrans, widusu, widdivisa, widprom, widcuenbanc,
                                                     widusuadmin, wdtfecha, witipodiv, wdolarven, wdolarcompr, whora, wvimgruta,
                                                     witipoenv, wiest, wvoper, wiorigenfond, wdenvio, wdrecibo, witipocamb,
                                                     witipotrans, wdigv, wvbancorecep, wdtfecreg, wdtfecmod, widusuMod, voperadmin);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListartransaccion(int widusuario, string wdtfecha, int westado)
        {
            List<edTransaccion> wsenTransaccion = new List<edTransaccion>();
            try
            {
                itdoperacion = new tdOperacion();
                wsenTransaccion = itdoperacion.tdListartransaccion(widusuario, wdtfecha, westado);
                return JsonConvert.SerializeObject(wsenTransaccion);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wsFiltrarCuentaBancaria(int widcuentabanc)
        {
            edCuentaBancaria wsenCuentaBancaria = new edCuentaBancaria();
            try
            {
                itdoperacion = new tdOperacion();
                wsenCuentaBancaria = itdoperacion.tdFiltrarCuentaBancaria(widcuentabanc);
                return JsonConvert.SerializeObject(wsenCuentaBancaria);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}