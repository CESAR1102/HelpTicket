﻿using HelpTicket.Autorizacion;
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
		public ActionResult Trabajador()
		{
			return View();
		}
	}
}