using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace control_portal_empleos_v2.Models
{
    [Table("respuestas")]
    public class respuestas
    {
        [Key]
        [Column(Order = 1)]
        public int id_respuesta { get; set; }

        public string nombre_respuesta { get; set; }
    }
}
