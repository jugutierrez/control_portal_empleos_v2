using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("tipo_usuarios")]
    public class tipo_usuarios
    {
        [Key]
        [Column(Order = 1)]
        public int id_tipo_usuario { get; set; }

        public string nombre_tipo_usuario { get; set; }
    }
}