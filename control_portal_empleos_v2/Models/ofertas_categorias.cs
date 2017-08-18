using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("ofertas_categorias")]
    public class ofertas_categorias
    {
        [Key]
        [Column(Order = 1)]

        public int id_oferta_categoria{ get; set; }

        public int id_oferta { get; set; }

        public int id_categoria { get; set; }

        public virtual categorias categorias { get; set; }

    }
}