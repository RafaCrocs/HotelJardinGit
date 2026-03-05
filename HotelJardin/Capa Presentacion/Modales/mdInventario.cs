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

        private List<Inventario> inventario = new CN_Inventario().Listar();

        public mdInventario()
        {
            InitializeComponent();
        }

        private void cargarGrid()
        {
            List<Inventario> lista = new CN_Inventario().Listar();
            dataGridInventario.DataSource = lista;
        }



        private void mdProducto_Load(object sender, EventArgs e)
        {
            cargarGrid();
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

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {

            if (txtbusqueda.Text.Length >= 3)
            {
                var listaFiltrada = inventario.Where(x => (x.Descripcion ?? "").ToLower().Contains((txtbusqueda.Text).ToLower()) || (x.Codigo.ToString() ?? "").Contains((txtbusqueda.Text)) || (x.Proveedor ?? "").ToLower().Contains((txtbusqueda.Text).ToLower())).ToList();
                dataGridInventario.DataSource = listaFiltrada;
            }
            else
            {
                dataGridInventario.DataSource = inventario;
            }
        }
    }
}
