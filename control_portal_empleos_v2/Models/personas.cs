using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("personas")]
    public class personas
    {
        [Key]
        [Column(Order = 1)]
        public int id_persona { get; set; }

        public string nombre_persona { get; set; }

        public string apellido_paterno_persona { get; set; }

        public string apellido_materno_persona { get; set; }

        public string correo_electronico_persona { get; set; }

        public DateTime fecha_nacimiento_persona { get; set; }

        public DateTime fecha_creacion_persona { get; set; }

        public DateTime fecha_modificacion_persona { get; set; }

        public string identificacion_persona { get; set; }

        public string clave_persona { get; set; }

        public int id_tipo_persona { get; set; }

        public int id_estado_persona { get; set; }

        public int id_curriculum { get; set; }

        public int id_comuna { get; set; }

        public int id_tipo_identificacion_persona { get; set; }



    }
}