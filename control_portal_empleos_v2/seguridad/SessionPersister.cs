using control_portal_empleos_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace control_portal_empleos_v2.seguridad
{
    public static class SessionPersister
    {


        public static bool activo
        {
            get
            {
                if (HttpContext.Current == null)
                    return false;
                var sessionVar = HttpContext.Current.Session["activo"];
                if (Convert.ToBoolean(sessionVar) == false)
                    return false;
                return true;
            }
            set
            {
                HttpContext.Current.Session["activo"] = value;
            }
        }
        public static string id_usuario
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["usuario_id"];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session["usuario_id"] = value;
            }
        }
        public static string id_tipo_usuario
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session["role_id"];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session["role_id"] = value;
            }
        }
        public static List<modulos_activos> lista_modulos
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                var sessionVar = HttpContext.Current.Session["modulos"];
                if (sessionVar != null)
                    return sessionVar as List<modulos_activos>;
                return null;
            }
            set
            {
                HttpContext.Current.Session["modulos"] = value;
            }
        }
    }
}