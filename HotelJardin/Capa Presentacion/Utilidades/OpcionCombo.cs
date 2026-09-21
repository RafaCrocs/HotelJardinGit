using System;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Elemento para ComboBox: texto visible + valor real.
    /// Se agrego ToString() para que el combo muestre el texto correcto incluso
    /// si por alguna razon no se alcanza a asignar DisplayMember.
    /// </summary>
    public class OpcionCombo
    {
        public string Texto { get; set; }
        public object Valor { get; set; }

        public override string ToString()
        {
            return Texto ?? string.Empty;
        }
    }
}
