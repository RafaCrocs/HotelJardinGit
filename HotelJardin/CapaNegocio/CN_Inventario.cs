using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Inventario
    {
        private CD_Inventario objcd_Inventario = new CD_Inventario();

        public List<Inventario> Listar()
        {
            return objcd_Inventario.Listar();
        }

        public int Registrar(Inventario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Codigo == null)
            {
                Mensaje = "El Codigo del Articulo no puede estar vacio\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje = "La descripcion no puede estar vacia\n";
            }
            if (obj.Precio == null)
            {
                Mensaje = "Debe seleccionar un precio\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Inventario.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Inventario obj, out string Mensaje)
        {

            Mensaje = string.Empty;
            if (obj.Codigo == null)
            {
                Mensaje = "El Codigo del Articulo no puede estar vacio\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje = "La descripcion no puede estar vacia\n";
            }
            if (obj.Precio == null)
            {
                Mensaje = "Debe seleccionar un precio\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Inventario.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Inventario obj, out string Mensaje)
        {
            return objcd_Inventario.Eliminar(obj, out Mensaje);
        }


    }
}
