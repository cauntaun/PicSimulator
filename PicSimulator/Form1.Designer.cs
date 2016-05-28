namespace PicSimulator
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menueToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiLadenToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleLog = new System.Windows.Forms.TextBox();
            this.consoleLabel = new System.Windows.Forms.Label();
            this.dataGridView_Lst = new System.Windows.Forms.DataGridView();
            this.Sourcecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.pclLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pclLabel1 = new System.Windows.Forms.Label();
            this.stackLabel = new System.Windows.Forms.Label();
            this.stackGridView = new System.Windows.Forms.DataGridView();
            this.stackColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rp1Label = new System.Windows.Forms.Label();
            this.rp0Label = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.pdLabel = new System.Windows.Forms.Label();
            this.zLabel = new System.Windows.Forms.Label();
            this.dcLabel = new System.Windows.Forms.Label();
            this.cLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.wLabel = new System.Windows.Forms.Label();
            this.storageGridView = new System.Windows.Forms.DataGridView();
            this.spalte00 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte04 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte05 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte06 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte07 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_nextStep = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Lst)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.storageGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menueToolStrip,
            this.aboutToolStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1456, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menueToolStrip
            // 
            this.menueToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiLadenToolStrip});
            this.menueToolStrip.Name = "menueToolStrip";
            this.menueToolStrip.Size = new System.Drawing.Size(50, 20);
            this.menueToolStrip.Text = "Menü";
            // 
            // dateiLadenToolStrip
            // 
            this.dateiLadenToolStrip.Name = "dateiLadenToolStrip";
            this.dateiLadenToolStrip.Size = new System.Drawing.Size(133, 22);
            this.dateiLadenToolStrip.Text = "Datei laden";
            this.dateiLadenToolStrip.Click += new System.EventHandler(this.dateiLadenToolStrip_Click);
            // 
            // aboutToolStrip
            // 
            this.aboutToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hilfeToolStrip});
            this.aboutToolStrip.Name = "aboutToolStrip";
            this.aboutToolStrip.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStrip.Text = "About";
            // 
            // hilfeToolStrip
            // 
            this.hilfeToolStrip.Name = "hilfeToolStrip";
            this.hilfeToolStrip.Size = new System.Drawing.Size(99, 22);
            this.hilfeToolStrip.Text = "Hilfe";
            // 
            // consoleLog
            // 
            this.consoleLog.Location = new System.Drawing.Point(1058, 61);
            this.consoleLog.Multiline = true;
            this.consoleLog.Name = "consoleLog";
            this.consoleLog.ReadOnly = true;
            this.consoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleLog.Size = new System.Drawing.Size(382, 373);
            this.consoleLog.TabIndex = 2;
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleLabel.Location = new System.Drawing.Point(1053, 32);
            this.consoleLabel.Name = "consoleLabel";
            this.consoleLabel.Size = new System.Drawing.Size(133, 25);
            this.consoleLabel.TabIndex = 3;
            this.consoleLabel.Text = "Console Log";
            // 
            // dataGridView_Lst
            // 
            this.dataGridView_Lst.AllowUserToAddRows = false;
            this.dataGridView_Lst.AllowUserToDeleteRows = false;
            this.dataGridView_Lst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Lst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sourcecode});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Lst.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Lst.Location = new System.Drawing.Point(354, 440);
            this.dataGridView_Lst.MultiSelect = false;
            this.dataGridView_Lst.Name = "dataGridView_Lst";
            this.dataGridView_Lst.ReadOnly = true;
            this.dataGridView_Lst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Lst.Size = new System.Drawing.Size(929, 377);
            this.dataGridView_Lst.TabIndex = 5;
            // 
            // Sourcecode
            // 
            this.Sourcecode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sourcecode.HeaderText = "Sourcecode";
            this.Sourcecode.Name = "Sourcecode";
            this.Sourcecode.ReadOnly = true;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(1365, 32);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 6;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timeLabel);
            this.groupBox1.Controls.Add(this.label_time);
            this.groupBox1.Controls.Add(this.pclLabel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.pclLabel1);
            this.groupBox1.Controls.Add(this.pcLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.wLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 231);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spezialregister";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(46, 142);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(10, 13);
            this.timeLabel.TabIndex = 12;
            this.timeLabel.Text = "-";
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Location = new System.Drawing.Point(3, 142);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(36, 13);
            this.label_time.TabIndex = 11;
            this.label_time.Text = "Time: ";
            // 
            // pclLabel
            // 
            this.pclLabel.AutoSize = true;
            this.pclLabel.Location = new System.Drawing.Point(57, 89);
            this.pclLabel.Name = "pclLabel";
            this.pclLabel.Size = new System.Drawing.Size(10, 13);
            this.pclLabel.TabIndex = 10;
            this.pclLabel.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "PCLATH:";
            // 
            // pclLabel1
            // 
            this.pclLabel1.AutoSize = true;
            this.pclLabel1.Location = new System.Drawing.Point(3, 89);
            this.pclLabel1.Name = "pclLabel1";
            this.pclLabel1.Size = new System.Drawing.Size(30, 13);
            this.pclLabel1.TabIndex = 9;
            this.pclLabel1.Text = "PCL:";
            // 
            // stackLabel
            // 
            this.stackLabel.AutoSize = true;
            this.stackLabel.Location = new System.Drawing.Point(354, 46);
            this.stackLabel.Name = "stackLabel";
            this.stackLabel.Size = new System.Drawing.Size(35, 13);
            this.stackLabel.TabIndex = 8;
            this.stackLabel.Text = "Stack";
            // 
            // stackGridView
            // 
            this.stackGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stackGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stackColumn});
            this.stackGridView.Location = new System.Drawing.Point(357, 62);
            this.stackGridView.Name = "stackGridView";
            this.stackGridView.Size = new System.Drawing.Size(151, 120);
            this.stackGridView.TabIndex = 7;
            // 
            // stackColumn
            // 
            this.stackColumn.HeaderText = "Stack-Value";
            this.stackColumn.Name = "stackColumn";
            this.stackColumn.Width = 105;
            // 
            // pcLabel
            // 
            this.pcLabel.AutoSize = true;
            this.pcLabel.Location = new System.Drawing.Point(297, 212);
            this.pcLabel.Name = "pcLabel";
            this.pcLabel.Size = new System.Drawing.Size(10, 13);
            this.pcLabel.TabIndex = 6;
            this.pcLabel.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Program Counter:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rp1Label);
            this.groupBox2.Controls.Add(this.rp0Label);
            this.groupBox2.Controls.Add(this.toLabel);
            this.groupBox2.Controls.Add(this.pdLabel);
            this.groupBox2.Controls.Add(this.zLabel);
            this.groupBox2.Controls.Add(this.dcLabel);
            this.groupBox2.Controls.Add(this.cLabel);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 65);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // rp1Label
            // 
            this.rp1Label.AutoSize = true;
            this.rp1Label.Location = new System.Drawing.Point(173, 39);
            this.rp1Label.Name = "rp1Label";
            this.rp1Label.Size = new System.Drawing.Size(10, 13);
            this.rp1Label.TabIndex = 4;
            this.rp1Label.Text = "-";
            this.rp1Label.Click += new System.EventHandler(this.rp0Label_Click);
            // 
            // rp0Label
            // 
            this.rp0Label.AutoSize = true;
            this.rp0Label.Location = new System.Drawing.Point(139, 39);
            this.rp0Label.Name = "rp0Label";
            this.rp0Label.Size = new System.Drawing.Size(10, 13);
            this.rp0Label.TabIndex = 4;
            this.rp0Label.Text = "-";
            this.rp0Label.Click += new System.EventHandler(this.rp0Label_Click);
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(109, 39);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(10, 13);
            this.toLabel.TabIndex = 4;
            this.toLabel.Text = "-";
            this.toLabel.Click += new System.EventHandler(this.toLabel_Click);
            // 
            // pdLabel
            // 
            this.pdLabel.AutoSize = true;
            this.pdLabel.Location = new System.Drawing.Point(82, 39);
            this.pdLabel.Name = "pdLabel";
            this.pdLabel.Size = new System.Drawing.Size(10, 13);
            this.pdLabel.TabIndex = 4;
            this.pdLabel.Text = "-";
            this.pdLabel.Click += new System.EventHandler(this.pdLabel_Click);
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(58, 39);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(10, 13);
            this.zLabel.TabIndex = 4;
            this.zLabel.Text = "-";
            this.zLabel.Click += new System.EventHandler(this.zLabel_Click);
            // 
            // dcLabel
            // 
            this.dcLabel.AutoSize = true;
            this.dcLabel.Location = new System.Drawing.Point(34, 39);
            this.dcLabel.Name = "dcLabel";
            this.dcLabel.Size = new System.Drawing.Size(10, 13);
            this.dcLabel.TabIndex = 4;
            this.dcLabel.Text = "-";
            this.dcLabel.Click += new System.EventHandler(this.dcLabel_Click);
            // 
            // cLabel
            // 
            this.cLabel.AutoSize = true;
            this.cLabel.Location = new System.Drawing.Point(11, 39);
            this.cLabel.Name = "cLabel";
            this.cLabel.Size = new System.Drawing.Size(10, 13);
            this.cLabel.TabIndex = 4;
            this.cLabel.Text = "-";
            this.cLabel.Click += new System.EventHandler(this.cLabel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(166, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "RP1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(132, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "RP0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "C";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(104, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "TO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "DC";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(76, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "PD";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "W-Register";
            // 
            // wLabel
            // 
            this.wLabel.AutoSize = true;
            this.wLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wLabel.Location = new System.Drawing.Point(137, 204);
            this.wLabel.Name = "wLabel";
            this.wLabel.Size = new System.Drawing.Size(16, 24);
            this.wLabel.TabIndex = 1;
            this.wLabel.Text = "-";
            // 
            // storageGridView
            // 
            this.storageGridView.AllowUserToAddRows = false;
            this.storageGridView.AllowUserToDeleteRows = false;
            this.storageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.storageGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.spalte00,
            this.spalte01,
            this.spalte02,
            this.spalte03,
            this.spalte04,
            this.spalte05,
            this.spalte06,
            this.spalte07});
            this.storageGridView.Location = new System.Drawing.Point(12, 264);
            this.storageGridView.Name = "storageGridView";
            this.storageGridView.ReadOnly = true;
            this.storageGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.storageGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.storageGridView.Size = new System.Drawing.Size(336, 553);
            this.storageGridView.TabIndex = 9;
            // 
            // spalte00
            // 
            this.spalte00.HeaderText = "00";
            this.spalte00.Name = "spalte00";
            this.spalte00.ReadOnly = true;
            this.spalte00.Width = 30;
            // 
            // spalte01
            // 
            this.spalte01.HeaderText = "01";
            this.spalte01.Name = "spalte01";
            this.spalte01.ReadOnly = true;
            this.spalte01.Width = 30;
            // 
            // spalte02
            // 
            this.spalte02.HeaderText = "02";
            this.spalte02.Name = "spalte02";
            this.spalte02.ReadOnly = true;
            this.spalte02.Width = 30;
            // 
            // spalte03
            // 
            this.spalte03.HeaderText = "03";
            this.spalte03.Name = "spalte03";
            this.spalte03.ReadOnly = true;
            this.spalte03.Width = 30;
            // 
            // spalte04
            // 
            this.spalte04.HeaderText = "04";
            this.spalte04.Name = "spalte04";
            this.spalte04.ReadOnly = true;
            this.spalte04.Width = 30;
            // 
            // spalte05
            // 
            this.spalte05.HeaderText = "05";
            this.spalte05.Name = "spalte05";
            this.spalte05.ReadOnly = true;
            this.spalte05.Width = 30;
            // 
            // spalte06
            // 
            this.spalte06.HeaderText = "06";
            this.spalte06.Name = "spalte06";
            this.spalte06.ReadOnly = true;
            this.spalte06.Width = 30;
            // 
            // spalte07
            // 
            this.spalte07.HeaderText = "07";
            this.spalte07.Name = "spalte07";
            this.spalte07.ReadOnly = true;
            this.spalte07.Width = 30;
            // 
            // btn_nextStep
            // 
            this.btn_nextStep.Location = new System.Drawing.Point(1289, 480);
            this.btn_nextStep.Name = "btn_nextStep";
            this.btn_nextStep.Size = new System.Drawing.Size(151, 23);
            this.btn_nextStep.TabIndex = 10;
            this.btn_nextStep.Text = "Next Step";
            this.btn_nextStep.UseVisualStyleBackColor = true;
            this.btn_nextStep.Click += new System.EventHandler(this.btn_nextStep_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1289, 509);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1289, 538);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(151, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 829);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_nextStep);
            this.Controls.Add(this.storageGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stackLabel);
            this.Controls.Add(this.stackGridView);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.dataGridView_Lst);
            this.Controls.Add(this.consoleLabel);
            this.Controls.Add(this.consoleLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Pic Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Lst)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.storageGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menueToolStrip;
        private System.Windows.Forms.ToolStripMenuItem dateiLadenToolStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStrip;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStrip;
        private System.Windows.Forms.TextBox consoleLog;
        private System.Windows.Forms.Label consoleLabel;
        private System.Windows.Forms.DataGridView dataGridView_Lst;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sourcecode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView storageGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte00;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte01;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte02;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte03;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte04;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte05;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte06;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte07;
        private System.Windows.Forms.Button btn_nextStep;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label wLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label rp0Label;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Label pdLabel;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label dcLabel;
        private System.Windows.Forms.Label cLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label pcLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label stackLabel;
        private System.Windows.Forms.DataGridView stackGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn stackColumn;
        private System.Windows.Forms.Label pclLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label pclLabel1;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label rp1Label;
        private System.Windows.Forms.Label label9;
    }
}

