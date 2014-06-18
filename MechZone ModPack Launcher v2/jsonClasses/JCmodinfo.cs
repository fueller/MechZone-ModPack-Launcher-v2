using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCmodinfo
    {
        public string name { get; set; }
        public string pretty_name { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public List<string> versions { get; set; }
    }
}
