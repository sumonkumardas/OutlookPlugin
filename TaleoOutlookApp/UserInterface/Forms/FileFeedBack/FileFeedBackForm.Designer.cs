namespace TaleoOutlookAddin.Forms.FileFeedBack
{
    partial class TaleoFileFeedBackForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaleoFileFeedBackForm));
            this.lbSeparatorLine = new System.Windows.Forms.Label();
            this.btnFileFeedBackNext = new System.Windows.Forms.Button();
            this.btnFileFeedBackBack = new System.Windows.Forms.Button();
            this.btnFIleFeedBackCancel = new System.Windows.Forms.Button();
            this.lblFileFeedBackNotifier = new System.Windows.Forms.Label();
            this.pbFileFeedBackBanner = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.fileFeedbackEmailBodyRTB = new System.Windows.Forms.RichTextBox();
            this.fileFeedBackStepsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFileFeedBackBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSeparatorLine
            // 
            this.lbSeparatorLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparatorLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeparatorLine.Location = new System.Drawing.Point(13, 386);
            this.lbSeparatorLine.Name = "lbSeparatorLine";
            this.lbSeparatorLine.Size = new System.Drawing.Size(546, 1);
            this.lbSeparatorLine.TabIndex = 6;
            // 
            // btnFileFeedBackNext
            // 
            this.btnFileFeedBackNext.Location = new System.Drawing.Point(482, 403);
            this.btnFileFeedBackNext.Name = "btnFileFeedBackNext";
            this.btnFileFeedBackNext.Size = new System.Drawing.Size(75, 23);
            this.btnFileFeedBackNext.TabIndex = 8;
            this.btnFileFeedBackNext.Text = "Next>";
            this.btnFileFeedBackNext.UseVisualStyleBackColor = true;
            this.btnFileFeedBackNext.Click += new System.EventHandler(this.btnFileFeedBackNext_Click);
            // 
            // btnFileFeedBackBack
            // 
            this.btnFileFeedBackBack.Location = new System.Drawing.Point(406, 403);
            this.btnFileFeedBackBack.Name = "btnFileFeedBackBack";
            this.btnFileFeedBackBack.Size = new System.Drawing.Size(75, 23);
            this.btnFileFeedBackBack.TabIndex = 7;
            this.btnFileFeedBackBack.Text = "<Back";
            this.btnFileFeedBackBack.UseVisualStyleBackColor = true;
            this.btnFileFeedBackBack.Click += new System.EventHandler(this.btnFileFeedBackBack_Click);
            // 
            // btnFIleFeedBackCancel
            // 
            this.btnFIleFeedBackCancel.Location = new System.Drawing.Point(321, 403);
            this.btnFIleFeedBackCancel.Name = "btnFIleFeedBackCancel";
            this.btnFIleFeedBackCancel.Size = new System.Drawing.Size(75, 23);
            this.btnFIleFeedBackCancel.TabIndex = 9;
            this.btnFIleFeedBackCancel.Text = "Cancel";
            this.btnFIleFeedBackCancel.UseVisualStyleBackColor = true;
            this.btnFIleFeedBackCancel.Click += new System.EventHandler(this.btnFIleFeedBackCancel_Click);
            // 
            // lblFileFeedBackNotifier
            // 
            this.lblFileFeedBackNotifier.AutoSize = true;
            this.lblFileFeedBackNotifier.Location = new System.Drawing.Point(13, 70);
            this.lblFileFeedBackNotifier.Name = "lblFileFeedBackNotifier";
            this.lblFileFeedBackNotifier.Size = new System.Drawing.Size(534, 13);
            this.lblFileFeedBackNotifier.TabIndex = 11;
            this.lblFileFeedBackNotifier.Text = "Please make sure feedback text is accurate, make necessary changes if needed, the" +
    "n click \"Next\" to continue.";
            // 
            // pbFileFeedBackBanner
            // 
            this.pbFileFeedBackBanner.BackColor = System.Drawing.Color.White;
            this.pbFileFeedBackBanner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFileFeedBackBanner.Image = global::TaleoOutlookAddin.Properties.Resources.file_feedbackdummy2;
            this.pbFileFeedBackBanner.Location = new System.Drawing.Point(-3, -2);
            this.pbFileFeedBackBanner.Name = "pbFileFeedBackBanner";
            this.pbFileFeedBackBanner.Size = new System.Drawing.Size(582, 65);
            this.pbFileFeedBackBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbFileFeedBackBanner.TabIndex = 10;
            this.pbFileFeedBackBanner.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(13, 86);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(552, 287);
            this.webBrowser.TabIndex = 12;
            this.webBrowser.Visible = false;
            // 
            // fileFeedbackEmailBodyRTB
            // 
            this.fileFeedbackEmailBodyRTB.Location = new System.Drawing.Point(11, 86);
            this.fileFeedbackEmailBodyRTB.Name = "fileFeedbackEmailBodyRTB";
            this.fileFeedbackEmailBodyRTB.Size = new System.Drawing.Size(552, 287);
            this.fileFeedbackEmailBodyRTB.TabIndex = 13;
            this.fileFeedbackEmailBodyRTB.Text = "";
            // 
            // fileFeedBackStepsLabel
            // 
            this.fileFeedBackStepsLabel.AutoSize = true;
            this.fileFeedBackStepsLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileFeedBackStepsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileFeedBackStepsLabel.Location = new System.Drawing.Point(65, 32);
            this.fileFeedBackStepsLabel.Name = "fileFeedBackStepsLabel";
            this.fileFeedBackStepsLabel.Size = new System.Drawing.Size(248, 13);
            this.fileFeedBackStepsLabel.TabIndex = 14;
            this.fileFeedBackStepsLabel.Text = "File feedback on an employee. Step 1 of 3.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "File Feedback";
            // 
            // TaleoFileFeedBackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(577, 436);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileFeedBackStepsLabel);
            this.Controls.Add(this.fileFeedbackEmailBodyRTB);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.lblFileFeedBackNotifier);
            this.Controls.Add(this.pbFileFeedBackBanner);
            this.Controls.Add(this.btnFIleFeedBackCancel);
            this.Controls.Add(this.btnFileFeedBackNext);
            this.Controls.Add(this.btnFileFeedBackBack);
            this.Controls.Add(this.lbSeparatorLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaleoFileFeedBackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo - File Feedback";
            ((System.ComponentModel.ISupportInitialize)(this.pbFileFeedBackBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSeparatorLine;
        private System.Windows.Forms.Button btnFileFeedBackNext;
        private System.Windows.Forms.Button btnFileFeedBackBack;
        private System.Windows.Forms.Button btnFIleFeedBackCancel;
        private System.Windows.Forms.PictureBox pbFileFeedBackBanner;
        private System.Windows.Forms.Label lblFileFeedBackNotifier;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.RichTextBox fileFeedbackEmailBodyRTB;
        private System.Windows.Forms.Label fileFeedBackStepsLabel;
        private System.Windows.Forms.Label label1;
    }
}