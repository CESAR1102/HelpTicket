using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ITicketRepository : ICrudRepository<Ticket>
    {
        Ticket FindByCodAtencion(string cod_atencion);
        List<Ticket> TicketsSolicitados(string codigo_cliente);
        List<Ticket> TicketsAsignados(string codigo_trabajador);
        string DestinatarioPara(string codigo_atencion);
		Ticket FindId(string id);
	}
}
