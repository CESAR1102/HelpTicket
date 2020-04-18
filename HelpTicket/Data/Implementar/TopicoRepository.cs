using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public List<Topico> FindByDepartamento(int departamento_id)
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
