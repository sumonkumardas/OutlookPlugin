namespace TaleoOutlookAddin.Forms.AddComments
{
    partial class AddCommentsForm
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblLoginCaption = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.headerPictureBox = new System.Windows.Forms.PictureBox();
            this.instructionalLabel = new System.Windows.Forms.Label();
            this.addCommentTextBox = new System.Windows.Forms.TextBox();
            this.separatorLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.addCommentWebBrowser = new System.Windows.Forms.WebBrowser();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.lblLoginCaption);
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.headerPictureBox);
            this.headerPanel.Location = new System.Drawing.Point(-1, -2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(587, 68);
            this.headerPanel.TabIndex = 0;
            // 
            // lblLoginCaption
            // 
            this.lblLoginCaption.AutoSize = true;
            this.lblLoginCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblLoginCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginCaption.Location = new System.Drawing.Point(62, 16);
            this.lblLoginCaption.Name = "lblLoginCaption";
            this.lblLoginCaption.Size = new System.Drawing.Size(153, 24);
            this.lblLoginCaption.TabIndex = 21;
            this.lblLoginCaption.Text = "Add Comments";
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitleLabel.Location = new System.Drawing.Point(62, 42);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(300, 13);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Add comments to an existing candidate. Step 1 of 3";
            // 
            // headerPictureBox
            // 
            this.headerPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerPictureBox.Image = global::TaleoOutlookAddin.Properties.Resources.addCommentsBanner;
            this.headerPictureBox.InitialImage = null;
            this.headerPictureBox.Location = new System.Drawing.Point(0, 0);
            this.headerPictureBox.Name = "headerPictureBox";
            this.headerPictureBox.Size = new System.Drawing.Size(587, 68);
            this.headerPictureBox.TabIndex = 0;
            this.headerPictureBox.TabStop = false;
            // 
            // instructionalLabel
            // 
            this.instructionalLabel.AutoSize = true;
            this.instructionalLabel.Location = new System.Drawing.Point(12, 79);
            this.instructionalLabel.Name = "instructionalLabel";
            this.instructionalLabel.Size = new System.Drawing.Size(400, 13);
            this.instructionalLabel.TabIndex = 1;
            this.instructionalLabel.Text = "Enter the comments to be added to the candidate’s record. Click “Next” to continu" +
    "e";
            // 
            // addCommentTextBox
            // 
            this.addCommentTextBox.Location = new System.Drawing.Point(12, 107);
            this.addCommentTextBox.Multiline = true;
            this.addCommentTextBox.Name = "addCommentTextBox";
            this.addCommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.addCommentTextBox.Size = new System.Drawing.Size(560, 206);
            this.addCommentTextBox.TabIndex = 2;
            // 
            // separatorLabel
            // 
            this.separatorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separatorLabel.Location = new System.Drawing.Point(12, 392);
            this.separatorLabel.Name = "separatorLabel";
            this.separatorLabel.Size = new System.Drawing.Size(560, 2);
            this.separatorLabel.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(335, 406);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(416, 406);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(497, 406);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = "Next >";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // addCommentWebBrowser
            // 
            this.addCommentWebBrowser.Location = new System.Drawing.Point(12, 107);
            this.addCommentWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.addCommentWebBrowser.Name = "addCommentWebBrowser";
            this.addCommentWebBrowser.Size = new System.Drawing.Size(560, 273);
            this.addCommentWebBrowser.TabIndex = 7;
            // 
            // AddCommentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.addCommentWebBrowser);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.separatorLabel);
            this.Controls.Add(this.addCommentTextBox);
            this.Controls.Add(this.instructionalLabel);
            this.Controls.Add(this.headerPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCommentsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo -  Add to Taleo";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.PictureBox headerPictureBox;
        private System.Windows.Forms.Label instructionalLabel;
        private System.Windows.Forms.TextBox addCommentTextBox;
        private System.Windows.Forms.Label separatorLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.WebBrowser addCommentWebBrowser;
        private System.Windows.Forms.Label lblLoginCaption;
    }
}