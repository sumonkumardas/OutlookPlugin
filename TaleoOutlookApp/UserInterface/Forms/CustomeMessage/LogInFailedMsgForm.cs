using System;
using System.Windows.Forms;

namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    public partial class LogInFailedMsgForm : Form
    {
        public LogInFailedMsgForm()
        {
            InitializeComponent();
        }

        private void btnLogInfailedMsgOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
