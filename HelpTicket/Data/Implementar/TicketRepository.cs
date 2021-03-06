﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class TicketRepository : ITicketRepository
    {
        public bool AsignarTicket(Ticket t)
        {
            bool actualizado = false;
            try
            {
                int filas;
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();
                    var query = new SqlCommand("begin transaction", conexion);
                    var fecha_asignado = t.fecha_asignado.Value.Year + "-" + t.fecha_asignado.Value.Month + "-" + t.fecha_asignado.Value.Day;
                    query.ExecuteNonQuery();
                    query = new SqlCommand("Update ticket set asignado_id = " + t.asignado_id + ", fecha_asignado = '" + fecha_asignado + "' where codigo_atencion = '" + t.codigo_atencion + "'", conexion);
                    filas = query.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        query = new SqlCommand("Update ticket set estado = 'A', aprobador_id = " + t.aprobador_id + " where codigo_atencion = '" + t.codigo_atencion + "'", conexion);
                        filas = query.ExecuteNonQuery();
                        if (filas > 0)
                        {

                            query = new SqlCommand("commit", conexion);
                            query.ExecuteNonQuery();
                            actualizado = true;
                        }
                        else
                        {
                            query = new SqlCommand("rollback", conexion);
                            query.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return actualizado;
        }

        public List<Tuple<string, int, int>> DatosReporte02(string fecha_ini, string fecha_fin)
        {
            List<Tuple<string, int, int>> datos = new List<Tuple<string, int, int>>();
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("select datos.fecha, sum(datos.solicitado) as solicitados,  sum(datos.asignado) as asignados ");
                    sql.Append("from ( ");
                    sql.Append("(select fecha_solicitado as fecha, count (*) as solicitado, 0 as asignado from ticket where estado = 'I' and fecha_solicitado >= '" +
                        fecha_ini + "' and fecha_solicitado <= '" + fecha_fin + "' group by fecha_solicitado) ");
                    sql.Append("union all ");
                    sql.Append("(select fecha_asignado as fecha, 0 as solicitado, count (*) as asignado from ticket where estado = 'A' and fecha_asignado >= '" +
                        fecha_ini + "' and fecha_asignado <= '" + fecha_fin + "' group by fecha_asignado) ");
                    sql.Append(") as datos ");
                    sql.Append("group by datos.fecha order by datos.fecha asc");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var fec = dr["fecha"].ToString();
                            fec = fec.Substring(0, 10);
                            var sol = Convert.ToInt32(dr["solicitados"].ToString());
                            var asig = Convert.ToInt32(dr["asignados"].ToString());

                            datos.Add(new Tuple<string, int, int>(fec, sol, asig));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return datos;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string DestinatarioPara(string codigo_atencion)
        {
            string codigo = string.Empty;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {
                    conexion.Open();

                    sql.Append("Select umr.usuario_id ");
                    sql.Append("from Ticket t, usuario_modulo_rol umr ");
                    sql.Append("where t.codigo_atencion = '" + codigo_atencion + "' and t.asignado_id = umr.id");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            codigo = dr["usuario_id"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return codigo;
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
					sql.Append("Select 1 from ticket where topico_id =");
					sql.Append(topico_id.ToString());
					sql.Append("");
				
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

		public List<Ticket> FindAll()
        {
            List<Ticket> t = new List<Ticket>(); ;
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {

                    conexion.Open();
                    var query = new SqlCommand("select * from ticket t inner join topico o on t.topico_id =o.id" +
                      " inner join usuario_modulo_rol umr on t.solicitante_id = umr.id " +
                     " where t.solicitante_id = umr.id order by t.fecha_solicitado desc", conexion);


                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var ticket = new Ticket();
                            var topico = new Topico();
                            var umr = new Usuario_Modulo_Rol();
                            ticket.codigo_atencion = dr["codigo_atencion"].ToString();
                            ticket.topico_id = Convert.ToInt32(dr["topico_id"].ToString());

                            ticket.importancia = Convert.ToInt16(dr["importancia"].ToString());
                            ticket.descripcion = dr["descripcion"].ToString();
                            ticket.estado = Convert.ToChar(dr["estado"].ToString());
                            ticket.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());

                            topico.topico = dr["topico"].ToString();
                            umr.id = Convert.ToInt32(dr["solicitante_id"].ToString());
                            umr.usuario_id = dr["usuario_id"].ToString();
                            ticket.Topico = topico;
                            ticket.Usuario_Modulo_Rol = umr;

                            t.Add(ticket);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return t;
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

		public Ticket FindId(string id)
		{
			var ticket = new Ticket();
			try
			{
				using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conn.Open();
                    //var query = new SqlCommand("select * from ticket t inner join topico o on t.topico_id =o.id" +
                    // " inner join usuario_modulo_rol umr on t.solicitante_id= umr.id " +
                    //" where t.codigo_atencion = @id", conn);
                    //query.Parameters.AddWithValue("@id", id);

                    
					var query = new SqlCommand("select o.id as topicoId ,umr.usuario_id as asignado, t.codigo_atencion,o.topico,(select u.codigo from ticket t  inner" +
				" join usuario_modulo_rol umr  on t.asignado_id = umr.id inner join usuario u on u.codigo = umr.usuario_id" +
				" where t.codigo_atencion = @id) as asignado, t.descripcion, t.estado, t.fecha_asignado, t.fecha_finalizado, t.fecha_solicitado" +
				" from ticket t inner join topico o on t.topico_id = o.id inner join usuario_modulo_rol umr on t.solicitante_id = umr.id INNER JOIN USUARIO u on u.codigo = umr.usuario_id" +
				" where t.codigo_atencion = @id", conn);

					
					query.Parameters.AddWithValue("@id", id);
                    using (var dr = query.ExecuteReader())
					{
						while (dr.Read())
						{

							//topico.id = Convert.ToInt32(dr["id"].ToString());
							//topico.topico = dr["topico"].ToString();
							//topico.fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]);
							//topico.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]);
							//topico.fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]);
							//topico.usuario_modificacion = dr["usuario_modificacion"].ToString();
							//topico.estado = Convert.ToChar(dr["estado"]);
							//topico.Departamento.id = Convert.ToInt32(dr["departamento_id"].ToString());



							var topico = new Topico();
                            var umr = new Usuario_Modulo_Rol();
                            ticket.codigo_atencion = dr["codigo_atencion"].ToString();
		
							umr.usuario_id = dr["asignado"].ToString();
							ticket.descripcion = dr["descripcion"].ToString();
                            ticket.estado = Convert.ToChar(dr["estado"].ToString());
                            ticket.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());



							ticket.topico_id = Convert.ToInt32(dr["topicoId"].ToString());
							topico.topico = dr["topico"].ToString();

                            ticket.Topico = topico;
                            ticket.Usuario_Modulo_Rol = umr;

							ticket.fecha_asignado = Convert.ToDateTime(dr["fecha_asignado"].ToString());
							ticket.fecha_finalizado = Convert.ToDateTime(dr["fecha_finalizado"].ToString());

						}
					}
				}
			}
			catch (Exception)
			{

				//throw;
			}
			return ticket;
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

                    sql.Append("Insert Into Ticket (codigo_atencion, solicitante_id, topico_id, importancia, descripcion, estado, fecha_solicitado, fecha_limite )");
                    sql.Append("Values ('" + t.codigo_atencion + "', " + t.solicitante_id + ", " + t.topico_id + ", " + t.importancia + ", '" + t.descripcion);
                    sql.Append("', '" + t.estado + "', '");
                    sql.Append(DateTime.Now.Year + " - " + DateTime.Now.Month + "-" + DateTime.Now.Day + "', ");
                    sql.Append("'" + t.fecha_limite.Year + " - " + t.fecha_limite.Month + "-" + t.fecha_limite.Day + "')");
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
					var query = new SqlCommand("select * from ticket t inner join topico o on t.topico_id =o.id" +
                      " inner join usuario_modulo_rol umr on t.asignado_id= umr.id " +
                     " where t.asignado_id = umr.id and umr.usuario_id = '" + codigo_trabajador + "' and t.estado in ('A', 'P') order by t.fecha_solicitado desc", conexion);


                    using (var dr = query.ExecuteReader())
                    {
                        t = new List<Ticket>();
						while (dr.Read())
                        {
                            var ticket = new Ticket();
                            var topico = new Topico();
                            var umr = new Usuario_Modulo_Rol();
                            ticket.codigo_atencion = dr["codigo_atencion"].ToString();
                            ticket.topico_id = Convert.ToInt32(dr["topico_id"].ToString());

                            ticket.importancia = Convert.ToInt16(dr["importancia"].ToString());
                            ticket.descripcion = dr["descripcion"].ToString();
                            ticket.estado = Convert.ToChar(dr["estado"].ToString());
                            ticket.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());

                            topico.topico = dr["topico"].ToString();
                            umr.id = Convert.ToInt32(dr["solicitante_id"].ToString());
                            umr.usuario_id = dr["usuario_id"].ToString();
                            ticket.Topico = topico;
                            ticket.Usuario_Modulo_Rol = umr;
                            ticket.fecha_limite = Convert.ToDateTime(dr["fecha_limite"].ToString());
                            ticket.fecha_asignado = Convert.ToDateTime(dr["fecha_asignado"].ToString());
                            t.Add(ticket);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return t;
        }

        public List<Ticket> TicketsSolicitados(string codigo_cliente)
        {
			//List<Ticket> t = null;
			//StringBuilder sql = new StringBuilder();
			var ticketsolicitados = new List<Ticket>();
			try
			{
				using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conexion.Open();
					var query = new SqlCommand("select * from ticket t inner join topico o on t.topico_id =o.id" +
					  " inner join usuario_modulo_rol umr on t.solicitante_id= umr.id " +
					 " where t.solicitante_id = umr.id and umr.usuario_id = '" + codigo_cliente + "' order by t.fecha_solicitado desc", conexion);
					

					using (var dr = query.ExecuteReader())
					{
						//t = new List<Ticket>();
						while (dr.Read())
						{
							var ticket = new Ticket();
							var topico = new Topico();
							var umr = new Usuario_Modulo_Rol();
							/*	Ticket ticket = new Ticket();*/
							ticket.codigo_atencion = dr["codigo_atencion"].ToString();
							ticket.topico_id = Convert.ToInt32(dr["topico_id"].ToString());
							//if (dr["asignado_id"] != null)
							//{
							//    ticket.asignado_id = Convert.ToInt32(dr["asignado_id"].ToString());
							//}
							//else
							//{

							//}
							ticket.importancia = Convert.ToInt16(dr["importancia"].ToString());
							ticket.descripcion = dr["descripcion"].ToString();
							ticket.estado = Convert.ToChar(dr["estado"].ToString());
							ticket.fecha_solicitado = Convert.ToDateTime(dr["fecha_solicitado"].ToString());

							topico.topico= dr["topico"].ToString();
							umr.id= Convert.ToInt32(dr["solicitante_id"].ToString());
							umr.usuario_id= dr["usuario_id"].ToString();
							ticket.Topico = topico;
							ticket.Usuario_Modulo_Rol = umr;

							
							//if (dr["fecha_asignado"] != null) ticket.fecha_asignado = Convert.ToDateTime(dr["fecha_asignado"].ToString());
							//if (dr["fecha_finalizado"] != null) ticket.fecha_finalizado = Convert.ToDateTime(dr["fecha_finalizado"].ToString());
							//if (dr["aprobador_id"] != null) ticket.aprobador_id = Convert.ToInt32(dr["aprobador_id"].ToString());

							ticketsolicitados.Add(ticket);
						}
					}
				}
			}
			catch (Exception e)
			{
				var a = e.Message;
			}
			return ticketsolicitados;
		}

        public string TicketsXtrabajadorXestado(string codigo, string estado)
        {
            string resultado = "0";
            StringBuilder sql = new StringBuilder();
            try
            {
                using (var conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
                {

                    conexion.Open();

                    sql.Append("Select count(*) cantidad from ticket t, usuario_modulo_rol urm ");
                    sql.Append("where t.asignado_id = urm.id and t.estado = '");
                    sql.Append(estado);
                    sql.Append("' and urm.usuario_id = '");
                    sql.Append(codigo);
                    sql.Append("'");

                    var query = new SqlCommand(sql.ToString(), conexion);

                    using (var dr = query.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            resultado = dr["cantidad"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            return resultado;
        }

		public List<Ticket> Topico_x_tickets()
		{
			var tickets = new List<Ticket>();
			try
			{
				using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					con.Open();

					var query = new SqlCommand("select t.topico,isnull(count(ti.codigo_atencion) ,0)tickets from departamento d inner join topico t on t.departamento_id=d.id inner join ticket ti on ti.topico_id = t.id group by t.topico", con);
					using (var dr = query.ExecuteReader())
					{
						while (dr.Read())
						{
							var topico = new Topico();
							var departamento = new Departamento();
							var ticket = new Ticket();
							ticket.codigo_atencion = dr["tickets"].ToString();




							topico.topico = dr["topico"].ToString();
							ticket.Topico = topico;





							tickets.Add(ticket);



						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return tickets;

		}

		public bool Update(Ticket t)
        {

			bool seActualizo = false;
			try
			{
				using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApp_Ticket"].ToString()))
				{
					conn.Open();
                    var update_finalizar = "";
                    if (t.estado == 'F')
                    {
                        update_finalizar = ", fecha_finalizado = '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "'";
                    }
					var query = new SqlCommand("UPDATE Ticket SET  topico_id=@topico_id, estado = @estado, fecha_limite = @fecha_limite "+ update_finalizar +" WHERE codigo_atencion=@codigo_atencion", conn);

					query.Parameters.AddWithValue("@codigo_atencion", t.codigo_atencion);
			
					query.Parameters.AddWithValue("@topico_id", t.topico_id);
                    query.Parameters.AddWithValue("@estado", t.estado);
                    query.Parameters.AddWithValue("@fecha_limite", t.fecha_limite);

                    query.ExecuteNonQuery();
					seActualizo = true;
				}
			}
			catch (Exception)
			{

				//throw;
			}
			return seActualizo;
		}
    }
}
