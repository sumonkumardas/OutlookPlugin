namespace TaleoOutlookAddin.Forms.RequestFeedback
{
    partial class RequestFeedBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestFeedBack));
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.reqFeedbackTemplateTextBox = new System.Windows.Forms.TextBox();
            this.ddlTaleoMessage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbRequestFeedbackBanner = new System.Windows.Forms.PictureBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.toButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.privacyPolicyLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.requestFeedBackStepsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbRequestFeedbackBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(174, 79);
            this.toTextBox.Multiline = true;
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.toTextBox.Size = new System.Drawing.Size(387, 21);
            this.toTextBox.TabIndex = 0;
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Location = new System.Drawing.Point(174, 134);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(387, 20);
            this.subjectTextBox.TabIndex = 2;
            // 
            // reqFeedbackTemplateTextBox
            // 
            this.reqFeedbackTemplateTextBox.Location = new System.Drawing.Point(14, 179);
            this.reqFeedbackTemplateTextBox.Multiline = true;
            this.reqFeedbackTemplateTextBox.Name = "reqFeedbackTemplateTextBox";
            this.reqFeedbackTemplateTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.reqFeedbackTemplateTextBox.Size = new System.Drawing.Size(547, 248);
            this.reqFeedbackTemplateTextBox.TabIndex = 3;
            // 
            // ddlTaleoMessage
            // 
            this.ddlTaleoMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlTaleoMessage.FormattingEnabled = true;
            this.ddlTaleoMessage.Items.AddRange(new object[] {
            "Employee Review Feedback",
            "Peer Review Feedback",
            "Exceptional Job Performance Feedback"});
            this.ddlTaleoMessage.Location = new System.Drawing.Point(174, 107);
            this.ddlTaleoMessage.Name = "ddlTaleoMessage";
            this.ddlTaleoMessage.Size = new System.Drawing.Size(387, 21);
            this.ddlTaleoMessage.TabIndex = 4;
            this.ddlTaleoMessage.SelectedIndexChanged += new System.EventHandler(this.ddlTaleoMessage_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Subject:";
            // 
            // pbRequestFeedbackBanner
            // 
            this.pbRequestFeedbackBanner.BackColor = System.Drawing.Color.White;
            this.pbRequestFeedbackBanner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbRequestFeedbackBanner.Image = global::TaleoOutlookAddin.Properties.Resources.requestfeedbackbanner;
            this.pbRequestFeedbackBanner.Location = new System.Drawing.Point(-1, 1);
            this.pbRequestFeedbackBanner.Name = "pbRequestFeedbackBanner";
            this.pbRequestFeedbackBanner.Size = new System.Drawing.Size(584, 65);
            this.pbRequestFeedbackBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRequestFeedbackBanner.TabIndex = 9;
            this.pbRequestFeedbackBanner.TabStop = false;
            // 
            // sendButton
            // 
            this.sendButton.Image = global::TaleoOutlookAddin.Properties.Resources.toButton;
            this.sendButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sendButton.Location = new System.Drawing.Point(12, 79);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 75);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // toButton
            // 
            this.toButton.Image = global::TaleoOutlookAddin.Properties.Resources.send;
            this.toButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toButton.Location = new System.Drawing.Point(89, 79);
            this.toButton.Name = "toButton";
            this.toButton.Size = new System.Drawing.Size(75, 21);
            this.toButton.TabIndex = 5;
            this.toButton.Text = "To...";
            this.toButton.UseVisualStyleBackColor = true;
            this.toButton.Click += new System.EventHandler(this.toButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TaleoOutlookAddin.Properties.Resources.lock_icon;
            this.pictureBox2.Location = new System.Drawing.Point(12, 430);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // privacyPolicyLinkLabel
            // 
            this.privacyPolicyLinkLabel.AutoSize = true;
            this.privacyPolicyLinkLabel.Location = new System.Drawing.Point(46, 437);
            this.privacyPolicyLinkLabel.Name = "privacyPolicyLinkLabel";
            this.privacyPolicyLinkLabel.Size = new System.Drawing.Size(249, 13);
            this.privacyPolicyLinkLabel.TabIndex = 11;
            this.privacyPolicyLinkLabel.TabStop = true;
            this.privacyPolicyLinkLabel.Text = "Taleo protects your privacy. View our privacy policy";
            this.privacyPolicyLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Request Feedback";
            // 
            // requestFeedBackStepsLabel
            // 
            this.requestFeedBackStepsLabel.AutoSize = true;
            this.requestFeedBackStepsLabel.BackColor = System.Drawing.Color.Transparent;
            this.requestFeedBackStepsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestFeedBackStepsLabel.Location = new System.Drawing.Point(93, 35);
            this.requestFeedBackStepsLabel.Name = "requestFeedBackStepsLabel";
            this.requestFeedBackStepsLabel.Size = new System.Drawing.Size(325, 13);
            this.requestFeedBackStepsLabel.TabIndex = 16;
            this.requestFeedBackStepsLabel.Text = "Request employee feedback from designated recipients.";
            // 
            // RequestFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(577, 458);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.requestFeedBackStepsLabel);
            this.Controls.Add(this.privacyPolicyLinkLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pbRequestFeedbackBanner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.toButton);
            this.Controls.Add(this.ddlTaleoMessage);
            this.Controls.Add(this.reqFeedbackTemplateTextBox);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.toTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RequestFeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo - Request Feedback";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RequestFeedBack_FormClosing);
            this.Load += new System.EventHandler(this.RequestFeedBack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRequestFeedbackBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.TextBox reqFeedbackTemplateTextBox;
        private System.Windows.Forms.ComboBox ddlTaleoMessage;
        private System.Windows.Forms.Button toButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbRequestFeedbackBanner;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel privacyPolicyLinkLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label requestFeedBackStepsLabel;
    }
}