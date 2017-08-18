using control_portal_empleos_v2.Models;
using control_portal_empleos_v2.seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.Threading.Tasks;

namespace control_portal_empleos_v2.Controllers
{
    public class control_postulacionController : Controller
    {

        PersonaDBContext db = new PersonaDBContext();


        public int itemsPerPage = 6;


        [CustomAuthorize]
        public ActionResult Index(int? page, int id)
        {
            try
            {

                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }



                var oferta = db.ofertas.Find(id);
                ViewBag.id = oferta.id_oferta;
                ViewBag.nombre_oferta = oferta.nombre_oferta;
                List<vista_buscador_curriculum> Articles;



                Articles = db.Database.SqlQuery<vista_buscador_curriculum>("exec sp_vista_buscador_postulaciones @id_oferta = {0}", id).ToList();
                int pageNumber = (page ?? 1);

                carga_estados(id);
                carga_combos();
                return View(Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }
        }

        [CustomAuthorize]
        public ActionResult filtrar_full(int? page, string hasta, string sexo, string id_comuna, string id_area_experiencia_laboral,
      string id_cargo_experiencia_laboral, string sueldo_hasta, string id_estudio, string id_institucion, string id_estado_estudio, string id_tipo_estudio,
      string id_idioma, string id_nivel_idioma, string ultimo_update, string id_ciudad, string id_region, int id_oferta, FormCollection form)
        {

            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                ViewBag.id = id_oferta;
                List<vista_buscador_curriculum> Articles;

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
                    rad = new string[16];

                    rad[1] = hasta;
                    rad[2] = sexo;
                    rad[3] = id_ciudad;
                    rad[4] = id_region;
                    rad[5] = id_comuna;

                    rad[6] = id_area_experiencia_laboral;
                    rad[7] = id_cargo_experiencia_laboral;

                    rad[8] = sueldo_hasta;
                    rad[9] = id_estudio;
                    rad[10] = id_institucion;
                    rad[11] = id_estado_estudio;
                    rad[12] = id_tipo_estudio;
                    rad[13] = id_idioma;
                    rad[14] = id_nivel_idioma;
                    rad[15] = ultimo_update;

                }


                if (page != 1)
                { }
                ViewBag.filtros1 = rad[1];
                ViewBag.filtros2 = rad[2];
                ViewBag.filtros3 = rad[3];


                ViewBag.filtros4 = rad[6];

                ViewBag.filtros5 = rad[7];
                ViewBag.filtros6 = rad[8];
                ViewBag.filtros7 = rad[9];

                ViewBag.filtros8 = rad[10];
                ViewBag.filtros9 = rad[11];
                ViewBag.filtros10 = rad[12];

                ViewBag.filtros11 = rad[13];
                ViewBag.filtros12 = rad[14];
                ViewBag.filtros13 = rad[15];


                ViewBag.filtros14 = rad[4];
                ViewBag.filtros15 = rad[5];



                Articles = db.Database.SqlQuery<vista_buscador_curriculum>
                    ("exec sp_buscador_postulaciones_avanzado @id_oferta = {0} , @hasta = {1}, @sexo = {2} , @id_comuna = {3}  , @id_area_experiencia_laboral = {4} ,"
                    + " @id_cargo_experiencia_laboral = {5}  , @sueldo_hasta = {6} , @id_estudio = {7} , @id_institucion = {8} , @id_estado_estudio = {9} ," +
                  " @id_tipo_estudio = {10} , @id_idioma = {11} , @id_nivel_idioma = {12} , @ultimo_update = {13} ",
                   id_oferta, rad[1], rad[2], rad[5], rad[6], rad[7], rad[8], rad[9], rad[10], rad[11], rad[12], rad[13], rad[14], rad[15]).ToList();





                int pageNumber = (page ?? 1);
                ViewBag.control = "filtrar_full";
                carga_estados(id_oferta);
                carga_combos();
                ViewBag.id_region = new SelectList(db.regiones, "id_region", "nombre_region", ViewBag.filtros15);


                ViewBag.id_comuna = new SelectList(db.comunas, "id_comuna", "nombre_comuna", rad[4]);

                ViewBag.id_ciudad = new SelectList(db.ciudades, "id_ciudad", "nombre_ciudad", ViewBag.filtros14);



                return View("Index", Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }
        }
        [CustomAuthorize]
        public ActionResult filtrar_basico(int? page, string palabra_clave, int id_oferta)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                ViewBag.id = id_oferta;
                List<vista_buscador_curriculum> Articles;

