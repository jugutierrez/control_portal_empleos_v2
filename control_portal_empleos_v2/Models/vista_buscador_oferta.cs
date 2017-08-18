using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class vista_buscador_oferta
    {
        [Key]
        [Column(Order = 1)]
        public int id_oferta { get; set; }

        public string nombre_oferta { get; set; }

        public string nombre_estado_oferta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public  DateTime fecha_creacion_oferta  { get; set; }
    }
}