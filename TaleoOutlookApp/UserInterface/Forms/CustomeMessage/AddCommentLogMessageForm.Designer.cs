namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    partial class AddCommentLogMessageForm
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
            this.addCommentLogPictureBox = new System.Windows.Forms.PictureBox();
            this.addCommentLogLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.addCommentLogPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addCommentLogPictureBox
            // 
            this.addCommentLogPictureBox.Image = global::TaleoOutlookAddin.Properties.Resources.AddCommentAnimation;
            this.addCommentLogPictureBox.Location = new System.Drawing.Point(2, 2);
            this.addCommentLogPictureBox.Name = "addCommentLogPictureBox";
            this.addCommentLogPictureBox.Size = new System.Drawing.Size(54, 45);
            this.addCommentLogPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.addCommentLogPictureBox.TabIndex = 0;
            this.addCommentLogPictureBox.TabStop = false;
            // 
            // addCommentLogLabel
            // 
            this.addCommentLogLabel.Location = new System.Drawing.Point(62, 13);
            this.addCommentLogLabel.Name = "addCommentLogLabel";
            this.addCommentLogLabel.Size = new System.Drawing.Size(350, 23);
            this.addCommentLogLabel.TabIndex = 1;
            this.addCommentLogLabel.Text = "Please wait while a new log is being added. It may take a few minutes…";
            this.addCommentLogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AddCommentLogMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 51);
            this.Controls.Add(this.addCommentLogLabel);
            this.Controls.Add(this.addCommentLogPictureBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 90);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 90);
            this.Name = "AddCommentLogMessageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo Outlook Toolbar";
            ((System.ComponentModel.ISupportInitialize)(this.addCommentLogPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox addCommentLogPictureBox;
        private System.Windows.Forms.Label addCommentLogLabel;
    }
}