using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class ListadoDepbyTop
    {
        public string nombreDepartamento { get; set; }
        public List<TopicoSelected> topicos = new List<TopicoSelected>(); 
    }
}