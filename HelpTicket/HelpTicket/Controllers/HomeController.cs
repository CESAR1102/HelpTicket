using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpTicket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Cliente()
		{

			var nombre = Convert.ToString(Session["usuario"]);
			ViewBag.nombre = nombre;
			return View();
		}
	}
}