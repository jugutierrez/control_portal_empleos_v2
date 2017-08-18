using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class habilidades_curriculums_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_habilidad_curriculum { get; set; }

        public int id_habilidad { get; set; }
        public string nombre_habilidad { get; set; }
        //public virtual habilidades habilidades { get; set; }

        public int id_curriculum { get; set; }
        //public virtual curriculums curriculums { get; set; }

        public int id_grado_habilidad { get; set; }
        public string nombre_grado_habilidad { get; set; }
       // public virtual grado_habilidades grado_habilidades { get; set; }

    }
}