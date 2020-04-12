using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class InputFields_ForgotPassword
    {
        [Required(ErrorMessage = " El correo es obligatorio")]
        [Display(Name = "correo")]
        public string correo { get; set; }

        public InputFields_ForgotPassword()
        {

        }

        public InputFields_ForgotPassword(string correo)
        {
            this.correo = correo;
        }
    }
}