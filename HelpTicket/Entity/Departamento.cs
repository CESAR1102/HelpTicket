using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Departamento
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre del departamento")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener un maximo de 50 caracteres")]
        [Display(Name = "departamento")]
        public string departamento { get; set; }

        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }

        public string usuario_modificacion { get; set; }
        public char estado { get; set; }
    }
}
