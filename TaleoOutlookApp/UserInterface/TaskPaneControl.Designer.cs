﻿using System;
namespace TaleoOutlookAddin
{
    partial class TaskPaneControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        /// 

      //  public static String navigationURL = "http://www.tbetvonline.com/index.php";

        private void InitializeComponent()
        {
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.None;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowserTBETVOnline";
            this.webBrowser.Size = new System.Drawing.Size(412, 283);
            this.webBrowser.TabIndex = 0;
            webBrowser.AllowNavigation = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser);
            this.Name = "TaskPaneControl";
            this.Size = new System.Drawing.Size(412, 283);
            this.ResumeLayout(true);

        }
//        public void SetNewUrl(string newUrl, bool runAgain, string jSessionID) {
//            this.SuspendLayout();
//            navigationURL = newUrl;
////            InitializeComponent();
//            webBrowser.Document.Cookie = "JSESSIONID: " + jSessionID + ";";
//            webBrowser.Navigate(newUrl);
//            webBrowser.Refresh(System.Windows.Forms.WebBrowserRefreshOption.Completely);

//            this.ResumeLayout(true);
///*
//            if (runAgain)
//            {
//                this.ResumeLayout(true);
//                navigationURL = newUrl;
//                webBrowserTBETVOnline.Document.Cookie = "JSESSIONID: " + jSessionID + ";";
//                webBrowserTBETVOnline.Navigate(newUrl);
//                this.ResumeLayout(true);
//            }
//*/        }

//        public void SetNewUrlWithCookie(string newUrl, bool runAgain, string cookies)
//        {
//            this.ResumeLayout(true);
//            navigationURL = newUrl;
//            //            InitializeComponent();
//            webBrowser.Document.Cookie = cookies; //"JSESSIONID: " + jSessionID + ";";
//            webBrowser.Navigate(newUrl);

//            this.ResumeLayout(true);

//            if (runAgain)
//            {
//                this.ResumeLayout(true);
//                navigationURL = newUrl;
//                webBrowser.Document.Cookie = cookies;
//                webBrowser.Navigate(newUrl);
//                this.ResumeLayout(true);
//            }
//        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
    }
}