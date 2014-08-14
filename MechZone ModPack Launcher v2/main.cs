using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MechZone_ModPack_Launcher_v2.Properties;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Deployment.Application;
using System.Reflection;
using System.Diagnostics;
using MechZone_ModPack_Launcher_v2.jsonClasses;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class MainWindow : MetroForm
    {
        JCmodpacks _aviableModpacks = new JCmodpacks();
        readonly Dictionary<string, JCmodpackInfo> _modPackInfos = new Dictionary<string, JCmodpackInfo>();
        private const string SolderUrl = @"http://solder.mechzone.net/";
        readonly string _solderApiUrl;
        string _location = "%appdata%/.mechzoneV2";
        readonly Guid _uuid;
        //Dictionary<string, JCdownloadList> downloadList = new Dictionary<string, JCdownloadList>();
        readonly List<JCdownloadList> _downloadList = new List<JCdownloadList>();
        JCmodpackInfo _selectedModpack = new JCmodpackInfo();
        private bool _changingUrl;


        public MainWindow()
        {
            try
            {

                InitializeComponent();

                mainTabControl.SelectedIndex = 0;
                string appdata = Environment.GetEnvironmentVariable("appdata");
                _location = appdata + "\\.mechzoneV2";
                _solderApiUrl = SolderUrl + @"index.php/api/";
                if (!Directory.Exists(_location))
                {
                    Directory.CreateDirectory(_location);
                }

                if (!File.Exists(_location + @"\mz_launcher_profiles.json"))
                {
                    JCprofileSave text = new JCprofileSave();
                    Guid g = Guid.NewGuid();
                    text.clientToken = g;
                    text.profiles = new Dictionary<string, profileInfo>();
                    text.authenticationDatabase = new Dictionary<string, userInfo>();
                    File.WriteAllText(_location + @"\mz_launcher_profiles.json", JsonConvert.SerializeObject(text, Formatting.Indented));
                }

                JCprofileSave uuidRead = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(_location + @"\mz_launcher_profiles.json"));
                _uuid = uuidRead.clientToken;
                Icon = Resources.taskbarIcon;
                Text = string.Format("{0} {1}", Text, (ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Assembly.GetExecutingAssembly().GetName().Version.ToString()));

                metroButton1.Visible = Debugger.IsAttached;

                foreach (MetroColorStyle color in (MetroColorStyle[])Enum.GetValues(typeof(MetroColorStyle)))
                {
                    styleSelector.Items.Add(color);
                }

                foreach (MetroThemeStyle color in (MetroThemeStyle[])Enum.GetValues(typeof(MetroThemeStyle)))
                {
                    themeSelector.Items.Add(color);
                }

                JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(_location + @"\mz_launcher_profiles.json"));
                profileBox.Items.Clear();
                foreach (string profile in profiles.profiles.Keys)
                {
                    profileBox.Items.Add(profile);

                }

                try
                {
                    profileBox.SelectedIndex = Settings.Default.selectedProfile;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                }

                try
                {
                    themeSelector.SelectedIndex = Settings.Default.selectedTheme;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                }

                try
                {
                    styleSelector.SelectedIndex = Settings.Default.selectedStyle;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                }

                ramSelector.Value = Settings.Default.ram;
                selectedRam.Text = string.Format("{0} GB ({1} MB)", ramSelector.Value / 1024, ramSelector.Value);

                extraJavaParameters.Text = Settings.Default.javaParameters;

                if (String.IsNullOrEmpty(Settings.Default.javaPath))
                {
                    Settings.Default.javaPath = getJavaInstallationPath();
                    Settings.Default.Save();
                    javaPathTextBox.Text = Settings.Default.javaPath;
                }
                else
                {
                    javaPathTextBox.Text = Settings.Default.javaPath;
                }

                if (String.IsNullOrEmpty(Settings.Default.installPath))
                {
                    Settings.Default.installPath = _location;
                    Settings.Default.Save();
                    installPathTextBox.Text = Settings.Default.installPath;
                }
                else
                {
                    installPathTextBox.Text = Settings.Default.installPath;
                    _location = Settings.Default.installPath;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            GetModPacks();
            GetModPackInfos();
            ListModPacks();
        }

        void GetModPacks()
        {
            try
            {
                String temp = getStringFromUrl(_solderApiUrl + "modpack");
                _aviableModpacks = JsonConvert.DeserializeObject<JCmodpacks>(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        void GetModPackInfos()
        {
            try
            {
                foreach (string modpack in _aviableModpacks.modpacks.Keys)
                {
                    _modPackInfos[modpack] = JsonConvert.DeserializeObject<JCmodpackInfo>(getStringFromUrl(_solderApiUrl + "modpack/" + modpack));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        void ListModPacks()
        {
            try
            {
                int i = 0;
                foreach (string modpack in _aviableModpacks.modpacks.Keys)
                {
                    ModPack mp = new ModPack
                    {
                        Location = new Point(5, 5 + (150*i)),
                        ModPackDescription = _modPackInfos[modpack].description,
                        ModPackName = _modPackInfos[modpack].display_name,
                        ModPackImage = _modPackInfos[modpack].icon
                    };
                    mp.Click += ModpackSelected;
                    mp.modInfoImage.Click += ModpackSelected;
                    mp.modInfoDescription.Click += ModpackSelected;
                    mp.modInfoName.Click += ModpackSelected;
                    mp.ModPackInfo = _modPackInfos[modpack];
                    modPacksContainer.Controls.Add(mp);
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }

        }

        private void ModpackSelected(object sender, EventArgs e)
        {
            try
            {
                ModPack mp = null;
                if (sender.GetType() == typeof(ModPack))
                {
                    mp = (ModPack)sender;
                }
                else if (sender.GetType() == typeof(MetroLabel))
                {
                    MetroLabel label = (MetroLabel)sender;
                    if (label.Parent.GetType() == typeof(ModPack))
                    {
                        mp = (ModPack)label.Parent;
                    }
                    else
                    {
                        throw new Exception("Selected ModPack Error");
                    }
                }
                else if (sender.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = (PictureBox)sender;
                    if (pb.Parent.GetType() == typeof(ModPack))
                    {
                        mp = (ModPack)pb.Parent;
                    }
                    else
                    {
                        throw new Exception("Selected ModPack Error");
                    }
                }

                if (mp != null) ChangeSelectedModPack(mp.ModPackInfo.name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void ChangeSelectedModPack(string smp)
        {
            try
            {
                for (int i = 0; i < modPacksContainer.Controls.Count; i++)
                {
                    if (modPacksContainer.Controls[i].GetType() == typeof(ModPack))
                    {
                        ModPack mp = (ModPack)modPacksContainer.Controls[i];
                        if (mp.ModPackInfo.name.Equals(smp))
                        {
                            mp.StyleManager.Theme = MetroThemeStyle.Dark;
                            Settings.Default.selectedModPack = mp.ModPackInfo.name;
                            _selectedModpack = mp.ModPackInfo;
                            Console.WriteLine("Now selected: {0}", _selectedModpack.name);
                            Settings.Default.Save();
                        }
                        else
                        {
                            mp.StyleManager.Theme = MetroThemeStyle.Light;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        string getStringFromUrl(string url)
        {
            WebClient client = new WebClient {Proxy = null};
            String erg = client.DownloadString(url);
            return erg;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            userInfo user = GetSelectedProfile();
            Refresh(user.accessToken, _uuid);
        }

        #region Refresh authenticate
        public static JCauthenticateResponse Authenticate(string username, string password, Guid uuid)
        {
            try
            {
                WebRequest.DefaultWebProxy = null;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/authenticate");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JCauthenticatePayload payload = new JCauthenticatePayload
                    {
                        agent = new Agent {name = "Minecraft", version = 1},
                        username = username,
                        password = password,
                        clientToken = uuid.ToString()
                    };
                    string json = JsonConvert.SerializeObject(payload);
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

// ReSharper disable once AssignNullToNotNullAttribute
                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    JCauthenticateResponse j = JsonConvert.DeserializeObject<JCauthenticateResponse>(response);
                    return j;

                }
            }
            catch (WebException)
            {
                MessageBox.Show(Resources.MainWindow_Authenticate_, Resources.MainWindow_Authenticate_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
        }

        private static void Invalidate(string accessToken, Guid uuid)
        {
            WebRequest.DefaultWebProxy = null;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/invalidate");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                JCrefreshPayload payload = new JCrefreshPayload
                {
                    accessToken = accessToken,
                    clientToken = uuid.ToString()
                };
                string json = JsonConvert.SerializeObject(payload);
                writer.Write(json);
                writer.Flush();
                writer.Close();
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
// ReSharper disable once AssignNullToNotNullAttribute
            using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string response = reader.ReadToEnd();
                Console.WriteLine(response);

            }
        }

        private static JCrefreshResponse Refresh(string accessToken, Guid uuid)
        {
            try
            {
                WebRequest.DefaultWebProxy = null;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/refresh");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JCrefreshPayload payload = new JCrefreshPayload
                    {
                        accessToken = accessToken,
                        clientToken = uuid.ToString()
                    };
                    string json = JsonConvert.SerializeObject(payload);
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

// ReSharper disable once AssignNullToNotNullAttribute
                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    JCrefreshResponse j = JsonConvert.DeserializeObject<JCrefreshResponse>(response);
                    return j;

                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region theme and style
        private void styleSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            StyleManager.Style = (MetroColorStyle)styleSelector.SelectedItem;
            Settings.Default.selectedStyle = styleSelector.SelectedIndex;
            Settings.Default.Save();
        }

        private void themeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            MetroThemeStyle selected = (MetroThemeStyle)themeSelector.SelectedItem;
            StyleManager.Theme = selected;
            gameSettings.ForeColor = selected == MetroThemeStyle.Dark ? Color.White : Color.Black;
            _changingUrl = true;
            Console.WriteLine(StyleManager.Theme.ToString());

            switch (StyleManager.Theme.ToString())
            {
                case "Dark":
                    infoWebBrowser.Navigate(new Uri("http://mechzone.net/modpack/launcher/info/info.php?color1=111&color2=fff"));
                    
                    break;
                case "Light":
                case "Default":
                    infoWebBrowser.Navigate(new Uri("http://mechzone.net/modpack/launcher/info/info.php?color1=fff&color2=000"));
                    break;
            }
            //Console.WriteLine("refresh");
            //infoWebBrowser.Refresh();
            Settings.Default.selectedTheme = themeSelector.SelectedIndex;
            Settings.Default.Save();
        }
        #endregion

        private void addProfile_Click(object sender, EventArgs e)
        {
            try
            {
                AddProfile form = new AddProfile
                {
                    metroStyleManager = {Style = Style, Theme = Theme},
                    SetUuid = _uuid,
                    Path = _location
                };
                DialogResult res = form.ShowDialog();
                if (res == DialogResult.OK)
                {
                    JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(_location + @"\mz_launcher_profiles.json"));
                    profileBox.Items.Clear();
                    foreach (string profile in profiles.profiles.Keys)
                    {
                        profileBox.Items.Add(profile);
                        Settings.Default.selectedProfile = 0;
                        Settings.Default.Save();
                        profileBox.SelectedIndex = Settings.Default.selectedProfile;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    Console.WriteLine(Resources.Cancel);
                }
                else if (res == DialogResult.Abort)
                {
                    MessageBox.Show(Resources.MainWindow_addProfile_Click_Es_ist_ein_Fehler_aufgetreten__bitte_versuche_es_noch_einmal_, Resources.MainWindow_Authenticate_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void profileBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.selectedProfile = profileBox.SelectedIndex;
            Settings.Default.Save();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                userInfo user = GetSelectedProfile();
                JCrefreshResponse response = Refresh(user.accessToken, _uuid);
                if (response != null)
                {
                    JCprofileSave profiles =
                        JsonConvert.DeserializeObject<JCprofileSave>(
                            File.ReadAllText(_location + @"\mz_launcher_profiles.json"));
                    profiles.authenticationDatabase[response.selectedProfile.id].accessToken = response.accessToken;
                    File.WriteAllText(_location + @"\mz_launcher_profiles.json",
                        JsonConvert.SerializeObject(profiles, Formatting.Indented));
                    LaunchModPack(user, _selectedModpack);
                }
                else
                {
                    MessageBox.Show("Error logging in, please recreate the Profile.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    toolStripMenuItem1_Click(null,null);
                    addProfile_Click(null,null);
                    MessageBox.Show("You can now try to log back in.", "Try Again", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private string _usedVersion;
        private void LaunchModPack(userInfo user, JCmodpackInfo selectedModPack)
        {
            try
            {
                
                logTextBox.Clear();
                _downloadList.Clear();
                GetModPacks();
                GetModPackInfos();

                ChangeSelectedModPack(_selectedModpack.name);
                


                JCmodpackVersion latestVersion = GetLatestModPackVersion(selectedModPack);
                _usedVersion = latestVersion.minecraft;
                Console.WriteLine("Starte Modpack: \"{0}\" V:\"{1}\" Benutzer: \"{2}\"", _selectedModpack.display_name, _selectedModpack.latest, user.displayName);

                userInfo profile = GetSelectedProfile();
                GetMinecraft(_selectedModpack, latestVersion.minecraft);
                GetAssetsForVersion(latestVersion.minecraft);
                GetLibrariesForVersion(_selectedModpack, latestVersion.minecraft, latestVersion.forgeVersion);
                GetNatives(latestVersion.forgeVersion, latestVersion.minecraft);
                GetMods(latestVersion);

                string fileLoc = _location + "\\modpacks\\" + _selectedModpack.name + "\\version.json";

                bool firstRun = false;

                if (!File.Exists(fileLoc))
                {
                    string directory = Path.GetDirectoryName(fileLoc);
                    if (directory != null) Directory.CreateDirectory(directory);
                    File.WriteAllText(fileLoc, JsonConvert.SerializeObject(new JCversion()));
                    firstRun = true;
                }

                string latestVersionString = _selectedModpack.latest;
                string localVersionString = JsonConvert.DeserializeObject<JCversion>(File.ReadAllText(fileLoc)).version;


                if (!latestVersionString.Equals(localVersionString))
                {
                    if (!firstRun)
                    {
                        DialogResult result = MessageBox.Show(Resources.MainWindow_LaunchModPack_, Resources.MainWindow_LaunchModPack_New_Update_Available, MessageBoxButtons.YesNo, MessageBoxIcon.None);
                        switch (result)
                        {
                            case DialogResult.Yes:
                            {
                                DownloadForm dlf = new DownloadForm
                                {
                                    SenderForm = this,
                                    DownloadList = _downloadList,
                                    SaveLocation = _location,
                                    Modpack = _selectedModpack
                                };
                                dlf.ShowDialog();
                            }
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                    else
                    {
                        DownloadForm dlf = new DownloadForm
                        {
                            SenderForm = this,
                            DownloadList = _downloadList,
                            SaveLocation = _location,
                            Modpack = _selectedModpack
                        };
                        dlf.ShowDialog();
                    }
                }

                ProcessStartInfo start = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    FileName = getJavaInstallationPath()
                };

                string workingDirectory = _location + "\\modpacks\\" + _selectedModpack.name;

                start.WorkingDirectory = workingDirectory;

                string arguments = "";
                arguments += "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump ";
                //arguments += "-Xmx4096m ";
                arguments += "-Xmx" + Settings.Default.ram + "m ";
                //arguments += "-XX:MaxPermSize=256m ";
                arguments += Settings.Default.javaParameters + " ";
                arguments += @"-Djava.library.path=" + _location + @"\modpacks\" + _selectedModpack.name + @"\bin\" + latestVersion.minecraft + "-" + latestVersion.forgeVersion + "-natives ";
                //arguments += "-Dminecraft.applet.TargetDirectory=" + workingDirectory + " ";
                arguments += "-cp ";
                arguments = _downloadList.Where(t => t.type.Equals("libraries")).Aggregate(arguments, (current, t) => current + (t.saveLocations[0] + ";"));
                arguments += _location + @"\modpacks\" + _selectedModpack.name + "\\bin\\minecraft.jar net.minecraft.launchwrapper.Launch ";
                switch (latestVersion.minecraft)
                {
                    
                    case "1.6.4":
                        arguments += "--username " + profile.displayName + " ";
                        arguments += "--session token:" + profile.accessToken + ":" + profile.uuid + " ";
                        arguments += "--version 1.6.4 ";
                        arguments += "--gameDir " + workingDirectory + " ";
                        arguments += "--assetsDir " + _location + @"\assets\virtual\legacy ";
                        arguments += "--assetIndex 1.6.4 ";
                        arguments += "--tweakClass cpw.mods.fml.common.launcher.FMLTweaker";
                        break;
                    default:
                        arguments += "--username " + profile.displayName + " ";
                        arguments += "--version 1.7.10 ";
                        arguments += "--gameDir " + workingDirectory + " ";
                        arguments += "--assetsDir " + _location + @"\assets ";
                        arguments += "--assetIndex 1.7.10 ";
                        arguments += "--uuid " + profile.uuid + " ";
                        arguments += "--accessToken " + profile.accessToken + " ";
                        arguments += "--userProperties {} ";
                        arguments += "--userType mojang ";
                        arguments += "--tweakClass cpw.mods.fml.common.launcher.FMLTweaker";
                        break;
                }


                Console.WriteLine(arguments);
                start.Arguments = arguments;

                mainTabControl.SelectedIndex = mainTabControl.TabPages.Count - 1;

                using (Process process = Process.Start(start))
                {
                    if (process == null) return;
                    process.OutputDataReceived += Minecraft_OutputDataReceived;
                    process.ErrorDataReceived += Minecraft_ErrorDataReceived;
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void Minecraft_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                AddLogText(e.Data, "error", _usedVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void Minecraft_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                AddLogText(e.Data, "output", _usedVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        delegate void LogTextAdd(string text, string type, string version);

        void AddLogText(string text, string type, string version)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new LogTextAdd(AddLogText), new object[] { text, type , version});
                return;
            }

            if (text == null)
            {
                return;
            }

            if ((version != "1.6.4" && type.Equals("error")) || text.Contains("Warning") || text.Contains("WARN") || text.Contains("ERROR") || text.Contains("Exception"))
            {
                logTextBox.AppendText(text + "\n", Color.Red);
            }
            else if (text.Contains("CHAT"))
            {
                logTextBox.AppendText(text + "\n", Color.Green);
            }
            else if (text.Contains("INFO"))
            {
                logTextBox.AppendText(text + "\n", Color.Blue);
            }
            else
            {
                logTextBox.AppendText(text + "\n");
            }
            logTextBox.Focus();
        }

        private void GetNatives(string forgeVersion, string minecraft)
        {
            try
            {
                JCdownloadList data = new JCdownloadList
                {
                    link = "http://solder.mechzone.net/natives/" + minecraft + "-" + forgeVersion + "-natives.zip"
                };
                Console.WriteLine("natives:{0}", data.link);
                data.hash = getStringFromUrl("http://solder.mechzone.net/natives/" + minecraft + "-" + forgeVersion + "-natives.zip.sha1");
                data.hashType = "sha1";
                data.type = "natives";
                data.saveLocations = new List<string>
                {
                    _location + "\\temp\\" + "natives-" + minecraft + "-" + forgeVersion + ".zip"
                };
                _downloadList.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private JCmodpackVersion GetLatestModPackVersion(JCmodpackInfo modPack)
        {
            string url = _solderApiUrl + "modpack/" + modPack.name;
            string data = getStringFromUrl(url);
            JCmodpackInfo json = JsonConvert.DeserializeObject<JCmodpackInfo>(data);
            _selectedModpack = json;
            url = _solderApiUrl + "modpack/" + json.name + "/" + json.latest;
            data = getStringFromUrl(url);
            JCmodpackVersion json2 = JsonConvert.DeserializeObject<JCmodpackVersion>(data);
            return json2;
        }

        private userInfo GetSelectedProfile()
        {
            try
            {
                JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(_location + @"\mz_launcher_profiles.json"));
                string selectedKey = null;
                foreach (string key in profiles.profiles.Keys)
                {
                    if (profiles.profiles[key].name.Equals(profileBox.SelectedItem.ToString()))
                    {
                        selectedKey = profiles.profiles[key].playerUUID;
                    }

                }

                if (selectedKey != null)
                {
                    return profiles.authenticationDatabase[selectedKey];
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);

            }
            return null;
        }

        private void mainWindow_Shown(object sender, EventArgs e)
        {
            try
            {
                ChangeSelectedModPack(Settings.Default.selectedModPack);
                Console.WriteLine("Selected ModPack: {0}", Settings.Default.selectedModPack);

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Close selected: {0}", Settings.Default.selectedModPack);
            Settings.Default.Save();
        }

        private void infoWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (_changingUrl)
            {
                _changingUrl = false; 
                return;
            }
            e.Cancel = true;
            Console.WriteLine(_changingUrl);
            Console.WriteLine("browser start");
            Process.Start(e.Url.ToString());
        }

        private void GetAssetsForVersion(string version)
        {
            try
            {
                string link;
                if (version == "1.6.4")
                {
                    link = "https://s3.amazonaws.com/Minecraft.Download/indexes/legacy.json";
                }
                else
                {
                    link = "https://s3.amazonaws.com/Minecraft.Download/indexes/" + version + ".json";
                }
                string web = getStringFromUrl(link);

                const string startLink = "http://resources.download.minecraft.net/";
                JCassets json = JsonConvert.DeserializeObject<JCassets>(web);
                _downloadList.Add(new JCdownloadList
                {
                    link = link,
                    hash = "",
                    hashType = "",
                    saveLocations = new List<string> { _location + "\\assets\\indexes\\" + version + ".json" },
                    type = "assets"

                });

                foreach (String key in json.objects.Keys)
                {
                    try
                    {
                        string dllink = startLink + json.objects[key].hash.Substring(0, 2) + "/" + json.objects[key].hash;
                        JCdownloadList data = new JCdownloadList
                        {
                            link = dllink,
                            hash = json.objects[key].hash,
                            hashType = "sha1",
                            type = "assets",
                            saveLocations =
                                new List<string>
                                {
                                    _location + "\\assets\\objects\\" + json.objects[key].hash.Substring(0, 2) + "\\" +
                                    json.objects[key].hash,
                                    _location + "\\assets\\virtual\\legacy\\" + key.Replace('/', '\\')
                                }
                        };
                        //data.saveLocations.Add(location + "\\assets\\virtual\\legacy\\" + key.Replace('/', '\\'));
                        _downloadList.Add(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void GetLibrariesForVersion(JCmodpackInfo modpack, string version, string forgeVersion)
        {
            try
            {
                string dlUrl = "http://solder.mechzone.net/install_profile/" + version + "-" + forgeVersion + ".json";
                //Console.WriteLine(dlUrl);
                string json = getStringFromUrl(dlUrl);
                JCmcInfo g = JsonConvert.DeserializeObject<JCmcInfo>(json);
                foreach (Library t in g.versionInfo.libraries)
                {
                    string dlLink = "";
                    string[] parts = t.name.Split(':');
                    dlLink += parts[0].Replace('.', '/') + "/";
                    dlLink += parts[1] + "/";
                    dlLink += parts[2] + "/";
                    dlLink += parts[1] + "-" + parts[2];
                    if (t.natives != null)
                    {
                        dlLink += "-" + t.natives.windows.Replace("${arch}", "64");
                    }
                    dlLink += ".jar";
                    JCdownloadList data = new JCdownloadList();
                    if (t.url != null)
                    {
                        if (dlLink.Contains("net/minecraftforge/"))
                        {
                            data.link = t.url + dlLink.Replace(".jar", "-universal.jar");
                        }
                        else
                        {
                            data.link = t.url + dlLink;
                        }
                        if (t.checksums != null)
                        {
                            data.hash = t.checksums[0];
                            data.hashType = "";
                        }
                        else
                        {
                            data.hash = "";
                            data.hashType = "";
                        }
                    }
                    else
                    {
                        data.link = ("https://libraries.minecraft.net/" + dlLink);
                        data.hash = getStringFromUrl("https://libraries.minecraft.net/" + dlLink + ".sha1");
                        data.hashType = "sha1";
                    }


                    data.type = "libraries";
                    data.saveLocations = new List<string>
                    {
                        _location + "\\modpacks\\" + modpack.name + "\\libraries\\" + dlLink
                    };
                    _downloadList.Add(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

/*
        private void GetForge(string forgeVersion, string minecraftVersion)
        {
            try
            {
                //http://files.minecraftforge.net/maven/net/minecraftforge/forge/1.7.10-10.13.0.1186/forge-1.7.10-10.13.0.1186-universal.jar
                string dllink = "http://files.minecraftforge.net/maven/net/minecraftforge/forge/";
                dllink += minecraftVersion;
                dllink += "-";
                dllink += forgeVersion;
                dllink += "\\forge-";
                dllink += minecraftVersion;
                dllink += "-";
                dllink += forgeVersion;
                dllink += "-universal.jar";

                JCdownloadList data = new JCdownloadList();
                data.link = dllink;
                data.hash = getStringFromUrl(dllink + ".sha1");
                data.hashType = "sha1";
                data.saveLocations = new List<string>();
                data.saveLocations.Add(_location + "\\libraries\\net\\minecraftforge\\minecraftforge\\" + forgeVersion + "\\minecraftforge-" + forgeVersion + ".jar");
                data.type = "forge";
                _downloadList.Add(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }
*/

        private void GetMinecraft(JCmodpackInfo modpack, string minecraftVersion)
        {
            try
            {
                string dllink = "http://s3.amazonaws.com/Minecraft.Download/versions/";
                dllink += minecraftVersion;
                dllink += "/";
                dllink += minecraftVersion;
                dllink += ".jar";
                JCdownloadList data = new JCdownloadList
                {
                    link = dllink,
                    hash = "",
                    hashType = "",
                    saveLocations =
                        new List<string> {_location + "\\modpacks\\" + modpack.name + "\\bin\\minecraft.jar"},
                    type = "minecraft"
                };
                _downloadList.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }

        }

        private void GetMods(JCmodpackVersion build)
        {
            try
            {
                foreach (Mod t in build.mods)
                {
                    JCdownloadList data = new JCdownloadList
                    {
                        hash = t.md5,
                        hashType = "md5",
                        link = t.url,
                        saveLocations = new List<string> {_location + "\\temp\\" + t.name + "-" + t.version + ".zip"},
                        type = "mods"
                    };
                    _downloadList.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private string getJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            //MessageBox.Show(environmentPath);
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath + "\\bin\\javaw.exe";
            }

            const string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(javaKey))
            {
                if (rk == null) return null;
                string currentVersion = rk.GetValue("CurrentVersion").ToString();
                using (RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    if (key != null) return key.GetValue("JavaHome") + "\\bin\\javaw.exe";
                    return null;
                }
            }
        }

        private void logTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Console.WriteLine(e.LinkText);
                Process.Start(e.LinkText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            try
            {
                Invalidate(GetSelectedProfile().accessToken, _uuid);
                int item = Settings.Default.selectedProfile;
                JCprofileSave save = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(_location + @"\\mz_launcher_profiles.json"));
                string profile = profileBox.Items[item].ToString();
                string uuid = "";
                string name2 = "";
                foreach (string name in save.profiles.Keys.Where(name => name.Equals(profile)))
                {
                    uuid = save.profiles[name].playerUUID;
                    name2 = name;
                }

                save.authenticationDatabase.Remove(uuid);
                save.profiles.Remove(name2);
                File.WriteAllText(_location + @"\\mz_launcher_profiles.json", JsonConvert.SerializeObject(save, Formatting.Indented));

                profileBox.Items.Remove(profileBox.Items[item]);

                

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }

        }

        private void ramSelector_Scroll(object sender, ScrollEventArgs e)
        {
            selectedRam.Text = string.Format("{0} GB ({1} MB)", ramSelector.Value / 1024, ramSelector.Value);

        }

        private void ramSelector_MouseUp(object sender, MouseEventArgs e)
        {
            Settings.Default.ram = ramSelector.Value;
            Settings.Default.Save();
        }

        private void extraJavaParameters_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.javaParameters = extraJavaParameters.Text;
            Settings.Default.Save();
        }

        private void changeJavaPath_Click(object sender, EventArgs e)
        {

            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = @"javaw File |javaw.exe",
                InitialDirectory = Path.GetDirectoryName(getJavaInstallationPath()),
                FilterIndex = 1
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Settings.Default.javaPath = fd.FileName;
                    Settings.Default.Save();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                }
            }


        }

        private void changeInstallPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog bd = new FolderBrowserDialog {SelectedPath = _location, ShowNewFolderButton = true};
            if (bd.ShowDialog() == DialogResult.OK)
            {
                /* if(!IsDirectoryEmpty(bd.SelectedPath))
                 {
                     MessageBox.Show("Directory is not empty.\nPlease select a different directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 } else
                 {*/
                Settings.Default.installPath = bd.SelectedPath;
                Settings.Default.Save();
                installPathTextBox.Text = bd.SelectedPath;
                string oldlocation = _location;
                _location = bd.SelectedPath;
                if (!File.Exists(_location + @"\mz_launcher_profiles.json"))
                {
                    File.Copy(oldlocation + @"\mz_launcher_profiles.json", _location + @"\mz_launcher_profiles.json");

                }
                //}
            }
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            try
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
