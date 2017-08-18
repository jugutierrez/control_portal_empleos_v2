using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using control_portal_empleos_v2.Models;
namespace control_portal_empleos_v2.seguridad
{
    
    public class customPrincipal : IPrincipal
    {
        PersonaDBContext db = new PersonaDBContext();
        private usuarios cuentas;
  
        public customPrincipal(usuarios cuentas)
        {
            this.cuentas = cuentas;
            this.Identity = new GenericIdentity(cuentas.nombre_usuario);
        }
        public IIdentity Identity
        {
            get; set;
         
        }

    

         public bool IsInRole(string role)
        {
          var roles = role.Split(new char[] { ',' });
            //var rol = db.personas.Find(0);
            //string[] id = new string[1];
            // id[0] = rol.id_estado_persona.ToString();    ViewBag.informes_basicos = false;
            

            foreach (var item in SessionPersister.lista_modulos)
            {
            
            }

            return roles.Any(r => this.cuentas.id_tipo_usuario.ToString().Contains(r));
        }
    }
}