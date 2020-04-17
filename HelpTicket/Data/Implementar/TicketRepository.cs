using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class TicketRepository : ITicketRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> FindAll()
        {
            throw new NotImplementedException();
        }

        public Ticket FindByCodAtencion(string cod_atencion)
        {
            Ticket t = null;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("Select codigo_atencion, solicitante_id, topico_id, asignado_id, importancia, descripcion, estado, ");
                    sql.Append("fecha_solicitado, fecha_asignado, fecha_finalizado, aprobador_id from Ticket ");
                    sql.Append("where codigo_atencion = '" + cod_atencion + "'");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        t = new Ticket();
                        t.codigo_atencion = string.Empty;
                        while (dr.Read())
                        {
                            if(t.codigo_atencion != string.Empty)
                            {
                                t = null;
                                break;
                            }
                            t.codigo_atencion= dr["codigo_atencion"].ToString();
                            t.solicitante_id = Convert.ToInt32(dr["solicitante_id"].ToString());
                            t.topico_id = Convert.ToInt32(dr["topico_id"].ToString());
                            t.asignado_id = Convert.ToInt32(dr["asignado_id"].ToString());
                            t.importancia = Convert.ToInt16(dr["importancia"].ToString());
                            t.descripcion = dr["descripcion"].ToString();
                            t.estado = Convert.ToChar(dr["estado"].ToString());
                            t.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());
                            t.fecha_asignado = Convert.ToDateTime(dr["fecha_asignado"].ToString());
                            t.fecha_finalizado = Convert.ToDateTime(dr["fecha_finalizado"].ToString());
                            t.aprobador_id = Convert.ToInt32(dr["aprobador_id"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return t;
        }

        public Ticket FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Ticket t)
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

                    sql.Append("Insert Into Ticket (solicitante_id, topico_id, importancia, descripcion, estado, fecha_solicitado )");
                    sql.Append("Values (" + t.solicitante_id + ", " + t.topico_id + ", " + t.importancia + ", " + t.descripcion);
                    sql.Append(", " + t.estado + ", " + DateTime.Now + ")");

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

        public List<Ticket> TicketsAsignados(string codigo_trabajador)
        {
            List<Ticket> t = null;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("Select codigo_atencion, solicitante_id, topico_id, asignado_id, importancia, descripcion, estado, ");
                    sql.Append("fecha_solicitado, fecha_asignado, fecha_finalizado, aprobador_id from Ticket ");
                    sql.Append("where asignado_id = '" + codigo_trabajador + "'");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        t = new List<Ticket>();
                        while (dr.Read())
                        {
                            Ticket ticket = new Ticket();
                            ticket.codigo_atencion = dr["codigo_atencion"].ToString();
                            ticket.solicitante_id = Convert.ToInt32(dr["solicitante_id"].ToString());
                            ticket.topico_id = Convert.ToInt32(dr["topico_id"].ToString());
                            ticket.asignado_id = Convert.ToInt32(dr["asignado_id"].ToString());
                            ticket.importancia = Convert.ToInt16(dr["importancia"].ToString());
                            ticket.descripcion = dr["descripcion"].ToString();
                            ticket.estado = Convert.ToChar(dr["estado"].ToString());
                            ticket.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());
                            ticket.fecha_asignado = Convert.ToDateTime(dr["fecha_asignado"].ToString());
                            ticket.fecha_finalizado = Convert.ToDateTime(dr["fecha_finalizado"].ToString());
                            ticket.aprobador_id = Convert.ToInt32(dr["aprobador_id"].ToString());

                            t.Add(ticket);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return t;
        }

        public List<Ticket> TicketsSolicitados(string codigo_cliente)
        {
            List<Ticket> t = null;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("Select codigo_atencion, solicitante_id, topico_id, asignado_id, importancia, descripcion, estado, ");
                    sql.Append("fecha_solicitado, fecha_asignado, fecha_finalizado, aprobador_id from Ticket ");
                    sql.Append("where solicitante_id = '" + codigo_cliente + "'");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        t = new List<Ticket>();
                        while (dr.Read())
                        {
                            Ticket ticket = new Ticket();
                            ticket.codigo_atencion = dr["codigo_atencion"].ToString();
                            ticket.solicitante_id = Convert.ToInt32(dr["solicitante_id"].ToString());
                            ticket.topico_id = Convert.ToInt32(dr["topico_id"].ToString());
                            ticket.asignado_id = Convert.ToInt32(dr["asignado_id"].ToString());
                            ticket.importancia = Convert.ToInt16(dr["importancia"].ToString());
                            ticket.descripcion = dr["descripcion"].ToString();
                            ticket.estado = Convert.ToChar(dr["estado"].ToString());
                            ticket.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());
                            ticket.fecha_asignado = Convert.ToDateTime(dr["fecha_asignado"].ToString());
                            ticket.fecha_finalizado = Convert.ToDateTime(dr["fecha_finalizado"].ToString());
                            ticket.aprobador_id = Convert.ToInt32(dr["aprobador_id"].ToString());

                            t.Add(ticket);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return t;
        }

        public bool Update(Ticket t)
        {
            throw new NotImplementedException();
        }
    }
}
