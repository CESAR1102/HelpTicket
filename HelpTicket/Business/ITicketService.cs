using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ITicketService : ICrudService<Ticket>
    {
        Ticket FindByCodAtencion(string cod_atencion, out string msm);
        List<Ticket> TicketsSolicitados(string codigo_cliente);
        List<Ticket> TicketsSolicitadosParaMsm(string codigo_cliente);
        List<Ticket> TicketsAsignadosParaFinalizar(string codigo_trabajador);
        List<Ticket> TicketsAsignados(string codigo_trabajador);
        bool Insert2(Ticket t, string userCode, out string msm, string mod);
        string DestinatarioPara(string codigo_atencion);
		Ticket FindId(string id);
        string TicketsXtrabajadorXestado(string codigo, string estado);
    }
}
