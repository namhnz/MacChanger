namespace MacChangerProject
{
    partial class AllMacList
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
            this.dgvAllMac = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllMac)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAllMac
            // 
            this.dgvAllMac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllMac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllMac.Location = new System.Drawing.Point(0, 0);
            this.dgvAllMac.Name = "dgvAllMac";
            this.dgvAllMac.Size = new System.Drawing.Size(448, 306);
            this.dgvAllMac.TabIndex = 0;
            // 
            // AllMacList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 306);
            this.Controls.Add(this.dgvAllMac);
            this.Name = "AllMacList";
            this.Text = "All MAC";
            this.Load += new System.EventHandler(this.AllMacList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllMac)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllMac;
    }
}