using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class InputFields_Recovery
    {
        public string codigo { get; set; }

        [MaxLength(20, ErrorMessage = "El número de caracteres de la {0} debe ser por lo mas de 20 caracteres")]
        [MinLength(6,ErrorMessage = "El número de caracteres de la {0} debe ser al menos de 6 caracteres")]
        [Required(ErrorMessage = " La nueva contraseña es obligatoria")]
        [Display(Name = "contraseña")]
        public string password1 { get; set; }

        [Required(ErrorMessage = " Repetir la contraseña es obigatorio")]
        [Display(Name = "contraseña")]
        public string password2 { get; set; }

        public InputFields_Recovery()
        {

        }

        public InputFields_Recovery(string codigo, string password1, string password2)
        {
            this.codigo = codigo;
            this.password1 = password1;
            this.password2 = password2;
        }
    }
}