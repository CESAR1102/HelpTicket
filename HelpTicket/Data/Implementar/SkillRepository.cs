using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class SkillRepository : ISkillRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

		public bool ExistByTopico(int topico_id, string identificador)
		{
			bool respuesta = false;
			StringBuilder sql = new StringBuilder();
			try
			{
				using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conexion.Open();
					sql.Append("Select 1 from skill where estado = '");
					sql.Append("S");
					sql.Append("' and topico_id =");
					sql.Append(topico_id.ToString());
					sql.Append(" and acceso_usuario like '%");
					sql.Append(identificador + "%'");
					var query = new SqlCommand(sql.ToString(), conexion);

					using (var dr = query.ExecuteReader())
					{
						while (dr.Read())
						{
							respuesta = true;
							break;
						}
					}
				}
			}
			catch (Exception)
			{

			}
			return respuesta;
		}

        public bool ExistByTrabajador(int topico_id, string usuario)
        {
            bool respuesta = false;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    sql.Append("Select 1 from skill ");
                    sql.Append("where topico_id =");
                    sql.Append(topico_id.ToString());
                    sql.Append(" and usuario_id = '");
                    sql.Append(usuario + "'");
                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            respuesta = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return respuesta;
        }

        public List<Skill> FindAll()
        {
            throw new NotImplementedException();
        }

        public Skill FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Skill t)
        {
            int filas;
            bool resultado = false;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    var query1 = new SqlCommand("begin transaction", conexion);
                    query1.ExecuteNonQuery();

                    sql.Append("Insert Into Skill (usuario_id, topico_id, nivel)");
                    sql.Append("Values ('" + t.usuario_id + "', " + t.topico_id + ", " + t.nivel + ")");
                    
                    query1 = new SqlCommand(sql.ToString(), conexion);

                    filas = query1.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        query1 = new SqlCommand("commit", conexion);
                        query1.ExecuteNonQuery();
                        resultado = true;
                    }
                    else
                    {
                        query1 = new SqlCommand("rollback", conexion);
                        query1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
            return resultado;
        }

        public List<string> SkillXtrabajadorSimple(string codigo)
        {
            List<string> skills = new List<string>();

            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("Select t.topico, s.nivel from skill s, usuario u, topico t ");
                    sql.Append("where s.usuario_id = u.codigo and s.topico_id = t.id and u.codigo = '");
                    sql.Append(codigo);
                    sql.Append("'");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var sk = string.Empty;
                            sk = dr["topico"].ToString() + " (nivel " + dr["nivel"].ToString() + ")";
                            skills.Add(sk);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return skills;
        }

        public bool Update(Skill t)
        {
            throw new NotImplementedException();
        }
    }
}
