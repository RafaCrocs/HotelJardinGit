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

        private void frmClientes_Load(object sender, EventArgs e)
        {
            //Mostrar opciones en cbmBoxguardar
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



            //Mostrar usuarios en el datagridview
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

        private void frmInicioGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;
            try
            {
                Cliente obj = new Cliente()
                {
                    IdCliente = Convert.ToInt32(txtId.Text),
                    NombreCompleto = txtNombreCompleto.Text,
                    CodigoCliente = Convert.ToInt32(txtCodigoCliente.Text),
                    Correo = txtCorreo.Text,
                    Presupuesto = Convert.ToDecimal(txtPresupuesto.Text),
                };



                if (obj.IdCliente == 0)
                {
                    int IdClienteGenerado = new CN_Cliente().Registrar(obj, out Mensaje);


                    if (IdClienteGenerado != 0)
                    {
                        dataGridCliente.Rows.Add(new object[] {
                        "",
                        IdClienteGenerado,
                        Convert.ToInt32(txtCodigoCliente.Text),
                        txtNombreCompleto.Text,
                        txtCorreo.Text,
                        Convert.ToDecimal(txtPresupuesto.Text)
                    });
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(Mensaje);
                    }
                }
                else
                {
                    bool resultado = new CN_Cliente().Editar(obj, out Mensaje);
                    if (resultado)
                    {
                        DataGridViewRow rowEncontrada = null;
                        foreach (DataGridViewRow row in dataGridCliente.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["IdCliente"].Value) == obj.IdCliente)
                            {
                                rowEncontrada = row;
                                break;
                            }
                        }
                        if (rowEncontrada != null)
                        {
                            rowEncontrada.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                            rowEncontrada.Cells["Codigo"].Value = Convert.ToInt32(txtCodigoCliente.Text);
                            rowEncontrada.Cells["Correo"].Value = txtCorreo.Text;
                            rowEncontrada.Cells["Presupuesto"].Value = Convert.ToDecimal(txtPresupuesto.Text);
                        }
                        Limpiar();


                    }
                    else
                    {
                        MessageBox.Show("Algo malio sal", Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datos Logicos...", "Error", MessageBoxButtons.OKCancel);
                Limpiar();
            }
        }
        private void Limpiar()
        {
            txtNombreCompleto.Text = "";
            txtCodigoCliente.Text = "";
            txtCorreo.Text = "";
            txtPresupuesto.Text = "";
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
                    txtCodigoCliente.Text = row.Cells["Codigo"].Value?.ToString() ?? "";
                    txtCorreo.Text = row.Cells["Correo"].Value?.ToString() ?? "";
                    txtPresupuesto.Text = row.Cells["Presupuesto"].Value?.ToString() ?? "";
                }
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    Cliente objCliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtId.Text)
                    };
                    bool resultado = new CN_Cliente().Eliminar(objCliente, out Mensaje);
                    if (resultado)
                    {
                        // Buscar la fila correspondiente por IdUsuario (valor de BD)
                        int idCliente = objCliente.IdCliente;
                        DataGridViewRow rowEncontrada = null;
                        foreach (DataGridViewRow r in dataGridCliente.Rows)
                        {
                            if (r.Cells["IdCliente"].Value != null &&
                                int.TryParse(r.Cells["IdCliente"].Value.ToString(), out int idCell) &&
                                idCell == idCliente)
                            {
                                rowEncontrada = r;
                                break;
                            }
                        }
                        if (rowEncontrada != null)
                        {
                            dataGridCliente.Rows.Remove(rowEncontrada);
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la fila correspondiente al Articulo eliminado.");
                        }
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void limpiarBuscar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar el formulario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar();
            }
        }

        private void buscar_Click(object sender, EventArgs e)
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
