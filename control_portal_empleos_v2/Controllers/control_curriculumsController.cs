using control_portal_empleos_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Rotativa;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Threading.Tasks;
using control_portal_empleos_v2.seguridad;
namespace control_portal_empleos_v2.Controllers
{
    public class control_curriculumsController : Controller
    {
        //
        // GET: /control_curriculums/

        PersonaDBContext db = new PersonaDBContext();


        public int itemsPerPage = 10;

        [CustomAuthorize]
        public ViewResult Index(int? page)
        {
            List<vista_buscador_curriculum> Articles;
            Articles = db.Database.SqlQuery<vista_buscador_curriculum>("exec sp_vista_buscador_curriculums").ToList();
            int pageNumber = (page ?? 1);
            ViewBag.control = "index";
            carga_combos();
            return View(Articles.ToPagedList(pageNumber, itemsPerPage));

        }

        public ViewResult filtrar_full(int? page, string hasta, string sexo, string id_comuna, string id_area_experiencia_laboral,
        string id_cargo_experiencia_laboral, string sueldo_hasta, string id_estudio, string id_institucion, string id_estado_estudio, string id_tipo_estudio,
        string id_idioma, string id_nivel_idioma, string ultimo_update, string id_ciudad, string id_region, FormCollection form)
        {
            List<vista_buscador_curriculum> Articles;
            modelo_postulaciones_vista m_p_v = new modelo_postulaciones_vista();
            super_modelo_curriculum s_m_c = new super_modelo_curriculum();
            string[] rad = new string[form.AllKeys.Count()];
            if (form.AllKeys.Length != 0)
            {
                for (int i = 0; i < form.AllKeys.Count(); i++)
                {
                    if (Request[form.Keys[i]] != "")
                    {
                        rad[i] = Request[form.Keys[i]];
                    }
                    else
                    {
                        rad[i] = null;
                    }

                }

            }
            else
            {
                rad = new string[15];

                rad[0] = hasta;
                rad[1] = sexo;
                rad[2] = id_ciudad;
                rad[3] = id_region;
                rad[4] = id_comuna;

                rad[5] = id_area_experiencia_laboral;
                rad[6] = id_cargo_experiencia_laboral;

                rad[7] = sueldo_hasta;
                rad[8] = id_estudio;
                rad[9] = id_institucion;
                rad[10] = id_estado_estudio;
                rad[11] = id_tipo_estudio;
                rad[12] = id_idioma;
                rad[13] = id_nivel_idioma;
                rad[14] = ultimo_update;

            }


            if (page != 1)
            { }
            ViewBag.filtros1 = rad[0];
            ViewBag.filtros2 = rad[1];
            ViewBag.filtros3 = rad[2];


            ViewBag.filtros4 = rad[5];

            ViewBag.filtros5 = rad[6];
            ViewBag.filtros6 = rad[7];
            ViewBag.filtros7 = rad[8];

            ViewBag.filtros8 = rad[9];
            ViewBag.filtros9 = rad[10];
            ViewBag.filtros10 = rad[11];

            ViewBag.filtros11 = rad[12];
            ViewBag.filtros12 = rad[13];
            ViewBag.filtros13 = rad[14];


            ViewBag.filtros14 = rad[3];
            ViewBag.filtros15 = rad[4];



            Articles = db.Database.SqlQuery<vista_buscador_curriculum>
                ("exec sp_buscador_curriculums_avanzado  @hasta = {0}, @sexo = {1} , @id_comuna = {2}  , @id_area_experiencia_laboral = {3} ,"
                + " @id_cargo_experiencia_laboral = {4}  , @sueldo_hasta = {5} , @id_estudio = {6} , @id_institucion = {7} , @id_estado_estudio = {8} ," +
              " @id_tipo_estudio = {9} , @id_idioma = {10} , @id_nivel_idioma = {11} , @ultimo_update = {12} ",
                rad[0], rad[1], rad[4], rad[5], rad[6], rad[7], rad[8], rad[9], rad[10], rad[11], rad[12], rad[13], rad[14]).ToList();





            int pageNumber = (page ?? 1);
            ViewBag.control = "filtrar_full";
            carga_combos();
            ViewBag.id_region = new SelectList(db.regiones, "id_region", "nombre_region", ViewBag.filtros15);


            ViewBag.id_comuna = new SelectList(db.comunas, "id_comuna", "nombre_comuna", rad[4]);

            ViewBag.id_ciudad = new SelectList(db.ciudades, "id_ciudad", "nombre_ciudad", ViewBag.filtros14);



            return View("Index", Articles.ToPagedList(pageNumber, itemsPerPage));

        }
        public ViewResult filtrar_basico(int? page, string palabra_clave)
        {
            List<vista_buscador_curriculum> Articles;
            modelo_postulaciones_vista m_p_v = new modelo_postulaciones_vista();
            super_modelo_curriculum s_m_c = new super_modelo_curriculum();
            if (palabra_clave == null)
            {
                palabra_clave = "";
            }



            Articles = db.Database.SqlQuery<vista_buscador_curriculum>(" exec sp_buscador_curriculums_basico @palabra_clave = {0} ", palabra_clave).ToList();

            carga_combos();
            int pageNumber = (page ?? 1);
            ViewBag.palabra_clave = palabra_clave;
            ViewBag.control = "filtrar_basico";
            return View("Index", Articles.ToPagedList(pageNumber, itemsPerPage));

        }

