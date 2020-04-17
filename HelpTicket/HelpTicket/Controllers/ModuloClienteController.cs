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
    public class ModuloClienteController : Controller
    {
        private ITicketService usuarioservice = new TicketService();
        private IDepartamentoService departamentoservice = new DepartamentoService();
        private ITopicoService topicoservice = new TopicoService();
        SesionData session = new SesionData();

        [HttpGet]
        public ActionResult Cliente()
        {

            var nombre = Convert.ToString(Session["usuario"]);
            ViewBag.nombre = nombre;
            return View();
        }

        [HttpGet]
        public ActionResult AgregarTicket()
        {
           // Ticket t = new Ticket();
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
                ViewBag.Importancia = ImportanciaTickets();
                ViewBag.Departamentos = Departamentos();
                ViewBag.Topicos = Topicos(0);
                return View(new Ticket());
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

            List<Topico> topicos = topicoservice.FindByDepartamento(departamento_id);
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