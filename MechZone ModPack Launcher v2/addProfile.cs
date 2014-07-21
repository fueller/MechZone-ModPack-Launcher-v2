using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using MechZone_ModPack_Launcher_v2;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class addProfile : MetroForm
    {
        Guid uuid;
        string location;

        public Guid setUuid
        {
            set { uuid = value; Invalidate(); }
        }

        public string path
        {
            set { location = value; Invalidate(); }
        }

        public addProfile()
        {
            InitializeComponent();
            username.Text = " ";
            password.Text = " ";
            profileNameBox.Text = " ";
        }

        private void addProfile_Shown(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            profileNameBox.Text = "";
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            profileNameBox.Text = username.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty(username.Text) && !String.IsNullOrEmpty(password.Text) && !String.IsNullOrEmpty(profileNameBox.Text))
                {
                    jsonClasses.JCauthenticateResponse res = mainWindow.authenticate(username.Text, password.Text, uuid);
                    if(res != null)
                    {
                        jsonClasses.JCprofileSave file = JsonConvert.DeserializeObject<jsonClasses.JCprofileSave>(File.ReadAllText(location + @"\mz_launcher_profiles.json"));

                        //profile
                        jsonClasses.profileInfo profile = new jsonClasses.profileInfo();
                        profile.name = profileNameBox.Text;
                        profile.playerUUID = res.selectedProfile.id;
                        file.profiles.Add(profileNameBox.Text, profile);

                        //authentication database
                        jsonClasses.userInfo user = new jsonClasses.userInfo();
                        user.uuid = res.selectedProfile.id;
                        user.displayName = res.selectedProfile.name;
                        user.accessToken = res.accessToken;
                        user.username = username.Text;
                        file.authenticationDatabase.Add(res.selectedProfile.id, user);

                        file.selectedProfile = res.selectedProfile.name;

                        string text = JsonConvert.SerializeObject(file, Formatting.Indented);
                        File.WriteAllText(location + @"\mz_launcher_profiles.json", text);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
            
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void addProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
