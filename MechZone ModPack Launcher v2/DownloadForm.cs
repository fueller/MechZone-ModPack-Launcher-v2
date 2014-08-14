using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MechZone_ModPack_Launcher_v2.Properties;
using MetroFramework.Forms;
using MechZone_ModPack_Launcher_v2.jsonClasses;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using Ionic.Zip;
using Newtonsoft.Json;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class DownloadForm : MetroForm
    {
        MetroForm _senderForm;
        List<JCdownloadList> _downloadList;
        readonly Stopwatch _sw = new Stopwatch();
        string _saveLocation;
        JCmodpackInfo _modpack;

        public DownloadForm()
        {
            InitializeComponent();
        }

        public MetroForm SenderForm
        {
            set { _senderForm = value; Invalidate(); }
        }

        public List<JCdownloadList> DownloadList
        {
            set { _downloadList = value; Invalidate(); }
        }
        public string SaveLocation
        {
            set { _saveLocation = value; Invalidate(); }
        }
        public JCmodpackInfo Modpack
        {
            set { _modpack = value; Invalidate(); }
        }


        private void DownloadForm_Load(object sender, EventArgs e)
        {
            metroStyleManager.Theme = _senderForm.Theme;
            metroStyleManager.Style = _senderForm.Style;
        }

        private readonly Queue<JCdownloadList> _items = new Queue<JCdownloadList>();

        private void DownloadForm_Shown(object sender, EventArgs e)
        {
            foreach (JCdownloadList t in _downloadList)
            {
                _items.Enqueue(t);
            }
            allFileProgress.Maximum = _items.Count;
            allFileProgress.Value = 0;
            metroLabel1.Text = string.Format("Downloading File {0} from {1}", 1, _items.Count);
            singleFileProgress.Value = 0;
            downloadSpeed.Text = string.Format("{0} kb/s", 0);
            labelDownload.Text = string.Format("{0} MB / {1} MB", 0, 0);
            Update();
            Thread.Sleep(1);
            DownloadItem();
        }

        private void DownloadItem()
        {
            if (_items.Count > 0)
            {
                JCdownloadList nextItem = _items.Dequeue();
                string outputFolder = Path.GetDirectoryName(nextItem.saveLocations[0]);
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                if (File.Exists(nextItem.saveLocations[0]))
                {
                    if (CheckFile(nextItem))
                    {
                        Client_DownloadFileCompleted(null, null);
                        return;
                    }
                    else
                    {
                        foreach (string t in nextItem.saveLocations)
                        {
                            File.Delete(t);
                        }
                    }

                }
                WebClient client = new WebClient {Proxy = null};
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                _sw.Start();
                client.DownloadFileAsync(new Uri(nextItem.link), nextItem.saveLocations[0]);

                singleFileProgress.Value = 0;
                return;
            }

            Console.WriteLine("done downloads");
            downloadSpeed.Text = "";
            labelDownload.Text = "";
            int numUnzip = _downloadList.Count(t => t.type.Equals("mods") || t.type.Equals("natives") || t.saveLocations.Count > 1);
            metroLabel1.Text = string.Format("Unzipping File {0} from {1}", 1, numUnzip);
            allFileProgress.Maximum = numUnzip;
            allFileProgress.Value = 0;
            singleFileProgress.Maximum = 100;
            singleFileProgress.Value = 50;
            Update();
            Thread.Sleep(1);
            CopyFiles();
            UnzipMods();
            CreateVersionFile();
            allFileProgress.Value = 0;
            allFileProgress.Maximum = 0;
            singleFileProgress.Value = 0;
            singleFileProgress.Maximum = 0;
            Close();
        }

        private void CreateVersionFile()
        {
            try
            {
                string fileLoc = _saveLocation + "\\modpacks\\" + _modpack.name + "\\version.json";
                if (!File.Exists(fileLoc))
                {
                    File.Create(fileLoc);
                    JCversion ver = new JCversion {version = _modpack.latest};
                    string outp = JsonConvert.SerializeObject(ver);
                    File.WriteAllText(fileLoc, outp);
                }
                else
                {
                    JCversion ver = JsonConvert.DeserializeObject<JCversion>(File.ReadAllText(fileLoc));
                    ver.version = _modpack.latest;
                    string outp = JsonConvert.SerializeObject(ver);
                    File.WriteAllText(fileLoc, outp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void UnzipMods()
        {
            try
            {
                if (Directory.Exists(_saveLocation + "\\modpacks\\" + _modpack.name + "\\config\\"))
                {
                    DialogResult result = MessageBox.Show(Resources.DownloadForm_UnzipMods_Do_you_want_to_Backup_your_Config_Files_, Resources.DownloadForm_UnzipMods_Backup_Config_Files, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (!Directory.Exists(_saveLocation + "\\modpacks\\" + _modpack.name + "\\config_backup\\"))
                        {
                            Directory.CreateDirectory(_saveLocation + "\\modpacks\\" + _modpack.name + "\\config_backup\\");
                        }

                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AddDirectory(_saveLocation + "\\modpacks\\" + _modpack.name + "\\config\\");
                            DateTime now = DateTime.Now;
                            zip.Save(_saveLocation + "\\modpacks\\" + _modpack.name + "\\config_backup\\" + now.Year + "-" + now.Month + "-" + now.Day + "_" + now.Hour + "-" + now.Minute + "-" + now.Second + ".zip");
                        }

                    }
                    Directory.Delete(_saveLocation + "\\modpacks\\" + _modpack.name + "\\config\\", true);
                }
                if (Directory.Exists(_saveLocation + "\\modpacks\\" + _modpack.name + "\\mods\\"))
                {
                    Directory.Delete(_saveLocation + "\\modpacks\\" + _modpack.name + "\\mods\\", true);

                }

                foreach (string directory in Directory.GetDirectories(_saveLocation + "\\modpacks\\" + _modpack.name + "\\bin\\", "*natives"))
                {
                    Directory.Delete(directory, true);
                }

                foreach (JCdownloadList t in _downloadList)
                {
                    if (t.type.Equals("natives"))
                    {
                        allFileProgress.PerformStep();
                        metroLabel1.Text = string.Format("Unzipping File {0} from {1}", allFileProgress.Value, allFileProgress.Maximum);

                        using (ZipFile zip = ZipFile.Read(t.saveLocations[0]))
                        {
                            string output = _saveLocation + "\\modpacks\\" + _modpack.name + "\\bin\\";
                            foreach (ZipEntry e in zip)
                            {
                                e.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                                e.Extract(output);
                            }
                        }
                    }
                    if (t.type.Equals("mods"))
                    {
                        allFileProgress.PerformStep();
                        metroLabel1.Text = string.Format("Unzipping File {0} from {1}", allFileProgress.Value, allFileProgress.Maximum);

                        using (ZipFile zip = ZipFile.Read(t.saveLocations[0]))
                        {
                            string output = _saveLocation + "\\modpacks\\" + _modpack.name + "\\";
                            foreach (ZipEntry e in zip)
                            {
                                e.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                                e.Extract(output);
                            }
                        }
                    }
                    Update();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void CopyFiles()
        {
            foreach (JCdownloadList t in _downloadList.Where(t => t.saveLocations.Count > 1))
            {
                allFileProgress.PerformStep();
                metroLabel1.Text = string.Format("Unzipping File {0} from {1}", allFileProgress.Value, allFileProgress.Maximum);
                for (int j = 1; j < t.saveLocations.Count; j++)
                {
                    if (File.Exists(t.saveLocations[j]))
                    {
                        continue;
                    }
                    string outputFolder = Path.GetDirectoryName(t.saveLocations[j]);
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                    }
                    File.Copy(t.saveLocations[0], t.saveLocations[j]);
                }
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (allFileProgress.Value < allFileProgress.Maximum)
            {
                allFileProgress.PerformStep();
            }

            metroLabel1.Text = string.Format("Downloading File {0} from {1}", allFileProgress.Value, allFileProgress.Maximum);
            if (e != null)
            {
                if (e.Error != null)
                {
                    Console.WriteLine("{0}\n{1}", e.Error.Message, e.Error.StackTrace);
                }
                if (e.Cancelled)
                {
                    Console.WriteLine("canceled");
                }
            }
            DownloadItem();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / _sw.Elapsed.TotalSeconds).ToString("0.00"));
            labelDownload.Text = string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            singleFileProgress.Value = e.ProgressPercentage;
        }

        private static bool CheckFile(JCdownloadList file)
        {
            switch (file.hashType)
            {
                case "sha1":
                    FileStream sha1FileCheck = File.OpenRead(file.saveLocations[0]);
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1Hash = sha1.ComputeHash(sha1FileCheck);
                    sha1FileCheck.Close();
                    string sha1HashString = BitConverter.ToString(sha1Hash).Replace("-", "").ToLower();
                    return file.hash.Equals(sha1HashString);
                case "md5":
                    FileStream md5FileCheck = File.OpenRead(file.saveLocations[0]);
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] md5Hash = md5.ComputeHash(md5FileCheck);
                    md5FileCheck.Close();
                    string md5HashString = BitConverter.ToString(md5Hash).Replace("-", "").ToLower();
                    return file.hash.Equals(md5HashString);
                case null:
                    return true;
                default:
                    return true;
            }
        }

    }
}
