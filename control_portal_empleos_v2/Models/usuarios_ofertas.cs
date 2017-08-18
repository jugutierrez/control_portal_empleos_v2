using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("usuarios_ofertas")]
    public class usuarios_ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_usuario_oferta { get; set; }

        public int id_usuario { get; set; }
        public virtual usuarios usuarios { get; set; }


        public int id_oferta { get; set; }
        public virtual ofertas ofertas { get; set; }

        public int id_area { get; set; }
        public virtual areas areas { get; set; }

        public int id_estado_oferta { get; set; }
        public virtual estado_ofertas estado_ofertas { get; set; }
    }
}