                if (palabra_clave == null)
                {
                    palabra_clave = "";
                }



                Articles = db.Database.SqlQuery<vista_buscador_curriculum>(" exec sp_buscador_postulaciones_basico @palabra_clave = {0} , @id_oferta= {1} ", palabra_clave, id_oferta).ToList();
                carga_estados(id_oferta);
                carga_combos();
                int pageNumber = (page ?? 1);
                ViewBag.palabra_clave = palabra_clave;
                ViewBag.control = "filtrar_basico";
                return View("Index", Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }
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

        public void carga_estados(int id)
        {

            List<vista_pie_detalle_oferta> datos = db.Database.SqlQuery<vista_pie_detalle_oferta>
             ("select id_estado_postulacion , nombre_estado_postulacion from estado_postulaciones").ToList();

            ViewBag.a1 = new string[(datos.Count())];
            ViewBag.a2 = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {

                ViewBag.a1[i] = k.id_estado_postulacion.ToString();

                ViewBag.a2[i] = k.nombre_estado_postulacion;
                i++;
            }

        }

        public ActionResult detalle_postulante(int id, int id_oferta)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
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


                //carga_estados_postulante();
                carga_estado_actual_postulante(s_m_c2.persona_curriculum_vista.id_persona, id_oferta);

                carga_cronologia_persona_oferta(s_m_c2.persona_curriculum_vista.id_persona, id_oferta);
                ViewBag.id_oferta = id_oferta;
                ViewBag.id_persona = s_m_c2.persona_curriculum_vista.id_persona;
                int activado = db.Database.SqlQuery<int>("select id_cuestionario from ofertas where id_oferta = {0}", id_oferta).Single();
                if (activado != 0)
                {
                    ViewBag.activado = activado;
                    carga_cuestionario(s_m_c2.persona_curriculum_vista.id_persona, id_oferta);
                }
                else
                {
                    ViewBag.activado = 0;
                }

