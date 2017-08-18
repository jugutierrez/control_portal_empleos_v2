using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public abstract class modelo_ofertas_usuario
    {
    

  



        public List<usuarios_ofertas> usuarios_ofertas { get; set; }

        public List<ofertas> ofertas { get; set; }


        public List<usuarios> usuarios { get; set; }


    }

    public class modelo_super_usuario_ofertas : modelo_ofertas_usuario
    { 
    }
}