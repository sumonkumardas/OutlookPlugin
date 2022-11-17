using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    public partial class AddToTaleoMessageForm : Form
    {
        public AddToTaleoMessageForm()
        {
            InitializeComponent();
            InitializeInfo();
        }

        public AddToTaleoMessageForm(string waitingMessage)
        {
            InitializeComponent();
            addToTaleoMessageLabel.Text = waitingMessage;
        }

        private void InitializeInfo()
        {
            ControlBox = false;
            addToTaleoMessageLabel.Text = GetResourceValueByName("AddComment_logMessage");
        }
        private static string GetResourceValueByName(string resourceName)
        {
            return TaleoFormHelper.TaleoFormHelper.GetResourceValueByName(resourceName);
        }
    }
}
