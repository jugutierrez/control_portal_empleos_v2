using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace control_portal_empleos_v2.Models
{
    public class cuentas
    {
        [Display(Name = "id_persona")]
        public int id_persona { get; set; }

        [Display(Name = "id_curriculum")]
        public int id_curriculum { get; set; }

        
    }

    public class PersonaDBContext : DbContext
    {
        public DbSet<direcciones> direcciones { get; set; }
        public DbSet<departamentos> departamentos { get; set; }
        public DbSet<areas> areas { get; set; }
        public DbSet<tipo_usuarios> tipo_usuarios { get; set; }
        public DbSet<estado_usuarios> estado_usuarios { get; set; }
        public DbSet<usuarios> usuarios { get; set; }

        public DbSet<contrato_ofertas> contrato_ofertas { get; set; }
        public DbSet<cuestionarios> cuestionarios { get; set; }
        public DbSet<especificaciones_ofertas> especificaciones_ofertas { get; set; }
        // public DbSet<idiomas> idiomas { get; set; }
        public DbSet<jornada_ofertas> jornada_ofertas { get; set; }
        public DbSet<ofertas> ofertas { get; set; }
        public DbSet<tipo_ofertas> tipo_ofertas { get; set; }



        public DbSet<usuarios_ofertas> usuarios_ofertas { get; set; }
        public DbSet<estado_ofertas> estado_ofertas { get; set; }
        public DbSet<profesiones> profesiones { get; set; }
        public DbSet<nivel_idiomas> nivel_idiomas { get; set; }
        public DbSet<especificaciones_ofertas_habilidades> especificaciones_ofertas_habilidades { get; set; }
        public DbSet<especificaciones_ofertas_idiomas> especificaciones_ofertas_idiomas { get; set; }
        public DbSet<especificaciones_ofertas_profesiones> especificaciones_ofertas_profesiones { get; set; }
        public DbSet<especificaciones_ofertas_softwares> especificaciones_ofertas_softwares { get; set; }




        public DbSet<estado_postulaciones> estado_postulaciones { get; set; }
        public DbSet<postulaciones> postulaciones { get; set; }
        public DbSet<cuestionarios_preguntas> cuestionarios_preguntas { get; set; }
        public DbSet<preguntas> preguntas { get; set; }
        public DbSet<postulaciones_cuestionarios> postulaciones_cuestionarios { get; set; }
        public DbSet<tipo_preguntas> tipo_preguntas { get; set; }
        public DbSet<preguntas_respuestas> preguntas_respuestas { get; set; }
        public DbSet<respuestas> respuestas { get; set; }
        public DbSet<personas> personas { get; set; }

        public System.Data.Entity.DbSet<control_portal_empleos_v2.Models.grado_habilidades> grado_habilidades { get; set; }

        public System.Data.Entity.DbSet<control_portal_empleos_v2.Models.habilidades> habilidades { get; set; }

        public System.Data.Entity.DbSet<control_portal_empleos_v2.Models.idiomas> idiomas { get; set; }

        public System.Data.Entity.DbSet<control_portal_empleos_v2.Models.grado_conocimiento_softwares> grado_conocimiento_softwares { get; set; }

        public System.Data.Entity.DbSet<control_portal_empleos_v2.Models.softwares> softwares { get; set; }

        public System.Data.Entity.DbSet<control_portal_empleos_v2.Models.especificaciones_ofertas_profesiones_vista> especificaciones_ofertas_profesiones_vista { get; set; }
        public DbSet<curriculums> curriculums { get; set; }
        public DbSet<comunas> comunas { get; set; }
        public DbSet<ciudades> ciudades { get; set; }
        public DbSet<regiones> regiones { get; set; }
        public DbSet<tipo_estudios> tipo_estudios { get; set; }
        public DbSet<estudios> estudios { get; set; }
        public DbSet<instituciones> instituciones { get; set; }
        public DbSet<estado_estudios> estado_estudios { get; set; }
        public DbSet<cargo_experiencias_laborales> cargo_experiencias_laborales { get; set; }
        public DbSet<area_experiencias_laborales> area_experiencias_laborales { get; set; }
        public DbSet<tipo_personas> tipo_personas { get; set; }
        public DbSet<nivel_importancia> nivel_importancia { get; set; }
        public DbSet<categorias> categorias { get; set; }
        public DbSet<ofertas_categorias> ofertas_categorias { get; set; }
        public DbSet<ofertas_documentos> ofertas_documentos { get; set; }
        public DbSet<documentos> documentos { get; set; }
        public DbSet<relevancia_documentos> relevancia_documentos { get; set; }

        /*
        public DbSet<documentos> documentos { get; set; }
        public DbSet<ofertas_categorias> ofertas_categorias { get; set; }
        public DbSet<ofertas_documentos> ofertas_documentos { get; set; }
        public DbSet<relevancia_documentos> relevancia_documentos { get; set; }
        public DbSet<categorias> categorias { get; set; }
        public DbSet<personas> personas { get; set; }
        
        public DbSet<tipo_personas> tipo_personas { get; set; }
        public DbSet<estado_personas> estado_personas { get; set; }
        public DbSet<discapacidad_personas> discapacidad_personas { get; set; }
        public DbSet<tipo_identificacion_personas> tipo_identificacion_personas { get; set; }
       
        public DbSet<estado_curriculums> estado_curriculums { get; set; }
     
        public DbSet<idiomas_curriculums> idiomas_curriculums { get; set; }
        public DbSet<curriculums_profesiones> curriculums_profesiones { get; set; }
        
        public DbSet<tipo_capacitaciones> tipo_capacitaciones { get; set; }
        
          */
    }


}