        public void carga_combos()
        {
            ViewBag.id_region = new SelectList(db.regiones, "id_region", "nombre_region");
            ViewBag.id_comuna = new SelectList(db.comunas, "id_comuna", "nombre_comuna");
            ViewBag.id_ciudad = new SelectList(db.ciudades, "id_ciudad", "nombre_ciudad");
            ViewBag.id_idioma = new SelectList(db.idiomas, "id_idioma", "nombre_idioma");
            ViewBag.id_estudio = new SelectList(db.estudios, "id_estudio", "nombre_estudio");
            ViewBag.id_tipo_persona = new SelectList(db.tipo_personas, "id_tipo_persona", "nombre_tipo_persona");
            ViewBag.id_institucion = new SelectList(db.instituciones, "id_institucion", "nombre_institucion");
            ViewBag.id_tipo_estudio = new SelectList(db.tipo_estudios, "id_tipo_estudio", "nombre_tipo_estudio");
            ViewBag.id_estado_estudio = new SelectList(db.estado_estudios, "id_estado_estudio", "nombre_estado_estudio");
            ViewBag.id_nivel_idioma = new SelectList(db.nivel_idiomas, "id_nivel_idioma", "nombre_nivel_idioma");
            ViewBag.id_area_experiencia_laboral = new SelectList(db.area_experiencias_laborales, "id_area_experiencia_laboral", "nombre_area_experiencia_laboral");
            ViewBag.id_cargo_experiencia_laboral = new SelectList(db.cargo_experiencias_laborales, "id_cargo_experiencia_laboral", "nombre_cargo_experiencia_laboral");
        }
        [CustomAuthorize]
        public PartialViewResult detalle_curriculum(int id)
        {
            try
            {

                filtro_total();
                super_modelo_curriculum s_m_c2 = new super_modelo_curriculum();

                s_m_c2.persona_curriculum_vista = db.Database.SqlQuery<persona_curriculum_vista>("Exec sp_obtener_datos_curriculums  @id_persona = {0} ", id).Single();




                s_m_c2.capacitaciones_curiculums_vista = db.Database.SqlQuery<capacitaciones_curriculums_vista>("Exec sp_obtener_capacitaciones_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.habilidades_curriculums_vista = db.Database.SqlQuery<habilidades_curriculums_vista>("Exec sp_obtener_habilidades_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.curriculums = db.curriculums.Find(s_m_c2.persona_curriculum_vista.id_curriculum);
                List<experiencias_laborales_curriculums_vista> k = db.Database.SqlQuery<experiencias_laborales_curriculums_vista>("Exec sp_obtener_experiencia_laboral_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();

                foreach (var l in k)
                {
                    l.referencia_laboral = db.Database.SqlQuery<referencia_laboral>("select * from referencia_laboral where id_referencia_laboral = {0}", l.id_referencia_laboral).Single();
                }

                s_m_c2.experiencias_laborales_curriculums_vista = k;
                s_m_c2.estudios_curriculums_vista = db.Database.SqlQuery<estudios_curriculums_vista>("Exec sp_obtener_estudios_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();

                s_m_c2.idiomas_curriculums_vista = db.Database.SqlQuery<idiomas_curriculums_vista>("Exec sp_obtener_idiomas_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.softwares_curriculums_vista = db.Database.SqlQuery<softwares_curriculums_vista>("Exec sp_obtener_softwares_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.documentos_curriculums = db.Database.SqlQuery<documentos_curriculums>("exec sp_obtener_documentos_curriculums @id_curriculum = {0}", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                var ka = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));


                ViewBag.id_oferta = new SelectList(db.Database.SqlQuery<modelo_ofertas_grupo_usuario>("exec sp_obtener_listado_ofertas_invitacion @id_grupo_usuario = {0}", ka.id_grupo_usuario).ToList(), "id_oferta", "nombre_oferta");

                ViewBag.cantidad_postulaciones = db.Database.SqlQuery<Int32>("sp_obtener_cantidad_historial_postulaciones @id_persona = {0}", id).Single();
                carga_historial(id);
                return PartialView("_control_curriculums_detalle_persona", s_m_c2);
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }




        }
        public PartialViewResult GetSamples(int id)
        {
            try
            {
                super_modelo_curriculum s_m_c2 = new super_modelo_curriculum();
                s_m_c2.persona_curriculum_vista = db.Database.SqlQuery<persona_curriculum_vista>("Exec sp_obtener_datos_curriculums @id_persona = {0} ", id).Single();
                s_m_c2.capacitaciones_curiculums_vista = db.Database.SqlQuery<capacitaciones_curriculums_vista>("Exec sp_obtener_capacitaciones_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.habilidades_curriculums_vista = db.Database.SqlQuery<habilidades_curriculums_vista>("Exec sp_obtener_habilidades_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.curriculums = db.curriculums.Find(s_m_c2.persona_curriculum_vista.id_curriculum);
                List<experiencias_laborales_curriculums_vista> k = db.Database.SqlQuery<experiencias_laborales_curriculums_vista>("Exec sp_obtener_experiencia_laboral_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();

                foreach (var l in k)
                {
                    l.referencia_laboral = db.Database.SqlQuery<referencia_laboral>("select * from referencia_laboral where id_referencia_laboral = {0}", l.id_referencia_laboral).Single();
                }

                s_m_c2.experiencias_laborales_curriculums_vista = k;



                s_m_c2.estudios_curriculums_vista = db.Database.SqlQuery<estudios_curriculums_vista>("Exec sp_obtener_estudios_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.idiomas_curriculums_vista = db.Database.SqlQuery<idiomas_curriculums_vista>("Exec sp_obtener_idiomas_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.softwares_curriculums_vista = db.Database.SqlQuery<softwares_curriculums_vista>("Exec sp_obtener_softwares_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.documentos_curriculums = db.Database.SqlQuery<documentos_curriculums>("exec sp_obtener_documentos_curriculums @id_curriculum = {0}", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                return PartialView("_formato_curriculum_pdf", s_m_c2);
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }
        }

        public PartialViewResult historial_pdf(int id)
        {
            super_modelo_curriculum s_m_c2 = new super_modelo_curriculum();

            s_m_c2.persona_curriculum_vista = db.Database.SqlQuery<persona_curriculum_vista>("Exec sp_obtener_datos_curriculums  @id_persona = {0} ", id).Single();
            ViewBag.cantidad_postulaciones = db.Database.SqlQuery<Int32>("sp_obtener_cantidad_historial_postulaciones @id_persona = {0}", id).Single();
            carga_historial(id);
            return PartialView("_historial_postulaciones_pdf", s_m_c2);
        }

        public ActionResult Descarga_pdf(int id, string nombre_persona)
        {



            return new Rotativa.ActionAsPdf("GetSamples", new { id = id }) { FileName = nombre_persona + ".pdf" };


        }
        public ActionResult Descarga_historial_pdf(int? id, string nombre_persona)
        {



            return new Rotativa.ActionAsPdf("historial_pdf", new { id = id }) { FileName = nombre_persona + ".pdf" };


        }
        public PartialViewResult cronologia_full_pdf(int id)
        {

            return PartialView("_cronologia_postulacion_persona_full_pdf");
        }
        public ActionResult Descarga_cronologia_full_pdf(int? id, string nombre_persona)
        {



            return new Rotativa.ActionAsPdf("cronologia_full_pdf", new { id = id }) { FileName = nombre_persona + ".pdf" };


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Correo_pdf(int? id, string correo)
        {

            try
            {
                validar_email v = new validar_email();
                Boolean k = v.email_bien_escrito(correo);
                if (k == false || correo == null || correo == "")
                {
                    return Json(new { success = false, responseText = "Ingrese un Correo electronico valido" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    mail m = new mail();
                    var lol2 = new ActionAsPdf("GetSamples", new { id = id }) { FileName = "comparte.pdf" };

                    byte[] Curriculum_vitae = lol2.BuildPdf(ControllerContext);

                    //string titulo = "Portal de empleos (curriculum de interes)";
                    //string mensaje = "Estimado , hago envio de copia de este curriculum el cual puede ser de su interes.";
                    //Boolean ka = await m.correos(correo, Curriculum_vitae, titulo, mensaje);
                    m.enviar_correo(Curriculum_vitae, correo, 1);

                    return Json(new { success = true, responseText = "El Correo ha sido enviado" }, JsonRequestBehavior.AllowGet);


                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Sucedio un imprevisto que no pudo realizar la accion " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Invita_oferta_laboral(int? id, int id_oferta, string correo)
        {
            try
            {
                if (correo == null || correo == "")
                {
                    return Json(new { success = false, responseText = "Ingrese un Correo electronico valido" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    mail m = new mail();
                    var lol2 = new ActionAsPdf("GetSamples", new { id = id }) { FileName = "invita_laboral.pdf" };

                    byte[] Curriculum_vitae = lol2.BuildPdf(ControllerContext);

                    string titulo = "Portal empleos Maipu(Invitacion Empleo)";
                    string mensaje = " estimado/a ,  te invito a postular a http://portalempleo.azurewebsites.net/Ofertas_Control/vista_detalle_oferta/" + id_oferta + " , ya que esta oferta puede ser de tu interes";
                    // Boolean ka = await m.correos(correo, Curriculum_vitae, titulo, mensaje);
                    m.enviar_correo(Curriculum_vitae, correo, 3);

                    return Json(new { success = true, responseText = "Correo Enviado Con Exito" }, JsonRequestBehavior.AllowGet);



                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Sucedio un imprevisto que no pudo realizar la accion " }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCountries()
        {
            return Json(db.regiones.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatesByCountryId(string countryId)
        {
            try
            {
                int Id = Convert.ToInt32(countryId);


                var states = from a in db.ciudades where a.id_region == Id select a;
                List<ciudades> results = db.Database.SqlQuery<ciudades>("Exec sp_obtener_ciudades @id_region = {0}  ", Id).ToList();
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetStatesByCountryId2(string countryId)
        {
            try
            {
                int Id = Convert.ToInt32(countryId);


                //var states = from a in db.ciudades where a.id_region == Id select a;
                List<comunas> results2 = db.Database.SqlQuery<comunas>("Exec sp_obtener_comunas @id_ciudad = {0}  ", Id).ToList();
                return Json(results2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public void carga_historial(int id)
        {

            List<vista_historial_postulaciones> datos = db.Database.SqlQuery<vista_historial_postulaciones>
             ("exec sp_listado_historial_postulaciones @id_persona = {0}", id).ToList();

            ViewBag.nombre_oferta = new string[(datos.Count())];
            ViewBag.fecha_postulacion = new string[(datos.Count())];
            ViewBag.estado_final_postulacion = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {

                ViewBag.nombre_oferta[i] = k.nombre_oferta.ToString();

                ViewBag.fecha_postulacion[i] = k.fecha_postulacion.ToString();

                ViewBag.estado_final_postulacion[i] = k.nombre_estado_postulacion;
                i++;
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