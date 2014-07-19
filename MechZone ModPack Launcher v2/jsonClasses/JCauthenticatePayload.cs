using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{
    public class JCauthenticatePayload
    {
        public Agent agent { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string clientToken { get; set; }
    }

    public class Agent
    {
        public string name { get; set; }
        public int version { get; set; }
    }
}
