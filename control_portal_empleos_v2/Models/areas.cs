using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("areas")]
    public class areas
    {
        [Key]
        [Column(Order = 1)]
        public int id_area { get; set; }

        public string nombre_area { get; set; }

        public int id_departamento { get; set; }
        public virtual departamentos departamentos { get; set; }

    }
}