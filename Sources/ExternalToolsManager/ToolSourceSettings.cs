namespace VeriSolRunner.ExternalTools
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class ToolSourceSettings
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public Dictionary<string, string> DownloadURLs { get; set; }
        public Dictionary<string, string> ExePathsWithinZip { get; set; }
        public string DependencyRelativePath { get; set; }
        public string CommandPath { get; set; } = Path.GetDirectoryName(Assembly.GetAssembly(typeof(ToolSourceSettings)).Location) +  "/Tools/";
    }
}