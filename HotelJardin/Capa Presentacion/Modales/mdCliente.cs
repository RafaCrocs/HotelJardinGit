using Capa_Presentacion.Utilidades;
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
    public partial class mdCliente : Form
    {

        public Cliente _Cliente { get; set; }



        public mdCliente()
        {
            InitializeComponent();
        }

        private void mdCliente_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dataGridCliente.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSelecionar")
                {
                    cmbBoxGuardar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cmbBoxGuardar.DisplayMember = "Texto";
            cmbBoxGuardar.ValueMember = "Value";
            cmbBoxGuardar.SelectedIndex = 2;

            List<Cliente> lista = new CN_Cliente().Listar();


            foreach (Cliente item in lista)
            {
                dataGridCliente.Rows.Add(new object[] {
                "",
                item.IdCliente,
                item.CodigoCliente,
                item.NombreCompleto,
                item.Correo,
                item.Presupuesto,
                });
            }





        }

        private void dataGridCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;
            if (iRow >= 0 && iColumn >= 0)
            {
                _Cliente = new Cliente()
                {
                    CodigoCliente = Convert.ToInt32(dataGridCliente.Rows[iRow].Cells["Codigo"].Value),
                    NombreCompleto = dataGridCliente.Rows[iRow].Cells["NombreCompleto"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBoxGuardar.SelectedItem).Valor.ToString();
            string search = txtbusqueda.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(search))
            {
                // optionally show all rows
                foreach (DataGridViewRow row in dataGridCliente.Rows)
                    row.Visible = true;
                return;
            }

            foreach (DataGridViewRow row in dataGridCliente.Rows)
            {
                if (row.IsNewRow) continue; // skip the 'new' row if present

                string cellText = Convert.ToString(row.Cells[columnaFiltro].Value); // safe for null
                bool match = cellText.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

                row.Visible = match;
            }
        }
    }
}
