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
<<<<<<< HEAD
        private ITicketRepository usuario = new TicketRepository();
=======
        private ITicketRepository ticket_1 = new TicketRepository();
>>>>>>> ed4d2e59db9aba7b9b7f8a06c04d35776d5e9002

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
<<<<<<< HEAD
            Ticket ticket = usuario.FindByCodAtencion(cod_atencion);
=======
            Ticket ticket = ticket_1.FindByCodAtencion(cod_atencion);
>>>>>>> ed4d2e59db9aba7b9b7f8a06c04d35776d5e9002
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

        public bool Insert(Ticket t)
        {
<<<<<<< HEAD
            return usuario.Insert(t);
=======
            return ticket_1.Insert(t);
>>>>>>> ed4d2e59db9aba7b9b7f8a06c04d35776d5e9002
        }

        public List<Ticket> TicketsAsignados(string codigo_trabajador, out string msm)
        {
            msm = string.Empty;
<<<<<<< HEAD
            List<Ticket> tickets = usuario.TicketsAsignados(codigo_trabajador);
=======
            List<Ticket> tickets = ticket_1.TicketsAsignados(codigo_trabajador);
>>>>>>> ed4d2e59db9aba7b9b7f8a06c04d35776d5e9002
            if (tickets is null)
            {
                msm = "ERROR";
                return null;
            }else if(tickets.Count == 0)
            {
                msm = "Aun no has solicitdo ningún ticket";
                return null;
            }
            else
            {
                return tickets;
            }
        }

        public List<Ticket> TicketsSolicitados(string codigo_cliente, out string msm)
        {
            msm = string.Empty;
<<<<<<< HEAD
            List<Ticket> tickets = usuario.TicketsSolicitados(codigo_cliente);
=======
            List<Ticket> tickets = ticket_1.TicketsSolicitados(codigo_cliente);
>>>>>>> ed4d2e59db9aba7b9b7f8a06c04d35776d5e9002
            if (tickets is null)
            {
                msm = "ERROR";
                return null;
            }
            else if (tickets.Count == 0)
            {
                msm = "Aun no has solicitdo ningún ticket";
                return null;
            }
            else
            {
                return tickets;
            }
        }

        public bool Update(Ticket t)
        {
            throw new NotImplementedException();
        }
    }
}
