using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Usuario_Modulo_Rol
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el usuario")]
        [Display(Name = "usuario")]
        public string usuario_id { get; set; }

        [Required(ErrorMessage = "Es necesario la relación modulo y rol")]
        [Display(Name = "modulo_rol")]
        public int modulo_rol_id { get; set; }

        public char estado { get; set; }

        [Required(ErrorMessage = "Es necesario el contenido del comentario")]
        [MaxLength(50, ErrorMessage = "El comentario debe tener un maximo de 50 caracteres")]
        [Display(Name = "comentario")]
        public string comentario { get; set; }
    }
}
