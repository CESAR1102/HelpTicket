using Business;
using Business.Implementar;
using Entity;
using HelpTicket.Autorizacion;
using HelpTicket.Models;
using HelpTicket.PartialClass;
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
            if (TempData["Agregado"] != null)
            {
                ViewBag.Agregado = TempData["Agregado"];
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

        [HttpGet]
        public ActionResult VerTrabajadores()
        {
            ManagerInfoTrabajador manager = new ManagerInfoTrabajador();
            List<InfoTrabajador> trabajadores = manager.LlenarDatos();
            if (trabajadores.Count == 0)
            {
                ViewBag.mensajeInformativo = "Aun no se ha registrado ningun trabajador";
            }
            return View(trabajadores);
        }

        [HttpGet]
        public ActionResult AsignarTicket(string id)
        {
            var t = ticketservice.FindId(id);
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            ViewBag.umr = umrservice.ObtenerTrabajadoresXskill(t.topico_id.ToString());
            return View(t);
        }

		[HttpGet]
		public ActionResult CrearUsuarios()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CrearUsuarios(Usuario us)
		{
			DateTime mifecha = DateTime.Now;
			us.fecha_creacion = mifecha;
		
			bool inserto = usuarioservice.Insert(us);
			if (inserto)
				return RedirectToAction("CrearUsuarios");
			return View();
		}
		



		[HttpPost]
        public ActionResult AsignarTicket(Ticket t)
        {
            if(t.codigo_atencion == null || t.asignado_id == null)
            {
                TempData["Error"] = "No se pudo asignar. Intente otra vez!";
                return RedirectToAction("AsignarTicket");
            }
            else
            {
                if (ticketservice.AsignarTicket(t))
                {
                    TempData["Agregado"] = "Se asigno el ticket: " + t.codigo_atencion;
                    return RedirectToAction("VerTicketsClientes");
                }
                else
                {
                    TempData["Error"] = "No se pudo asignar. Intente otra vez!";
                    return RedirectToAction("AsignarTicket");
                }
            }
        }

        [ActionName("ObtenerNombreTrabajador")]
        public ActionResult ObtenerNombreTrabajador(string id)
        {
            var usuario = usuarioservice.FindByCodigo(id);
            var user = new Usuario();
            user.codigo = usuario.codigo;
            user.nombres = usuario.nombres;
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}