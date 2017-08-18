using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class idiomas_curriculums_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_idioma_curriculum { get; set; }

        public int id_idioma { get; set; }
        public string nombre_idioma { get; set; }
        public virtual idiomas idiomas { get; set; }

        public int id_curriculum { get; set; }

        public virtual curriculums curriculums { get; set; }

        public int id_nivel_idioma { get; set; }
        public string nombre_nivel_idioma { get; set; }
        public virtual nivel_idiomas nivel_idiomas { get; set; }
    }
}