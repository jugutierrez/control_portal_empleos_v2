using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("departamentos")]
    public class departamentos
    {
        [Key]
        [Column(Order = 1)]
        public int id_departamento { get; set; }

        public string nombre_departamento { get; set; }

        public int id_direccion { get; set; }
        public virtual direcciones direcciones { get; set; }


    }
}