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
			bool ed = ticketservice.Update(t);
			if (ed)
			{
				return RedirectToAction("VerTicketsClientes");
			}

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
			

			if (us.codigo == null || us.codigo == string.Empty || us.nombres == null || us.nombres == string.Empty ||us.contraseña==null ||us.contraseña==string.Empty||us.correo==null||us.correo==string.Empty||us.rol_creacion==null||us.rol_creacion==string.Empty||us.fecha_creacion==null)
			{
				TempData["Error"] = "No se pudo crear el usuario.LLenar todos los campos.";
				return RedirectToAction("CrearUsuarios");
			}

			DateTime mifecha = DateTime.Now;
			us.fecha_creacion = mifecha;
			bool inserto = usuarioservice.Insert(us);
			if (inserto)
			{
				TempData["Agregado"] = "Se registro el usuario: " + us.codigo + " exitosamente";
				return RedirectToAction("CrearUsuarios");
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
				return RedirectToAction("VerDepartamento");
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
                return RedirectToAction("Usuarios");
            return View();
        }

        [HttpPost]
        public ActionResult AsignarTicket(Ticket t)
        {
            if(t.codigo_atencion == null || t.asignado_id == null)
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

        [ActionName("ObtenerNombreTrabajador")]
        public ActionResult ObtenerNombreTrabajador(string id)
        {
            var usuario = usuarioservice.FindByCodigo(id);
            var user = new Usuario();
            user.codigo = usuario.codigo;
            user.nombres = usuario.nombres;
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}