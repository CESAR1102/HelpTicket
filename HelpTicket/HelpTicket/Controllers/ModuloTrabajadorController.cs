using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpTicket.Controllers
{
    public class ModuloTrabajadorController : Controller
    {
        // GET: ModuloTrabajador
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Trabajador()
		{
			return View();
		}
	}
}