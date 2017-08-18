using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("preguntas_respuestas")]
    public class preguntas_respuestas
    {
        [Key]
        [Column(Order = 1)]
        public int id_pregunta_respuesta { get; set; }

        public int id_pregunta { get; set; }
        public virtual preguntas preguntas { get; set; }

        public int id_respuesta { get; set; }
        public virtual respuestas respuestas { get; set; }

    }
}