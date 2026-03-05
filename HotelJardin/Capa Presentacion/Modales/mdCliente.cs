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

        private List<Cliente> clientes = new List<Cliente>();

        private void cargarGrid()
        {
            clientes = new CN_Cliente().Listar();
            dataGridCliente.DataSource = clientes;

        }
        private void mdCliente_Load(object sender, EventArgs e)
        {
            cargarGrid();
        }

        private void dataGridCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;
            if (iRow >= 0 && iColumn >= 0)
            {
                _Cliente = new Cliente()
                {
                    IdCliente = Convert.ToInt32(dataGridCliente.Rows[iRow].Cells["IdCliente"].Value),
                    CodigoCliente = Convert.ToInt32(dataGridCliente.Rows[iRow].Cells["Codigo"].Value),
                    NombreCompleto = dataGridCliente.Rows[iRow].Cells["NombreCompleto"].Value.ToString(),
                    Presupuesto = Convert.ToDecimal(dataGridCliente.Rows[iRow].Cells["Presupuesto"].Value)
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        

        private void cmbBoxGuardar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {
            if(txtbusqueda.TextLength >= 3)
            {
                var listaFiltrada = clientes.Where(x => x.NombreCompleto.ToLower().Contains(txtbusqueda.Text.ToLower()) || x.CodigoCliente.ToString().Contains(txtbusqueda.Text)).ToList();
                dataGridCliente.DataSource = listaFiltrada;
            }
            else
            {
                dataGridCliente.DataSource = clientes;
            }
        }
    }
}
