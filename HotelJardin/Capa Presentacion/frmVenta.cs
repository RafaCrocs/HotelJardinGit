using Capa_Presentacion.Modales;
using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class frmVenta : Form
    {

        private Usuario _Usuario;

        private decimal presupuestoInicial = 0m;
        private bool clienteSeleccionado = false;
        private decimal totalVenta = 0m;

        private int IdClienteSeleccionado = 0;
        private string correoCliente = "";
        public frmVenta(Usuario oUsuario = null)
        {

            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCodigo.Text = "";
            txtTotal.Text = "";
            txtCantidadArticulos.Text = "";
            txtColones.Text = "";

            if (this.Controls.ContainsKey("txtPresupuesto"))
            {
                var ctrl = this.Controls["txtPresupuesto"] as TextBox;
                if (ctrl != null) ctrl.Text = "";
            }


            try
            {
                cmbModoPago.Items.Clear();
                cmbModoPago.Items.Add("Efectivo");
                cmbModoPago.Items.Add("Tarjeta");
                cmbModoPago.DropDownStyle = ComboBoxStyle.DropDownList;

                if (cmbModoPago.Items.Contains("Tarjeta"))
                {
                    cmbModoPago.SelectedItem = "Tarjeta";
                }
                else if (cmbModoPago.Items.Count > 0)
                {
                    cmbModoPago.SelectedIndex = 0;
                }
            }
            catch
            {
                // Evitar que errores en la UI impidan la carga; no debería ocurrir.
            }
        }



        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtQRCliente.Text = modal._Cliente.CodigoCliente.ToString();
                    txtNombreCompleto.Text = modal._Cliente.NombreCompleto.ToString();

                    try
                    {
                        presupuestoInicial = Convert.ToDecimal(modal._Cliente.Presupuesto);
                        clienteSeleccionado = true;

                        IdClienteSeleccionado = modal._Cliente.IdCliente;
                        if (this.Controls.ContainsKey("txtPresupuesto"))
                        {
                            var ctrl = this.Controls["txtPresupuesto"] as TextBox;
                            if (ctrl != null)
                            {
                                ctrl.Text = (-presupuestoInicial).ToString("0.00");
                            }
                        }
                    }
                    catch
                    {
                        presupuestoInicial = 0m;
                        clienteSeleccionado = true;
                        
                    }
                }
            }
        }

        private void txtQRCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Evitar el sonido de 'ding' y la repetición de la tecla
                e.Handled = true;
                e.SuppressKeyPress = true;

                var input = txtQRCliente.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Ingrese un código de cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(input, out int codigo))
                {
                    MessageBox.Show("El código debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Buscar cliente por código usando la lista actual. Mejor crear un método específico en CN_Cliente si la lista es grande.
                List<Cliente> lista;
                try
                {
                    lista = new CN_Cliente().Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var cliente = lista.FirstOrDefault(c => c.CodigoCliente == codigo);

                if (cliente != null)
                {
                    txtQRCliente.Text = cliente.CodigoCliente.ToString();
                    txtNombreCompleto.Text = cliente.NombreCompleto ?? "";
                    try
                    {
                        presupuestoInicial = Convert.ToDecimal(cliente.Presupuesto);
                    }
                    catch
                    {
                        presupuestoInicial = 0m;
                    }

                    clienteSeleccionado = true;

                    if (this.Controls.ContainsKey("txtPresupuesto"))
                    {
                        var ctrl = this.Controls["txtPresupuesto"] as TextBox;
                        if (ctrl != null)
                        {
                            ctrl.Text = (-presupuestoInicial).ToString("0.00");
                        }
                    }

                    txtTotal.Text = (-presupuestoInicial).ToString("0.00");
                    totalVenta = 0m;

                    this.ActiveControl = txtCodigo;
                    txtCodigo.Focus();
                }
                else
                {
                    txtNombreCompleto.Text = "";
                    MessageBox.Show($"No se encontró un cliente con el código {codigo}.", "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            // Evitar el sonido de 'ding' y la repetición de la tecla
            e.Handled = true;
            e.SuppressKeyPress = true;

            var input = txtCodigo.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Ingrese un código de producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(input, out int codigo))
            {
                MessageBox.Show("El código debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Inventario> lista;
            try
            {
                lista = new CN_Inventario().Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var item = lista.FirstOrDefault(i => i.Codigo == codigo);
            if (item == null)
            {
                MessageBox.Show($"No se encontró producto con el código {codigo}.", "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Clear();
                return;
            }

            AgregarInventarioAlGrid(item);

            // Limpiar y devolver foco
            txtCodigo.Clear();
            txtCodigo.Focus();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            using (var modal = new mdInventario())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtCodigo.Text = modal._Inventario.Codigo.ToString();
                }
            }
        }

        // Agrega un inventario al grid; si ya existe, incrementa cantidad y actualiza subtotal
        private void AgregarInventarioAlGrid(Inventario inv)
        {
            if (inv == null) return;

            string codigoStr = inv.Codigo.ToString();
            string descripcion = inv.Descripcion ?? "";
            decimal precio = inv.Precio;

            // Buscar fila existente por Código
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                var cellVal = Convert.ToString(row.Cells["Codigo"].Value);
                if (cellVal == codigoStr)
                {
                    int cantidad = 1;
                    int.TryParse(Convert.ToString(row.Cells["Cantidad"].Value), out cantidad);
                    cantidad++;
                    row.Cells["Cantidad"].Value = cantidad.ToString();

                    decimal subtotal = cantidad * precio;
                    row.Cells["SubTotal"].Value = subtotal.ToString("0.00");

                    CalcularTotales();
                    return;
                }
            }

            // Si no existe, agregar nueva fila con cantidad 1
            // ORDEN de columnas en el grid: Codigo, Descripcion, Cantidad, Agregar (botón), SubTotal, asd (botón)
            dataGridView1.Rows.Add(new object[]
            {
                codigoStr,              // Codigo
                descripcion,            // Descripcion
                1,                      // Cantidad
                "",                     // Agregar (columna botón) -> placeholder
                precio.ToString("0.00"),// SubTotal
                precio.ToString("0.00"),// SubTotal inicial (cantidad 1)
                ""                      // asd (columna botón) -> placeholder
            });

            CalcularTotales();
        }

        private void CalcularTotales()
        {
            decimal total = 0m;
            int cantidadArticulos = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                decimal subtotal = 0m;
                int cantidad = 0;
                decimal.TryParse(Convert.ToString(row.Cells["SubTotal"].Value), out subtotal);
                int.TryParse(Convert.ToString(row.Cells["Cantidad"].Value), out cantidad);

                total += subtotal;
                cantidadArticulos += cantidad;
            }

            totalVenta = total;

            txtCantidadArticulos.Text = cantidadArticulos.ToString();

            if (clienteSeleccionado)
            {
                decimal presupuestoActual = -presupuestoInicial + total;
                txtTotal.Text = presupuestoActual.ToString("0.00");
            }
            else
            {
                txtTotal.Text = totalVenta.ToString("0.00");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var column = dataGridView1.Columns[e.ColumnIndex];
                if (column == null) return;

                var row = dataGridView1.Rows[e.RowIndex];
                if (row == null || row.IsNewRow) return;

                // Lectura segura de cantidad y subtotal actuales
                int cantidad = 0;
                decimal subtotal = 0m;
                int.TryParse(Convert.ToString(row.Cells["Cantidad"].Value), out cantidad);
                decimal.TryParse(Convert.ToString(row.Cells["SubTotal"].Value), out subtotal);

                // Determinar acción por nombre de columna
                if (column.Name == "Agregar")
                {
                    // Incrementar 1
                    decimal precioUnitario = 0m;
                    if (cantidad > 0)
                    {
                        // deducir precio unitario del subtotal
                        precioUnitario = subtotal / Math.Max(cantidad, 1);
                    }
                    else
                    {
                        // si no hay subtotal ni cantidad, intentar leer precio unitario desde fila (si existe una columna oculta u otra fuente)
                        // aquí asumimos que cuando se agregó la fila con cantidad=1, SubTotal ya lleva el precio unitario; si subtotal es 0 y cantidad 0,
                        // no podemos calcular: dejar precioUnitario en 0 y evitar división por cero
                        decimal.TryParse(Convert.ToString(row.Cells["SubTotal"].Value), out precioUnitario);
                    }

                    // Si precioUnitario es 0 y cantidad==0, intentar inferirlo desde el último valor conocido (no disponible), por ahora no cambiaría subtotal.
                    int nuevaCantidad = cantidad + 1;
                    decimal nuevoSubtotal = Math.Round(precioUnitario * nuevaCantidad, 2);

                    // Si subtotal era 0 y precioUnitario 0, intentar usar el valor actual de SubTotal como unitario (fallback)
                    if (precioUnitario == 0m && subtotal == 0m)
                    {
                        // no hay precio conocido; no modificar y avisar
                        MessageBox.Show("No se puede obtener el precio unitario para aumentar la cantidad.", "Precio desconocido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    row.Cells["Cantidad"].Value = nuevaCantidad.ToString();
                    row.Cells["SubTotal"].Value = nuevoSubtotal.ToString("0.00");

                    CalcularTotales();
                    return;
                }

                if (column.Name == "asd")
                {
                    if (cantidad <= 0) return;

                    if (cantidad == 1)
                    {
                        // eliminar fila si queda 0
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        CalcularTotales();
                        return;
                    }

                    // calcular precio unitario a partir del subtotal actual
                    decimal precioUnitario = subtotal / Math.Max(cantidad, 1);
                    int nuevaCantidad = cantidad - 1;
                    decimal nuevoSubtotal = Math.Round(precioUnitario * nuevaCantidad, 2);

                    row.Cells["Cantidad"].Value = nuevaCantidad.ToString();
                    row.Cells["SubTotal"].Value = nuevoSubtotal.ToString("0.00");

                    CalcularTotales();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar acción del grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;

            // Obtener el índice actual de la columna por nombre en vez de asumir 0
            var iconCol = dataGridView1.Columns["Agregar"];
            var iconCol2 = dataGridView1.Columns["asd"];
            if (iconCol == null)
                return;

            if (e.ColumnIndex == iconCol.Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var bmp = Properties.Resources.mas;
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
            if (e.ColumnIndex == iconCol2.Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var bmp = Properties.Resources.signo_menos_de_una_linea_en_posicion_horizontal;
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
            if (double.TryParse(txtTotal.Text, out double totalColones))
            {
                txtColones.Text = (totalColones * 490).ToString("0");
            }
            else
            {
                txtColones.Text = "0";
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQRCliente.Text))
            {
                MessageBox.Show("Debe agregar un cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Debe agregar productos al detalle de la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Construir DataTable exactamente como el UDT dbo.EDetalle_Venta
            DataTable dtDetalle = new DataTable();

            dtDetalle.Columns.Add("IdInventario", typeof(int));
            dtDetalle.Columns.Add("Detalle", typeof(string));
            dtDetalle.Columns.Add("Precio", typeof(decimal));
            dtDetalle.Columns.Add("Cantidad", typeof(int));
            dtDetalle.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dtDetalle.Rows.Add(
                    Convert.ToInt32(row.Cells["Codigo"].Value),
                    row.Cells["Descripcion"].Value.ToString(),
                    Convert.ToDecimal(row.Cells["PrecioUnitario"].Value),
                    Convert.ToInt32(row.Cells["Cantidad"].Value),
                    Convert.ToDecimal(row.Cells["SubTotal"].Value)
                );
            }

            // Validar/parsear cliente
            int idCliente = 0;
            if (!int.TryParse(txtQRCliente.Text, out idCliente))
            {
                MessageBox.Show("Código de cliente inválido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int IdCorrelativo = new CN_Venta().ObtenerCorrelativo();
            string NumeroVentaStr = string.Format("{0:000}", IdCorrelativo);

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                NumeroFact = Convert.ToInt32(NumeroVentaStr),
                oCliente = new Cliente() {IdCliente = idCliente, CodigoCliente = Convert.ToInt32(txtQRCliente.Text) }, // asignar IdCliente
                NombreCliente = txtNombreCompleto.Text,
                ModoPago = cmbModoPago.SelectedItem.ToString(),
                MontoTotal = decimal.TryParse(txtTotal.Text, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal mt) ? mt : 0,
                FechaRegistro = DateTime.Now
            };

            string Mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta, dtDetalle, out Mensaje);

            if (respuesta)
            {
                MessageBox.Show("Venta registrada correctamente " + NumeroVentaStr, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQRCliente.Text = "";
                txtNombreCompleto.Text = "";
                txtColones.Text = "";
                txtTotal.Text = "";
                dataGridView1.Rows.Clear();

                presupuestoInicial = 0m;
                clienteSeleccionado = false;
                totalVenta = 0m;
                if (this.Controls.ContainsKey("txtPresupuesto"))
                {
                    var ctrl = this.Controls["txtPresupuesto"] as TextBox;
                    if (ctrl != null) ctrl.Text = "";
                }

                // Mover el cursor (focus) automáticamente a txtQRCliente
                this.ActiveControl = txtQRCliente;   // asegura que el control del formulario sea txtQRCliente
                txtQRCliente.Focus();                // pone el foco en el TextBox

            }
            else
            {
                MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LogDetalleVenta(DataTable dt)
        {
            Debug.WriteLine("---- DetalleVenta SCHEMA ----");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var c = dt.Columns[i];
                Debug.WriteLine($"Col[{i}] Name: '{c.ColumnName}', Type: {c.DataType}, AllowDBNull: {c.AllowDBNull}");
            }

            Debug.WriteLine("---- DetalleVenta FILAS (hasta 20) ----");
            int rowCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (rowCount++ >= 20) break;
                var vals = dt.Columns.Cast<DataColumn>().Select(c =>
                {
                    if (r.IsNull(c)) return "<NULL>";
                    var v = r[c];
                    return $"{v} (CLR:{v.GetType().Name})";
                });
                Debug.WriteLine($"Fila {rowCount}: {string.Join(" | ", vals)}");
            }
        }
        private void ValidarSchemaDetalle(DataTable dt)
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            if (dt.Columns.Count != 5) throw new ArgumentException("DetalleVenta: columnas inválidas");

            if (dt.Columns[0].ColumnName != "IdInventario" || dt.Columns[0].DataType != typeof(int)) throw new ArgumentException("Columna 0 inválida");
            if (dt.Columns[1].ColumnName != "Precio" || dt.Columns[1].DataType != typeof(decimal)) throw new ArgumentException("Columna 1 inválida");
            if (dt.Columns[2].ColumnName != "Detalle" || dt.Columns[2].DataType != typeof(string)) throw new ArgumentException("Columna 2 inválida");
            if (dt.Columns[3].ColumnName != "Cantidad" || dt.Columns[3].DataType != typeof(int)) throw new ArgumentException("Columna 3 inválida");
            if (dt.Columns[4].ColumnName != "MontoTotal" || dt.Columns[4].DataType != typeof(decimal)) throw new ArgumentException("Columna 4 inválida");
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Desea borrar la venta actual y limpiar el formulario?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            // Limpiar campos y grid
            txtQRCliente.Text = "";
            txtNombreCompleto.Text = "";
            txtColones.Text = "";
            txtTotal.Text = "";
            txtCantidadArticulos.Text = "";
            dataGridView1.Rows.Clear();

            // Restaurar modo de pago por defecto si existe
            if (cmbModoPago != null)
            {
                if (cmbModoPago.Items.Contains("Tarjeta"))
                    cmbModoPago.SelectedItem = "Tarjeta";
                else if (cmbModoPago.Items.Count > 0)
                    cmbModoPago.SelectedIndex = 0;
            }

            // Devolver foco al campo de cliente
            this.ActiveControl = txtQRCliente;
            txtQRCliente.Focus();
        }


    }
}
