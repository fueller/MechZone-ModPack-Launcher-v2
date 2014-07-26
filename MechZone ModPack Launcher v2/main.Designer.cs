namespace MechZone_ModPack_Launcher_v2
{
    partial class mainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.mainTabControl = new MetroFramework.Controls.MetroTabControl();
            this.infoTabPage = new MetroFramework.Controls.MetroTabPage();
            this.infoWebBrowser = new System.Windows.Forms.WebBrowser();
            this.modPacksTabPage = new MetroFramework.Controls.MetroTabPage();
            this.modPacksContainer = new MetroFramework.Controls.MetroPanel();
            this.logTabPage = new MetroFramework.Controls.MetroTabPage();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.optionsTabPage = new MetroFramework.Controls.MetroTabPage();
            this.selectedRam = new MetroFramework.Controls.MetroLabel();
            this.ramSelector = new MetroFramework.Controls.MetroTrackBar();
            this.themeSelector = new MetroFramework.Controls.MetroComboBox();
            this.themeLabel = new MetroFramework.Controls.MetroLabel();
            this.styleLabel = new MetroFramework.Controls.MetroLabel();
            this.styleSelector = new MetroFramework.Controls.MetroComboBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.loginButton = new MetroFramework.Controls.MetroButton();
            this.profileBox = new MetroFramework.Controls.MetroComboBox();
            this.profileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addProfile = new MetroFramework.Controls.MetroButton();
            this.gameSettings = new System.Windows.Forms.GroupBox();
            this.ramDescription = new MetroFramework.Controls.MetroLabel();
            this.extraJavaParameters = new MetroFramework.Controls.MetroTextBox();
            this.extraDescription = new MetroFramework.Controls.MetroLabel();
            this.javaPathDescription = new MetroFramework.Controls.MetroLabel();
            this.javaPathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.changeJavaPath = new MetroFramework.Controls.MetroButton();
            this.installPathDescripton = new MetroFramework.Controls.MetroLabel();
            this.installPathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.changeInstallPath = new MetroFramework.Controls.MetroButton();
            this.mainTabControl.SuspendLayout();
            this.infoTabPage.SuspendLayout();
            this.modPacksTabPage.SuspendLayout();
            this.logTabPage.SuspendLayout();
            this.optionsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.profileContextMenu.SuspendLayout();
            this.gameSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.infoTabPage);
            this.mainTabControl.Controls.Add(this.optionsTabPage);
            this.mainTabControl.Controls.Add(this.modPacksTabPage);
            this.mainTabControl.Controls.Add(this.logTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(23, 63);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 3;
            this.mainTabControl.Size = new System.Drawing.Size(1092, 467);
            this.mainTabControl.TabIndex = 0;
            // 
            // infoTabPage
            // 
            this.infoTabPage.Controls.Add(this.infoWebBrowser);
            this.infoTabPage.HorizontalScrollbarBarColor = true;
            this.infoTabPage.HorizontalScrollbarSize = 0;
            this.infoTabPage.Location = new System.Drawing.Point(4, 35);
            this.infoTabPage.Name = "infoTabPage";
            this.infoTabPage.Size = new System.Drawing.Size(1084, 428);
            this.infoTabPage.TabIndex = 0;
            this.infoTabPage.Text = "Info";
            this.infoTabPage.VerticalScrollbarBarColor = true;
            this.infoTabPage.VerticalScrollbarSize = 0;
            // 
            // infoWebBrowser
            // 
            this.infoWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.infoWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.infoWebBrowser.Name = "infoWebBrowser";
            this.infoWebBrowser.Size = new System.Drawing.Size(1084, 428);
            this.infoWebBrowser.TabIndex = 2;
            this.infoWebBrowser.Url = new System.Uri("http://mechzone.net/modpack/launcher/info/info.html", System.UriKind.Absolute);
            this.infoWebBrowser.WebBrowserShortcutsEnabled = false;
            this.infoWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.infoWebBrowser_Navigating);
            // 
            // modPacksTabPage
            // 
            this.modPacksTabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.modPacksTabPage.Controls.Add(this.modPacksContainer);
            this.modPacksTabPage.HorizontalScrollbarBarColor = true;
            this.modPacksTabPage.Location = new System.Drawing.Point(4, 35);
            this.modPacksTabPage.Name = "modPacksTabPage";
            this.modPacksTabPage.Size = new System.Drawing.Size(1084, 428);
            this.modPacksTabPage.TabIndex = 1;
            this.modPacksTabPage.Text = "ModPacks";
            this.modPacksTabPage.VerticalScrollbarBarColor = true;
            // 
            // modPacksContainer
            // 
            this.modPacksContainer.AutoScroll = true;
            this.modPacksContainer.HorizontalScrollbar = true;
            this.modPacksContainer.HorizontalScrollbarBarColor = true;
            this.modPacksContainer.HorizontalScrollbarHighlightOnWheel = false;
            this.modPacksContainer.HorizontalScrollbarSize = 10;
            this.modPacksContainer.Location = new System.Drawing.Point(3, 3);
            this.modPacksContainer.Name = "modPacksContainer";
            this.modPacksContainer.Size = new System.Drawing.Size(510, 418);
            this.modPacksContainer.TabIndex = 2;
            this.modPacksContainer.VerticalScrollbar = true;
            this.modPacksContainer.VerticalScrollbarBarColor = false;
            this.modPacksContainer.VerticalScrollbarHighlightOnWheel = false;
            this.modPacksContainer.VerticalScrollbarSize = 10;
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logTextBox);
            this.logTabPage.HorizontalScrollbarBarColor = true;
            this.logTabPage.HorizontalScrollbarSize = 0;
            this.logTabPage.Location = new System.Drawing.Point(4, 35);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Size = new System.Drawing.Size(1084, 428);
            this.logTabPage.TabIndex = 3;
            this.logTabPage.Text = "Log";
            this.logTabPage.VerticalScrollbarBarColor = true;
            this.logTabPage.VerticalScrollbarSize = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(1084, 428);
            this.logTextBox.TabIndex = 2;
            this.logTextBox.Text = "";
            this.logTextBox.WordWrap = false;
            this.logTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.logTextBox_LinkClicked);
            // 
            // optionsTabPage
            // 
            this.optionsTabPage.Controls.Add(this.gameSettings);
            this.optionsTabPage.Controls.Add(this.themeSelector);
            this.optionsTabPage.Controls.Add(this.themeLabel);
            this.optionsTabPage.Controls.Add(this.styleLabel);
            this.optionsTabPage.Controls.Add(this.styleSelector);
            this.optionsTabPage.HorizontalScrollbarBarColor = true;
            this.optionsTabPage.Location = new System.Drawing.Point(4, 35);
            this.optionsTabPage.Name = "optionsTabPage";
            this.optionsTabPage.Size = new System.Drawing.Size(1084, 428);
            this.optionsTabPage.TabIndex = 2;
            this.optionsTabPage.Text = "Options";
            this.optionsTabPage.VerticalScrollbarBarColor = true;
            // 
            // selectedRam
            // 
            this.selectedRam.AutoSize = true;
            this.selectedRam.Location = new System.Drawing.Point(6, 70);
            this.selectedRam.Name = "selectedRam";
            this.selectedRam.Size = new System.Drawing.Size(61, 19);
            this.selectedRam.TabIndex = 7;
            this.selectedRam.Text = "2048 MB";
            // 
            // ramSelector
            // 
            this.ramSelector.BackColor = System.Drawing.Color.Transparent;
            this.ramSelector.LargeChange = 1024;
            this.ramSelector.Location = new System.Drawing.Point(6, 44);
            this.ramSelector.Maximum = 8192;
            this.ramSelector.Minimum = 1024;
            this.ramSelector.Name = "ramSelector";
            this.ramSelector.Size = new System.Drawing.Size(360, 23);
            this.ramSelector.SmallChange = 512;
            this.ramSelector.TabIndex = 6;
            this.ramSelector.Text = "metroTrackBar1";
            this.ramSelector.Value = 2048;
            this.ramSelector.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ramSelector_Scroll);
            this.ramSelector.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ramSelector_MouseUp);
            // 
            // themeSelector
            // 
            this.themeSelector.FormattingEnabled = true;
            this.themeSelector.ItemHeight = 23;
            this.themeSelector.Location = new System.Drawing.Point(58, 38);
            this.themeSelector.Name = "themeSelector";
            this.themeSelector.Size = new System.Drawing.Size(121, 29);
            this.themeSelector.TabIndex = 5;
            this.themeSelector.SelectedIndexChanged += new System.EventHandler(this.themeSelector_SelectedIndexChanged);
            // 
            // themeLabel
            // 
            this.themeLabel.AutoSize = true;
            this.themeLabel.Location = new System.Drawing.Point(3, 41);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(49, 19);
            this.themeLabel.TabIndex = 4;
            this.themeLabel.Text = "Theme";
            // 
            // styleLabel
            // 
            this.styleLabel.AutoSize = true;
            this.styleLabel.Location = new System.Drawing.Point(3, 9);
            this.styleLabel.Name = "styleLabel";
            this.styleLabel.Size = new System.Drawing.Size(36, 19);
            this.styleLabel.TabIndex = 3;
            this.styleLabel.Text = "Style";
            // 
            // styleSelector
            // 
            this.styleSelector.FormattingEnabled = true;
            this.styleSelector.ItemHeight = 23;
            this.styleSelector.Location = new System.Drawing.Point(58, 3);
            this.styleSelector.Name = "styleSelector";
            this.styleSelector.Size = new System.Drawing.Size(121, 29);
            this.styleSelector.TabIndex = 2;
            this.styleSelector.SelectedIndexChanged += new System.EventHandler(this.styleSelector_SelectedIndexChanged);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(745, 34);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 1;
            this.metroButton1.Text = "metroButton1";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(1040, 536);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Login";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // profileBox
            // 
            this.profileBox.ContextMenuStrip = this.profileContextMenu;
            this.profileBox.FormattingEnabled = true;
            this.profileBox.ItemHeight = 23;
            this.profileBox.Location = new System.Drawing.Point(857, 532);
            this.profileBox.Name = "profileBox";
            this.profileBox.Size = new System.Drawing.Size(177, 29);
            this.profileBox.TabIndex = 3;
            this.profileBox.SelectedIndexChanged += new System.EventHandler(this.profileBox_SelectedIndexChanged);
            // 
            // profileContextMenu
            // 
            this.profileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.profileContextMenu.Name = "profileContextMenu";
            this.profileContextMenu.Size = new System.Drawing.Size(141, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem1.Text = "deleteProfile";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // addProfile
            // 
            this.addProfile.Location = new System.Drawing.Point(776, 536);
            this.addProfile.Name = "addProfile";
            this.addProfile.Size = new System.Drawing.Size(75, 23);
            this.addProfile.TabIndex = 4;
            this.addProfile.Text = "Add Profile";
            this.addProfile.Click += new System.EventHandler(this.addProfile_Click);
            // 
            // gameSettings
            // 
            this.gameSettings.BackColor = System.Drawing.Color.Transparent;
            this.gameSettings.Controls.Add(this.changeInstallPath);
            this.gameSettings.Controls.Add(this.installPathTextBox);
            this.gameSettings.Controls.Add(this.installPathDescripton);
            this.gameSettings.Controls.Add(this.changeJavaPath);
            this.gameSettings.Controls.Add(this.javaPathTextBox);
            this.gameSettings.Controls.Add(this.javaPathDescription);
            this.gameSettings.Controls.Add(this.extraDescription);
            this.gameSettings.Controls.Add(this.extraJavaParameters);
            this.gameSettings.Controls.Add(this.ramDescription);
            this.gameSettings.Controls.Add(this.ramSelector);
            this.gameSettings.Controls.Add(this.selectedRam);
            this.gameSettings.Location = new System.Drawing.Point(249, 3);
            this.gameSettings.Name = "gameSettings";
            this.gameSettings.Size = new System.Drawing.Size(372, 422);
            this.gameSettings.TabIndex = 9;
            this.gameSettings.TabStop = false;
            this.gameSettings.Text = "Game Settings";
            // 
            // ramDescription
            // 
            this.ramDescription.AutoSize = true;
            this.ramDescription.Location = new System.Drawing.Point(7, 20);
            this.ramDescription.Name = "ramDescription";
            this.ramDescription.Size = new System.Drawing.Size(36, 19);
            this.ramDescription.TabIndex = 8;
            this.ramDescription.Text = "Ram";
            // 
            // extraJavaParameters
            // 
            this.extraJavaParameters.Location = new System.Drawing.Point(7, 137);
            this.extraJavaParameters.Name = "extraJavaParameters";
            this.extraJavaParameters.Size = new System.Drawing.Size(359, 23);
            this.extraJavaParameters.TabIndex = 9;
            this.extraJavaParameters.TextChanged += new System.EventHandler(this.extraJavaParameters_TextChanged);
            // 
            // extraDescription
            // 
            this.extraDescription.AutoSize = true;
            this.extraDescription.Location = new System.Drawing.Point(7, 112);
            this.extraDescription.Name = "extraDescription";
            this.extraDescription.Size = new System.Drawing.Size(137, 19);
            this.extraDescription.TabIndex = 10;
            this.extraDescription.Text = "Extra Java Parameters";
            // 
            // javaPathDescription
            // 
            this.javaPathDescription.AutoSize = true;
            this.javaPathDescription.Location = new System.Drawing.Point(7, 183);
            this.javaPathDescription.Name = "javaPathDescription";
            this.javaPathDescription.Size = new System.Drawing.Size(63, 19);
            this.javaPathDescription.TabIndex = 11;
            this.javaPathDescription.Text = "Java Path";
            // 
            // javaPathTextBox
            // 
            this.javaPathTextBox.Location = new System.Drawing.Point(7, 206);
            this.javaPathTextBox.Name = "javaPathTextBox";
            this.javaPathTextBox.ReadOnly = true;
            this.javaPathTextBox.Size = new System.Drawing.Size(300, 23);
            this.javaPathTextBox.TabIndex = 12;
            // 
            // changeJavaPath
            // 
            this.changeJavaPath.Location = new System.Drawing.Point(313, 206);
            this.changeJavaPath.Name = "changeJavaPath";
            this.changeJavaPath.Size = new System.Drawing.Size(53, 23);
            this.changeJavaPath.TabIndex = 13;
            this.changeJavaPath.Text = "Change";
            this.changeJavaPath.Click += new System.EventHandler(this.changeJavaPath_Click);
            // 
            // installPathDescripton
            // 
            this.installPathDescripton.AutoSize = true;
            this.installPathDescripton.Location = new System.Drawing.Point(7, 250);
            this.installPathDescripton.Name = "installPathDescripton";
            this.installPathDescripton.Size = new System.Drawing.Size(70, 19);
            this.installPathDescripton.TabIndex = 14;
            this.installPathDescripton.Text = "Install Path";
            // 
            // installPathTextBox
            // 
            this.installPathTextBox.Location = new System.Drawing.Point(7, 273);
            this.installPathTextBox.Name = "installPathTextBox";
            this.installPathTextBox.ReadOnly = true;
            this.installPathTextBox.Size = new System.Drawing.Size(300, 23);
            this.installPathTextBox.TabIndex = 15;
            // 
            // changeInstallPath
            // 
            this.changeInstallPath.Location = new System.Drawing.Point(313, 273);
            this.changeInstallPath.Name = "changeInstallPath";
            this.changeInstallPath.Size = new System.Drawing.Size(53, 23);
            this.changeInstallPath.TabIndex = 16;
            this.changeInstallPath.Text = "Change";
            this.changeInstallPath.Click += new System.EventHandler(this.changeInstallPath_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1138, 575);
            this.Controls.Add(this.addProfile);
            this.Controls.Add(this.profileBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainWindow";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.DropShadow;
            this.Text = "MechZone ModPack Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainWindow_FormClosing);
            this.Load += new System.EventHandler(this.mainWindow_Load);
            this.Shown += new System.EventHandler(this.mainWindow_Shown);
            this.mainTabControl.ResumeLayout(false);
            this.infoTabPage.ResumeLayout(false);
            this.modPacksTabPage.ResumeLayout(false);
            this.logTabPage.ResumeLayout(false);
            this.optionsTabPage.ResumeLayout(false);
            this.optionsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.profileContextMenu.ResumeLayout(false);
            this.gameSettings.ResumeLayout(false);
            this.gameSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl mainTabControl;
        private MetroFramework.Controls.MetroTabPage infoTabPage;
        private System.Windows.Forms.WebBrowser infoWebBrowser;
        private MetroFramework.Controls.MetroTabPage modPacksTabPage;
        private MetroFramework.Controls.MetroPanel modPacksContainer;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroTabPage optionsTabPage;
        private MetroFramework.Controls.MetroLabel styleLabel;
        private MetroFramework.Controls.MetroComboBox styleSelector;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MetroFramework.Controls.MetroComboBox themeSelector;
        private MetroFramework.Controls.MetroLabel themeLabel;
        private MetroFramework.Controls.MetroButton addProfile;
        private MetroFramework.Controls.MetroComboBox profileBox;
        private MetroFramework.Controls.MetroButton loginButton;
        private MetroFramework.Controls.MetroTabPage logTabPage;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.ContextMenuStrip profileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private MetroFramework.Controls.MetroTrackBar ramSelector;
        private MetroFramework.Controls.MetroLabel selectedRam;
        private System.Windows.Forms.GroupBox gameSettings;
        private MetroFramework.Controls.MetroLabel ramDescription;
        private MetroFramework.Controls.MetroLabel extraDescription;
        private MetroFramework.Controls.MetroTextBox extraJavaParameters;
        private MetroFramework.Controls.MetroButton changeJavaPath;
        private MetroFramework.Controls.MetroTextBox javaPathTextBox;
        private MetroFramework.Controls.MetroLabel javaPathDescription;
        private MetroFramework.Controls.MetroButton changeInstallPath;
        private MetroFramework.Controls.MetroTextBox installPathTextBox;
        private MetroFramework.Controls.MetroLabel installPathDescripton;
    }
}

