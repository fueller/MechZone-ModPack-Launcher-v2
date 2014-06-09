namespace MechZone_ModPack_Launcher_v2
{
    partial class ModPack
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.modInfoImage = new System.Windows.Forms.PictureBox();
            this.modInfoName = new MetroFramework.Controls.MetroLabel();
            this.modInfoDescription = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.modInfoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // modInfoImage
            // 
            this.modInfoImage.Location = new System.Drawing.Point(3, 3);
            this.modInfoImage.Name = "modInfoImage";
            this.modInfoImage.Size = new System.Drawing.Size(144, 144);
            this.modInfoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modInfoImage.TabIndex = 0;
            this.modInfoImage.TabStop = false;
            // 
            // modInfoName
            // 
            this.modInfoName.AutoSize = true;
            this.modInfoName.Location = new System.Drawing.Point(154, 4);
            this.modInfoName.Name = "modInfoName";
            this.modInfoName.Size = new System.Drawing.Size(81, 19);
            this.modInfoName.TabIndex = 1;
            this.modInfoName.Text = "metroLabel1";
            // 
            // modInfoDescription
            // 
            this.modInfoDescription.AutoSize = true;
            this.modInfoDescription.Location = new System.Drawing.Point(154, 27);
            this.modInfoDescription.Name = "modInfoDescription";
            this.modInfoDescription.Size = new System.Drawing.Size(83, 19);
            this.modInfoDescription.TabIndex = 2;
            this.modInfoDescription.Text = "metroLabel2";
            // 
            // ModPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.Controls.Add(this.modInfoDescription);
            this.Controls.Add(this.modInfoName);
            this.Controls.Add(this.modInfoImage);
            this.Name = "ModPack";
            this.Size = new System.Drawing.Size(488, 148);
            this.Load += new System.EventHandler(this.ModPack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.modInfoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox modInfoImage;
        private MetroFramework.Controls.MetroLabel modInfoName;
        private MetroFramework.Controls.MetroLabel modInfoDescription;
    }
}
