namespace VeriSolRunner.ExternalTools
{
    using System;
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
        public string CommandPath { get; set; } = GetDefaultPath();

        private static string GetDefaultPath()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(ToolSourceSettings)).Location);
            return assemblyPath.Split(new string[] { @".store" }, StringSplitOptions.None)[0];
        }
    }
}