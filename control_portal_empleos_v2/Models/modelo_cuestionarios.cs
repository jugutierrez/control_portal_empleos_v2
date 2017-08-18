using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.Models
{
    public abstract class modelo_cuestionarios
    {

        public int id_cuestionario { get; set; }

        public List<cuestionarios_preguntas_vista> cuestionarios_pregunta { get; set; }

        
        public List<preguntas_respuestas> preguntas_respuesta { get; set; }

   
  
    }

    public class modelo_vista_cuestionarios : modelo_cuestionarios
    {

    }
}