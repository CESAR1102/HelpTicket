using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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

        public bool Update(Usuario_Modulo_Rol t)
        {
            throw new NotImplementedException();
        }
    }
}
