using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CambioTotalTD;
using CambioTotalED;
using System.Web.Http;
using Newtonsoft.Json;

namespace CambioTotalAPI.Controllers
{
    public class MaestroController : ApiController
    {
        tdMaestro itdmaestro;


        [HttpGet]
        public string wsListarMaestro(int wiMaestro)
        {
            List<edMaestro> wsenUsuario = new List<edMaestro>();
            try
            {
                itdmaestro = new tdMaestro();
                wsenUsuario = itdmaestro.tdListarMaestro(wiMaestro);
                return JsonConvert.SerializeObject(wsenUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

    }
}