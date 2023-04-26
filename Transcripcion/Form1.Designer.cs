namespace Transcripcion
{
    partial class Trancripciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trancripciones));
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listaCanciones = new System.Windows.Forms.ListBox();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.Reproductor = new AxWMPLib.AxWindowsMediaPlayer();
            this.listBoxConversacion = new System.Windows.Forms.ListBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btneEliminar = new System.Windows.Forms.Button();
            this.radioButtonA = new System.Windows.Forms.RadioButton();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.Reproductor)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.BackColor = System.Drawing.Color.Green;
            this.buttonAgregar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAgregar.Location = new System.Drawing.Point(791, 431);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(102, 46);
            this.buttonAgregar.TabIndex = 2;
            this.buttonAgregar.Text = "Seleccionar Audio";
            this.buttonAgregar.UseVisualStyleBackColor = false;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonAgregar_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label.Location = new System.Drawing.Point(443, 616);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(140, 13);
            this.label.TabIndex = 3;
            this.label.Text = "Produccón Correa Jonathan";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 478);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(763, 78);
            this.textBox1.TabIndex = 4;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(809, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lista de Reproducción";
            // 
            // listaCanciones
            // 
            this.listaCanciones.AllowDrop = true;
            this.listaCanciones.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.listaCanciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listaCanciones.CausesValidation = false;
            this.listaCanciones.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaCanciones.FormattingEnabled = true;
            this.listaCanciones.ItemHeight = 16;
            this.listaCanciones.Location = new System.Drawing.Point(791, 100);
            this.listaCanciones.Name = "listaCanciones";
            this.listaCanciones.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listaCanciones.Size = new System.Drawing.Size(214, 306);
            this.listaCanciones.TabIndex = 7;
            this.listaCanciones.SelectedIndexChanged += new System.EventHandler(this.listaCanciones_SelectedIndexChanged);
            this.listaCanciones.DragDrop += new System.Windows.Forms.DragEventHandler(this.listaCanciones_DragDrop);
            this.listaCanciones.DragEnter += new System.Windows.Forms.DragEventHandler(this.listaCanciones_DragEnter);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.BackColor = System.Drawing.Color.Red;
            this.buttonEliminar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonEliminar.Location = new System.Drawing.Point(920, 431);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(85, 46);
            this.buttonEliminar.TabIndex = 8;
            this.buttonEliminar.Text = "Eliminar Audio";
            this.buttonEliminar.UseVisualStyleBackColor = false;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // Reproductor
            // 
            this.Reproductor.Enabled = true;
            this.Reproductor.Location = new System.Drawing.Point(3, 12);
            this.Reproductor.Name = "Reproductor";
            this.Reproductor.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Reproductor.OcxState")));
            this.Reproductor.Size = new System.Drawing.Size(772, 81);
            this.Reproductor.TabIndex = 0;
            this.Reproductor.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.Reproductor_PlayStateChange);
            // 
            // listBoxConversacion
            // 
            this.listBoxConversacion.AllowDrop = true;
            this.listBoxConversacion.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.listBoxConversacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxConversacion.CausesValidation = false;
            this.listBoxConversacion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxConversacion.FormattingEnabled = true;
            this.listBoxConversacion.ItemHeight = 16;
            this.listBoxConversacion.Location = new System.Drawing.Point(3, 100);
            this.listBoxConversacion.Name = "listBoxConversacion";
            this.listBoxConversacion.Size = new System.Drawing.Size(772, 306);
            this.listBoxConversacion.TabIndex = 9;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Green;
            this.btnAgregar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.Location = new System.Drawing.Point(15, 562);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(81, 46);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.Green;
            this.btnModificar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnModificar.Location = new System.Drawing.Point(117, 562);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(87, 46);
            this.btnModificar.TabIndex = 11;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            // 
            // btneEliminar
            // 
            this.btneEliminar.BackColor = System.Drawing.Color.Green;
            this.btneEliminar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btneEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btneEliminar.Location = new System.Drawing.Point(226, 562);
            this.btneEliminar.Name = "btneEliminar";
            this.btneEliminar.Size = new System.Drawing.Size(79, 46);
            this.btneEliminar.TabIndex = 12;
            this.btneEliminar.Text = "Eliminar";
            this.btneEliminar.UseVisualStyleBackColor = false;
            // 
            // radioButtonA
            // 
            this.radioButtonA.AutoSize = true;
            this.radioButtonA.Checked = true;
            this.radioButtonA.ForeColor = System.Drawing.Color.White;
            this.radioButtonA.Location = new System.Drawing.Point(12, 423);
            this.radioButtonA.Name = "radioButtonA";
            this.radioButtonA.Size = new System.Drawing.Size(32, 17);
            this.radioButtonA.TabIndex = 13;
            this.radioButtonA.TabStop = true;
            this.radioButtonA.Text = "A";
            this.radioButtonA.UseVisualStyleBackColor = true;
            // 
            // radioButtonB
            // 
            this.radioButtonB.AutoSize = true;
            this.radioButtonB.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.radioButtonB.Location = new System.Drawing.Point(12, 446);
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.Size = new System.Drawing.Size(32, 17);
            this.radioButtonB.TabIndex = 14;
            this.radioButtonB.TabStop = true;
            this.radioButtonB.Text = "B";
            this.radioButtonB.UseVisualStyleBackColor = true;
            // 
            // Trancripciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1017, 638);
            this.Controls.Add(this.radioButtonB);
            this.Controls.Add(this.radioButtonA);
            this.Controls.Add(this.btneEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.listBoxConversacion);
            this.Controls.Add(this.buttonEliminar);
            this.Controls.Add(this.listaCanciones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.buttonAgregar);
            this.Controls.Add(this.Reproductor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Trancripciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Focus";
            ((System.ComponentModel.ISupportInitialize)(this.Reproductor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listaCanciones;
        private AxWMPLib.AxWindowsMediaPlayer Reproductor;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.ListBox listBoxConversacion;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btneEliminar;
        private System.Windows.Forms.RadioButton radioButtonA;
        private System.Windows.Forms.RadioButton radioButtonB;
    }
}

