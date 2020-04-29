using Business;
using Business.Implementar;
using Entity;
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
    [SoloTrabajadores]
    public class ModuloTrabajadorController : Controller
    {
		private ITicketService ticketservice = new TicketService();
		SesionData session = new SesionData();

		public ActionResult Trabajador()
		{
			return View();
		}


		[HttpGet]
		public ActionResult VerTicketsAsignados()
			{
			var usuario = (Entity.Usuario)(Session["usuario"]);

			List<Ticket> ticketsAsignados = ticketservice.TicketsAsignados(usuario.codigo);
			if (ticketsAsignados is null)
			{
				ViewBag.mensajeInformativo = "Aun no tienes tickets asignados";
			}
			if (TempData["Agregado"] != null)
				ViewBag.Agregado = TempData["Agregado"].ToString();
			//ViewBag.ListaTicketsAsignados = tPersonalizados.ConvertToTicketsPersonalizados(ticketsAsignados);
			return View(ticketsAsignados);
		}
	}
}