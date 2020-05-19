using Business;
using Business.Implementar;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class ManagerInfoTrabajador
    {
        private IUsuarioService usuarioservice = new UsuarioService();
        private ITicketService ticketservice = new TicketService();
        private ISkillService skillservice = new SkillService();
        public ManagerInfoTrabajador()
        {

        }
        public List<InfoTrabajador> LlenarDatos()
        {
            List<InfoTrabajador> infoTrabajadores = new List<InfoTrabajador>();
            List<Usuario> usuarios = usuarioservice.ObtenerTrabajadores();
            foreach(var item in usuarios)
            {
                InfoTrabajador info = new InfoTrabajador();
                info.codigo = item.codigo;
                info.correo = item.correo;
                info.nombre = item.nombres;

                info.skills = skillservice.SkillXtrabajadorSimple(info.codigo);

                info.tkAsignados = ticketservice.TicketsXtrabajadorXestado(info.codigo, "A");
                info.tkFinalizados = ticketservice.TicketsXtrabajadorXestado(info.codigo, "F");

                infoTrabajadores.Add(info);
            }
            return infoTrabajadores;
        }
    }
}