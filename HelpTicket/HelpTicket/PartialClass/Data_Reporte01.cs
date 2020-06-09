using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class Data_Reporte01
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string tkAsignados { get; set; }
        public string tkFinalizados { get; set; }

        public Data_Reporte01()
        {
            codigo = string.Empty;
            nombre = string.Empty;
            tkAsignados = string.Empty;
            tkFinalizados = string.Empty;
        }
    }
}