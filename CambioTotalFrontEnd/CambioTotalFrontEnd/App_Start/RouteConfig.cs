using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CambioTotalFrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            //routes.MapRoute(
            //    name: "apiv1/api.json/",
            //    url: "apiv1/api.json/",
            //    defaults: new { controller = "apiv1", action = "apires" }
            //);

            // routes.MapRoute(
            //    name: "api/Dolar/Auth",
            //    url: "api/Dolar/Auth",
            //    defaults: new { controller = "api", action = "Auth" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Inicio", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
