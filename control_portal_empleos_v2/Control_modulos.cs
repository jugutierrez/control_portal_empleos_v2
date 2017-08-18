using control_portal_empleos_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace control_portal_empleos_v2
{
    public class Control_modulos
    {


        public List<modulos_activos> validador_modulos(int id_tipo_usuario)
        {

            PersonaDBContext db = new PersonaDBContext();
          
            List<modulos_activos> k = db.Database.SqlQuery<modulos_activos>("select id_modulo from tipo_usuario_modulo where id_tipo_usuario = {0}" , id_tipo_usuario).ToList();
            foreach (var l in k)

            {
                l.estado_activado = true;
            }
            return k;
        }
    }
}