using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class ModPack : MetroUserControl
    {
        jsonClasses.JCmodpackInfo packInfo;
        public ModPack()
        {
            InitializeComponent();
        }

        public string modPackImage
        {
            get { return modInfoImage.ImageLocation; }
            set { modInfoImage.ImageLocation = value; Invalidate(); }
        }

        public string modPackDescription
        {
            get { return modInfoDescription.Text; }
            set { modInfoDescription.Text = value; Invalidate(); }
        }

        public string modPackName
        {
            get { return modInfoName.Text; }
            set { modInfoName.Text = value; Invalidate(); }
        }

        public jsonClasses.JCmodpackInfo modPackInfo
        {
            get { return packInfo; }
            set { packInfo = modPackInfo; Invalidate(); }
        }

        private void ModPack_Load(object sender, EventArgs e)
        {
            modInfoName.Text = modPackName;
            modInfoDescription.Text = modPackDescription;
            modInfoImage.ImageLocation = modPackImage;
        }
    }
}
