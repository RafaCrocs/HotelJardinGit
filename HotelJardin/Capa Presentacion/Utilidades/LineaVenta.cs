using System;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Una linea del detalle de venta, en memoria.
    ///
    /// En el proyecto original la verdad de la venta vivia en las celdas del
    /// DataGridView: el precio unitario se RECONSTRUIA dividiendo subtotal entre
    /// cantidad cada vez que se pulsaba + o -, lo cual acumula error de redondeo
    /// y deja de funcionar si la cantidad llega a cero. Aqui el precio unitario es
    /// un dato que se guarda tal cual vino de la base, y el subtotal se calcula.
    /// </summary>
    public class LineaVenta
    {
        public int IdInventario { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }

        /// <summary>Existencia que tenia el articulo cuando se cargo al carrito.</summary>
        public int StockDisponible { get; set; }

        public decimal SubTotal
        {
            get { return Math.Round(PrecioUnitario * Cantidad, 2, MidpointRounding.AwayFromZero); }
        }
    }
}
