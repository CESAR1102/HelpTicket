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
		private ISkillService skillservice = new SkillService();
		private SesionData sesion = new SesionData();
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
            ViewBag.estados = CambiosEstadoTickets();
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
            ViewBag.estados = CambiosEstadoTickets();
            bool ed = ticketservice.Update(t);
			if (ed)
			{
                TempData["Agregado"] = "Se modifico con exito el ticket " + t.codigo_atencion;
				return RedirectToAction("VerTicketsClientes");
			}
            ViewBag.Error = "No se pudo Editar el ticket " + t.codigo_atencion;
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
        public ActionResult Usuarios()
        {
            List<Usuario> usuarios = usuarioservice.FindAll();
            if (usuarios.Count == 0)
            {
                ViewBag.mensajeInformativo = "Aun no se ha ingresado ningun usuario";
            }
            if (TempData["Agregado"] != null)
            {
                ViewBag.Agregado = TempData["Agregado"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(usuarios);

        }

        [HttpGet]
		public ActionResult CrearUsuarios()
		{
		
			if (TempData["Error"] != null)
			{
				ViewBag.Error = TempData["Error"];
			}
			return View();


		}

		[HttpPost]
		public ActionResult CrearUsuarios(Usuario us)
		{
			

			//if (us.codigo == null || us.codigo == string.Empty || us.nombres == null || us.nombres == string.Empty ||us.contraseña==null ||us.contraseña==string.Empty||us.correo==null||us.correo==string.Empty||us.rol_creacion==null||us.rol_creacion==string.Empty||us.fecha_creacion==null)
			if (us.codigo == null || us.codigo == string.Empty || us.nombres == null || us.nombres == string.Empty ||us.contraseña==null ||us.contraseña==string.Empty||us.correo==null||us.correo==string.Empty||us.fecha_creacion==null)
			{
				TempData["Error"] = "No se pudo crear el usuario. LLenar todos los campos.";
				return RedirectToAction("CrearUsuarios");
			}

			DateTime mifecha = DateTime.Now;
			us.fecha_creacion = mifecha;
            us.rol_creacion = sesion.getSession("usuario").codigo;
			bool inserto = usuarioservice.Insert(us);
			if (inserto)
			{
				TempData["Agregado"] = "Se registro el usuario: " + us.codigo + " exitosamente";
				return RedirectToAction("Usuarios");
			}
			else
			{
				TempData["Error"] = "No se pudo crear el usuario. Intente nuevamente.";
				return RedirectToAction("CrearUsuarios");
			}

			


		}

		[HttpGet]
		public ActionResult EliminarUsuario(string id)
		{
			
			if (usuarioservice.DeleteUser(id))
			{
				TempData["Agregado"] = "Se elimino exitosamente el usuario";
				return RedirectToAction("Usuarios");
			}
			else
			{
				TempData["Error"] = "No se pudo eliminar el usuario. Intente nuevamente.";
				return RedirectToAction("Usuarios");
			}
		}
		//public ActionResult EliminarUsuario(string id)
  //      {
  //          var user = usuarioservice.FindAll().Where(p => p.codigo == id).FirstOrDefault();
  //          return View(user);
  //      }

  //      [HttpPost]
  //      public ActionResult EliminarUsuario(Usuario user)
  //      {
  //          bool elimino = usuarioservice.DeleteUser(user.codigo);
  //          if (elimino)
  //              return RedirectToAction("Usuarios");
  //          return View();
  //      }

        public ActionResult EditarUsuario(string id)
        {
            var pa = usuarioservice.FindAll().Where(p => p.codigo == id).FirstOrDefault();
            return View(pa);
        }

        [HttpPost]
        public ActionResult EditarUsuario(Usuario usuario)
        {
            bool actualizo = usuarioservice.Update(usuario);
            if (actualizo)
            {
                TempData["Agregado"] = "Se modifico con exito el usuario " + usuario.codigo;
                return RedirectToAction("Usuarios");
            }
            ViewBag.Error = "Ocurrio un error. No se pudo modificar el usuario " + usuario.codigo;
            return View();
        }

        [HttpPost]
        public ActionResult AsignarTicket(Ticket t)
        {
            var modulo = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Administrador"];
            var rol = System.Configuration.ConfigurationSettings.AppSettings["Rol_Administrador"];
            t.aprobador_id = umrservice.obtenerIDUserModRol(sesion.getSession("usuario").codigo, modulo, rol);
            t.fecha_asignado = DateTime.Now;
            if (t.codigo_atencion == null || t.asignado_id == null)
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

        [HttpGet]
        public ActionResult VerDepartamento()
        {
            var departamentos = departamentoservice.FindAll();
            if (TempData["Agregado"] != null)
            {
                ViewBag.Agregado = TempData["Agregado"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(departamentos);
        }

        [HttpGet]
        public ActionResult CrearDepartamento()
        {
            var departamento = new Departamento();
            departamento.estado = 'S';
            departamento.fecha_creacion = DateTime.Now;
            departamento.fecha_modificacion = DateTime.Now;
            departamento.usuario_modificacion = sesion.getSession("usuario").codigo;
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(departamento);
        }

        [HttpPost]
        public ActionResult CrearDepartamento(Departamento d)
        {
            if(d.departamento==null || d.departamento == string.Empty)
            {
                TempData["Error"] = "No se pudo crear el departamento. El nombre esta vacio.";
                return RedirectToAction("CrearDepartamento");
            }
            if (d.departamento.Length >= 50)
            {
                TempData["Error"] = "El nombre debe contener como maximo 50 caracteres";
                return RedirectToAction("CrearDepartamento");
            }
            d.estado = 'S';
            d.fecha_creacion = DateTime.Now;
            d.fecha_modificacion = DateTime.Now;
            d.usuario_modificacion = sesion.getSession("usuario").codigo;
            if (departamentoservice.Insert(d))
            {
                TempData["Agregado"] = "Se registro el departamento: " + d.departamento + " exitosamente";
                return RedirectToAction("VerDepartamento");
            }
            else
            {
                TempData["Error"] = "No se pudo crear el departamento. Intente nuevamente.";
                return RedirectToAction("CrearDepartamento");
            }
        }

        [HttpGet]
        public ActionResult EliminarDepartamentos(string id)
        {
            if(topicoservice.ExistByDepartamento(Convert.ToInt32(id), ""))
            {
                TempData["Error"] = "No se pudo eliminar el departamento. Existen Topicos asociados a este departamento. Revisar.";
                return RedirectToAction("VerDepartamento");
            }
            if (departamentoservice.Delete(Convert.ToInt32(id)))
            {
                TempData["Agregado"] = "Se elimino exitosamente el departamento";
                return RedirectToAction("VerDepartamento");
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar el departamento. Intente nuevamente.";
                return RedirectToAction("VerDepartamento");
            }
        }

        [HttpGet]
        public ActionResult EditarDepartamentos(string id)
        {
            Departamento dep = departamentoservice.FindById(Convert.ToInt32(id));
            dep.fecha_modificacion = DateTime.Now;
            dep.usuario_modificacion = sesion.getSession("usuario").codigo;
            if (dep is null)
            {
                TempData["Error"] = "No se pudo obtener los datos del departamento.";
                return RedirectToAction("VerDepartamento");
            }
            return View(dep);
        }

        [HttpPost]
        public ActionResult EditarDepartamentos(Departamento d)
        {
            d.fecha_modificacion = DateTime.Now;
            d.usuario_modificacion = sesion.getSession("usuario").codigo;
            if (departamentoservice.Update(d))
            {
                TempData["Agregado"] = "Se actualizo exitosamente el departamento";
            }
            else
            {
                TempData["Error"] = "No se pudo actualizar los datos del departamento. Intente nuevamente";
            }
            return RedirectToAction("VerDepartamento");
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

		[HttpGet]
		public ActionResult Topicos()
		{
			
			List<Topico> topicos = topicoservice.FindAll();
			if (topicos.Count == 0)
			{
				ViewBag.mensajeInformativo = "Aun no se ha ingresado ningun topico";
			}
			if (TempData["Agregado1"] != null)
			{
				ViewBag.Agregado = TempData["Agregado1"];
			}
			return View(topicos);

		}
		[HttpGet]
		public ActionResult CrearTopico()
		{
			ViewBag.departamentos2 = departamentoservice.FindAll();
			var topico = new Topico();
			topico.estado = 'S';
			topico.fecha_creacion = DateTime.Now;
			topico.fecha_modificacion = DateTime.Now;
			topico.usuario_modificacion = sesion.getSession("usuario").codigo;
			if (TempData["Error1"] != null)
			{
				ViewBag.Error = TempData["Error1"];
			}
			return View(topico);
		}

		[HttpPost]
		public ActionResult CrearTopico(Topico t)
		{
			if (t.topico== null || t.topico == string.Empty)
			{
				TempData["Error1"] = "No se pudo crear el topico. El nombre esta vacio.";
				return RedirectToAction("Topicos");
			}
			if (t.topico.Length >= 50)
			{
				TempData["Error1"] = "El nombre debe contener como maximo 50 caracteres";
				return RedirectToAction("Topicos");
			}
			ViewBag.departamentos2 = departamentoservice.FindAll();
			t.estado = 'S';
			t.fecha_creacion = DateTime.Now;
			t.fecha_modificacion = DateTime.Now;
		
			t.usuario_modificacion = sesion.getSession("usuario").codigo;
			if (topicoservice.Insert(t))
			{
				TempData["Agregado1"] = "Se registro el topico: " + t.topico + " exitosamente";
				return RedirectToAction("Topicos");
			}
			else
			{
				
				TempData["Error1"] = "No se pudo crear el topico. Intente nuevamente.";
				return RedirectToAction("CrearTopico");
			}
		}


		[HttpGet]
		public ActionResult EliminarTopicos(string id)
		{
			if (skillservice.ExistByTopico(Convert.ToInt32(id), ""))
			{
				TempData["Error1"] = "No se pudo eliminar el topico. Existen Skills asociados a este topico. Revisar.";
				return RedirectToAction("Topicos");
			}
			if (ticketservice.ExistByTopico(Convert.ToInt32(id), ""))
			{
				TempData["Error1"] = "No se pudo eliminar el departamento. Existen Tickets asociados a este topico. Revisar.";
				return RedirectToAction("Topicos");
			}
			if (topicoservice.Delete(Convert.ToInt32(id)))
			{
				TempData["Agregado1"] = "Se agrego exitosamente el topico";
				return RedirectToAction("Topicos");
			}
			else
			{
				TempData["Error1"] = "No se pudo eliminar el topico. Intente nuevamente.";
				return RedirectToAction("Topicos");
			}
		}

		[HttpGet]
		public ActionResult EditarTopicos(string id)
		{
			Topico top = topicoservice.FindById(Convert.ToInt32(id));
	
			top.fecha_modificacion = DateTime.Now;
			top.usuario_modificacion = sesion.getSession("usuario").codigo;
			if (top is null)
			{
				TempData["Error"] = "No se pudo obtener los datos del topico.";
				return RedirectToAction("Topicos");
			}
			return View(top);
		}

		[HttpPost]
		public ActionResult EditarTopicos(Topico t)
		{
			t.fecha_modificacion = DateTime.Now;
			t.usuario_modificacion = sesion.getSession("usuario").codigo;
			if (topicoservice.Update(t))
			{
				TempData["Agregado"] = "Se actualizo exitosamente el topico";
			}
			else
			{
				TempData["Error"] = "No se pudo actualizar los datos del topico. Intente nuevamente";
			}
			return RedirectToAction("Topicos");
		}


        [HttpGet]
        public ActionResult AsignarTopicos()
        {
            if (TempData["Exito"] != null)
            {
                ViewBag.Exito = TempData["Exito"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            ViewBag.Trabajadores = usuarioservice.ObtenerTrabajadores();
            ViewBag.Departamentos = Departamentos();
            ViewBag.Topicos = new List<SelectListItem>();
            return View(new Skill());
        }

        [HttpPost]
        public ActionResult AsignarTopicos(Skill item)
        {
            if (ModelState.IsValid)
            {
                if (skillservice.ExistByTrabajador(item.topico_id, item.usuario_id))
                {
                    TempData["Error"] = "El usuario seleccionado ya esta asignado a ese topico";
                    return RedirectToAction("AsignarTopicos");
                }
                if (skillservice.Insert(item))
                {
                    TempData["Exito"] = "Se asigno el skill al trabajador " + item.usuario_id;
                    //ViewBag.Exito = "Se asigno el skill al trabajador " + item.usuario_id;
                    return RedirectToAction("AsignarTopicos");
                }
                else
                {
                   TempData["Error"] = "No se pudo asignar el skill. Intente nuevamente";
                    return RedirectToAction("AsignarTopicos");
                    //ViewBag.Error = "No se pudo asignar el skill. Intente nuevamente";
                }
                //ViewBag.Trabajadores = usuarioservice.ObtenerTrabajadores();
                //ViewBag.Departamentos = Departamentos();
                //ViewBag.Topicos = new List<SelectListItem>();
                //return View(new Skill());
            }
            else
            {
                ViewBag.Trabajadores = usuarioservice.ObtenerTrabajadores();
                ViewBag.Departamentos = Departamentos();
                ViewBag.Topicos = new List<SelectListItem>();
                
                //return RedirectToAction("AsignarTopicos");
                return View(item);
            }
        }

        [HttpGet]
        public ActionResult Reportes()
        {
            ViewBag.Reporte1 = Datos_Rep01();
			var tickets = ticketservice.Topico_x_tickets();
			return View();
        }

        private List<Data_Reporte01> Datos_Rep01()
        {
            var datos = new List<Data_Reporte01>();
            var trabajadores = usuarioservice.ObtenerTrabajadores();
            foreach (var item in trabajadores)
            {
                datos.Add(new Data_Reporte01()
                {
                    codigo = item.codigo,
                    nombre = item.nombres,
                    tkAsignados = ticketservice.TicketsXtrabajadorXestado(item.codigo, "A"),
                    tkFinalizados = ticketservice.TicketsXtrabajadorXestado(item.codigo, "F")
                });
            }
            return datos;
        }

		/**/
		
		public JsonResult JsonGRAFTicketsXTopico()
		{
			List<Grafica_Reporte> items = new List<Grafica_Reporte>();
			foreach (var item in (ticketservice.Topico_x_tickets()))
			{
				items.Add(new Grafica_Reporte() { topico = item.Topico.topico, tickets = Int32.Parse(item.codigo_atencion) });
			}
			return (Json(items, JsonRequestBehavior.AllowGet));
		}
		public JsonResult JsonGRAFTicketsXDepartamento()
		{
			List<Grafica_Reporte2> items = new List<Grafica_Reporte2>();
			foreach (var item in (topicoservice.Departamento_x_tickets()))
			{
				items.Add(new Grafica_Reporte2() { departamento = item.Departamento.departamento, tickets = Int32.Parse(item.topico) });
			}
			return (Json(items, JsonRequestBehavior.AllowGet));
		}
		public JsonResult JsonGRAFUsuariosXTopico()
		{
			List<Grafica_Reporte3> items = new List<Grafica_Reporte3>();
			foreach (var item in (skillservice.Usuarios_X_Topico()))
			{
				items.Add(new Grafica_Reporte3() { topico = item.Topico.topico, cantidad = Int32.Parse(item.usuario_id) });
			}
			return (Json(items, JsonRequestBehavior.AllowGet));
		}


		/**/


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

        private List<SelectListItem> Topicos(int departamentoID)
        {
            List<SelectListItem> tops = new List<SelectListItem>();

            List<Topico> topicos = topicoservice.FindByDepartamento(departamentoID, "");
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

        [ActionName("ObtenerTopicosDisponibles")]
        public ActionResult ObtenerTopicosDisponibles(string id)
        {
            var topicos = Topicos(Convert.ToInt32(id));
            return Json(topicos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerDatosRep2(string id)
        {
            var inicio = id.Substring(0, 10);
            var fin = id.Substring(11, 10);

            var datos = ticketservice.DatosReporte02(inicio, fin);

            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> CambiosEstadoTickets()
        {
            List<SelectListItem> item = new List<SelectListItem>();

            item.Add(new SelectListItem { Text = "Finalizado", Value = "F" });
            item.Add(new SelectListItem { Text = "Ingresado", Value = "I" });
            item.Add(new SelectListItem { Text = "Anulado", Value = "N" });
            return item;
        }
    }
}