﻿using Business;
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
	[SoloAdministradores]
	public class ModuloAdministradorController : Controller
	{

		private ITicketService ticketservice = new TicketService();
		private IDepartamentoService departamentoservice = new DepartamentoService();
		private ITopicoService topicoservice = new TopicoService();
		private IUsuarioModuloRolService umrservice = new UsuarioModuloRolService();
		private IUsuarioService usuarioservice = new UsuarioService();
		private IMensajeService mensajeservice = new MensajeService();
		string identificador = "c";
		//private TicketsPersonalizados tPersonalizados = new TicketsPersonalizados();
		SesionData session = new SesionData();

		[HttpGet]
		public ActionResult Administrador()
		{
			return View();
		}



		[HttpGet]
		public ActionResult VerTicketsClientes()
		{
			//var usuario = (Entity.Usuario)(Session["usuario"]);

			List<Ticket> tickets = ticketservice.FindAll();
            if (tickets.Count == 0)
			{
				ViewBag.mensajeInformativo = "Aun no se ha ingresado ningun ticket";
			}
			return View(tickets);
		}





		[HttpGet]
		public ActionResult Editar_Tickets(string id)
		{
			ViewBag.umr = umrservice.FindAll();
			ViewBag.usuarios = usuarioservice.FindAll();
			ViewBag.topicos = topicoservice.FindAll();
			if (id == null)
			{
				return HttpNotFound();
			}

			return View(ticketservice.FindId(id));



		}
		[HttpPost]
		public ActionResult Editar_Tickets(Ticket t)
		{
			ViewBag.umr = umrservice.FindAll();
			ViewBag.usuarios = usuarioservice.FindAll();
			ViewBag.topicos = topicoservice.FindAll();
			bool ed = ticketservice.Update(t);
			if (ed)
			{
				return RedirectToAction("VerTicketsClientes");
			}

			return View();

		}
		

	}
}