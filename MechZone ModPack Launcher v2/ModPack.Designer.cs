﻿namespace MechZone_ModPack_Launcher_v2
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
            this.components = new System.ComponentModel.Container();
            this.modInfoImage = new System.Windows.Forms.PictureBox();
            this.modInfoName = new MetroFramework.Controls.MetroLabel();
            this.modInfoDescription = new MetroFramework.Controls.MetroLabel();
            this.StyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.modInfoImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // modInfoImage
            // 
            this.modInfoImage.BackColor = System.Drawing.Color.Transparent;
            this.modInfoImage.Location = new System.Drawing.Point(3, 3);
            this.modInfoImage.Name = "modInfoImage";
            this.modInfoImage.Size = new System.Drawing.Size(144, 144);
            this.modInfoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modInfoImage.TabIndex = 0;
            this.modInfoImage.TabStop = false;
            // 
            // modInfoName
            // 
            this.modInfoName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.modInfoName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.modInfoName.Location = new System.Drawing.Point(154, 4);
            this.modInfoName.Name = "modInfoName";
            this.modInfoName.Size = new System.Drawing.Size(326, 25);
            this.modInfoName.TabIndex = 1;
            this.modInfoName.Text = "metroLabel1";
            this.modInfoName.UseStyleColors = true;
            // 
            // modInfoDescription
            // 
            this.modInfoDescription.Location = new System.Drawing.Point(154, 33);
            this.modInfoDescription.Name = "modInfoDescription";
            this.modInfoDescription.Size = new System.Drawing.Size(326, 102);
            this.modInfoDescription.TabIndex = 2;
            this.modInfoDescription.Text = "metroLabel2";
            // 
            // StyleManager
            // 
            this.StyleManager.Owner = this;
            // 
            // ModPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.Controls.Add(this.modInfoDescription);
            this.Controls.Add(this.modInfoName);
            this.Controls.Add(this.modInfoImage);
            this.Name = "ModPack";
            this.Size = new System.Drawing.Size(488, 148);
            this.Load += new System.EventHandler(this.ModPack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.modInfoImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.PictureBox modInfoImage;
        public MetroFramework.Controls.MetroLabel modInfoName;
        public MetroFramework.Controls.MetroLabel modInfoDescription;
        //public MetroFramework.Components.MetroStyleManager StyleManager;
    }
}
