using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class MensajeRepository : IMensajeRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Mensaje> FindAll()
        {
            throw new NotImplementedException();
        }

        public Mensaje FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Mensaje t)
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

                    sql.Append("Insert Into Mensaje (asunto, motivo, remitente_id, destinatario_id, enviado_el, contenido, ticket_id )");
                    sql.Append("Values ('" + t.asunto + "', '" + t.motivo + "', '" + t.remitente_id + "', '" + t.destinatario_id + "', '");
                    sql.Append(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "', '" + t.contenido + "', '" + t.ticket_id + "')");

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

        public bool Update(Mensaje t)
        {
            throw new NotImplementedException();
        }
    }
}
