using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Mensaje
    {
        public int id { get; set; }
        public string remitente_id { get; set; }

        [Required(ErrorMessage = "Es necesario el detinatario para enviar del mensaje")]
        [Display(Name = "destinatario")]
        public string destinatario_id { get; set; }

        public DateTime enviado_el { get; set; }

        [Required(ErrorMessage = "Es necesario el asunto para enviar del mensaje")]
        [MaxLength(100, ErrorMessage ="Un máximo de 100 caracteres")]
        [Display(Name = "asunto")]
        public string asunto { get; set; }

        [Required(ErrorMessage = "Es necesario el motivo para enviar del mensaje")]
        [MaxLength(40, ErrorMessage = "Un máximo de 40 caracteres")]
        [Display(Name = "motivo")]
        public string motivo { get; set; }

        [Required(ErrorMessage = "Es necesario el contenido del mensaje")]
        [MaxLength(1000, ErrorMessage = "El contenido debe tener un maximo de 1000 caracteres")]
        [Display(Name = "departamento")]
        public string contenido { get; set; }

        [Required(ErrorMessage = "Es necesario el ticket para enviar del mensaje")]
        [Display(Name = "ticket")]
        public string ticket_id { get; set; }
    }
}
