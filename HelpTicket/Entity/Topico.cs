using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Topico
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre del topico")]
        [MaxLength(70, ErrorMessage = "El nombre del topico debe tener un maximo de 70 caracteres")]
        [Display(Name = "topico")]
        public string topico { get; set; }

        [Required(ErrorMessage = "Es necesario el departamento")]
        [Display(Name = "departamento")]
        public int departamento_id { get; set; }

        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public string usuario_modificacion { get; set; }
        public char estado { get; set; }
		public string acceso_usuario { get; set; }
		
		public Departamento Departamento { get; set; }
	}
}
