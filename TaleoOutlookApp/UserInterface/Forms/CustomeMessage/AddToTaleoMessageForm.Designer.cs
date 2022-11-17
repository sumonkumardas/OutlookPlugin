namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    partial class AddToTaleoMessageForm
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
            this.addToTaleoMessageLabel = new System.Windows.Forms.Label();
            this.addToTaleoMessagePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.addToTaleoMessagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addToTaleoMessageLabel
            // 
            this.addToTaleoMessageLabel.Location = new System.Drawing.Point(67, 14);
            this.addToTaleoMessageLabel.Name = "addToTaleoMessageLabel";
            this.addToTaleoMessageLabel.Size = new System.Drawing.Size(350, 23);
            this.addToTaleoMessageLabel.TabIndex = 3;
            this.addToTaleoMessageLabel.Text = "Please wait while a new log is being added. It may take a few minutes…";
            this.addToTaleoMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addToTaleoMessagePictureBox
            // 
            this.addToTaleoMessagePictureBox.Image = global::TaleoOutlookAddin.Properties.Resources.AddToTaleoAnimation;
            this.addToTaleoMessagePictureBox.Location = new System.Drawing.Point(7, 3);
            this.addToTaleoMessagePictureBox.Name = "addToTaleoMessagePictureBox";
            this.addToTaleoMessagePictureBox.Size = new System.Drawing.Size(54, 45);
            this.addToTaleoMessagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.addToTaleoMessagePictureBox.TabIndex = 2;
            this.addToTaleoMessagePictureBox.TabStop = false;
            // 
            // AddToTaleoMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 51);
            this.Controls.Add(this.addToTaleoMessageLabel);
            this.Controls.Add(this.addToTaleoMessagePictureBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 90);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 90);
            this.Name = "AddToTaleoMessageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo Outlook Toolbar";
            ((System.ComponentModel.ISupportInitialize)(this.addToTaleoMessagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label addToTaleoMessageLabel;
        private System.Windows.Forms.PictureBox addToTaleoMessagePictureBox;
    }
}