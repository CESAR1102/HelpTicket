using Business;
using Business.Implementar;
using Entity;
using HelpTicket.Models;
using HelpTicket.PartialClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HelpTicket.Controllers
{
    public class TicketController : Controller
    {
        private IUsuarioService usuarioservice = new UsuarioService();
        SesionData session = new SesionData();

        //[HttpGet]
        //public ActionResult AgregarTicket()
        //{ 
        //    return View(new Ticket());
        //}

        //[HttpPost]
        //public ActionResult AgregarTicket(Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View(new Ticket());
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
    }
}