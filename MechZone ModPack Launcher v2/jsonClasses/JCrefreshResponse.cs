using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCrefreshResponse
    {
        public string accessToken { get; set; }
        public string clientToken { get; set; }
        public SelectedProfile selectedProfile { get; set; }
    }
}
