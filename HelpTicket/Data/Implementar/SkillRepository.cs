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
            throw new NotImplementedException();
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
