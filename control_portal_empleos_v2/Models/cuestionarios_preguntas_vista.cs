using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class cuestionarios_preguntas_vista
 

    {
        [Key]
        [Column(Order = 1)]
        public int id_cuestionario_pregunta { get; set; }

        public int id_cuestionario { get; set; }

        public int id_pregunta { get; set; }

        public string nombre_pregunta { get; set; }

        public int id_tipo_pregunta { get; set; }


       
 


    }
}