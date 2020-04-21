using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class ModuloRepository : IModuloRepository
    {
        public bool AccesoModulo(string usuario_code, string modulo)
        {
            StringBuilder sql = new StringBuilder();
            bool acceso = false;
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("select 1 from usuario_modulo_rol umr, modulo_rol mr, modulo m ");
                    sql.Append("where umr.usuario_id = '" + usuario_code + "' ");
                    sql.Append("and umr.estado = 'S' and umr.modulo_rol_id = mr.id and mr.estado = 'S' ");
                    sql.Append("and mr.modulo_id = m.id and m.estado = 'S' and m.modulo = '" + modulo + "' ");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            acceso = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return acceso;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Modulo> FindAll()
        {
            throw new NotImplementedException();
        }

        public Modulo FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Modulo t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Modulo t)
        {
            throw new NotImplementedException();
        }
    }
}
