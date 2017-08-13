namespace MacChangerProject
{
    partial class MacChanger
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.btnChangeMac = new System.Windows.Forms.Button();
            this.btnShowAllMac = new System.Windows.Forms.Button();
            this.btnAddNewMac = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCurrentMac = new System.Windows.Forms.Label();
            this.btnCurrentMac = new System.Windows.Forms.Button();
            this.btnThisMacOnline = new System.Windows.Forms.Button();
            this.txtMacToCheck = new System.Windows.Forms.TextBox();
            this.cbFromAllMac = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "MAC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Manufacturer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "HostName";
            // 
            // txtMac
            // 
            this.txtMac.Location = new System.Drawing.Point(109, 11);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(287, 20);
            this.txtMac.TabIndex = 3;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Location = new System.Drawing.Point(109, 50);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(287, 20);
            this.txtManufacturer.TabIndex = 4;
            // 
            // txtHostName
            // 
            this.txtHostName.Location = new System.Drawing.Point(109, 88);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(287, 20);
            this.txtHostName.TabIndex = 5;
            // 
            // btnChangeMac
            // 
            this.btnChangeMac.Location = new System.Drawing.Point(226, 128);
            this.btnChangeMac.Name = "btnChangeMac";
            this.btnChangeMac.Size = new System.Drawing.Size(123, 23);
            this.btnChangeMac.TabIndex = 6;
            this.btnChangeMac.Text = "Change New MAC";
            this.btnChangeMac.UseVisualStyleBackColor = true;
            this.btnChangeMac.Click += new System.EventHandler(this.btnChangeMac_Click);
            // 
            // btnShowAllMac
            // 
            this.btnShowAllMac.Location = new System.Drawing.Point(24, 253);
            this.btnShowAllMac.Name = "btnShowAllMac";
            this.btnShowAllMac.Size = new System.Drawing.Size(101, 23);
            this.btnShowAllMac.TabIndex = 7;
            this.btnShowAllMac.Text = "Show All MAC";
            this.btnShowAllMac.UseVisualStyleBackColor = true;
            this.btnShowAllMac.Click += new System.EventHandler(this.btnShowAllMac_Click);
            // 
            // btnAddNewMac
            // 
            this.btnAddNewMac.Location = new System.Drawing.Point(150, 253);
            this.btnAddNewMac.Name = "btnAddNewMac";
            this.btnAddNewMac.Size = new System.Drawing.Size(101, 23);
            this.btnAddNewMac.TabIndex = 8;
            this.btnAddNewMac.Text = "Add New MAC";
            this.btnAddNewMac.UseVisualStyleBackColor = true;
            this.btnAddNewMac.Click += new System.EventHandler(this.btnAddNewMac_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label4.Location = new System.Drawing.Point(12, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(419, 1);
            this.label4.TabIndex = 9;
            // 
            // lbCurrentMac
            // 
            this.lbCurrentMac.AutoSize = true;
            this.lbCurrentMac.Location = new System.Drawing.Point(125, 178);
            this.lbCurrentMac.Name = "lbCurrentMac";
            this.lbCurrentMac.Size = new System.Drawing.Size(0, 13);
            this.lbCurrentMac.TabIndex = 11;
            // 
            // btnCurrentMac
            // 
            this.btnCurrentMac.Location = new System.Drawing.Point(25, 173);
            this.btnCurrentMac.Name = "btnCurrentMac";
            this.btnCurrentMac.Size = new System.Drawing.Size(75, 23);
            this.btnCurrentMac.TabIndex = 12;
            this.btnCurrentMac.Text = "Current MAC";
            this.btnCurrentMac.UseVisualStyleBackColor = true;
            this.btnCurrentMac.Click += new System.EventHandler(this.btnCurrentMac_Click);
            // 
            // btnThisMacOnline
            // 
            this.btnThisMacOnline.Location = new System.Drawing.Point(24, 202);
            this.btnThisMacOnline.Name = "btnThisMacOnline";
            this.btnThisMacOnline.Size = new System.Drawing.Size(117, 23);
            this.btnThisMacOnline.TabIndex = 13;
            this.btnThisMacOnline.Text = "Is This MAC Online?";
            this.btnThisMacOnline.UseVisualStyleBackColor = true;
            this.btnThisMacOnline.Click += new System.EventHandler(this.btnThisMacOnline_Click);
            // 
            // txtMacToCheck
            // 
            this.txtMacToCheck.Location = new System.Drawing.Point(150, 204);
            this.txtMacToCheck.Name = "txtMacToCheck";
            this.txtMacToCheck.Size = new System.Drawing.Size(246, 20);
            this.txtMacToCheck.TabIndex = 14;
            // 
            // cbFromAllMac
            // 
            this.cbFromAllMac.AutoSize = true;
            this.cbFromAllMac.Checked = true;
            this.cbFromAllMac.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFromAllMac.Location = new System.Drawing.Point(83, 132);
            this.cbFromAllMac.Name = "cbFromAllMac";
            this.cbFromAllMac.Size = new System.Drawing.Size(109, 17);
            this.cbFromAllMac.TabIndex = 15;
            this.cbFromAllMac.Text = "Get From All MAC";
            this.cbFromAllMac.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label5.Location = new System.Drawing.Point(12, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(419, 1);
            this.label5.TabIndex = 16;
            // 
            // MacChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 288);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbFromAllMac);
            this.Controls.Add(this.txtMacToCheck);
            this.Controls.Add(this.btnThisMacOnline);
            this.Controls.Add(this.btnCurrentMac);
            this.Controls.Add(this.lbCurrentMac);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAddNewMac);
            this.Controls.Add(this.btnShowAllMac);
            this.Controls.Add(this.btnChangeMac);
            this.Controls.Add(this.txtHostName);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(this.txtMac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MacChanger";
            this.Text = "Mac Changer Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MacChanger_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MacChanger_FormClosed);
            this.Load += new System.EventHandler(this.MacChanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.Button btnChangeMac;
        private System.Windows.Forms.Button btnShowAllMac;
        private System.Windows.Forms.Button btnAddNewMac;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCurrentMac;
        private System.Windows.Forms.Button btnCurrentMac;
        private System.Windows.Forms.Button btnThisMacOnline;
        private System.Windows.Forms.TextBox txtMacToCheck;
        private System.Windows.Forms.CheckBox cbFromAllMac;
        private System.Windows.Forms.Label label5;
    }
}

