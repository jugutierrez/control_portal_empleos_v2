using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class vista_pie_detalle_oferta
    {

        public int id_estado_postulacion { get; set; }

        public int cantidad_postulados { get; set; }

        public string nombre_estado_postulacion { get; set; }
    }
}