using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("especificaciones_ofertas_softwares")]
    public class especificaciones_ofertas_softwares
    {
        [Key]
        [Column(Order = 1)]
        public int id_especificacion_oferta_software { get; set; }


        public int id_especificacion_oferta { get; set; }
        public virtual especificaciones_ofertas especificaciones_ofertas { get; set; }

        public int id_grado_conocimiento_software { get; set; }
        public virtual grado_conocimiento_softwares grado_conocimiento_softwares { get; set; }

        public int id_software { get; set; }
        public virtual softwares softwares { get; set; }

        public int id_nivel_importancia { get; set; }
  
        public virtual nivel_importancia nivel_importancia { get; set; }
    }
}