using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Utilidades de DataGridView compartidas.
    ///
    /// El bloque de 30 lineas que dibujaba el icono de una columna boton estaba
    /// copiado y pegado en frmVenta (dos veces), frmUsuarios, frmClientes y
    /// frmInventario. Aqui vive una sola vez.
    ///
    /// El estilo del grid tambien se aplicaba dentro del metodo que agregaba un
    /// producto, es decir se reasignaba fuente y colores en cada producto escaneado.
    /// Ahora se aplica una sola vez en el Load del formulario.
    /// </summary>
    public static class GridHelper
    {
        /// <summary>Aplica el estilo visual del sistema. Llamar una vez, en el Load.</summary>
        public static void AplicarEstilo(DataGridView grid)
        {
            if (grid == null) return;

            grid.Font = new Font("Segoe UI", 12);
            grid.RowsDefaultCellStyle.BackColor = SystemColors.InactiveBorder;
            grid.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.InactiveCaption;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
        }

        /// <summary>
        /// Prepara un grid que se va a llenar con DataSource.
        /// AutoGenerateColumns queda en false: si se deja en true (el valor por defecto),
        /// al asignar la lista el grid AGREGA columnas automaticas ademas de las que ya
        /// estan definidas en el Designer, y aparecen columnas duplicadas.
        /// </summary>
        public static void PrepararParaBinding(DataGridView grid)
        {
            if (grid == null) return;
            AplicarEstilo(grid);
            grid.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Dibuja un icono centrado dentro de la celda de una columna boton.
        /// Devuelve true si pinto la celda (para que el llamador marque e.Handled).
        /// </summary>
        public static bool PintarIcono(DataGridView grid,
                                       DataGridViewCellPaintingEventArgs e,
                                       string nombreColumna,
                                       Bitmap icono)
        {
            if (grid == null || e == null) return false;
            if (e.RowIndex < 0) return false;
            if (!grid.Columns.Contains(nombreColumna)) return false;

            DataGridViewColumn columna = grid.Columns[nombreColumna];
            if (e.ColumnIndex != columna.Index) return false;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (icono != null)
            {
                int ancho = icono.Width;
                int alto = icono.Height;

                int anchoMax = e.CellBounds.Width - 4;
                int altoMax = e.CellBounds.Height - 4;

                if (anchoMax > 0 && altoMax > 0 && (ancho > anchoMax || alto > altoMax))
                {
                    float escala = Math.Min((float)anchoMax / ancho, (float)altoMax / alto);
                    ancho = (int)(ancho * escala);
                    alto = (int)(alto * escala);
                }

                int x = e.CellBounds.Left + (e.CellBounds.Width - ancho) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - alto) / 2;
                e.Graphics.DrawImage(icono, new Rectangle(x, y, ancho, alto));
            }

            return true;
        }

        /// <summary>
        /// Indica si el click cayo sobre una columna concreta y sobre una fila valida.
        /// Evita el IndexOutOfRange de acceder a Columns[e.ColumnIndex] cuando el
        /// indice es -1 (click sobre el encabezado).
        /// </summary>
        public static bool ClickEnColumna(DataGridView grid,
                                          DataGridViewCellEventArgs e,
                                          string nombreColumna)
        {
            if (grid == null || e == null) return false;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return false;
            if (e.ColumnIndex >= grid.Columns.Count) return false;
            return grid.Columns[e.ColumnIndex].Name == nombreColumna;
        }
    }
}
