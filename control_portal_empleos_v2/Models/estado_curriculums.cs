using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("estado_curriculums")]
    public class estado_curriculums
    {
        [Key]
        [Column(Order = 1)]
        public int id_estado_curriculum { get; set; }

        public string nombre_estado_curriculum { get; set; }

    }
}