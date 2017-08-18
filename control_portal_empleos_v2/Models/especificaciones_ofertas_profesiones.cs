using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("especificaciones_ofertas_profesiones")]
    public class especificaciones_ofertas_profesiones
    {
        [Key]
        [Column(Order = 1)]
        public int id_especificacion_oferta_profesion { get; set; }


        public int id_especificacion_oferta { get; set; }
        public virtual especificaciones_ofertas especificaciones_ofertas { get; set; }

        public int id_profesion { get; set; }
       
        public virtual profesiones profesiones { get; set; }

        public int id_nivel_importancia { get; set; }
       
        public virtual nivel_importancia nivel_importancia { get; set; }
    }
}