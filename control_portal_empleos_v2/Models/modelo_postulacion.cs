using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public abstract class modelo_postulacion
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

     


    }

    public class super_modelo_postulacion : modelo_postulacion
    {

    }
}