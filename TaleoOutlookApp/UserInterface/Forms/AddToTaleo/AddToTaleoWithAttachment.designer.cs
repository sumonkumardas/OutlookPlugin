namespace TaleoOutlookAddin.Forms.AddToTaleo
{
    partial class AddToTaleoWithAttachment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddToTaleoWithAttachment));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.addtoTaleoStepsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.step4FinishButton = new System.Windows.Forms.Button();
            this.step4BackButton = new System.Windows.Forms.Button();
            this.Step4CencelButton = new System.Windows.Forms.Button();
            this.step4Line = new System.Windows.Forms.Label();
            this.confirmationTextBox = new System.Windows.Forms.RichTextBox();
            this.step4Instruction = new System.Windows.Forms.Label();
            this.step3AddButton = new System.Windows.Forms.Button();
            this.step3FinishButton = new System.Windows.Forms.Button();
            this.step3BackButton = new System.Windows.Forms.Button();
            this.step3CencelButton = new System.Windows.Forms.Button();
            this.step3Line = new System.Windows.Forms.Label();
            this.step3Browser = new System.Windows.Forms.WebBrowser();
            this.step3Instruction = new System.Windows.Forms.Label();
            this.AddAllAttachmentCheckBox = new System.Windows.Forms.CheckBox();
            this.emailAttachmentsListView = new System.Windows.Forms.ListView();
            this.cancelStep2Button = new System.Windows.Forms.Button();
            this.nextStep2Button = new System.Windows.Forms.Button();
            this.backStep2Button = new System.Windows.Forms.Button();
            this.lineStep2 = new System.Windows.Forms.Label();
            this.emailTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.emailAttachmentRadio = new System.Windows.Forms.RadioButton();
            this.emailBodyRadio = new System.Windows.Forms.RadioButton();
            this.step2Instruction = new System.Windows.Forms.Label();
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
            this.emailTypeGroupBox.SuspendLayout();
            this.emailDetailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Transparent;
            this.headerPanel.BackgroundImage = global::TaleoOutlookAddin.Properties.Resources.addtotaleo;
            this.headerPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerPanel.Controls.Add(this.addtoTaleoStepsLabel);
            this.headerPanel.Controls.Add(this.label1);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(563, 49);
            this.headerPanel.TabIndex = 1;
            // 
            // addtoTaleoStepsLabel
            // 
            this.addtoTaleoStepsLabel.AutoSize = true;
            this.addtoTaleoStepsLabel.BackColor = System.Drawing.Color.Transparent;
            this.addtoTaleoStepsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addtoTaleoStepsLabel.Location = new System.Drawing.Point(58, 28);
            this.addtoTaleoStepsLabel.Name = "addtoTaleoStepsLabel";
            this.addtoTaleoStepsLabel.Size = new System.Drawing.Size(428, 13);
            this.addtoTaleoStepsLabel.TabIndex = 20;
            this.addtoTaleoStepsLabel.Text = "Use selected email to manage candidates or contacts in taleo. Step 1 of 4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Add to Taleo";
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
            this.step1.TabIndex = 3;
            // 
            // step2
            // 
            this.step2.Controls.Add(this.step3);
            this.step2.Controls.Add(this.AddAllAttachmentCheckBox);
            this.step2.Controls.Add(this.emailAttachmentsListView);
            this.step2.Controls.Add(this.cancelStep2Button);
            this.step2.Controls.Add(this.nextStep2Button);
            this.step2.Controls.Add(this.backStep2Button);
            this.step2.Controls.Add(this.lineStep2);
            this.step2.Controls.Add(this.emailTypeGroupBox);
            this.step2.Controls.Add(this.step2Instruction);
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
            this.step3.Controls.Add(this.step3AddButton);
            this.step3.Controls.Add(this.step3FinishButton);
            this.step3.Controls.Add(this.step3BackButton);
            this.step3.Controls.Add(this.step3CencelButton);
            this.step3.Controls.Add(this.step3Line);
            this.step3.Controls.Add(this.step3Browser);
            this.step3.Controls.Add(this.step3Instruction);
            this.step3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step3.Location = new System.Drawing.Point(0, 0);
            this.step3.Name = "step3";
            this.step3.Size = new System.Drawing.Size(563, 431);
            this.step3.TabIndex = 12;
            this.step3.Visible = false;
            // 
            // step4
            // 
            this.step4.Controls.Add(this.referral);
            this.step4.Controls.Add(this.step4FinishButton);
            this.step4.Controls.Add(this.step4BackButton);
            this.step4.Controls.Add(this.Step4CencelButton);
            this.step4.Controls.Add(this.step4Line);
            this.step4.Controls.Add(this.confirmationTextBox);
            this.step4.Controls.Add(this.step4Instruction);
            this.step4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step4.Location = new System.Drawing.Point(0, 0);
            this.step4.Name = "step4";
            this.step4.Size = new System.Drawing.Size(563, 431);
            this.step4.TabIndex = 20;
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
            this.referral.TabIndex = 17;
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
            // step4FinishButton
            // 
            this.step4FinishButton.Location = new System.Drawing.Point(476, 396);
            this.step4FinishButton.Name = "step4FinishButton";
            this.step4FinishButton.Size = new System.Drawing.Size(75, 23);
            this.step4FinishButton.TabIndex = 16;
            this.step4FinishButton.Text = "Finish";
            this.step4FinishButton.UseVisualStyleBackColor = true;
            this.step4FinishButton.Click += new System.EventHandler(this.step4FinishButton_Click);
            // 
            // step4BackButton
            // 
            this.step4BackButton.Location = new System.Drawing.Point(386, 396);
            this.step4BackButton.Name = "step4BackButton";
            this.step4BackButton.Size = new System.Drawing.Size(75, 23);
            this.step4BackButton.TabIndex = 15;
            this.step4BackButton.Text = "Back";
            this.step4BackButton.UseVisualStyleBackColor = true;
            // 
            // Step4CencelButton
            // 
            this.Step4CencelButton.Location = new System.Drawing.Point(296, 396);
            this.Step4CencelButton.Name = "Step4CencelButton";
            this.Step4CencelButton.Size = new System.Drawing.Size(75, 23);
            this.Step4CencelButton.TabIndex = 14;
            this.Step4CencelButton.Text = "Cancel";
            this.Step4CencelButton.UseVisualStyleBackColor = true;
            // 
            // step4Line
            // 
            this.step4Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.step4Line.Location = new System.Drawing.Point(12, 389);
            this.step4Line.Name = "step4Line";
            this.step4Line.Size = new System.Drawing.Size(539, 2);
            this.step4Line.TabIndex = 13;
            // 
            // confirmationTextBox
            // 
            this.confirmationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmationTextBox.Location = new System.Drawing.Point(12, 27);
            this.confirmationTextBox.Name = "confirmationTextBox";
            this.confirmationTextBox.ReadOnly = true;
            this.confirmationTextBox.Size = new System.Drawing.Size(539, 352);
            this.confirmationTextBox.TabIndex = 12;
            this.confirmationTextBox.Text = " Candidate Create Confirmation.\n Candidate was created successfully!";
            // 
            // step4Instruction
            // 
            this.step4Instruction.AutoSize = true;
            this.step4Instruction.Location = new System.Drawing.Point(12, 11);
            this.step4Instruction.Name = "step4Instruction";
            this.step4Instruction.Size = new System.Drawing.Size(403, 13);
            this.step4Instruction.TabIndex = 1;
            this.step4Instruction.Text = "A new candidate was successfully added to Taleo. Click “Finish” to close the dial" +
    "og.";
            // 
            // step3AddButton
            // 
            this.step3AddButton.Location = new System.Drawing.Point(476, 396);
            this.step3AddButton.Name = "step3AddButton";
            this.step3AddButton.Size = new System.Drawing.Size(75, 23);
            this.step3AddButton.TabIndex = 19;
            this.step3AddButton.Text = "Add >";
            this.step3AddButton.UseVisualStyleBackColor = true;
            this.step3AddButton.Click += new System.EventHandler(this.step3AddButton_Click);
            // 
            // step3FinishButton
            // 
            this.step3FinishButton.Location = new System.Drawing.Point(476, 396);
            this.step3FinishButton.Name = "step3FinishButton";
            this.step3FinishButton.Size = new System.Drawing.Size(75, 23);
            this.step3FinishButton.TabIndex = 19;
            this.step3FinishButton.Text = "Finish";
            this.step3FinishButton.UseVisualStyleBackColor = true;
            this.step3FinishButton.Click += new System.EventHandler(this.step3FinishButton_Click);
            // 
            // step3BackButton
            // 
            this.step3BackButton.Location = new System.Drawing.Point(386, 396);
            this.step3BackButton.Name = "step3BackButton";
            this.step3BackButton.Size = new System.Drawing.Size(75, 23);
            this.step3BackButton.TabIndex = 18;
            this.step3BackButton.Text = "< Back";
            this.step3BackButton.UseVisualStyleBackColor = true;
            this.step3BackButton.Click += new System.EventHandler(this.step3BackButton_Click);
            // 
            // step3CencelButton
            // 
            this.step3CencelButton.Location = new System.Drawing.Point(296, 396);
            this.step3CencelButton.Name = "step3CencelButton";
            this.step3CencelButton.Size = new System.Drawing.Size(75, 23);
            this.step3CencelButton.TabIndex = 17;
            this.step3CencelButton.Text = "Cancel";
            this.step3CencelButton.UseVisualStyleBackColor = true;
            this.step3CencelButton.Click += new System.EventHandler(this.step3CencelButton_Click);
            // 
            // step3Line
            // 
            this.step3Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.step3Line.Location = new System.Drawing.Point(12, 389);
            this.step3Line.Name = "step3Line";
            this.step3Line.Size = new System.Drawing.Size(539, 2);
            this.step3Line.TabIndex = 16;
            // 
            // step3Browser
            // 
            this.step3Browser.Location = new System.Drawing.Point(12, 27);
            this.step3Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.step3Browser.Name = "step3Browser";
            this.step3Browser.Size = new System.Drawing.Size(539, 352);
            this.step3Browser.TabIndex = 15;
            // 
            // step3Instruction
            // 
            this.step3Instruction.AutoSize = true;
            this.step3Instruction.Location = new System.Drawing.Point(12, 11);
            this.step3Instruction.Name = "step3Instruction";
            this.step3Instruction.Size = new System.Drawing.Size(507, 13);
            this.step3Instruction.TabIndex = 1;
            this.step3Instruction.Text = "Please review the new candidate information.  Once confirmed, click “Add” to add " +
    "this candidate to Taleo.";
            // 
            // AddAllAttachmentCheckBox
            // 
            this.AddAllAttachmentCheckBox.AutoSize = true;
            this.AddAllAttachmentCheckBox.Location = new System.Drawing.Point(62, 346);
            this.AddAllAttachmentCheckBox.Name = "AddAllAttachmentCheckBox";
            this.AddAllAttachmentCheckBox.Size = new System.Drawing.Size(255, 17);
            this.AddAllAttachmentCheckBox.TabIndex = 12;
            this.AddAllAttachmentCheckBox.Text = "Add remaining attachment(s) to candidate record";
            this.AddAllAttachmentCheckBox.UseVisualStyleBackColor = true;
            this.AddAllAttachmentCheckBox.Visible = false;
            // 
            // emailAttachmentsListView
            // 
            this.emailAttachmentsListView.Location = new System.Drawing.Point(56, 181);
            this.emailAttachmentsListView.Name = "emailAttachmentsListView";
            this.emailAttachmentsListView.Size = new System.Drawing.Size(446, 138);
            this.emailAttachmentsListView.TabIndex = 11;
            this.emailAttachmentsListView.UseCompatibleStateImageBehavior = false;
            this.emailAttachmentsListView.View = System.Windows.Forms.View.SmallIcon;
            this.emailAttachmentsListView.Click += new System.EventHandler(this.emailAttachmentsListView_Click);
            // 
            // cancelStep2Button
            // 
            this.cancelStep2Button.Location = new System.Drawing.Point(296, 396);
            this.cancelStep2Button.Name = "cancelStep2Button";
            this.cancelStep2Button.Size = new System.Drawing.Size(75, 23);
            this.cancelStep2Button.TabIndex = 10;
            this.cancelStep2Button.Text = "Cancel";
            this.cancelStep2Button.UseVisualStyleBackColor = true;
            this.cancelStep2Button.Click += new System.EventHandler(this.cancelStep2Button_Click);
            // 
            // nextStep2Button
            // 
            this.nextStep2Button.Location = new System.Drawing.Point(476, 396);
            this.nextStep2Button.Name = "nextStep2Button";
            this.nextStep2Button.Size = new System.Drawing.Size(75, 23);
            this.nextStep2Button.TabIndex = 9;
            this.nextStep2Button.Text = "Next >";
            this.nextStep2Button.UseVisualStyleBackColor = true;
            this.nextStep2Button.Click += new System.EventHandler(this.nextStep2Button_Click);
            // 
            // backStep2Button
            // 
            this.backStep2Button.Location = new System.Drawing.Point(386, 396);
            this.backStep2Button.Name = "backStep2Button";
            this.backStep2Button.Size = new System.Drawing.Size(75, 23);
            this.backStep2Button.TabIndex = 8;
            this.backStep2Button.Text = "< Back";
            this.backStep2Button.UseVisualStyleBackColor = true;
            this.backStep2Button.Click += new System.EventHandler(this.backStep2Button_Click);
            // 
            // lineStep2
            // 
            this.lineStep2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineStep2.Location = new System.Drawing.Point(12, 391);
            this.lineStep2.Name = "lineStep2";
            this.lineStep2.Size = new System.Drawing.Size(539, 2);
            this.lineStep2.TabIndex = 5;
            // 
            // emailTypeGroupBox
            // 
            this.emailTypeGroupBox.Controls.Add(this.emailAttachmentRadio);
            this.emailTypeGroupBox.Controls.Add(this.emailBodyRadio);
            this.emailTypeGroupBox.Location = new System.Drawing.Point(56, 87);
            this.emailTypeGroupBox.Name = "emailTypeGroupBox";
            this.emailTypeGroupBox.Size = new System.Drawing.Size(446, 88);
            this.emailTypeGroupBox.TabIndex = 2;
            this.emailTypeGroupBox.TabStop = false;
            // 
            // emailAttachmentRadio
            // 
            this.emailAttachmentRadio.AutoSize = true;
            this.emailAttachmentRadio.Location = new System.Drawing.Point(7, 53);
            this.emailAttachmentRadio.Name = "emailAttachmentRadio";
            this.emailAttachmentRadio.Size = new System.Drawing.Size(107, 17);
            this.emailAttachmentRadio.TabIndex = 1;
            this.emailAttachmentRadio.TabStop = true;
            this.emailAttachmentRadio.Text = "Email Attachment";
            this.emailAttachmentRadio.UseVisualStyleBackColor = true;
            this.emailAttachmentRadio.CheckedChanged += new System.EventHandler(this.emailAttachmentRadio_CheckedChanged);
            // 
            // emailBodyRadio
            // 
            this.emailBodyRadio.AutoSize = true;
            this.emailBodyRadio.Location = new System.Drawing.Point(6, 19);
            this.emailBodyRadio.Name = "emailBodyRadio";
            this.emailBodyRadio.Size = new System.Drawing.Size(121, 17);
            this.emailBodyRadio.TabIndex = 0;
            this.emailBodyRadio.TabStop = true;
            this.emailBodyRadio.Text = "Email message body";
            this.emailBodyRadio.UseVisualStyleBackColor = true;
            // 
            // step2Instruction
            // 
            this.step2Instruction.AutoSize = true;
            this.step2Instruction.Location = new System.Drawing.Point(53, 24);
            this.step2Instruction.MaximumSize = new System.Drawing.Size(462, 50);
            this.step2Instruction.Name = "step2Instruction";
            this.step2Instruction.Size = new System.Drawing.Size(449, 39);
            this.step2Instruction.TabIndex = 1;
            this.step2Instruction.Text = resources.GetString("step2Instruction.Text");
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
            this.cencelButton.Text = "Cencel";
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
            this.Instruction.Size = new System.Drawing.Size(459, 39);
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
            // AddToTaleoWithAttachment
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
            this.Name = "AddToTaleoWithAttachment";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Taleo - Add to Taleo with email attachment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddToTaleoWithAttachment_FormClosing);
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
            this.emailTypeGroupBox.ResumeLayout(false);
            this.emailTypeGroupBox.PerformLayout();
            this.emailDetailsGroupBox.ResumeLayout(false);
            this.emailDetailsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel step1;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button cencelButton;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Label emailFrom;
        private System.Windows.Forms.Label Instruction;
        private System.Windows.Forms.ComboBox candidateTypeComboBox;
        private System.Windows.Forms.GroupBox emailDetailsGroupBox;
        private System.Windows.Forms.TextBox subjectTextbox;
        private System.Windows.Forms.TextBox receivedOnTextBox;
        private System.Windows.Forms.TextBox senderTextBox;
        private System.Windows.Forms.Label subject;
        private System.Windows.Forms.Label receivedOn;
        private System.Windows.Forms.Label sender;
        private System.Windows.Forms.Panel step2;
        private System.Windows.Forms.Label step2Instruction;
        private System.Windows.Forms.GroupBox emailTypeGroupBox;
        private System.Windows.Forms.RadioButton emailAttachmentRadio;
        private System.Windows.Forms.RadioButton emailBodyRadio;
        private System.Windows.Forms.Button cancelStep2Button;
        private System.Windows.Forms.Button nextStep2Button;
        private System.Windows.Forms.Button backStep2Button;
        private System.Windows.Forms.Label lineStep2;
        private System.Windows.Forms.ListView emailAttachmentsListView;
        private System.Windows.Forms.Panel step3;
        private System.Windows.Forms.Label step3Instruction;
        private System.Windows.Forms.WebBrowser step3Browser;
        private System.Windows.Forms.Label step3Line;
        private System.Windows.Forms.Button step3CencelButton;
        private System.Windows.Forms.Button step3BackButton;
        private System.Windows.Forms.Button step3AddButton;
        private System.Windows.Forms.Button step3FinishButton;
        private System.Windows.Forms.Panel step4;
        private System.Windows.Forms.Label step4Instruction;
        private System.Windows.Forms.RichTextBox confirmationTextBox;
        private System.Windows.Forms.Label step4Line;
        private System.Windows.Forms.Button Step4CencelButton;
        private System.Windows.Forms.Button step4BackButton;
        private System.Windows.Forms.Button step4FinishButton;
        private System.Windows.Forms.Panel referral;
        private System.Windows.Forms.TextBox referNameTextBox;
        private System.Windows.Forms.Label referralNameLabel;
        private System.Windows.Forms.Label referralInstruction;
        private System.Windows.Forms.Label referralLine;
        private System.Windows.Forms.Button referralNextButton;
        private System.Windows.Forms.Button referralBackButton;
        private System.Windows.Forms.Button referralCencelButton;
        private System.Windows.Forms.CheckBox AddAllAttachmentCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label addtoTaleoStepsLabel;

    }
}