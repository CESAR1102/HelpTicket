using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpTicket.Models
{
	public class UserLogin
	{
		public int ID_usuario { get; set; }
		public string nombres { get; set; }
		public string correo { get; set; }

		[Required(ErrorMessage = " El usuario es requerido")]
		[Display(Name = "codigo")]
		public string codigo { get; set; }

		[StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos de 6 caracteres")]
		[Required(ErrorMessage = " La contraseña es requerida")]
		[Display(Name = "Contraseña")]
		public string Contraseña { get; set; }

		public string Rol { get; set; }
		public DateTime Fecha_creacion { get; set; }
		public string token { get; set; }


		WebApp_TicketEntities user = new WebApp_TicketEntities();
		public bool logeo()
		{
			var query = from u in user.Usuario
						where u.codigo== codigo && u.contraseña== Contraseña
						select u;
			
			if (query.Count() > 0)
			{
				var datos = query.ToList();
				foreach (var Data in datos)
				{
					ID_usuario = Data.ID_usuario;
					nombres = Data.nombres;
					codigo = Data.codigo;
					correo = Data.correo;
					Contraseña = Data.contraseña;
					
				}
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}