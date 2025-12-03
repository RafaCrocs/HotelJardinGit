using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Venta
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from VENTA");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());


                }
                catch (Exception ex)
                {
                    idcorrelativo = 0;

                }
            }
            return idcorrelativo;
        }

        public bool RestarCantidad(int IdInventario, int Cantidad)
        {
            bool resultado = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update INVENTARIO set Cantidad = Cantidad - @Cantidad where IdInventario = @IdInventario");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@IdInventario", IdInventario);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    resultado = false;  
                }
            }
            return resultado;
        }

        public bool SumarCantidad(int IdInventario, int Cantidad)
        {
            bool resultado = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update INVENTARIO set Cantidad = Cantidad + @Cantidad where IdInventario = @IdInventario");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@IdInventario", IdInventario);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
            }
            return resultado;
        }


        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                        cmd.Parameters.AddWithValue("IdCliente", obj.oCliente.IdCliente); // debe estar poblado
                        cmd.Parameters.AddWithValue("NombreCliente", obj.NombreCliente);
                        cmd.Parameters.AddWithValue("ModoPago", obj.ModoPago);
                        cmd.Parameters.AddWithValue("NumeroFact", obj.NumeroFact);
                        cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                        cmd.Parameters.AddWithValue("FechaRegistro", obj.FechaRegistro);

                        // Agregar DataTable como parámetro estructurado (TVP)
                        var p = cmd.Parameters.AddWithValue("@DetalleVenta", DetalleVenta);
                        p.SqlDbType = SqlDbType.Structured;
                        p.TypeName = "dbo.EDetalle_Venta"; // nombre del UDT en la BD

                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        oConexion.Open();
                        cmd.ExecuteNonQuery();

                        resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
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


