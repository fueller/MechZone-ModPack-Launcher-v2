using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCauthenticateResponse
    {
        public string accessToken { get; set; }
        public string clientToken { get; set; }
        public List<AvailableProfile> availableProfiles { get; set; }
        public SelectedProfile selectedProfile { get; set; }
    }

    public class AvailableProfile
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool legacy { get; set; }
    }

    public class SelectedProfile
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool legacy { get; set; }
    }
}
