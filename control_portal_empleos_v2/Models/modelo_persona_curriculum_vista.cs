using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class modelo_persona_curriculum_vista
    {

          [Key]
        [Column(Order = 1)]
        public int id_persona { get; set; }

        public string nombre_persona { get; set; }


        public string  correo_electronico_persona { get; set; }

       
    }
}