using System;
using System.Web.Mvc;

namespace CambioTotalFrontEnd.App_Start
{
    public abstract class InitialPage<T> : WebViewPage<T>
    {
        protected override void InitializePage()
        {
            SetViewBagDefaultProperties();
            base.InitializePage();
        }
        private void SetViewBagDefaultProperties()
        {
            //ViewBag.sNombreCompletoI = UtlAuditoria.ObtenerNombreCompleto();
            //ViewBag.sCorreo = UtlAuditoria.ObtenerCorreo();
            //ViewBag.sPrimeroNombre = UtlAuditoria.ObtenerNombreCompleto();
            ViewBag.sFecha = DateTime.Now.ToString("dd/MM/yyyy");

            //ViewBag.lstMenuP = lstMenuPadre;
            //ViewBag.lstMenu = lstMenuItem;
        }
    }
}