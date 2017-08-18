using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class vista_listado_cronologia_persona
    {
        [Key]
        [Column(Order = 1)]
        public int id_cronologia { get; set; }

        public string observacion_cronologica { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_registro_accion { get; set; }

        public string nombre_accion_cronologica { get; set; }
    }
}