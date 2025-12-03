using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Rol
    {
            public List<Rol> Listar()
            {
                List<Rol> lista = new List<Rol>();

                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    try
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("select IdRol, Descripion from ROL");



                        SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                        cmd.CommandType = CommandType.Text;

                        oConexion.Open();

                        using (SqlDataReader drd = cmd.ExecuteReader())
                        {

                            while (drd.Read())
                            {
                                lista.Add(new Rol()
                                {
                                    IdRol = Convert.ToInt32(drd["IdRol"]),
                                    Descripcion = drd["Descripion"].ToString()

                                });
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        lista = new List<Rol>();
                    }

                }
                return lista;
            }
        }



    
}
