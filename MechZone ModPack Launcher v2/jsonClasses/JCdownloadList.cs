using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCdownloadList
    {
        public string link { get; set; }
        public List<string> saveLocations { get; set; }
        public string hash { get; set; }
        public string hashType { get; set; }
        public string type { get; set; }
    }
}
