using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Util
{
    class Correo
    {
        MailMessage mail;
        SmtpClient smtp;
        string user;
        string password;

        public Correo()
        {
            mail = new MailMessage();
            smtp = new SmtpClient();
            user = System.Configuration.ConfigurationSettings.AppSettings["EmailUser"];
            password = System.Configuration.ConfigurationSettings.AppSettings["EMailPassword"];
        }

        public bool EnviarMensaje(Entity.Mensaje msm, string para, string mensaje)
        {
            try
            {
                mail.Priority = MailPriority.High;
                mail.Subject = msm.asunto;

                mail.IsBodyHtml = true;
                mail.Body = mensaje;
                mail.From = new MailAddress(user);
                mail.To.Add(para);

                smtp.Host = "eas.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(user, password);
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RecuperarContraseña(string para, string mensaje)
        {
            try
            {
                mail.Priority = MailPriority.High;
                mail.Subject = "Recuperación de Contraseña - HelpTicket";
                mail.IsBodyHtml = true;
                mail.Body = mensaje;
                mail.From = new MailAddress(user);
                mail.To.Add(para);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "eas.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(user, password);
                smtp.Send(mail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CorreoSolicitarFinalizar(string para, string mensaje, string cod_atencion)
        {
            try
            {
                mail.Priority = MailPriority.High;
                mail.Subject = "Solicitud de FINALIZACION de ticket " + cod_atencion;
                mail.IsBodyHtml = true;
                mail.Body = mensaje;
                mail.From = new MailAddress(user);
                mail.To.Add(para);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "eas.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(user, password);
                smtp.Send(mail);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
