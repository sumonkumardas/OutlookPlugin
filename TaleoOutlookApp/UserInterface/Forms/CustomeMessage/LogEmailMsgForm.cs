using System;
using System.Windows.Forms;

namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    public partial class LogEmailMsgForm : Form
    {
        public LogEmailMsgForm()
        {
            InitializeComponent();
        }

        private void LogEmailMsgForm_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }
    }
}
