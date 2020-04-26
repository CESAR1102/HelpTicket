using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Ticket
    {
        public string codigo_atencion { get; set; }


		public Topico Topico { get; set; }

		public Usuario_Modulo_Rol Usuario_Modulo_Rol { get; set; }






		public int solicitante_id { get; set; }

        [Required(ErrorMessage = "Es necesario elegir un topico")]
        [Display(Name = "topico")]
        public int topico_id { get; set; }

        public int? asignado_id { get; set; }

        [Required(ErrorMessage = "Es necesario elegir un grado de importancia")]
        [Display(Name = "importancia")]
        public short importancia { get; set; }

        [Required(ErrorMessage = "Es necesario indicar el motivo del ticket")]
        [MaxLength(600,ErrorMessage ="El motivo debe tener un maximo de 600 caracteres")]
        [Display(Name = "descripcion")]
        public string descripcion { get; set; }

        public char estado { get; set; }
        public DateTime fecha_solicitado { get; set; }
        public DateTime? fecha_asignado { get; set; }
        public DateTime? fecha_finalizado { get; set; }

        public int? aprobador_id { get; set; }
    }
}
