using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaleoOutlookAddin.TaleoWebControl.Common
{
    public partial class CtrlTaleoMyView : UserControl
    {
		private CtrlTaleoMyView ctrlTaleoMyView;

		public CtrlTaleoMyView()
        {
            InitializeComponent();
        }

		public CtrlTaleoMyView(CtrlTaleoMyView ctrlTaleoMyView)
		{
			this.ctrlTaleoMyView = ctrlTaleoMyView;
			this.ctrlTaleoMyView.webBrowser = ctrlTaleoMyView.webBrowser;
			this.webBrowser = ctrlTaleoMyView.webBrowser;
			InitializeComponent();
		}

		public void externalInit()
		{
			InitializeComponent();
		}
    }
}
