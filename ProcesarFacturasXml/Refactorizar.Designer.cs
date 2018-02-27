namespace ProcesarFacturasXml
{
    partial class Refactorizar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvXmls = new System.Windows.Forms.DataGridView();
            this.dgvPdfs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXmls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPdfs)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 40);
            this.panel1.TabIndex = 1;
            // 
            // dgvXmls
            // 
            this.dgvXmls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvXmls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXmls.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvXmls.Location = new System.Drawing.Point(0, 40);
            this.dgvXmls.Name = "dgvXmls";
            this.dgvXmls.Size = new System.Drawing.Size(497, 221);
            this.dgvXmls.TabIndex = 3;
            // 
            // dgvPdfs
            // 
            this.dgvPdfs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPdfs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPdfs.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvPdfs.Location = new System.Drawing.Point(498, 40);
            this.dgvPdfs.Name = "dgvPdfs";
            this.dgvPdfs.Size = new System.Drawing.Size(497, 221);
            this.dgvPdfs.TabIndex = 4;
            // 
            // Refactorizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 261);
            this.Controls.Add(this.dgvPdfs);
            this.Controls.Add(this.dgvXmls);
            this.Controls.Add(this.panel1);
            this.Name = "Refactorizar";
            this.Text = "Refactorizar";
            ((System.ComponentModel.ISupportInitialize)(this.dgvXmls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPdfs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvXmls;
        private System.Windows.Forms.DataGridView dgvPdfs;
    }
}