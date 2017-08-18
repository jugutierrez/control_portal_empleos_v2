using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class usuarios_ofertas_vista
    {
        [Key]
        [Column(Order = 1)]

        public int id_usuario { get; set; }

        public int id_oferta { get; set; }

        public int id_area { get; set; }
        public string nombre_area { get; set; }
        public virtual areas areas { get; set; }

        public string nombre_oferta { get; set; }

        public string descripcion_oferta { get; set; }

        public int monto_oferta { get; set; }

        public int id_jornada_oferta { get; set; }
        public string nombre_jornada_oferta { get; set; }
        public virtual jornada_ofertas jornada_ofertas { get; set; }

        public int id_contrato_oferta { get; set; }
        public string nombre_contrato_oferta { get; set; }
        public virtual contrato_ofertas contrato_ofertas { get; set; }

        public int id_tipo_oferta { get; set; }
        public string nombre_tipo_oferta { get; set; }
        public virtual tipo_ofertas tipo_ofertas { get; set; }

    }
}