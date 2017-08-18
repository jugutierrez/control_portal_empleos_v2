using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("cuestionarios")]
    public class cuestionarios
    {

        [Key]
        [Column(Order = 1)]
        public int id_cuestionario { get; set; }

        public long codigo_oferta { get; set; }
    }
}