using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objcd_Usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objcd_Usuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if(obj.NombreCompleto == "")
            {
                Mensaje = "El nombre del usuario no puede estar vacio\n";
            }
            if (obj.Clave == "")
            {
                Mensaje = "La clave del usuario no puede estar vacia\n";
            }
            if (obj.oRol == null || obj.oRol.IdRol == 0)
            {
                Mensaje = "Debe seleccionar un rol para el usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else 
            {
                return objcd_Usuario.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje = "El nombre del usuario no puede estar vacio\n";
            }
            if (obj.Clave == "")
            {
                Mensaje = "La clave del usuario no puede estar vacia\n";
            }
            if (obj.oRol == null || obj.oRol.IdRol == 0)
            {
                Mensaje = "Debe seleccionar un rol para el usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Usuario.Editar(obj, out Mensaje);
            }
        }   

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objcd_Usuario.Eliminar(obj, out Mensaje);
        }

    }
}
