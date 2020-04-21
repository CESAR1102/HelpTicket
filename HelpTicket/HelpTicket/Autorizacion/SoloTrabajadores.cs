using Business;
using Business.Implementar;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpTicket.Autorizacion
{
    public class SoloTrabajadores : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)HttpContext.Current.Session["usuario"];
                string moduloTrabajador = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Trabajador"];
                IModuloService moduloservice = new ModuloService();

                if (moduloservice.AccesoModulo(usuario.codigo, moduloTrabajador))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("/Home/AccesoDenegado");
        }
    }
}