namespace TaleoOutlookAddin.Forms.AddressBook
{
    partial class AddressBookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressBookForm));
            this.wbAddressBook = new System.Windows.Forms.WebBrowser();
            this.displayLabel = new System.Windows.Forms.Label();
            this.candidatesRadioButton = new System.Windows.Forms.RadioButton();
            this.contactsRadioButton = new System.Windows.Forms.RadioButton();
            this.notifierLabel = new System.Windows.Forms.Label();
            this.toRadioButton = new System.Windows.Forms.RadioButton();
            this.ccRadioButton = new System.Windows.Forms.RadioButton();
            this.bccRadioButton = new System.Windows.Forms.RadioButton();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.tbBcc = new System.Windows.Forms.TextBox();
            this.tbCc = new System.Windows.Forms.TextBox();
            this.lbSeparatorLine = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbAddressBook
            // 
            this.wbAddressBook.Location = new System.Drawing.Point(12, 50);
            this.wbAddressBook.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbAddressBook.Name = "wbAddressBook";
            this.wbAddressBook.Size = new System.Drawing.Size(627, 396);
            this.wbAddressBook.TabIndex = 0;
            // 
            // displayLabel
            // 
            this.displayLabel.AutoSize = true;
            this.displayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLabel.Location = new System.Drawing.Point(5, 7);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Size = new System.Drawing.Size(52, 13);
            this.displayLabel.TabIndex = 1;
            this.displayLabel.Text = "Display:";
            // 
            // candidatesRadioButton
            // 
            this.candidatesRadioButton.AutoSize = true;
            this.candidatesRadioButton.Checked = true;
            this.candidatesRadioButton.Location = new System.Drawing.Point(61, 7);
            this.candidatesRadioButton.Name = "candidatesRadioButton";
            this.candidatesRadioButton.Size = new System.Drawing.Size(78, 17);
            this.candidatesRadioButton.TabIndex = 2;
            this.candidatesRadioButton.TabStop = true;
            this.candidatesRadioButton.Text = "Candidates";
            this.candidatesRadioButton.UseVisualStyleBackColor = true;
            this.candidatesRadioButton.Click += new System.EventHandler(this.rbCandidates_Click);
            // 
            // contactsRadioButton
            // 
            this.contactsRadioButton.AutoSize = true;
            this.contactsRadioButton.Location = new System.Drawing.Point(145, 7);
            this.contactsRadioButton.Name = "contactsRadioButton";
            this.contactsRadioButton.Size = new System.Drawing.Size(67, 17);
            this.contactsRadioButton.TabIndex = 3;
            this.contactsRadioButton.Text = "Contacts";
            this.contactsRadioButton.UseVisualStyleBackColor = true;
            this.contactsRadioButton.Click += new System.EventHandler(this.rbContacts_Click);
            // 
            // notifierLabel
            // 
            this.notifierLabel.AutoSize = true;
            this.notifierLabel.Location = new System.Drawing.Point(3, 31);
            this.notifierLabel.Name = "notifierLabel";
            this.notifierLabel.Size = new System.Drawing.Size(140, 13);
            this.notifierLabel.TabIndex = 4;
            this.notifierLabel.Text = "Processing. Please wait.......";
            // 
            // toRadioButton
            // 
            this.toRadioButton.AutoSize = true;
            this.toRadioButton.Checked = true;
            this.toRadioButton.Enabled = false;
            this.toRadioButton.Location = new System.Drawing.Point(12, 462);
            this.toRadioButton.Name = "toRadioButton";
            this.toRadioButton.Size = new System.Drawing.Size(44, 17);
            this.toRadioButton.TabIndex = 5;
            this.toRadioButton.TabStop = true;
            this.toRadioButton.Text = "To :";
            this.toRadioButton.UseVisualStyleBackColor = true;
            // 
            // ccRadioButton
            // 
            this.ccRadioButton.AutoSize = true;
            this.ccRadioButton.Enabled = false;
            this.ccRadioButton.Location = new System.Drawing.Point(12, 485);
            this.ccRadioButton.Name = "ccRadioButton";
            this.ccRadioButton.Size = new System.Drawing.Size(41, 17);
            this.ccRadioButton.TabIndex = 6;
            this.ccRadioButton.Text = "Cc:";
            this.ccRadioButton.UseVisualStyleBackColor = true;
            // 
            // bccRadioButton
            // 
            this.bccRadioButton.AutoSize = true;
            this.bccRadioButton.Enabled = false;
            this.bccRadioButton.Location = new System.Drawing.Point(12, 508);
            this.bccRadioButton.Name = "bccRadioButton";
            this.bccRadioButton.Size = new System.Drawing.Size(47, 17);
            this.bccRadioButton.TabIndex = 7;
            this.bccRadioButton.Text = "Bcc:";
            this.bccRadioButton.UseVisualStyleBackColor = true;
            // 
            // tbTo
            // 
            this.tbTo.Enabled = false;
            this.tbTo.Location = new System.Drawing.Point(56, 462);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(583, 20);
            this.tbTo.TabIndex = 8;
            // 
            // tbBcc
            // 
            this.tbBcc.Enabled = false;
            this.tbBcc.Location = new System.Drawing.Point(56, 508);
            this.tbBcc.Name = "tbBcc";
            this.tbBcc.Size = new System.Drawing.Size(583, 20);
            this.tbBcc.TabIndex = 9;
            // 
            // tbCc
            // 
            this.tbCc.Enabled = false;
            this.tbCc.Location = new System.Drawing.Point(56, 485);
            this.tbCc.Name = "tbCc";
            this.tbCc.Size = new System.Drawing.Size(583, 20);
            this.tbCc.TabIndex = 10;
            // 
            // lbSeparatorLine
            // 
            this.lbSeparatorLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparatorLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeparatorLine.Location = new System.Drawing.Point(13, 541);
            this.lbSeparatorLine.Name = "lbSeparatorLine";
            this.lbSeparatorLine.Size = new System.Drawing.Size(625, 2);
            this.lbSeparatorLine.TabIndex = 11;
            // 
            // okButton
            // 
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(486, 552);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(564, 552);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.contactsRadioButton);
            this.panel1.Controls.Add(this.displayLabel);
            this.panel1.Controls.Add(this.candidatesRadioButton);
            this.panel1.Controls.Add(this.notifierLabel);
            this.panel1.Location = new System.Drawing.Point(7, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 47);
            this.panel1.TabIndex = 14;
            // 
            // AddressBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 580);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.lbSeparatorLine);
            this.Controls.Add(this.tbCc);
            this.Controls.Add(this.tbBcc);
            this.Controls.Add(this.tbTo);
            this.Controls.Add(this.bccRadioButton);
            this.Controls.Add(this.ccRadioButton);
            this.Controls.Add(this.toRadioButton);
            this.Controls.Add(this.wbAddressBook);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddressBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo - Select Names";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbAddressBook;
        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.RadioButton candidatesRadioButton;
        private System.Windows.Forms.RadioButton contactsRadioButton;
        private System.Windows.Forms.Label notifierLabel;
        private System.Windows.Forms.RadioButton ccRadioButton;
        private System.Windows.Forms.RadioButton bccRadioButton;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.TextBox tbBcc;
        private System.Windows.Forms.TextBox tbCc;
        private System.Windows.Forms.Label lbSeparatorLine;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton toRadioButton;
        private System.Windows.Forms.Panel panel1;
    }
}