using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using control_portal_empleos_v2.seguridad;
using control_portal_empleos_v2.Models;
using System.Web.Routing;

namespace control_portal_empleos_v2.seguridad
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionPersister.activo == false)
            {
                
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "login", action = "index" }));
               
            }
            else

            {
                modelo_cuentas mc = new modelo_cuentas();
                customPrincipal cp = new customPrincipal(mc.find(SessionPersister.id_usuario));
                if (!cp.IsInRole(Roles ))
                    
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "login", action = "denegado" }));
            }
            }
        }
    
}