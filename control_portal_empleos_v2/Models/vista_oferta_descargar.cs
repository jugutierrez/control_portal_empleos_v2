using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class vista_oferta_descargar
    {
        public int id_oferta { get; set; }

        public string nombre_oferta { get; set; }

        public string descripcion_oferta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_creacion_oferta { get; set; }

        public int monto_oferta { get; set; }

        public string  nombre_tipo_oferta { get; set; }

        public string nombre_jornada_oferta { get; set; }

        public string nombre_contrato_oferta { get; set; }

        public string nombre_area { get; set; }

        public string  nombre_departamento { get; set; }

        public string  nombre_direccion { get; set; }

        public int oferta_inclusiva { get; set; }
    }
}