using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("ofertas_documentos")]
    public class ofertas_documentos
    {
        [Key]
        [Column(Order = 1)]
        public int id_oferta_documento { get; set; }
        public int id_oferta { get; set; }

        public int id_documento { get; set; }
        public virtual documentos documentos { get; set; }

        public int id_relevancia_documento{ get; set; }
        public virtual relevancia_documentos relevancia_documentos { get; set; }

        public string nombre_documento_oferta { get; set; }

        public string enlace_documento_oferta { get; set; }
    }
}