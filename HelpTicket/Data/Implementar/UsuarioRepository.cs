using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
	public class UsuarioRepository : IUsuarioRepository
	{
		public List<Usuario> FindAll()
		{
			var usuarios = new List<Usuario>();
			try
			{
				using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conexion.Open();
					var query = new SqlCommand("Select u.ID_usuario, u.nombres,u.correo,u.codigo,u.contraseña from Usuario as u ", conexion);
					using (var dr = query.ExecuteReader())
					{

						while (dr.Read())
						{
							var usuario = new Usuario();
							usuario.ID_usuario = Convert.ToInt32(dr["ID_usuario"]);
							usuario.nombres = dr["nombres"].ToString();
							usuario.correo = dr["correo"].ToString();
							usuario.codigo = dr["codigo"].ToString();
							usuario.Contraseña = dr["contraseña"].ToString();
							

							usuarios.Add(usuario);
						}


					}
				}
			}
			catch (Exception)
			{

				throw;
			}
			return usuarios;
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		
		public Usuario FindById(int? id)
		{
			throw new NotImplementedException();
		}

		public bool Insert(Usuario t)
		{
			throw new NotImplementedException();
		}

		public bool Update(Usuario t)
		{
			throw new NotImplementedException();
		}

        public string ValidarCorreo(string correo, out string msm)
        {
            string usuario = string.Empty;
            msm = string.Empty;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("Select u.correo, u.codigo from Usuario as u where u.correo = '" + correo + "'", conexion);
                    using (var dr = query.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            usuario = dr["codigo"].ToString(); ;
                        }


                    }
                }
            }
            catch (Exception)
            {

                msm = "Ocurrio un error";
            }
            return usuario;
        }

        public bool AsignarToken(string codigo, string token, out string msm)
        {
            int filas;
            bool asignado = false;
            msm = string.Empty;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("Update usuario set token = '" + token + "' where codigo = '" + codigo + "'", conexion);
                    filas = query.ExecuteNonQuery();
                    if (filas > 0) asignado = true;
                }
            }
            catch (Exception)
            {

                msm = "Ocurrio un error";
            }
            return asignado;
        }

        public string GetTokenByEmail(string correo)
        {
            string token = string.Empty;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("Select u.token from Usuario as u where u.correo = '" + correo + "'", conexion);
                    using (var dr = query.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            token = dr["token"].ToString(); ;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return token;
        }
    }
}
