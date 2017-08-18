using control_portal_empleos_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace control_portal_empleos_v2.Controllers
{
    public class SoporteController : Controller
    {
        PersonaDBContext db = new PersonaDBContext();
        //
        // GET: /Soporte/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult soporte()
        {
            if (Session["usuario_id"] != null)
            {
                var usuario = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));
                ViewBag.nombre_usuario = usuario.nombre_usuario;
                ViewBag.anexo_usuario = usuario.anexo_usuario;
                ViewBag.correo_usuario = usuario.correo_electronico_usuario;
                return PartialView("_soporte");
            }
            else
            {
                return PartialView("_soporte");
            }
        }

        [HttpPost]
        public async Task<JsonResult> correo_soporte([Bind(Include = "nombre_remitente, correo_remitente,anexo_remitente ,asunto_soporte ,mensaje_soporte , foto_soporte , file")] Correo_Soporte correo_soporte)

        {


            mail m = new mail();
            try
            {
                if (correo_soporte.nombre_remitente == null || correo_soporte.correo_remitente == null || correo_soporte.anexo_remitente < 1)
                {
                    return Json(new { success = false, responseText = "Flatan Completar Campos" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    if (correo_soporte.file == null) { }
                    else
                    {
                        byte[] data = new byte[correo_soporte.file.ContentLength];
                        correo_soporte.file.InputStream.Read(data, 0, correo_soporte.file.ContentLength);

                        correo_soporte.foto_soporte = data;
                    }

                    m.enviar_correo(correo_soporte.foto_soporte, correo_soporte.correo_remitente, 0);
                    return Json(new { success = true, responseText = "Correo Enviado Con Exito" }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception)
            {

                return Json(new { success = false, responseText = "Error al enviar el correo" }, JsonRequestBehavior.AllowGet);

            }





        }



    }
}