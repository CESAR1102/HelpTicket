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
        private IDepartamentoService departamentoservice = new DepartamentoService();
        private ITopicoService topicoservice = new TopicoService();
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

        [ActionName("ObtenerTopicos")]
        public ActionResult ObtenerTopicos(string id)
        {
            var topicos = Topicos(Convert.ToInt32(id));
            return Json(topicos, JsonRequestBehavior.AllowGet);
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
    }
}