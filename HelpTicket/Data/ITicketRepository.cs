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
        string TicketsXtrabajadorXestado(string codigo, string estado);
        bool AsignarTicket(Ticket t);
		bool ExistByTopico(int topico_id, string identificador);
        List<Tuple<string, int, int>> DatosReporte02(string fecha_ini, string fecha_fin);
        List<Ticket> Topico_x_tickets();
	}
}
