using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class especificaciones_ofertas_idiomas_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_especificacion_oferta_idioma { get; set; }

        public int id_idioma { get; set; }
        public string nombre_idioma { get; set; }
        public virtual idiomas idiomas { get; set; }

        public int id_especificacion_oferta { get; set; }
        public virtual especificaciones_ofertas especificaciones_ofertas { get; set; }

        public int id_nivel_idioma { get; set; }
        public string nombre_nivel_idioma { get; set; }
        public virtual nivel_idiomas nivel_idiomas { get; set; }

        public int id_nivel_importancia { get; set; }
        public string nombre_nivel_importancia { get; set; }
        public virtual nivel_importancia nivel_importancia { get; set; }
    }
}