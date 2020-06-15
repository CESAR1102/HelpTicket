using Business;
using Business.Implementar;
using Business.Util;
using Entity;
using HelpTicket.Autorizacion;
using HelpTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpTicket.PartialClass;

namespace HelpTicket.Controllers
{
    [Logueado]
    [SoloTrabajadores]
    public class ModuloTrabajadorController : Controller
    {
		private ITicketService ticketservice = new TicketService();
        private IDepartamentoService departamentoservice = new DepartamentoService();
        private ITopicoService topicoservice = new TopicoService();
        private IMensajeService mensajeservice = new MensajeService();
        SesionData session = new SesionData();
        string identificador = "t";

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
                ticketsAsignados = new List<Ticket>();
                ViewBag.mensajeInformativo = "Aun no tienes tickets asignados";
			}
			if (TempData["Agregado"] != null)
				ViewBag.Agregado = TempData["Agregado"].ToString();
			//ViewBag.ListaTicketsAsignados = tPersonalizados.ConvertToTicketsPersonalizados(ticketsAsignados);
			return View(ticketsAsignados);
		}

        [HttpGet]
        public ActionResult SolicitarRecurso()
        {
            ViewBag.Importancia = ImportanciaTickets();
            ViewBag.Departamentos = Departamentos();
            ViewBag.Topicos = Topicos(0);
            return View();
        }

        [HttpPost]
        public ActionResult SolicitarRecurso(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                string msm;
                var user = (Usuario)session.getSession("usuario");
                if (ticketservice.Insert2(ticket, user.codigo, out msm, identificador))
                {
                    TempData["Agregado"] = "Se generó un ticket satisfactoriamente con el codigo de atención: " + msm;
                }
                else
                {
                    ViewBag.Error = msm;
                }

                return RedirectToAction("VerTicketsAsignados");
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
        public ActionResult SolicitarFinalizacion()
        {
            FinalizarTicket f = new FinalizarTicket();
            var usuario = (Entity.Usuario)(Session["usuario"]);

            ViewBag.Tickets = TicketsAsignadoDisponibles(usuario.codigo);

            return View(f);
        }

        [HttpPost]
        public ActionResult SolicitarFinalizacion(FinalizarTicket f)
        {
            var usuario = (Entity.Usuario)(Session["usuario"]);
            ViewBag.Tickets = TicketsAsignadoDisponibles(usuario.codigo);
            if (ModelState.IsValid)
            {
                if (mensajeservice.MensajeSolFinTicket(usuario.codigo, f.Contenido, f.ticket))
                {
                    ViewBag.Exito = "Se envio satisfactoriamente su solicitud de finalizacion del ticket";
                    return View(new FinalizarTicket());
                }
                else
                {
                    ViewBag.Error = "No se pudo enviar la solicictud. Intente nuevamente";
                    return View(f);
                }
            }
            else
            {
                return View(f);
            }
        }

        [ActionName("ObtenerTopicos")]
        public ActionResult ObtenerTopicos(string id)
        {
            var topicos = Topicos(Convert.ToInt32(id));
            return Json(topicos, JsonRequestBehavior.AllowGet);
        }

        [ActionName("ObtenerDatosTicket1")]
        public ActionResult ObtenerDatosTicket1(string id)
        {
            var response = new ResponseTicket1();
            var t = ticketservice.FindId(id);
            if (t.codigo_atencion != null)
            {
                response.response = "OK";
                response.descripcion = t.descripcion;
                response.solicitante = t.Usuario_Modulo_Rol.usuario_id;
                response.topico = t.Topico.topico;
            }
            else
            {
                response.response = "Error";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

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

            List<Ticket> tickets = ticketservice.TicketsAsignadosParaFinalizar(codigo);
            if (!(tickets is null))
            {
                ticket.Add(new SelectListItem { Text = "Seleccione un ticket como referencia al mensaje...", Value = "" });
                foreach (var d in tickets)
                {
                    ticket.Add(new SelectListItem { Text = d.codigo_atencion, Value = d.codigo_atencion.ToString() });
                }
            }
            return ticket;
        }
    }
}