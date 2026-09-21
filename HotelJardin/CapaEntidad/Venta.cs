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

        /// <summary>
        /// Personas que comparten esta factura y aportan su presupuesto.
        ///
        /// Si queda vacia, la venta se registra a nombre de oCliente y el
        /// comportamiento es identico al de antes: el procedimiento almacenado
        /// acepta las dos formas.
        ///
        /// El reparto definitivo lo hace usp_RegistrarVenta con los saldos
        /// reales; lo que viaja desde aqui son los codigos y su orden.
        /// </summary>
        public List<ParticipanteVenta> oParticipantes { get; set; } = new List<ParticipanteVenta>();

    }
}
