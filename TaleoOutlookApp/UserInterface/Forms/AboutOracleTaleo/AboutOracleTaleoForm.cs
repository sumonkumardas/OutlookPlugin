using System;
using System.Windows.Forms;

namespace TaleoOutlookAddin.Forms.AboutOracleTaleoForm
{
    public partial class AboutOracleTaleoForm : Form
    {
        public AboutOracleTaleoForm()
        {
            InitializeComponent();
        }

        private void btnAboutUsOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
