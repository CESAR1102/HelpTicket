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
	public class LoginController : Controller
	{
		private IUsuarioService usuarioservice = new UsuarioService();
		SesionData session = new SesionData();

		[AllowAnonymous]
		public ActionResult Login_()
		{
			
			return View("Login_");
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> Login_(Usuario datos)
		{
			if (ModelState.IsValid)
			{
				if (usuarioservice.logeo(datos.codigo,datos.contraseña) == true)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.Message1 = "El usuario y la contraseña es incorrecto ";
					return View("Login_");
				}
			}
			else
			{
				return View("Login_");
			}
		}

		public ActionResult Notificacion_token()
		{
			return View();
		}


		[HttpGet]
		public ActionResult Forgot_Password()
		{
			
			return View();
		}

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Forgot_Password(InputFields data)
        {
            string mensaje;
            if (usuarioservice.AsignarToken(data.correo, out mensaje)){
                if (usuarioservice.EnviarEmailRecuperarContra(data.correo))
                {
                    return View("Notificacion_token");
                }
                else
                {
                    ViewBag.Message = "No se pudo enviar el correo. Intente nuevamente.";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = mensaje;
                return View();
            }
        }


        [HttpGet]
		public ActionResult Recovery()
		{
			
				return View();
			
		}

        [HttpGet]
        public ActionResult Token_Expirado()
        {

            return View();

        }
    }
}