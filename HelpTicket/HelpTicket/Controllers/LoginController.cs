﻿using Business;
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
		public async Task<ActionResult> Login_(InputFields_Loguin datos)
		{
			if (ModelState.IsValid)
			{
                Usuario user = usuarioservice.logeo(datos.codigo, datos.contraseña);
                //if (usuarioservice.logeo(datos.codigo,datos.contraseña) == true)
                if (user != null)
				{
                    session.setSession("usuario", user);
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
        public ActionResult Forgot_Password(InputFields_ForgotPassword data)
        {
            if (ModelState.IsValid)
            {
                string mensaje;
                if (usuarioservice.AsignarToken(data.correo, out mensaje))
                {
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
            else
            {
                return View();
            }
        }

        [HttpGet]
		public ActionResult Recovery(string token)
		{
            if (token is null || token == string.Empty)
            {
                return View("Token_Expirado");
            }
            else
            {
                string codigo;
                if (usuarioservice.VerificarToken(token, out codigo))
                {
                    if (codigo == string.Empty) return View("Token_Expirado");
                    else
                    {
                        return View(new InputFields_Recovery(codigo, string.Empty, string.Empty));
                    }
                }
                else
                {
                    return View("Token_Expirado");
                }
            }          
		}

        [HttpPost]
        public ActionResult Recovery(InputFields_Recovery data)
        {
            if (ModelState.IsValid)
            {
                string msm;
                if (data.password1 == data.password2) {
                    if(usuarioservice.ActualizarContraseña(data.codigo, data.password1, out msm))
                    {
                        return View("Notificacion_UpdatePass");
                    }
                    else
                    {
                        ViewBag.Message = msm;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Las contraseñas deben ser iguales";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}