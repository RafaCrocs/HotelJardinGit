using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Cliente
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select CodigoCliente, Nombres, Apellidos, PresupuestoInicial, Presupuesto from Clientes");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader drd = cmd.ExecuteReader())
                    {
                        while (drd.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                CodigoCliente = Convert.ToInt32(drd["CodigoCliente"]),
                                Nombre = drd["Nombres"].ToString(),
                                Apellido = drd["Apellidos"].ToString(),
                                PresupuestoInicial = drd["PresupuestoInicial"] != DBNull.Value ? Convert.ToDecimal(drd["PresupuestoInicial"]) : 0,
                                Presupuesto = drd["Presupuesto"] != DBNull.Value ? Convert.ToDecimal(drd["Presupuesto"]) : 0
                            });
                        }
                    }
                }
                catch (Exception ex) { lista = new List<Cliente>(); }
            }
            return lista;
        }

        public bool Registrar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena)) {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarCliente", oConexion);
                    cmd.Parameters.AddWithValue("@CodigoCliente", obj.CodigoCliente);
                    cmd.Parameters.AddWithValue("@Nombres", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", obj.Apellido);
                    cmd.Parameters.AddWithValue("@PresupuestoInicial", obj.PresupuestoInicial);
                    cmd.Parameters.AddWithValue("@Presupuesto", obj.Presupuesto);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            } catch (Exception ex) { resultado = false; Mensaje = ex.Message; }
            return resultado;
        }

        //Editar Cliente
        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarCliente", oConexion);
                    cmd.Parameters.AddWithValue("CodigoCliente", obj.CodigoCliente);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellido);
                    cmd.Parameters.AddWithValue("PresupuestoInicial", obj.PresupuestoInicial);
                    cmd.Parameters.AddWithValue("Presupuesto", obj.Presupuesto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


        //Eliminar Cliente
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("DELETE from Clientes where CodigoCliente = @CodigoCliente", oConexion);
                    cmd.Parameters.AddWithValue("@CodigoCliente", obj.CodigoCliente);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    resultado = cmd.ExecuteNonQuery() > 0 ? true:false;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;

        }

        //Actualizar Presupuesto Cliente
        public bool ActualizarPresupuesto(int idCliente, decimal nuevoPresupuesto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ActualizarPresupuestoCliente", oConexion);
                    cmd.Parameters.AddWithValue("@CodigoCliente", idCliente);
                    cmd.Parameters.AddWithValue("@NuevoPresupuesto", nuevoPresupuesto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

    }
}
