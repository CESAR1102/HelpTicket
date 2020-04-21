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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> FindAll()
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

        public bool Insert(Ticket t, string userCode, out string msm)
        {
            msm = string.Empty;
            string moduloCliente = System.Configuration.ConfigurationSettings.AppSettings["Modulo_Cliente"];
            string rolCliente = System.Configuration.ConfigurationSettings.AppSettings["Rol_Cliente"];

            int id = umr.obtenerIDUserModRol(userCode, moduloCliente, rolCliente);
            if (id == 0)
            {
                msm = "No se encontro usuario. Intente nuevamente.";
                return false;
            }
            t.codigo_atencion = "";
            t.solicitante_id = id;
            t.estado = 'I';
            return ticket_1.Insert(t);
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
    }
}
