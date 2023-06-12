using System;

namespace Transcripcion
{
    partial class Hash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hash));
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.listBoxArchivos = new System.Windows.Forms.ListBox();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNomOfRecibe = new System.Windows.Forms.TextBox();
            this.DgvElementos = new System.Windows.Forms.DataGridView();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxControlOfEntrega = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelControlRecibe = new System.Windows.Forms.Label();
            this.textBoxConOfRecibe = new System.Windows.Forms.TextBox();
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
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxTipo = new System.Windows.Forms.ComboBox();
            this.labelDependenciaRecibe = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.comboBoxDependeciaEntrega = new System.Windows.Forms.ComboBox();
            this.comboBoxDependeciaRecibe = new System.Windows.Forms.ComboBox();
            this.labelAeropuerto = new System.Windows.Forms.Label();
            this.comboBoxAeropuertos = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxIdentificacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUtilidad = new System.Windows.Forms.TextBox();
            this.checkBoxOficialRecibe = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxLugar = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxJuzgado = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxSecretaria = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxSumario = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxCaratula = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.labelJerarquiaRecibe = new System.Windows.Forms.Label();
            this.comboBoxJerarquiaRecibe = new System.Windows.Forms.ComboBox();
            this.labelNombreRecibe = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxJerarquiaEntrega = new System.Windows.Forms.ComboBox();
            this.textBoxNomEntrega = new System.Windows.Forms.TextBox();
            this.buttonImpCustodia = new System.Windows.Forms.Button();
            this.circularProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelPesoTotal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvElementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHash)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAgregar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAgregar.Location = new System.Drawing.Point(346, 506);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(82, 46);
            this.buttonAgregar.TabIndex = 2;
            this.buttonAgregar.Text = "Carpeta Origen";
            this.buttonAgregar.UseVisualStyleBackColor = false;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonCarpetaSeleccionada_Click2);
            // 
            // listBoxArchivos
            // 
            this.listBoxArchivos.AllowDrop = true;
            this.listBoxArchivos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.listBoxArchivos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxArchivos.CausesValidation = false;
            this.listBoxArchivos.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxArchivos.FormattingEnabled = true;
            this.listBoxArchivos.HorizontalScrollbar = true;
            this.listBoxArchivos.ItemHeight = 16;
            this.listBoxArchivos.Location = new System.Drawing.Point(346, 34);
            this.listBoxArchivos.Name = "listBoxArchivos";
            this.listBoxArchivos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxArchivos.Size = new System.Drawing.Size(272, 466);
            this.listBoxArchivos.TabIndex = 7;
            this.listBoxArchivos.SelectedIndexChanged += new System.EventHandler(this.listBoxArchivos_SelectedIndexChanged);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.BackColor = System.Drawing.Color.Red;
            this.buttonEliminar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonEliminar.Location = new System.Drawing.Point(544, 507);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(74, 45);
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
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "N° Hash / Custodia ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(108, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Oficial Entrega";
            // 
            // textBoxNomOfRecibe
            // 
            this.textBoxNomOfRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNomOfRecibe.Location = new System.Drawing.Point(12, 620);
            this.textBoxNomOfRecibe.Name = "textBoxNomOfRecibe";
            this.textBoxNomOfRecibe.Size = new System.Drawing.Size(301, 18);
            this.textBoxNomOfRecibe.TabIndex = 18;
            this.textBoxNomOfRecibe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNomOfRecibe_KeyDown);
            // 
            // DgvElementos
            // 
            this.DgvElementos.AllowUserToAddRows = false;
            this.DgvElementos.AllowUserToDeleteRows = false;
            this.DgvElementos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvElementos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvElementos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvElementos.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DgvElementos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvElementos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvElementos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvElementos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvElementos.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvElementos.Location = new System.Drawing.Point(624, 35);
            this.DgvElementos.Margin = new System.Windows.Forms.Padding(2);
            this.DgvElementos.MultiSelect = false;
            this.DgvElementos.Name = "DgvElementos";
            this.DgvElementos.ReadOnly = true;
            this.DgvElementos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvElementos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvElementos.RowHeadersVisible = false;
            this.DgvElementos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.DgvElementos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvElementos.RowTemplate.Height = 60;
            this.DgvElementos.RowTemplate.ReadOnly = true;
            this.DgvElementos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgvElementos.Size = new System.Drawing.Size(696, 466);
            this.DgvElementos.TabIndex = 62;
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
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(210, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 64;
            this.label4.Text = "Nro, Control";
            // 
            // textBoxControlOfEntrega
            // 
            this.textBoxControlOfEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxControlOfEntrega.Location = new System.Drawing.Point(202, 436);
            this.textBoxControlOfEntrega.Name = "textBoxControlOfEntrega";
            this.textBoxControlOfEntrega.Size = new System.Drawing.Size(108, 18);
            this.textBoxControlOfEntrega.TabIndex = 63;
            this.textBoxControlOfEntrega.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxControlOfEntrega_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(955, 515);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 17);
            this.label6.TabIndex = 66;
            this.label6.Text = "Imagenes:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(436, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 18);
            this.label7.TabIndex = 67;
            this.label7.Text = "Listado Archivos";
            // 
            // labelControlRecibe
            // 
            this.labelControlRecibe.AutoSize = true;
            this.labelControlRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlRecibe.ForeColor = System.Drawing.SystemColors.Control;
            this.labelControlRecibe.Location = new System.Drawing.Point(210, 565);
            this.labelControlRecibe.Name = "labelControlRecibe";
            this.labelControlRecibe.Size = new System.Drawing.Size(67, 12);
            this.labelControlRecibe.TabIndex = 69;
            this.labelControlRecibe.Text = "Nro, Control";
            // 
            // textBoxConOfRecibe
            // 
            this.textBoxConOfRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConOfRecibe.Location = new System.Drawing.Point(184, 582);
            this.textBoxConOfRecibe.Name = "textBoxConOfRecibe";
            this.textBoxConOfRecibe.Size = new System.Drawing.Size(129, 18);
            this.textBoxConOfRecibe.TabIndex = 68;
            this.textBoxConOfRecibe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxConOfRecibe_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(854, 515);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 17);
            this.label9.TabIndex = 70;
            this.label9.Text = "Audio:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(1074, 515);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 17);
            this.label11.TabIndex = 70;
            this.label11.Text = "Clips:";
            // 
            // lblImg
            // 
            this.lblImg.AutoSize = true;
            this.lblImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImg.ForeColor = System.Drawing.SystemColors.Control;
            this.lblImg.Location = new System.Drawing.Point(1032, 515);
            this.lblImg.Name = "lblImg";
            this.lblImg.Size = new System.Drawing.Size(17, 17);
            this.lblImg.TabIndex = 71;
            this.lblImg.Text = "0";
            // 
            // lblAudio
            // 
            this.lblAudio.AutoSize = true;
            this.lblAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudio.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAudio.Location = new System.Drawing.Point(903, 515);
            this.lblAudio.Name = "lblAudio";
            this.lblAudio.Size = new System.Drawing.Size(17, 17);
            this.lblAudio.TabIndex = 72;
            this.lblAudio.Text = "0";
            // 
            // lblClip
            // 
            this.lblClip.AutoSize = true;
            this.lblClip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClip.ForeColor = System.Drawing.SystemColors.Control;
            this.lblClip.Location = new System.Drawing.Point(1117, 515);
            this.lblClip.Name = "lblClip";
            this.lblClip.Size = new System.Drawing.Size(17, 17);
            this.lblClip.TabIndex = 73;
            this.lblClip.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(1155, 515);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 17);
            this.label13.TabIndex = 74;
            this.label13.Text = "Texto:";
            // 
            // lblTxt
            // 
            this.lblTxt.AutoSize = true;
            this.lblTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxt.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTxt.Location = new System.Drawing.Point(1200, 515);
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size(17, 17);
            this.lblTxt.TabIndex = 75;
            this.lblTxt.Text = "0";
            // 
            // lblVarios
            // 
            this.lblVarios.AutoSize = true;
            this.lblVarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVarios.ForeColor = System.Drawing.SystemColors.Control;
            this.lblVarios.Location = new System.Drawing.Point(1296, 515);
            this.lblVarios.Name = "lblVarios";
            this.lblVarios.Size = new System.Drawing.Size(17, 17);
            this.lblVarios.TabIndex = 77;
            this.lblVarios.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(1238, 515);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 17);
            this.label10.TabIndex = 76;
            this.label10.Text = "Varios:";
            // 
            // numericUpDownHash
            // 
            this.numericUpDownHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHash.Location = new System.Drawing.Point(16, 34);
            this.numericUpDownHash.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownHash.Name = "numericUpDownHash";
            this.numericUpDownHash.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownHash.TabIndex = 78;
            this.numericUpDownHash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownHash.ValueChanged += new System.EventHandler(this.numericUpDownHash_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(901, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 18);
            this.label5.TabIndex = 79;
            this.label5.Text = "Listado Hash";
            // 
            // buttonImprimirHash
            // 
            this.buttonImprimirHash.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonImprimirHash.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImprimirHash.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonImprimirHash.Location = new System.Drawing.Point(448, 642);
            this.buttonImprimirHash.Name = "buttonImprimirHash";
            this.buttonImprimirHash.Size = new System.Drawing.Size(78, 44);
            this.buttonImprimirHash.TabIndex = 80;
            this.buttonImprimirHash.Text = "Imprimir Hash";
            this.buttonImprimirHash.UseVisualStyleBackColor = false;
            this.buttonImprimirHash.Click += new System.EventHandler(this.buttonImprimir_Click);
            // 
            // textBoxDescripcion
            // 
            this.textBoxDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescripcion.Location = new System.Drawing.Point(13, 375);
            this.textBoxDescripcion.Name = "textBoxDescripcion";
            this.textBoxDescripcion.Size = new System.Drawing.Size(291, 18);
            this.textBoxDescripcion.TabIndex = 86;
            this.textBoxDescripcion.Text = "N/C";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBoxTipo);
            this.groupBox1.Controls.Add(this.labelDependenciaRecibe);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.comboBoxDependeciaEntrega);
            this.groupBox1.Controls.Add(this.comboBoxDependeciaRecibe);
            this.groupBox1.Controls.Add(this.labelAeropuerto);
            this.groupBox1.Controls.Add(this.comboBoxAeropuertos);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.textBoxIdentificacion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxUtilidad);
            this.groupBox1.Controls.Add(this.checkBoxOficialRecibe);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.textBoxLugar);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.textBoxJuzgado);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.textBoxSecretaria);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.textBoxSumario);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.textBoxCaratula);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.labelJerarquiaRecibe);
            this.groupBox1.Controls.Add(this.comboBoxJerarquiaRecibe);
            this.groupBox1.Controls.Add(this.labelNombreRecibe);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.comboBoxJerarquiaEntrega);
            this.groupBox1.Controls.Add(this.numericUpDownHash);
            this.groupBox1.Controls.Add(this.textBoxDescripcion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxNomEntrega);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxControlOfEntrega);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxNomOfRecibe);
            this.groupBox1.Controls.Add(this.textBoxConOfRecibe);
            this.groupBox1.Controls.Add(this.labelControlRecibe);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 688);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(13, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 13);
            this.label8.TabIndex = 117;
            this.label8.Text = "Tipo de Procedimiento";
            // 
            // comboBoxTipo
            // 
            this.comboBoxTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipo.FormattingEnabled = true;
            this.comboBoxTipo.Items.AddRange(new object[] {
            "CONTROL VUELO",
            "CONTROL VEHICULAR",
            "CONTROL POBLACIONAL",
            "ALLANAMIENTO",
            "PROCEDIMIENTO",
            "REQUISA"});
            this.comboBoxTipo.Location = new System.Drawing.Point(16, 74);
            this.comboBoxTipo.Name = "comboBoxTipo";
            this.comboBoxTipo.Size = new System.Drawing.Size(173, 20);
            this.comboBoxTipo.TabIndex = 116;
            this.comboBoxTipo.Text = "CONTROL VUELO";
            this.comboBoxTipo.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipo_SelectedIndexChanged);
            // 
            // labelDependenciaRecibe
            // 
            this.labelDependenciaRecibe.AutoSize = true;
            this.labelDependenciaRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDependenciaRecibe.ForeColor = System.Drawing.SystemColors.Control;
            this.labelDependenciaRecibe.Location = new System.Drawing.Point(11, 643);
            this.labelDependenciaRecibe.Name = "labelDependenciaRecibe";
            this.labelDependenciaRecibe.Size = new System.Drawing.Size(70, 12);
            this.labelDependenciaRecibe.TabIndex = 115;
            this.labelDependenciaRecibe.Text = "Dependencia";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.SystemColors.Control;
            this.label26.Location = new System.Drawing.Point(14, 501);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(70, 12);
            this.label26.TabIndex = 113;
            this.label26.Text = "Dependencia";
            // 
            // comboBoxDependeciaEntrega
            // 
            this.comboBoxDependeciaEntrega.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDependeciaEntrega.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDependeciaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDependeciaEntrega.FormattingEnabled = true;
            this.comboBoxDependeciaEntrega.Location = new System.Drawing.Point(15, 515);
            this.comboBoxDependeciaEntrega.Name = "comboBoxDependeciaEntrega";
            this.comboBoxDependeciaEntrega.Size = new System.Drawing.Size(295, 20);
            this.comboBoxDependeciaEntrega.TabIndex = 112;
            // 
            // comboBoxDependeciaRecibe
            // 
            this.comboBoxDependeciaRecibe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDependeciaRecibe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDependeciaRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDependeciaRecibe.FormattingEnabled = true;
            this.comboBoxDependeciaRecibe.Location = new System.Drawing.Point(11, 657);
            this.comboBoxDependeciaRecibe.Name = "comboBoxDependeciaRecibe";
            this.comboBoxDependeciaRecibe.Size = new System.Drawing.Size(302, 20);
            this.comboBoxDependeciaRecibe.TabIndex = 114;
            // 
            // labelAeropuerto
            // 
            this.labelAeropuerto.AutoSize = true;
            this.labelAeropuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAeropuerto.ForeColor = System.Drawing.SystemColors.Control;
            this.labelAeropuerto.Location = new System.Drawing.Point(220, 61);
            this.labelAeropuerto.Name = "labelAeropuerto";
            this.labelAeropuerto.Size = new System.Drawing.Size(69, 13);
            this.labelAeropuerto.TabIndex = 111;
            this.labelAeropuerto.Text = "Aeropuerto";
            this.labelAeropuerto.Visible = false;
            // 
            // comboBoxAeropuertos
            // 
            this.comboBoxAeropuertos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxAeropuertos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxAeropuertos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAeropuertos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAeropuertos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.comboBoxAeropuertos.FormattingEnabled = true;
            this.comboBoxAeropuertos.Location = new System.Drawing.Point(202, 75);
            this.comboBoxAeropuertos.Name = "comboBoxAeropuertos";
            this.comboBoxAeropuertos.Size = new System.Drawing.Size(104, 20);
            this.comboBoxAeropuertos.TabIndex = 110;
            this.comboBoxAeropuertos.Visible = false;
            this.comboBoxAeropuertos.SelectedIndexChanged += new System.EventHandler(this.comboBoxAeropuertos_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.SystemColors.Control;
            this.label24.Location = new System.Drawing.Point(10, 325);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(106, 12);
            this.label24.TabIndex = 109;
            this.label24.Text = "Identifición material";
            // 
            // textBoxIdentificacion
            // 
            this.textBoxIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIdentificacion.Location = new System.Drawing.Point(12, 338);
            this.textBoxIdentificacion.Name = "textBoxIdentificacion";
            this.textBoxIdentificacion.Size = new System.Drawing.Size(291, 18);
            this.textBoxIdentificacion.TabIndex = 108;
            this.textBoxIdentificacion.Text = "N/C";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(13, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 107;
            this.label3.Text = "Otra utilidad";
            // 
            // textBoxUtilidad
            // 
            this.textBoxUtilidad.AutoCompleteCustomSource.AddRange(new string[] {
            "N/C"});
            this.textBoxUtilidad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxUtilidad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxUtilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUtilidad.Location = new System.Drawing.Point(13, 301);
            this.textBoxUtilidad.Name = "textBoxUtilidad";
            this.textBoxUtilidad.Size = new System.Drawing.Size(291, 18);
            this.textBoxUtilidad.TabIndex = 106;
            this.textBoxUtilidad.Text = "N/C";
            // 
            // checkBoxOficialRecibe
            // 
            this.checkBoxOficialRecibe.AutoSize = true;
            this.checkBoxOficialRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOficialRecibe.Location = new System.Drawing.Point(108, 542);
            this.checkBoxOficialRecibe.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxOficialRecibe.Name = "checkBoxOficialRecibe";
            this.checkBoxOficialRecibe.Size = new System.Drawing.Size(106, 17);
            this.checkBoxOficialRecibe.TabIndex = 95;
            this.checkBoxOficialRecibe.Text = "Oficial Recibe";
            this.checkBoxOficialRecibe.UseVisualStyleBackColor = true;
            this.checkBoxOficialRecibe.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.Control;
            this.label23.Location = new System.Drawing.Point(13, 247);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 12);
            this.label23.TabIndex = 105;
            this.label23.Text = "Lugar de recolección";
            // 
            // textBoxLugar
            // 
            this.textBoxLugar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLugar.Location = new System.Drawing.Point(14, 262);
            this.textBoxLugar.Name = "textBoxLugar";
            this.textBoxLugar.Size = new System.Drawing.Size(291, 18);
            this.textBoxLugar.TabIndex = 104;
            this.textBoxLugar.Text = "N/C";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.Control;
            this.label22.Location = new System.Drawing.Point(13, 170);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 12);
            this.label22.TabIndex = 102;
            this.label22.Text = "Juzgado/Fiscalia";
            // 
            // textBoxJuzgado
            // 
            this.textBoxJuzgado.AutoCompleteCustomSource.AddRange(new string[] {
            "N/C"});
            this.textBoxJuzgado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxJuzgado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxJuzgado.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxJuzgado.Location = new System.Drawing.Point(13, 185);
            this.textBoxJuzgado.Name = "textBoxJuzgado";
            this.textBoxJuzgado.Size = new System.Drawing.Size(291, 18);
            this.textBoxJuzgado.TabIndex = 101;
            this.textBoxJuzgado.Text = "N/C";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.Control;
            this.label21.Location = new System.Drawing.Point(13, 209);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 12);
            this.label21.TabIndex = 100;
            this.label21.Text = "Secretaria";
            // 
            // textBoxSecretaria
            // 
            this.textBoxSecretaria.AutoCompleteCustomSource.AddRange(new string[] {
            "N/C"});
            this.textBoxSecretaria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxSecretaria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxSecretaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSecretaria.Location = new System.Drawing.Point(14, 224);
            this.textBoxSecretaria.Name = "textBoxSecretaria";
            this.textBoxSecretaria.Size = new System.Drawing.Size(291, 18);
            this.textBoxSecretaria.TabIndex = 99;
            this.textBoxSecretaria.Text = "N/C";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.Control;
            this.label20.Location = new System.Drawing.Point(14, 134);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 98;
            this.label20.Text = "Sumario";
            // 
            // textBoxSumario
            // 
            this.textBoxSumario.AutoCompleteCustomSource.AddRange(new string[] {
            "N/C"});
            this.textBoxSumario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxSumario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxSumario.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSumario.Location = new System.Drawing.Point(15, 147);
            this.textBoxSumario.Name = "textBoxSumario";
            this.textBoxSumario.Size = new System.Drawing.Size(291, 18);
            this.textBoxSumario.TabIndex = 97;
            this.textBoxSumario.Text = "N/C";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.Control;
            this.label19.Location = new System.Drawing.Point(15, 100);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(145, 13);
            this.label19.TabIndex = 96;
            this.label19.Text = "Caratula / Procedimiento";
            // 
            // textBoxCaratula
            // 
            this.textBoxCaratula.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCaratula.Location = new System.Drawing.Point(16, 113);
            this.textBoxCaratula.Name = "textBoxCaratula";
            this.textBoxCaratula.Size = new System.Drawing.Size(292, 18);
            this.textBoxCaratula.TabIndex = 95;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.Control;
            this.label18.Location = new System.Drawing.Point(174, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 13);
            this.label18.TabIndex = 93;
            this.label18.Text = "Fecha y Hora Entrega";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy      HH:mm";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(162, 35);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(145, 20);
            this.dateTimePicker1.TabIndex = 94;
            this.dateTimePicker1.Value = new System.DateTime(2023, 6, 9, 11, 10, 26, 842);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.Control;
            this.label17.Location = new System.Drawing.Point(13, 362);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 12);
            this.label17.TabIndex = 93;
            this.label17.Text = "Breve descripción";
            // 
            // labelJerarquiaRecibe
            // 
            this.labelJerarquiaRecibe.AutoSize = true;
            this.labelJerarquiaRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJerarquiaRecibe.ForeColor = System.Drawing.SystemColors.Control;
            this.labelJerarquiaRecibe.Location = new System.Drawing.Point(12, 564);
            this.labelJerarquiaRecibe.Name = "labelJerarquiaRecibe";
            this.labelJerarquiaRecibe.Size = new System.Drawing.Size(52, 12);
            this.labelJerarquiaRecibe.TabIndex = 92;
            this.labelJerarquiaRecibe.Text = "Jerarquia";
            // 
            // comboBoxJerarquiaRecibe
            // 
            this.comboBoxJerarquiaRecibe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJerarquiaRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxJerarquiaRecibe.FormattingEnabled = true;
            this.comboBoxJerarquiaRecibe.Location = new System.Drawing.Point(12, 580);
            this.comboBoxJerarquiaRecibe.Name = "comboBoxJerarquiaRecibe";
            this.comboBoxJerarquiaRecibe.Size = new System.Drawing.Size(148, 20);
            this.comboBoxJerarquiaRecibe.TabIndex = 91;
            // 
            // labelNombreRecibe
            // 
            this.labelNombreRecibe.AutoSize = true;
            this.labelNombreRecibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombreRecibe.ForeColor = System.Drawing.SystemColors.Control;
            this.labelNombreRecibe.Location = new System.Drawing.Point(12, 604);
            this.labelNombreRecibe.Name = "labelNombreRecibe";
            this.labelNombreRecibe.Size = new System.Drawing.Size(93, 12);
            this.labelNombreRecibe.TabIndex = 90;
            this.labelNombreRecibe.Text = "Nombre completo";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(15, 463);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 12);
            this.label14.TabIndex = 89;
            this.label14.Text = "Nombre completo";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(17, 421);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 12);
            this.label12.TabIndex = 88;
            this.label12.Text = "Jerarquia";
            // 
            // comboBoxJerarquiaEntrega
            // 
            this.comboBoxJerarquiaEntrega.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxJerarquiaEntrega.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxJerarquiaEntrega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJerarquiaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxJerarquiaEntrega.FormattingEnabled = true;
            this.comboBoxJerarquiaEntrega.Location = new System.Drawing.Point(17, 436);
            this.comboBoxJerarquiaEntrega.Name = "comboBoxJerarquiaEntrega";
            this.comboBoxJerarquiaEntrega.Size = new System.Drawing.Size(155, 20);
            this.comboBoxJerarquiaEntrega.TabIndex = 87;
            // 
            // textBoxNomEntrega
            // 
            this.textBoxNomEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNomEntrega.Location = new System.Drawing.Point(15, 476);
            this.textBoxNomEntrega.Name = "textBoxNomEntrega";
            this.textBoxNomEntrega.Size = new System.Drawing.Size(295, 18);
            this.textBoxNomEntrega.TabIndex = 16;
            this.textBoxNomEntrega.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxNomEntrega_KeyDown);
            // 
            // buttonImpCustodia
            // 
            this.buttonImpCustodia.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonImpCustodia.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImpCustodia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonImpCustodia.Location = new System.Drawing.Point(346, 642);
            this.buttonImpCustodia.Name = "buttonImpCustodia";
            this.buttonImpCustodia.Size = new System.Drawing.Size(83, 44);
            this.buttonImpCustodia.TabIndex = 88;
            this.buttonImpCustodia.Text = "Imprimir Custodia";
            this.buttonImpCustodia.UseVisualStyleBackColor = false;
            this.buttonImpCustodia.Click += new System.EventHandler(this.buttonImpCustodia_Click);
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
            this.circularProgressBar1.Location = new System.Drawing.Point(944, 564);
            this.circularProgressBar1.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar1.Maximum = 1000;
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.OuterColor = System.Drawing.Color.White;
            this.circularProgressBar1.OuterMargin = -25;
            this.circularProgressBar1.OuterWidth = 26;
            this.circularProgressBar1.ProgressColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.circularProgressBar1.ProgressWidth = 15;
            this.circularProgressBar1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.circularProgressBar1.Size = new System.Drawing.Size(79, 77);
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
            // buttonReset
            // 
            this.buttonReset.BackColor = System.Drawing.Color.Red;
            this.buttonReset.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonReset.Location = new System.Drawing.Point(1252, 644);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(67, 42);
            this.buttonReset.TabIndex = 92;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTotal.Location = new System.Drawing.Point(637, 513);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(90, 18);
            this.labelTotal.TabIndex = 93;
            this.labelTotal.Text = "Peso total:";
            // 
            // labelPesoTotal
            // 
            this.labelPesoTotal.AutoSize = true;
            this.labelPesoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPesoTotal.ForeColor = System.Drawing.SystemColors.Control;
            this.labelPesoTotal.Location = new System.Drawing.Point(738, 513);
            this.labelPesoTotal.Name = "labelPesoTotal";
            this.labelPesoTotal.Size = new System.Drawing.Size(17, 18);
            this.labelPesoTotal.TabIndex = 94;
            this.labelPesoTotal.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.Control;
            this.label15.Location = new System.Drawing.Point(654, 674);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(167, 12);
            this.label15.TabIndex = 118;
            this.label15.Text = "Producido por:  Jonathan Correa";
            // 
            // Hash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1331, 691);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.labelPesoTotal);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.circularProgressBar1);
            this.Controls.Add(this.buttonImpCustodia);
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
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DgvElementos);
            this.Controls.Add(this.buttonEliminar);
            this.Controls.Add(this.listBoxArchivos);
            this.Controls.Add(this.buttonAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Hash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hash Copy";
            this.Load += new System.EventHandler(this.Hash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvElementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHash)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.ListBox listBoxArchivos;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNomOfRecibe;
        private System.Windows.Forms.DataGridView DgvElementos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxControlOfEntrega;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelControlRecibe;
        private System.Windows.Forms.TextBox textBoxConOfRecibe;
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
        private System.Windows.Forms.Label labelJerarquiaRecibe;
        private System.Windows.Forms.ComboBox comboBoxJerarquiaRecibe;
        private System.Windows.Forms.Label labelNombreRecibe;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxJerarquiaEntrega;
        private System.Windows.Forms.TextBox textBoxNomEntrega;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonImpCustodia;
        private CircularProgressBar.CircularProgressBar circularProgressBar1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelPesoTotal;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxCaratula;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxLugar;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxJuzgado;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxSecretaria;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxSumario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUtilidad;
        private System.Windows.Forms.CheckBox checkBoxOficialRecibe;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxIdentificacion;
        private System.Windows.Forms.Label labelDependenciaRecibe;
        private System.Windows.Forms.ComboBox comboBoxDependeciaRecibe;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboBoxDependeciaEntrega;
        private System.Windows.Forms.Label labelAeropuerto;
        private System.Windows.Forms.ComboBox comboBoxAeropuertos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxTipo;
        private System.Windows.Forms.Label label15;
    }
}

