using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Modulo
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre del modulo")]
        [MaxLength(50, ErrorMessage = "El nombre del modulo debe tener un maximo de 50 caracteres")]
        [Display(Name = "modulo")]
        public string modulo { get; set; }

        public char estado { get; set; }
    }
}
