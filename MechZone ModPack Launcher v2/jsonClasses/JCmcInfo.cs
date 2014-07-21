using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechZone_ModPack_Launcher_v2.jsonClasses
{

    public class Natives
    {
        public string linux { get; set; }
        public string windows { get; set; }
        public string osx { get; set; }
    }

    public class Install
    {
        public string profileName { get; set; }
        public string target { get; set; }
        public string path { get; set; }
        public string version { get; set; }
        public string filePath { get; set; }
        public string welcome { get; set; }
        public string minecraft { get; set; }
        public string mirrorList { get; set; }
        public string logo { get; set; }
    }

    public class Extract
    {
        public List<string> exclude { get; set; }
    }

    public class Os
    {
        public string name { get; set; }
    }

    public class Rule
    {
        public string action { get; set; }
        public Os os { get; set; }
    }

    public class Library
    {
        public string name { get; set; }
        public string url { get; set; }
        public bool? serverreq { get; set; }
        public List<string> checksums { get; set; }
        public bool? clientreq { get; set; }
        public Natives natives { get; set; }
        public Extract extract { get; set; }
        public List<Rule> rules { get; set; }
    }

    public class VersionInfo
    {
        public string id { get; set; }
        public string time { get; set; }
        public string releaseTime { get; set; }
        public string type { get; set; }
        public string minecraftArguments { get; set; }
        public int minimumLauncherVersion { get; set; }
        public string assets { get; set; }
        public List<Library> libraries { get; set; }
        public string mainClass { get; set; }
    }

    public class JCmcInfo
    {
        public Install install { get; set; }
        public VersionInfo versionInfo { get; set; }
    }

}
