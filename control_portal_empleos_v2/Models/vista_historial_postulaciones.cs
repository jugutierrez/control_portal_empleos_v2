using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class vista_historial_postulaciones
    {
        public string nombre_oferta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_postulacion { get; set; }

        public string nombre_estado_postulacion { get; set; }
    }
}