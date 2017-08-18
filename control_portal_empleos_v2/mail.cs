using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Postal;

namespace control_portal_empleos_v2
{
    public class mail
    {
        
        public void enviar_correo(byte[] pdf , string email_para , int tipo_correo)
        {
            string formato;
            string titulo;
            Boolean adjunto = false;
            switch (tipo_correo)
            {
                case 0:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                case 1:
                    formato = "comparte_curriculum_correo";
                    titulo= "Portal de empleos (curriculum de interes)";
                    adjunto = true;
                    break;
                case 2:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                case 3:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                case 4:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                case 5:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                case 6:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                case 7:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    break;
                default:
                    formato = "";
                    titulo = "";
                    break;
            }

          
            dynamic email = new Email(formato);
            email.To = email_para;
            email.Subject = titulo;
             if (adjunto == true)
            {
                Attachment data = new Attachment(new MemoryStream(pdf), "Documento_adjunto.pdf", "application/pdf");
                email.Attachments.Add(data);
            }
            email.IsBodyHtml = true;
            email.Send();
        }

    }
}