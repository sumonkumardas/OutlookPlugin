using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    public partial class AuthenticationMessageForm : Form
    {
        public AuthenticationMessageForm()
        {
            InitializeComponent();
            ControlBox = false;
        }

        public AuthenticationMessageForm(string labelText)
        {
            InitializeComponent();
            label1.Text = labelText;
            ControlBox = false;
        }
    }
}
