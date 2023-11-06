namespace Hash
{
    partial class Form_Contenedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Contenedor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu_Hash = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Custodia = new System.Windows.Forms.ToolStripMenuItem();
            this.Login = new System.Windows.Forms.ToolStripMenuItem();
            this.CambiarContrasena = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.Enabled = false;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Hash,
            this.Menu_Custodia,
            this.Login,
            this.CambiarContrasena});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1532, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu_Hash
            // 
            this.Menu_Hash.BackColor = System.Drawing.Color.SteelBlue;
            this.Menu_Hash.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Hash.ForeColor = System.Drawing.Color.White;
            this.Menu_Hash.Name = "Menu_Hash";
            this.Menu_Hash.Size = new System.Drawing.Size(57, 28);
            this.Menu_Hash.Text = "Hash";
            this.Menu_Hash.Click += new System.EventHandler(this.Form_Hash_Click);
            // 
            // Menu_Custodia
            // 
            this.Menu_Custodia.BackColor = System.Drawing.Color.SteelBlue;
            this.Menu_Custodia.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Custodia.ForeColor = System.Drawing.Color.White;
            this.Menu_Custodia.Name = "Menu_Custodia";
            this.Menu_Custodia.Size = new System.Drawing.Size(87, 28);
            this.Menu_Custodia.Text = "Custodia";
            this.Menu_Custodia.Click += new System.EventHandler(this.Form_Custodia_Click);
            // 
            // Login
            // 
            this.Login.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Login.BackColor = System.Drawing.Color.SteelBlue;
            this.Login.Enabled = false;
            this.Login.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login.ForeColor = System.Drawing.Color.White;
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(117, 28);
            this.Login.Text = "Cierra sesión";
            this.Login.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // CambiarContrasena
            // 
            this.CambiarContrasena.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CambiarContrasena.BackColor = System.Drawing.Color.SteelBlue;
            this.CambiarContrasena.Enabled = false;
            this.CambiarContrasena.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CambiarContrasena.ForeColor = System.Drawing.Color.White;
            this.CambiarContrasena.Name = "CambiarContrasena";
            this.CambiarContrasena.Size = new System.Drawing.Size(176, 28);
            this.CambiarContrasena.Text = "Cambiar Contraseña";
            // 
            // Form_Contenedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1532, 778);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Contenedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hash Copy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Menu_Hash;
        private System.Windows.Forms.ToolStripMenuItem Menu_Custodia;
        private System.Windows.Forms.ToolStripMenuItem Login;
        private System.Windows.Forms.ToolStripMenuItem CambiarContrasena;
    }

    
}

