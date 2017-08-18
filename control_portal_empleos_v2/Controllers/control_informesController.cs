using control_portal_empleos_v2.Models;
using control_portal_empleos_v2.seguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Rotativa;


namespace control_portal_empleo.Controllers
{
    public class control_informesController : Controller
    {
        //
        // GET: /control_informes/

        PersonaDBContext db = new PersonaDBContext();

        [CustomAuthorize]
        public ActionResult Index()
        {
            filtro_total();
            return View();
        }

        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            gv.DataSource = db.Database.SqlQuery<areas>("select * from areas").ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return Json(new { success = true, responseText = "lol" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult postulados_por_oferta(int? id)
        {
            var chartsdata = db.Database.SqlQuery<vista_pie_detalle_oferta>
                ("exec sp_cantidad_postulados_oferta_estado @id_oferta = {0}", id).ToList();



            return Json(chartsdata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult postulados_por_ano()
        {
            var chartsdata = db.Database.SqlQuery<vista_postulados_mes_año_usuario>
                ("exec sp_obtener_total_postulados_por_mes_usuario @id_usuario = {0}", Convert.ToInt32(Session["usuario_id"])).ToList();



            return Json(chartsdata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult postulados_por_oferta_ultima(int id_usuario, int id_oferta)
        {
            var chartsdata2 = db.Database.SqlQuery<vista_pie_detalle_oferta>
                ("exec sp_cantidad_postulados_ultima_oferta_usuario @id_usuario = {0} , @id_oferta = {1}", id_usuario, id_oferta).ToList();



            return Json(chartsdata2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult informes_filtros(int? id)

        {

            ViewBag.id_oferta = new SelectList(db.ofertas, "id_oferta", "nombre_oferta");
            return PartialView("_control_informes_oferta");
        }
        public PartialViewResult crea_pdf(int id_oferta)
        {
            //vista_oferta_descargar k = db.Database.SqlQuery<vista_oferta_descargar>("exec sp_obtener_datos_postulados_oferta @id_oferta = {0}", id_oferta).Single();

            ViewBag.nombre_oferta = "bla bla";
            return PartialView("_formato_informe_pdf");
        }

        public ActionResult descarga_informe_pdf(int? id_oferta)
        {



            return new Rotativa.ActionAsPdf("crea_pdf", new { id_oferta = id_oferta }) { FileName = "informe_datos_contacto_oferta.pdf" };


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