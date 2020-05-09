using Data.Implementar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Data;

namespace Business.Implementar
{
    public class TicketService : ITicketService
    {
        private ITicketRepository ticket_1 = new TicketRepository();
        private IUsuarioModuloRolRepository umr = new UsuarioModuloRolRepository();
        Random ran = new Random();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> FindAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Ticket t)
        {
            throw new NotImplementedException();
        }

        public Ticket FindByCodAtencion(string cod_atencion, out string msm)
        {
            msm = string.Empty;
            Ticket ticket = ticket_1.FindByCodAtencion(cod_atencion);
            if (ticket is null)
            {
                msm = "ERROR";
                return null;
            }
            else if (ticket.codigo_atencion == string.Empty)
            {
                msm = "No se encontro el ticket seleccionado";
                return null;
            }
            else
            {
                return ticket;
            }
        }

        public Ticket FindById(int? id)
        {
            throw new NotImplementedException();
        }

        private void llenar_datos(out string modulo, out string rol, string mod)
        {
            switch (mod) {
                case "c":
                    modulo = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Cliente"];
                    rol = System.Configuration.ConfigurationSettings.AppSettings["Rol_Cliente"];
                    break;
                case "t":
                    modulo = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Trabajador"];
                    rol = System.Configuration.ConfigurationSettings.AppSettings["Rol_Trabajador"];
                    break;
                case "a":
                    modulo = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Administrador"];
                    rol = System.Configuration.ConfigurationSettings.AppSettings["Rol_Administrador"];
                    break;
                default:
                    modulo = string.Empty;
                    rol = string.Empty;
                    break;
            }
        }

        public bool Insert2(Ticket t, string userCode, out string msm, string mod)
        {
            msm = string.Empty;
            //string moduloCliente = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Cliente"];
            // string rolCliente = System.Configuration.ConfigurationSettings.AppSettings["Rol_Cliente"];
            string modulo;
            string rol;

            llenar_datos(out modulo, out rol, mod);
            // int id = umr.obtenerIDUserModRol(userCode, moduloCliente, rolCliente);
            int id = umr.obtenerIDUserModRol(userCode, modulo, rol);
            if (id == 0)
            {
                msm = "No se encontro usuario. Intente nuevamente.";
                return false;
            }
            string code = "TK" + id + t.topico_id;

            for (int i = 0; i < 15; i++)
            {
                code += (char)ran.Next(65, 90);
            }
            t.codigo_atencion = code;
            t.solicitante_id = id;
            t.estado = 'I';
            if (!ticket_1.Insert(t))
            {
                msm = "Ocurrio un error. Intente nuevamente";
                return false;
            }
            msm = code;
            return true;
        }

        public List<Ticket> TicketsAsignados(string codigo_trabajador)
        {
            List<Ticket> tickets = ticket_1.TicketsAsignados(codigo_trabajador);
            if (tickets != null)
            {
                if (tickets.Count == 0)
                {
                    return null;
                }
            }
            return tickets;
        }

		public List<Ticket> TicketsSolicitados(string codigo_cliente)
		{
			List<Ticket> tickets = ticket_1.TicketsSolicitados(codigo_cliente);
			if (tickets != null)
			{
				if (tickets.Count == 0)
				{
					return null;
				}
			}
			return tickets;
		}

		public bool Update(Ticket t)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> TicketsSolicitadosParaMsm(string codigo_cliente)
        {
            List<Ticket> originales = TicketsSolicitados(codigo_cliente);
            if (originales != null)
            {
                for (short i = 0; i < originales.Count; i++)
                {
                    if (originales[i].estado != 'P' && originales[i].estado != 'A')
                    {
                        originales.Remove(originales[i]);
                        i--;
                    }
                }
            }
            else
            {
                originales = new List<Ticket>();
            }
            return originales;
        }

        public string DestinatarioPara(string codigo_atencion)
        {
            string codigo = ticket_1.DestinatarioPara(codigo_atencion);
            if (codigo == string.Empty)
            {
                return "Error";
            }
            return codigo;
        }

		public Ticket FindId(string id)
		{
			return ticket_1.FindId(id);
		}
	}
}
