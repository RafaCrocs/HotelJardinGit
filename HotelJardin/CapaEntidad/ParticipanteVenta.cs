using System;

namespace CapaEntidad
{
    /// <summary>
    /// Una persona que aporta presupuesto a una factura compartida.
    ///
    /// Varias personas (pareja, trio, familia) pueden pagar una misma factura
    /// con sus presupuestos. Cada una llega a la caja y da su codigo; el grupo
    /// se arma en el momento y no queda guardado como vinculo permanente.
    ///
    /// Asignado lo calcula de forma definitiva el procedimiento almacenado
    /// usp_RegistrarVenta, con los saldos reales y las filas bloqueadas dentro
    /// de la transaccion. El valor que se llena aqui es solo la vista previa
    /// que ve el cajero mientras arma la venta.
    /// </summary>
    public class ParticipanteVenta
    {
        /// <summary>Posicion en que se escaneo. El de Orden 1 es el principal.</summary>
        public int Orden { get; set; }

        public int CodigoCliente { get; set; }

        public string Nombre { get; set; }

        /// <summary>Saldo de presupuesto con el que llego a la caja.</summary>
        public decimal SaldoAntes { get; set; }

        /// <summary>Cuanto de esta factura le corresponde pagar.</summary>
        public decimal Asignado { get; set; }

        public decimal SaldoDespues
        {
            get { return SaldoAntes - Asignado; }
        }
    }
}
