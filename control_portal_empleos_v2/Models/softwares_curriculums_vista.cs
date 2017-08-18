using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class softwares_curriculums_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_software_curriculum { get; set; }

        public int id_curriculum { get; set; }
        public virtual curriculums curriculums { get; set; }

        public int id_software { get; set; }
        public string nombre_software { get; set; }
        public virtual softwares softwares { get; set; }

        public int id_grado_conocimiento_software { get; set; }
        public string nombre_grado_conocimiento_software { get; set; }
        public virtual grado_conocimiento_softwares grado_conocimiento_softwares { get; set; }
    }
}