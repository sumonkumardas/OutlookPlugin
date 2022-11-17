namespace TaleoOutlookAddin.Forms.AddToTaleo
{
    partial class AddToTaleo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddToTaleo));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.addtoTaleoStepsLabel = new System.Windows.Forms.Label();
            this.step1 = new System.Windows.Forms.Panel();
            this.step2 = new System.Windows.Forms.Panel();
            this.step3 = new System.Windows.Forms.Panel();
            this.step4 = new System.Windows.Forms.Panel();
            this.referral = new System.Windows.Forms.Panel();
            this.referNameTextBox = new System.Windows.Forms.TextBox();
            this.referralNameLabel = new System.Windows.Forms.Label();
            this.referralInstruction = new System.Windows.Forms.Label();
            this.referralLine = new System.Windows.Forms.Label();
            this.referralNextButton = new System.Windows.Forms.Button();
            this.referralBackButton = new System.Windows.Forms.Button();
            this.referralCencelButton = new System.Windows.Forms.Button();
            this.step4BackButton = new System.Windows.Forms.Button();
            this.step4WebBrowser = new System.Windows.Forms.WebBrowser();
            this.Step4CencelButton = new System.Windows.Forms.Button();
            this.step4FinishButton = new System.Windows.Forms.Button();
            this.step4Line = new System.Windows.Forms.Label();
            this.step4Instruction = new System.Windows.Forms.Label();
            this.step3Browser = new System.Windows.Forms.WebBrowser();
            this.step3CencelButton = new System.Windows.Forms.Button();
            this.step3BackButton = new System.Windows.Forms.Button();
            this.step3AddButton = new System.Windows.Forms.Button();
            this.step3FinishButton = new System.Windows.Forms.Button();
            this.step3Instruction = new System.Windows.Forms.Label();
            this.step3Line = new System.Windows.Forms.Label();
            this.parseButton = new System.Windows.Forms.Button();
            this.step2BackButton = new System.Windows.Forms.Button();
            this.step2CencelButton = new System.Windows.Forms.Button();
            this.step2line = new System.Windows.Forms.Label();
            this.emailBodyTextbox = new System.Windows.Forms.RichTextBox();
            this.step2Instruction = new System.Windows.Forms.Label();
            this.AddAllAttachmentCheckBox = new System.Windows.Forms.CheckBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.cencelButton = new System.Windows.Forms.Button();
            this.line = new System.Windows.Forms.Label();
            this.emailFrom = new System.Windows.Forms.Label();
            this.Instruction = new System.Windows.Forms.Label();
            this.candidateTypeComboBox = new System.Windows.Forms.ComboBox();
            this.emailDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.subjectTextbox = new System.Windows.Forms.TextBox();
            this.receivedOnTextBox = new System.Windows.Forms.TextBox();
            this.senderTextBox = new System.Windows.Forms.TextBox();
            this.subject = new System.Windows.Forms.Label();
            this.receivedOn = new System.Windows.Forms.Label();
            this.sender = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.step1.SuspendLayout();
            this.step2.SuspendLayout();
            this.step3.SuspendLayout();
            this.step4.SuspendLayout();
            this.referral.SuspendLayout();
            this.emailDetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackgroundImage = global::TaleoOutlookAddin.Properties.Resources.addtotaleo;
            this.headerPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.headerPanel.Controls.Add(this.label1);
            this.headerPanel.Controls.Add(this.addtoTaleoStepsLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(563, 49);
            this.headerPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Add to Taleo";
            // 
            // addtoTaleoStepsLabel
            // 
            this.addtoTaleoStepsLabel.AutoSize = true;
            this.addtoTaleoStepsLabel.BackColor = System.Drawing.Color.Transparent;
            this.addtoTaleoStepsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addtoTaleoStepsLabel.Location = new System.Drawing.Point(59, 29);
            this.addtoTaleoStepsLabel.Name = "addtoTaleoStepsLabel";
            this.addtoTaleoStepsLabel.Size = new System.Drawing.Size(428, 13);
            this.addtoTaleoStepsLabel.TabIndex = 17;
            this.addtoTaleoStepsLabel.Text = "Use selected email to manage candidates or contacts in taleo. Step 1 of 4";
            // 
            // step1
            // 
            this.step1.Controls.Add(this.step2);
            this.step1.Controls.Add(this.nextButton);
            this.step1.Controls.Add(this.backButton);
            this.step1.Controls.Add(this.cencelButton);
            this.step1.Controls.Add(this.line);
            this.step1.Controls.Add(this.emailFrom);
            this.step1.Controls.Add(this.Instruction);
            this.step1.Controls.Add(this.candidateTypeComboBox);
            this.step1.Controls.Add(this.emailDetailsGroupBox);
            this.step1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step1.Location = new System.Drawing.Point(0, 49);
            this.step1.Name = "step1";
            this.step1.Size = new System.Drawing.Size(563, 431);
            this.step1.TabIndex = 2;
            // 
            // step2
            // 
            this.step2.Controls.Add(this.step3);
            this.step2.Controls.Add(this.parseButton);
            this.step2.Controls.Add(this.step2BackButton);
            this.step2.Controls.Add(this.step2CencelButton);
            this.step2.Controls.Add(this.step2line);
            this.step2.Controls.Add(this.emailBodyTextbox);
            this.step2.Controls.Add(this.step2Instruction);
            this.step2.Controls.Add(this.AddAllAttachmentCheckBox);
            this.step2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step2.Location = new System.Drawing.Point(0, 0);
            this.step2.Name = "step2";
            this.step2.Size = new System.Drawing.Size(563, 431);
            this.step2.TabIndex = 8;
            this.step2.Visible = false;
            // 
            // step3
            // 
            this.step3.Controls.Add(this.step4);
            this.step3.Controls.Add(this.step3Browser);
            this.step3.Controls.Add(this.step3CencelButton);
            this.step3.Controls.Add(this.step3BackButton);
            this.step3.Controls.Add(this.step3AddButton);
            this.step3.Controls.Add(this.step3FinishButton);
            this.step3.Controls.Add(this.step3Instruction);
            this.step3.Controls.Add(this.step3Line);
            this.step3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step3.Location = new System.Drawing.Point(0, 0);
            this.step3.Name = "step3";
            this.step3.Size = new System.Drawing.Size(563, 431);
            this.step3.TabIndex = 11;
            this.step3.Visible = false;
            // 
            // step4
            // 
            this.step4.Controls.Add(this.referral);
            this.step4.Controls.Add(this.step4BackButton);
            this.step4.Controls.Add(this.step4WebBrowser);
            this.step4.Controls.Add(this.Step4CencelButton);
            this.step4.Controls.Add(this.step4FinishButton);
            this.step4.Controls.Add(this.step4Line);
            this.step4.Controls.Add(this.step4Instruction);
            this.step4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step4.Location = new System.Drawing.Point(0, 0);
            this.step4.Name = "step4";
            this.step4.Size = new System.Drawing.Size(563, 431);
            this.step4.TabIndex = 15;
            this.step4.Visible = false;
            // 
            // referral
            // 
            this.referral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.referral.Controls.Add(this.referNameTextBox);
            this.referral.Controls.Add(this.referralNameLabel);
            this.referral.Controls.Add(this.referralInstruction);
            this.referral.Controls.Add(this.referralLine);
            this.referral.Controls.Add(this.referralNextButton);
            this.referral.Controls.Add(this.referralBackButton);
            this.referral.Controls.Add(this.referralCencelButton);
            this.referral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.referral.Location = new System.Drawing.Point(0, 0);
            this.referral.Name = "referral";
            this.referral.Size = new System.Drawing.Size(563, 431);
            this.referral.TabIndex = 15;
            this.referral.Visible = false;
            // 
            // referNameTextBox
            // 
            this.referNameTextBox.Location = new System.Drawing.Point(29, 103);
            this.referNameTextBox.Name = "referNameTextBox";
            this.referNameTextBox.Size = new System.Drawing.Size(457, 20);
            this.referNameTextBox.TabIndex = 20;
            this.referNameTextBox.TextChanged += new System.EventHandler(this.referNameTextBox_TextChanged);
            // 
            // referralNameLabel
            // 
            this.referralNameLabel.AutoSize = true;
            this.referralNameLabel.Location = new System.Drawing.Point(26, 87);
            this.referralNameLabel.Name = "referralNameLabel";
            this.referralNameLabel.Size = new System.Drawing.Size(76, 13);
            this.referralNameLabel.TabIndex = 19;
            this.referralNameLabel.Text = "Referral name:";
            // 
            // referralInstruction
            // 
            this.referralInstruction.AutoSize = true;
            this.referralInstruction.Location = new System.Drawing.Point(17, 27);
            this.referralInstruction.Name = "referralInstruction";
            this.referralInstruction.Size = new System.Drawing.Size(458, 13);
            this.referralInstruction.TabIndex = 18;
            this.referralInstruction.Text = "Please enter a new candidate referral’s full name (i.e. John Smith), then click “" +
    "Next” to continue.";
            // 
            // referralLine
            // 
            this.referralLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.referralLine.Location = new System.Drawing.Point(12, 389);
            this.referralLine.Name = "referralLine";
            this.referralLine.Size = new System.Drawing.Size(539, 2);
            this.referralLine.TabIndex = 17;
            // 
            // referralNextButton
            // 
            this.referralNextButton.Location = new System.Drawing.Point(476, 396);
            this.referralNextButton.Name = "referralNextButton";
            this.referralNextButton.Size = new System.Drawing.Size(75, 23);
            this.referralNextButton.TabIndex = 16;
            this.referralNextButton.Text = "Next >";
            this.referralNextButton.UseVisualStyleBackColor = true;
            this.referralNextButton.Click += new System.EventHandler(this.referralNextButton_Click);
            // 
            // referralBackButton
            // 
            this.referralBackButton.Location = new System.Drawing.Point(386, 396);
            this.referralBackButton.Name = "referralBackButton";
            this.referralBackButton.Size = new System.Drawing.Size(75, 23);
            this.referralBackButton.TabIndex = 15;
            this.referralBackButton.Text = "< Back";
            this.referralBackButton.UseVisualStyleBackColor = true;
            this.referralBackButton.Click += new System.EventHandler(this.referralBackButton_Click);
            // 
            // referralCencelButton
            // 
            this.referralCencelButton.Location = new System.Drawing.Point(296, 396);
            this.referralCencelButton.Name = "referralCencelButton";
            this.referralCencelButton.Size = new System.Drawing.Size(75, 23);
            this.referralCencelButton.TabIndex = 14;
            this.referralCencelButton.Text = "Cancel";
            this.referralCencelButton.UseVisualStyleBackColor = true;
            this.referralCencelButton.Click += new System.EventHandler(this.referralCencelButton_Click);
            // 
            // step4BackButton
            // 
            this.step4BackButton.Location = new System.Drawing.Point(386, 396);
            this.step4BackButton.Name = "step4BackButton";
            this.step4BackButton.Size = new System.Drawing.Size(75, 23);
            this.step4BackButton.TabIndex = 14;
            this.step4BackButton.Text = "Back";
            this.step4BackButton.UseVisualStyleBackColor = true;
            // 
            // step4WebBrowser
            // 
            this.step4WebBrowser.Location = new System.Drawing.Point(12, 43);
            this.step4WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.step4WebBrowser.Name = "step4WebBrowser";
            this.step4WebBrowser.ScriptErrorsSuppressed = true;
            this.step4WebBrowser.Size = new System.Drawing.Size(539, 336);
            this.step4WebBrowser.TabIndex = 21;
            // 
            // Step4CencelButton
            // 
            this.Step4CencelButton.Location = new System.Drawing.Point(296, 396);
            this.Step4CencelButton.Name = "Step4CencelButton";
            this.Step4CencelButton.Size = new System.Drawing.Size(75, 23);
            this.Step4CencelButton.TabIndex = 13;
            this.Step4CencelButton.Text = "Cancel";
            this.Step4CencelButton.UseVisualStyleBackColor = true;
            // 
            // step4FinishButton
            // 
            this.step4FinishButton.Location = new System.Drawing.Point(476, 396);
            this.step4FinishButton.Name = "step4FinishButton";
            this.step4FinishButton.Size = new System.Drawing.Size(75, 23);
            this.step4FinishButton.TabIndex = 12;
            this.step4FinishButton.Text = "Finish";
            this.step4FinishButton.UseVisualStyleBackColor = true;
            this.step4FinishButton.Click += new System.EventHandler(this.step4FinishButton_Click);
            // 
            // step4Line
            // 
            this.step4Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.step4Line.Location = new System.Drawing.Point(12, 391);
            this.step4Line.Name = "step4Line";
            this.step4Line.Size = new System.Drawing.Size(539, 2);
            this.step4Line.TabIndex = 10;
            // 
            // step4Instruction
            // 
            this.step4Instruction.AutoSize = true;
            this.step4Instruction.Location = new System.Drawing.Point(12, 7);
            this.step4Instruction.Name = "step4Instruction";
            this.step4Instruction.Size = new System.Drawing.Size(403, 13);
            this.step4Instruction.TabIndex = 0;
            this.step4Instruction.Text = "A new candidate was successfully added to Taleo. Click “Finish” to close the dial" +
    "og.";
            // 
            // step3Browser
            // 
            this.step3Browser.Location = new System.Drawing.Point(12, 27);
            this.step3Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.step3Browser.Name = "step3Browser";
            this.step3Browser.Size = new System.Drawing.Size(539, 352);
            this.step3Browser.TabIndex = 14;
            // 
            // step3CencelButton
            // 
            this.step3CencelButton.Location = new System.Drawing.Point(296, 396);
            this.step3CencelButton.Name = "step3CencelButton";
            this.step3CencelButton.Size = new System.Drawing.Size(75, 23);
            this.step3CencelButton.TabIndex = 13;
            this.step3CencelButton.Text = "Cancel";
            this.step3CencelButton.UseVisualStyleBackColor = true;
            this.step3CencelButton.Click += new System.EventHandler(this.step3CencelButton_Click);
            // 
            // step3BackButton
            // 
            this.step3BackButton.Location = new System.Drawing.Point(386, 396);
            this.step3BackButton.Name = "step3BackButton";
            this.step3BackButton.Size = new System.Drawing.Size(75, 23);
            this.step3BackButton.TabIndex = 12;
            this.step3BackButton.Text = "< Back";
            this.step3BackButton.UseVisualStyleBackColor = true;
            this.step3BackButton.Click += new System.EventHandler(this.step3BackButton_Click);
            // 
            // step3AddButton
            // 
            this.step3AddButton.Location = new System.Drawing.Point(476, 396);
            this.step3AddButton.Name = "step3AddButton";
            this.step3AddButton.Size = new System.Drawing.Size(75, 23);
            this.step3AddButton.TabIndex = 11;
            this.step3AddButton.Text = "Add >";
            this.step3AddButton.UseVisualStyleBackColor = true;
            this.step3AddButton.Click += new System.EventHandler(this.step3AddButton_Click);
            // 
            // step3FinishButton
            // 
            this.step3FinishButton.Location = new System.Drawing.Point(476, 396);
            this.step3FinishButton.Name = "step3FinishButton";
            this.step3FinishButton.Size = new System.Drawing.Size(75, 23);
            this.step3FinishButton.TabIndex = 11;
            this.step3FinishButton.Text = "Finish";
            this.step3FinishButton.UseVisualStyleBackColor = true;
            this.step3FinishButton.Visible = false;
            this.step3FinishButton.Click += new System.EventHandler(this.step3FinishButton_Click);
            // 
            // step3Instruction
            // 
            this.step3Instruction.AutoSize = true;
            this.step3Instruction.Location = new System.Drawing.Point(13, 7);
            this.step3Instruction.Name = "step3Instruction";
            this.step3Instruction.Size = new System.Drawing.Size(0, 13);
            this.step3Instruction.TabIndex = 0;
            // 
            // step3Line
            // 
            this.step3Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.step3Line.Location = new System.Drawing.Point(12, 391);
            this.step3Line.Name = "step3Line";
            this.step3Line.Size = new System.Drawing.Size(539, 2);
            this.step3Line.TabIndex = 9;
            // 
            // parseButton
            // 
            this.parseButton.Location = new System.Drawing.Point(476, 396);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 10;
            this.parseButton.Text = "Parse >";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // step2BackButton
            // 
            this.step2BackButton.Location = new System.Drawing.Point(386, 396);
            this.step2BackButton.Name = "step2BackButton";
            this.step2BackButton.Size = new System.Drawing.Size(75, 23);
            this.step2BackButton.TabIndex = 9;
            this.step2BackButton.Text = "< Back";
            this.step2BackButton.UseVisualStyleBackColor = true;
            this.step2BackButton.Click += new System.EventHandler(this.step2BackButton_Click);
            // 
            // step2CencelButton
            // 
            this.step2CencelButton.Location = new System.Drawing.Point(296, 396);
            this.step2CencelButton.Name = "step2CencelButton";
            this.step2CencelButton.Size = new System.Drawing.Size(75, 23);
            this.step2CencelButton.TabIndex = 8;
            this.step2CencelButton.Text = "Cancel";
            this.step2CencelButton.UseVisualStyleBackColor = true;
            this.step2CencelButton.Click += new System.EventHandler(this.step2CencelButton_Click);
            // 
            // step2line
            // 
            this.step2line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.step2line.Location = new System.Drawing.Point(12, 391);
            this.step2line.Name = "step2line";
            this.step2line.Size = new System.Drawing.Size(539, 2);
            this.step2line.TabIndex = 5;
            // 
            // emailBodyTextbox
            // 
            this.emailBodyTextbox.Location = new System.Drawing.Point(12, 66);
            this.emailBodyTextbox.Name = "emailBodyTextbox";
            this.emailBodyTextbox.Size = new System.Drawing.Size(539, 313);
            this.emailBodyTextbox.TabIndex = 1;
            this.emailBodyTextbox.Text = "";
            // 
            // step2Instruction
            // 
            this.step2Instruction.AutoSize = true;
            this.step2Instruction.Location = new System.Drawing.Point(17, 15);
            this.step2Instruction.MaximumSize = new System.Drawing.Size(550, 50);
            this.step2Instruction.Name = "step2Instruction";
            this.step2Instruction.Size = new System.Drawing.Size(534, 39);
            this.step2Instruction.TabIndex = 0;
            this.step2Instruction.Text = resources.GetString("step2Instruction.Text");
            // 
            // AddAllAttachmentCheckBox
            // 
            this.AddAllAttachmentCheckBox.AutoSize = true;
            this.AddAllAttachmentCheckBox.Location = new System.Drawing.Point(35, 46);
            this.AddAllAttachmentCheckBox.Name = "AddAllAttachmentCheckBox";
            this.AddAllAttachmentCheckBox.Size = new System.Drawing.Size(255, 17);
            this.AddAllAttachmentCheckBox.TabIndex = 13;
            this.AddAllAttachmentCheckBox.Text = "Add remaining attachment(s) to candidate record";
            this.AddAllAttachmentCheckBox.UseVisualStyleBackColor = true;
            this.AddAllAttachmentCheckBox.Visible = false;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(476, 396);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "Next >";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(386, 396);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 6;
            this.backButton.Text = "< Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // cencelButton
            // 
            this.cencelButton.Location = new System.Drawing.Point(296, 396);
            this.cencelButton.Name = "cencelButton";
            this.cencelButton.Size = new System.Drawing.Size(75, 23);
            this.cencelButton.TabIndex = 5;
            this.cencelButton.Text = "Cancel";
            this.cencelButton.UseVisualStyleBackColor = true;
            this.cencelButton.Click += new System.EventHandler(this.cencelButton_Click);
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.Location = new System.Drawing.Point(12, 391);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(539, 2);
            this.line.TabIndex = 4;
            // 
            // emailFrom
            // 
            this.emailFrom.AutoSize = true;
            this.emailFrom.Location = new System.Drawing.Point(53, 87);
            this.emailFrom.Name = "emailFrom";
            this.emailFrom.Size = new System.Drawing.Size(67, 13);
            this.emailFrom.TabIndex = 3;
            this.emailFrom.Text = "This email is:";
            // 
            // Instruction
            // 
            this.Instruction.AutoSize = true;
            this.Instruction.Location = new System.Drawing.Point(53, 24);
            this.Instruction.MaximumSize = new System.Drawing.Size(462, 50);
            this.Instruction.Name = "Instruction";
            this.Instruction.Size = new System.Drawing.Size(459, 50);
            this.Instruction.TabIndex = 2;
            this.Instruction.Text = resources.GetString("Instruction.Text");
            // 
            // candidateTypeComboBox
            // 
            this.candidateTypeComboBox.DisplayMember = "(none)";
            this.candidateTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.candidateTypeComboBox.Items.AddRange(new object[] {
            "Add a new candidate",
            "Add a new candidate from a referral",
            "Add to an existing candidate",
            "Add a new contact",
            "Add to an existing contact"});
            this.candidateTypeComboBox.Location = new System.Drawing.Point(56, 112);
            this.candidateTypeComboBox.Name = "candidateTypeComboBox";
            this.candidateTypeComboBox.Size = new System.Drawing.Size(457, 21);
            this.candidateTypeComboBox.TabIndex = 1;
            this.candidateTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.candidateTypeComboBox_SelectedIndexChanged);
            // 
            // emailDetailsGroupBox
            // 
            this.emailDetailsGroupBox.Controls.Add(this.subjectTextbox);
            this.emailDetailsGroupBox.Controls.Add(this.receivedOnTextBox);
            this.emailDetailsGroupBox.Controls.Add(this.senderTextBox);
            this.emailDetailsGroupBox.Controls.Add(this.subject);
            this.emailDetailsGroupBox.Controls.Add(this.receivedOn);
            this.emailDetailsGroupBox.Controls.Add(this.sender);
            this.emailDetailsGroupBox.Location = new System.Drawing.Point(56, 158);
            this.emailDetailsGroupBox.Name = "emailDetailsGroupBox";
            this.emailDetailsGroupBox.Size = new System.Drawing.Size(457, 155);
            this.emailDetailsGroupBox.TabIndex = 0;
            this.emailDetailsGroupBox.TabStop = false;
            this.emailDetailsGroupBox.Text = "Email details";
            // 
            // subjectTextbox
            // 
            this.subjectTextbox.Location = new System.Drawing.Point(108, 99);
            this.subjectTextbox.Name = "subjectTextbox";
            this.subjectTextbox.Size = new System.Drawing.Size(327, 20);
            this.subjectTextbox.TabIndex = 5;
            // 
            // receivedOnTextBox
            // 
            this.receivedOnTextBox.Location = new System.Drawing.Point(108, 65);
            this.receivedOnTextBox.Name = "receivedOnTextBox";
            this.receivedOnTextBox.Size = new System.Drawing.Size(327, 20);
            this.receivedOnTextBox.TabIndex = 4;
            // 
            // senderTextBox
            // 
            this.senderTextBox.Location = new System.Drawing.Point(108, 34);
            this.senderTextBox.Name = "senderTextBox";
            this.senderTextBox.Size = new System.Drawing.Size(327, 20);
            this.senderTextBox.TabIndex = 3;
            // 
            // subject
            // 
            this.subject.AutoSize = true;
            this.subject.Location = new System.Drawing.Point(29, 102);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(46, 13);
            this.subject.TabIndex = 2;
            this.subject.Text = "Subject:";
            // 
            // receivedOn
            // 
            this.receivedOn.AutoSize = true;
            this.receivedOn.Location = new System.Drawing.Point(29, 68);
            this.receivedOn.Name = "receivedOn";
            this.receivedOn.Size = new System.Drawing.Size(73, 13);
            this.receivedOn.TabIndex = 1;
            this.receivedOn.Text = "Received On:";
            // 
            // sender
            // 
            this.sender.AutoSize = true;
            this.sender.Location = new System.Drawing.Point(29, 37);
            this.sender.Name = "sender";
            this.sender.Size = new System.Drawing.Size(44, 13);
            this.sender.TabIndex = 0;
            this.sender.Text = "Sender:";
            // 
            // AddToTaleo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(563, 480);
            this.Controls.Add(this.step1);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddToTaleo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo - Add to Taleo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddToTaleo_FormClosed);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.step1.ResumeLayout(false);
            this.step1.PerformLayout();
            this.step2.ResumeLayout(false);
            this.step2.PerformLayout();
            this.step3.ResumeLayout(false);
            this.step3.PerformLayout();
            this.step4.ResumeLayout(false);
            this.step4.PerformLayout();
            this.referral.ResumeLayout(false);
            this.referral.PerformLayout();
            this.emailDetailsGroupBox.ResumeLayout(false);
            this.emailDetailsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel step1;
        private System.Windows.Forms.ComboBox candidateTypeComboBox;
        private System.Windows.Forms.GroupBox emailDetailsGroupBox;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Label emailFrom;
        private System.Windows.Forms.Label Instruction;
        private System.Windows.Forms.TextBox subjectTextbox;
        private System.Windows.Forms.TextBox receivedOnTextBox;
        private System.Windows.Forms.TextBox senderTextBox;
        private System.Windows.Forms.Label subject;
        private System.Windows.Forms.Label receivedOn;
        private System.Windows.Forms.Label sender;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button cencelButton;
        private System.Windows.Forms.Panel step2;
        private System.Windows.Forms.RichTextBox emailBodyTextbox;
        private System.Windows.Forms.Label step2Instruction;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.Button step2BackButton;
        private System.Windows.Forms.Button step2CencelButton;
        private System.Windows.Forms.Label step2line;
        private System.Windows.Forms.Panel step3;
        private System.Windows.Forms.Label step3Instruction;
        private System.Windows.Forms.Button step3CencelButton;
        private System.Windows.Forms.Button step3BackButton;
        private System.Windows.Forms.Button step3AddButton;
        private System.Windows.Forms.Button step3FinishButton;
        private System.Windows.Forms.Label step3Line;
        private System.Windows.Forms.WebBrowser step3Browser;
        private System.Windows.Forms.Panel step4;
        private System.Windows.Forms.Label step4Instruction;
        private System.Windows.Forms.Label step4Line;
        private System.Windows.Forms.Button step4BackButton;
        private System.Windows.Forms.Button Step4CencelButton;
        private System.Windows.Forms.Button step4FinishButton;
        private System.Windows.Forms.Panel referral;
        private System.Windows.Forms.Button referralNextButton;
        private System.Windows.Forms.Button referralBackButton;
        private System.Windows.Forms.Button referralCencelButton;
        private System.Windows.Forms.Label referralLine;
        private System.Windows.Forms.Label referralInstruction;
        private System.Windows.Forms.TextBox referNameTextBox;
        private System.Windows.Forms.Label referralNameLabel;
        private System.Windows.Forms.WebBrowser step4WebBrowser;
        private System.Windows.Forms.CheckBox AddAllAttachmentCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label addtoTaleoStepsLabel;
    }
}