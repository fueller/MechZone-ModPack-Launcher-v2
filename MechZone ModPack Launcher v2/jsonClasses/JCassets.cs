using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    class JCassets
    {
        public bool @virtual { get; set; }
        public Dictionary<string, hs> objects { get; set; }
    }

    class hs
    {
        public string hash { get; set; }
        public int size { get; set; }
    }
}
