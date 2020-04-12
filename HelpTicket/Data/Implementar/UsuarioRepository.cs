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
					var query = new SqlCommand("Select u.ID_usuario, u.nombres,u.correo,u.codigo,u.contraseña from usuario as u ", conexion);
					using (var dr = query.ExecuteReader())
					{

						while (dr.Read())
						{
							var usuario = new Usuario();
							
							usuario.nombres = dr["nombres"].ToString();
							usuario.correo = dr["correo"].ToString();
							usuario.codigo = dr["codigo"].ToString();
							usuario.contraseña = dr["contraseña"].ToString();
							

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
            int registros = 0;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query1 = new SqlCommand("Select count(*) as num from usuario where codigo = '" + codigo + "'", conexion);
                    using (var dr = query1.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            registros = Convert.ToInt32(dr["num"].ToString());
                        }
                    }
                    if (registros == 1)
                    {
                        var query2 = new SqlCommand("Update usuario set token = '" + token + "' where codigo = '" + codigo + "'", conexion);
                        filas = query2.ExecuteNonQuery();
                        if (filas > 0) asignado = true;
                    }
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
		public List<Usuario> validar_usuario(string codigo, string contraseña)
		{

			var usuarios = new List<Usuario>();
			//try
			//{
		
			using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
			{
				conexion.Open();
				var query = new SqlCommand("Select u.codigo, u.contraseña from usuario as u where u.codigo = '" + codigo + "'" + " and u.contraseña = '" + contraseña + "'", conexion);
				using (var dr = query.ExecuteReader())
				{
					var usuario = new Usuario();
					while (dr.Read())
					{
						usuario.codigo = dr["codigo"].ToString();
						usuario.contraseña = dr["contraseña"].ToString();
						usuarios.Add(usuario);
					}


				}
			}
			//}
			//catch (Exception)
			//{

			//	throw;
			//	//msm = "Ocurrio un error";
			//}
			return usuarios;
		}

        public string VerificarToken(string token)
        {
            string codigo = string.Empty;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("Select u.codigo, u.token from Usuario as u where u.token = '" + token + "'", conexion);
                    using (var dr = query.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            codigo = dr["codigo"].ToString(); ;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return codigo;
        }

        public bool ActualizarContraseña(string codigo, string contra)
        {
            int filas;
            bool actualizado = false;
            int registros = 0;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query1 = new SqlCommand("Select count(*) as num from usuario where codigo = '" + codigo + "'", conexion);
                    using (var dr = query1.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            registros = Convert.ToInt32(dr["num"].ToString());
                        }
                    }
                    if (registros == 1)
                    {
                        var query2 = new SqlCommand("begin transaction", conexion);
                        query2.ExecuteNonQuery();
                        query2 = new SqlCommand("Update usuario set contraseña = '" + contra + "' where codigo = '" + codigo + "'", conexion);
                        filas = query2.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            query2 = new SqlCommand("Update usuario set token = NULL where codigo = '" + codigo + "'", conexion);
                            filas = query2.ExecuteNonQuery();
                            if (filas > 0)
                            {
                                query2 = new SqlCommand("commit", conexion);
                                query2.ExecuteNonQuery();
                                actualizado = true;
                            }
                            else
                            {
                                query2 = new SqlCommand("rollback", conexion);
                                query2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return actualizado;
        }
    }
}
