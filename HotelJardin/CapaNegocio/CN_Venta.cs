using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_Venta = new CD_Venta();

        public bool RestarCantidad(int IdInventario, int Cantidad)
        {
            return objcd_Venta.RestarCantidad(IdInventario, Cantidad);
        }

        public bool SumarCantidad(int IdInventario, int Cantidad)
        {
            return objcd_Venta.SumarCantidad(IdInventario, Cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_Venta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_Venta.Registrar(obj, DetalleVenta, out Mensaje);
        }


    }
}
