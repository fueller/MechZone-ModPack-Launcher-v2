using System;
using MetroFramework.Controls;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class ModPack : MetroUserControl
    {
        jsonClasses.JCmodpackInfo _packInfo;
        public ModPack()
        {
            InitializeComponent();
        }

        public string ModPackImage
        {
            private get { return modInfoImage.ImageLocation; }
            set { modInfoImage.ImageLocation = value; Invalidate(); }
        }

        public string ModPackDescription
        {
            private get { return modInfoDescription.Text; }
            set { modInfoDescription.Text = value; Invalidate(); }
        }

        public string ModPackName
        {
            private get { return modInfoName.Text; }
            set { modInfoName.Text = value; Invalidate(); }
        }

        public jsonClasses.JCmodpackInfo ModPackInfo
        {
            get { return _packInfo; }
            set { _packInfo = value; Invalidate(); }
        }

        private void ModPack_Load(object sender, EventArgs e)
        {
            modInfoName.Text = ModPackName;
            modInfoDescription.Text = ModPackDescription;
            modInfoImage.ImageLocation = ModPackImage;
        }
    }
}
