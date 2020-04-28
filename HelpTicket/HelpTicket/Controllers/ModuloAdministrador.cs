using HelpTicket.Autorizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpTicket.Controllers
{
    [Logueado]
    [SoloAdministradores]
    public class ModuloAdministrador : Controller
    {
        [HttpGet]
        public ActionResult Administrador()
        {
            return View();
        }
    }
}
