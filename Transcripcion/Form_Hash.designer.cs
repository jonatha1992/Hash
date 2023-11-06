using System;

namespace Hash
{
    partial class Form_Hash
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Hash));
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNomOfRecibe = new System.Windows.Forms.TextBox();
            this.DgvElementos = new System.Windows.Forms.DataGridView();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblImg = new System.Windows.Forms.Label();
            this.lblAudio = new System.Windows.Forms.Label();
            this.lblClip = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblTxt = new System.Windows.Forms.Label();
            this.lblVarios = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownHash = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonImprimirHash = new System.Windows.Forms.Button();
            this.textBoxDescripcion = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxRecibe = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBoxDestRecibe = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxDestEntrega = new System.Windows.Forms.ComboBox();
            this.numericUpDownRecibe = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEntrega = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxJerarquiaRecibe = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxJerarquiaEntrega = new System.Windows.Forms.ComboBox();
            this.textBoxNomEntrega = new System.Windows.Forms.TextBox();
            this.circularProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelPesoTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvElementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHash)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecibe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEntrega)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAgregar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAgregar.Location = new System.Drawing.Point(16, 604);
            this.buttonAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(109, 58);
            this.buttonAgregar.TabIndex = 2;
            this.buttonAgregar.Text = "Carpeta Origen";
            this.buttonAgregar.UseVisualStyleBackColor = false;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonCarpetaSeleccionada_Click);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.BackColor = System.Drawing.Color.Red;
            this.buttonEliminar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonEliminar.Location = new System.Drawing.Point(406, 470);
            this.buttonEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(99, 58);
            this.buttonEliminar.TabIndex = 8;
            this.buttonEliminar.Text = "Eliminar";
            this.buttonEliminar.UseVisualStyleBackColor = false;
            this.buttonEliminar.Visible = false;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "N° Hash";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(141, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Entrega";
            // 
            // textBoxNomOfRecibe
            // 
            this.textBoxNomOfRecibe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNomOfRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNomOfRecibe.Location = new System.Drawing.Point(18, 424);
            this.textBoxNomOfRecibe.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNomOfRecibe.Name = "textBoxNomOfRecibe";
            this.textBoxNomOfRecibe.Size = new System.Drawing.Size(334, 24);
            this.textBoxNomOfRecibe.TabIndex = 18;
            this.textBoxNomOfRecibe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNomOfRecibe_KeyDown);
            // 
            // DgvElementos
            // 
            this.DgvElementos.AllowUserToAddRows = false;
            this.DgvElementos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.DgvElementos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvElementos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvElementos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvElementos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvElementos.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DgvElementos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvElementos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvElementos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvElementos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvElementos.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvElementos.Location = new System.Drawing.Point(406, 54);
            this.DgvElementos.Margin = new System.Windows.Forms.Padding(2);
            this.DgvElementos.MultiSelect = false;
            this.DgvElementos.Name = "DgvElementos";
            this.DgvElementos.ReadOnly = true;
            this.DgvElementos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvElementos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvElementos.RowHeadersVisible = false;
            this.DgvElementos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.DgvElementos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DgvElementos.RowTemplate.Height = 60;
            this.DgvElementos.RowTemplate.ReadOnly = true;
            this.DgvElementos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvElementos.Size = new System.Drawing.Size(1084, 409);
            this.DgvElementos.TabIndex = 62;
            this.DgvElementos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvElementos_CellContentClick);
            // 
            // Sel
            // 
            this.Sel.FalseValue = "";
            this.Sel.HeaderText = "Sel";
            this.Sel.IndeterminateValue = "";
            this.Sel.MinimumWidth = 6;
            this.Sel.Name = "Sel";
            this.Sel.ReadOnly = true;
            this.Sel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Sel.TrueValue = "";
            this.Sel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(18, 250);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 17);
            this.label4.TabIndex = 64;
            this.label4.Text = "Nro, Control";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(834, 486);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 66;
            this.label6.Text = "Imagenes:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(18, 454);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 69;
            this.label8.Text = "Nro, Control";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(689, 486);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 20);
            this.label9.TabIndex = 70;
            this.label9.Text = "Audio:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(1020, 486);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 20);
            this.label11.TabIndex = 70;
            this.label11.Text = "Clips:";
            // 
            // lblImg
            // 
            this.lblImg.AutoSize = true;
            this.lblImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImg.ForeColor = System.Drawing.SystemColors.Control;
            this.lblImg.Location = new System.Drawing.Point(940, 486);
            this.lblImg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImg.Name = "lblImg";
            this.lblImg.Size = new System.Drawing.Size(19, 20);
            this.lblImg.TabIndex = 71;
            this.lblImg.Text = "0";
            // 
            // lblAudio
            // 
            this.lblAudio.AutoSize = true;
            this.lblAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudio.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAudio.Location = new System.Drawing.Point(760, 486);
            this.lblAudio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAudio.Name = "lblAudio";
            this.lblAudio.Size = new System.Drawing.Size(19, 20);
            this.lblAudio.TabIndex = 72;
            this.lblAudio.Text = "0";
            // 
            // lblClip
            // 
            this.lblClip.AutoSize = true;
            this.lblClip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClip.ForeColor = System.Drawing.SystemColors.Control;
            this.lblClip.Location = new System.Drawing.Point(1086, 486);
            this.lblClip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClip.Name = "lblClip";
            this.lblClip.Size = new System.Drawing.Size(19, 20);
            this.lblClip.TabIndex = 73;
            this.lblClip.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(1170, 486);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 20);
            this.label13.TabIndex = 74;
            this.label13.Text = "Texto:";
            // 
            // lblTxt
            // 
            this.lblTxt.AutoSize = true;
            this.lblTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxt.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTxt.Location = new System.Drawing.Point(1240, 486);
            this.lblTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size(19, 20);
            this.lblTxt.TabIndex = 75;
            this.lblTxt.Text = "0";
            // 
            // lblVarios
            // 
            this.lblVarios.AutoSize = true;
            this.lblVarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVarios.ForeColor = System.Drawing.SystemColors.Control;
            this.lblVarios.Location = new System.Drawing.Point(1394, 486);
            this.lblVarios.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVarios.Name = "lblVarios";
            this.lblVarios.Size = new System.Drawing.Size(19, 20);
            this.lblVarios.TabIndex = 77;
            this.lblVarios.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(1319, 486);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.TabIndex = 76;
            this.label10.Text = "Varios:";
            // 
            // numericUpDownHash
            // 
            this.numericUpDownHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHash.Location = new System.Drawing.Point(18, 48);
            this.numericUpDownHash.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownHash.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownHash.Name = "numericUpDownHash";
            this.numericUpDownHash.Size = new System.Drawing.Size(102, 24);
            this.numericUpDownHash.TabIndex = 78;
            this.numericUpDownHash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(940, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 24);
            this.label5.TabIndex = 79;
            this.label5.Text = "Archivos";
            // 
            // buttonImprimirHash
            // 
            this.buttonImprimirHash.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonImprimirHash.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImprimirHash.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonImprimirHash.Location = new System.Drawing.Point(252, 604);
            this.buttonImprimirHash.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImprimirHash.Name = "buttonImprimirHash";
            this.buttonImprimirHash.Size = new System.Drawing.Size(124, 62);
            this.buttonImprimirHash.TabIndex = 80;
            this.buttonImprimirHash.Text = "Imprimir Hash";
            this.buttonImprimirHash.UseVisualStyleBackColor = false;
            this.buttonImprimirHash.Click += new System.EventHandler(this.buttonImprimirHash_Click);
            // 
            // textBoxDescripcion
            // 
            this.textBoxDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescripcion.Location = new System.Drawing.Point(18, 110);
            this.textBoxDescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDescripcion.Multiline = true;
            this.textBoxDescripcion.Name = "textBoxDescripcion";
            this.textBoxDescripcion.Size = new System.Drawing.Size(326, 56);
            this.textBoxDescripcion.TabIndex = 86;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxRecibe);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.comboBoxDestRecibe);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBoxDestEntrega);
            this.groupBox1.Controls.Add(this.numericUpDownRecibe);
            this.groupBox1.Controls.Add(this.numericUpDownEntrega);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.comboBoxJerarquiaRecibe);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.comboBoxJerarquiaEntrega);
            this.groupBox1.Controls.Add(this.numericUpDownHash);
            this.groupBox1.Controls.Add(this.textBoxDescripcion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxNomEntrega);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxNomOfRecibe);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(368, 581);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // checkBoxRecibe
            // 
            this.checkBoxRecibe.AutoSize = true;
            this.checkBoxRecibe.Checked = true;
            this.checkBoxRecibe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRecibe.Location = new System.Drawing.Point(127, 382);
            this.checkBoxRecibe.Name = "checkBoxRecibe";
            this.checkBoxRecibe.Size = new System.Drawing.Size(79, 20);
            this.checkBoxRecibe.TabIndex = 101;
            this.checkBoxRecibe.Text = "Recibe";
            this.checkBoxRecibe.UseVisualStyleBackColor = true;
            this.checkBoxRecibe.CheckedChanged += new System.EventHandler(this.checkBoxRecibe_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.Control;
            this.label19.Location = new System.Drawing.Point(18, 508);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 17);
            this.label19.TabIndex = 100;
            this.label19.Text = "Destino";
            // 
            // comboBoxDestRecibe
            // 
            this.comboBoxDestRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDestRecibe.FormattingEnabled = true;
            this.comboBoxDestRecibe.Location = new System.Drawing.Point(18, 528);
            this.comboBoxDestRecibe.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDestRecibe.Name = "comboBoxDestRecibe";
            this.comboBoxDestRecibe.Size = new System.Drawing.Size(326, 26);
            this.comboBoxDestRecibe.TabIndex = 99;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(18, 309);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 98;
            this.label7.Text = "Destino";
            // 
            // comboBoxDestEntrega
            // 
            this.comboBoxDestEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDestEntrega.FormattingEnabled = true;
            this.comboBoxDestEntrega.Location = new System.Drawing.Point(18, 329);
            this.comboBoxDestEntrega.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDestEntrega.Name = "comboBoxDestEntrega";
            this.comboBoxDestEntrega.Size = new System.Drawing.Size(326, 26);
            this.comboBoxDestEntrega.TabIndex = 97;
            // 
            // numericUpDownRecibe
            // 
            this.numericUpDownRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownRecibe.Location = new System.Drawing.Point(18, 471);
            this.numericUpDownRecibe.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownRecibe.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownRecibe.Name = "numericUpDownRecibe";
            this.numericUpDownRecibe.Size = new System.Drawing.Size(118, 24);
            this.numericUpDownRecibe.TabIndex = 96;
            this.numericUpDownRecibe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRecibe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDownRecibe_KeyDown);
            // 
            // numericUpDownEntrega
            // 
            this.numericUpDownEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownEntrega.Location = new System.Drawing.Point(18, 271);
            this.numericUpDownEntrega.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownEntrega.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownEntrega.Name = "numericUpDownEntrega";
            this.numericUpDownEntrega.Size = new System.Drawing.Size(118, 24);
            this.numericUpDownEntrega.TabIndex = 95;
            this.numericUpDownEntrega.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownEntrega.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDownEntrega_KeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.Control;
            this.label18.Location = new System.Drawing.Point(185, 28);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(110, 17);
            this.label18.TabIndex = 93;
            this.label18.Text = "Fecha y Hora ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(145, 48);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(199, 24);
            this.dateTimePicker1.TabIndex = 94;
            this.dateTimePicker1.Value = new System.DateTime(2023, 11, 5, 20, 1, 22, 810);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.Control;
            this.label17.Location = new System.Drawing.Point(18, 89);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 17);
            this.label17.TabIndex = 93;
            this.label17.Text = "Descripción";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.Control;
            this.label16.Location = new System.Drawing.Point(168, 451);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 17);
            this.label16.TabIndex = 92;
            this.label16.Text = "Jerarquia";
            // 
            // comboBoxJerarquiaRecibe
            // 
            this.comboBoxJerarquiaRecibe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJerarquiaRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxJerarquiaRecibe.FormattingEnabled = true;
            this.comboBoxJerarquiaRecibe.Location = new System.Drawing.Point(168, 471);
            this.comboBoxJerarquiaRecibe.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxJerarquiaRecibe.Name = "comboBoxJerarquiaRecibe";
            this.comboBoxJerarquiaRecibe.Size = new System.Drawing.Size(176, 26);
            this.comboBoxJerarquiaRecibe.TabIndex = 91;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.Control;
            this.label15.Location = new System.Drawing.Point(18, 405);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(134, 17);
            this.label15.TabIndex = 90;
            this.label15.Text = "Nombre completo";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(18, 202);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 17);
            this.label14.TabIndex = 89;
            this.label14.Text = "Nombre completo";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(168, 252);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 17);
            this.label12.TabIndex = 88;
            this.label12.Text = "Jerarquia";
            // 
            // comboBoxJerarquiaEntrega
            // 
            this.comboBoxJerarquiaEntrega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJerarquiaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxJerarquiaEntrega.FormattingEnabled = true;
            this.comboBoxJerarquiaEntrega.Location = new System.Drawing.Point(168, 271);
            this.comboBoxJerarquiaEntrega.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxJerarquiaEntrega.Name = "comboBoxJerarquiaEntrega";
            this.comboBoxJerarquiaEntrega.Size = new System.Drawing.Size(176, 26);
            this.comboBoxJerarquiaEntrega.TabIndex = 87;
            // 
            // textBoxNomEntrega
            // 
            this.textBoxNomEntrega.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNomEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNomEntrega.Location = new System.Drawing.Point(18, 222);
            this.textBoxNomEntrega.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNomEntrega.Name = "textBoxNomEntrega";
            this.textBoxNomEntrega.Size = new System.Drawing.Size(326, 24);
            this.textBoxNomEntrega.TabIndex = 16;
            this.textBoxNomEntrega.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNomEntrega_KeyDown_1);
            // 
            // circularProgressBar1
            // 
            this.circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.CircularEaseIn;
            this.circularProgressBar1.AnimationSpeed = 500;
            this.circularProgressBar1.BackColor = System.Drawing.Color.Black;
            this.circularProgressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.circularProgressBar1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.circularProgressBar1.InnerColor = System.Drawing.SystemColors.InactiveCaption;
            this.circularProgressBar1.InnerMargin = 2;
            this.circularProgressBar1.InnerWidth = -1;
            this.circularProgressBar1.Location = new System.Drawing.Point(954, 552);
            this.circularProgressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.circularProgressBar1.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.OuterColor = System.Drawing.Color.White;
            this.circularProgressBar1.OuterMargin = -25;
            this.circularProgressBar1.OuterWidth = 26;
            this.circularProgressBar1.ProgressColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.circularProgressBar1.ProgressWidth = 15;
            this.circularProgressBar1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.circularProgressBar1.Size = new System.Drawing.Size(100, 89);
            this.circularProgressBar1.StartAngle = 270;
            this.circularProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.circularProgressBar1.SubscriptText = "";
            this.circularProgressBar1.SuperscriptColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.circularProgressBar1.SuperscriptMargin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar1.SuperscriptText = "";
            this.circularProgressBar1.TabIndex = 91;
            this.circularProgressBar1.Text = "Wait";
            this.circularProgressBar1.TextMargin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar1.Value = 68;
            this.circularProgressBar1.Visible = false;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTotal.Location = new System.Drawing.Point(1349, 604);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(100, 20);
            this.labelTotal.TabIndex = 93;
            this.labelTotal.Text = "Peso total:";
            // 
            // labelPesoTotal
            // 
            this.labelPesoTotal.AutoSize = true;
            this.labelPesoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPesoTotal.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPesoTotal.Location = new System.Drawing.Point(1471, 604);
            this.labelPesoTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPesoTotal.Name = "labelPesoTotal";
            this.labelPesoTotal.Size = new System.Drawing.Size(19, 20);
            this.labelPesoTotal.TabIndex = 94;
            this.labelPesoTotal.Text = "0";
            // 
            // Form_Hash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1514, 668);
            this.Controls.Add(this.labelPesoTotal);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.circularProgressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonImprimirHash);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblVarios);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblTxt);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblClip);
            this.Controls.Add(this.lblAudio);
            this.Controls.Add(this.lblImg);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DgvElementos);
            this.Controls.Add(this.buttonEliminar);
            this.Controls.Add(this.buttonAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form_Hash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hash";
            ((System.ComponentModel.ISupportInitialize)(this.DgvElementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHash)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecibe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEntrega)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNomOfRecibe;
        private System.Windows.Forms.DataGridView DgvElementos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblImg;
        private System.Windows.Forms.Label lblAudio;
        private System.Windows.Forms.Label lblClip;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblTxt;
        private System.Windows.Forms.Label lblVarios;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownHash;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonImprimirHash;
        private System.Windows.Forms.TextBox textBoxDescripcion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxJerarquiaRecibe;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxJerarquiaEntrega;
        private System.Windows.Forms.TextBox textBoxNomEntrega;
        private System.Windows.Forms.Label label17;
        private CircularProgressBar.CircularProgressBar circularProgressBar1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelPesoTotal;
        private System.Windows.Forms.NumericUpDown numericUpDownRecibe;
        private System.Windows.Forms.NumericUpDown numericUpDownEntrega;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxDestEntrega;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBoxDestRecibe;
        private System.Windows.Forms.CheckBox checkBoxRecibe;
    }
}

