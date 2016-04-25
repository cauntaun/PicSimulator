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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TRIS = new System.Windows.Forms.TabPage();
            this.Spezialregister = new System.Windows.Forms.TabPage();
            this.c_Checkbox = new System.Windows.Forms.CheckBox();
            this.dc_Checkbox = new System.Windows.Forms.CheckBox();
            this.z_Checkbox = new System.Windows.Forms.CheckBox();
            this.pd_Checkbox = new System.Windows.Forms.CheckBox();
            this.to_Checkbox = new System.Windows.Forms.CheckBox();
            this.rp0_Checkbox = new System.Windows.Forms.CheckBox();
            this.irp_Checkbox = new System.Windows.Forms.CheckBox();
            this.rp1_Checkbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Lst)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Spezialregister.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menueToolStrip,
            this.aboutToolStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1167, 24);
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
            this.consoleLog.Location = new System.Drawing.Point(773, 54);
            this.consoleLog.Multiline = true;
            this.consoleLog.Name = "consoleLog";
            this.consoleLog.ReadOnly = true;
            this.consoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleLog.Size = new System.Drawing.Size(382, 376);
            this.consoleLog.TabIndex = 2;
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleLabel.Location = new System.Drawing.Point(768, 28);
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
            this.dataGridView_Lst.Location = new System.Drawing.Point(12, 436);
            this.dataGridView_Lst.MultiSelect = false;
            this.dataGridView_Lst.Name = "dataGridView_Lst";
            this.dataGridView_Lst.ReadOnly = true;
            this.dataGridView_Lst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Lst.Size = new System.Drawing.Size(1143, 262);
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
            this.btn_Clear.Location = new System.Drawing.Point(1080, 28);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 6;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TRIS);
            this.tabControl1.Controls.Add(this.Spezialregister);
            this.tabControl1.Location = new System.Drawing.Point(12, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(755, 398);
            this.tabControl1.TabIndex = 7;
            // 
            // TRIS
            // 
            this.TRIS.Location = new System.Drawing.Point(4, 22);
            this.TRIS.Name = "TRIS";
            this.TRIS.Padding = new System.Windows.Forms.Padding(3);
            this.TRIS.Size = new System.Drawing.Size(747, 372);
            this.TRIS.TabIndex = 0;
            this.TRIS.Text = "TRIS";
            this.TRIS.UseVisualStyleBackColor = true;
            // 
            // Spezialregister
            // 
            this.Spezialregister.Controls.Add(this.c_Checkbox);
            this.Spezialregister.Controls.Add(this.dc_Checkbox);
            this.Spezialregister.Controls.Add(this.z_Checkbox);
            this.Spezialregister.Controls.Add(this.pd_Checkbox);
            this.Spezialregister.Controls.Add(this.to_Checkbox);
            this.Spezialregister.Controls.Add(this.rp0_Checkbox);
            this.Spezialregister.Controls.Add(this.irp_Checkbox);
            this.Spezialregister.Controls.Add(this.rp1_Checkbox);
            this.Spezialregister.Location = new System.Drawing.Point(4, 22);
            this.Spezialregister.Name = "Spezialregister";
            this.Spezialregister.Padding = new System.Windows.Forms.Padding(3);
            this.Spezialregister.Size = new System.Drawing.Size(747, 372);
            this.Spezialregister.TabIndex = 1;
            this.Spezialregister.Text = "Spezialregister";
            this.Spezialregister.UseVisualStyleBackColor = true;
            // 
            // c_Checkbox
            // 
            this.c_Checkbox.AutoSize = true;
            this.c_Checkbox.Location = new System.Drawing.Point(6, 355);
            this.c_Checkbox.Name = "c_Checkbox";
            this.c_Checkbox.Size = new System.Drawing.Size(33, 17);
            this.c_Checkbox.TabIndex = 7;
            this.c_Checkbox.Text = "C";
            this.c_Checkbox.UseVisualStyleBackColor = true;
            // 
            // dc_Checkbox
            // 
            this.dc_Checkbox.AutoSize = true;
            this.dc_Checkbox.Location = new System.Drawing.Point(6, 332);
            this.dc_Checkbox.Name = "dc_Checkbox";
            this.dc_Checkbox.Size = new System.Drawing.Size(41, 17);
            this.dc_Checkbox.TabIndex = 6;
            this.dc_Checkbox.Text = "DC";
            this.dc_Checkbox.UseVisualStyleBackColor = true;
            // 
            // z_Checkbox
            // 
            this.z_Checkbox.AutoSize = true;
            this.z_Checkbox.Location = new System.Drawing.Point(6, 309);
            this.z_Checkbox.Name = "z_Checkbox";
            this.z_Checkbox.Size = new System.Drawing.Size(33, 17);
            this.z_Checkbox.TabIndex = 5;
            this.z_Checkbox.Text = "Z";
            this.z_Checkbox.UseVisualStyleBackColor = true;
            // 
            // pd_Checkbox
            // 
            this.pd_Checkbox.AutoSize = true;
            this.pd_Checkbox.Location = new System.Drawing.Point(6, 286);
            this.pd_Checkbox.Name = "pd_Checkbox";
            this.pd_Checkbox.Size = new System.Drawing.Size(41, 17);
            this.pd_Checkbox.TabIndex = 4;
            this.pd_Checkbox.Text = "PD";
            this.pd_Checkbox.UseVisualStyleBackColor = true;
            // 
            // to_Checkbox
            // 
            this.to_Checkbox.AutoSize = true;
            this.to_Checkbox.Location = new System.Drawing.Point(6, 263);
            this.to_Checkbox.Name = "to_Checkbox";
            this.to_Checkbox.Size = new System.Drawing.Size(41, 17);
            this.to_Checkbox.TabIndex = 3;
            this.to_Checkbox.Text = "TO";
            this.to_Checkbox.UseVisualStyleBackColor = true;
            // 
            // rp0_Checkbox
            // 
            this.rp0_Checkbox.AutoSize = true;
            this.rp0_Checkbox.Location = new System.Drawing.Point(6, 240);
            this.rp0_Checkbox.Name = "rp0_Checkbox";
            this.rp0_Checkbox.Size = new System.Drawing.Size(47, 17);
            this.rp0_Checkbox.TabIndex = 2;
            this.rp0_Checkbox.Text = "RP0";
            this.rp0_Checkbox.UseVisualStyleBackColor = true;
            // 
            // irp_Checkbox
            // 
            this.irp_Checkbox.AutoSize = true;
            this.irp_Checkbox.Location = new System.Drawing.Point(6, 194);
            this.irp_Checkbox.Name = "irp_Checkbox";
            this.irp_Checkbox.Size = new System.Drawing.Size(44, 17);
            this.irp_Checkbox.TabIndex = 1;
            this.irp_Checkbox.Text = "IRP";
            this.irp_Checkbox.UseVisualStyleBackColor = true;
            // 
            // rp1_Checkbox
            // 
            this.rp1_Checkbox.AutoSize = true;
            this.rp1_Checkbox.Location = new System.Drawing.Point(6, 217);
            this.rp1_Checkbox.Name = "rp1_Checkbox";
            this.rp1_Checkbox.Size = new System.Drawing.Size(47, 17);
            this.rp1_Checkbox.TabIndex = 0;
            this.rp1_Checkbox.Text = "RP1";
            this.rp1_Checkbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 710);
            this.Controls.Add(this.tabControl1);
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
            this.tabControl1.ResumeLayout(false);
            this.Spezialregister.ResumeLayout(false);
            this.Spezialregister.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TRIS;
        private System.Windows.Forms.TabPage Spezialregister;
        private System.Windows.Forms.CheckBox c_Checkbox;
        private System.Windows.Forms.CheckBox dc_Checkbox;
        private System.Windows.Forms.CheckBox z_Checkbox;
        private System.Windows.Forms.CheckBox pd_Checkbox;
        private System.Windows.Forms.CheckBox to_Checkbox;
        private System.Windows.Forms.CheckBox rp0_Checkbox;
        private System.Windows.Forms.CheckBox irp_Checkbox;
        private System.Windows.Forms.CheckBox rp1_Checkbox;
    }
}

