﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    [Table("regiones")]
    public class regiones
    {
        [Key]
        [Column(Order = 1)]
        public int id_region { get; set; }

        public string nombre_region { get; set; }
    }
}