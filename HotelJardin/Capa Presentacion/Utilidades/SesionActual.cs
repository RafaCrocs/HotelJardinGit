using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidad;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Usuario conectado y sus permisos.
    ///
    /// Reemplaza los campos "private static Usuario UsuarioActual" que vivian dentro
    /// del formulario Inicio. Tener estado estatico colgando de un Form hace que el
    /// dato sobreviva al cierre de la ventana y que dos instancias se pisen entre si.
    /// </summary>
    public static class SesionActual
    {
        private static readonly List<string> _menus = new List<string>();

        public static Usuario Usuario { get; private set; }

        public static bool HayUsuario
        {
            get { return Usuario != null; }
        }

        public static IEnumerable<string> MenusPermitidos
        {
            get { return _menus.AsReadOnly(); }
        }

        public static void Iniciar(Usuario usuario, IEnumerable<Permiso> permisos)
        {
            Usuario = usuario;
            _menus.Clear();

            if (permisos != null)
            {
                _menus.AddRange(permisos
                    .Where(p => p != null && !string.IsNullOrWhiteSpace(p.NombreMenu))
                    .Select(p => p.NombreMenu.Trim()));
            }
        }

        public static bool TienePermiso(string nombreMenu)
        {
            if (string.IsNullOrWhiteSpace(nombreMenu)) return false;
            return _menus.Any(m => string.Equals(m, nombreMenu, StringComparison.OrdinalIgnoreCase));
        }

        public static void Cerrar()
        {
            Usuario = null;
            _menus.Clear();
        }
    }
}
