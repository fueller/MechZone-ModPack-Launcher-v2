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
            this.optionsTabPage = new MetroFramework.Controls.MetroTabPage();
            this.themeSelector = new MetroFramework.Controls.MetroComboBox();
            this.themeLabel = new MetroFramework.Controls.MetroLabel();
            this.styleLabel = new MetroFramework.Controls.MetroLabel();
            this.styleSelector = new MetroFramework.Controls.MetroComboBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.loginButton = new MetroFramework.Controls.MetroButton();
            this.profileBox = new MetroFramework.Controls.MetroComboBox();
            this.addProfile = new MetroFramework.Controls.MetroButton();
            this.mainTabControl.SuspendLayout();
            this.infoTabPage.SuspendLayout();
            this.modPacksTabPage.SuspendLayout();
            this.optionsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.infoTabPage);
            this.mainTabControl.Controls.Add(this.modPacksTabPage);
            this.mainTabControl.Controls.Add(this.optionsTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(23, 63);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
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
            // optionsTabPage
            // 
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
            this.profileBox.FormattingEnabled = true;
            this.profileBox.ItemHeight = 23;
            this.profileBox.Location = new System.Drawing.Point(857, 532);
            this.profileBox.Name = "profileBox";
            this.profileBox.Size = new System.Drawing.Size(177, 29);
            this.profileBox.TabIndex = 3;
            this.profileBox.SelectedIndexChanged += new System.EventHandler(this.profileBox_SelectedIndexChanged);
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
            this.optionsTabPage.ResumeLayout(false);
            this.optionsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
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
    }
}

