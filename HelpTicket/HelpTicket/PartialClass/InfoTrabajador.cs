using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class InfoTrabajador
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public List<string> skills { get; set; }
        public string tkAsignados { get; set; }
        public string tkFinalizados { get; set; }
   
        public InfoTrabajador()
        {
            codigo = string.Empty;
            nombre = string.Empty;
            correo = string.Empty;
            skills = new List<string>();
            tkAsignados = string.Empty;
            tkFinalizados = string.Empty;
        }
    }
}