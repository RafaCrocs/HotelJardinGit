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
using CapaEntidad;
using CapaNegocio;

namespace Capa_Presentacion
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private List<Cliente> clientes = new List<Cliente>();

        private void cargarGrid()
        {
            clientes = new CN_Cliente().Listar();
            dataGridCliente.DataSource = clientes;
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            cargarGrid();
        }

        private void frmInicioGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;
            try
            {
                // Al crear o editar, el presupuesto actual (Presupuesto) 
                // inicialmente es igual al PresupuestoInicial si es un cliente nuevo.
                Cliente obj = new Cliente()
                {
                    IdCliente = Convert.ToInt32(txtId.Text),
                    NombreCompleto = txtNombreCompleto.Text,
                    CodigoCliente = Convert.ToInt32(txtCodigoCliente.Text),
                    Correo = txtCorreo.Text,
                    PresupuestoInicial = Convert.ToDecimal(txtPresupuestoInicial.Text),
                    // Si es nuevo, el saldo disponible es el total inicial
                    Presupuesto = Convert.ToInt32(txtId.Text) == 0 ? 
                                  Convert.ToDecimal(txtPresupuestoInicial.Text) : 
                                  Convert.ToDecimal(txtPresupuesto.Text)
                };

                if (obj.IdCliente == 0)
                {
                    int IdClienteGenerado = new CN_Cliente().Registrar(obj, out Mensaje);
                    if (IdClienteGenerado != 0)
                    {
                        Limpiar();
                    }
                    else { MessageBox.Show(Mensaje); }
                }
                else
                {
                    bool resultado = new CN_Cliente().Editar(obj, out Mensaje);
                    if (resultado)
                    {
                        foreach (DataGridViewRow row in dataGridCliente.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["IdCliente"].Value) == obj.IdCliente)
                            {
                                row.Cells["NombreCompleto"].Value = obj.NombreCompleto;
                                row.Cells["CodigoCliente"].Value = obj.CodigoCliente;
                                row.Cells["Correo"].Value = obj.Correo;
                                row.Cells["PresupuestoInicial"].Value = obj.PresupuestoInicial;
                                row.Cells["Presupuesto"].Value = obj.Presupuesto;
                                break;
                            }
                        }
                        Limpiar();
                    }
                }
                cargarGrid();
            }
            catch { MessageBox.Show("Error en el formato de los datos."); }
        }
        private void Limpiar()
        {
            txtNombreCompleto.Text = "";
            txtCodigoCliente.Text = "";
            txtCorreo.Text = "";
            txtPresupuestoInicial.Text = "";
            txtPresupuesto.Text = "";
            txtId.Text = "0";
        }

        private void dataGridCliente_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Obtener el índice actual de la columna por nombre en vez de asumir 0
            var iconCol = dataGridCliente.Columns["dsa"];
            if (iconCol == null)
                return;

            if (e.ColumnIndex == iconCol.Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var bmp = Properties.Resources.controlar;
                if (bmp != null)
                {
                    var w = bmp.Width;
                    var h = bmp.Height;
                    // Escalar si la imagen es más grande que la celda
                    var maxW = e.CellBounds.Width - 4;
                    var maxH = e.CellBounds.Height - 4;
                    if (w > maxW || h > maxH)
                    {
                        var scale = Math.Min((float)maxW / w, (float)maxH / h);
                        w = (int)(w * scale);
                        h = (int)(h * scale);
                    }
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                    e.Graphics.DrawImage(bmp, new Rectangle(x, y, w, h));
                }
                e.Handled = true;
            }
        }

        private void dataGridCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridCliente.Columns[e.ColumnIndex].Name == "dsa")
            {
                int indice = e.RowIndex;
                if (indice >= 0) // allow row 0
                {
                    var row = dataGridCliente.Rows[indice];
                    // Use existing column names from designer
                    txtId.Text = row.Cells["IdCliente"].Value?.ToString() ?? "0";
                    txtNombreCompleto.Text = row.Cells["NombreCompleto"].Value?.ToString() ?? "";
                    txtCodigoCliente.Text = row.Cells["CodigoCliente"].Value?.ToString() ?? "";
                    txtCorreo.Text = row.Cells["Correo"].Value?.ToString() ?? "";
                    txtPresupuestoInicial.Text = row.Cells["PresupuestoInicial"].Value?.ToString() ?? "";
                    txtPresupuesto.Text = row.Cells["Presupuesto"].Value?.ToString() ?? "";
                }   
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int idCliente) || idCliente == 0)
                return;

            if (MessageBox.Show("¿Desea eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string Mensaje = string.Empty;
                Cliente objCliente = new Cliente() { IdCliente = idCliente };

                bool resultado = new CN_Cliente().Eliminar(objCliente, out Mensaje);
                if (resultado)
                {
                    txtId.Text = "0";
                    Limpiar();
                    cargarGrid();
                }
                else
                {
                    MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        

        

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtbusqueda.Text.Length >= 3)
            {
                var listaFiltrada = clientes.Where(x => (x.NombreCompleto ?? "").ToLower().Contains((txtbusqueda.Text).ToLower()) || (x.CodigoCliente.ToString() ?? "").ToLower().Contains((txtbusqueda.Text).ToLower())).ToList();
                dataGridCliente.DataSource = listaFiltrada;
            }
            else
            {
                dataGridCliente.DataSource = clientes;
            }
        }
    }
}
