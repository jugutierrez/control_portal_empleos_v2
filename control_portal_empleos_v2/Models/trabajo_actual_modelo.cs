using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class trabajo_actual_modelo
    {
        [Key]
        [Column(Order = 1)]
        public int id_trabajo_actual { get; set; }

        public string nombre_respuesta_trabajo_actual { get; set; }
    }
}