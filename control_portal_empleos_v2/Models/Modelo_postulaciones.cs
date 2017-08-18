using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public abstract class Modelo_postulaciones
    {
        public modelo_persona_curriculum_vista modelo_persona_curriculum_vista { get; set; }

        public List<capacitaciones_curriculums_vista> capacitaciones_curiculums_vista { get; set; }

        public List<habilidades_curriculums_vista> habilidades_curriculums_vista { get; set; }

        public curriculums curriculums { get; set; }

        public List<curriculums> curriculums_lista { get; set; }

        public persona_curriculum_vista persona_curriculum_vista { get; set; }

        public List<experiencias_laborales_curriculums_vista> experiencias_laborales_curriculums_vista { get; set; }

        public List<estudios_curriculums_vista> estudios_curriculums_vista { get; set; }

        public List<idiomas_curriculums_vista> idiomas_curriculums_vista { get; set; }

        public List<softwares_curriculums_vista> softwares_curriculums_vista { get; set; }

        public postulaciones postulaciones { get; set; }

        public personas personas { get; set; }

        public List<cuestionarios> cuestionarios { get; set; }

        public List<postulaciones_cuestionarios> postulaciones_cuestionarios { get; set; }

        public List<estado_postulaciones> estado_postulaciones { get; set; }
    }

    public class modelo_postulaciones_vista : Modelo_postulaciones
    { }
}