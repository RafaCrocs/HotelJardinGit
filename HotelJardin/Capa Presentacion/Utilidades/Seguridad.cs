using System;
using System.Security.Cryptography;
using System.Text;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Comparacion de claves.
    ///
    /// LIMITE IMPORTANTE: la base de datos actual guarda la clave en texto plano y
    /// esta capa no puede cambiar eso por si sola. Lo que si se puede hacer desde aqui
    /// es dejar el camino de migracion listo:
    ///
    ///   - Coincide() detecta automaticamente si el valor guardado es un hash SHA-256
    ///     (64 caracteres hexadecimales) o una clave en texto plano heredada.
    ///   - Cuando quieran migrar, basta con guardar Hash(clave) al registrar y editar
    ///     usuarios (frmUsuarios ya llama a Seguridad.ParaGuardar) y correr un UPDATE
    ///     una sola vez sobre las claves existentes. El login sigue funcionando durante
    ///     la transicion porque acepta los dos formatos.
    ///
    /// Para que la migracion quede completa hay que cambiar ademas CD_Usuario.Listar
    /// por un SP_ValidarUsuario que reciba usuario + hash y devuelva un solo registro,
    /// porque hoy el login descarga la tabla entera. Eso vive en CapaDatos.
    /// </summary>
    public static class Seguridad
    {
        /// <summary>Poner en true cuando ya se hayan migrado las claves de la base.</summary>
        public const bool GuardarClavesComoHash = false;

        public static string Hash(string texto)
        {
            if (texto == null) texto = string.Empty;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>Valor que debe persistirse cuando se registra o edita un usuario.</summary>
        public static string ParaGuardar(string claveEnClaro)
        {
            return GuardarClavesComoHash ? Hash(claveEnClaro) : claveEnClaro;
        }

        /// <summary>
        /// Compara la clave escrita contra la almacenada, sirva esta en hash o en claro.
        /// La comparacion es de tiempo fijo para no filtrar informacion por el tiempo
        /// que tarda en responder.
        /// </summary>
        public static bool Coincide(string claveIngresada, string claveAlmacenada)
        {
            if (claveIngresada == null) claveIngresada = string.Empty;
            if (claveAlmacenada == null) claveAlmacenada = string.Empty;

            string aComparar = EsHash(claveAlmacenada) ? Hash(claveIngresada) : claveIngresada;
            return ComparacionSegura(aComparar, claveAlmacenada);
        }

        private static bool EsHash(string valor)
        {
            if (valor == null || valor.Length != 64) return false;
            foreach (char c in valor)
            {
                bool hex = (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
                if (!hex) return false;
            }
            return true;
        }

        private static bool ComparacionSegura(string a, string b)
        {
            if (a.Length != b.Length) return false;
            int diferencia = 0;
            for (int i = 0; i < a.Length; i++) diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }
    }
}
