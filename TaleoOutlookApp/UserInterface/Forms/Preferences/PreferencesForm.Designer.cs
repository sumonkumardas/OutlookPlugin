namespace TaleoOutlookAddin.Forms.Preferences
{
    partial class PreferencesForm
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
            this.advanceButton = new System.Windows.Forms.Button();
            this.feedbackButton = new System.Windows.Forms.Button();
            this.accountButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.languageButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.languagePanel = new System.Windows.Forms.Panel();
            this.languageSelectComboBox = new System.Windows.Forms.ComboBox();
            this.selectLanguageLabel = new System.Windows.Forms.Label();
            this.advancePanel = new System.Windows.Forms.Panel();
            this.advanceCheckBox = new System.Windows.Forms.CheckBox();
            this.feedbackPanel = new System.Windows.Forms.Panel();
            this.signatureTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.accountPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.rememberPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.companyCodeTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.preferencePictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.languagePanel.SuspendLayout();
            this.advancePanel.SuspendLayout();
            this.feedbackPanel.SuspendLayout();
            this.accountPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preferencePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // advanceButton
            // 
            this.advanceButton.ForeColor = System.Drawing.Color.Navy;
            this.advanceButton.Location = new System.Drawing.Point(6, 96);
            this.advanceButton.Name = "advanceButton";
            this.advanceButton.Size = new System.Drawing.Size(78, 23);
            this.advanceButton.TabIndex = 3;
            this.advanceButton.Text = "Advanced";
            this.advanceButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.advanceButton.UseVisualStyleBackColor = true;
            this.advanceButton.Click += new System.EventHandler(this.advanceButton_Click);
            // 
            // feedbackButton
            // 
            this.feedbackButton.ForeColor = System.Drawing.Color.Navy;
            this.feedbackButton.Location = new System.Drawing.Point(6, 38);
            this.feedbackButton.Name = "feedbackButton";
            this.feedbackButton.Size = new System.Drawing.Size(78, 23);
            this.feedbackButton.TabIndex = 2;
            this.feedbackButton.Text = "Feedback";
            this.feedbackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.feedbackButton.UseVisualStyleBackColor = true;
            this.feedbackButton.Click += new System.EventHandler(this.feedbackButton_Click);
            // 
            // accountButton
            // 
            this.accountButton.ForeColor = System.Drawing.Color.Navy;
            this.accountButton.Location = new System.Drawing.Point(6, 9);
            this.accountButton.Name = "accountButton";
            this.accountButton.Size = new System.Drawing.Size(78, 23);
            this.accountButton.TabIndex = 1;
            this.accountButton.Text = "Account";
            this.accountButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.accountButton.UseVisualStyleBackColor = true;
            this.accountButton.Click += new System.EventHandler(this.accountButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(327, 339);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(408, 339);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.languageButton);
            this.panel1.Controls.Add(this.advanceButton);
            this.panel1.Controls.Add(this.accountButton);
            this.panel1.Controls.Add(this.feedbackButton);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, 320);
            this.panel1.TabIndex = 0;
            // 
            // languageButton
            // 
            this.languageButton.ForeColor = System.Drawing.Color.Navy;
            this.languageButton.Location = new System.Drawing.Point(6, 67);
            this.languageButton.Name = "languageButton";
            this.languageButton.Size = new System.Drawing.Size(78, 23);
            this.languageButton.TabIndex = 4;
            this.languageButton.Text = "Language";
            this.languageButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.languageButton.UseVisualStyleBackColor = true;
            this.languageButton.Click += new System.EventHandler(this.languageButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.languagePanel);
            this.panel2.Controls.Add(this.advancePanel);
            this.panel2.Controls.Add(this.feedbackPanel);
            this.panel2.Controls.Add(this.accountPanel);
            this.panel2.Controls.Add(this.preferencePictureBox);
            this.panel2.Location = new System.Drawing.Point(108, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(375, 320);
            this.panel2.TabIndex = 4;
            // 
            // languagePanel
            // 
            this.languagePanel.Controls.Add(this.languageSelectComboBox);
            this.languagePanel.Controls.Add(this.selectLanguageLabel);
            this.languagePanel.Location = new System.Drawing.Point(3, 36);
            this.languagePanel.Name = "languagePanel";
            this.languagePanel.Size = new System.Drawing.Size(370, 281);
            this.languagePanel.TabIndex = 1;
            // 
            // languageSelectComboBox
            // 
            this.languageSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageSelectComboBox.FormattingEnabled = true;
            this.languageSelectComboBox.Location = new System.Drawing.Point(147, 37);
            this.languageSelectComboBox.Name = "languageSelectComboBox";
            this.languageSelectComboBox.Size = new System.Drawing.Size(188, 21);
            this.languageSelectComboBox.TabIndex = 1;
            // 
            // selectLanguageLabel
            // 
            this.selectLanguageLabel.AutoSize = true;
            this.selectLanguageLabel.Location = new System.Drawing.Point(22, 40);
            this.selectLanguageLabel.Name = "selectLanguageLabel";
            this.selectLanguageLabel.Size = new System.Drawing.Size(119, 13);
            this.selectLanguageLabel.TabIndex = 0;
            this.selectLanguageLabel.Text = "Select Your Language :";
            // 
            // advancePanel
            // 
            this.advancePanel.Controls.Add(this.advanceCheckBox);
            this.advancePanel.Location = new System.Drawing.Point(3, 36);
            this.advancePanel.Name = "advancePanel";
            this.advancePanel.Size = new System.Drawing.Size(370, 280);
            this.advancePanel.TabIndex = 2;
            // 
            // advanceCheckBox
            // 
            this.advanceCheckBox.AutoSize = true;
            this.advanceCheckBox.Checked = true;
            this.advanceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.advanceCheckBox.Location = new System.Drawing.Point(39, 31);
            this.advanceCheckBox.Name = "advanceCheckBox";
            this.advanceCheckBox.Size = new System.Drawing.Size(310, 17);
            this.advanceCheckBox.TabIndex = 0;
            this.advanceCheckBox.Text = "Animate Taleo button when Resource Center site is updated";
            this.advanceCheckBox.UseVisualStyleBackColor = true;
            // 
            // feedbackPanel
            // 
            this.feedbackPanel.Controls.Add(this.signatureTextBox);
            this.feedbackPanel.Controls.Add(this.label5);
            this.feedbackPanel.Location = new System.Drawing.Point(3, 36);
            this.feedbackPanel.Name = "feedbackPanel";
            this.feedbackPanel.Size = new System.Drawing.Size(370, 280);
            this.feedbackPanel.TabIndex = 9;
            // 
            // signatureTextBox
            // 
            this.signatureTextBox.Location = new System.Drawing.Point(22, 45);
            this.signatureTextBox.Multiline = true;
            this.signatureTextBox.Name = "signatureTextBox";
            this.signatureTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.signatureTextBox.Size = new System.Drawing.Size(325, 128);
            this.signatureTextBox.TabIndex = 1;
            this.signatureTextBox.Text = "Thanks";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Feedback signature:";
            // 
            // accountPanel
            // 
            this.accountPanel.Controls.Add(this.resetButton);
            this.accountPanel.Controls.Add(this.rememberPasswordCheckBox);
            this.accountPanel.Controls.Add(this.companyCodeTextBox);
            this.accountPanel.Controls.Add(this.passwordTextBox);
            this.accountPanel.Controls.Add(this.userNameTextBox);
            this.accountPanel.Controls.Add(this.label4);
            this.accountPanel.Controls.Add(this.label3);
            this.accountPanel.Controls.Add(this.label2);
            this.accountPanel.Controls.Add(this.label1);
            this.accountPanel.Location = new System.Drawing.Point(3, 36);
            this.accountPanel.Name = "accountPanel";
            this.accountPanel.Size = new System.Drawing.Size(370, 280);
            this.accountPanel.TabIndex = 1;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(248, 195);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset...";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // rememberPasswordCheckBox
            // 
            this.rememberPasswordCheckBox.AutoSize = true;
            this.rememberPasswordCheckBox.Checked = true;
            this.rememberPasswordCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rememberPasswordCheckBox.Location = new System.Drawing.Point(125, 152);
            this.rememberPasswordCheckBox.Name = "rememberPasswordCheckBox";
            this.rememberPasswordCheckBox.Size = new System.Drawing.Size(126, 17);
            this.rememberPasswordCheckBox.TabIndex = 7;
            this.rememberPasswordCheckBox.Text = "Remember Password";
            this.rememberPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // companyCodeTextBox
            // 
            this.companyCodeTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.companyCodeTextBox.Location = new System.Drawing.Point(125, 114);
            this.companyCodeTextBox.Name = "companyCodeTextBox";
            this.companyCodeTextBox.Size = new System.Drawing.Size(178, 20);
            this.companyCodeTextBox.TabIndex = 6;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(125, 88);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(178, 20);
            this.passwordTextBox.TabIndex = 5;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(125, 62);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(179, 20);
            this.userNameTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Company Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User name:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your user name, password and company code for your Taleo account:";
            // 
            // preferencePictureBox
            // 
            this.preferencePictureBox.Image = global::TaleoOutlookAddin.Properties.Resources.preferenceAccount;
            this.preferencePictureBox.Location = new System.Drawing.Point(0, 0);
            this.preferencePictureBox.Name = "preferencePictureBox";
            this.preferencePictureBox.Size = new System.Drawing.Size(375, 32);
            this.preferencePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.preferencePictureBox.TabIndex = 0;
            this.preferencePictureBox.TabStop = false;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 372);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OkButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(510, 410);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 410);
            this.Name = "PreferencesForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo - Preferences";
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.languagePanel.ResumeLayout(false);
            this.languagePanel.PerformLayout();
            this.advancePanel.ResumeLayout(false);
            this.advancePanel.PerformLayout();
            this.feedbackPanel.ResumeLayout(false);
            this.feedbackPanel.PerformLayout();
            this.accountPanel.ResumeLayout(false);
            this.accountPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preferencePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button advanceButton;
        private System.Windows.Forms.Button feedbackButton;
        private System.Windows.Forms.Button accountButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox preferencePictureBox;
        private System.Windows.Forms.Panel accountPanel;
        private System.Windows.Forms.TextBox companyCodeTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox rememberPasswordCheckBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel feedbackPanel;
        private System.Windows.Forms.TextBox signatureTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel advancePanel;
        private System.Windows.Forms.CheckBox advanceCheckBox;
        private System.Windows.Forms.Button languageButton;
        private System.Windows.Forms.Panel languagePanel;
        private System.Windows.Forms.ComboBox languageSelectComboBox;
        private System.Windows.Forms.Label selectLanguageLabel;
    }
}