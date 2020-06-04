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
			bool seElimino = false;
			try
			{
				using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conn.Open();
					var query = new SqlCommand("DELETE FROM topico WHERE id='" + id + "'", conn);

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

        public List<Topico> FindAll()
        {
			var topicos = new List<Topico>();
			try
			{
				using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					con.Open();

					var query = new SqlCommand("SELECT * FROM topico as t inner join departamento as d on  d.id=t.departamento_id", con);
					using (var dr = query.ExecuteReader())
					{
						while (dr.Read())
						{
							var topico = new Topico();
							var departamento = new Departamento();
							topico.id = Convert.ToInt32(dr["id"]);
							topico.topico = dr["topico"].ToString();
							topico.usuario_modificacion = dr["usuario_modificacion"].ToString();
							//topico.estado = Convert.ToChar("estado");
							topico.fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"].ToString());
							topico.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"].ToString());
							topico.estado= Convert.ToChar(dr["estado"].ToString());


							departamento.id = Convert.ToInt32(dr["departamento_id"]);
							departamento.departamento= dr["departamento"].ToString();
							topico.Departamento = departamento;



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

        public bool ExistByDepartamento(int departamento_id, string identificador)
        {
            bool respuesta = false;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    sql.Append("Select 1 from topico where estado = '");
                    sql.Append("S");
                    sql.Append("' and departamento_id =");
                    sql.Append(departamento_id.ToString());
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

        public Topico FindById(int? id)
        {
			Topico topico = null;
			StringBuilder sql = new StringBuilder();
			try
			{
				using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conexion.Open();

					sql.Append("Select * from topico where estado = '");
					sql.Append("S");
					sql.Append("' ");
					sql.Append("and id =");
					sql.Append(id.ToString());

					var query = new SqlCommand(sql.ToString(), conexion);

					using (var dr = query.ExecuteReader())
					{
						topico = new Topico();
						while (dr.Read())
						{
							topico.id = Convert.ToInt32(dr["id"].ToString());
							topico.topico = dr["topico"].ToString();
							topico.fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]);
							topico.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]);
							topico.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]);
							topico.usuario_modificacion = dr["usuario_modificacion"].ToString();
							topico.estado = Convert.ToChar(dr["estado"]);
							topico.Departamento.id= Convert.ToInt32(dr["departamento_id"].ToString());
						}
					}
				}
			}
			catch (Exception)
			{

			}
			return topico;
		}

        public bool Insert(Topico t)
        {
			bool seInserto = false;
			try
			{
				using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conn.Open();
					var query = new SqlCommand("INSERT INTO topico VALUES(@topico,@departamento_id,@fecha_creacion,@fecha_modificacion,@usuario_modificacion,@estado)", conn);
					query.Parameters.AddWithValue("@topico", t.topico);
					query.Parameters.AddWithValue("@departamento_id", t.Departamento.id);
					query.Parameters.AddWithValue("@fecha_creacion", t.fecha_creacion);
					query.Parameters.AddWithValue("@fecha_modificacion", t.fecha_modificacion);
					query.Parameters.AddWithValue("@usuario_modificacion", t.usuario_modificacion);
					query.Parameters.AddWithValue("@estado", t.estado);
					query.ExecuteNonQuery();
					seInserto = true;
				}
			}
			catch (Exception e)
			{

				var a = e.Message;
			}
			return seInserto;
		}

        public bool Update(Topico t)
        {
			bool seActualizo = false;
			try
			{
				using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conn.Open();
					var query = new SqlCommand("Update topico set  topico = @topico, fecha_modificacion = @fecha_modificacion, usuario_modificacion = @usuario_modificacion where id = @id", conn);
					query.Parameters.AddWithValue("@topico", t.topico);
					query.Parameters.AddWithValue("@fecha_modificacion", t.fecha_modificacion);
					query.Parameters.AddWithValue("@usuario_modificacion", t.usuario_modificacion);
					query.Parameters.AddWithValue("@id", t.id);
					query.ExecuteNonQuery();
					seActualizo = true;
				}
			}
			catch (Exception e)
			{

				var a = e.Message;
			}
			return seActualizo;
		}
    }
}
