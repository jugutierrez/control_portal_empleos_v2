﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public class nivel_importancia
    {
        [Key]
        [Column(Order = 1)]

        public int id_nivel_importancia { get; set; }

        public string nombre_nivel_importancia { get; set; }
    }
}