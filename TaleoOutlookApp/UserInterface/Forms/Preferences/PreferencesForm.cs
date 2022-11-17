using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Model.AuthenticateData;
using Service.AddIn;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.Login;
using TaleoOutlookAddin.Forms.TaleoFormHelper;
using Util.Utilities;
using Application = System.Windows.Forms.Application;
using Language = Model.AuthenticateData.Language;
using Service.Authentication;
using Util.Enums;

namespace TaleoOutlookAddin.Forms.Preferences
{
    public partial class PreferencesForm : Form
    {
        private string _signature;
        /// <summary>
        /// Preference form constructor
        /// </summary>
        public PreferencesForm()
        {
            InitializeComponent();
            prepopulateValues();
        }

        private void prepopulateValues()
        {
            _signature = GetSignature();
            signatureTextBox.Text = _signature;
        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            LeftsideButtonStyle();
            GetAccountInfoFromSession();
            accountPanel.Visible = true;
            feedbackPanel.Visible = false;
            languagePanel.Visible = false;
            advancePanel.Visible = false;
        }

        private void LeftsideButtonStyle()
        {
            accountButton.TabStop = false;
            accountButton.FlatStyle = FlatStyle.Flat;
            accountButton.FlatAppearance.BorderSize = 0;
            accountButton.BackColor = Color.Goldenrod;

            feedbackButton.TabStop = false;
            feedbackButton.FlatStyle = FlatStyle.Flat;
            feedbackButton.FlatAppearance.BorderSize = 0;

            languageButton.TabStop = false;
            languageButton.FlatStyle = FlatStyle.Flat;
            languageButton.FlatAppearance.BorderSize = 0;

            advanceButton.TabStop = false;
            advanceButton.FlatStyle = FlatStyle.Flat;
            advanceButton.FlatAppearance.BorderSize = 0;
        }

