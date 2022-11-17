using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Utilities;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin
{
    public partial class TaskPaneControl : UserControl
    {
        private string p;
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();

        public TaskPaneControl()
        {
            InitializeComponent();
        }

        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }

        public TaskPaneControl(string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            this.webBrowser.Navigate(this.p, null, null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
            this.ResumeLayout(true);
        }
    }
}
