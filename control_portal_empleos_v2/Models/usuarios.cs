using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("usuarios")]
    public class usuarios
    {
        [Key]
        [Column(Order = 1)]
        public int id_usuario { get; set; }

        public string nombre_usuario { get; set; }

        public string apellido_paterno_usuario { get; set; }

        public string apellido_materno_usuario { get; set; }

        public string rut_usuario { get; set; }

        public string clave_usuario { get; set; }

        public long id_grupo_usuario { get; set; }
        //public virtual areas areas { get; set; }

        public int id_tipo_usuario { get; set; }
        public virtual tipo_usuarios tipo_usuarios { get; set; }

        public int id_estado_usuario { get; set; }
        public virtual estado_usuarios estado_usuarios { get; set; }


        public string dig_rut_usuario { get; set; }

        public int anexo_usuario { get; set; }

        public string correo_electronico_usuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_creacion_usuario { get; set; }

        public string nombre_cuenta_usuario { get; set; }

        public byte[] foto_usuario { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase file { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_actualizacion_usuario { get; set; }


    }
}