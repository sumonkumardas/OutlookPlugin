namespace TaleoOutlookAddin
{
    partial class EmailWindowRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public EmailWindowRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
        private void InitializeComponent()
        {
            this.TabNewMailMessage = this.Factory.CreateRibbonTab();
            this.taleoGroup = this.Factory.CreateRibbonGroup();
            this.addressBookButton = this.Factory.CreateRibbonButton();
            this.logEmailToggleButton = this.Factory.CreateRibbonToggleButton();
            this.TabNewMailMessage.SuspendLayout();
            this.taleoGroup.SuspendLayout();
            // 
            // TabNewMailMessage
            // 
            this.TabNewMailMessage.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabNewMailMessage.ControlId.OfficeId = "TabNewMailMessage";
            this.TabNewMailMessage.Groups.Add(this.taleoGroup);
            this.TabNewMailMessage.Label = "TabNewMailMessage";
            this.TabNewMailMessage.Name = "TabNewMailMessage";
            // 
            // taleoGroup
            // 
            this.taleoGroup.Items.Add(this.addressBookButton);
            this.taleoGroup.Items.Add(this.logEmailToggleButton);
            this.taleoGroup.Label = "Taleo";
            this.taleoGroup.Name = "taleoGroup";
            // 
            // addressBookButton
            // 
            this.addressBookButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.addressBookButton.Image = global::TaleoOutlookAddin.Properties.Resources.taleo;
            this.addressBookButton.Label = "Address Book";
            this.addressBookButton.Name = "addressBookButton";
            this.addressBookButton.ShowImage = true;
            this.addressBookButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.addressBookButton_Click);
            // 
            // logEmailToggleButton
            // 
            this.logEmailToggleButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.logEmailToggleButton.Image = global::TaleoOutlookAddin.Properties.Resources.server_doc_part1;
            this.logEmailToggleButton.Label = "Log Email";
            this.logEmailToggleButton.Name = "logEmailToggleButton";
            this.logEmailToggleButton.ShowImage = true;
            this.logEmailToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.logEmailToggleButton_Click);
            // 
            // EmailWindowRibbon
            // 
            this.Name = "EmailWindowRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose";
            this.Tabs.Add(this.TabNewMailMessage);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.EmailWindowRibbon_Load);
            this.TabNewMailMessage.ResumeLayout(false);
            this.TabNewMailMessage.PerformLayout();
            this.taleoGroup.ResumeLayout(false);
            this.taleoGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabNewMailMessage;
        public Microsoft.Office.Tools.Ribbon.RibbonGroup taleoGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addressBookButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton logEmailToggleButton;
    }

    partial class ThisRibbonCollection
    {
        internal EmailWindowRibbon EmailWindowRibbon
        {
            get { return this.GetRibbon<EmailWindowRibbon>(); }
        }
    }
}
