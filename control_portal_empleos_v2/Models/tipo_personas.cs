using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("tipo_personas")]
    public class tipo_personas
    {
        [Key]
        [Column(Order = 1)]
        public int id_tipo_persona { get; set; }

        public string nombre_tipo_persona { get; set; }
    }
}