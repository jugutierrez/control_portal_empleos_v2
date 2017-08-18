using control_portal_empleos_v2.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using control_portal_empleos_v2.seguridad;


namespace control_portal_empleos_v2.Controllers
{
    public class control_cuentaController : Controller
    {
        //
        // GET: /control_cuenta/
        PersonaDBContext db = new PersonaDBContext();
        [CustomAuthorize]
        public ActionResult Index()
        {


            usuarios usuario = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));
            return View(usuario);
        }

        public PartialViewResult vista_foto()
        {


            usuarios usuario = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));

            return PartialView("_info_usuario_cuenta", usuario);

        }

        public PartialViewResult vista_actualiza_foto_usuario(int? id)
        {
            usuarios usuario = db.usuarios.Find(id);
            return PartialView("_actualiza_foto_usuario", usuario);
        }

        public PartialViewResult vista_actualiza_clave_usuario(int? id)
        {
            ViewBag.id = id;
            return PartialView("_vista_actualiza_clave_usuario");
        }

        public PartialViewResult vista_actualiza_datos_contacto_usuario(int? id)
        {
            usuarios usuario = db.usuarios.Find(id);
            return PartialView("_vista_actualiza_datos_contacto_usuario", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult actualiza_datos_contacto_usuario(int id_usuario, string correo_electronico_usuario, int anexo_usuario)
        {
            try
            {
                if (correo_electronico_usuario != null || anexo_usuario != 0)
                {
                    db.Database.ExecuteSqlCommand("exec sp_actualiza_datos_contacto_usuario @id_usuario = {0} , @correo_usuario = {1} , @anexo_usuario = {2}", id_usuario, correo_electronico_usuario, anexo_usuario);
                    return Json(new { success = true, responseText = "se han actualizado los datos correctamente" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "no has completado algun campo" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult actualiza_clave_usuario(int id_usuario, string clave_usuario, string clave_usuario_repite)
        {
            try
            {
                if (clave_usuario.ToUpper() == clave_usuario_repite.ToUpper())
                {
                    db.Database.ExecuteSqlCommand("exec sp_actualiza_clave_usuario @id_usuario = {0} , @clave_usuario = {1}", id_usuario, clave_usuario);
                    return Json(new { success = true, responseText = "su clave se ah actualizado correctamente" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "ambas claves no coinciden" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public virtual ActionResult update_foto([Bind(Include = "id_usuario , foto_usuario , file")] usuarios usuarios)
        {

            if (usuarios.file == null)
            {
                return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (usuarios.file.ContentLength > (2 * 1024 * 1024))
                {
                    return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
                }




                byte[] data = new byte[usuarios.file.ContentLength];
                usuarios.file.InputStream.Read(data, 0, usuarios.file.ContentLength);

                usuarios.foto_usuario = data;

                db.Database.ExecuteSqlCommand("Exec sp_actualiza_usuario_foto @id_usuario = {0}, @foto_usuario  = {1}", usuarios.id_usuario, usuarios.foto_usuario);

                return Json(new { success = true, responseText = "Descripcion Actualizada!" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoginPartial()
        {
            try
            {



                filtro_total();


                return PartialView("_LoginPartial");

            }
            catch (Exception ex)
            {

                return PartialView("_LoginPartial");

            }


        }

        public void filtro_total()
        {
            ViewBag.informes_basicos = false;
            ViewBag.informes_avanzados = false;
            ViewBag.informes_especiales = false;
            ViewBag.crear_ofertas = false;
            ViewBag.listado_ofertas = false;
            ViewBag.re_publicar_ofertas = false;
            ViewBag.editar_ofertas = false;
            ViewBag.ver_postulantes = false;
            ViewBag.editar_estado_p = false;
            ViewBag.ver_curriculums = false;
            ViewBag.invitar_postulante = false;
            ViewBag.compartir_curriculums = false;
            ViewBag.panel_administrador = false;

            foreach (var item in (List<modulos_activos>)Session["modulos"])
            {
                switch (item.id_modulo)
                {
                    case 0:
                        ViewBag.informes_basicos = true;
                        break;
                    case 1:
                        ViewBag.informes_avanzados = true;
                        break;
                    case 2:
                        ViewBag.informes_especiales = true;
                        break;
                    case 3:
                        ViewBag.crear_ofertas = true;
                        break;
                    case 4:
                        ViewBag.listado_ofertas = true;
                        break;
                    case 5:
                        ViewBag.re_publicar_ofertas = true;
                        break;
                    case 6:
                        ViewBag.editar_ofertas = true;
                        break;
                    case 7:
                        ViewBag.ver_postulantes = true;
                        break;
                    case 8:
                        ViewBag.editar_estado_p = true;
                        break;
                    case 9:
                        ViewBag.ver_curriculums = true;
                        break;
                    case 10:
                        ViewBag.invitar_postulante = true;
                        break;
                    case 11:
                        ViewBag.compartir_curriculums = true;
                        break;
                    case 12:
                        ViewBag.panel_administrador = true;
                        break;


                }
            }

        }
    }
}