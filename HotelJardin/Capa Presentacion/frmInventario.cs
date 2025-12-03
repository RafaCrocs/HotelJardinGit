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
using System.Xml.Linq;

namespace Capa_Presentacion
{
    public partial class frmInventario : Form
    {
        public frmInventario()
        {
            InitializeComponent();
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {

            //Mostrar opciones en cbmBoxguardar
            foreach (DataGridViewColumn columna in dataGridInventario.Columns)
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
            List<Inventario> lista = new CN_Inventario().Listar();


            foreach (Inventario item in lista)
            {
                dataGridInventario.Rows.Add(new object[] {
                "",
                item.IdInventario,
                item.Codigo,
                item.Descripcion,
                item.Proveedor,
                item.Cantidad,
                item.Precio
                });
            }
        }

        private void frmInicioGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;
            try
            {
                Inventario obj = new Inventario()
                {
                    IdInventario = Convert.ToInt32(txtId.Text),
                    Codigo = Convert.ToInt32(txtInventarioCodigo.Text),
                    Descripcion = txtInventarioDescripcion.Text,
                    Proveedor = txtInventarioProveedor.Text,
                    Cantidad = Convert.ToInt32(txtInventarioCantidad.Text),
                    Precio = Convert.ToDecimal(txtInventarioPrecio.Text)
                };



                if (obj.IdInventario == 0)
                {
                    int IdProductoGenerado = new CN_Inventario().Registrar(obj, out Mensaje);


                    if (IdProductoGenerado != 0)
                    {
                        dataGridInventario.Rows.Add(new object[] {
                    "",
                    IdProductoGenerado,
                    txtInventarioCodigo.Text,
                    txtInventarioDescripcion.Text,
                    txtInventarioProveedor.Text,
                    txtInventarioCantidad.Text,
                    txtInventarioPrecio.Text
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
                    bool resultado = new CN_Inventario().Editar(obj, out Mensaje);
                    if (resultado)
                    {
                        DataGridViewRow rowEncontrada = null;
                        foreach (DataGridViewRow row in dataGridInventario.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["IdInventario"].Value) == obj.IdInventario)
                            {
                                rowEncontrada = row;
                                break;
                            }
                        }
                        if (rowEncontrada != null)
                        {
                            rowEncontrada.Cells["Codigo"].Value = txtInventarioCodigo.Text;
                            rowEncontrada.Cells["Descripcion"].Value = txtInventarioDescripcion.Text;
                            rowEncontrada.Cells["Proveedor"].Value = txtInventarioProveedor.Text;
                            rowEncontrada.Cells["Cantidad"].Value = txtInventarioCantidad.Text;
                            rowEncontrada.Cells["Precio"].Value = txtInventarioPrecio.Text;
                        }
                        Limpiar();


                    }
                    else
                    {
                        MessageBox.Show(Mensaje);
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
            txtInventarioCodigo.Text = "";
            txtInventarioDescripcion.Text = "";
            txtInventarioProveedor.Text = "";
            txtInventarioCantidad.Text = "";
            txtInventarioPrecio.Text = "";
        }

        private void dataGridInventario_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Obtener el índice actual de la columna por nombre en vez de asumir 0
            var iconCol = dataGridInventario.Columns["dsa"];
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

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Articulo?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    Inventario objInventario = new Inventario()
                    {
                        IdInventario = Convert.ToInt32(txtId.Text)
                    };
                    bool resultado = new CN_Inventario().Eliminar(objInventario, out Mensaje);
                    if (resultado)
                    {
                        // Buscar la fila correspondiente por IdUsuario (valor de BD)
                        int idInventario = objInventario.IdInventario;
                        DataGridViewRow rowEncontrada = null;
                        foreach (DataGridViewRow r in dataGridInventario.Rows)
                        {
                            if (r.Cells["IdInventario"].Value != null &&
                                int.TryParse(r.Cells["IdInventario"].Value.ToString(), out int idCell) &&
                                idCell == idInventario)
                            {
                                rowEncontrada = r;
                                break;
                            }
                        }
                        if (rowEncontrada != null)
                        {
                            dataGridInventario.Rows.Remove(rowEncontrada);
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

        private void btnLimpiar_Click(object sender, EventArgs e)
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

        private void limpiarBuscar_Click(object sender, EventArgs e)
        {
            //Limpiar busqueda
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dataGridInventario.Rows)
            {
                row.Visible = true;
            }
        }

        private void dataGridInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridInventario.Columns[e.ColumnIndex].Name == "dsa")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtId.Text = dataGridInventario.Rows[indice].Cells["IdInventario"].Value.ToString();
                    txtInventarioCodigo.Text = dataGridInventario.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtInventarioDescripcion.Text = dataGridInventario.Rows[indice].Cells["Descripcion"].Value.ToString();
                    txtInventarioProveedor.Text = dataGridInventario.Rows[indice].Cells["Proveedor"].Value.ToString();
                    txtInventarioCantidad.Text = dataGridInventario.Rows[indice].Cells["Cantidad"].Value.ToString();
                    txtInventarioPrecio.Text = dataGridInventario.Rows[indice].Cells["Precio"].Value.ToString();
                }
            }
        }
    }
}


