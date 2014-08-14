using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;
using Newtonsoft.Json;

namespace MechZone_ModPack_Launcher_v2
{
    public partial class AddProfile : MetroForm
    {
        Guid _uuid;
        string _location;

        public Guid SetUuid
        {
            set { _uuid = value; Invalidate(); }
        }

        public string Path
        {
            set { _location = value; Invalidate(); }
        }

        public AddProfile()
        {
            InitializeComponent();
            username.Text = @" ";
            password.Text = @" ";
            profileNameBox.Text = @" ";
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
                    jsonClasses.JCauthenticateResponse res = MainWindow.Authenticate(username.Text, password.Text, _uuid);
                    if(res != null)
                    {
                        jsonClasses.JCprofileSave file = JsonConvert.DeserializeObject<jsonClasses.JCprofileSave>(File.ReadAllText(_location + @"\mz_launcher_profiles.json"));

                        //profile
                        jsonClasses.profileInfo profile = new jsonClasses.profileInfo
                        {
                            name = profileNameBox.Text,
                            playerUUID = res.selectedProfile.id
                        };
                        file.profiles.Add(profileNameBox.Text, profile);

                        //authentication database
                        jsonClasses.userInfo user = new jsonClasses.userInfo
                        {
                            uuid = res.selectedProfile.id,
                            displayName = res.selectedProfile.name,
                            accessToken = res.accessToken,
                            username = username.Text
                        };
                        file.authenticationDatabase.Add(res.selectedProfile.id, user);

                        file.selectedProfile = res.selectedProfile.name;

                        string text = JsonConvert.SerializeObject(file, Formatting.Indented);
                        File.WriteAllText(_location + @"\mz_launcher_profiles.json", text);

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("{0}\n{1}", ex.Message, ex.StackTrace);
                DialogResult = DialogResult.Abort;
                Close();
            }
            
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void addProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
