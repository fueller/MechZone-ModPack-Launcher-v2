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
using System.Windows;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Fonts;
using MetroFramework.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Deployment.Application;
using System.Reflection;
using System.Diagnostics;
using MechZone_ModPack_Launcher_v2.jsonClasses;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class mainWindow : MetroForm
    {
        JCmodpacks aviableModpacks = new JCmodpacks();
        Dictionary<string, JCmodpackInfo> modPackInfos = new Dictionary<string, JCmodpackInfo>();
        string appdata;
        string solderUrl = @"http://solder.mechzone.net/";
        string solderApiUrl;
        string modDownloadUrl;
        string location = "%appdata%/.mechzoneV2";
        Guid uuid;
        //Dictionary<string, JCdownloadList> downloadList = new Dictionary<string, JCdownloadList>();
        List<JCdownloadList> downloadList = new List<JCdownloadList>();
        JCmodpackInfo selectedModpack = new JCmodpackInfo();


        public mainWindow()
        {
            try
            {
                
                InitializeComponent();

                mainTabControl.SelectedIndex = 0;
                appdata = Environment.GetEnvironmentVariable("appdata");
                location = appdata + "\\.mechzoneV2";
                solderApiUrl = solderUrl + @"index.php/api/";
                modDownloadUrl = solderUrl + @"mods/";
                if(!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }

                if(!File.Exists(location + @"\mz_launcher_profiles.json"))
                {
                    JCprofileSave text = new JCprofileSave();
                    Guid g = Guid.NewGuid();
                    text.clientToken = g;
                    text.profiles = new Dictionary<string, profileInfo>();
                    text.authenticationDatabase = new Dictionary<string, userInfo>();
                    File.WriteAllText(location + @"\mz_launcher_profiles.json", JsonConvert.SerializeObject(text, Formatting.Indented));
                }

                JCprofileSave uuidRead = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(location + @"\mz_launcher_profiles.json"));
                uuid = uuidRead.clientToken;
                Icon = Properties.Resources.taskbarIcon;
                this.Text = this.Text + " " + (ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Assembly.GetExecutingAssembly().GetName().Version.ToString());

                if(System.Diagnostics.Debugger.IsAttached)
                {
                    metroButton1.Visible = true;
                } else
                {
                    metroButton1.Visible = false;
                }

                foreach(MetroColorStyle color in (MetroColorStyle[])Enum.GetValues(typeof(MetroColorStyle)))
                {
                    styleSelector.Items.Add(color);
                }

                foreach(MetroThemeStyle color in (MetroThemeStyle[])Enum.GetValues(typeof(MetroThemeStyle)))
                {
                    themeSelector.Items.Add(color);
                }

                JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(location + @"\mz_launcher_profiles.json"));
                profileBox.Items.Clear();
                foreach(string profile in profiles.profiles.Keys)
                {
                    profileBox.Items.Add(profile);

                }

                try
                {
                    profileBox.SelectedIndex = Properties.Settings.Default.selectedProfile;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                }

                themeSelector.SelectedIndex = Properties.Settings.Default.selectedTheme;
                styleSelector.SelectedIndex = Properties.Settings.Default.selectedStyle;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            getModPacks();
            getModPackInfos();
            listModPacks();
        }

        void getModPacks()
        {
            try
            {
                String temp = getStringFromUrl(solderApiUrl + "modpack");
                aviableModpacks = JsonConvert.DeserializeObject<JCmodpacks>(temp);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        void getModPackInfos()
        {
            try
            {
                foreach(string modpack in aviableModpacks.modpacks.Keys)
                {
                    modPackInfos[modpack] = JsonConvert.DeserializeObject<JCmodpackInfo>(getStringFromUrl(solderApiUrl + "modpack/" + modpack));
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        void listModPacks()
        {
            try
            {
                int i = 0;
                foreach(string modpack in aviableModpacks.modpacks.Keys)
                {
                    ModPack mp = new ModPack();
                    mp.Location = new Point(5, 5 + (150 * i));
                    mp.modPackDescription = modPackInfos[modpack].description;
                    mp.modPackName = modPackInfos[modpack].display_name;
                    mp.modPackImage = modPackInfos[modpack].icon;
                    mp.Click += new EventHandler(modpackSelected);
                    mp.modInfoImage.Click += new EventHandler(modpackSelected);
                    mp.modInfoDescription.Click += new EventHandler(modpackSelected);
                    mp.modInfoName.Click += new EventHandler(modpackSelected);
                    mp.modPackInfo = modPackInfos[modpack];
                    modPacksContainer.Controls.Add(mp);
                    i++;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void modpackSelected(object sender, EventArgs e)
        {
            try
            {
                ModPack mp = null;
                if(sender.GetType().Equals(typeof(ModPack)))
                {
                    mp = (ModPack)sender;
                } else if(sender.GetType().Equals(typeof(MetroLabel)))
                {
                    MetroLabel label = (MetroLabel)sender;
                    if(label.Parent.GetType().Equals(typeof(ModPack)))
                    {
                        mp = (ModPack)label.Parent;
                    } else
                    {
                        throw new Exception("Selected ModPack Error");
                    }
                } else if(sender.GetType().Equals(typeof(PictureBox)))
                {
                    PictureBox pb = (PictureBox)sender;
                    if(pb.Parent.GetType().Equals(typeof(ModPack)))
                    {
                        mp = (ModPack)pb.Parent;
                    } else
                    {
                        throw new Exception("Selected ModPack Error");
                    }
                }

                changeSelectedModPack(mp.modPackInfo.name);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void changeSelectedModPack(string smp)
        {
            try
            {

                for(int i = 0; i < modPacksContainer.Controls.Count; i++)
                {
                    if(modPacksContainer.Controls[i].GetType().Equals(typeof(ModPack)))
                    {
                        ModPack mp = (ModPack)modPacksContainer.Controls[i];
                        if(mp.modPackInfo.name.Equals(smp))
                        {
                            mp.StyleManager.Theme = MetroThemeStyle.Dark;
                            Properties.Settings.Default.selectedModPack = mp.modPackInfo.name;
                            selectedModpack = mp.modPackInfo;
                            Properties.Settings.Default.Save();
                        } else
                        {
                            mp.StyleManager.Theme = MetroThemeStyle.Light;
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        string getStringFromUrl(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            String erg = client.DownloadString(url);
            return erg;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string regex = @"\[\d\d:\d\d:\d\d\]";
                string text = @"[17:04:56] [Client thread/INFO]: [CHAT] fueller has just earned the achievement [Taking Inventory]";
                Regex mypattern = new Regex(regex);
                var test = mypattern.Match(text);
                MessageBox.Show("");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        #region refresh authenticate
        public static JCauthenticateResponse authenticate(string username, string password, Guid uuid)
        {
            try
            {
                WebRequest.DefaultWebProxy = null;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/authenticate");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JCauthenticatePayload payload = new JCauthenticatePayload();
                    payload.agent = new Agent();
                    payload.agent.name = "Minecraft";
                    payload.agent.version = 1;
                    payload.username = username;
                    payload.password = password;
                    payload.clientToken = uuid.ToString();
                    string json = JsonConvert.SerializeObject(payload);
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    JCauthenticateResponse j = JsonConvert.DeserializeObject<JCauthenticateResponse>(response);
                    return j;

                }
            } catch (System.Net.WebException)
            {
                MessageBox.Show("Entweder ist der Server nicht erreichbar oder\ndein Passwort oder Benutzername stimmt nicht!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        public static JCrefreshResponse refresh(string accessToken, Guid uuid)
        {
            try
            {
                WebRequest.DefaultWebProxy = null;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/refresh");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    JCrefreshPayload payload = new JCrefreshPayload();
                    payload.accessToken = accessToken;
                    payload.clientToken = uuid.ToString();
                    string json = JsonConvert.SerializeObject(payload);
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    JCrefreshResponse j = JsonConvert.DeserializeObject<JCrefreshResponse>(response);
                    return j;

                }
            } catch (WebException ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region theme and style
        private void styleSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            StyleManager.Style = (MetroColorStyle)styleSelector.SelectedItem;
            Properties.Settings.Default.selectedStyle = styleSelector.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void themeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            StyleManager.Theme = (MetroThemeStyle)themeSelector.SelectedItem;
            Properties.Settings.Default.selectedTheme = themeSelector.SelectedIndex;
            Properties.Settings.Default.Save();
        }
        #endregion

        private void addProfile_Click(object sender, EventArgs e)
        {
            try
            {
                addProfile form = new MechZone_ModPack_Launcher_v2.addProfile();
                form.metroStyleManager.Style = Style;
                form.metroStyleManager.Theme = Theme;
                form.setUuid = uuid;
                form.path = location;
                DialogResult res = form.ShowDialog();
                if(res == DialogResult.OK)
                {
                    JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(location + @"\mz_launcher_profiles.json"));
                    profileBox.Items.Clear();
                    foreach(string profile in profiles.profiles.Keys)
                    {
                        profileBox.Items.Add(profile);
                        Properties.Settings.Default.selectedProfile = 0;
                        Properties.Settings.Default.Save();
                        profileBox.SelectedIndex = Properties.Settings.Default.selectedProfile;
                    }
                } else if(res == DialogResult.Cancel)
                {
                    Console.WriteLine("Cancel");
                } else if(res == DialogResult.Abort)
                {
                    MessageBox.Show("Es ist ein Fehler aufgetreten, bitte versuche es noch einmal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void profileBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.selectedProfile = profileBox.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                userInfo user = getSelectedProfile();
                JCrefreshResponse response = refresh(user.accessToken, uuid);
                if(response != null)
                {
                    JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(location + @"\mz_launcher_profiles.json"));
                    profiles.authenticationDatabase[response.selectedProfile.id].accessToken = response.accessToken;
                    File.WriteAllText(location + @"\mz_launcher_profiles.json", JsonConvert.SerializeObject(profiles, Formatting.Indented));
                }
                launchModPack(user, selectedModpack);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void launchModPack(userInfo user, JCmodpackInfo selectedModPack)
        {
            try
            {
                Console.WriteLine("Starte Modpack: \"" + selectedModpack.display_name + "\" mit Benutzer: \"" + user.displayName + "\"");
                JCmodpackVersion latestVersion = getLatestModPackVersion(selectedModPack);
                getMinecraft(selectedModpack, latestVersion.minecraft);
                getAssetsForVersion(latestVersion.minecraft);
                getLibrariesForVersion(latestVersion.minecraft, latestVersion.forgeVersion);
                ////getForge(selectedModpack, latestVersion.forgeVersion, latestVersion.minecraft);
                getNatives(selectedModPack, latestVersion.forgeVersion, latestVersion.minecraft);
                getMods(selectedModPack, latestVersion);

                string fileLoc = location + "\\modpacks\\" + selectedModpack.name + "\\version.json";

                if(!File.Exists(fileLoc))
                {
                    File.WriteAllText(fileLoc, JsonConvert.SerializeObject(new JCversion()));
                }

                string latestVersionString = selectedModpack.latest;
                string localVersionString = JsonConvert.DeserializeObject<JCversion>(File.ReadAllText(fileLoc)).version;


                if(!latestVersionString.Equals(localVersionString))
                {
                    DownloadForm dlf = new DownloadForm();
                    dlf.senderForm = this;
                    dlf.downloadList = downloadList;
                    dlf.location = location;
                    dlf.modpack = selectedModpack;
                    dlf.ShowDialog(); 
                }

                //Close();

                ProcessStartInfo start = new ProcessStartInfo();
                start.UseShellExecute = false;
                start.RedirectStandardError = true;
                start.RedirectStandardOutput = true;

                start.FileName = getJavaInstallationPath() + "\\bin\\javaw.exe";
                
                string workingDirectory = location + "\\modpacks\\" + selectedModpack.name;

                start.WorkingDirectory = workingDirectory;

                string arguments = "";
                arguments += "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump ";
                arguments += "-Xmx4096m ";
                arguments += "-XX:MaxPermSize=256m ";
                arguments += @"-Djava.library.path=" + location + @"\modpacks\" + selectedModpack.name + @"\bin\" + latestVersion.minecraft + "-" + latestVersion.forgeVersion + "-natives ";
                //arguments += "-Dminecraft.applet.TargetDirectory=" + workingDirectory + " ";
                arguments += "-cp ";
                for(int i = 0; i < downloadList.Count; i++)
                {
                    if(downloadList[i].type.Equals("libraries"))
                    {
                        arguments += downloadList[i].saveLocations[0] + ";";
                    }
                }
                arguments += location + @"\modpacks\" + selectedModpack.name + "\\bin\\minecraft.jar net.minecraft.launchwrapper.Launch ";
                arguments += "--username fueller ";
                arguments += "--version 1.7.10 ";
                arguments += "--gameDir " + workingDirectory + " ";
                arguments += "--assetsDir " + location + @"\assets ";
                arguments += "--assetIndex 1.7.10 ";
                arguments += "--uuid 7d5e5b10011e4403b53f931d523375f3 ";
                arguments += "--accessToken 92d11ff5516e4a909d25a854a7884ed0 ";
                arguments += "--userProperties {} ";
                arguments += "--userType mojang ";
                arguments += "--tweakClass cpw.mods.fml.common.launcher.FMLTweaker";



                //Console.WriteLine(arguments);
                start.Arguments = arguments;

                using (Process process = Process.Start(start))
                {
                    process.OutputDataReceived += Minecraft_OutputDataReceived;
                    process.ErrorDataReceived += Minecraft_ErrorDataReceived;
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Minecraft_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                addLogText(e.Data, "error");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Minecraft_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                addLogText(e.Data, "output");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        delegate void logTextAdd(string text, string type);

        void addLogText(string text, string type)
        {
            if(InvokeRequired)
            {
                BeginInvoke(new logTextAdd(addLogText), new object[] { text, type });
                return;
            }

            if(text == null)
            {
                return;
            }

            if(type.Equals("error") || text.Contains("Warning") || text.Contains("WARN"))
            {
                logTextBox.AppendText(text + "\n", Color.Red);
            } else if(text.Contains("INFO"))
            {
                logTextBox.AppendText(text + "\n", Color.Blue);
            }else
            {
                logTextBox.AppendText(text + "\n");
            }
            logTextBox.Focus();
        }

        private void getNatives(JCmodpackInfo selectedModPack, string forgeVersion, string minecraft)
        {
            try
            {
                JCdownloadList data = new JCdownloadList();
                data.link = "http://solder.mechzone.net/natives/" + minecraft + "-" + forgeVersion + "-natives.zip";
                data.hash = getStringFromUrl("http://solder.mechzone.net/natives/" + minecraft + "-" + forgeVersion + "-natives.zip.sha1");
                data.hashType = "sha1";
                data.type = "natives";
                data.saveLocations = new List<string>();
                data.saveLocations.Add(location + "\\temp\\" + "natives-" + minecraft + "-" + forgeVersion + ".zip");
                downloadList.Add(data);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private JCmodpackVersion getLatestModPackVersion(JCmodpackInfo modPack)
        {
            string url = solderApiUrl + "modpack/" + modPack.name + "/" + modPack.latest;
            string data = getStringFromUrl(url);
            JCmodpackVersion json = JsonConvert.DeserializeObject<JCmodpackVersion>(data);
            return json;
        }

        private userInfo getSelectedProfile()
        {
            try
            {
                JCprofileSave profiles = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(location + @"\mz_launcher_profiles.json"));
                string selectedKey = null;
                foreach(string key in profiles.profiles.Keys)
                {
                    if(profiles.profiles[key].name.Equals(profileBox.SelectedItem.ToString()))
                    {
                        selectedKey = profiles.profiles[key].playerUUID;
                    }

                }

                if(selectedKey != null)
                {
                    return profiles.authenticationDatabase[selectedKey];
                } else
                {
                    return null;
                }


            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);

            }
            return null;
        }

        private void mainWindow_Shown(object sender, EventArgs e)
        {
            try
            {
                changeSelectedModPack(Properties.Settings.Default.selectedModPack);
                Console.WriteLine("Selected ModPack: " + Properties.Settings.Default.selectedModPack);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Close selected: " + Properties.Settings.Default.selectedModPack);
            Properties.Settings.Default.Save();
        }

        private void infoWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
            Process.Start(e.Url.ToString());
        }

        private void getAssetsForVersion(string version)
        {
            try
            {
                string web = getStringFromUrl("https://s3.amazonaws.com/Minecraft.Download/indexes/" + version + ".json");
                string startLink = "http://resources.download.minecraft.net/";
                JCassets json = JsonConvert.DeserializeObject<JCassets>(web);
                downloadList.Add(new JCdownloadList()
                {
                    link = "https://s3.amazonaws.com/Minecraft.Download/indexes/" + version + ".json",
                    hash = "",
                    hashType = "",
                    saveLocations = new List<string>() { location + "\\assets\\indexes\\" + version + ".json" },
                    type = "assets"

                });

                foreach(String key in json.objects.Keys)
                {
                    try
                    {
                        string dllink = startLink + json.objects[key].hash.Substring(0, 2) + "/" + json.objects[key].hash;
                        JCdownloadList data = new JCdownloadList();
                        data.link = dllink;
                        data.hash = json.objects[key].hash;
                        data.hashType = "sha1";
                        data.type = "assets";
                        data.saveLocations = new List<string>();
                        data.saveLocations.Add(location + "\\assets\\objects\\" + json.objects[key].hash.Substring(0, 2) + "\\" + json.objects[key].hash);
                        //data.saveLocations.Add(location + "\\assets\\virtual\\legacy\\" + key.Replace('/', '\\'));
                        downloadList.Add(data);
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }


                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void getLibrariesForVersion(string version, string forgeVersion)
        {
            try
            {
                //string dlUrl = "http://s3.amazonaws.com/Minecraft.Download/versions/" + version + "/" + version + ".json";
                string dlUrl = "http://solder.mechzone.net/install_profile/" + version + "-" + forgeVersion + ".json";
                Console.WriteLine(dlUrl);
                string json = getStringFromUrl(dlUrl);
                JCmcInfo g = JsonConvert.DeserializeObject<JCmcInfo>(json);
                for(int i = 0; i < g.versionInfo.libraries.Count; i++)
                {
                    string dlLink = "";
                    string[] parts = g.versionInfo.libraries[i].name.Split(':');
                    dlLink += parts[0].Replace('.', '/') + "/";
                    dlLink += parts[1] + "/";
                    dlLink += parts[2] + "/";
                    dlLink += parts[1] + "-" + parts[2];
                    if(g.versionInfo.libraries[i].natives != null)
                    {
                        dlLink += "-" + g.versionInfo.libraries[i].natives.windows.Replace("${arch}", "64");
                    }
                    dlLink += ".jar";
                    JCdownloadList data = new JCdownloadList();
                    if(g.versionInfo.libraries[i].url != null)
                    {
                        if(dlLink.Contains("net/minecraftforge/forge/"))
                        {
                            data.link = g.versionInfo.libraries[i].url + dlLink.Replace(".jar", "-universal.jar");
                        } else {
                            data.link = g.versionInfo.libraries[i].url + dlLink;
                        }
                        if(g.versionInfo.libraries[i].checksums != null)
                        {
                            data.hash = g.versionInfo.libraries[i].checksums[0];
                            data.hashType = "";
                        } else
                        {
                            data.hash = "";
                            data.hashType = "";
                        }
                    } else
                    {
                        data.link = ("https://libraries.minecraft.net/" + dlLink);
                        data.hash = getStringFromUrl("https://libraries.minecraft.net/" + dlLink + ".sha1");
                        data.hashType = "sha1";
                    }


                    data.type = "libraries";
                    data.saveLocations = new List<string>();
                    data.saveLocations.Add(location + "\\libraries\\" + dlLink);
                    downloadList.Add(data);

                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void getForge(JCmodpackInfo modpack, string forgeVersion, string minecraftVersion)
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
                data.saveLocations.Add(location + "\\libraries\\net\\minecraftforge\\minecraftforge\\" + forgeVersion + "\\minecraftforge-" + forgeVersion + ".jar");
                data.type = "forge";
                downloadList.Add(data);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void getMinecraft(JCmodpackInfo modpack, string minecraftVersion)
        {
            try
            {
                string dllink = "http://s3.amazonaws.com/Minecraft.Download/versions/";
                dllink += minecraftVersion;
                dllink += "/";
                dllink += minecraftVersion;
                dllink += ".jar";
                JCdownloadList data = new JCdownloadList();
                data.link = dllink;
                data.hash = "";
                data.hashType = "";
                data.saveLocations = new List<string>();
                data.saveLocations.Add(location + "\\modpacks\\" + modpack.name + "\\bin\\minecraft.jar");
                data.type = "minecraft";
                downloadList.Add(data);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void getMods(JCmodpackInfo modpack, JCmodpackVersion build)
        {
            try
            {
                for(int i = 0; i < build.mods.Count; i++)
                {
                    JCdownloadList data = new JCdownloadList();
                    data.hash = build.mods[i].md5;
                    data.hashType = "md5";
                    data.link = build.mods[i].url;
                    data.saveLocations = new List<string>();
                    data.saveLocations.Add(location + "\\temp\\" + build.mods[i].name + "-" + build.mods[i].version + ".zip");
                    data.type = "mods";
                    downloadList.Add(data);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private string getJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            if(!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
            {
                string currentVersion = rk.GetValue("CurrentVersion").ToString();
                using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    return key.GetValue("JavaHome").ToString();
                }
            }
        }

        private void logTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Console.WriteLine(e.LinkText);
                Process.Start(e.LinkText);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            try
            {
                int item = Properties.Settings.Default.selectedProfile;
                JCprofileSave save = JsonConvert.DeserializeObject<JCprofileSave>(File.ReadAllText(location + @"\\mz_launcher_profiles.json"));
                string profile = profileBox.Items[item].ToString();
                string uuid = "";
                string name2 = "";
                foreach(string name in save.profiles.Keys)
                {
                    if(name.Equals(profile))
                    {
                        uuid = save.profiles[name].playerUUID;
                        name2 = name;
                        
                    }
                }

                save.authenticationDatabase.Remove(uuid);
                save.profiles.Remove(name2);
                File.WriteAllText(location + @"\\mz_launcher_profiles.json", JsonConvert.SerializeObject(save,Formatting.Indented));

                profileBox.Items.Remove(profileBox.Items[item]);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
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
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
