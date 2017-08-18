using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class vista_buscador_curriculum
    {
        [Key]       
        [Column(Order = 1)]
        public int id_persona { get; set; }

        public string nombre_persona { get; set; }

        public string apellido_paterno_persona { get; set; }

        public string  apellido_materno_persona { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_modificacion_persona { get; set; }

public string direccion_curriculum { get; set; }

        public byte[] foto_curriculum { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase file { get; set; }

        public string nombre_comuna { get; set; }

        public string nombre_ciudad { get; set; }

    public string nombre_region { get; set; }

    public string empresa_experiencia_laboral { get; set; }

    public string nombre_area_experiencia_laboral { get; set; }

    public string nombre_cargo_experiencia_laboral { get; set; }

    public string nombre_experiencia_laboral { get; set; }

    public string nombre_profesion { get; set; }


        
    }
}