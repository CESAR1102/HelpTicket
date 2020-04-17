using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Rol
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre del rol")]
        [MaxLength(50, ErrorMessage = "El nombre del rol debe tener un maximo de 50 caracteres")]
        [Display(Name = "rol")]
        public string rol { get; set; }

        public char estado { get; set; }
    }
}
