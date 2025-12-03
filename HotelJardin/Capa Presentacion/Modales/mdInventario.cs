using Capa_Presentacion.Utilidades;
using CapaEntidad;
using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion.Modales
{
    public partial class mdInventario : Form
    {

        public Inventario _Inventario { get; set; }

        public mdInventario()
        {
            InitializeComponent();
        }

        private void mdProducto_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dataGridInventario.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSelecionar")
                {
                    cmbBoxGuardar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cmbBoxGuardar.DisplayMember = "Texto";
            cmbBoxGuardar.ValueMember = "Value";
            cmbBoxGuardar.SelectedIndex = 1;





            List<Inventario> lista = new CN_Inventario().Listar();
            foreach (Inventario item in lista)
            {
                dataGridInventario.Rows.Add(new object[] {
                "",
                item.IdInventario,
                item.Codigo,
                item.Descripcion,
                item.Cantidad,
                item.Precio
                });
            }

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBoxGuardar.SelectedItem).Valor.ToString();
            string search = txtbusqueda.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(search))
            {
                // optionally show all rows
                foreach (DataGridViewRow row in dataGridInventario.Rows)
                    row.Visible = true;
                return;
            }

            foreach (DataGridViewRow row in dataGridInventario.Rows)
            {
                if (row.IsNewRow) continue; // skip the 'new' row if present

                string cellText = Convert.ToString(row.Cells[columnaFiltro].Value); // safe for null
                bool match = cellText.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

                row.Visible = match;
            }
        }

        private void dataGridInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;
            if (iRow >= 0 && iColumn >= 0)
            {
                _Inventario = new Inventario()
                {
                    Codigo = Convert.ToInt32(dataGridInventario.Rows[iRow].Cells["Codigo"].Value),
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
