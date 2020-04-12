using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
	public class Usuario
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

		
	}
}
