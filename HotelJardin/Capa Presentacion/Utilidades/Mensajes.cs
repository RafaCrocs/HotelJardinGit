using System.Windows.Forms;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Centraliza los MessageBox para que todos los formularios avisen igual.
    /// En el codigo original habia titulos como "wuamp wuamp" y llamadas con los
    /// argumentos invertidos: MessageBox.Show("Datos Logicos", ex.Message) ponia
    /// el mensaje de la excepcion como TITULO de la ventana.
    /// </summary>
    public static class Mensajes
    {
        private const string TITULO = "Hotel Jardin";

        public static void Info(string mensaje)
        {
            MessageBox.Show(mensaje, TITULO, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, TITULO, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>Error tecnico: muestra un texto entendible y el detalle debajo.</summary>
        public static void Error(string mensaje, System.Exception ex)
        {
            string detalle = ex == null ? string.Empty : "\n\nDetalle tecnico:\n" + ex.Message;
            MessageBox.Show(mensaje + detalle, TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string pregunta)
        {
            return MessageBox.Show(pregunta, TITULO, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                   == DialogResult.Yes;
        }
    }
}
