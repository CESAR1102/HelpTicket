using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Util
{
    public class FinalizarTicket
    {
        [Required(ErrorMessage = " Debe seleccionar un ticket")]
        [Display(Name = "ticket")]
        public string ticket { get; set; }

        [Required(ErrorMessage = " Debe escribir un mensaje para la solicitud")]
        [Display(Name = "Contenido")]
        [MaxLength(ErrorMessage = " Debe tener un maximo de 1000 caracteres")]
        public string Contenido { get; set; }
    }
}
