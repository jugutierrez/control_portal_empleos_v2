using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("cuestionarios_preguntas")]
    public class cuestionarios_preguntas
    {
        [Key]
        [Column(Order = 1)]
        public int id_cuestionario_pregunta { get; set; }

        public int id_pregunta { get; set; }
  
        public virtual preguntas preguntas { get; set; }

        public int id_cuestionario { get; set; }
        public virtual cuestionarios cuestionarios { get; set; }


    }
}