using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class TopicoSelected
    {
        public int id { get; set; }
        public string nombre {get;set;}
        public bool selected { get; set; }

        public TopicoSelected()
        {
            id = 0;
            nombre = string.Empty;
            selected = false;
        }
        public TopicoSelected(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
            this.selected = false;
        }
    }
}