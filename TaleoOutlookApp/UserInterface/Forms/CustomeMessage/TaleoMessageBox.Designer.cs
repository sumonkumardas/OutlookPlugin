namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    partial class TaleoMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaleoMessageForm));
            this.picStatusIcon = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picStatusIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picStatusIcon
            // 
            this.picStatusIcon.Image = global::TaleoOutlookAddin.Properties.Resources.error2;
            this.picStatusIcon.Location = new System.Drawing.Point(9, 28);
            this.picStatusIcon.Name = "picStatusIcon";
            this.picStatusIcon.Size = new System.Drawing.Size(48, 48);
            this.picStatusIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatusIcon.TabIndex = 11;
            this.picStatusIcon.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(63, 16);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(380, 68);
            this.lblDescription.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(358, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // TaleoMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 122);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.picStatusIcon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 206);
            this.MinimumSize = new System.Drawing.Size(5, 5);
            this.Name = "TaleoMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo Message";
            ((System.ComponentModel.ISupportInitialize)(this.picStatusIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picStatusIcon;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnOK;
    }
}