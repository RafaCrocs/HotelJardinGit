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
        private Conexion conexionDAL = new Conexion();

        public bool ModificarCadenaConexion(string ipCadena)
        {
            return conexionDAL.ModificarCadenaConexion(ipCadena);
        }
    }
}
