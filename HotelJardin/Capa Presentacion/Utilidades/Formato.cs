using System;
using System.Globalization;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Punto unico de formato y conversion de numeros de toda la capa de presentacion.
    ///
    /// Por que existe: el proyecto original mezclaba CultureInfo.InvariantCulture (punto decimal)
    /// en frmVenta con Convert.ToDecimal() (cultura del sistema, coma decimal en es-CR/es-ES)
    /// en frmInventario y frmClientes. En una maquina en espanol eso convertia "1500.50"
    /// en 150050. Aqui se acepta cualquiera de los dos separadores al leer y se escribe
    /// siempre con el mismo formato, de modo que no importa como este configurada la maquina.
    /// </summary>
    public static class Formato
    {
        /// <summary>Cultura usada para ESCRIBIR numeros en pantalla, grids y archivos.</summary>
        public static readonly CultureInfo Cultura = CultureInfo.InvariantCulture;

        #region Escritura

        public static string Moneda(decimal valor)
        {
            return valor.ToString("0.00", Cultura);
        }

        public static string Entero(int valor)
        {
            return valor.ToString(Cultura);
        }

        #endregion

        #region Lectura tolerante

        /// <summary>
        /// Convierte texto a decimal aceptando coma o punto como separador decimal.
        /// Devuelve false si el texto no representa un numero.
        /// </summary>
        public static bool TryMoneda(string texto, out decimal valor)
        {
            valor = 0m;
            if (string.IsNullOrWhiteSpace(texto)) return false;

            string limpio = Normalizar(texto);

            return decimal.TryParse(
                limpio,
                NumberStyles.Number | NumberStyles.AllowLeadingSign,
                Cultura,
                out valor);
        }

        /// <summary>Convierte texto a decimal; si no se puede, devuelve porDefecto.</summary>
        public static decimal Moneda(string texto, decimal porDefecto = 0m)
        {
            decimal valor;
            return TryMoneda(texto, out valor) ? valor : porDefecto;
        }

        public static bool TryEntero(string texto, out int valor)
        {
            valor = 0;
            if (string.IsNullOrWhiteSpace(texto)) return false;
            return int.TryParse(texto.Trim(), NumberStyles.Integer, Cultura, out valor);
        }

        /// <summary>
        /// Convierte texto a entero; si no se puede, devuelve porDefecto.
        /// Reemplaza los Convert.ToInt32(txtId.Text) del codigo original, que lanzaban
        /// FormatException y cerraban la aplicacion cuando la caja estaba vacia.
        /// </summary>
        public static int Entero(string texto, int porDefecto = 0)
        {
            int valor;
            return TryEntero(texto, out valor) ? valor : porDefecto;
        }

        /// <summary>Lee el valor de una celda de grid como decimal, tolerando nulos.</summary>
        public static decimal MonedaDeCelda(object valorCelda, decimal porDefecto = 0m)
        {
            if (valorCelda == null || valorCelda == DBNull.Value) return porDefecto;
            if (valorCelda is decimal) return (decimal)valorCelda;
            if (valorCelda is double) return Convert.ToDecimal((double)valorCelda);
            if (valorCelda is int) return Convert.ToDecimal((int)valorCelda);
            return Moneda(valorCelda.ToString(), porDefecto);
        }

        public static int EnteroDeCelda(object valorCelda, int porDefecto = 0)
        {
            if (valorCelda == null || valorCelda == DBNull.Value) return porDefecto;
            if (valorCelda is int) return (int)valorCelda;
            return Entero(valorCelda.ToString(), porDefecto);
        }

        public static string TextoDeCelda(object valorCelda)
        {
            if (valorCelda == null || valorCelda == DBNull.Value) return string.Empty;
            return valorCelda.ToString();
        }

        #endregion

        /// <summary>
        /// Deja el texto en un formato que InvariantCulture pueda parsear:
        /// quita espacios y simbolos, y decide cual separador es el decimal.
        /// "1.234,56" -> "1234.56"   |   "1,234.56" -> "1234.56"   |   "1234,56" -> "1234.56"
        /// </summary>
        private static string Normalizar(string texto)
        {
            string limpio = texto.Trim()
                                 .Replace(" ", string.Empty)
                                 .Replace("$", string.Empty)
                                 .Replace("\u20A1", string.Empty); // simbolo colon

            int ultimaComa = limpio.LastIndexOf(',');
            int ultimoPunto = limpio.LastIndexOf('.');

            if (ultimaComa >= 0 && ultimoPunto >= 0)
            {
                // Vienen los dos: el que aparece de ultimo es el separador decimal,
                // el otro es separador de miles y se elimina.
                if (ultimaComa > ultimoPunto)
                    limpio = limpio.Replace(".", string.Empty).Replace(',', '.');
                else
                    limpio = limpio.Replace(",", string.Empty);
            }
            else if (ultimaComa >= 0)
            {
                // Solo coma: se asume decimal (es el caso tipico en es-CR).
                limpio = limpio.Replace(',', '.');
            }

            return limpio;
        }
    }
}
