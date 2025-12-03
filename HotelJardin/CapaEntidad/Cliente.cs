using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int CodigoCliente { get; set; }
        public string NombreCompleto { get; set; }
        public Decimal Presupuesto { get; set; }
        public string Correo { get; set; }
    }
}
