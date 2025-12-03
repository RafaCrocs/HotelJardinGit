using System.Drawing;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Menu2 = new System.Windows.Forms.MenuStrip();
            this.MenuUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.MenuVenta = new FontAwesome.Sharp.IconMenuItem();
            this.MenuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.MenuInventario = new FontAwesome.Sharp.IconMenuItem();
            this.MenuReporte = new FontAwesome.Sharp.IconMenuItem();
            this.MenuConfiguracion = new FontAwesome.Sharp.IconMenuItem();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.btnIncioSalir = new FontAwesome.Sharp.IconMenuItem();
            this.lblInicioUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.lblMenu = new System.Windows.Forms.Label();
            this.Contenedor = new System.Windows.Forms.Panel();
            this.Menu2.SuspendLayout();
            this.Menu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu2
            // 
            this.Menu2.AutoSize = false;
            this.Menu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUsuario,
            this.MenuVenta,
            this.MenuClientes,
            this.MenuInventario,
            this.MenuReporte,
            this.MenuConfiguracion});
            this.Menu2.Location = new System.Drawing.Point(0, 67);
            this.Menu2.Name = "Menu2";
            this.Menu2.Size = new System.Drawing.Size(961, 77);
            this.Menu2.TabIndex = 0;
            this.Menu2.Text = "menuStrip1";
            // 
            // MenuUsuario
            // 
            this.MenuUsuario.AutoSize = false;
            this.MenuUsuario.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MenuUsuario.IconChar = FontAwesome.Sharp.IconChar.Person;
            this.MenuUsuario.IconColor = System.Drawing.Color.Black;
            this.MenuUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuUsuario.IconSize = 45;
            this.MenuUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuUsuario.Name = "MenuUsuario";
            this.MenuUsuario.Size = new System.Drawing.Size(155, 60);
            this.MenuUsuario.Text = "Usuarios";
            this.MenuUsuario.Click += new System.EventHandler(this.MenuUsuario_Click);
            // 
            // MenuVenta
            // 
            this.MenuVenta.AutoSize = false;
            this.MenuVenta.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MenuVenta.IconChar = FontAwesome.Sharp.IconChar.MoneyBill;
            this.MenuVenta.IconColor = System.Drawing.Color.Black;
            this.MenuVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuVenta.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuVenta.Name = "MenuVenta";
            this.MenuVenta.Size = new System.Drawing.Size(155, 52);
            this.MenuVenta.Text = "Venta";
            this.MenuVenta.Click += new System.EventHandler(this.MenuVenta_Click);
            // 
            // MenuClientes
            // 
            this.MenuClientes.AutoSize = false;
            this.MenuClientes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MenuClientes.IconChar = FontAwesome.Sharp.IconChar.PeopleGroup;
            this.MenuClientes.IconColor = System.Drawing.Color.Black;
            this.MenuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuClientes.Name = "MenuClientes";
            this.MenuClientes.Size = new System.Drawing.Size(155, 52);
            this.MenuClientes.Text = "Clientes";
            this.MenuClientes.Click += new System.EventHandler(this.MenuClientes_Click);
            // 
            // MenuInventario
            // 
            this.MenuInventario.AutoSize = false;
            this.MenuInventario.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MenuInventario.IconChar = FontAwesome.Sharp.IconChar.BoxOpen;
            this.MenuInventario.IconColor = System.Drawing.Color.Black;
            this.MenuInventario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuInventario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuInventario.Name = "MenuInventario";
            this.MenuInventario.Size = new System.Drawing.Size(155, 52);
            this.MenuInventario.Text = "Inventario";
            this.MenuInventario.Click += new System.EventHandler(this.MenuInventario_Click);
            // 
            // MenuReporte
            // 
            this.MenuReporte.AutoSize = false;
            this.MenuReporte.AutoToolTip = true;
            this.MenuReporte.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MenuReporte.IconChar = FontAwesome.Sharp.IconChar.Newspaper;
            this.MenuReporte.IconColor = System.Drawing.Color.Black;
            this.MenuReporte.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuReporte.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuReporte.Name = "MenuReporte";
            this.MenuReporte.Size = new System.Drawing.Size(155, 52);
            this.MenuReporte.Text = "Reporte";
            this.MenuReporte.Click += new System.EventHandler(this.MenuReporte_Click);
            // 
            // MenuConfiguracion
            // 
            this.MenuConfiguracion.AutoSize = false;
            this.MenuConfiguracion.AutoToolTip = true;
            this.MenuConfiguracion.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MenuConfiguracion.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.MenuConfiguracion.IconColor = System.Drawing.Color.Black;
            this.MenuConfiguracion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuConfiguracion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuConfiguracion.Name = "MenuConfiguracion";
            this.MenuConfiguracion.Size = new System.Drawing.Size(180, 52);
            this.MenuConfiguracion.Text = "Configuraciones";
            this.MenuConfiguracion.Click += new System.EventHandler(this.MenuConfiguracion_Click);
            // 
            // Menu1
            // 
            this.Menu1.AutoSize = false;
            this.Menu1.BackColor = System.Drawing.Color.SeaGreen;
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIncioSalir,
            this.lblInicioUsuario});
            this.Menu1.Location = new System.Drawing.Point(0, 0);
            this.Menu1.Name = "Menu1";
            this.Menu1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Menu1.Size = new System.Drawing.Size(961, 67);
            this.Menu1.TabIndex = 1;
            this.Menu1.Text = "menuStrip2";
            // 
            // btnIncioSalir
            // 
            this.btnIncioSalir.AutoSize = false;
            this.btnIncioSalir.Font = new System.Drawing.Font("Segoe UI", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnIncioSalir.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.btnIncioSalir.IconColor = System.Drawing.Color.Black;
            this.btnIncioSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnIncioSalir.IconSize = 30;
            this.btnIncioSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIncioSalir.Name = "btnIncioSalir";
            this.btnIncioSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnIncioSalir.Size = new System.Drawing.Size(110, 63);
            this.btnIncioSalir.Text = "Salir";
            this.btnIncioSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncioSalir.Click += new System.EventHandler(this.btnIncioSalir_Click);
            // 
            // lblInicioUsuario
            // 
            this.lblInicioUsuario.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblInicioUsuario.IconChar = FontAwesome.Sharp.IconChar.None;
            this.lblInicioUsuario.IconColor = System.Drawing.Color.Black;
            this.lblInicioUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.lblInicioUsuario.Name = "lblInicioUsuario";
            this.lblInicioUsuario.Size = new System.Drawing.Size(180, 63);
            this.lblInicioUsuario.Text = "NombreUsuario";
            // 
            // lblMenu
            // 
            this.lblMenu.BackColor = System.Drawing.Color.SeaGreen;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.Location = new System.Drawing.Point(23, 20);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(333, 30);
            this.lblMenu.TabIndex = 2;
            this.lblMenu.Text = "Programa de Facturacion";
            // 
            // Contenedor
            // 
            this.Contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Contenedor.Location = new System.Drawing.Point(0, 144);
            this.Contenedor.Name = "Contenedor";
            this.Contenedor.Size = new System.Drawing.Size(961, 346);
            this.Contenedor.TabIndex = 3;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 490);
            this.Controls.Add(this.Contenedor);
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.Menu2);
            this.Controls.Add(this.Menu1);
            this.MainMenuStrip = this.Menu2;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.Menu2.ResumeLayout(false);
            this.Menu2.PerformLayout();
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu2;
        private System.Windows.Forms.MenuStrip Menu1;
        private System.Windows.Forms.Label lblMenu;
        private FontAwesome.Sharp.IconMenuItem MenuUsuario;
        private FontAwesome.Sharp.IconMenuItem MenuVenta;
        private FontAwesome.Sharp.IconMenuItem MenuClientes;
        private FontAwesome.Sharp.IconMenuItem MenuInventario;
        private FontAwesome.Sharp.IconMenuItem MenuReporte;
        private FontAwesome.Sharp.IconMenuItem MenuConfiguracion;
        private Panel Contenedor;
        private FontAwesome.Sharp.IconMenuItem btnIncioSalir;
        private FontAwesome.Sharp.IconMenuItem lblInicioUsuario;
    }
}

