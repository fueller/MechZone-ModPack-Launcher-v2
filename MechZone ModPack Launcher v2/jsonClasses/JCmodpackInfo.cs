using System.Collections.Generic;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCmodpackInfo
    {
        public string name { get; set; }
        public string display_name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string icon_md5 { get; set; }
        public string logo { get; set; }
        public string logo_md5 { get; set; }
        public string background { get; set; }
        public string background_md5 { get; set; }
        public string recommended { get; set; }
        public string latest { get; set; }
        public List<string> builds { get; set; }
    }
}
