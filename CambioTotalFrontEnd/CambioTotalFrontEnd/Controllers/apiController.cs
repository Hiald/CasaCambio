using CambioTotalED;
using CambioTotalTD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CambioTotalFrontEnd.Controllers
{
    [RoutePrefix("apiv1")]
    public class apiController : Controller
    {
        tdOperacion itddoperacion;

        //[AllowAnonymous]
        [Route("api.json")]
        public async Task<JsonResult> apiv1()
        {
            try
            {
                string fechaconvertida = DateTime.Now.ToString("yyyy-MM-dd");
                string horaconvertida = DateTime.Now.ToString("hh:mm");
                string rescompra = "";
                string resventa = "";

                itddoperacion = new tdOperacion();
                List<edDivisa> eddivisa = new List<edDivisa>();
                eddivisa = itddoperacion.tdListardivisa(0, fechaconvertida);
                if (eddivisa.Count == 0)
                {
                    //no hay, se debe buscar en roblex
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage respuesta = await client.GetAsync("https://operations.roblex.pe/valuation/active-valuation");
                    api result = new api();
                    if (respuesta.IsSuccessStatusCode)
                    {
                        var categories = respuesta.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<api>(categories);
                    }

                    int iresultado = itddoperacion.tdOperaciondivisa(1, 0, 8, 1,
                                                        0, decimal.Parse(result.amountSale),
                                                        decimal.Parse(result.amountBuy),
                                                        decimal.Parse(result.amountSale),
                                                        decimal.Parse(result.amountBuy),
                                                        0, fechaconvertida, horaconvertida, fechaconvertida);
                    rescompra = result.amountBuy;
                    resventa = result.amountSale;
                }
                else
                {

                    rescompra = eddivisa[0].dsolescompra.ToString();
                    resventa = eddivisa[0].dsolesventa.ToString();
                }

                string fechamostrar = DateTime.Now.ToString("yyyy/MM/dd");
                return Json(new
                {
                    compra = rescompra,
                    venta = resventa,
                    fecha = fechamostrar
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}