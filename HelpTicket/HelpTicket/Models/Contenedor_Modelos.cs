using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.Models
{
	public class Contenedor_Modelos
	{
		public IEnumerable<HelpTicket.PartialClass.Grafica_Reporte> Grafica_Reporte { get; set; }
		public IEnumerable<HelpTicket.PartialClass.Grafica_Reporte2> Grafica_Reporte2 { get; set; }
		public IEnumerable<HelpTicket.PartialClass.Grafica_Reporte3> Grafica_Reporte3 { get; set; }
	}
}