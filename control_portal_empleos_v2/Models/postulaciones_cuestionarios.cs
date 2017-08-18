using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
     [Table("postulaciones_preguntas")]
    public class postulaciones_cuestionarios
    {

        [Key]
        [Column(Order = 1)]
        public int id_postulacion_pregunta { get; set; }

        public string respuesta_usuarios { get; set; }

        public string respuestas_id { get; set; }

        public int id_pregunta { get; set; }
        public virtual preguntas preguntas { get; set; }

        public int id_postulacion { get; set; }
        public virtual postulaciones postulaciones { get; set; }
    }
}