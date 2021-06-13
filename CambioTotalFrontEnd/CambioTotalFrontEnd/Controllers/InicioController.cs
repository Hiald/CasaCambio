using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CambioTotalFrontEnd.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Index()
        {
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

    }
}