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
    public class UsuarioModuloRolRepository : IUsuarioModuloRolRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario_Modulo_Rol> FindAll()
        {
			var umrs = new List<Usuario_Modulo_Rol>();
			try
			{
				using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					con.Open();

					var query = new SqlCommand("SELECT * FROM usuario_modulo_rol as umr inner join modulo_rol as mr on  umr.modulo_rol_id=mr.id inner join usuario as u on u.codigo=umr.usuario_id", con);
					using (var dr = query.ExecuteReader())
					{
						while (dr.Read())
						{
							var umr = new Usuario_Modulo_Rol();
							var usuario = new Usuario();
							var mr = new Modulo_Rol();


							umr.id = Convert.ToInt32(dr["id"]);
							umr.estado = Convert.ToChar(dr["estado"]); 
							umr.comentario = dr["comentario"].ToString(); 

							usuario.codigo= dr["usuario_id"].ToString();
							umr.Usuario = usuario;

							mr.id= Convert.ToInt32(dr["modulo_rol_id"]);
							umr.Modulo_Rol = mr;

						

							umrs.Add(umr);
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return umrs;
		}

        public Usuario_Modulo_Rol FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Usuario_Modulo_Rol t)
        {
            throw new NotImplementedException();
        }

        public int obtenerIDUserModRol(string codigo_usuario, string modulo, string rol)
        {
            StringBuilder sql = new StringBuilder();
            int id = 0;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("select umr.id from usuario_modulo_rol umr, modulo_rol mr, modulo m, rol r ");
                    sql.Append("where umr.usuario_id = '" + codigo_usuario + "' ");
                    sql.Append("and umr.estado = 'S' and umr.modulo_rol_id = mr.id and mr.estado = 'S' ");
                    sql.Append("and mr.modulo_id = m.id and m.estado = 'S' and m.modulo = '" + modulo + "' and mr.rol_id = r.id and r.estado = 'S' ");
                    sql.Append("and r.rol = '" + rol + "' ");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            id = Convert.ToInt32(dr["id"].ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return id;
        }

        public List<Usuario_Modulo_Rol> ObtenerTrabajadoresXskill(string topicoID)
        {
            var umrs = new List<Usuario_Modulo_Rol>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    con.Open();
                    StringBuilder cadena = new StringBuilder();
                    cadena.Append("Select urm.* from usuario_modulo_rol urm, modulo_rol mr, modulo m ");
                    cadena.Append("where urm.modulo_rol_id = mr.id and mr.modulo_id = m.id ");
                    cadena.Append("and urm.estado = 'S' and mr.estado = 'S' and m.estado = 'S' and m.modulo = 'ModuloTrabajador' ");
                    cadena.Append("and urm.usuario_id in (select s.usuario_id from skill s where s.topico_id = ");
                    cadena.Append(topicoID);
                    cadena.Append(")");

                   var query = new SqlCommand(cadena.ToString(), con);
                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var umr = new Usuario_Modulo_Rol();
                           // var usuario = new Usuario();
                           // var mr = new Modulo_Rol();


                            umr.id = Convert.ToInt32(dr["id"]);
                            umr.estado = Convert.ToChar(dr["estado"]);
                            umr.comentario = dr["comentario"].ToString();
                            umr.usuario_id = dr["usuario_id"].ToString();
                            //usuario.codigo = dr["usuario_id"].ToString();
                            //umr.Usuario = usuario;

                            // mr.id = Convert.ToInt32(dr["modulo_rol_id"]);
                            //umr.Modulo_Rol = mr;



                            umrs.Add(umr);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return umrs;
        }

        public bool Update(Usuario_Modulo_Rol t)
        {
            throw new NotImplementedException();
        }
    }
}
