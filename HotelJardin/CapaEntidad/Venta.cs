using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Usuario oUsuario { get; set; }
        public Cliente oCliente { get; set; }
        public Inventario oCodigo { get; set; }
        public string NombreCliente { get; set; }
        public string ModoPago { get; set; }
        public int NumeroFact { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; } = new List<DetalleVenta>();

    }
}
