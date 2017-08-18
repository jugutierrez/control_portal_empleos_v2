using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public abstract class modelo_especificaciones
    {


        public int id_especificacion { get; set; }

        public List <especificaciones_ofertas_habilidades_vista> especificaciones_ofertas_h { get; set; }

        public List <especificaciones_ofertas_idiomas_vista> especificaciones_ofertas_i { get; set; }

        public List <especificaciones_ofertas_profesiones_vista> especificaciones_ofertas_p { get; set; }

        public List <especificaciones_ofertas_softwares_vista> especificaciones_ofertas_s { get; set; }
    }

    public class MainPageViewModel : modelo_especificaciones
    {
    }
}