using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("estudios")]
    public class estudios
    {

        [Key]
        [Column(Order = 1)]
        public int id_estudio { get; set; }

 public string nombre_estudio { get; set; }
    }
}