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
    public class CD_Reporte
    {
        public List<ReporteVenta> Venta(string FechaInicio, string FechaFin)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_ReporteVenta", oConexion);

                    cmd.Parameters.AddWithValue("FechaInicio", FechaInicio);
                    cmd.Parameters.AddWithValue("FechaFin", FechaFin);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                ModoPago = dr["ModoPago"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();
                }
                return lista;
            }
        }


    }
}
