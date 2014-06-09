using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class DownloadForm : MetroForm
    {
        MetroForm SenderForm;

        public DownloadForm()
        {
            InitializeComponent();
        }

        public MetroForm senderForm
        {
            set { SenderForm = value; Invalidate(); }
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            if (SenderForm.Theme == MetroThemeStyle.Dark)
            {
                Theme = MetroThemeStyle.Light;
            }
            else
            {
                Theme = MetroThemeStyle.Dark;
            }
            Style = SenderForm.Style;
        }
    }
}
