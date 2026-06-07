using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal PresupuestoInicial { get; set; }
        public decimal Presupuesto { get; set; }
    }
}
