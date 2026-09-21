using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Capa_Presentacion.Modales;
using Capa_Presentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace Capa_Presentacion
{
    /// <summary>
    /// Punto de venta con presupuesto compartido.
    ///
    /// Varias personas (pareja, trio, familia) pueden pagar una misma factura.
    /// Cada una llega a la caja y da su codigo; el cajero las va agregando y el
    /// sistema reparte el consumo en proporcion al saldo de cada quien.
    ///
    /// COMO SE USA EN CAJA
    ///   1. Cada persona dicta o escanea su codigo en la caja "Codigo QR" y se
    ///      pulsa Enter. Tambien se puede buscar con la lupa.
    ///   2. Van apareciendo en el grid "Personas que comparten la factura",
    ///      con su saldo y cuanto le toca pagar. La columna "Le toca" se
    ///      recalcula sola con cada producto que se agrega.
    ///   3. Se escanean los productos como siempre.
    ///   4. Guardar. Cada persona queda con su saldo descontado.
    ///
    /// NOTA SOBRE EL GRID DE PARTICIPANTES
    /// Se crea por codigo en ConstruirGridParticipantes(), no en el Designer.
    /// Se ubica en el espacio libre que queda entre el grupo "Informacion Venta"
    /// y el grid de productos. Si se reacomoda el formulario, basta con ajustar
    /// las constantes de posicion que estan al inicio de ese metodo.
    /// </summary>
    public partial class frmVenta : Form
    {
        private readonly Usuario _usuario;
        private readonly CarritoVenta _carrito = new CarritoVenta();

        private List<Inventario> _inventario = new List<Inventario>();
        private List<Cliente> _clientes = new List<Cliente>();

        // Grid de participantes, construido por codigo.
        private GroupBox gbParticipantes;

        private bool _actualizandoCliente;
        private bool _refrescando;

        public frmVenta(Usuario oUsuario = null)
        {
            _usuario = oUsuario;
            InitializeComponent();
        }

        #region Carga

        private void frmVenta_Load(object sender, EventArgs e)
        {
            if (_usuario == null)
            {
                Mensajes.Error("No hay un usuario valido en la sesion. No se pueden registrar ventas.");
                btnGuardar.Enabled = false;
            }

            ConfigurarGrid();
            ConfigurarModosPago();

            // El Designer deja esta caja en solo lectura. Como ahora cada
            // persona llega y dicta su codigo, tiene que aceptar escritura.
            txtQRCliente.ReadOnly = false;
            txtQRCliente.KeyDown += txtQRCliente_KeyDown;

            txtNombreCompleto.ReadOnly = true;
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            CargarInventario();
            CargarClientes();
            LimpiarVenta();

            ActiveControl = txtQRCliente;
        }

        /// <summary>
        /// Crea el grid de participantes en el espacio libre del formulario.
        ///
        /// Coordenadas de referencia del Designer:
        ///   groupBox2 ("Informacion Venta") ocupa hasta x=366, y=361
        ///   dataGridView1 (productos)       empieza en y=393
        ///   groupBox4                       empieza en x=1507
        /// Queda libre un rectangulo de 1080x180 que es justo donde se coloca.
        /// </summary>

        private void ConfigurarGrid()
        {
            GridHelper.AplicarEstilo(dataGridView1);
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;

            dataGridView1.Columns["Codigo"].ReadOnly = true;
            dataGridView1.Columns["Descripcion"].ReadOnly = true;
            dataGridView1.Columns["PrecioUnitario"].ReadOnly = true;
            dataGridView1.Columns["SubTotal"].ReadOnly = true;
            dataGridView1.Columns["Cantidad"].ReadOnly = false;
        }

        private void ConfigurarModosPago()
        {
            cmbModoPago.Items.Clear();
            cmbModoPago.Items.Add("Efectivo");
            cmbModoPago.Items.Add("Tarjeta");
            cmbModoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModoPago.SelectedIndex = 0;
        }

        private void CargarInventario()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _inventario = new CN_Inventario().Listar() ?? new List<Inventario>();

                if (_inventario.Count == 0)
                    Mensajes.Advertencia("El inventario esta vacio o no se pudo consultar la base de datos.");
            }
            catch (Exception ex)
            {
                Mensajes.Error("No se pudo cargar el inventario.", ex);
                _inventario = new List<Inventario>();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Trae los clientes una sola vez. Como ahora se escanean varios codigos
        /// por factura, consultar la tabla completa en cada uno seria una
        /// consulta por persona.
        /// </summary>
        private void CargarClientes()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _clientes = new CN_Cliente().Listar() ?? new List<Cliente>();
            }
            catch (Exception ex)
            {
                Mensajes.Error("No se pudo cargar la lista de clientes.", ex);
                _clientes = new List<Cliente>();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Participantes

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (mdCliente modal = new mdCliente())
            {
                if (modal.ShowDialog() != DialogResult.OK) return;
                AgregarParticipante(modal._Cliente);
            }
        }

        /// <summary>Enter en la caja de codigo agrega a la persona al grupo.</summary>
        private void txtQRCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            string texto = (txtQRCliente.Text ?? string.Empty).Trim();
            if (texto.Length == 0) return;

            int codigo;
            if (!Formato.TryEntero(texto, out codigo) || codigo <= 0)
            {
                Mensajes.Advertencia("El codigo de cliente debe ser un numero.");
                txtQRCliente.SelectAll();
                return;
            }

            Cliente cliente = _clientes.FirstOrDefault(c => c.CodigoCliente == codigo);

            if (cliente == null)
            {
                // Puede ser un huesped registrado despues de abrir la pantalla.
                CargarClientes();
                cliente = _clientes.FirstOrDefault(c => c.CodigoCliente == codigo);
            }

            if (cliente == null)
            {
                Mensajes.Info(string.Format("No se encontro un cliente con el codigo {0}.", codigo));
                txtQRCliente.SelectAll();
                return;
            }

            AgregarParticipante(cliente);
        }

        private void AgregarParticipante(Cliente cliente)
        {
            if (cliente == null) return;

            string error;
            if (!_carrito.AgregarParticipante(cliente, out error))
            {
                Mensajes.Advertencia(error);
                LimpiarCajaCodigoCliente();
                return;
            }

            RefrescarParticipantes();
            RefrescarTotales();
            LimpiarCajaCodigoCliente();

            // Con la primera persona ya se puede empezar a escanear productos.
            if (_carrito.CantidadParticipantes == 1)
            {
                ActiveControl = txtCodigo;
                txtCodigo.Focus();
            }
        }

        private void gridClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridClientes.Columns[e.ColumnIndex].Name != "QuitarCliente") return;

            int codigo = Formato.EnteroDeCelda(
                gridClientes.Rows[e.RowIndex].Cells["CodCliente"].Value);

            if (codigo <= 0) return;

            if (_carrito.CantidadParticipantes == 1 && !_carrito.EstaVacio)
            {
                if (!Mensajes.Confirmar(
                        "Es la unica persona de la factura y ya hay productos cargados.\n\n" +
                        "Si la quita, la venta se queda sin presupuesto asignado. Desea continuar?"))
                    return;
            }

            _carrito.QuitarParticipante(codigo);

            RefrescarParticipantes();
            RefrescarTotales();
        }

        private void RefrescarParticipantes()
        {
            gridClientes.Rows.Clear();

            foreach (ParticipanteVenta p in _carrito.Participantes)
            {
                int indice = gridClientes.Rows.Add();
                DataGridViewRow fila = gridClientes.Rows[indice];

                fila.Cells["CodCliente"].Value = p.CodigoCliente;
                fila.Cells["NomCliente"].Value = p.Nombre;
                fila.Cells["SaldoCliente"].Value = Formato.Moneda(p.SaldoAntes);
                fila.Cells["AsignadoCliente"].Value = Formato.Moneda(p.Asignado);
                fila.Cells["RestanteCliente"].Value = Formato.Moneda(p.SaldoDespues);

                // Se marca en rojo a quien llego sin saldo: no aporta nada al
                // presupuesto del grupo y conviene que el cajero lo vea.
                if (p.SaldoAntes <= 0m)
                    fila.DefaultCellStyle.ForeColor = Color.Firebrick;
            }

            _actualizandoCliente = true;
            try
            {
                txtNombreCompleto.Text = _carrito.NombreCliente;
            }
            finally
            {
                _actualizandoCliente = false;
            }
        }

        private void LimpiarCajaCodigoCliente()
        {
            _actualizandoCliente = true;
            try
            {
                txtQRCliente.Clear();
            }
            finally
            {
                _actualizandoCliente = false;
            }
        }

        /// <summary>
        /// Se conserva porque el Designer tiene el evento cableado. Los
        /// participantes se agregan con Enter o con la lupa, no mientras se
        /// escribe: si se agregara en cada tecla, un codigo como 1001 intentaria
        /// agregar al cliente 1, luego al 10, luego al 100.
        /// </summary>
        private void txtQRCliente_TextChanged(object sender, EventArgs e)
        {
            if (_actualizandoCliente) return;
        }

        private bool ValidarClienteSeleccionado()
        {
            if (_carrito.HayCliente) return true;

            Mensajes.Advertencia(
                "Debe agregar al menos una persona a la factura antes de cargar productos.");
            ActiveControl = txtQRCliente;
            txtQRCliente.Focus();
            return false;
        }

        #endregion

        #region Productos

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            string codigo = (txtCodigo.Text ?? string.Empty).Trim();
            if (codigo.Length == 0) return;

            if (!ValidarClienteSeleccionado()) return;

            Inventario articulo = _inventario.FirstOrDefault(i =>
                string.Equals(i.Codigo, codigo, StringComparison.OrdinalIgnoreCase));

            if (articulo == null)
            {
                Mensajes.Info(string.Format("No se encontro un producto con el codigo {0}.", codigo));
                txtCodigo.Clear();
                txtCodigo.Focus();
                return;
            }

            AgregarArticulo(articulo);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '+' && e.KeyChar != '-') return;

            e.Handled = true;

            LineaVenta linea = _carrito.Primera;
            if (linea == null) return;

            if (e.KeyChar == '+')
            {
                string error;
                if (!_carrito.Incrementar(linea.Codigo, out error))
                {
                    Mensajes.Advertencia(error);
                    return;
                }
            }
            else
            {
                _carrito.Disminuir(linea.Codigo);
            }

            RefrescarGrid();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (!ValidarClienteSeleccionado()) return;

            using (mdInventario modal = new mdInventario())
            {
                if (modal.ShowDialog() != DialogResult.OK) return;
                if (modal._Inventario == null) return;

                AgregarArticulo(modal._Inventario);
            }
        }

        private void AgregarArticulo(Inventario articulo)
        {
            string error;

            if (!_carrito.Agregar(articulo, out error))
            {
                Mensajes.Advertencia(error);
                txtCodigo.Clear();
                txtCodigo.Focus();
                return;
            }

            RefrescarGrid();

            txtCodigo.Clear();
            txtCodigo.Focus();
        }

        #endregion

        #region Grid de productos

        /// <summary>
        /// Vuelve a dibujar el grid de productos a partir del carrito, y de paso
        /// refresca el reparto entre participantes, porque cada producto que se
        /// agrega cambia cuanto le toca pagar a cada quien.
        /// </summary>
        private void RefrescarGrid()
        {
            if (_refrescando) return;

            _refrescando = true;
            try
            {
                dataGridView1.Rows.Clear();

                foreach (LineaVenta linea in _carrito.Lineas)
                {
                    dataGridView1.Rows.Add(
                        linea.Codigo,
                        linea.Descripcion,
                        linea.Cantidad,
                        string.Empty,
                        Formato.Moneda(linea.PrecioUnitario),
                        Formato.Moneda(linea.SubTotal),
                        string.Empty);
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[0].Selected = true;
                }

                RefrescarParticipantes();
                RefrescarTotales();
            }
            finally
            {
                _refrescando = false;
            }
        }

        private void RefrescarTotales()
        {
            txtCantidadArticulos.Text = Formato.Entero(_carrito.CantidadArticulos);
            txtTotalVenta.Text = Formato.Moneda(_carrito.Total);
            txtTotal.Text = Formato.Moneda(_carrito.ExtraAPagar);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string codigo = Formato.TextoDeCelda(dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value);
            if (string.IsNullOrEmpty(codigo)) return;

            if (GridHelper.ClickEnColumna(dataGridView1, e, "Agregar"))
            {
                string error;
                if (!_carrito.Incrementar(codigo, out error))
                {
                    Mensajes.Advertencia(error);
                    return;
                }
                RefrescarGrid();
                return;
            }

            if (GridHelper.ClickEnColumna(dataGridView1, e, "asd"))
            {
                _carrito.Disminuir(codigo);
                RefrescarGrid();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_refrescando) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "Cantidad") return;

            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
            string codigo = Formato.TextoDeCelda(fila.Cells["Codigo"].Value);

            int cantidad;
            if (!Formato.TryEntero(Formato.TextoDeCelda(fila.Cells["Cantidad"].Value), out cantidad))
            {
                Mensajes.Advertencia("La cantidad debe ser un numero entero.");
                RefrescarGrid();
                return;
            }

            string error;
            if (!_carrito.FijarCantidad(codigo, cantidad, out error))
                Mensajes.Advertencia(error);

            RefrescarGrid();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (GridHelper.PintarIcono(dataGridView1, e, "Agregar", Properties.Resources.mas))
            {
                e.Handled = true;
                return;
            }

            if (GridHelper.PintarIcono(dataGridView1, e, "asd",
                    Properties.Resources.signo_menos_de_una_linea_en_posicion_horizontal))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            if (dataGridView1.CurrentRow == null) return;

            e.Handled = true;

            string codigo = Formato.TextoDeCelda(dataGridView1.CurrentRow.Cells["Codigo"].Value);
            if (string.IsNullOrEmpty(codigo)) return;

            _carrito.Quitar(codigo);
            RefrescarGrid();
        }

        #endregion

        #region Guardar

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarVenta()) return;

            decimal totalVenta = _carrito.Total;
            decimal presupuestoUsado = _carrito.PresupuestoUsado;
            decimal extraAPagar = _carrito.ExtraAPagar;

            List<ParticipanteVenta> participantes = _carrito.ConstruirParticipantes();

            if (!ConfirmarReparto(totalVenta, extraAPagar, participantes)) return;

            Venta venta = ConstruirVenta(totalVenta, participantes);
            DataTable detalle = _carrito.ConstruirDetalle();

            string mensaje;
            bool registrada;

            try
            {
                Cursor = Cursors.WaitCursor;
                registrada = new CN_Venta().Registrar(venta, detalle, out mensaje);
            }
            catch (Exception ex)
            {
                Mensajes.Error("No se pudo registrar la venta.", ex);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (!registrada)
            {
                Mensajes.Error(string.IsNullOrWhiteSpace(mensaje)
                    ? "No se pudo registrar la venta."
                    : mensaje);
                return;
            }

            MostrarComprobante(venta.NumeroFact, totalVenta, presupuestoUsado,
                               extraAPagar, participantes);

            // Los saldos y el stock cambiaron en el servidor.
            CargarInventario();
            CargarClientes();
            LimpiarVenta();
        }

        /// <summary>
        /// Muestra el reparto antes de guardar. Con varias personas involucradas
        /// conviene que el cajero pueda leerlo en voz alta y confirmar, porque
        /// despues de guardar deshacerlo implica corregir saldos a mano.
        /// </summary>
        private bool ConfirmarReparto(decimal total, decimal extra,
                                      List<ParticipanteVenta> participantes)
        {
            if (participantes.Count < 2) return true;

            string detalle = string.Join("\n", participantes.Select(p =>
                string.Format("   {0} - {1}: paga {2}  (le quedan {3})",
                              p.CodigoCliente, p.Nombre,
                              Formato.Moneda(p.Asignado),
                              Formato.Moneda(p.SaldoDespues))));

            string texto = string.Format(
                "La factura de {0} se reparte entre {1} personas:\n\n{2}",
                Formato.Moneda(total), participantes.Count, detalle);

            if (extra > 0m)
                texto += string.Format("\n\nA pagar en efectivo o tarjeta: {0}", Formato.Moneda(extra));

            return Mensajes.Confirmar(texto + "\n\nConfirma la venta?");
        }

        private bool ValidarVenta()
        {
            if (_usuario == null)
            {
                Mensajes.Error("No hay un usuario valido en la sesion.");
                return false;
            }

            if (!_carrito.HayCliente)
            {
                Mensajes.Advertencia("Debe agregar al menos una persona a la factura.");
                return false;
            }

            if (_carrito.EstaVacio)
            {
                Mensajes.Advertencia("Debe agregar productos al detalle de la venta.");
                return false;
            }

            if (cmbModoPago.SelectedItem == null)
            {
                Mensajes.Advertencia("Seleccione el modo de pago.");
                cmbModoPago.Focus();
                return false;
            }

            if (_carrito.Total <= 0m)
            {
                Mensajes.Advertencia("El total de la venta debe ser mayor que cero.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Arma el objeto Venta.
        ///
        /// MontoTotal es el total real de la venta.
        ///
        /// oCliente sigue apuntando al participante principal para que la
        /// cabecera, la clave foranea y el reporte actual no cambien.
        /// oParticipantes lleva al grupo completo.
        ///
        /// El reparto definitivo lo calcula usp_RegistrarVenta con los saldos
        /// reales y las filas bloqueadas: lo que se ve en pantalla es la vista
        /// previa. Si alguien consumio en otra caja entre medio, manda el
        /// procedimiento.
        /// </summary>
        private Venta ConstruirVenta(decimal totalVenta, List<ParticipanteVenta> participantes)
        {
            return new Venta
            {
                oUsuario = new Usuario { IdUsuario = _usuario.IdUsuario },
                oCliente = new Cliente { CodigoCliente = _carrito.CodigoCliente },
                oParticipantes = participantes,
                NombreCliente = _carrito.NombreCliente,
                ModoPago = cmbModoPago.SelectedItem.ToString(),
                NumeroFact = ObtenerNumeroFactura(),
                MontoTotal = totalVenta,
                FechaRegistro = DateTime.Now
            };
        }

        /// <summary>
        /// Numero de factura de referencia.
        ///
        /// CD_Venta.ObtenerCorrelativo() sigue usando "select count(*) + 1".
        /// Ya no es peligroso: usp_RegistrarVenta verifica que el numero este
        /// libre y, si no lo esta, toma el siguiente de dbo.SEQ_NumeroFactura.
        /// Con la restriccion UNIQUE de la tabla, duplicarlo es imposible.
        /// </summary>
        private int ObtenerNumeroFactura()
        {
            try
            {
                int correlativo = new CN_Venta().ObtenerCorrelativo();
                return correlativo > 0 ? correlativo : 1;
            }
            catch (Exception ex)
            {
                Mensajes.Error("No se pudo obtener el numero de factura.", ex);
                return 1;
            }
        }

        private void MostrarComprobante(int numeroFactura, decimal total, decimal presupuestoUsado,
                                        decimal extra, List<ParticipanteVenta> participantes)
        {
            string reparto = string.Join("\n", participantes.Select(p =>
                string.Format("   {0} - {1}: {2}   (saldo: {3})",
                              p.CodigoCliente, p.Nombre,
                              Formato.Moneda(p.Asignado),
                              Formato.Moneda(p.SaldoDespues))));

            Mensajes.Info(string.Format(
                "Venta registrada correctamente.\n\n" +
                "Numero de factura : {0}\n" +
                "Total de venta    : {1}\n" +
                "Presupuesto usado : {2}\n" +
                "Pago del cliente  : {3}\n\n" +
                "Reparto:\n{4}",
                numeroFactura,
                Formato.Moneda(total),
                Formato.Moneda(presupuestoUsado),
                Formato.Moneda(extra),
                reparto));
        }

        #endregion

        #region Limpiar

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (_carrito.EstaVacio && !_carrito.HayCliente) return;

            if (!Mensajes.Confirmar("Desea borrar la venta actual y limpiar el formulario?"))
                return;

            LimpiarVenta();
        }

        private void LimpiarVenta()
        {
            _carrito.Limpiar();
            _carrito.QuitarTodosLosParticipantes();

            LimpiarCajaCodigoCliente();

            _actualizandoCliente = true;
            try
            {
                txtNombreCompleto.Clear();
            }
            finally
            {
                _actualizandoCliente = false;
            }

            txtCodigo.Clear();

            if (cmbModoPago.Items.Count > 0) cmbModoPago.SelectedIndex = 0;

            RefrescarGrid();

            ActiveControl = txtQRCliente;
            txtQRCliente.Focus();
        }

        #endregion
    }
}
