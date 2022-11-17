namespace TaleoOutlookAddin.Forms.AboutOracleTaleoForm
{
    partial class AboutOracleTaleoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutOracleTaleoForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAboutUsOk = new System.Windows.Forms.Button();
            this.lblAboutUsWarning = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblAboutUsProductId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbAboutUsMSLicense = new System.Windows.Forms.Label();
            this.lbAboutUsCopyRight = new System.Windows.Forms.Label();
            this.lbAboutUsVersion = new System.Windows.Forms.Label();
            this.lbAboutUsHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TaleoOutlookAddin.Properties.Resources.taleoAboutUsLogo;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(438, 65);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnAboutUsOk);
            this.panel1.Controls.Add(this.lblAboutUsWarning);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbAboutUsMSLicense);
            this.panel1.Controls.Add(this.lbAboutUsCopyRight);
            this.panel1.Controls.Add(this.lbAboutUsVersion);
            this.panel1.Controls.Add(this.lbAboutUsHeader);
            this.panel1.Location = new System.Drawing.Point(-2, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 256);
            this.panel1.TabIndex = 11;
            // 
            // btnAboutUsOk
            // 
            this.btnAboutUsOk.Location = new System.Drawing.Point(350, 219);
            this.btnAboutUsOk.Name = "btnAboutUsOk";
            this.btnAboutUsOk.Size = new System.Drawing.Size(75, 23);
            this.btnAboutUsOk.TabIndex = 6;
            this.btnAboutUsOk.Text = "OK";
            this.btnAboutUsOk.UseVisualStyleBackColor = true;
            this.btnAboutUsOk.Click += new System.EventHandler(this.btnAboutUsOk_Click);
            // 
            // lblAboutUsWarning
            // 
            this.lblAboutUsWarning.Location = new System.Drawing.Point(14, 156);
            this.lblAboutUsWarning.Name = "lblAboutUsWarning";
            this.lblAboutUsWarning.Size = new System.Drawing.Size(411, 64);
            this.lblAboutUsWarning.TabIndex = 5;
            this.lblAboutUsWarning.Text = resources.GetString("lblAboutUsWarning.Text");
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblAboutUsProductId);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(14, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 57);
            this.panel2.TabIndex = 4;
            // 
            // lblAboutUsProductId
            // 
            this.lblAboutUsProductId.AutoSize = true;
            this.lblAboutUsProductId.Location = new System.Drawing.Point(60, 35);
            this.lblAboutUsProductId.Name = "lblAboutUsProductId";
            this.lblAboutUsProductId.Size = new System.Drawing.Size(70, 13);
            this.lblAboutUsProductId.TabIndex = 5;
            this.lblAboutUsProductId.Text = "Not Available";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Oracle Corp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Product Id:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oracle";
            // 
            // lbAboutUsMSLicense
            // 
            this.lbAboutUsMSLicense.AutoSize = true;
            this.lbAboutUsMSLicense.Location = new System.Drawing.Point(14, 75);
            this.lbAboutUsMSLicense.Name = "lbAboutUsMSLicense";
            this.lbAboutUsMSLicense.Size = new System.Drawing.Size(154, 13);
            this.lbAboutUsMSLicense.TabIndex = 3;
            this.lbAboutUsMSLicense.Text = "Microsoft Outlook is licensed to";
            // 
            // lbAboutUsCopyRight
            // 
            this.lbAboutUsCopyRight.AutoSize = true;
            this.lbAboutUsCopyRight.Location = new System.Drawing.Point(14, 43);
            this.lbAboutUsCopyRight.Name = "lbAboutUsCopyRight";
            this.lbAboutUsCopyRight.Size = new System.Drawing.Size(296, 13);
            this.lbAboutUsCopyRight.TabIndex = 2;
            this.lbAboutUsCopyRight.Text = "Copyright © 2007-2011 Taleo Corporation, All rights reserved.";
            // 
            // lbAboutUsVersion
            // 
            this.lbAboutUsVersion.AutoSize = true;
            this.lbAboutUsVersion.Location = new System.Drawing.Point(14, 27);
            this.lbAboutUsVersion.Name = "lbAboutUsVersion";
            this.lbAboutUsVersion.Size = new System.Drawing.Size(96, 13);
            this.lbAboutUsVersion.TabIndex = 1;
            this.lbAboutUsVersion.Text = "Version 3.0.0";
            // 
            // lbAboutUsHeader
            // 
            this.lbAboutUsHeader.AutoSize = true;
            this.lbAboutUsHeader.Location = new System.Drawing.Point(14, 11);
            this.lbAboutUsHeader.Name = "lbAboutUsHeader";
            this.lbAboutUsHeader.Size = new System.Drawing.Size(302, 13);
            this.lbAboutUsHeader.TabIndex = 0;
            this.lbAboutUsHeader.Text = "Oracle Taleo Business Edition Cloud Service - Outlook Toolbar";
            // 
            // AboutOracleTaleoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(435, 320);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutOracleTaleoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Oracle Taleo Outlook Toolbar";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbAboutUsMSLicense;
        private System.Windows.Forms.Label lbAboutUsCopyRight;
        private System.Windows.Forms.Label lbAboutUsVersion;
        private System.Windows.Forms.Label lbAboutUsHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblAboutUsProductId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAboutUsWarning;
        private System.Windows.Forms.Button btnAboutUsOk;
    }
}