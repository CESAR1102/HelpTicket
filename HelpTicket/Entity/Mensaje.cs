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
        public bool leido { get; set; }

        [Required(ErrorMessage = "Es necesario el contenido del mensaje")]
        [MaxLength(1000, ErrorMessage = "El contenido debe tener un maximo de 1000 caracteres")]
        [Display(Name = "departamento")]
        public string contenido { get; set; }

        public int ticket_id { get; set; }
    }
}
