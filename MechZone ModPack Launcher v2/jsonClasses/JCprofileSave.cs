using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCprofileSave
    {
        public Dictionary<string,profileInfo> profiles { get; set; }
        public string selectedProfile { get; set; }
        public Guid clientToken { get; set; }
        public Dictionary<string,userInfo> authenticationDatabase { get; set; }
    }

    public class userInfo
    {
        public string displayName { get; set; }
        public List<UserProperty> userProperties { get; set; }
        public string accessToken { get; set; }
        public string userid { get; set; }
        public string uuid { get; set; }
        public string username { get; set; }
    }

    public class profileInfo
    {
        public string name { get; set; }
        public string gameDir { get; set; }
        public string javaDir { get; set; }
        public string lastVersionId { get; set; }
        public Resolution resolution { get; set; }
        public List<string> allowedReleaseTypes { get; set; }
        public string javaArgs { get; set; }
        public string playerUUID { get; set; }
        public bool useHopperCrashService { get; set; }
        public string launcherVisibilityOnGameClose { get; set; }
    }

    public class UserProperty
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Resolution
    {
        public int width { get; set; }
        public int height { get; set; }
    }
}
