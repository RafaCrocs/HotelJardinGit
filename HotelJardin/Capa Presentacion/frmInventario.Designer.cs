namespace Capa_Presentacion
{
    partial class frmInventario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridInventario = new System.Windows.Forms.DataGridView();
            this.dsa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdInventario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblfrm1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInventarioCantidad = new System.Windows.Forms.TextBox();
            this.txtInventarioProveedor = new System.Windows.Forms.TextBox();
            this.txtInventarioDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.frmUsuario1 = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtInventarioPrecio = new System.Windows.Forms.TextBox();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.frmInicioGuardar = new FontAwesome.Sharp.IconButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInventarioCodigo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buscar = new FontAwesome.Sharp.IconButton();
            this.limpiarBuscar = new FontAwesome.Sharp.IconButton();
            this.txtbusqueda = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbBoxGuardar = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(292, 74);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(38, 20);
            this.txtId.TabIndex = 31;
            this.txtId.Text = "0";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(367, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(293, 34);
            this.label5.TabIndex = 30;
            this.label5.Text = "Lista de Articulos";
            // 
            // dataGridInventario
            // 
            this.dataGridInventario.AllowUserToAddRows = false;
            this.dataGridInventario.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridInventario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dsa,
            this.IdInventario,
            this.Codigo,
            this.Descripcion,
            this.Proveedor,
            this.Cantidad,
            this.Precio});
            this.dataGridInventario.Location = new System.Drawing.Point(369, 97);
            this.dataGridInventario.MultiSelect = false;
            this.dataGridInventario.Name = "dataGridInventario";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridInventario.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridInventario.RowTemplate.Height = 28;
            this.dataGridInventario.Size = new System.Drawing.Size(1155, 678);
            this.dataGridInventario.TabIndex = 29;
            this.dataGridInventario.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridInventario_CellContentClick);
            this.dataGridInventario.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridInventario_CellPainting);
            // 
            // dsa
            // 
            this.dsa.HeaderText = "";
            this.dsa.Name = "dsa";
            this.dsa.Width = 30;
            // 
            // IdInventario
            // 
            this.IdInventario.HeaderText = "Id nventario";
            this.IdInventario.Name = "IdInventario";
            this.IdInventario.Visible = false;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.Width = 80;
            // 
            // Descripcion
            // 
            this.Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            // 
            // Proveedor
            // 
            this.Proveedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Proveedor.HeaderText = "Proveedor";
            this.Proveedor.Name = "Proveedor";
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Width = 90;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 80;
            // 
            // lblfrm1
            // 
            this.lblfrm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfrm1.Location = new System.Drawing.Point(23, 19);
            this.lblfrm1.Name = "lblfrm1";
            this.lblfrm1.Size = new System.Drawing.Size(338, 47);
            this.lblfrm1.TabIndex = 28;
            this.lblfrm1.Text = "Agregar Articulo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-221, 262);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 18);
            this.label4.TabIndex = 27;
            this.label4.Text = "Rol";
            // 
            // txtInventarioCantidad
            // 
            this.txtInventarioCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInventarioCantidad.Location = new System.Drawing.Point(30, 402);
            this.txtInventarioCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtInventarioCantidad.Name = "txtInventarioCantidad";
            this.txtInventarioCantidad.Size = new System.Drawing.Size(301, 23);
            this.txtInventarioCantidad.TabIndex = 22;
            // 
            // txtInventarioProveedor
            // 
            this.txtInventarioProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInventarioProveedor.Location = new System.Drawing.Point(30, 326);
            this.txtInventarioProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtInventarioProveedor.Name = "txtInventarioProveedor";
            this.txtInventarioProveedor.Size = new System.Drawing.Size(301, 23);
            this.txtInventarioProveedor.TabIndex = 21;
            // 
            // txtInventarioDescripcion
            // 
            this.txtInventarioDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInventarioDescripcion.Location = new System.Drawing.Point(30, 247);
            this.txtInventarioDescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.txtInventarioDescripcion.Name = "txtInventarioDescripcion";
            this.txtInventarioDescripcion.Size = new System.Drawing.Size(301, 23);
            this.txtInventarioDescripcion.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 370);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Cantidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 288);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Proveedor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 207);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 18);
            this.label6.TabIndex = 17;
            this.label6.Text = "Descripcion";
            // 
            // frmUsuario1
            // 
            this.frmUsuario1.Dock = System.Windows.Forms.DockStyle.Left;
            this.frmUsuario1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmUsuario1.Location = new System.Drawing.Point(0, 0);
            this.frmUsuario1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.frmUsuario1.Name = "frmUsuario1";
            this.frmUsuario1.Size = new System.Drawing.Size(346, 805);
            this.frmUsuario1.TabIndex = 16;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.Location = new System.Drawing.Point(27, 439);
            this.lblPrecio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(51, 18);
            this.lblPrecio.TabIndex = 32;
            this.lblPrecio.Text = "Precio";
            // 
            // txtInventarioPrecio
            // 
            this.txtInventarioPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInventarioPrecio.Location = new System.Drawing.Point(30, 478);
            this.txtInventarioPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.txtInventarioPrecio.Name = "txtInventarioPrecio";
            this.txtInventarioPrecio.Size = new System.Drawing.Size(301, 23);
            this.txtInventarioPrecio.TabIndex = 33;
            // 
            // iconButton3
            // 
            this.iconButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.iconButton3.IconColor = System.Drawing.Color.Black;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 30;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(91, 645);
            this.iconButton3.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(172, 43);
            this.iconButton3.TabIndex = 25;
            this.iconButton3.Text = "Eliminar";
            this.iconButton3.UseVisualStyleBackColor = true;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Black;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 30;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(91, 593);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(172, 43);
            this.btnLimpiar.TabIndex = 24;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // frmInicioGuardar
            // 
            this.frmInicioGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmInicioGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.frmInicioGuardar.IconColor = System.Drawing.Color.Black;
            this.frmInicioGuardar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.frmInicioGuardar.IconSize = 30;
            this.frmInicioGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.frmInicioGuardar.Location = new System.Drawing.Point(91, 542);
            this.frmInicioGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.frmInicioGuardar.Name = "frmInicioGuardar";
            this.frmInicioGuardar.Size = new System.Drawing.Size(172, 43);
            this.frmInicioGuardar.TabIndex = 23;
            this.frmInicioGuardar.Text = "Guardar";
            this.frmInicioGuardar.UseVisualStyleBackColor = true;
            this.frmInicioGuardar.Click += new System.EventHandler(this.frmInicioGuardar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 134);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 18);
            this.label7.TabIndex = 34;
            this.label7.Text = "Codigo";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInventarioCodigo
            // 
            this.txtInventarioCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInventarioCodigo.Location = new System.Drawing.Point(30, 165);
            this.txtInventarioCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtInventarioCodigo.Name = "txtInventarioCodigo";
            this.txtInventarioCodigo.Size = new System.Drawing.Size(301, 23);
            this.txtInventarioCodigo.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(367, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1158, 60);
            this.label8.TabIndex = 36;
            // 
            // buscar
            // 
            this.buscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.buscar.IconColor = System.Drawing.Color.Black;
            this.buscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.buscar.IconSize = 23;
            this.buscar.Location = new System.Drawing.Point(1415, 37);
            this.buscar.Name = "buscar";
            this.buscar.Size = new System.Drawing.Size(41, 29);
            this.buscar.TabIndex = 37;
            this.buscar.UseVisualStyleBackColor = true;
            this.buscar.Click += new System.EventHandler(this.buscar_Click);
            // 
            // limpiarBuscar
            // 
            this.limpiarBuscar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.limpiarBuscar.IconColor = System.Drawing.Color.Black;
            this.limpiarBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.limpiarBuscar.IconSize = 23;
            this.limpiarBuscar.Location = new System.Drawing.Point(1462, 37);
            this.limpiarBuscar.Name = "limpiarBuscar";
            this.limpiarBuscar.Size = new System.Drawing.Size(41, 29);
            this.limpiarBuscar.TabIndex = 38;
            this.limpiarBuscar.UseVisualStyleBackColor = true;
            this.limpiarBuscar.Click += new System.EventHandler(this.limpiarBuscar_Click);
            // 
            // txtbusqueda
            // 
            this.txtbusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbusqueda.Location = new System.Drawing.Point(1249, 37);
            this.txtbusqueda.Name = "txtbusqueda";
            this.txtbusqueda.Size = new System.Drawing.Size(160, 27);
            this.txtbusqueda.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1164, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 23);
            this.label9.TabIndex = 40;
            this.label9.Text = "Buscar:";
            // 
            // cmbBoxGuardar
            // 
            this.cmbBoxGuardar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBoxGuardar.FormattingEnabled = true;
            this.cmbBoxGuardar.Location = new System.Drawing.Point(992, 37);
            this.cmbBoxGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBoxGuardar.Name = "cmbBoxGuardar";
            this.cmbBoxGuardar.Size = new System.Drawing.Size(165, 28);
            this.cmbBoxGuardar.TabIndex = 41;
            // 
            // frmInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1567, 805);
            this.Controls.Add(this.cmbBoxGuardar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtbusqueda);
            this.Controls.Add(this.limpiarBuscar);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.txtInventarioCodigo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtInventarioPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblfrm1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.iconButton3);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.frmInicioGuardar);
            this.Controls.Add(this.txtInventarioCantidad);
            this.Controls.Add(this.txtInventarioProveedor);
            this.Controls.Add(this.txtInventarioDescripcion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.frmUsuario1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridInventario);
            this.Name = "frmInventario";
            this.Text = "frmInventario";
            this.Load += new System.EventHandler(this.frmInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInventario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridInventario;
        private System.Windows.Forms.Label lblfrm1;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton frmInicioGuardar;
        private System.Windows.Forms.TextBox txtInventarioCantidad;
        private System.Windows.Forms.TextBox txtInventarioProveedor;
        private System.Windows.Forms.TextBox txtInventarioDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label frmUsuario1;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtInventarioPrecio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInventarioCodigo;
        private System.Windows.Forms.Label label8;
        private FontAwesome.Sharp.IconButton buscar;
        private FontAwesome.Sharp.IconButton limpiarBuscar;
        private System.Windows.Forms.TextBox txtbusqueda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbBoxGuardar;
        private System.Windows.Forms.DataGridViewButtonColumn dsa;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdInventario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
    }
}