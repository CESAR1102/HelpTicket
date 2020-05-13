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
    public class TopicoRepository : ITopicoRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Topico> FindAll()
        {
			var topicos = new List<Topico>();
			try
			{
				using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					con.Open();

					var query = new SqlCommand("SELECT * FROM Topico ", con);
					using (var dr = query.ExecuteReader())
					{
						while (dr.Read())
						{
							var topico = new Topico();
							topico.id = Convert.ToInt32(dr["id"]);
							topico.topico = dr["topico"].ToString();
							topico.usuario_modificacion = dr["usuario_modificacion"].ToString();



							topicos.Add(topico);
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return topicos;
		}

        public List<Topico> FindByDepartamento(int departamento_id, string identificador)
        {
            List<Topico> topicos = null;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    sql.Append("Select id, topico from topico where estado = '");
                    sql.Append("S");
                    sql.Append("' and departamento_id =");
                    sql.Append(departamento_id.ToString());
                    sql.Append(" and acceso_usuario like '%");
                    sql.Append(identificador + "%'");
                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        topicos = new List<Topico>();
                        while (dr.Read())
                        {
                            Topico t = new Topico();
                            t.id = Convert.ToInt32(dr["id"].ToString());
                            t.topico = dr["topico"].ToString();
                            topicos.Add(t);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return topicos;
        }

        public Topico FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Topico t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Topico t)
        {
            throw new NotImplementedException();
        }
    }
}
