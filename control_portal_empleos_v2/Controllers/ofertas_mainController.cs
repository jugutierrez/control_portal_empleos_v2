using control_portal_empleos_v2.Models;
using control_portal_empleos_v2.seguridad;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace control_portal_empleo.Controllers
{
    public class ofertas_mainController : Controller
    {
        //
        // GET: /ofertas_main/
        PersonaDBContext db = new PersonaDBContext();
        public int id_oferta;
        public int itemsPerPage = 10;

        [CustomAuthorize]
        public ActionResult Index()
        {
            try
            {
               


                return View("inicio_login_ofertas/index");
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }
        }
        [CustomAuthorize]
        public ActionResult oferta_main_crear()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {

                    return RedirectToAction("LogOff", "login");
                }
                ViewBag.id_direccion = new SelectList(db.Database.SqlQuery<direcciones>("exec sp_listado_direcciones_por_grupo_usuarios @id_usuario = {0}", Convert.ToInt32(Session["usuario_id"])).ToList(), "id_direccion", "nombre_direccion");
                ViewBag.id_jornada_oferta = new SelectList(db.jornada_ofertas, "id_jornada_oferta", "nombre_jornada_oferta");
                ViewBag.id_contrato_oferta = new SelectList(db.contrato_ofertas, "id_contrato_oferta", "nombre_contrato_oferta");
                ViewBag.id_tipo_oferta = new SelectList(db.tipo_ofertas, "id_tipo_oferta", "nombre_tipo_oferta");
                ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "nombre_departamento");
                ViewBag.id_area = new SelectList(db.areas, "id_area", "nombre_area");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }
        }
        [CustomAuthorize]
        public ActionResult mis_ofertas(int? page)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {

                    return RedirectToAction("LogOff", "login");
                }
                filtro_total();

                List<vista_buscador_oferta> Articles;
                var l = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));


                Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_vista_buscador_ofertas @id_grupo_usuario = {0}", l.id_grupo_usuario).ToList();

                cargarcombos();

                carga_estados();


                int pageNumber = (page ?? 1);
                ViewBag.control = "mis_ofertas";

                return View("oferta_main_mis_ofertas", Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }
        }
        public void carga_estados()
        {
            List<vista_cantidad_estados_oferta> datos;


            datos = db.Database.SqlQuery<vista_cantidad_estados_oferta>
             (" exec sp_cantidad_estados_ofertas @id_usuario = {0}", Convert.ToInt32(Session["usuario_id"])).ToList();



            ViewBag.a1 = new string[(datos.Count())];
            ViewBag.a2 = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {

                ViewBag.a1[i] = k.cantidad_estado.ToString();

                ViewBag.a2[i] = k.nombre_estado_oferta;
                i++;
            }
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult create_oferta([Bind(Include = "id_usuario , id_area , id_jornada_oferta , id_contrato_oferta , id_tipo_oferta , nombre_oferta , descripcion_oferta , monto_oferta")] usuarios_ofertas_vista usuarios_ofertas_vista)
        {

            try
            {
                if (Session.Contents.Count < 1)
                {

                    return RedirectToAction("LogOff", "login");
                }
                usuarios_ofertas_vista.id_usuario = Convert.ToInt32(Session["usuario_id"]);
                if (ModelState.IsValid)
                {


                    db.Database.ExecuteSqlCommand("Exec sp_inserta_oferta_persona @nombre_oferta = {0} , @descripcion_oferta = {1} , @id_jornada_oferta = {2}, @id_tipo_oferta = {3}, @id_contrato_oferta = {4} , @id_usuario = {5} , @id_area = {6} , @monto_oferta = {7}", usuarios_ofertas_vista.nombre_oferta, usuarios_ofertas_vista.descripcion_oferta, usuarios_ofertas_vista.id_jornada_oferta, usuarios_ofertas_vista.id_tipo_oferta, usuarios_ofertas_vista.id_contrato_oferta, usuarios_ofertas_vista.id_usuario, usuarios_ofertas_vista.id_area, usuarios_ofertas_vista.monto_oferta);

                    int? k = db.ofertas.Max(u => (int?)u.id_oferta);

                    return Json(new { success = true, responseText = k }, JsonRequestBehavior.AllowGet);
                }



                return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }

        }

        [CustomAuthorize]
        public ActionResult Detalle(int? id)
        {

            try
            {
                filtro_total();


                if (id == null)
                {
                    ViewBag.codigo = 2;
                    return View("Error");
                }

                var l = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));
                int k = db.Database.SqlQuery<int>("exec sp_verificar_oferta_usuario_grupo @id_grupo_usuario= {0}", l.id_grupo_usuario).Single();

                if (k < 1)
                {
                    ViewBag.codigo = 2;
                    return View("Error");
                }
                else
                {
                    ofertas ofertas = db.ofertas.Find(id);

                    usuarios_ofertas usuario_ofertas = db.usuarios_ofertas.First(c => c.id_oferta == id);
                    ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "nombre_departamento", usuario_ofertas.areas.id_departamento);


                    ViewBag.id_area = new SelectList(db.areas, "id_area", "nombre_area", usuario_ofertas.id_area);
                    ViewBag.id_direccion = new SelectList(db.direcciones, "id_direccion", "nombre_direccion", usuario_ofertas.areas.departamentos.id_direccion);
                    ViewBag.id_jornada_oferta = new SelectList(db.jornada_ofertas, "id_jornada_oferta", "nombre_jornada_oferta", ofertas.id_jornada_oferta);
                    ViewBag.id_contrato_oferta = new SelectList(db.contrato_ofertas, "id_contrato_oferta", "nombre_contrato_oferta", ofertas.id_contrato_oferta);
                    ViewBag.id_tipo_oferta = new SelectList(db.tipo_ofertas, "id_tipo_oferta", "nombre_tipo_oferta", ofertas.id_tipo_oferta);
                    ViewBag.oferta_inclusiva = ofertas.oferta_inclusiva;
                    carga_estado_ofertas(id);

                    id_oferta = Convert.ToInt32(id);
                    if (ofertas == null)
                    {
                        return HttpNotFound();
                    }

                    return View("oferta_main_detalle_oferta_v2", ofertas);
                }

            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return View("Error");
            }



        }

        public JsonResult GetCountries()
        {
            return Json(db.direcciones.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatesByCountryId(string countryId)
        {
            try
            {
                int Id = Convert.ToInt32(countryId);


                // var states = from a in db.departamentos where a.id_direccion == Id select a;
                List<departamentos> results = db.Database.SqlQuery<departamentos>("Exec sp_obtener_departamentos @id_direccion = {0}  ", Id).ToList();
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
                List<areas> results2 = db.Database.SqlQuery<areas>("Exec sp_obtener_areas @id_departamento = {0}  ", Id).ToList();
                return Json(results2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult vista_especificaciones_habilidades(int? id)
        {

            try
            {

                MainPageViewModel e_o = new MainPageViewModel();
                filtro_total();
                var x = db.Database.SqlQuery<int>("select id_especificacion_oferta from ofertas where id_oferta  = {0}", id).Single();
                e_o.id_especificacion = x;
                List<especificaciones_ofertas_habilidades_vista> k = db.Database.SqlQuery<especificaciones_ofertas_habilidades_vista>("Exec sp_obtener_especificaciones_ofertas_habilidades @id_especificacion_oferta ={0}", x).ToList();
                List<especificaciones_ofertas_idiomas_vista> k1 = db.Database.SqlQuery<especificaciones_ofertas_idiomas_vista>("Exec sp_obtener_especificaciones_ofertas_idiomas @id_especificacion_oferta ={0}", x).ToList();
                List<especificaciones_ofertas_profesiones_vista> k2 = db.Database.SqlQuery<especificaciones_ofertas_profesiones_vista>("Exec sp_obtener_especificaciones_ofertas_profesiones @id_especificacion_oferta ={0}", x).ToList();
                List<especificaciones_ofertas_softwares_vista> k3 = db.Database.SqlQuery<especificaciones_ofertas_softwares_vista>("Exec sp_obtener_especificaciones_ofertas_softwares @id_especificacion_oferta ={0}", x).ToList();
                e_o.especificaciones_ofertas_h = k;
                e_o.especificaciones_ofertas_i = k1;
                e_o.especificaciones_ofertas_p = k2;
                e_o.especificaciones_ofertas_s = k3;

                return PartialView("_oferta_main_detalle_especificaciones", e_o);
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return PartialView("Error");
            }

        }
        public PartialViewResult vista_cuestionarios(int? id)
        {
            try
            {
                filtro_total();
                modelo_vista_cuestionarios m_c = new modelo_vista_cuestionarios();
                var x = db.Database.SqlQuery<int>("exec sp_obtener_cuestionarios @id_oferta  = {0}", id).Single();
                m_c.id_cuestionario = x;
                List<cuestionarios_preguntas_vista> k = db.Database.SqlQuery<cuestionarios_preguntas_vista>("Exec sp_obtener_preguntas_cuestionarios @id_cuestionario ={0}", id).ToList();
                List<preguntas_respuestas> k1 = db.preguntas_respuestas.ToList();
                m_c.cuestionarios_pregunta = k;
                m_c.preguntas_respuesta = k1;
                return PartialView("_oferta_main_detalle_cuestionarios", m_c);
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return PartialView("Error");
            }
        }

        public PartialViewResult seleccion_agrega_habilidad(int? id)
        {
            try
            {

                ViewBag.id_habilidad = new SelectList(db.habilidades, "id_habilidad", "nombre_habilidad");
                ViewBag.id_grado_habilidad = new SelectList(db.grado_habilidades, "id_grado_habilidad", "nombre_grado_habilidad");
                ViewBag.id_nivel_importancia = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
                ViewBag.id = id;
                return PartialView("_ofertas_main_especificaciones_habilidades_agrega");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult seleccion_agrega_idioma(int? id)
        {
            try
            {

                ViewBag.id_idioma = new SelectList(db.idiomas, "id_idioma", "nombre_idioma");
                ViewBag.id_nivel_idioma = new SelectList(db.nivel_idiomas, "id_nivel_idioma", "nombre_nivel_idioma");
                ViewBag.id_nivel_importancia = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
                ViewBag.id = id;
                return PartialView("_ofertas_main_especificaciones_idiomas_agrega");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult seleccion_agrega_profesion(int? id)
        {
            try
            {

                ViewBag.id_profesion = new SelectList(db.profesiones, "id_profesion", "nombre_profesion");
                ViewBag.id_nivel_importancia = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
                ViewBag.id = id;
                return PartialView("_ofertas_main_especificaciones_profesiones_agrega");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult seleccion_agrega_software(int? id)
        {
            try
            {

                ViewBag.id_software = new SelectList(db.softwares, "id_software", "nombre_software");
                ViewBag.id_grado_conocimiento_software = new SelectList(db.grado_conocimiento_softwares, "id_grado_conocimiento_software", "nombre_grado_conocimiento_software");
                ViewBag.id_nivel_importancia = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
                ViewBag.id = id;
                return PartialView("_ofertas_main_especificaciones_softwares_agrega");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult seleccion_agrega_pregunta(int? id)
        {
            try
            {

                ViewBag.id_tipo_pregunta = new SelectList(db.tipo_preguntas, "id_tipo_pregunta", "nombre_tipo_pregunta");
                ViewBag.id_preguntas_defecto = new SelectList(db.Database.SqlQuery<preguntas>("select * from preguntas where id_estado_pregunta = 1").ToList(), "id_pregunta", "nombre_pregunta");
                ViewBag.id = id;

                return PartialView("_ofertas_main_cuestionarios_pregunta_agrega");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult seleccion_agrega_respuesta(int? id)
        {
            try
            {

                ViewBag.id_respuesta = new SelectList(db.respuestas, "id_respuesta", "nombre_respuesta");

                ViewBag.id = id;

                return PartialView("_ofertas_main_preguntas_respuestas_agrega");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult seleccion_elimina_pregunta(int? id)
        {
            cuestionarios_preguntas c_p = db.cuestionarios_preguntas.Find(id);

            return PartialView("_ofertas_main_cuestionarios_pregunta_elimina", c_p);


        }
        public PartialViewResult seleccion_elimina_respuesta(int? id)
        {

            preguntas_respuestas p_r = db.preguntas_respuestas.Find(id);
            return PartialView("_ofertas_main_preguntas_respuestas_elimina", p_r);


        }
        public PartialViewResult seleccion_elimina_habilidad(int? id)
        {

            especificaciones_ofertas_habilidades s_o_h = db.especificaciones_ofertas_habilidades.Find(id);
            return PartialView("_ofertas_main_especificaciones_habilidades_elimina", s_o_h);


        }
        public PartialViewResult seleccion_elimina_idiomas(int? id)
        {

            especificaciones_ofertas_idiomas i_c = db.especificaciones_ofertas_idiomas.Find(id);
            return PartialView("_ofertas_main_especificaciones_idiomas_elimina", i_c);


        }
        public PartialViewResult seleccion_elimina_software(int? id)
        {

            especificaciones_ofertas_softwares h_c = db.especificaciones_ofertas_softwares.Find(id);
            return PartialView("_ofertas_main_especificaciones_softwares_elimina", h_c);


        }
        public PartialViewResult seleccion_elimina_profesiones(int? id)
        {

            especificaciones_ofertas_profesiones h_c = db.especificaciones_ofertas_profesiones.Find(id);
            return PartialView("_ofertas_main_especificaciones_profesiones_elimina", h_c);


        }

        [HttpPost, ActionName("delete_cuestionario_pregunta")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_1(int? id)
        {

            try
            {
                db.Database.ExecuteSqlCommand("exec sp_elimina_pregunta_cascada @id_pregunta = {0}", id);
                return Json(new { success = true, responseText = "Pregunta eliminada con exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "problema al eliminar la pregunta" }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost, ActionName("delete_pregunta_respuesta")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_2(int? id)
        {
            try
            {
                preguntas_respuestas p_r = db.preguntas_respuestas.Find(id);
                db.preguntas_respuestas.Remove(p_r);
                db.SaveChanges();
                return Json(new { success = true, responseText = " Respuesta Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost, ActionName("delete_especificacion_idioma")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int? id)
        {
            try
            {
                especificaciones_ofertas_idiomas e_o_i = db.especificaciones_ofertas_idiomas.Find(id);
                db.especificaciones_ofertas_idiomas.Remove(e_o_i);
                db.SaveChanges();
                return Json(new { success = true, responseText = " Especificacion Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("delete_especificacion_profesion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int? id)
        {
            try
            {
                especificaciones_ofertas_profesiones e_o_p = db.especificaciones_ofertas_profesiones.Find(id);
                db.especificaciones_ofertas_profesiones.Remove(e_o_p);
                db.SaveChanges();
                return Json(new { success = true, responseText = " Especificacion Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet); }

        }
        [HttpPost, ActionName("delete_especificacion_habilidad")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed3(int? id)
        {
            try
            {
                especificaciones_ofertas_habilidades e_o_h = db.especificaciones_ofertas_habilidades.Find(id);
                db.especificaciones_ofertas_habilidades.Remove(e_o_h);
                db.SaveChanges();
                return Json(new { success = true, responseText = " Especificacion Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet); }

        }
        [HttpPost, ActionName("delete_especificacion_software")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed4(int? id)
        {
            try
            {
                especificaciones_ofertas_softwares e_o_s = db.especificaciones_ofertas_softwares.Find(id);
                db.especificaciones_ofertas_softwares.Remove(e_o_s);
                db.SaveChanges();
                return Json(new { success = true, responseText = " Especificacion Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet); }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create_especificacion_habilidad([Bind(Include = "id_especificacion_oferta_habilidad , id_especificacion_oferta , id_habilidad , id_grado_habilidad , id_nivel_importancia")] especificaciones_ofertas_habilidades especificaciones_ofertas_habilidades)
        {


            if (ModelState.IsValid)
            {
                if (!db.especificaciones_ofertas_habilidades.Any(x => x.id_especificacion_oferta == especificaciones_ofertas_habilidades.id_especificacion_oferta && x.id_habilidad == especificaciones_ofertas_habilidades.id_habilidad))
                {
                    db.especificaciones_ofertas_habilidades.Add(especificaciones_ofertas_habilidades);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "Habilidad agregada correctamente!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "esta habilidad ya se encuentra ingresada!" }, JsonRequestBehavior.AllowGet);
            }



            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create_especificacion_idioma([Bind(Include = "id_especificacion_oferta_idioma , id_especificacion_oferta , id_idioma , id_nivel_idioma , id_nivel_importancia")] especificaciones_ofertas_idiomas especificaciones_ofertas_idiomas)
        {


            if (ModelState.IsValid)
            {
                if (!db.especificaciones_ofertas_idiomas.Any(x => x.id_especificacion_oferta == especificaciones_ofertas_idiomas.id_especificacion_oferta && x.id_idioma == especificaciones_ofertas_idiomas.id_idioma))
                {
                    db.especificaciones_ofertas_idiomas.Add(especificaciones_ofertas_idiomas);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "Idioma agregado Correctamente!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "este idioma ya se encuentra ingresado" }, JsonRequestBehavior.AllowGet);
            }



            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create_especificacion_profesion([Bind(Include = "id_especificacion_oferta_profesion , id_especificacion_oferta , id_profesion ,id_nivel_importancia")] especificaciones_ofertas_profesiones especificaciones_ofertas_profesiones)
        {


            if (ModelState.IsValid)
            {
                if (!db.especificaciones_ofertas_profesiones.Any(x => x.id_especificacion_oferta == especificaciones_ofertas_profesiones.id_especificacion_oferta && x.id_profesion == especificaciones_ofertas_profesiones.id_profesion))
                {
                    db.especificaciones_ofertas_profesiones.Add(especificaciones_ofertas_profesiones);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "Profesion agregada correctamente!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "esta profesion ya se encuentra ingresada" }, JsonRequestBehavior.AllowGet);
            }



            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create_especificacion_software([Bind(Include = "id_especificacion_oferta_software , id_especificacion_oferta , id_software , id_grado_conocimiento_software ,id_nivel_importancia")] especificaciones_ofertas_softwares especificaciones_ofertas_softwares)
        {


            if (ModelState.IsValid)
            {
                if (!db.especificaciones_ofertas_softwares.Any(x => x.id_especificacion_oferta == especificaciones_ofertas_softwares.id_especificacion_oferta && x.id_software == especificaciones_ofertas_softwares.id_software))
                {
                    db.especificaciones_ofertas_softwares.Add(especificaciones_ofertas_softwares);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "Software necesario agregado Correctamente!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "este software ya fue ingresado!" }, JsonRequestBehavior.AllowGet);
            }



            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult create_pregunta([Bind(Include = "id_cuestionario , id_tipo_pregunta , id_software , nombre_pregunta")] preguntas_vista preguntas_vista)
        {


            if (ModelState.IsValid)
            {

                db.Database.ExecuteSqlCommand("Exec sp_inserta_pregunta @id_cuestionario = {0}, @id_tipo_pregunta  = {1}, @nombre_pregunta ={2}", preguntas_vista.id_cuestionario, preguntas_vista.id_tipo_pregunta, preguntas_vista.nombre_pregunta);

                return Json(new { success = true, responseText = "Pregunta creada Correctamente!" }, JsonRequestBehavior.AllowGet);

            }



            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult agrega_pregunta_defecto_cuestionario([Bind(Include = "id_cuestionario , id_pregunta")] preguntas_vista preguntas_vista)
        {


            if (ModelState.IsValid)
            {

                db.Database.ExecuteSqlCommand("Exec sp_inserta_pregunta_defecto @id_cuestionario = {0}, @id_pregunta  = {1}", preguntas_vista.id_cuestionario, preguntas_vista.id_pregunta);

                return Json(new { success = true, responseText = "Pregunta creada correctamente!" }, JsonRequestBehavior.AllowGet);

            }



            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult create_respuesta_pregunta([Bind(Include = "id_pregunta , id_respuesta ")] preguntas_respuestas preguntas_respuestas)
        {

            if (ModelState.IsValid)
            {
                if (!db.preguntas_respuestas.Any(x => x.id_pregunta == preguntas_respuestas.id_pregunta && x.id_respuesta == preguntas_respuestas.id_respuesta))
                {
                    db.preguntas_respuestas.Add(preguntas_respuestas);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "respuesta asignada correctamente a tu pregunta" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "esta respuesta ya se encuentra asiganada a esta pregunta" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { success = false, responseText = "error inesperado , favor contactar con el administrador" }, JsonRequestBehavior.AllowGet);


        }

        public PartialViewResult especificacion_desactiva(int? id)
        {
            try
            {

                var x = db.Database.SqlQuery<int>("exec sp_obtener_id_especificacion_oferta @id_oferta  = {0}", id).Single();
                ViewBag.id_especificacion_oferta = 0;
                ViewBag.id = id;
                return PartialView("_ofertas_main_especificaciones_control");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult especificacion_activa(int? id)
        {
            try
            {

                var x = db.Database.SqlQuery<int>("exec sp_obtener_id_especificacion_oferta @id_oferta  = {0}", id).Single();
                ViewBag.id_especificacion_oferta = x;


                ViewBag.id = id;
                return PartialView("_ofertas_main_especificaciones_control");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult cuestionario_desactiva(int? id)
        {
            try
            {


                var x = db.Database.SqlQuery<int>("exec sp_obtener_id_cuestionario @id_oferta  = {0}", id).Single();
                ViewBag.id_cuestionario = 0;
                ViewBag.id = id;
                return PartialView("_ofertas_main_cuestionarios_control");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        public PartialViewResult cuestionario_activa(int? id)
        {
            try
            {

                var x = db.Database.SqlQuery<int>("exec sp_obtener_id_cuestionario @id_oferta  = {0}", id).Single();
                ViewBag.id_cuestionario = x;


                ViewBag.id = id;
                return PartialView("_ofertas_main_cuestionarios_control");

            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult activador_especificacion([Bind(Include = "id_oferta , id_especificacion_oferta")] ofertas ofertas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    filtro_total();
                    db.Database.ExecuteSqlCommand("Exec sp_actualiza_oferta_especificacion_oferta @id_oferta = {0}, @id_especificacion_oferta  = {1} ", ofertas.id_oferta, ofertas.id_especificacion_oferta);


                    return Json(new { success = true, responseText = "Descripcion Actualizada!" }, JsonRequestBehavior.AllowGet);

                }
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();
                return Json(new { success = false, responseText = errorList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult activador_cuestionario([Bind(Include = "id_oferta , id_cuestionario")] ofertas ofertas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    filtro_total();
                    db.Database.ExecuteSqlCommand("Exec sp_actualiza_oferta_cuestionario @id_oferta = {0}, @id_cuestionario  = {1} ", ofertas.id_oferta, ofertas.id_cuestionario);


                    return Json(new { success = true, responseText = "Descripcion Actualizada!" }, JsonRequestBehavior.AllowGet);

                }
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();
                return Json(new { success = false, responseText = errorList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        public void cargarcombos()
        {
            ViewBag.id_categoria = new SelectList(db.categorias, "id_categoria", "nombre_categoria");
            ViewBag.id_direccion = new SelectList(db.direcciones, "id_direccion", "nombre_direccion");
            ViewBag.id_jornada_oferta = new SelectList(db.jornada_ofertas, "id_jornada_oferta", "nombre_jornada_oferta");
            ViewBag.id_contrato_oferta = new SelectList(db.contrato_ofertas, "id_contrato_oferta", "nombre_contrato_oferta");
            ViewBag.id_tipo_oferta = new SelectList(db.tipo_ofertas, "id_tipo_oferta", "nombre_tipo_oferta");
            ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "nombre_departamento");
            ViewBag.id_area = new SelectList(db.areas, "id_area", "nombre_area");
            ViewBag.id_estado_oferta = new SelectList(db.estado_ofertas, "id_estado_oferta", "nombre_estado_oferta");
            ViewBag.id_habilidad = new SelectList(db.habilidades, "id_habilidad", "nombre_habilidad");
            ViewBag.id_idioma = new SelectList(db.idiomas, "id_idioma", "nombre_idioma");
            ViewBag.id_profesion = new SelectList(db.profesiones, "id_profesion", "nombre_profesion");
            ViewBag.id_software = new SelectList(db.softwares, "id_software", "nombre_software");
            ViewBag.id_nivel_habilidad = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
            ViewBag.id_nivel_idioma = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
            ViewBag.id_nivel_profesion = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
            ViewBag.id_nivel_software = new SelectList(db.nivel_importancia, "id_nivel_importancia", "nombre_nivel_importancia");
        }

        public ActionResult filtrar_basico(int? page, string palabra_clave)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {

                    return RedirectToAction("LogOff", "login");
                }
                List<vista_buscador_oferta> Articles;


                if (palabra_clave == null)
                {
                    palabra_clave = "";
                }
                filtro_total();
                carga_estados();

                var l = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));

                Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_buscador_ofertas_basico @palabra_clave = {0} , @id_grupo_usuario = {1}", palabra_clave, l.id_grupo_usuario).ToList();


                cargarcombos();
                int pageNumber = (page ?? 1);
                ViewBag.palabra_clave = palabra_clave;
                ViewBag.control = "filtrar_basico";
                return View("oferta_main_mis_ofertas", Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                return PartialView("error");

            }

        }

        public ActionResult filtrar_full(int? page, string id_categoria, string id_tipo_oferta, string id_contrato_oferta,
            string id_jornada_oferta, string monto_maximo, string id_direccion, string id_departamento, string id_area,
          string ultimo_update, FormCollection form)
        {
            try
            {

                if (Session.Contents.Count < 1)
                {

                    return RedirectToAction("LogOff", "login");
                }

                List<vista_buscador_oferta> Articles;

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
                    rad = new string[9];
                    rad[0] = id_categoria;
                    rad[1] = id_tipo_oferta;
                    rad[2] = id_contrato_oferta;
                    rad[3] = id_jornada_oferta;
                    rad[4] = monto_maximo;
                    rad[5] = id_direccion;
                    rad[6] = id_departamento;
                    rad[7] = id_area;
                    rad[8] = ultimo_update;


                }



                ViewBag.filtros1 = rad[0];
                ViewBag.filtros2 = rad[1];
                ViewBag.filtros3 = rad[2];

                ViewBag.filtros4 = rad[3];
                ViewBag.filtros5 = rad[4];
                ViewBag.filtros6 = rad[5];

                ViewBag.filtros7 = rad[6];
                ViewBag.filtros8 = rad[7];
                ViewBag.filtros9 = rad[8];


                filtro_total();

                carga_estados();
                var l = db.usuarios.Find(Convert.ToInt32(Session["usuario_id"]));
                Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_buscador_ofertas_avanzado @id_categoria = {0}  , @id_tipo_oferta = {1} , "
          + "@id_contrato_oferta = {2}, @id_jornada_oferta = {3}, "
          + " @monto_maximo = {4}, @id_area = {5}, @ultimo_update = {6} , @id_grupo_usuario = {7}",
         rad[0], rad[1], rad[2], rad[3], rad[4],
          rad[7], rad[8], l.id_grupo_usuario).ToList();

                cargarcombos();
                int pageNumber = (page ?? 1);

                ViewBag.control = "filtrar_full";
                return View("oferta_main_mis_ofertas", Articles.ToPagedList(pageNumber, itemsPerPage));
            }
            catch (Exception ex)
            {
                return PartialView("error");
            }
        }
        public ActionResult filtrar_estado(int? page, int id_estado_oferta_filtro)
        {
            try
            {

                if (Session.Contents.Count < 1)
                {

                    return RedirectToAction("LogOff", "login");
                }
                filtro_total();
                List<vista_buscador_oferta> Articles;
                carga_estados();
                if (Convert.ToInt32(Session["role_id"]) >= 2)
                {
                    Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_buscador_ofertas_estado @id_estado_oferta = {0} , @id_usuario = {1} ,@id_usuario_oferta = {2}", id_estado_oferta_filtro, Convert.ToInt32(Session["usuario_id"]), null).ToList();
                }
                else
                {
                    Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_buscador_ofertas_estado @id_estado_oferta = {0} , @id_usuario = {1} ,@id_usuario_oferta = {2}", id_estado_oferta_filtro, Convert.ToInt32(Session["usuario_id"]), Convert.ToInt32(Session["usuario_id"])).ToList();
                }


                cargarcombos();
                int pageNumber = (page ?? 1);
                ViewBag.id_estado_oferta_filtro = id_estado_oferta_filtro;
                ViewBag.control = "filtrar_estado";
                return View("oferta_main_mis_ofertas", Articles.ToPagedList(pageNumber, itemsPerPage));

            }
            catch (Exception ex)
            {
                return PartialView("error");
            }





        }


        public PartialViewResult vista_duplicar_oferta(int? id)
        {
            try
            {

                ViewBag.id_oferta_dupli = id;

                return PartialView("_ofertas_main_valida_duplica_oferta");


            }

            catch (Exception ex)
            {
                return PartialView("error");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult duplicar_oferta(int id_oferta)
        {

            try
            {

                db.Database.ExecuteSqlCommand("exec sp_republicar_oferta @id_oferta = {0}", id_oferta);

                return Json(new { success = true, responseText = "Oferta Republicada Exitosamente" }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Hubo un problema y la oferta no pudo ser duplicada" }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult descarga_resumen_oferta_pdf(int? id, string nombre_oferta)
        {



            return new Rotativa.ActionAsPdf("crea_pdf", new { id = id }) { FileName = nombre_oferta + ".pdf" };


        }
        public PartialViewResult crea_pdf(int id)
        {
            vista_oferta_descargar k = db.Database.SqlQuery<vista_oferta_descargar>("exec sp_obtener_datos_oferta @id_oferta = {0}", id).Single();

            carga_especificaciones(id);
            return PartialView("_resumen_oferta_pdf", k);
        }

        public void carga_especificaciones(int id)
        {


            var x = db.Database.SqlQuery<int>("select id_especificacion_oferta from ofertas where id_oferta  = {0}", id).Single();

            List<especificaciones_ofertas_habilidades_vista> datos = db.Database.SqlQuery<especificaciones_ofertas_habilidades_vista>
            ("exec sp_obtener_especificaciones_ofertas_habilidades @id_especificacion_oferta = {0} ", x).ToList();


            ViewBag.nombre_habilidad = new string[(datos.Count())];
            ViewBag.nombre_grado_habilidad = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {



                ViewBag.nombre_habilidad[i] = k.nombre_habilidad;
                ViewBag.nombre_grado_habilidad[i] = k.nombre_grado_habilidad;

                i++;
            }

            List<especificaciones_ofertas_idiomas_vista> datos1 = db.Database.SqlQuery<especificaciones_ofertas_idiomas_vista>
      ("exec sp_obtener_especificaciones_ofertas_idiomas @id_especificacion_oferta = {0}", x).ToList();


            ViewBag.nombre_idioma = new string[(datos1.Count())];
            ViewBag.nombre_nivel_idioma = new string[(datos1.Count())];
            i = 0;
            foreach (var k1 in datos1)
            {



                ViewBag.nombre_idioma[i] = k1.nombre_idioma;
                ViewBag.nombre_nivel_idioma[i] = k1.nombre_nivel_idioma;
                i++;
            }
            List<especificaciones_ofertas_profesiones_vista> datos2 = db.Database.SqlQuery<especificaciones_ofertas_profesiones_vista>
      ("exec sp_obtener_especificaciones_ofertas_profesiones @id_especificacion_oferta = {0}", x).ToList();

            ViewBag.nombre_profesion = new string[(datos2.Count())];
            i = 0;
            foreach (var k2 in datos2)
            {


                ViewBag.nombre_profesion[i] = k2.nombre_profesion;

                i++;
            }
            List<especificaciones_ofertas_softwares_vista> datos3 = db.Database.SqlQuery<especificaciones_ofertas_softwares_vista>
      ("exec sp_obtener_especificaciones_ofertas_softwares @id_especificacion_oferta = {0}", x).ToList();


            ViewBag.nombre_software = new string[(datos3.Count())];
            ViewBag.nombre_grado_conocimiento_software = new string[(datos3.Count())];
            i = 0;
            foreach (var k3 in datos3)
            {


                ViewBag.nombre_software[i] = k3.nombre_software;
                ViewBag.nombre_grado_conocimiento_software[i] = k3.nombre_grado_conocimiento_software;
                i++;
            }

        }
        public void carga_estado_ofertas(int? id)
        {


            var a = db.Database.SqlQuery<int>("select usuarios_ofertas.id_estado_oferta  from usuarios_ofertas where id_oferta = {0} ", id).Single();

            List<estado_ofertas> datos = db.estado_ofertas.ToList();

            ViewBag.id_estado_oferta_actual = a;
            ViewBag.id_estado_oferta_control = new int[(datos.Count())];
            ViewBag.nombre_estado_oferta_control = new string[(datos.Count())];
            int i = 0;
            foreach (var k in datos)
            {



                ViewBag.id_estado_oferta_control[i] = k.id_estado_oferta;
                ViewBag.nombre_estado_oferta_control[i] = k.nombre_estado_oferta;

                i++;
            }

        }

        public PartialViewResult actualiza_estado_oferta(int id, int id_oferta_estado)
        {
            ViewBag.id_oferta = id;
            ViewBag.id_oferta_estado = id_oferta_estado;
            carga_estado_ofertas(id);
            return PartialView("ofertas_main_actualiza_estado_oferta");
        }

        public PartialViewResult actualiza_datos_oferta(int id)
        {


            ofertas ofertas = db.ofertas.Find(id);

            usuarios_ofertas usuario_ofertas = db.usuarios_ofertas.First(c => c.id_oferta == id);
            ViewBag.id_departamento = new SelectList(db.departamentos, "id_departamento", "nombre_departamento", usuario_ofertas.areas.id_departamento);


            ViewBag.id_area = new SelectList(db.areas, "id_area", "nombre_area", usuario_ofertas.id_area);
            ViewBag.id_direccion = new SelectList(db.direcciones, "id_direccion", "nombre_direccion", usuario_ofertas.areas.departamentos.id_direccion);
            ViewBag.id_jornada_oferta = new SelectList(db.jornada_ofertas, "id_jornada_oferta", "nombre_jornada_oferta", ofertas.id_jornada_oferta);
            ViewBag.id_contrato_oferta = new SelectList(db.contrato_ofertas, "id_contrato_oferta", "nombre_contrato_oferta", ofertas.id_contrato_oferta);
            ViewBag.id_tipo_oferta = new SelectList(db.tipo_ofertas, "id_tipo_oferta", "nombre_tipo_oferta", ofertas.id_tipo_oferta);
            ViewBag.id_oferta = id;
            return PartialView("_ofertas_main_actualiza_datos_oferta", ofertas);
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult actualiza_estado_oferta_datos(int id, string id_oferta_estado)
        {
            try
            {
                if (id_oferta_estado != null)
                {
                    db.Database.ExecuteSqlCommand("exec  sp_actualiza_estado_oferta @id_oferta = {0} , @id_estado_oferta = {1}", id, id_oferta_estado);
                }
                else
                {
                    return Json(new { success = false, responseText = "Selecciona un estado para actualizar la oferta" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, responseText = "actualizado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "han surgido problemirijillas" }, JsonRequestBehavior.AllowGet);
            }

        }


        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult actualiza_oferta_datos(FormCollection form)
        {
            try
            {

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
                if (rad[6] == null)
                {
                    return Json(new { success = false, responseText = "debes ingresar un area para la oferta" }, JsonRequestBehavior.AllowGet);
                }
                db.Database.ExecuteSqlCommand("Exec sp_actualiza_datos_oferta @id_oferta = {0} , @id_area = {1} , @nombre_oferta = {2} , " +
                        " @id_jornada_oferta = {3} , @id_contrato_oferta = {4} , @id_tipo_oferta = {5} , @descripcion_oferta = {6} , " +
                        "@monto_oferta = {7} , @id_usuario_modificador = {8}", rad[1], rad[6], rad[2], rad[3], rad[4], rad[5], rad[8], rad[7], Convert.ToInt32(Session["usuario_id"]));

                return Json(new { success = true, responseText = "actualizado correctamente" }, JsonRequestBehavior.AllowGet);



            }

            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "han surgido problemirijillas" }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult vista_categorias_ofertas(int? id)
        {
            filtro_total();
            List<ofertas_categorias> o_c = db.ofertas_categorias.Where(x => x.id_oferta == id).ToList();

            return PartialView("ofertas_categorias/_ofertas_main_categorias_vista", o_c);


        }
        public PartialViewResult seleccion_elimina_categoria(int? id)
        {
            ViewBag.id = id;
            return PartialView("ofertas_categorias/_ofertas_main_categorias_elimina");


        }
        [HttpPost, ActionName("elimina_categoria_oferta")]

        public ActionResult elimina_categoria_oferta(int? id)
        {
            try
            {
                db.Database.ExecuteSqlCommand("delete from ofertas_categorias where id_oferta_categoria = {0}", id);
                return Json(new { success = true, responseText = " Respuesta Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet);
            }

        }
        public PartialViewResult agrega_vista_categorias_ofertas(int? id)
        {
            ViewBag.id = id;
            ViewBag.id_categoria = new SelectList(db.categorias, "id_categoria", "nombre_categoria");

            return PartialView("ofertas_categorias/_ofertas_main_categorias_agrega");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult agrega_categorias_ofertas([Bind(Include = "id_categoria , id_oferta ")] ofertas_categorias ofertas_categorias)
        {

            if (ModelState.IsValid)
            {
                int k = db.Database.SqlQuery<int>("select count( id_oferta_categoria) from ofertas_categorias where id_categoria = {0} and id_oferta = {1}", ofertas_categorias.id_categoria, ofertas_categorias.id_oferta).Single();
                if (k < 1)
                {
                    db.ofertas_categorias.Add(ofertas_categorias);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "La Categoria se agrego Correctamente!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "Ya tienes Esta Habilidad , aprende una nueva!!" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { success = false, responseText = "modelo no valido" }, JsonRequestBehavior.AllowGet);


        }

        public PartialViewResult vista_documentos_ofertas(int? id)
        {
            filtro_total();

            List<ofertas_documentos> o_d = db.ofertas_documentos.Where(x => x.id_oferta == id).ToList();
            return PartialView("ofertas_documentos/_ofertas_main_documentos_vista", o_d);


        }
        public PartialViewResult agrega_vista_documentos_ofertas(int? id)
        {

            ViewBag.id = id;
            ViewBag.id_documento = new SelectList(db.documentos, "id_documento", "nombre_documento");
            ViewBag.id_relevancia_documento = new SelectList(db.relevancia_documentos, "id_relevancia_documento", "nombre_relevancia_documento");
            return PartialView("ofertas_documentos/_ofertas_main_documentos_agrega");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult agrega_documentos_ofertas([Bind(Include = "id_documento , id_oferta , id_relevancia_documento , nombre_documento_oferta , enlace_documento_oferta ")] ofertas_documentos ofertas_documentos)
        {

            if (ModelState.IsValid)
            {
                int k = db.Database.SqlQuery<int>("select count( id_oferta_documento) from ofertas_documentos where id_documento = {0} and id_oferta = {1}", ofertas_documentos.id_oferta, ofertas_documentos.id_documento).Single();
                if (k < 1)
                {
                    db.Database.ExecuteSqlCommand("exec sp_inserta_documentos_oferta @id_oferta = {0} , @id_documento = {1} , @id_relevancia_documento = {2} , @nombre_documento_oferta = {3} ,@enlace_documento_oferta = {4}"
                        , ofertas_documentos.id_oferta, ofertas_documentos.id_documento, ofertas_documentos.id_relevancia_documento, ofertas_documentos.nombre_documento_oferta, ofertas_documentos.enlace_documento_oferta);
                    return Json(new { success = true, responseText = "Documento ingresado Correctamente!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, responseText = "Ya tienes ingresado este documento , aprende una nueva!!" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { success = false, responseText = "modelo no valido" }, JsonRequestBehavior.AllowGet);


        }

        public PartialViewResult detalle_documentos_ofertas(int? id)
        {


            return PartialView("ofertas_documentos/_ofertas_main_documentos_detalle");


        }
        public PartialViewResult seleccion_elimina_documento(int? id)
        {

            ViewBag.id = id;
            return PartialView("ofertas_documentos/_ofertas_main_documentos_elimina");


        }
        [HttpPost, ActionName("elimina_documento_oferta")]
        public ActionResult elimina_documento_oferta(int? id)
        {
            try
            {

                db.Database.ExecuteSqlCommand("delete from ofertas_documentos where id_oferta_documento = {0}", id);
                return Json(new { success = true, responseText = " Respuesta Eliminada con Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "el elemento no se encuentra disponible para eliminar" }, JsonRequestBehavior.AllowGet);
            }

        }

        public PartialViewResult vista_tabla_actualizaciones()

        {

            return PartialView("inicio_login_ofertas/_vista_tabla_actualizacion_sistema");
        }
        public PartialViewResult vista_porcentaje_oferta()

        {
            try
            {
                ViewBag.id_usuario = Convert.ToInt32(Session["usuario_id"]);
                ofertas ofertas = db.Database.SqlQuery<ofertas>("select top(1) * from ofertas inner join usuarios_ofertas on ofertas.id_oferta = usuarios_ofertas.id_oferta where id_usuario = {0} order by fecha_modificacion_oferta DESC", Convert.ToInt32(Session["usuario_id"])).SingleOrDefault();
                ViewBag.id_oferta = ofertas.id_oferta;
                ViewBag.nombre_oferta = ofertas.nombre_oferta;
                return PartialView("inicio_login_ofertas/_vista_procentaje_ultima_oferta");
            }
            catch (Exception ex)
            {
                ViewBag.id_oferta = null;
                ViewBag.nombre_oferta = null;
                return PartialView("inicio_login_ofertas/_vista_procentaje_ultima_oferta");
            }
        }
        public PartialViewResult vista_total_postulados()

        {
            try
            {

                return PartialView("inicio_login_ofertas/_vista_total_postulados_mes_oferta");
            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return PartialView("Error");
            }
        }

        public PartialViewResult preview_oferta(int id)

        {
            try
            {
                vista_oferta_descargar k = db.Database.SqlQuery<vista_oferta_descargar>("exec sp_obtener_datos_oferta @id_oferta = {0}", id).Single();
                // ViewBag.id_oferta = 1;
                ViewBag.oferta_inclusiva = k.oferta_inclusiva;
                carga_especificaciones(id);
                return PartialView("_resumen_oferta_pdf", k);

            }
            catch (Exception ex)
            {
                ViewBag.codigo = 2;
                return PartialView("Error");
            }
        }

        public ActionResult vista_activador_inclusividad_oferta(int id)
        {
            ViewBag.id = id;
            var k = db.ofertas.Find(id);
            ViewBag.oferta_inclusiva = k.oferta_inclusiva;
            return PartialView("ofertas_inclusivas/_vista_oferta_inclusiva");
        }

        public ActionResult activador_inclusividad_oferta(int id, int oferta_inclusiva)
        {

            try
            {
                if (oferta_inclusiva == 0)
                {
                    oferta_inclusiva = 1;
                }
                else
                {
                    oferta_inclusiva = 0;
                }

                db.Database.ExecuteSqlCommand("exec sp_actualiza_oferta_inclusiva @id_oferta = {0} , @oferta_inclusiva = {1}", id, oferta_inclusiva);

                return Json(new { success = true, responseText = "Se ha realizado el cambio de estado de la inclusividad" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "ah ocurrido un error al realizar el cambio de estado" }, JsonRequestBehavior.AllowGet);

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