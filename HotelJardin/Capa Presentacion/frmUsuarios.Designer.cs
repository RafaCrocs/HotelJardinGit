namespace Capa_Presentacion
{
    partial class frmUsuarios
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
            this.frmUsuario1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtfrmNombreCompleto = new System.Windows.Forms.TextBox();
            this.textfrmContraseña = new System.Windows.Forms.TextBox();
            this.txtfrmConfirmar = new System.Windows.Forms.TextBox();
            this.frmInicioGuardar = new FontAwesome.Sharp.IconButton();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.cmbboxRol = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblfrm1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.asd = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // frmUsuario1
            // 
            this.frmUsuario1.BackColor = System.Drawing.Color.White;
            this.frmUsuario1.Dock = System.Windows.Forms.DockStyle.Left;
            this.frmUsuario1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmUsuario1.Location = new System.Drawing.Point(0, 0);
            this.frmUsuario1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.frmUsuario1.Name = "frmUsuario1";
            this.frmUsuario1.Size = new System.Drawing.Size(346, 669);
            this.frmUsuario1.TabIndex = 0;
            this.frmUsuario1.Click += new System.EventHandler(this.frmUsuario1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre Completo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 188);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 265);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Confirmar Contraseña";
            // 
            // txtfrmNombreCompleto
            // 
            this.txtfrmNombreCompleto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfrmNombreCompleto.Location = new System.Drawing.Point(13, 143);
            this.txtfrmNombreCompleto.Margin = new System.Windows.Forms.Padding(4);
            this.txtfrmNombreCompleto.Name = "txtfrmNombreCompleto";
            this.txtfrmNombreCompleto.Size = new System.Drawing.Size(301, 23);
            this.txtfrmNombreCompleto.TabIndex = 4;
            // 
            // textfrmContraseña
            // 
            this.textfrmContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textfrmContraseña.Location = new System.Drawing.Point(13, 211);
            this.textfrmContraseña.Margin = new System.Windows.Forms.Padding(4);
            this.textfrmContraseña.Name = "textfrmContraseña";
            this.textfrmContraseña.Size = new System.Drawing.Size(301, 23);
            this.textfrmContraseña.TabIndex = 5;
            // 
            // txtfrmConfirmar
            // 
            this.txtfrmConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfrmConfirmar.Location = new System.Drawing.Point(13, 287);
            this.txtfrmConfirmar.Margin = new System.Windows.Forms.Padding(4);
            this.txtfrmConfirmar.Name = "txtfrmConfirmar";
            this.txtfrmConfirmar.Size = new System.Drawing.Size(301, 23);
            this.txtfrmConfirmar.TabIndex = 6;
            // 
            // frmInicioGuardar
            // 
            this.frmInicioGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmInicioGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.frmInicioGuardar.IconColor = System.Drawing.Color.Black;
            this.frmInicioGuardar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.frmInicioGuardar.IconSize = 30;
            this.frmInicioGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.frmInicioGuardar.Location = new System.Drawing.Point(16, 421);
            this.frmInicioGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.frmInicioGuardar.Name = "frmInicioGuardar";
            this.frmInicioGuardar.Size = new System.Drawing.Size(172, 43);
            this.frmInicioGuardar.TabIndex = 7;
            this.frmInicioGuardar.Text = "Guardar";
            this.frmInicioGuardar.UseVisualStyleBackColor = true;
            this.frmInicioGuardar.Click += new System.EventHandler(this.frmInicioGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Black;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 30;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(16, 472);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(172, 43);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // iconButton3
            // 
            this.iconButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.iconButton3.IconColor = System.Drawing.Color.Black;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 30;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(16, 524);
            this.iconButton3.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(172, 43);
            this.iconButton3.TabIndex = 9;
            this.iconButton3.Text = "Eliminar";
            this.iconButton3.UseVisualStyleBackColor = true;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // cmbboxRol
            // 
            this.cmbboxRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbboxRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbboxRol.FormattingEnabled = true;
            this.cmbboxRol.Location = new System.Drawing.Point(13, 365);
            this.cmbboxRol.Margin = new System.Windows.Forms.Padding(4);
            this.cmbboxRol.Name = "cmbboxRol";
            this.cmbboxRol.Size = new System.Drawing.Size(301, 24);
            this.cmbboxRol.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 343);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Rol";
            // 
            // lblfrm1
            // 
            this.lblfrm1.BackColor = System.Drawing.Color.White;
            this.lblfrm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfrm1.Location = new System.Drawing.Point(6, 41);
            this.lblfrm1.Name = "lblfrm1";
            this.lblfrm1.Size = new System.Drawing.Size(338, 47);
            this.lblfrm1.TabIndex = 12;
            this.lblfrm1.Text = "Creacion de Usuario";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.asd,
            this.IdUsuario,
            this.NombreCompleto,
            this.Clave,
            this.IdRol,
            this.Rol});
            this.dataGridView1.Location = new System.Drawing.Point(376, 131);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(892, 483);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // asd
            // 
            this.asd.HeaderText = "";
            this.asd.Name = "asd";
            this.asd.Width = 30;
            // 
            // IdUsuario
            // 
            this.IdUsuario.HeaderText = "Id Usuario";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.Visible = false;
            this.IdUsuario.Width = 80;
            // 
            // NombreCompleto
            // 
            this.NombreCompleto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NombreCompleto.HeaderText = "Nombre Completo";
            this.NombreCompleto.Name = "NombreCompleto";
            // 
            // Clave
            // 
            this.Clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            // 
            // IdRol
            // 
            this.IdRol.HeaderText = "IdRol";
            this.IdRol.Name = "IdRol";
            // 
            // Rol
            // 
            this.Rol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rol.HeaderText = "Rol";
            this.Rol.Name = "Rol";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(376, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(892, 51);
            this.label5.TabIndex = 14;
            this.label5.Text = "Lista de Usuarios";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(275, 96);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(38, 24);
            this.txtId.TabIndex = 15;
            this.txtId.Text = "0";
            // 
            // frmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1379, 669);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblfrm1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbboxRol);
            this.Controls.Add(this.iconButton3);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.frmInicioGuardar);
            this.Controls.Add(this.txtfrmConfirmar);
            this.Controls.Add(this.textfrmContraseña);
            this.Controls.Add(this.txtfrmNombreCompleto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.frmUsuario1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUsuarios";
            this.Text = "frmUsuarios";
            this.Load += new System.EventHandler(this.frmUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label frmUsuario1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtfrmNombreCompleto;
        private System.Windows.Forms.TextBox textfrmContraseña;
        private System.Windows.Forms.TextBox txtfrmConfirmar;
        private FontAwesome.Sharp.IconButton frmInicioGuardar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton iconButton3;
        private System.Windows.Forms.ComboBox cmbboxRol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblfrm1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.DataGridViewButtonColumn asd;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rol;
    }
}