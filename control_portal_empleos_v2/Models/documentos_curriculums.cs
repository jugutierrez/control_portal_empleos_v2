using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class documentos_curriculums
    {
        [Key]
        [Column(Order = 1)]
        public long id_documento_curriculum { get; set; }

        public string nombre_documento_curriculum{ get; set; }

        public string enlace_documento_curriculum { get; set; }

        public string nombre_documento { get; set; }
    }

}