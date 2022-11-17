namespace TaleoOutlookAddin
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
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
            this.taleoRibbonTab = this.Factory.CreateRibbonTab();
            this.taleoGroup = this.Factory.CreateRibbonGroup();
            this.feedbackGroup = this.Factory.CreateRibbonGroup();
            this.addGroup = this.Factory.CreateRibbonGroup();
            this.optionsGroup = this.Factory.CreateRibbonGroup();
            this.taleoButton = this.Factory.CreateRibbonButton();
            this.dashboardButton = this.Factory.CreateRibbonButton();
            this.taleoLoginButton = this.Factory.CreateRibbonButton();
            this.requestFeedbackButton = this.Factory.CreateRibbonButton();
            this.fileFeedbackButton = this.Factory.CreateRibbonButton();
            this.addToTaleoButton = this.Factory.CreateRibbonButton();
            this.addCommentsButton = this.Factory.CreateRibbonButton();
            this.preferencesButton = this.Factory.CreateRibbonButton();
            this.checkForUpdatesButton = this.Factory.CreateRibbonButton();
            this.aboutButton = this.Factory.CreateRibbonButton();
            this.taleoRibbonTab.SuspendLayout();
            this.taleoGroup.SuspendLayout();
            this.feedbackGroup.SuspendLayout();
            this.addGroup.SuspendLayout();
            this.optionsGroup.SuspendLayout();
            // 
            // taleoRibbonTab
            // 
            this.taleoRibbonTab.Groups.Add(this.taleoGroup);
            this.taleoRibbonTab.Groups.Add(this.feedbackGroup);
            this.taleoRibbonTab.Groups.Add(this.addGroup);
            this.taleoRibbonTab.Groups.Add(this.optionsGroup);
            this.taleoRibbonTab.Label = "Taleo";
            this.taleoRibbonTab.Name = "taleoRibbonTab";
            // 
            // taleoGroup
            // 
            this.taleoGroup.Items.Add(this.taleoButton);
            this.taleoGroup.Items.Add(this.dashboardButton);
            this.taleoGroup.Items.Add(this.taleoLoginButton);
            this.taleoGroup.Label = "Taleo";
            this.taleoGroup.Name = "taleoGroup";
            // 
            // feedbackGroup
            // 
            this.feedbackGroup.Items.Add(this.requestFeedbackButton);
            this.feedbackGroup.Items.Add(this.fileFeedbackButton);
            this.feedbackGroup.Label = "Feedback";
            this.feedbackGroup.Name = "feedbackGroup";
            // 
            // addGroup
            // 
            this.addGroup.Items.Add(this.addToTaleoButton);
            this.addGroup.Items.Add(this.addCommentsButton);
            this.addGroup.Label = "Add";
            this.addGroup.Name = "addGroup";
            // 
            // optionsGroup
            // 
            this.optionsGroup.Items.Add(this.preferencesButton);
            this.optionsGroup.Items.Add(this.checkForUpdatesButton);
            this.optionsGroup.Items.Add(this.aboutButton);
            this.optionsGroup.Label = "Options";
            this.optionsGroup.Name = "optionsGroup";
            // 
            // taleoButton
            // 
            this.taleoButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.taleoButton.Image = global::TaleoOutlookAddin.Properties.Resources.taleo_new;
            this.taleoButton.Label = "Taleo";
            this.taleoButton.Name = "taleoButton";
            this.taleoButton.ShowImage = true;
            this.taleoButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.taleoButton_Click);
            // 
            // dashboardButton
            // 
            this.dashboardButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.dashboardButton.Image = global::TaleoOutlookAddin.Properties.Resources.dashboard_new;
            this.dashboardButton.Label = "Dashboard";
            this.dashboardButton.Name = "dashboardButton";
            this.dashboardButton.ShowImage = true;
            this.dashboardButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dashboardButton_Click);
            // 
            // taleoLoginButton
            // 
            this.taleoLoginButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.taleoLoginButton.Image = global::TaleoOutlookAddin.Properties.Resources.taleo_new;
            this.taleoLoginButton.Label = "Taleo Login";
            this.taleoLoginButton.Name = "taleoLoginButton";
            this.taleoLoginButton.ShowImage = true;
            this.taleoLoginButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.taleoLoginButton_Click);
            // 
            // requestFeedbackButton
            // 
            this.requestFeedbackButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.requestFeedbackButton.Image = global::TaleoOutlookAddin.Properties.Resources.request_feedback_new;
            this.requestFeedbackButton.Label = "Request Feedback";
            this.requestFeedbackButton.Name = "requestFeedbackButton";
            this.requestFeedbackButton.ShowImage = true;
            this.requestFeedbackButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.requestFeedbackButton_Click);
            // 
            // fileFeedbackButton
            // 
            this.fileFeedbackButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.fileFeedbackButton.Image = global::TaleoOutlookAddin.Properties.Resources.file_feedback_new;
            this.fileFeedbackButton.Label = "File Feedback";
            this.fileFeedbackButton.Name = "fileFeedbackButton";
            this.fileFeedbackButton.ShowImage = true;
            this.fileFeedbackButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.fileFeedbackButton_Click);
            // 
            // addToTaleoButton
            // 
            this.addToTaleoButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.addToTaleoButton.Image = global::TaleoOutlookAddin.Properties.Resources.add_to_taleo_new;
            this.addToTaleoButton.Label = "Add to Taleo";
            this.addToTaleoButton.Name = "addToTaleoButton";
            this.addToTaleoButton.ShowImage = true;
            this.addToTaleoButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.addToTaleoButton_Click);
            // 
            // addCommentsButton
            // 
            this.addCommentsButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.addCommentsButton.Image = global::TaleoOutlookAddin.Properties.Resources.add_comment_new;
            this.addCommentsButton.Label = "Add Comments";
            this.addCommentsButton.Name = "addCommentsButton";
            this.addCommentsButton.ShowImage = true;
            this.addCommentsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.addCommentsButton_Click);
            // 
            // preferencesButton
            // 
            this.preferencesButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.preferencesButton.Image = global::TaleoOutlookAddin.Properties.Resources.PreferenceRB;
            this.preferencesButton.Label = "Preferences";
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.ShowImage = true;
            this.preferencesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.preferencesButton_Click);
            // 
            // checkForUpdatesButton
            // 
            this.checkForUpdatesButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.checkForUpdatesButton.Image = global::TaleoOutlookAddin.Properties.Resources.updates_new;
            this.checkForUpdatesButton.Label = "Check for Updates";
            this.checkForUpdatesButton.Name = "checkForUpdatesButton";
            this.checkForUpdatesButton.ShowImage = true;
            this.checkForUpdatesButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkForUpdatesButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.aboutButton.Image = global::TaleoOutlookAddin.Properties.Resources.info_round;
            this.aboutButton.Label = "About";
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.ShowImage = true;
            this.aboutButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.aboutButton_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.taleoRibbonTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.TaleoRibbon_Load);
            this.taleoRibbonTab.ResumeLayout(false);
            this.taleoRibbonTab.PerformLayout();
            this.taleoGroup.ResumeLayout(false);
            this.taleoGroup.PerformLayout();
            this.feedbackGroup.ResumeLayout(false);
            this.feedbackGroup.PerformLayout();
            this.addGroup.ResumeLayout(false);
            this.addGroup.PerformLayout();
            this.optionsGroup.ResumeLayout(false);
            this.optionsGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab taleoRibbonTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup taleoGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton taleoButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton dashboardButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton taleoLoginButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup feedbackGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton requestFeedbackButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton fileFeedbackButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup addGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addToTaleoButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton addCommentsButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup optionsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton preferencesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton checkForUpdatesButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton aboutButton;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon TaleoRibbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
