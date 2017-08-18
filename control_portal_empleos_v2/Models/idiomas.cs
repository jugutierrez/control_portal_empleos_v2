using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace control_portal_empleos_v2.Models
{
    [Table("idiomas")]
    public class idiomas
    {
        [Key]
        [Column(Order = 1)]
        public int id_idioma { get; set; }

        public string nombre_idioma { get; set; }
    }
}
