using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("ofertas")]
    public class ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_oferta { get; set; }

        public string nombre_oferta { get; set; }

        public string descripcion_oferta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_creacion_oferta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_modificacion_oferta { get; set; }

       
        public int monto_oferta { get; set; }

        public int id_jornada_oferta { get; set; }
        public virtual jornada_ofertas jornada_ofertas { get; set; }

        public int id_contrato_oferta { get; set; }
        public virtual contrato_ofertas contrato_ofertas { get; set; }

        public int id_tipo_oferta { get; set; }
        public virtual tipo_ofertas tipo_ofertas { get; set; }

        public int id_cuestionario { get; set; }
        public virtual cuestionarios cuestionarios { get; set; }


        public int id_especificacion_oferta { get; set; }
        public virtual especificaciones_ofertas especificaciones_ofertas { get; set; }

        public int oferta_inclusiva { get; set; }



    }
}