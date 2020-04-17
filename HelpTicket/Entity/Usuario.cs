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
        [Required(ErrorMessage = " El codigo de usuario es requerido")]
        [Display(Name = "codigo")]
        public string codigo { get; set; }

        [MaxLength(50, ErrorMessage = "El número de caracteres de los {0} deben ser por lo mas 50 caracteres")]
        [Required(ErrorMessage = "Los nombres son requeridos")]
        [Display(Name = "nombres")]
        public string nombres { get; set; }

        [MaxLength(50, ErrorMessage = "El número de caracteres del {0} debe ser por lo mas 50 caracteres")]
        [Required(ErrorMessage = "El correo es requerido")]
        [Display(Name = "correo")]
        public string correo { get; set; }

		[MinLength(6, ErrorMessage = "El número de caracteres de la {0} debe ser al menos de 6 caracteres")]
        [MaxLength(20, ErrorMessage = "El número de caracteres de la {0} debe ser por lo mas 20 caracteres")]
        [Required(ErrorMessage = " La contraseña es requerida")]
		[Display(Name = "Contraseña")]
		public string contraseña { get; set; }

		public string rol_creacion { get; set; }
		public DateTime fecha_creacion { get; set; }
		public string token { get; set; }

		
	}
}
