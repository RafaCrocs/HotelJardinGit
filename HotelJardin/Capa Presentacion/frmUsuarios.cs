using Capa_Presentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Presentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace Capa_Presentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuario1_Click(object sender, EventArgs e)
        {

        }

        private void frmInicioGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;
            try
            {
                Usuario objUsuario = new Usuario()
                {
                    IdUsuario = Convert.ToInt32(txtId.Text),
                    NombreCompleto = txtfrmNombreCompleto.Text,
                    Clave = textfrmContraseña.Text,
                    oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cmbboxRol.SelectedItem).Valor) }
                };

                if (objUsuario.IdUsuario == 0)
                {
                    int IdUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out Mensaje);


                    if (IdUsuarioGenerado != 0)
                    {
                        MessageBox.Show("Usuario registrado con exito");
                        dataGridView1.Rows.Add(new object[] {
                    "",
                    IdUsuarioGenerado,
                    txtfrmNombreCompleto.Text,
                    textfrmContraseña.Text,
                    ((OpcionCombo)cmbboxRol.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cmbboxRol.SelectedItem).Texto.ToString()
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
                    bool resultado = new CN_Usuario().Editar(objUsuario, out Mensaje);
                    if (resultado)
                    {
                        // Buscar la fila correspondiente por IdUsuario (valor de BD)
                        int idUsuario = objUsuario.IdUsuario;
                        DataGridViewRow rowEncontrada = null;
                        foreach (DataGridViewRow r in dataGridView1.Rows)
                        {
                            if (r.Cells["IdUsuario"].Value != null &&
                                int.TryParse(r.Cells["IdUsuario"].Value.ToString(), out int idCell) &&
                                idCell == idUsuario)
                            {
                                rowEncontrada = r;
                                break;
                            }
                        }

                        if (rowEncontrada != null)
                        {
                            rowEncontrada.Cells["NombreCompleto"].Value = txtfrmNombreCompleto.Text;
                            rowEncontrada.Cells["Clave"].Value = textfrmContraseña.Text;
                            rowEncontrada.Cells["IdRol"].Value = ((OpcionCombo)cmbboxRol.SelectedItem).Valor.ToString();
                            rowEncontrada.Cells["Rol"].Value = ((OpcionCombo)cmbboxRol.SelectedItem).Texto.ToString();
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la fila correspondiente al usuario editado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datos Logicos", ex.Message);
            }
        }

        private void Limpiar()
        {
            txtId.Text = "";
            txtfrmNombreCompleto.Text = "";
            textfrmContraseña.Text = "";
            txtfrmConfirmar.Text = "";
            cmbboxRol.SelectedIndex = 0;
        }
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                cmbboxRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }

            cmbboxRol.DisplayMember = "Texto";
            cmbboxRol.ValueMember = "Valor";
            cmbboxRol.SelectedIndex = 0;


            //Mostrar usuarios en el datagridview
            List<Usuario> listaUsuario = new CN_Usuario().Listar();
            foreach (Usuario item in listaUsuario)
            {
                dataGridView1.Rows.Add(new object[] {
                "",
                item.IdUsuario,
                item.NombreCompleto,
                item.Clave,
                item.oRol.IdRol,
                item.oRol.Descripcion,
                });


            }


        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Obtener el índice actual de la columna por nombre en vez de asumir 0
            var iconCol = dataGridView1.Columns["asd"];
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "asd")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtId.Text = dataGridView1.Rows[indice].Cells["IdUsuario"].Value.ToString();
                    txtfrmNombreCompleto.Text = dataGridView1.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    textfrmContraseña.Text = dataGridView1.Rows[indice].Cells["Clave"].Value.ToString();
                    txtfrmConfirmar.Text = dataGridView1.Rows[indice].Cells["Clave"].Value.ToString();
                    cmbboxRol.SelectedValue = dataGridView1.Rows[indice].Cells["IdRol"].Value.ToString();


                    foreach (OpcionCombo oc in cmbboxRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataGridView1.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cmbboxRol.Items.IndexOf(oc);
                            cmbboxRol.SelectedIndex = indice_combo;
                            break;

                            ///
                            ///
                            /// 34Min Cap 5
                            ///

                        }
                    }
                }
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtId.Text) != 0)
            {
                if(MessageBox.Show("¿Desea eliminar el usuario?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtId.Text)
                    };
                    bool resultado = new CN_Usuario().Eliminar(objUsuario, out Mensaje);
                    if (resultado)
                    {
                        // Buscar la fila correspondiente por IdUsuario (valor de BD)
                        int idUsuario = objUsuario.IdUsuario;
                        DataGridViewRow rowEncontrada = null;
                        foreach (DataGridViewRow r in dataGridView1.Rows)
                        {
                            if (r.Cells["IdUsuario"].Value != null &&
                                int.TryParse(r.Cells["IdUsuario"].Value.ToString(), out int idCell) &&
                                idCell == idUsuario)
                            {
                                rowEncontrada = r;
                                break;
                            }
                        }
                        if (rowEncontrada != null)
                        {
                            dataGridView1.Rows.Remove(rowEncontrada);
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la fila correspondiente al usuario eliminado.");
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
            if(MessageBox.Show("¿Desea limpiar el formulario?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar();
            }
        }
    }
}
