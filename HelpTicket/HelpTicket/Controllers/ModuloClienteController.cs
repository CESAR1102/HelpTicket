using Business;
using Business.Implementar;
using Entity;
using HelpTicket.Autorizacion;
using HelpTicket.Models;
using HelpTicket.PartialClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace HelpTicket.Controllers
{
    [Logueado]
    [SoloClientes]
    public class ModuloClienteController : Controller
    {
        private ITicketService ticketservice = new TicketService();
        private IDepartamentoService departamentoservice = new DepartamentoService();
        private ITopicoService topicoservice = new TopicoService();
        private IMensajeService mensajeservice = new MensajeService();
        string identificador = "c";
        //private TicketsPersonalizados tPersonalizados = new TicketsPersonalizados();
        SesionData session = new SesionData();

        [HttpGet]
        public ActionResult Cliente()
        {
            var usuario = (Entity.Usuario)(Session["usuario"]);
            ViewBag.nombre = usuario.nombres;
            return View();
        }

        [HttpGet]
        public ActionResult AgregarTicket()
        {
            ViewBag.Importancia = ImportanciaTickets();
            ViewBag.Departamentos = Departamentos();
            ViewBag.Topicos = Topicos(0);
            return View();
        }

        [HttpPost]
        public ActionResult AgregarTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                string msm;
                var user = (Usuario)session.getSession("usuario");
                if (ticketservice.Insert2(ticket, user.codigo, out msm, identificador))
                {
                    TempData["Agregado"] = "Se generó un ticket satisfactoriamente con el codigo de atención: " + msm;
                }
                else {
                    ViewBag.Error = msm;
                }
                
                return RedirectToAction("VerTickets");
            }
            else
            {
                ViewBag.Importancia = ImportanciaTickets();
                ViewBag.Departamentos = Departamentos();
                ViewBag.Topicos = Topicos(0);
                return View();
            }
        }

		
		[HttpGet]
		public ActionResult VerTickets()
		{
			var usuario = (Entity.Usuario)(Session["usuario"]);
			
			List<Ticket> ticketsAsignados = ticketservice.TicketsSolicitados(usuario.codigo);
			if (ticketsAsignados is null)
			{
                ticketsAsignados = new List<Ticket>();
                ViewBag.mensajeInformativo = "Aun no has solicitado ningun ticket";
			}
			if (TempData["Agregado"] != null)
				ViewBag.Agregado = TempData["Agregado"].ToString();
			//ViewBag.ListaTicketsAsignados = tPersonalizados.ConvertToTicketsPersonalizados(ticketsAsignados);
			return View(ticketsAsignados);
		}


		[ActionName("ObtenerTopicos")]
        public ActionResult ObtenerTopicos(string id)
        {
            var topicos = Topicos(Convert.ToInt32(id));
            return Json(topicos, JsonRequestBehavior.AllowGet);
        }

        [ActionName("ObtenerDestinatario")]
        public ActionResult ObtenerDestinatario(string id)
        {
            //string codigo = ticketservice.DestinatarioPara(id);
            ResponseDestinatarios respuesta = new ResponseDestinatarios();
            respuesta.response = ticketservice.DestinatarioPara(id); ;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

       
		public ActionResult Detalle_Tickets(string id)
		{

			if (id == null)
			{
				return HttpNotFound();
			}

			return View(ticketservice.FindId(id));

		}
		public ActionResult Enviar_Mensaje()
		{
			Mensaje msm = new Mensaje();
			var usuario = (Entity.Usuario)(Session["usuario"]);
			//ViewBag.Tickets = ticketservice.TicketsSolicitadosParaMsm(usuario.codigo);
			ViewBag.Tickets = TicketsAsignadoDisponibles(usuario.codigo);
			return View(msm);
		}

		[HttpPost]
        public ActionResult Enviar_Mensaje(Mensaje msm)
        {
            var usuario = (Entity.Usuario)(Session["usuario"]);
            msm.remitente_id = usuario.codigo;
            msm.enviado_el = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (mensajeservice.EnviarMensaje(msm))
                {
                    ViewBag.Exito = "Se envio el mensaje Satisfactoriamente";
                }
                else
                {
                    ViewBag.Error = "No se pudo enviar el mensaje. Intente nuevamente";
                }
                ViewBag.Tickets = TicketsAsignadoDisponibles(usuario.codigo);
                   // ticketservice.TicketsSolicitadosParaMsm(usuario.codigo);
                return View(new Mensaje());
            }
            else
            {
                ViewBag.Tickets = TicketsAsignadoDisponibles(usuario.codigo);
                //ViewBag.Tickets = ticketservice.TicketsSolicitadosParaMsm(usuario.codigo);
                return View();
            }
        }
        //interno

        private List<SelectListItem> ImportanciaTickets()
        {
            List<SelectListItem> importancias = new List<SelectListItem>()
            {
            new SelectListItem{ Text="Alta", Value="1" },
            new SelectListItem{ Text="Normal", Value="2" },
            new SelectListItem{ Text="Baja", Value="3" }
            };

            return importancias;
        }

        private List<SelectListItem> Departamentos()
        {
            List<SelectListItem> depas = new List<SelectListItem>();

            List<Departamento> departamentos = departamentoservice.FindAll();
            if (!(departamentos is null))
            {
                depas.Add(new SelectListItem { Text = "Seleccione un Departamento...", Value = "0" });
                foreach (var d in departamentos)
                {
                    depas.Add(new SelectListItem { Text = d.departamento, Value = d.id.ToString() });
                }
            }
            return depas;
        }

        private List<SelectListItem> Topicos(int departamento_id)
        {
            List<SelectListItem> tops = new List<SelectListItem>();

            List<Topico> topicos = topicoservice.FindByDepartamento(departamento_id, identificador);
            if (!(topicos is null))
            {
                tops.Add(new SelectListItem { Text = "Seleccione un Tópico...", Value = "" });
                foreach (var t in topicos)
                {
                    tops.Add(new SelectListItem { Text = t.topico, Value = t.id.ToString() });
                }
            }
            return tops;
        }

        private List<SelectListItem> TicketsAsignadoDisponibles(string codigo)
        {
            List<SelectListItem> ticket = new List<SelectListItem>();

            List<Ticket> tickets = ticketservice.TicketsSolicitadosParaMsm(codigo);
            if (!(tickets is null))
            {
                ticket.Add(new SelectListItem { Text = "Seleccione un ticket cmo referencia al mensaje...", Value = "0" });
                foreach (var d in tickets)
                {
                    ticket.Add(new SelectListItem { Text = d.codigo_atencion, Value = d.codigo_atencion.ToString() });
                }
            }
            return ticket;
        }
    }
}