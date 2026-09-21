using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Capa_Presentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace Capa_Presentacion.Modales
{
    public partial class mdInventario : Form
    {
        /// <summary>Articulo elegido, completo. Queda en null si se cierra sin seleccionar.</summary>
        public Inventario _Inventario { get; private set; }

        private List<Inventario> _inventario = new List<Inventario>();

        public mdInventario()
        {
            InitializeComponent();
        }

        /// <summary>
        /// La carga se hace aqui y no en el inicializador del campo.
        /// En el original la lista se llenaba con "new CN_Inventario().Listar()" como
        /// valor inicial del campo, lo que significa que se golpeaba la base de datos
        /// dentro del constructor del formulario: si fallaba, la excepcion salia antes
        /// de que existiera la ventana y era imposible mostrar un mensaje decente.
        /// </summary>
        private void mdProducto_Load(object sender, EventArgs e)
        {
            GridHelper.PrepararParaBinding(dataGridInventario);
            CargarGrid();
            txtbusqueda.Focus();
        }

        private void CargarGrid()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _inventario = new CN_Inventario().Listar() ?? new List<Inventario>();
                dataGridInventario.DataSource = _inventario;
            }
            catch (Exception ex)
            {
                Mensajes.Error("No se pudo cargar el inventario.", ex);
                _inventario = new List<Inventario>();
                dataGridInventario.DataSource = _inventario;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void dataGridInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            Inventario seleccionado = dataGridInventario.Rows[e.RowIndex].DataBoundItem as Inventario;

            if (seleccionado == null)
            {
                Mensajes.Advertencia("No se pudo leer el articulo seleccionado.");
                return;
            }

            if (seleccionado.Cantidad <= 0)
            {
                Mensajes.Advertencia(
                    string.Format("\"{0}\" no tiene existencias disponibles.",
                                  seleccionado.Descripcion));
                return;
            }

            _Inventario = seleccionado;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {
            string filtro = (txtbusqueda.Text ?? string.Empty).Trim().ToLower();

            if (filtro.Length < 3)
            {
                dataGridInventario.DataSource = _inventario;
                return;
            }

            List<Inventario> filtrados = _inventario.Where(i =>
                (i.Descripcion ?? string.Empty).ToLower().Contains(filtro) ||
                (i.Codigo ?? string.Empty).ToLower().Contains(filtro) ||
                (i.Proveedor ?? string.Empty).ToLower().Contains(filtro)).ToList();

            dataGridInventario.DataSource = filtrados;
        }
    }
}
