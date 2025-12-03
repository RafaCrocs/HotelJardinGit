using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int Idventa { get; set; }
        public Venta oVenta { get; set; }
        public Inventario oCodigo { get; set; }
        public double PrecioVenta { get; set; }
        public double Subtotal {  get; set; }
        public string FechaRegistro { get; set; }
    }
}