                return PartialView("_control_postulacion_detalle_persona", s_m_c2);
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }
        }
        public void carga_cronologia_persona_oferta(int id, int id_oferta)
        {

            List<vista_listado_cronologia_persona> datos = db.Database.SqlQuery<vista_listado_cronologia_persona>
             ("exec sp_obtener_cronologia_persona_por_oferta @id_persona = {0} , @id_oferta = {1}", id, id_oferta).ToList();

            ViewBag.nombre_accion = new string[(datos.Count())];
            ViewBag.fecha_cronologia = new string[(datos.Count())];
            ViewBag.observacion_cronologia = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {

                ViewBag.nombre_accion[i] = k.nombre_accion_cronologica.ToString();

                ViewBag.fecha_cronologia[i] = k.fecha_registro_accion.ToString();

                ViewBag.observacion_cronologia[i] = k.observacion_cronologica.ToString();
                i++;
            }

        }

        public void carga_cuestionario(int id, int id_oferta)
        {


            List<cuestionario_respuestas_vista> datos = db.Database.SqlQuery<cuestionario_respuestas_vista>
            ("exec sp_obtener_nombre_pregunta_1 @id_oferta = {0} , @id_persona = {1}", id_oferta, id).ToList();

            ViewBag.id_pregunta1 = new string[(datos.Count())];
            ViewBag.nombre_pregunta_1 = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {

                ViewBag.id_pregunta1[i] = k.id.ToString();

                ViewBag.nombre_pregunta_1[i] = k.nombre_pregunta_1;
                i++;
            }

            List<cuestionario_respuestas_vista> datos1 = db.Database.SqlQuery<cuestionario_respuestas_vista>
      ("exec sp_obtener_nombre_respuesta_1 @id_oferta = {0} , @id_persona = {1}", id_oferta, id).ToList();

            ViewBag.id_respuesta1 = new string[(datos1.Count())];
            ViewBag.nombre_respuesta_1 = new string[(datos1.Count())];
            i = 0;
            foreach (var k1 in datos1)
            {

                ViewBag.id_respuesta1[i] = k1.id.ToString();

                ViewBag.nombre_respuesta_1[i] = k1.nombre_pregunta_1;
                i++;
            }
            List<cuestionario_respuestas_vista> datos2 = db.Database.SqlQuery<cuestionario_respuestas_vista>
      ("exec sp_obtener_nombre_pregunta_2 @id_oferta = {0} , @id_persona = {1}", id_oferta, id).ToList();

            ViewBag.id_pregunta2 = new string[(datos2.Count())];
            ViewBag.nombre_pregunta_2 = new string[(datos2.Count())];
            i = 0;
            foreach (var k2 in datos2)
            {

                ViewBag.id_pregunta2[i] = k2.id.ToString();

                ViewBag.nombre_pregunta_2[i] = k2.nombre_pregunta_1;
                i++;
            }
            List<cuestionario_respuestas_vista> datos3 = db.Database.SqlQuery<cuestionario_respuestas_vista>
      ("exec sp_obtener_nombre_respuesta_2 @id_oferta = {0} , @id_persona = {1}", id_oferta, id).ToList();

            ViewBag.id_respuesta2 = new string[(datos3.Count())];
            ViewBag.nombre_respuesta_2 = new string[(datos3.Count())];
            i = 0;
            foreach (var k3 in datos3)
            {

                ViewBag.id_respuesta2[i] = k3.id.ToString();

                ViewBag.nombre_respuesta_2[i] = k3.nombre_pregunta_1;
                i++;
            }

        }
        public void carga_estados_postulante()
        {

            List<estado_postulaciones> datos = db.estado_postulaciones.ToList();

            ViewBag.id_estado_postulacion = new string[(datos.Count())];
            ViewBag.nombre_estado_postulacion = new string[(datos.Count())];
            ViewBag.alerta_postulacion = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {

                ViewBag.id_estado_postulacion[i] = k.id_estado_postulacion.ToString();

                ViewBag.nombre_estado_postulacion[i] = k.nombre_estado_postulacion;
                //ViewBag.alerta_postulacion[i] = k.alerta_postulacion.ToString();
                i++;
            }
        }
        public void carga_estado_actual_postulante(int id, int id_oferta)
        {

            estado_postulaciones datos = db.Database.SqlQuery<estado_postulaciones>
     ("select estado_postulaciones.id_estado_postulacion ,nombre_estado_postulacion  from estado_postulaciones inner join postulaciones on postulaciones.id_estado_postulacion = estado_postulaciones.id_estado_postulacion where postulaciones.id_persona = {0} and postulaciones.id_oferta = {1}", id, id_oferta).Single();


            ViewBag.id_estado_postulacion_actual = datos.id_estado_postulacion;
            ViewBag.nombre_estado_actual_postulante = datos.nombre_estado_postulacion;

            //int i = 0;
            //foreach (var k in datos)
            //{

            //  ViewBag.id_estado_postulacion_actual[i] = k.id_estado_postulacion.ToString();

            //ViewBag.nombre_estado_actual_postulante[i] = k.nombre_estado_postulacion;

            //i++;
            //}
        }

        public async Task<ActionResult> actualiza_estado_postulante(string id_estado_postulacion, int id_oferta, int id_persona, string observacion_cronologica)
        {
            try
            {
                ViewBag.id_persona = id_persona;
                ViewBag.id_oferta = id_oferta;
                if (id_estado_postulacion == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.Database.ExecuteSqlCommand("exec sp_actualiza_estado_postulacion_persona @id_estado_postulacion = {0} , @id_oferta = {1} , @id_persona = {2}", Convert.ToInt32(id_estado_postulacion), id_oferta, id_persona);
                db.Database.ExecuteSqlCommand("exec sp_inserta_cronologia_persona  @id_accion_cronologica = {0} , @id_persona = {1} , @id_usuario_reportador = {2} , @id_oferta_reportador = {3} , @observacion_cronologica = {4}", 0, id_persona, Convert.ToInt32(Session["usuario_id"]), id_oferta, observacion_cronologica);
                var a = db.estado_postulaciones.Find(Convert.ToInt32(id_estado_postulacion));


                personas k = db.personas.Find(id_persona);
                mail m = new mail();
                titulo = "Estado de su postulacion ";
                correo = k.correo_electronico_persona;




                if (id_estado_postulacion == "2")
                {
                    mensaje = "Le agradecemos haber participado en el proceso de seleccion pero tenemos un candidato que se ajusta mas al perfil requerido. en cualquier caso , nos pondremos en contacto con usted si surge una vacante relacionada con su perfil profesional.";


                    m.enviar_correo(null, correo, 4);
                }

                if (id_estado_postulacion == "3")
                {

                    mensaje = " ha sido seleccionado como un candidato con el perfil necesario para el puesto de trabajo , en lo proximo se contactaran con usted.";

                    m.enviar_correo(null, correo, 5);
                }
                carga_estado_actual_postulante(id_persona, id_oferta);
                return PartialView("_status_final_postulacion_persona");
                //return Json(new { success = true, responseText = "Se ha actualizado correctamente el estado de la persona", id = id_estado_postulacion , nombre_estado = a.nombre_estado_postulacion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //   return Json(new { success = false, responseText = ex }, JsonRequestBehavior.AllowGet);
            }


        }

        [CustomAuthorize]
        public ActionResult filtrar_estado(int? page, int id_estado_oferta_filtro, int id_oferta)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                ViewBag.id = id_oferta;
                List<vista_buscador_curriculum> Articles;
                carga_estados(id_oferta);
                Articles = db.Database.SqlQuery<vista_buscador_curriculum>("exec sp_buscador_postulacion_estado @id_estado_postulacion = {0}, @id_oferta = {1} ", id_estado_oferta_filtro, id_oferta).ToList();


                carga_combos();
                int pageNumber = (page ?? 1);
                ViewBag.id_estado_oferta_filtro = id_estado_oferta_filtro;
                ViewBag.control = "filtrar_estado";
                return View("Index", Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                return View("Error");
            }





        }
        public PartialViewResult cronologia_full_pdf(int id, int id_oferta)
        {
            carga_cronologia_persona_oferta(id, id_oferta);
            return PartialView("_cronologia_postulacion_persona");
        }
        public ActionResult Descarga_cronologia_pdf(int? id, int id_oferta, string nombre_persona)
        {



            return new Rotativa.ActionAsPdf("cronologia_full_pdf", new { id = id, id_oferta = id_oferta }) { FileName = nombre_persona + ".pdf" };


        }
        public ActionResult Descarga_pdf(int? id, int id_oferta, string nombre_persona)
        {



            return new Rotativa.ActionAsPdf("Descarga_full_pdf", new { id = id, id_oferta = id_oferta }) { FileName = nombre_persona + ".pdf" };


        }
        public PartialViewResult Descarga_full_pdf(int? id, int id_oferta, string nombre_persona)
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



                int activado = db.Database.SqlQuery<int>("select id_cuestionario from ofertas where id_oferta = {0}", id_oferta).Single();
                if (activado != 0)
                {
                    ViewBag.activado = activado;
                    carga_cuestionario(s_m_c2.persona_curriculum_vista.id_persona, id_oferta);
                }
                else
                {
                    ViewBag.activado = 0;
                }

                return PartialView("_formato_postulacion_pdf", s_m_c2);
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }


        }
        string correo;
        string titulo;
        string mensaje;
        int estado;
        public async Task<ActionResult> activa_revision_postulante(int id_oferta, int id_persona, Boolean si_correo)

        {
            try
            {
                ViewBag.id_persona = id_persona;
                ViewBag.id_oferta = id_oferta;
                if (si_correo == true)
                {

                    personas k = db.personas.Find(id_persona);
                    mail m = new mail();
                    titulo = "Estado de su postulacion ";
                    correo = k.correo_electronico_persona;
                    mensaje = "estimado en estos momentos se ha comenzado la revision de sus datos para su evaluacion de aptitud para la oferta postulada.";
                    // Boolean a = await m.Correo_estados_postulacion(correo, titulo, mensaje);
                    m.enviar_correo(null, correo, 6);

                }
                return PartialView("_status_postulacion_persona");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //return  Json(new { success = true, responseText  }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> reversa_estado_postulante(int id_oferta, int id_persona, Boolean si_correo, string motivo_cambio)

        {
            try
            {
                ViewBag.id_persona = id_persona;
                ViewBag.id_oferta = id_oferta;
                if (si_correo == true)
                {

                    personas k = db.personas.Find(-1);
                    mail m = new mail();
                    titulo = "Estado de su postulacion";
                    correo = k.correo_electronico_persona;
                    mensaje = "Se ha revocado la decision anterior sobre el estado de su postulacion , le informamos que nuevmente ha ingresado al proceso de evaluacion." + motivo_cambio;
                    //Boolean a = await m.Correo_estados_postulacion(correo, titulo, mensaje);
                    m.enviar_correo(null, correo, 7);

                }

                return PartialView("_status_postulacion_persona");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //return  Json(new { success = true, responseText  }, JsonRequestBehavior.AllowGet);
        }


    }
}