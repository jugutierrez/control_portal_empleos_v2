using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("relevancia_documentos")]
    public class relevancia_documentos
    {
        [Key]
        [Column(Order = 1)]

        public int id_relevancia_documento { get; set; }

        public string nombre_relevancia_documento { get; set; }
    }
}