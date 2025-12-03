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
using FontAwesome.Sharp;
using CapaNegocio;

namespace Capa_Presentacion
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private static Usuario UsuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;

        public Inicio(Usuario objUsuario)
        {
            UsuarioActual = objUsuario; 
            InitializeComponent();
        }

        private void btnIncioSalir_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Close();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(UsuarioActual.IdUsuario);

            foreach(IconMenuItem iconmenu in Menu2.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);
                if (!encontrado)
                {
                    iconmenu.Visible = false;
                }   
            }


            lblInicioUsuario.Text = UsuarioActual.NombreCompleto;
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if(MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;

            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.White;

            Contenedor.Controls.Add(formulario);

            formulario.Show();
        }

        private void MenuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }
        private void MenuVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmVenta(UsuarioActual));
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void MenuInventario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmInventario());
        }

        private void MenuReporte_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReporte());
        }

        private void MenuConfiguracion_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmConfiguraciones());
        }
    }
}
