using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class modelo_ofertas_grupo_usuario
    {
        [Key]
        [Column(Order = 1)]
        public int id_oferta { get; set; }

        public string nombre_oferta { get; set; }
    }
}