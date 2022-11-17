using System;
using System.Windows.Forms;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin.Forms.CheckForUpdate
{
    public partial class CheckForUpdate : Form
    {
        public CheckForUpdate()
        {
            InitializeComponent();
            webBrowser.Navigate("https://www.google.com","_self", null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));

        }

        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
