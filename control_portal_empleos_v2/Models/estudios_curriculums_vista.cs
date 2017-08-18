using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class estudios_curriculums_vista
    {


        [Key]
        [Column(Order = 1)]
        public int id_estudio_curriculum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_inicio_estudio_curriculum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_termino_estudio_curriculum { get; set; }

        public string nombre_estudio { get; set; }

        public string nombre_tipo_estudio { get; set; }

        public string nombre_estado_estudio { get; set; }

        public string nombre_institucion { get; set; }
    }
}