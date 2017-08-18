using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class Correo_Soporte
    {
        public string nombre_remitente { get; set; }

        public string  correo_remitente { get; set; }

        public int anexo_remitente { get; set; }

        public string asunto_soporte { get; set; }

        public string mensaje_soporte { get; set; }

        public byte[] foto_soporte { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase file { get; set; }

    }
}