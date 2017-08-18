using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{   
    public class modelo_cuentas
    { private List<usuarios> lista_cuentas = new List<usuarios>();
        PersonaDBContext db = new PersonaDBContext();

        public modelo_cuentas()
        {
            lista_cuentas = db.usuarios.ToList();
            
        }
        public usuarios find(string user)
        {
            return lista_cuentas.Where(acc => acc.id_usuario.Equals(Convert.ToInt32(user))).FirstOrDefault();
        }

        public usuarios login(string user, string pass)

        {
            return lista_cuentas.Where(acc => acc.nombre_cuenta_usuario.Equals(user) && acc.clave_usuario.Equals(pass)).FirstOrDefault();
        }
    
    }
}