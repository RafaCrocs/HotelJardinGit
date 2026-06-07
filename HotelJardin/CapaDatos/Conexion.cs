using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        public static string cadena = "server=30.0.0.205, 1433; database=DbSistemaVentas7; integrated security=false; user=sa; password=J4rd1n@2030";

        public bool ModificarCadenaConexion(string ipCadena)
        {
            bool resultado = false;
            cadena = "server=" + ipCadena + "; database=DbSistemaVentas7; integrated security=false; user=sa; password=J4rd1n@2030";
            resultado = true;
            return resultado;
        }
    }

}
