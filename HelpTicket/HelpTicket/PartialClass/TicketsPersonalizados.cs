using Business;
using Business.Implementar;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class TicketsPersonalizados
    {
        public string codigo_atencion { get; set; }
        public string solicitante_nombre { get; set; }
        public string solicitante_codigo { get; set; }
        public string topico { get; set; }
        public string departamento { get; set; }
        public string asignado_nombre { get; set; }
        public string asignado_codigo { get; set; }
        public string importancia { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public DateTime fecha_solicitado { get; set; }
        public DateTime? fecha_asignado { get; set; }
        public DateTime? fecha_finalizado { get; set; }
        public string aprobador_nombre { get; set; }
        public string aprobador_codigo { get; set; }

        public TicketsPersonalizados()
        {

        }

        public List<TicketsPersonalizados> ConvertToTicketsPersonalizados(List<Ticket> data)
        {
            List<TicketsPersonalizados> nuevosTickets = new List<TicketsPersonalizados>();
            DatosCompartidos convertidor = new DatosCompartidos();
            if (data != null)
            {
                foreach(var item in data)
                {
                    TicketsPersonalizados t = new TicketsPersonalizados();
                    t.codigo_atencion = item.codigo_atencion;
                    t.solicitante_nombre = "p";
                    t.solicitante_codigo = "p";
                    t.topico = "p";
                    t.departamento = "p";
                    t.asignado_nombre = "p";
                    t.asignado_codigo = "p";
                    t.importancia = convertidor.GetImportancia(item.estado);
                    t.descripcion = item.descripcion;
                    t.estado = convertidor.GetEstado(item.estado);
                    t.fecha_solicitado = item.fecha_solicitado;
                    t.fecha_asignado = item.fecha_asignado;
                    t.fecha_finalizado = item.fecha_finalizado;
                    t.aprobador_nombre = "p";
                    t.aprobador_codigo = "p";
                    nuevosTickets.Add(t);
                }
            }
            return nuevosTickets;
        }
    }
}