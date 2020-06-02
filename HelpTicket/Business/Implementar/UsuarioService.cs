using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Implementar;
using Entity;
using System.Net;
using System.Net.Mail;
using Business.Util;

namespace Business.Implementar
{
	public class UsuarioService : IUsuarioService
	{
		private IUsuarioRepository usuario = new UsuarioRepository();
        private Random valor = new Random();
        

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Usuario> FindAll()
		{
			
			return usuario.FindAll();
		}
		
		public Usuario FindById(int? id)
		{
            throw new NotImplementedException();
        }

		public bool Insert(Usuario t)
		{
			return usuario.Insert(t);
		}

		public bool Update(Usuario t)
		{
            return usuario.Update(t);
        }
		
		public Usuario logeo(string codigo,string contraseña)
		{

            Usuario user = null;
            if (usuario.validar_usuario(codigo,contraseña).Count() > 0)
			{
                user = new Usuario();
                var datos = usuario.validar_usuario(codigo, contraseña).ToList();
				foreach (var Data in datos)
				{
					user.codigo = Data.codigo;
                    user.nombres = Data.nombres;
                    user.correo = Data.correo;
                }
			}
			return user;
		}

        private string GenerarToken(string correo, out string msm, out string codigo)
        {
            msm = string.Empty;
            codigo = usuario.ValidarCorreo(correo, out msm);
            if (codigo != string.Empty) 
            {
                string token = codigo;

                for (int i = 0; i < 10; i++)
                {
                    token += (char) valor.Next(65, 90);
                }
                return token;
            }
            else
            {
                msm = "No se encontro correo ingresado";
                return string.Empty;
            }
        }

        public bool AsignarToken(string correo, out string msm)
        {
            string codigo;
            string token = GenerarToken(correo, out msm, out codigo);
            if (token != string.Empty)
            {
                if (usuario.AsignarToken(codigo, token, out msm))
                {
                    return true;
                }
                else
                {
                    msm = "No se pudo generar token";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool EnviarEmailRecuperarContra(string correo)
        {
            try
            {
                string link = System.Configuration.ConfigurationSettings.AppSettings["IP"] + System.Configuration.ConfigurationSettings.AppSettings["LinkRecuperar"];
                string token = string.Empty;
                Correo mail = new Correo();
                StringBuilder mensaje = new StringBuilder();
                token = usuario.GetTokenByEmail(correo);
                if (token != string.Empty)
                {
                    link += token;
                }
                else
                {
                    return false;
                }
                mensaje.Append("<html><head></head><body>");
                mensaje.Append("<p>Estimado usuario, <br/> Ingresar a este link: ");
                mensaje.Append("<a href = '");
                mensaje.Append(link);
                mensaje.Append("'>" + link + "</a>");
                mensaje.Append(", para cambiar la contraseña.");
                mensaje.Append("<br/><br/>");
                mensaje.Append("No responder a este correo ya que nadie respondera. Envio automático.</p>");
                mensaje.Append("</body></html>");

                if (mail.RecuperarContraseña(correo, mensaje.ToString()))
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

        public bool VerificarToken(string token, out string codigo)
        {
            codigo = string.Empty;
            codigo = usuario.VerificarToken(token);
            if(codigo == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ActualizarContraseña(string codigo, string contra, out string msm)
        {
            msm = string.Empty;
            if (usuario.ActualizarContraseña(codigo, contra))
            {
                return true;
            }
            else
            {
                msm = "No se pudo actualizar la contraseña. Intente otra vez."; 
                return false;
            }
        }

        public string ObtenerCorreo(string codigo)
        {
            return usuario.ObtenerCorreo(codigo);
        }

        public string ObtenerAdministrador(string codigo)
        {
            return usuario.ObtenerAdministrador(codigo);
        }

        public List<Usuario> ObtenerTrabajadores()
        {
            return usuario.ObtenerTrabajadores();
        }

        public Usuario FindByCodigo(string codigo)
        {
            return usuario.FindByCodigo(codigo);
        }

        public bool DeleteUser(string id)
        {
            return usuario.DeleteUser(id);
        }

		
	}
}
