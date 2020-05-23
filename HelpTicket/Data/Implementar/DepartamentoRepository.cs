using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        public bool Delete(int id)
        {
            bool eliminado = false;
            try
            {
                int filas;
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("begin transaction", conexion);
                    query.ExecuteNonQuery();
                    try
                    {
                        query = new SqlCommand("Delete from departamento where id = " + id, conexion);
                        filas = query.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            query = new SqlCommand("commit", conexion);
                            query.ExecuteNonQuery();
                            eliminado = true;
                        }
                        else
                        {
                            query = new SqlCommand("rollback", conexion);
                            query.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        query = new SqlCommand("rollback", conexion);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
            return eliminado;
        }

        public List<Departamento> FindAll()
        {
            List<Departamento> departamentos = null;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("Select * from departamento where estado = '");
                    sql.Append("S");
                    sql.Append("'");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        departamentos = new List<Departamento>();
                        while (dr.Read())
                        {
                            Departamento d = new Departamento();
                            d.id = Convert.ToInt32(dr["id"].ToString());
                            d.departamento = dr["departamento"].ToString();
                            d.fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]);
                            d.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]);
                            d.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]);
                            d.usuario_modificacion = dr["usuario_modificacion"].ToString();
                            d.estado = Convert.ToChar(dr["estado"]);
                            departamentos.Add(d);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return departamentos;
        }

        public Departamento FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Departamento t)
        {
            bool seInserto = false;
            try
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conn.Open();
                    var query = new SqlCommand("INSERT INTO departamento VALUES(@departamento,@fecha_creacion,@fecha_modificacion,@usuario_modificacion,@estado)", conn);
                    query.Parameters.AddWithValue("@departamento", t.departamento);
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

        public bool Update(Departamento t)
        {
            throw new NotImplementedException();
        }
    }
}
