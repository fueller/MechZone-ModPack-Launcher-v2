﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using MetroFramework.Forms;
using MechZone_ModPack_Launcher_v2.jsonClasses;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using Ionic.Zip;
using Newtonsoft.Json;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class DownloadForm : MetroForm
    {
        MetroForm SenderForm;
        List<JCdownloadList> DownloadList;
        Stopwatch sw = new Stopwatch();
        string saveLocation;
        JCmodpackInfo Modpack;

        public DownloadForm()
        {
            InitializeComponent();
        }

        public MetroForm senderForm
        {
            set { SenderForm = value; Invalidate(); }
        }

        public List<JCdownloadList> downloadList
        {
            set { DownloadList = value; Invalidate(); }
        }
        public string location
        {
            set { saveLocation = value; Invalidate(); }
        }
        public JCmodpackInfo modpack
        {
            set { Modpack = value; Invalidate(); }
        }


        private void DownloadForm_Load(object sender, EventArgs e)
        {
            metroStyleManager.Theme = SenderForm.Theme;
            metroStyleManager.Style = SenderForm.Style;
        }

        private Queue<JCdownloadList> _items = new Queue<JCdownloadList>();
        private List<string> _results = new List<string>();

        private void DownloadForm_Shown(object sender, EventArgs e)
        {
            for(int i = 0; i < DownloadList.Count; i++)
            {
                _items.Enqueue(DownloadList[i]);
            }
            allFileProgress.Maximum = _items.Count;
            allFileProgress.Value = 0;
            metroLabel1.Text = "Downloading File " + 1 + " from " + _items.Count;
            singleFileProgress.Value = 0;            
            downloadSpeed.Text = string.Format("{0} kb/s", 0);
            labelDownload.Text = string.Format("{0} MB / {1} MB", 0, 0);
            this.Update();
            Thread.Sleep(1);
            downloadItem();
        }

        private void downloadItem()
        {
            if(_items.Count > 0)
            {
                JCdownloadList nextItem = _items.Dequeue();
                string outputFolder = Path.GetDirectoryName(nextItem.saveLocations[0]);
                if(!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                if(File.Exists(nextItem.saveLocations[0]))
                {
                    if(checkFile(nextItem))
                    {
                        Client_DownloadFileCompleted(null, null);
                        return;
                    } else
                    {
                        for(int i = 0; i < nextItem.saveLocations.Count; i++)
                        {
                            File.Delete(nextItem.saveLocations[i]);
                        }
                        
                    }
                    
                }
                WebClient client = new WebClient();
                client.Proxy = null;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                sw.Start();
                client.DownloadFileAsync(new Uri(nextItem.link), nextItem.saveLocations[0]);
                
                singleFileProgress.Value = 0;
                return;
            }

            Console.WriteLine("done downloads");
            downloadSpeed.Text = "";
            labelDownload.Text = "";
            int numUnzip = 0;
            for(int i = 0; i < DownloadList.Count; i++)
            {
                if(DownloadList[i].type.Equals("mods") || DownloadList[i].type.Equals("natives") || DownloadList[i].saveLocations.Count > 1)
                {
                    numUnzip++;
                }
            }
            metroLabel1.Text = "Unzipping File " + 1 + " from " + numUnzip;
            allFileProgress.Maximum = numUnzip;
            allFileProgress.Value = 0;
            singleFileProgress.Maximum = 100;
            singleFileProgress.Value = 50;
            this.Update();
            Thread.Sleep(1);
            copyFiles();
            unzipMods();
            createVersionFile();
            Close();
        }

        private void createVersionFile()
        {
            try
            {
                string fileLoc = saveLocation + "\\modpacks\\" + Modpack.name + "\\version.json";
                if(!File.Exists(fileLoc))
                {
                    File.Create(fileLoc);
                    JCversion ver = new JCversion();
                    ver.version = Modpack.latest;
                    string outp = JsonConvert.SerializeObject(ver);
                    File.WriteAllText(fileLoc, outp);
                } else
                {
                    JCversion ver = JsonConvert.DeserializeObject<JCversion>(File.ReadAllText(fileLoc));
                    ver.version = Modpack.latest;
                    string outp = JsonConvert.SerializeObject(ver);
                    File.WriteAllText(fileLoc, outp);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void unzipMods()
        {
            for(int i = 0; i < DownloadList.Count; i++)
            {
                
                if(DownloadList[i].type.Equals("natives"))
                {
                    allFileProgress.PerformStep();
                    metroLabel1.Text = "Unzipping File " + allFileProgress.Value + " from " + allFileProgress.Maximum;
                    
                    using (ZipFile zip = ZipFile.Read(DownloadList[i].saveLocations[0]))
                    {
                        string output = saveLocation + "\\modpacks\\" + Modpack.name + "\\bin\\";
                        foreach(ZipEntry e in zip)
                        {
                            e.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                            e.Extract(output);
                        }
                    }
                }
                if(DownloadList[i].type.Equals("mods"))
                {
                    allFileProgress.PerformStep();
                    metroLabel1.Text = "Unzipping File " + allFileProgress.Value + " from " + allFileProgress.Maximum;
                    
                    using (ZipFile zip = ZipFile.Read(DownloadList[i].saveLocations[0]))
                    {
                        string output = saveLocation + "\\modpacks\\" + Modpack.name + "\\";
                        foreach(ZipEntry e in zip)
                        {
                            e.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                            e.Extract(output);
                        }
                    }
                }
                this.Update();
            }
        }

        private void copyFiles()
        {
            for(int i = 0; i < DownloadList.Count;i++)
            {
                if(DownloadList[i].saveLocations.Count > 1)
                {
                    allFileProgress.PerformStep();
                    metroLabel1.Text = "Unzipping File " + allFileProgress.Value + " from " + allFileProgress.Maximum;
                    for(int j = 1; j < DownloadList[i].saveLocations.Count; j++)
                    {
                        if(File.Exists(DownloadList[i].saveLocations[j]))
                        {
                            continue;
                        }
                        string outputFolder = Path.GetDirectoryName(DownloadList[i].saveLocations[j]);
                        if(!Directory.Exists(outputFolder))
                        {
                            Directory.CreateDirectory(outputFolder);
                        }
                        File.Copy(DownloadList[i].saveLocations[0], DownloadList[i].saveLocations[j]);
                    }
                }
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            allFileProgress.PerformStep();
            metroLabel1.Text = "Downloading File " + allFileProgress.Value + " from " + allFileProgress.Maximum;
            if(e != null)
            {
                if(e.Error != null)
                {
                    Console.WriteLine(e.Error.Message + "\n" + e.Error.StackTrace);
                }
                if(e.Cancelled)
                {
                    Console.WriteLine("canceled");
                }
            }
            downloadItem();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            labelDownload.Text = string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"),(e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            singleFileProgress.Value = e.ProgressPercentage;
        }

        private bool checkFile(JCdownloadList file)
        {
            switch(file.hashType)
            {
                case "sha1":
                    FileStream sha1FileCheck = File.OpenRead(file.saveLocations[0]);
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1hash = sha1.ComputeHash(sha1FileCheck);
                    sha1FileCheck.Close();
                    string sha1HashString = BitConverter.ToString(sha1hash).Replace("-", "").ToLower();
                    if(file.hash.Equals(sha1HashString))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                case "md5":
                    FileStream md5FileCheck = File.OpenRead(file.saveLocations[0]);
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] md5hash = md5.ComputeHash(md5FileCheck);
                    md5FileCheck.Close();
                    string md5HashString = BitConverter.ToString(md5hash).Replace("-", "").ToLower();
                    if(file.hash.Equals(md5HashString))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                case null:
                    return true;
                    break;
                default:
                    return true;
                    break;
            }
        }

    }
}
