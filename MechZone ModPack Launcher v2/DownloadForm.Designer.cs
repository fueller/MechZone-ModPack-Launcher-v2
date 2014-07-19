namespace MechZone_ModPack_Launcher_v2
{
    partial class DownloadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.allFileProgress = new MetroFramework.Controls.MetroProgressBar();
            this.singleFileProgress = new MetroFramework.Controls.MetroProgressBar();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.downloadSpeed = new MetroFramework.Controls.MetroLabel();
            this.labelDownload = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(385, 150);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            // 
            // allFileProgress
            // 
            this.allFileProgress.Location = new System.Drawing.Point(23, 63);
            this.allFileProgress.Name = "allFileProgress";
            this.allFileProgress.Size = new System.Drawing.Size(437, 23);
            this.allFileProgress.Step = 1;
            this.allFileProgress.TabIndex = 1;
            // 
            // singleFileProgress
            // 
            this.singleFileProgress.Location = new System.Drawing.Point(24, 93);
            this.singleFileProgress.Name = "singleFileProgress";
            this.singleFileProgress.Size = new System.Drawing.Size(436, 23);
            this.singleFileProgress.TabIndex = 2;
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 123);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(0, 0);
            this.metroLabel1.TabIndex = 3;
            // 
            // downloadSpeed
            // 
            this.downloadSpeed.AutoSize = true;
            this.downloadSpeed.Location = new System.Drawing.Point(24, 146);
            this.downloadSpeed.Name = "downloadSpeed";
            this.downloadSpeed.Size = new System.Drawing.Size(0, 0);
            this.downloadSpeed.TabIndex = 4;
            // 
            // labelDownload
            // 
            this.labelDownload.AutoSize = true;
            this.labelDownload.Location = new System.Drawing.Point(185, 145);
            this.labelDownload.Name = "labelDownload";
            this.labelDownload.Size = new System.Drawing.Size(0, 0);
            this.labelDownload.TabIndex = 5;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(483, 196);
            this.Controls.Add(this.labelDownload);
            this.Controls.Add(this.downloadSpeed);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.singleFileProgress);
            this.Controls.Add(this.allFileProgress);
            this.Controls.Add(this.cancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownloadForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.DropShadow;
            this.Text = "Downloading";
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.Shown += new System.EventHandler(this.DownloadForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton cancelButton;
        private MetroFramework.Controls.MetroProgressBar allFileProgress;
        private MetroFramework.Controls.MetroProgressBar singleFileProgress;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MetroFramework.Controls.MetroLabel labelDownload;
        private MetroFramework.Controls.MetroLabel downloadSpeed;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}