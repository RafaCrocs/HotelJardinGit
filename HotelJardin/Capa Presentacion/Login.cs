using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;

namespace Capa_Presentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btnLoginIngresar_Click(object sender, EventArgs e)
        {

            List<Usuario> test = new CN_Usuario().Listar();
            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.NombreCompleto == txtLoginUsuario.Text && u.Clave == txtLoginContra.Text).FirstOrDefault();
            if (oUsuario != null)
            {
                Inicio form = new Inicio(oUsuario);
                form.Show();
                this.Hide();
                txtLoginContra.Clear();
                txtLoginUsuario.Clear();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrecta", "wuamp wuamp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            
        }
        private void btnLoginSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxContra_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxContra.Checked)
            {
                txtLoginContra.UseSystemPasswordChar = false;
            }
            else
            {
                txtLoginContra.UseSystemPasswordChar = true;
            }
        }

    }
}
