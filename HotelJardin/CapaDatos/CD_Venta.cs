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
                        cmd.Parameters.AddWithValue("CodigoCliente", obj.oCliente.CodigoCliente);
                        cmd.Parameters.AddWithValue("NombreCliente", obj.NombreCliente);
                        cmd.Parameters.AddWithValue("ModoPago", obj.ModoPago);
                        cmd.Parameters.AddWithValue("NumeroFact", obj.NumeroFact);
                        cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                        cmd.Parameters.AddWithValue("FechaRegistro", obj.FechaRegistro);

                        // Agregar DataTable como parámetro estructurado (TVP)
                        var p = cmd.Parameters.AddWithValue("@DetalleVenta", DetalleVenta);
                        p.SqlDbType = SqlDbType.Structured;
                        p.TypeName = "dbo.EDetalle_Venta2"; // nombre del UDT en la BD

                        // Participantes de la factura compartida.
                        // Solo viajan los codigos y su orden: el reparto del
                        // presupuesto lo calcula usp_RegistrarVenta con los
                        // saldos reales y las filas bloqueadas, no la aplicacion.
                        var pClientes = cmd.Parameters.AddWithValue("@Participantes",
                                            ConstruirTablaParticipantes(obj));
                        pClientes.SqlDbType = SqlDbType.Structured;
                        pClientes.TypeName = "dbo.EClientes_Venta";

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

    
        /// <summary>
        /// Arma la tabla de participantes que espera el parametro estructurado
        /// dbo.EClientes_Venta.
        ///
        /// Si la venta no trae participantes (por ejemplo, una llamada vieja de
        /// un solo cliente), se envia el cliente de la cabecera. El
        /// procedimiento almacenado acepta las dos formas.
        /// </summary>
        private DataTable ConstruirTablaParticipantes(Venta obj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CodigoCliente", typeof(int));
            dt.Columns.Add("Orden", typeof(int));

            if (obj.oParticipantes != null && obj.oParticipantes.Count > 0)
            {
                int orden = 1;
                foreach (ParticipanteVenta p in obj.oParticipantes)
                {
                    if (p == null || p.CodigoCliente <= 0) continue;
                    dt.Rows.Add(p.CodigoCliente, p.Orden > 0 ? p.Orden : orden);
                    orden++;
                }
            }

            if (dt.Rows.Count == 0 && obj.oCliente != null && obj.oCliente.CodigoCliente > 0)
            {
                dt.Rows.Add(obj.oCliente.CodigoCliente, 1);
            }

            return dt;
        }
}
}


