using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Modulo_Rol
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el modulo")]
        [Display(Name = "modulo")]
        public int modulo_id { get; set; }

        [Required(ErrorMessage = "Es necesario el rol")]
        [Display(Name = "rol")]
        public int rol_id { get; set; }

        public char estado { get; set; }
    }
}
