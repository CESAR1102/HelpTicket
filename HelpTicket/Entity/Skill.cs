using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Skill
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario el usuario al cual se le asignara un skill")]
        [Display(Name = "usuario")]
        public string usuario_id { get; set; }

        [Required(ErrorMessage = "Es necesario el topico de conocimiento para el skill")]
        [Display(Name = "topico")]
        public int topico_id { get; set; }

        public short nivel { get; set; }
    }
}
