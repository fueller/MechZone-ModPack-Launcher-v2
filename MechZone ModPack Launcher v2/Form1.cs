using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Fonts;
using MetroFramework.Forms;
using Newtonsoft.Json;
using System.Net;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class mainWindow : MetroForm
    {
        jsonClasses.JCmodpacks aviableModpacks = new jsonClasses.JCmodpacks();
        Dictionary<string, jsonClasses.JCmodpackInfo> modPackInfos = new Dictionary<string, jsonClasses.JCmodpackInfo>();
        jsonClasses.JCassets MCassets = new jsonClasses.JCassets();
        string appdata = "";
        
        string solderApiUrl = @"http://solder.mechzone.net/index.php/api/";
        string location = "%appdata%/.mechzoneV2";

        public mainWindow()
        {
            InitializeComponent();
            Icon = Properties.Resources.taskbarIcon;
            string var = Environment.GetEnvironmentVariable("appdata");
            appdata = var;
            location = appdata + "/.mechzoneV2";

            foreach (MetroColorStyle color in (MetroColorStyle[]) Enum.GetValues(typeof(MetroColorStyle)))
            {
                styleSelector.Items.Add(color);
            }

            foreach (MetroThemeStyle color in (MetroThemeStyle[])Enum.GetValues(typeof(MetroThemeStyle)))
            {
                themeSelector.Items.Add(color);
            }

            themeSelector.SelectedIndex = Properties.Settings.Default.selectedTheme;
            styleSelector.SelectedIndex = Properties.Settings.Default.selectedStyle;

        }

        private void mainWindow_Load(object sender, EventArgs e)
        {            
            getModPacks();
            getModPackInfos();
            listModPacks();
            getAssets("1.7.");
        }

        void getModPacks()
        {
            String temp = getStringFromUrl(solderApiUrl + "modpack");
            aviableModpacks = JsonConvert.DeserializeObject<jsonClasses.JCmodpacks>(temp);
        }

        void getModPackInfos()
        {
            foreach (string modpack in aviableModpacks.modpacks.Keys)
            {
                modPackInfos[modpack] = JsonConvert.DeserializeObject<jsonClasses.JCmodpackInfo>(getStringFromUrl(solderApiUrl + "modpack/" + modpack));
            }
        }

        void listModPacks()
        {
            int i = 0;
            foreach (string modpack in aviableModpacks.modpacks.Keys)
            {
                ModPack mp = new ModPack();
                mp.Location = new Point(5, 5 + (150 * i));
                mp.modPackDescription = modPackInfos[modpack].description;
                mp.modPackName = modPackInfos[modpack].display_name;
                mp.modPackImage = modPackInfos[modpack].icon;
                modPacksContainer.Controls.Add(mp);
                i++;
            }
            
        }

        string getStringFromUrl(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            String erg = client.DownloadString(url);
            return erg;
        }

        void getAssets(string version)
        {
            if (version == "1.7.2")
            {
                String temp = getStringFromUrl(@"https://s3.amazonaws.com/Minecraft.Download/indexes/legacy.json");
                MCassets = JsonConvert.DeserializeObject<jsonClasses.JCassets>(temp);

                foreach (string key in MCassets.objects.Keys)
                {
                    Console.WriteLine(key);
                    Console.WriteLine(MCassets.objects[key].hash);
                    Console.WriteLine(MCassets.objects[key].size);
                }
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            DownloadForm df = new DownloadForm();
            df.senderForm = this;
            df.ShowDialog();
        }

        private void styleSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            StyleManager.Style = (MetroColorStyle) styleSelector.SelectedItem;
            Properties.Settings.Default.selectedStyle = styleSelector.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void themeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            StyleManager.Theme = (MetroThemeStyle)themeSelector.SelectedItem;
            Properties.Settings.Default.selectedTheme = themeSelector.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void metroUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
