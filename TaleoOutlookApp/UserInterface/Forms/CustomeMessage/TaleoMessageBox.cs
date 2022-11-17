using System;
using System.Windows.Forms;
using Util.Enums;

namespace TaleoOutlookAddin.Forms.CustomeMessage
{
    public partial class TaleoMessageForm : Form
    {
        public TaleoMessageForm()
        {
            InitializeComponent();
        }

        public TaleoMessageForm(string description, string title = "", TaleoMsgBoxIcon icon = TaleoMsgBoxIcon.Information, bool showPictureBox = true)
        {
            InitializeComponent();
            this.Text = title;
            lblDescription.Text = description;
            if (icon == TaleoMsgBoxIcon.Warning)
                picStatusIcon.Image = Properties.Resources.alert;

            else if (icon == TaleoMsgBoxIcon.Error)
                picStatusIcon.Image = Properties.Resources.error2;

            else if (icon == TaleoMsgBoxIcon.Information)
                picStatusIcon.Image = Properties.Resources.info;

            else
                picStatusIcon.Image = null;

            //if (showPictureBox == false)
            //    pictureBox1.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public static class TaleoMessageBox
    {
        public static void Show(string description, string title = "Taleo Outlook Toolbar", TaleoMsgBoxIcon icon = TaleoMsgBoxIcon.Information, bool showPictureBox = true)
        {
            // using construct ensures the resources are freed when form is closed
            using (var form = new TaleoMessageForm(description, title, icon, showPictureBox))
            {
                form.ShowDialog();
            }
        } 
    }
}