namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    partial class LogInFailedMsgForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInFailedMsgForm));
            this.lblLoginFailedWarning = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLoginFailedErrorMsg = new System.Windows.Forms.Label();
            this.btnLogInfailedMsgOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLoginFailedWarning
            // 
            this.lblLoginFailedWarning.AutoSize = true;
            this.lblLoginFailedWarning.Location = new System.Drawing.Point(49, 26);
            this.lblLoginFailedWarning.Name = "lblLoginFailedWarning";
            this.lblLoginFailedWarning.Size = new System.Drawing.Size(316, 26);
            this.lblLoginFailedWarning.TabIndex = 5;
            this.lblLoginFailedWarning.Text = "You have entered invalid credentials, or the taleo website can not\r\nbe connected." +
    " Please re-enter your information and try again.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TaleoOutlookAddin.Properties.Resources.taleo_Warning;
            this.pictureBox1.Location = new System.Drawing.Point(10, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 24);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.lblLoginFailedErrorMsg);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblLoginFailedWarning);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 105);
            this.panel1.TabIndex = 7;
            // 
            // lblLoginFailedErrorMsg
            // 
            this.lblLoginFailedErrorMsg.AutoSize = true;
            this.lblLoginFailedErrorMsg.Location = new System.Drawing.Point(49, 67);
            this.lblLoginFailedErrorMsg.Name = "lblLoginFailedErrorMsg";
            this.lblLoginFailedErrorMsg.Size = new System.Drawing.Size(340, 26);
            this.lblLoginFailedErrorMsg.TabIndex = 7;
            this.lblLoginFailedErrorMsg.Text = "Error message: Invalid User Name or Password for this Company Code.\r\nPlease try a" +
    "gain or ask your Administrator to reset your password.";
            // 
            // btnLogInfailedMsgOk
            // 
            this.btnLogInfailedMsgOk.Location = new System.Drawing.Point(365, 112);
            this.btnLogInfailedMsgOk.Name = "btnLogInfailedMsgOk";
            this.btnLogInfailedMsgOk.Size = new System.Drawing.Size(75, 23);
            this.btnLogInfailedMsgOk.TabIndex = 8;
            this.btnLogInfailedMsgOk.Text = "OK";
            this.btnLogInfailedMsgOk.UseVisualStyleBackColor = true;
            this.btnLogInfailedMsgOk.Click += new System.EventHandler(this.btnLogInfailedMsgOk_Click);
            // 
            // LogInFailedMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 139);
            this.Controls.Add(this.btnLogInfailedMsgOk);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogInFailedMsgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo Outlook Toolbar";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLoginFailedWarning;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLoginFailedErrorMsg;
        private System.Windows.Forms.Button btnLogInfailedMsgOk;
    }
}