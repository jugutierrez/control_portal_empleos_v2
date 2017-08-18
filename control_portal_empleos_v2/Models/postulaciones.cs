using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace control_portal_empleos_v2.Models
{
    [Table("postulaciones")]
    public class postulaciones
    {
        [Key]
        [Column(Order = 1)]
        public int id_postulacion { get; set; }

        public int id_estado_postulacion { get; set; }
        public virtual estado_postulaciones estado_postulaciones { get; set; }

        public int id_persona { get; set; }
        public virtual personas personas { get; set; }

        public int id_oferta { get; set; }
        public virtual ofertas ofertas { get; set; }

    }
}
