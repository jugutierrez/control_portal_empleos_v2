
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using control_portal_empleos_v2.seguridad;

namespace control_portal_empleos_v2.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    
        public PartialViewResult GetMenu(int? id)
        {
            if (id == 1)
            { ViewBag.id = 1; }
            else
            { ViewBag.id = 0; }

            return PartialView("_vista_usuarios_pendientes_adm");
        }
    }
}