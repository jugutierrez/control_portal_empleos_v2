using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    
        [Table("estado_ofertas")]
        public class estado_ofertas
        {
            [Key]
            [Column(Order = 1)]
            public int id_estado_oferta { get; set; }

            public string nombre_estado_oferta { get; set; }
        }
    
}