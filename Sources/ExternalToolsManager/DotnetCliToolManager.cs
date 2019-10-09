namespace VeriSolRunner.ExternalTools
{
    using System;
    using System.Diagnostics;
    using System.IO;

    internal class DotnetCliToolManager : ToolManager
    {
        internal string DependencyTargetPath
        {
            get
            {
                return Path.Combine(this.settings.CommandPath, this.settings.DependencyRelativePath);
            }
        }

        internal DotnetCliToolManager(ToolSourceSettings settings) : base(settings)
        {
        }

        internal override void EnsureExisted()
        {
            EnsureCommandPathExisted();
            if (!Exists())
            {
                InstallDotnetCliTool();
            }
            else
            {
                ExternalToolsManager.Log($"Skip installing tool {this.settings.Name} as we could find it under {this.settings.CommandPath}.");
            }
        }

        private string InstallDotnetCliTool()
        {
            var logStr = $"... Installing {this.settings.Name} as we could not find it from {this.settings.CommandPath}.";
            Console.WriteLine(logStr); // until we have better verbosity for printing
            ExternalToolsManager.Log(logStr);

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "dotnet";
            p.StartInfo.Arguments = $"tool install {this.settings.Name} --tool-path {this.settings.CommandPath} --version {this.settings.Version}";
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            string errorMsg = p.StandardError.ReadToEnd();

            p.StandardOutput.Close();
            p.StandardError.Close();

            if (!String.IsNullOrEmpty(errorMsg))
            {
                ExternalToolsManager.Log($"Installation failed: {errorMsg}");
            }
            else
            {
                ExternalToolsManager.Log("Done.");
            }

            return output;
        }

        internal void EnsureLinkedToZ3(ToolManager z3)
        {
            var z3DependencyPath = GetZ3DependencyPath(z3);

            // Workaround: Boogie and Corral are looking for z3.exe, even on linux/mac
            if (!z3DependencyPath.EndsWith(".exe"))
            {
                z3DependencyPath += ".exe";
            }

            if (!File.Exists(z3DependencyPath))
            {
                ExternalToolsManager.Log($"Z3 does not exist under {this.settings.Name}");
                ExternalToolsManager.Log($"Copying {z3.Command} to {z3DependencyPath}");
                File.Copy(z3.Command, z3DependencyPath);
            }
            else
            {
                ExternalToolsManager.Log($"Z3 already exists under {this.settings.Name}");
                ExternalToolsManager.Log("Skip copying");
            }
        }

        private string GetZ3DependencyPath(ToolManager z3)
        {
            return this.DependencyTargetPath + z3.ExeName;
        }
    }
}