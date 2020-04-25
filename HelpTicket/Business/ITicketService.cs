﻿using Entity;
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
        List<Ticket> TicketsAsignados(string codigo_trabajador);
        bool Insert2(Ticket t, string userCode, out string msm);
    }
}
