using System;
using System.Collections.Generic;
using System.Configuration;
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
					var query = new SqlCommand("Select * from usuario as u ", conexion);
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

        public Usuario FindByCodigo(string codigo)
        {
            var usuario = new Usuario();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("Select * from usuario as u where codigo = '" + codigo + "'", conexion);
                    using (var dr = query.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            usuario.nombres = dr["nombres"].ToString();
                            usuario.correo = dr["correo"].ToString();
                            usuario.codigo = dr["codigo"].ToString();
                        }


                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return usuario;
        }

        public bool Insert(Usuario t)
		{
			bool seInserto = false;
			try
			{
				using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conn.Open();
					var query = new SqlCommand("INSERT INTO usuario VALUES(@codigo,@nombres,@correo,@contraseña,@rol_creacion,@fecha_creacion,NULL)", conn);
					query.Parameters.AddWithValue("@codigo", t.codigo);
					query.Parameters.AddWithValue("@nombres", t.nombres);
					query.Parameters.AddWithValue("@correo", t.correo);
					query.Parameters.AddWithValue("@contraseña", t.contraseña);
					query.Parameters.AddWithValue("@rol_creacion", t.rol_creacion);
					query.Parameters.AddWithValue("@fecha_creacion", t.fecha_creacion);
					//query.Parameters.AddWithValue("@token", t.token);
					query.ExecuteNonQuery();
					seInserto = true;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return seInserto;
		}

        public bool Update(Usuario t)
        {
            bool seActualizo = false;
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conn.Open();
                    var query = new SqlCommand("UPDATE usuario SET nombres = @nombres,correo= @correo,contraseña= @contraseña,rol_creacion=@rol_creacion,fecha_creacion=@fecha_creacion WHERE codigo=@codigo", conn);

                    query.Parameters.AddWithValue("@codigo", t.codigo);
                    query.Parameters.AddWithValue("@nombres", t.nombres);
                    query.Parameters.AddWithValue("@correo", t.correo);
                    query.Parameters.AddWithValue("@contraseña", t.contraseña);
                    query.Parameters.AddWithValue("@rol_creacion", t.rol_creacion);
                    query.Parameters.AddWithValue("@fecha_creacion", t.fecha_creacion);
                    query.ExecuteNonQuery();
                    seActualizo = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return seActualizo;
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
				var query = new SqlCommand("Select u.codigo, u.contraseña, u.nombres, u.correo from usuario as u where u.codigo = '" + codigo + "'" + " and u.contraseña = '" + contraseña + "'", conexion);
				using (var dr = query.ExecuteReader())
				{
					var usuario = new Usuario();
					while (dr.Read())
					{
						usuario.codigo = dr["codigo"].ToString();
						usuario.contraseña = dr["contraseña"].ToString();
                        usuario.nombres = dr["nombres"].ToString();
                        usuario.correo = dr["correo"].ToString();
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

        public string ObtenerCorreo(string codigo)
        {
            string cod = string.Empty;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query1 = new SqlCommand("Select correo from usuario where codigo = '" + codigo + "'", conexion);
                    using (var dr = query1.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cod = dr["correo"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return cod;
        }

        public string ObtenerAdministrador(string codigo)
        {
            string cod = string.Empty;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query1 = new SqlCommand("select umr.usuario_id from ticket t inner join usuario_modulo_rol umr on t.aprobador_id= umr.id " +
                    "where t.codigo_atencion = '" + codigo + "'", conexion);
                    using (var dr = query1.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cod = dr["usuario_id"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return cod;
        }

        public List<Usuario> ObtenerTrabajadores()
        {
            var usuarios = new List<Usuario>();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    StringBuilder cadena = new StringBuilder();
                    //var query = new SqlCommand("Select u.nombres, u.correo, u.codigo from usuario u ", conexion);
                    cadena.Append("Select u.nombres, u.correo, u.codigo from usuario u, usuario_modulo_rol urm, modulo_rol mr, modulo m ");
                    cadena.Append("where urm.usuario_id = u.codigo and urm.modulo_rol_id = mr.id and mr.modulo_id = m.id ");
                    cadena.Append("and urm.estado = 'S' and mr.estado = 'S' and m.estado = 'S' and m.modulo = 'ModuloTrabajador'");

                    var query = new SqlCommand(cadena.ToString(), conexion);
                    using (var dr = query.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            var usuario = new Usuario();

                            usuario.nombres = dr["nombres"].ToString();
                            usuario.correo = dr["correo"].ToString();
                            usuario.codigo = dr["codigo"].ToString();


                            usuarios.Add(usuario);
                        }


                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return usuarios;
        }

        public bool DeleteUser(string id)
        {
            bool seElimino = false;
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conn.Open();
                    var query = new SqlCommand("DELETE FROM usuario WHERE codigo='" + id + "'", conn);

                    query.ExecuteNonQuery();
                    seElimino = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return seElimino;
        }
    }
}
