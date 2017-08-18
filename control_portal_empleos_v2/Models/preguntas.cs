using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace control_portal_empleos_v2.Models
{
    [Table("preguntas")]
    public class preguntas
    {
        [Key]
        [Column(Order = 1)]
        public int id_pregunta { get; set; }

        public string nombre_pregunta { get; set; }

        public int id_tipo_pregunta { get; set; }
        public virtual tipo_preguntas tipo_preguntas { get; set; }



    }
}