        private void GetAccountInfoFromSession()
        {
            if (!String.IsNullOrEmpty(TaleoAddIn.sessionData.lastLoginData.userName) || !String.IsNullOrEmpty(TaleoAddIn.sessionData.lastLoginData.password) || !String.IsNullOrEmpty(TaleoAddIn.sessionData.lastLoginData.companyCode))
            {
                userNameTextBox.Text = TaleoAddIn.sessionData.lastLoginData.userName;
                passwordTextBox.Text = TaleoAddIn.sessionData.lastLoginData.password;
                companyCodeTextBox.Text = TaleoAddIn.sessionData.lastLoginData.companyCode;
                rememberPasswordCheckBox.Checked = TaleoAddIn.sessionData.lastLoginData.remember == "True";
            }
            else if (!String.IsNullOrEmpty(TaleoAddIn.lastLogInData.userName) || !String.IsNullOrEmpty(TaleoAddIn.lastLogInData.password) || !String.IsNullOrEmpty(TaleoAddIn.lastLogInData.companyCode))
            {
                userNameTextBox.Text = TaleoAddIn.lastLogInData.userName;
                passwordTextBox.Text = TaleoAddIn.lastLogInData.password;
                companyCodeTextBox.Text = TaleoAddIn.lastLogInData.companyCode;
                rememberPasswordCheckBox.Checked = (TaleoAddIn.lastLogInData.remember != null &&
                                              TaleoAddIn.lastLogInData.remember.ToLower() == "true") ? true : false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show(GetResourceValueByName("Preference_AccountInfoResetWarning"),
                    GetResourceValueByName("Preference_MessageTitle"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;
                userNameTextBox.Clear();
                passwordTextBox.Clear();
                companyCodeTextBox.Clear();
                rememberPasswordCheckBox.Checked = false;
                resetButton.Enabled = false;
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                           Thread.CurrentThread.Name, ex.Message);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                //                if (accountPanel.Visible)
                {
                    if (AccountPanelAction())
                    {
                        if (!relogin())
                        {
                            TaleoMessageBox.Show(GetResourceValueByName("Preference_InvalidAccountInfo"));
                            return;
                        }
                        else
                        {
                            TaleoAddIn.OrgSettingsResponse = TaleoAddIn.addInService.GetEnableServiceResponse_REST(TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());

                        }
                        Close();
                    }
                }
                //                else if (feedbackPanel.Visible)
                {
                    if (FeedbackPanelAction())
                        Close();
                }
                //                else if (languagePanel.Visible)
                {
                    if (languageSelectComboBox.SelectedIndex != -1)
                    {
                        GetSelectedLanguageRsources();
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                           Thread.CurrentThread.Name, ex.Message);
            }
        }

        private bool relogin()
        {
            try
            {
                TaleoAddIn.serviceURL = AddInServices.getInstance().getServiceURL(TaleoAddIn.sessionData.lastLoginData.companyCode);
                if (!String.IsNullOrEmpty(TaleoAddIn.serviceURL))
                {
                    AuthenticationService authenticationService =
                        AuthenticationService.getInstance(TaleoAddIn.serviceURL, TaleoAddIn.USE_REST);
                    TaleoAddIn.sessionData = authenticationService.checkAuthentication(TaleoAddIn.sessionData,
                        AccessDepth.Only_AuthToken, false, false);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                           Thread.CurrentThread.Name, ex.Message);
                return false;
            }
            return true;
        }
        private string GetMaxVersion(string versionListString)
        {
            try
            {
                string[] versionList = null;
                if (!String.IsNullOrEmpty(versionListString))
                {
                    versionList = versionListString.Split(',');
                    return versionList[versionList.Length - 1];
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                           Thread.CurrentThread.Name, ex.Message);
            }
            return "";
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool AccountPanelAction()
        {
            try
            {
                if (userNameTextBox.Text == "" || passwordTextBox.Text == "" || companyCodeTextBox.Text == "")
                {
                    TaleoMessageBox.Show(GetResourceValueByName("Preference_AccountInfoEmpryWarning"));
                    return false;
                }
                else
                {
                    AuthenticationMessageForm authenticationMessageForm = new AuthenticationMessageForm();
                    authenticationMessageForm.Show();
                    Application.DoEvents();
                    LogInData logInDataObj = new LogInData();
                    logInDataObj.userName = userNameTextBox.Text;
                    logInDataObj.password = passwordTextBox.Text;
                    logInDataObj.companyCode = companyCodeTextBox.Text;
                    logInDataObj.remember = rememberPasswordCheckBox.Checked + "";
                    bool authenticateUseer = TaleoFormHelper.TaleoFormHelper.IsAutoLoogedIn();
                    if (authenticateUseer)
                    {
                        authenticationMessageForm.Close();
                        TaleoAddIn.sessionData.lastLoginData = logInDataObj;
                        AddInServices addInService = AddInServices.getInstance();
                        LoginForm loginForm = new LoginForm();
                        loginForm.saveLoginData(logInDataObj, addInService);
                    }
                    else if (String.IsNullOrEmpty(TaleoAddIn.lastLogInData.userName) || String.IsNullOrEmpty(TaleoAddIn.lastLogInData.password) || String.IsNullOrEmpty(TaleoAddIn.lastLogInData.companyCode))
                    {
                        authenticationMessageForm.Close();
                        TaleoAddIn.sessionData.lastLoginData = logInDataObj;
                        AddInServices addInService = AddInServices.getInstance();
                        LoginForm loginForm = new LoginForm();
                        loginForm.saveLoginData(logInDataObj, addInService);
                    }
                    else
                    {
                        authenticationMessageForm.Close();
                        TaleoMessageBox.Show(GetResourceValueByName("Preference_InvalidAccountInfo"));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                            Thread.CurrentThread.Name, ex.Message);
                return false;
            }
            return true;
        }

        private bool FeedbackPanelAction()
        {
            try
            {
                if (signatureTextBox.Text == "")
                    return false;
                _signature = GetSignature();
                if (_signature == signatureTextBox.Text)
                {
                    return true;
                }
                DialogResult dr = MessageBox.Show(GetResourceValueByName("Preference_SignatureChangeAlert"),
                    GetResourceValueByName("Preference_MessageTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                    return false;
                String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                String oraclePath = globalPath + "\\Oracle";
                String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
                String userSignetureSettingsFile = taleoPath + "\\Signeture.ini";
                if (File.Exists(userSignetureSettingsFile))
                {
                    System.IO.File.WriteAllText(userSignetureSettingsFile, signatureTextBox.Text);
                    TaleoMessageBox.Show(GetResourceValueByName("Preference_ApplicationRestart"));
                    return true;
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                return false;
            }
            return true;
        }

        private void accountButton_Click(object sender, EventArgs e)
        {
            accountPanel.Visible = true;

            accountButton.BackColor = Color.Goldenrod;
            feedbackButton.BackColor = Color.Transparent;
            languageButton.BackColor = Color.Transparent;
            advanceButton.BackColor = Color.Transparent;

            accountPanel.Visible = true;
            feedbackPanel.Visible = false;
            languagePanel.Visible = false;
            advancePanel.Visible = false;

            preferencePictureBox.Image = Properties.Resources.preferenceAccount;
        }

        private void feedbackButton_Click(object sender, EventArgs e)
        {
            accountButton.BackColor = Color.Transparent;
            feedbackButton.BackColor = Color.Goldenrod;
            languageButton.BackColor = Color.Transparent;
            advanceButton.BackColor = Color.Transparent;

            accountPanel.Visible = false;
            feedbackPanel.Visible = true;
            languagePanel.Visible = false;
            advancePanel.Visible = false;

            preferencePictureBox.Image = Properties.Resources.preferenceFeedback;
            // _signature = GetResourceValueByName("Signature");
            _signature = GetSignature();
            signatureTextBox.Text = String.IsNullOrEmpty(_signature) ? "Thanks," : _signature;
        }
        private string GetSignature()
        {
            try
            {
                string signature = "";
                String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                String oraclePath = globalPath + "\\Oracle";
                String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
                String userSettingsFile = taleoPath + "\\Signeture.ini";
                if (File.Exists(userSettingsFile))
                    signature = File.ReadAllText(userSettingsFile);
                return signature;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
            return "";
        }
        private void languageButton_Click(object sender, EventArgs e)
        {
            LoadLanguageSelectComboBox();
            accountButton.BackColor = Color.Transparent;
            feedbackButton.BackColor = Color.Transparent;
            languageButton.BackColor = Color.Goldenrod;
            advanceButton.BackColor = Color.Transparent;

            accountPanel.Visible = false;
            feedbackPanel.Visible = false;
            languagePanel.Visible = true;
            advancePanel.Visible = false;

            preferencePictureBox.Image = Properties.Resources.PrefereneLanguage;
        }

        private void advanceButton_Click(object sender, EventArgs e)
        {
            accountButton.BackColor = Color.Transparent;
            feedbackButton.BackColor = Color.Transparent;
            languageButton.BackColor = Color.Transparent;
            advanceButton.BackColor = Color.Goldenrod;

            accountPanel.Visible = false;
            feedbackPanel.Visible = false;
            languagePanel.Visible = false;
            advancePanel.Visible = true;

            preferencePictureBox.Image = Properties.Resources.PrefereneAdvance;
        }

        #region Language
        private void LoadLanguageSelectComboBox()
        {
            //Build a list
            try
            {
                string languageSource = TaleoFormHelper.TaleoFormHelper.GetValueFromSettingByKey("Language", "TaleoSettings.ini");

                Language language = new Language();
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                languageSource =  @"{
                                        
                                            ""Name"": ""English"",
		                                    ""Version"": ""1.0.1"",
                                                ""Code"":""en"",
		                                    ""Resource"":  [
					                                      {""Key"": ""Book"", ""Value"": ""Book""},
					                                      {""Key"": ""Cat"", ""Value"": ""Cat""},
					                                      {""Key"": ""Lion"", ""Value"": ""Lion""}
                                                         ]
                                            
        
                                     }
                                 ";
                language = serializer.Deserialize<Language>(languageSource);

                //var dataSource = new List<ComboLanguage>();
                //dataSource.Add(new ComboLanguage() { Name = "English", Value = "en" });
                //dataSource.Add(new ComboLanguage() { Name = "বাংলা", Value = "bn" });
                //dataSource.Add(new ComboLanguage() { Name = "العربية", Value = "ar" });
                //dataSource.Add(new ComboLanguage() { Name = "français", Value = "fr" });
                //dataSource.Add(new ComboLanguage() { Name = "中國", Value = "ch" });
                //dataSource.Add(new ComboLanguage() { Name = "日本人", Value = "jp" });
                //dataSource.Add(new ComboLanguage() { Name = "한국의", Value = "kr" });

                //Setup data binding
                if (language != null && language.Languages != null)
                {
                    this.languageSelectComboBox.DataSource = language.Languages;
                    this.languageSelectComboBox.DisplayMember = "Value";
                    this.languageSelectComboBox.ValueMember = "Key";

                    // make it readonly
                    this.languageSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    string currentLanguageVersion = TaleoFormHelper.TaleoFormHelper.GetValueFromSettingByKey("LanguageCurrentVersion", "ApplicationSettings.ini");
                    if (!String.IsNullOrEmpty(currentLanguageVersion))
                    {
                        languageSelectComboBox.SelectedItem = currentLanguageVersion;
                    }
                    this.languageSelectComboBox.SelectedIndex = this.languageSelectComboBox.FindString(currentLanguageVersion);

                    for (int i = 0; i < this.languageSelectComboBox.Items.Count; i++)
                        if (((Model.AuthenticateData.ComboLanguage)this.languageSelectComboBox.Items[i]).Key.ToString() == currentLanguageVersion)
                        {
                            this.languageSelectComboBox.SelectedIndex = i;
                            break;
                        }
                }

            }
            catch (Exception)
            {

            }
        }
        private void GetSelectedLanguageRsources()
        {
            if (TaleoFormHelper.TaleoFormHelper.GetValueFromSettingByKey("LanguageCurrentVersion", "ApplicationSettings.ini") != languageSelectComboBox.SelectedValue.ToString().Trim())
            {
                TaleoFormHelper.TaleoFormHelper.SetValueFromSettingByKey("LanguageFileName", "Taleo." + languageSelectComboBox.SelectedValue.ToString().Trim() + ".resx", "ApplicationSettings.ini");
                TaleoFormHelper.TaleoFormHelper.SetValueFromSettingByKey("LanguageCurrentVersion", languageSelectComboBox.SelectedValue.ToString(), "ApplicationSettings.ini");
                MessageBox.Show("To see the changes please restart the outlook.", "Taleo Outlook Plugin",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //DialogResult dr = MessageBox.Show("Do you want to restart Outlook to see the changes?", "Taleo Outlook Addin",
                //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{

                //    Process.Start("outlook.exe");
                //    Process.GetCurrentProcess().Kill();
                //}
            }
            else
            {
                //                TaleoMessageBox.Show("Your lanuage version is already updated");
            }
            //string languageRestVersion = "1.0.1";
            //string maxVersion = "";
            //System.Version versionApp = null;
            //string languageCurrentVersionKey = TaleoFormHelper.TaleoFormHelper.GetResourceValueByKey("Language_" + languageSelectComboBox.SelectedValue + "_Version");
            //if (!String.IsNullOrEmpty(languageCurrentVersionKey))
            //{
            //    maxVersion = GetMaxVersion(languageCurrentVersionKey);
            //}
            //System.Version versionRest = new System.Version(languageRestVersion);
            //if (!String.IsNullOrEmpty(maxVersion))
            //    versionApp = new System.Version(maxVersion);
            //string fullResourcePath = GetResoursePath();
            //if (String.IsNullOrEmpty(languageCurrentVersionKey) && !String.IsNullOrEmpty(languageRestVersion))
            //{
            //    if (TaleoFormHelper.TaleoFormHelper.AddOrUpdateResourceFile(
            //            "Language_" + languageSelectComboBox.SelectedValue + "_Version", languageRestVersion,
            //            fullResourcePath))
            //    {
            //        SetLanguageResourcesFromRest();
            //        TaleoMessageBox.Show("Your lanuage version is updated successfully");
            //    }
            //}
            //else if (versionRest > versionApp)
            //{
            //    SetLanguageResourcesFromRest();
            //    TaleoMessageBox.Show("Your lanuage version is updated successfully");
            //}
            //else
            //{
            //    TaleoMessageBox.Show("Your lanuage version is already updated");
            //}
        }
        private void SetLanguageResourcesFromRest()
        {
            string fullResourcePath = GetResoursePath();
            string languageDummyJson = @"{
                                        
                                            ""Name"": ""English"",
		                                    ""Version"": ""1.0.1"",
                                                ""Code"":""en"",
		                                    ""Resource"":  [
					                                      {""Key"": ""Book"", ""Value"": ""Book""},
					                                      {""Key"": ""Cat"", ""Value"": ""Cat""},
					                                      {""Key"": ""Lion"", ""Value"": ""Lion""}
                                                         ]
                                            
        
                                     }
                                 ";
            Language language = new Language();
            var serializer1 = new System.Web.Script.Serialization.JavaScriptSerializer();
            language = serializer1.Deserialize<Language>(languageDummyJson);

            try
            {
                foreach (LanguageResourceData resource in language.Resource)
                {

                    string convertedLanguageRestVersion = language.Version.Replace(".", "");
                    if (TaleoFormHelper.TaleoFormHelper.AddOrUpdateResourceFile(resource.Key + "_" + language.Code + "_" + convertedLanguageRestVersion, resource.Value,
                        fullResourcePath))
                    {
                        continue;
                    }
                    else
                    {
                        Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                            Thread.CurrentThread.Name, resource.Key + " not added.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name,
                            Thread.CurrentThread.Name, ex);
                TaleoMessageBox.Show(fullResourcePath + " is not created.");
            }
        }
        private string GetResoursePath()
        {
            string currentVersion = TaleoFormHelper.TaleoFormHelper.GetApplicationCurrentVersion();
            string resourceFileFullPath = TaleoFormHelper.TaleoFormHelper.RootPath + "Resources\\Language\\TalaeoLanguage.resx";
            return resourceFileFullPath;
        }
        private static string GetResourceValueByName(string resourceName)
        {
            return TaleoFormHelper.TaleoFormHelper.GetResourceValueByName(resourceName);
        }
        /// <summary>
        /// this is dummy method for load language, in future we remove it
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private static string GetResourceValueByName1(string resourceName)
        {
            ResourceManager _resourceManager = new ResourceManager("TaleoOutlookAddin.Resources.Language.TalaeoLanguage", typeof(PreferencesForm).Assembly);
            return _resourceManager.GetString(resourceName);
        }
        /// <summary>
        /// this is dummy method for load language, in future we remove it
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public string GetResourceValueByName2(string resourceName)
        {

            try
            {

                //Create a new ResXResource reader and set the resx path to resxPathName
                ResXResourceReader resReader = new ResXResourceReader(GetResoursePath());
                //Enumerate the elements within the resx file and dispaly them

                foreach (DictionaryEntry d in resReader)
                {
                    if (d.Key.ToString() == resourceName)
                    {
                        resReader.Close();
                        return d.Value.ToString();
                    }
                    //MessageBox.Show(d.Key.ToString() + ": " + d.Value.ToString());
                }

                //Close the resxReader
                resReader.Close();
            }

       //If the resx file represents some incoherences

            catch (ArgumentException caught)
            {
                TaleoMessageBox.Show("Source: " + caught.Source + "Message: " + caught.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, caught.Message);
            }
            return null;
        }
        #endregion
    }
}
