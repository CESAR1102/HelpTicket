using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Business.Util;
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
                    Correo mail = new Correo();

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

                    if (mail.EnviarMensaje(msm, usuario.ObtenerCorreo(msm.destinatario_id), mensaje.ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public bool MensajeSolFinTicket(string de,  string contenido, string cod_Atencion)
        {
            try
            {
                Correo mail = new Correo();
                StringBuilder mensaje = new StringBuilder();
                var aprobador = usuario.ObtenerAdministrador(cod_Atencion);
                mensaje.Append("<html><head></head><body>");
                mensaje.Append("<p>");
                mensaje.Append("Mensaje enviado por el usuario: " + de);
                mensaje.Append("</p> <br/>");
                mensaje.Append("<p>");
                mensaje.Append(contenido);
                mensaje.Append("</p>");
                mensaje.Append("<br/>");
                mensaje.Append("</body></html>");

                if (mail.CorreoSolicitarFinalizar(usuario.ObtenerCorreo(aprobador), mensaje.ToString(), cod_Atencion))
                {
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

        public bool Update(Mensaje t)
        {
            throw new NotImplementedException();
        }
    }
}
