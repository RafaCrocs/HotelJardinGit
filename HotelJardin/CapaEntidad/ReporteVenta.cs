using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteVenta
    {
        public string FechaRegistro { get; set; }
        public int IdUsuario { get; set; }
        public int IdVenta { get; set; }
        public string NombreCliente { get; set; }
        public string ModoPago { get; set; }
        public decimal MontoTotal { get; set; }



    }
}
