using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCmodpackVersion
    {
        public string minecraft { get; set; }
        public string minecraft_md5 { get; set; }
        public string forge { get; set; }
        public string forgeVersion { get; set; }
        public List<Mod> mods { get; set; }
    }

    public class Mod
    {
        public string name { get; set; }
        public string version { get; set; }
        public string md5 { get; set; }
        public string url { get; set; }
    }
}
