using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class InputFields_Loguin
    {
        [Required(ErrorMessage = " El codigo de usuario es requerido")]
        [Display(Name = "codigo")]
        public string codigo { get; set; }

        [MinLength(6, ErrorMessage = "El número de caracteres de la {0} debe ser al menos de 6 caracteres")]
        [MaxLength(20, ErrorMessage = "El número de caracteres de la {0} debe ser por lo mas 20 caracteres")]
        [Required(ErrorMessage = " La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        public InputFields_Loguin()
        {

        }

        public InputFields_Loguin(string codigo, string contraseña)
        {
            this.codigo = codigo;
            this.contraseña = contraseña;
        }
    }
}