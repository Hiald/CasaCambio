using CambioTotalFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace CambioTotalFrontEnd.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        //[HttpPost]
        //[Route("authenticate")]
        //public IHttpActionResult Authenticate(string email, string password)
        //{
        //    if (email == "")
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    //TODO: Validate credentials Correctly, this code is only for demo !!
        //    bool isCredentialValid = (password == "123456");
        //    if (isCredentialValid)
        //    {
        //        var token = TokenGenerator.GenerateTokenJwt(email);
        //        return Ok(token);
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(HttpRequestMessage request)
        {
            var email = HttpContext.Current.Request.Form["email"];
            var password = HttpContext.Current.Request.Form["password"];

            if (email == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: This code is only for demo - extract method in new class & validate correctly in your application !!
            var isUserValid = (email == "user" && password == "123456");
            if (isUserValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(email);
                return Ok(token);
                //return Json(new Item { Id = 123, Name = "Hero" });
            }

            // Unauthorized access 
            return Unauthorized();
        }

    }
}