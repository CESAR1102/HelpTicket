using HelpTicket.Autorizacion;
using HelpTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpTicket.Controllers
{
    [Logueado]
    public class HomeController : Controller
    {
        SesionData sesion = new SesionData();
        // GET: Home
        public ActionResult Index()
        {
            var nombre = (Entity.Usuario)(Session["usuario"]);
            ViewBag.nombre = nombre.nombres;
            return View();
        }

        public ActionResult CerrarSesion()
        {
            sesion.destroySession();
            return RedirectToAction("Login_", "Login");
        }
    }
}