namespace Capa_Presentacion
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.lblLoginUsuario = new System.Windows.Forms.Label();
            this.lblLogin1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnLoginIngresar = new FontAwesome.Sharp.IconButton();
            this.btnLoginSalir = new FontAwesome.Sharp.IconButton();
            this.checkBoxContra = new System.Windows.Forms.CheckBox();
            this.txtLoginContra = new System.Windows.Forms.TextBox();
            this.txtLoginUsuario = new System.Windows.Forms.TextBox();
            this.lblLoginUsu = new System.Windows.Forms.Label();
            this.lblLoginContra = new System.Windows.Forms.Label();
            this.lblIniciodeSesion = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoginUsuario
            // 
            this.lblLoginUsuario.BackColor = System.Drawing.Color.SeaGreen;
            this.lblLoginUsuario.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLoginUsuario.Location = new System.Drawing.Point(0, 0);
            this.lblLoginUsuario.Name = "lblLoginUsuario";
            this.lblLoginUsuario.Size = new System.Drawing.Size(266, 450);
            this.lblLoginUsuario.TabIndex = 0;
            // 
            // lblLogin1
            // 
            this.lblLogin1.BackColor = System.Drawing.Color.Beige;
            this.lblLogin1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin1.Location = new System.Drawing.Point(-2, 280);
            this.lblLogin1.Name = "lblLogin1";
            this.lblLogin1.Size = new System.Drawing.Size(269, 45);
            this.lblLogin1.TabIndex = 1;
            this.lblLogin1.Text = "Programa Facturacion";
            this.lblLogin1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.SeaGreen;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(244, 155);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.Beige;
            this.panelLogin.Controls.Add(this.btnLoginIngresar);
            this.panelLogin.Controls.Add(this.btnLoginSalir);
            this.panelLogin.Controls.Add(this.checkBoxContra);
            this.panelLogin.Controls.Add(this.txtLoginContra);
            this.panelLogin.Controls.Add(this.txtLoginUsuario);
            this.panelLogin.Controls.Add(this.lblLoginUsu);
            this.panelLogin.Controls.Add(this.lblLoginContra);
            this.panelLogin.Controls.Add(this.lblIniciodeSesion);
            this.panelLogin.Controls.Add(this.pictureBox2);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLogin.Location = new System.Drawing.Point(266, 0);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(534, 450);
            this.panelLogin.TabIndex = 3;
            // 
            // btnLoginIngresar
            // 
            this.btnLoginIngresar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnLoginIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginIngresar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLoginIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginIngresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginIngresar.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.btnLoginIngresar.IconColor = System.Drawing.Color.Black;
            this.btnLoginIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLoginIngresar.IconSize = 25;
            this.btnLoginIngresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoginIngresar.Location = new System.Drawing.Point(218, 312);
            this.btnLoginIngresar.Name = "btnLoginIngresar";
            this.btnLoginIngresar.Size = new System.Drawing.Size(112, 40);
            this.btnLoginIngresar.TabIndex = 9;
            this.btnLoginIngresar.Text = "Ingresar";
            this.btnLoginIngresar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoginIngresar.UseVisualStyleBackColor = false;
            this.btnLoginIngresar.Click += new System.EventHandler(this.btnLoginIngresar_Click);
            // 
            // btnLoginSalir
            // 
            this.btnLoginSalir.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnLoginSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLoginSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginSalir.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnLoginSalir.IconColor = System.Drawing.Color.Black;
            this.btnLoginSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLoginSalir.Location = new System.Drawing.Point(434, 23);
            this.btnLoginSalir.Name = "btnLoginSalir";
            this.btnLoginSalir.Size = new System.Drawing.Size(88, 37);
            this.btnLoginSalir.TabIndex = 7;
            this.btnLoginSalir.Text = "Salir";
            this.btnLoginSalir.UseVisualStyleBackColor = false;
            this.btnLoginSalir.Click += new System.EventHandler(this.btnLoginSalir_Click);
            // 
            // checkBoxContra
            // 
            this.checkBoxContra.AutoSize = true;
            this.checkBoxContra.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.checkBoxContra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxContra.Location = new System.Drawing.Point(91, 269);
            this.checkBoxContra.Name = "checkBoxContra";
            this.checkBoxContra.Size = new System.Drawing.Size(140, 24);
            this.checkBoxContra.TabIndex = 5;
            this.checkBoxContra.Text = "Ver Contraseña";
            this.checkBoxContra.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxContra.UseVisualStyleBackColor = false;
            this.checkBoxContra.CheckedChanged += new System.EventHandler(this.checkBoxContra_CheckedChanged);
            // 
            // txtLoginContra
            // 
            this.txtLoginContra.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtLoginContra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginContra.Location = new System.Drawing.Point(91, 240);
            this.txtLoginContra.Name = "txtLoginContra";
            this.txtLoginContra.Size = new System.Drawing.Size(215, 23);
            this.txtLoginContra.TabIndex = 4;
            this.txtLoginContra.UseSystemPasswordChar = true;
            // 
            // txtLoginUsuario
            // 
            this.txtLoginUsuario.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtLoginUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginUsuario.Location = new System.Drawing.Point(91, 165);
            this.txtLoginUsuario.Name = "txtLoginUsuario";
            this.txtLoginUsuario.Size = new System.Drawing.Size(215, 23);
            this.txtLoginUsuario.TabIndex = 4;
            // 
            // lblLoginUsu
            // 
            this.lblLoginUsu.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblLoginUsu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoginUsu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginUsu.Location = new System.Drawing.Point(91, 139);
            this.lblLoginUsu.Name = "lblLoginUsu";
            this.lblLoginUsu.Size = new System.Drawing.Size(100, 32);
            this.lblLoginUsu.TabIndex = 2;
            this.lblLoginUsu.Text = "Usuario:";
            // 
            // lblLoginContra
            // 
            this.lblLoginContra.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblLoginContra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoginContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginContra.Location = new System.Drawing.Point(91, 214);
            this.lblLoginContra.Name = "lblLoginContra";
            this.lblLoginContra.Size = new System.Drawing.Size(128, 30);
            this.lblLoginContra.TabIndex = 1;
            this.lblLoginContra.Text = "Contraseña:";
            // 
            // lblIniciodeSesion
            // 
            this.lblIniciodeSesion.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblIniciodeSesion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIniciodeSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIniciodeSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIniciodeSesion.Location = new System.Drawing.Point(31, 43);
            this.lblIniciodeSesion.Name = "lblIniciodeSesion";
            this.lblIniciodeSesion.Size = new System.Drawing.Size(224, 37);
            this.lblIniciodeSesion.TabIndex = 0;
            this.lblIniciodeSesion.Text = "Inicio de Sesion";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(534, 450);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblLogin1);
            this.Controls.Add(this.lblLoginUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLoginUsuario;
        private System.Windows.Forms.Label lblLogin1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label lblLoginUsu;
        private System.Windows.Forms.Label lblLoginContra;
        private System.Windows.Forms.Label lblIniciodeSesion;
        private System.Windows.Forms.TextBox txtLoginContra;
        private System.Windows.Forms.TextBox txtLoginUsuario;
        private System.Windows.Forms.CheckBox checkBoxContra;
        private FontAwesome.Sharp.IconButton btnLoginSalir;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton btnLoginIngresar;
    }
}