using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Implementar;
using Entity;

namespace Business.Implementar
{
    public class MensajeService : IMensajeService
    {
        IMensajeRepository mensa = new MensajeRepository();
        IUsuarioRepository usuario = new UsuarioRepository();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool EnviarMensaje(Mensaje msm)
        {
            try
            {
                if (mensa.Insert(msm))
                {
                    MailMessage mail = new MailMessage();
                    string user = System.Configuration.ConfigurationSettings.AppSettings["EmailUser"];
                    string password = System.Configuration.ConfigurationSettings.AppSettings["EMailPassword"];
                    StringBuilder mensaje = new StringBuilder();

                    mensaje.Append("<html><head></head><body>");
                    mensaje.Append("<p>");
                    mensaje.Append("Mensaje enviado por el usuario: " + msm.remitente_id);
                    mensaje.Append("</p> <br/>");
                    mensaje.Append("<p>");
                    mensaje.Append(msm.contenido);
                    mensaje.Append("</p>");
                    mensaje.Append("<br/>");
                    mensaje.Append("<p>Para seguir con el hilo de consersación, utilizar la aplicación.</p>");
                    mensaje.Append("</body></html>");

                    mail.Priority = MailPriority.High;
                    mail.Subject = msm.asunto;

                    mail.IsBodyHtml = true;
                    mail.Body = mensaje.ToString();
                    mail.From = new MailAddress(user);
                    mail.To.Add(usuario.ObtenerCorreo(msm.destinatario_id));

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "eas.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(user, password);
                    smtp.Send(mail);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
                return false;
            }
        }

        public List<Mensaje> FindAll()
        {
            throw new NotImplementedException();
        }

        public Mensaje FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Mensaje t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Mensaje t)
        {
            throw new NotImplementedException();
        }
    }
}
