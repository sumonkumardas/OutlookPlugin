namespace TaleoOutlookAddin.Forms.Login
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lblTaleoLoginBanner = new System.Windows.Forms.Label();
            this.lblTaleoLoginUserName = new System.Windows.Forms.Label();
            this.tbTaleoLoginUserName = new System.Windows.Forms.TextBox();
            this.lblTaleoLoginPassword = new System.Windows.Forms.Label();
            this.tbTaleoLoginPassword = new System.Windows.Forms.TextBox();
            this.btnTaleoLogin = new System.Windows.Forms.Button();
            this.tbTaleoLoginCompanyCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSeparatorLine = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbRememberPassword = new System.Windows.Forms.CheckBox();
            this.loginPictureBox = new System.Windows.Forms.PictureBox();
            this.lnkTaleoTermsAndCondition = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblLoginCaption = new System.Windows.Forms.Label();
            this.lblLoginCaptionSologan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.loginPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTaleoLoginBanner
            // 
            this.lblTaleoLoginBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaleoLoginBanner.Location = new System.Drawing.Point(59, 88);
            this.lblTaleoLoginBanner.Name = "lblTaleoLoginBanner";
            this.lblTaleoLoginBanner.Size = new System.Drawing.Size(447, 29);
            this.lblTaleoLoginBanner.TabIndex = 0;
            this.lblTaleoLoginBanner.Text = "Enter your user name, password and the company code for your Taleo account, and\r\n" +
    "then click  \"Login\" to login";
            // 
            // lblTaleoLoginUserName
            // 
            this.lblTaleoLoginUserName.AutoSize = true;
            this.lblTaleoLoginUserName.Location = new System.Drawing.Point(132, 150);
            this.lblTaleoLoginUserName.Name = "lblTaleoLoginUserName";
            this.lblTaleoLoginUserName.Size = new System.Drawing.Size(63, 13);
            this.lblTaleoLoginUserName.TabIndex = 1;
            this.lblTaleoLoginUserName.Text = "User Name:";
            // 
            // tbTaleoLoginUserName
            // 
            this.tbTaleoLoginUserName.Location = new System.Drawing.Point(210, 147);
            this.tbTaleoLoginUserName.Name = "tbTaleoLoginUserName";
            this.tbTaleoLoginUserName.Size = new System.Drawing.Size(185, 20);
            this.tbTaleoLoginUserName.TabIndex = 0;
            // 
            // lblTaleoLoginPassword
            // 
            this.lblTaleoLoginPassword.AutoSize = true;
            this.lblTaleoLoginPassword.Location = new System.Drawing.Point(139, 181);
            this.lblTaleoLoginPassword.Name = "lblTaleoLoginPassword";
            this.lblTaleoLoginPassword.Size = new System.Drawing.Size(56, 13);
            this.lblTaleoLoginPassword.TabIndex = 1;
            this.lblTaleoLoginPassword.Text = "Password:";
            // 
            // tbTaleoLoginPassword
            // 
            this.tbTaleoLoginPassword.Location = new System.Drawing.Point(210, 178);
            this.tbTaleoLoginPassword.Name = "tbTaleoLoginPassword";
            this.tbTaleoLoginPassword.PasswordChar = '*';
            this.tbTaleoLoginPassword.Size = new System.Drawing.Size(185, 20);
            this.tbTaleoLoginPassword.TabIndex = 1;
            // 
            // btnTaleoLogin
            // 
            this.btnTaleoLogin.Location = new System.Drawing.Point(374, 437);
            this.btnTaleoLogin.Name = "btnTaleoLogin";
            this.btnTaleoLogin.Size = new System.Drawing.Size(75, 23);
            this.btnTaleoLogin.TabIndex = 4;
            this.btnTaleoLogin.Text = "Login";
            this.btnTaleoLogin.UseVisualStyleBackColor = true;
            this.btnTaleoLogin.Click += new System.EventHandler(this.btnTaleoLogin_Click);
            // 
            // tbTaleoLoginCompanyCode
            // 
            this.tbTaleoLoginCompanyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbTaleoLoginCompanyCode.Location = new System.Drawing.Point(210, 206);
            this.tbTaleoLoginCompanyCode.Name = "tbTaleoLoginCompanyCode";
            this.tbTaleoLoginCompanyCode.Size = new System.Drawing.Size(185, 20);
            this.tbTaleoLoginCompanyCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Company Code:";
            // 
            // lbSeparatorLine
            // 
            this.lbSeparatorLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparatorLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeparatorLine.Location = new System.Drawing.Point(12, 429);
            this.lbSeparatorLine.Name = "lbSeparatorLine";
            this.lbSeparatorLine.Size = new System.Drawing.Size(515, 2);
            this.lbSeparatorLine.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(452, 437);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbRememberPassword
            // 
            this.cbRememberPassword.AutoSize = true;
            this.cbRememberPassword.Location = new System.Drawing.Point(210, 231);
            this.cbRememberPassword.Name = "cbRememberPassword";
            this.cbRememberPassword.Size = new System.Drawing.Size(126, 17);
            this.cbRememberPassword.TabIndex = 8;
            this.cbRememberPassword.Text = "Remember Password";
            this.cbRememberPassword.UseVisualStyleBackColor = true;
            // 
            // loginPictureBox
            // 
            this.loginPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.loginPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("loginPictureBox.Image")));
            this.loginPictureBox.Location = new System.Drawing.Point(2, 1);
            this.loginPictureBox.Name = "loginPictureBox";
            this.loginPictureBox.Size = new System.Drawing.Size(537, 57);
            this.loginPictureBox.TabIndex = 9;
            this.loginPictureBox.TabStop = false;
            // 
            // lnkTaleoTermsAndCondition
            // 
            this.lnkTaleoTermsAndCondition.AutoSize = true;
            this.lnkTaleoTermsAndCondition.LinkColor = System.Drawing.Color.DodgerBlue;
            this.lnkTaleoTermsAndCondition.Location = new System.Drawing.Point(86, 307);
            this.lnkTaleoTermsAndCondition.Name = "lnkTaleoTermsAndCondition";
            this.lnkTaleoTermsAndCondition.Size = new System.Drawing.Size(252, 13);
            this.lnkTaleoTermsAndCondition.TabIndex = 10;
            this.lnkTaleoTermsAndCondition.TabStop = true;
            this.lnkTaleoTermsAndCondition.Text = "Taleo protects your privacy. View our privacy policy.";
            this.lnkTaleoTermsAndCondition.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTaleoTermsAndCondition_LinkClicked);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TaleoOutlookAddin.Properties.Resources.lock_icon;
            this.pictureBox2.Location = new System.Drawing.Point(62, 302);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // lblLoginCaption
            // 
            this.lblLoginCaption.AutoSize = true;
            this.lblLoginCaption.BackColor = System.Drawing.Color.White;
            this.lblLoginCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginCaption.Location = new System.Drawing.Point(69, 12);
            this.lblLoginCaption.Name = "lblLoginCaption";
            this.lblLoginCaption.Size = new System.Drawing.Size(281, 24);
            this.lblLoginCaption.TabIndex = 20;
            this.lblLoginCaption.Text = "Welcome to Taleo in Outlook";
            // 
            // lblLoginCaptionSologan
            // 
            this.lblLoginCaptionSologan.AutoSize = true;
            this.lblLoginCaptionSologan.BackColor = System.Drawing.Color.White;
            this.lblLoginCaptionSologan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginCaptionSologan.Location = new System.Drawing.Point(69, 38);
            this.lblLoginCaptionSologan.Name = "lblLoginCaptionSologan";
            this.lblLoginCaptionSologan.Size = new System.Drawing.Size(210, 13);
            this.lblLoginCaptionSologan.TabIndex = 19;
            this.lblLoginCaptionSologan.Text = "Specify your Taleo login credentials.";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(540, 469);
            this.Controls.Add(this.lblLoginCaption);
            this.Controls.Add(this.lblLoginCaptionSologan);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lnkTaleoTermsAndCondition);
            this.Controls.Add(this.loginPictureBox);
            this.Controls.Add(this.cbRememberPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbSeparatorLine);
            this.Controls.Add(this.tbTaleoLoginCompanyCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTaleoLogin);
            this.Controls.Add(this.tbTaleoLoginPassword);
            this.Controls.Add(this.tbTaleoLoginUserName);
            this.Controls.Add(this.lblTaleoLoginPassword);
            this.Controls.Add(this.lblTaleoLoginUserName);
            this.Controls.Add(this.lblTaleoLoginBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sign in to Taleo";
            ((System.ComponentModel.ISupportInitialize)(this.loginPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTaleoLoginBanner;
        private System.Windows.Forms.Label lblTaleoLoginUserName;
        private System.Windows.Forms.TextBox tbTaleoLoginUserName;
        private System.Windows.Forms.Label lblTaleoLoginPassword;
        private System.Windows.Forms.TextBox tbTaleoLoginPassword;
        private System.Windows.Forms.Button btnTaleoLogin;
        private System.Windows.Forms.TextBox tbTaleoLoginCompanyCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSeparatorLine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbRememberPassword;
        private System.Windows.Forms.PictureBox loginPictureBox;
        private System.Windows.Forms.LinkLabel lnkTaleoTermsAndCondition;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblLoginCaption;
        private System.Windows.Forms.Label lblLoginCaptionSologan;
    }
}