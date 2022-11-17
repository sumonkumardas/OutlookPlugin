using System;
using Model.Response;
using Util.Utilities;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin.TaleoWebControl.Common
{
	partial class CtrlTaleoMyView
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

		public void InitializeComponent()
		{
			if (this.webBrowser == null)
			{
				this.webBrowser = new System.Windows.Forms.WebBrowser();
				this.SuspendLayout();
				this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
				this.webBrowser.Location = new System.Drawing.Point(0, 0);
                this.webBrowser.MinimumSize = new System.Drawing.Size(200, 200);
				this.webBrowser.Name = "webBrowserMyView";
				this.webBrowser.Size = new System.Drawing.Size(800, 283);
				this.webBrowser.TabIndex = 0;
                this.webBrowser.AllowNavigation = true;

				this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
				this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
				this.Controls.Add(this.webBrowser);
				this.Name = "MyView";
				this.Size = new System.Drawing.Size(412, 283);
				this.webBrowser.Url = new System.Uri("about: nothing", System.UriKind.Absolute);
			}
			String currentURL = TaleoAddIn.addInService.GetTaleoSettingsValue("DASHBOARD").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
			currentURL = currentURL.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
			String loginToken = TaleoAddIn.sessionData.loginToken;

			loginToken = ((LoginTokenResponse)(TaleoAddIn.addInService.logInTokenSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken))).response.loginToken;
			currentURL = currentURL.Replace("<LOGIN_TOKEN>", loginToken);

			String cookieString = string.Empty;
			for (int i = 0; TaleoAddIn.sessionData.Cookies != null && i < TaleoAddIn.sessionData.Cookies.Count; i++)
				cookieString = cookieString + TaleoAddIn.sessionData.Cookies[i].ToString() + ";";

			this.ResumeLayout(true);
			this.ResumeLayout(false);
            string customheader = string.IsNullOrEmpty(cookieString) ? "" : string.Format("Cookie: {0}\r\n", cookieString);
            this.webBrowser.Navigate(currentURL, "_self", null, customheader + string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
			this.webBrowser.ResumeLayout(false);

			this.ResumeLayout(true);
		}
		public void SetNewUrl(string newUrl, bool runAgain, string jSessionID)
		{
			this.SuspendLayout();

			InitializeComponent();
			//            InitializeComponent();
			//			webBrowser.Document.Cookie = "JSESSIONID: " + jSessionID + ";";
            webBrowser.Navigate(newUrl, null, null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
			webBrowser.Refresh(System.Windows.Forms.WebBrowserRefreshOption.Completely);

			this.ResumeLayout(true);
			/*
						if (runAgain)
						{
							this.ResumeLayout(true);
							navigationURL = newUrl;
							webBrowserTBETVOnline.Document.Cookie = "JSESSIONID: " + jSessionID + ";";
							webBrowserTBETVOnline.Navigate(newUrl);
							this.ResumeLayout(true);
						}
			*/
		}
        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }

		#endregion

		public void SetNewUrlWithCookie(string newUrl, bool runAgain, string cookies)
		{
			this.ResumeLayout(true);
			//            InitializeComponent();
			webBrowser.Document.Cookie = cookies; //"JSESSIONID: " + jSessionID + ";";
            webBrowser.Navigate(webBrowser.Url, null, null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));

			this.ResumeLayout(true);

			if (runAgain)
			{
				this.ResumeLayout(true);
				webBrowser.Document.Cookie = cookies;
                webBrowser.Navigate(newUrl, null, null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
				this.ResumeLayout(true);
			}
		}
		public System.Windows.Forms.WebBrowser webBrowser;
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
	}
}
