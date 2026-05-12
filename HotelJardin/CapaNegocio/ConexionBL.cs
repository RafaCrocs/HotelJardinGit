using CapaDatos;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ConexionBL
    {
        public bool ModificarConexion(string nuevaCadena)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["Cadena_conexion"].ConnectionString = "server=" + nuevaCadena + "; database=DBSistema_Ventas; integrated security=true";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar la conexión: " + ex.Message);
                return false;
            }
        }
    }
}
