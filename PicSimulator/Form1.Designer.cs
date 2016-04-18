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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menueToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiLadenToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewLst = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLst)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menueToolStrip,
            this.aboutToolStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(843, 24);
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
            // dataGridViewLst
            // 
            this.dataGridViewLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLst.Location = new System.Drawing.Point(13, 28);
            this.dataGridViewLst.Name = "dataGridViewLst";
            this.dataGridViewLst.Size = new System.Drawing.Size(541, 563);
            this.dataGridViewLst.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 603);
            this.Controls.Add(this.dataGridViewLst);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Pic Simulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menueToolStrip;
        private System.Windows.Forms.ToolStripMenuItem dateiLadenToolStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStrip;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStrip;
        private System.Windows.Forms.DataGridView dataGridViewLst;
    }
}

