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
            throw new NotImplementedException();
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

                    sql.Append("Select id, departamento from departamento where estado = '");
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
            throw new NotImplementedException();
        }

        public bool Update(Departamento t)
        {
            throw new NotImplementedException();
        }
    }
}
