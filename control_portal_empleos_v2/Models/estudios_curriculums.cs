using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
     [Table("estudios_curriculums")]
    public class estudios_curriculums
    {
        [Key]
        [Column(Order = 1)]
        public int id_estudio_curriculum { get; set; }

        public int id_estudio { get; set; }
        public virtual estudios estudios { get; set; }

        public int id_curriculum { get; set; }
        public virtual curriculums curriculums { get; set; }

        public int id_estado_estudio { get; set; }
        public virtual estado_estudios estado_estudios { get; set; }

        public int id_tipo_estudio { get; set; }
        public virtual tipo_estudios tipo_estudios { get; set; }

        public int id_institucion { get; set; }
        public virtual instituciones instituciones { get; set; }

        public string ano_inicio_estudio_curriculum { get; set; }

        public string ano_termino_estudio_curriculum { get; set; }
    }
}