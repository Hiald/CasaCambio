using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CambioTotalED;
using CambioTotalFrontEnd.Controllers;
using CambioTotalFrontEnd.Models;
using CambioTotalTD;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CambioTotalFrontEnd.App_Start
{
    [RoutePrefix("api/Dolar")]
    public class apiController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("Auth")]
        public IHttpActionResult Auth(HttpRequestMessage request)
        {
            var email = HttpContext.Current.Request.Form["email"];
            var password = HttpContext.Current.Request.Form["password"];

            bool isCredentialValid = false;
            if(email == "api@tucambiototal.com" && password == "@CambioAPI21.") {
                isCredentialValid = true;
            }
            if (isCredentialValid)
            {
                var tokenGenerado = TokenGenerator.GenerateTokenJwt(email);
                return Json(new apiresponse { status = 200, token = tokenGenerado, message = "token generado con éxito" });
            }
            else
            {
                return Json(new { status = 400, message = "email y/o password no coinciden"});
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Actualizar")]
        public IHttpActionResult ActualizarDolar()
        {
            var customersFake = new string[] { "TOKEN VALIDADO, ENCRIPTACION A EXTREMOS GENERADA PARA ESTA SESION: 9798f25f-351e-40ef-85b2-31d00f30a51c " };

            return Ok(customersFake);
        }

    }
}