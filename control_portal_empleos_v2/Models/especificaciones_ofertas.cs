using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("especificaciones_ofertas")]
    public class especificaciones_ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_especificacion_oferta { get; set; }

        public long codigo_oferta { get; set; }
    }
}