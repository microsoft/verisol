using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace VeriSolRunner.Tools
{
    internal class DotnetCliToolSourceManager : ToolSourceManager
    {
        internal string DependencyTargetPath
        {
            get
            {
                return this.settings.CommandPath + this.settings.DependencyRelativePath;
            }
        }

        internal DotnetCliToolSourceManager(ToolSourceSettings settings) : base(settings)
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
                Console.WriteLine($"Skip installing tool {this.settings.Name} as we could find it under {this.settings.CommandPath}.");
            }
        }

        private string InstallDotnetCliTool()
        {
            Console.WriteLine($"Installing {this.settings.Name} as we could not find it from {this.settings.CommandPath}.");

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
                Console.WriteLine($"Installation failed: {errorMsg}");
            }
            else
            {
                Console.WriteLine("Done.");
            }

            return output;
        }

        internal void EnsureLinkedToZ3(ToolSourceManager z3)
        {
            var z3DependencyPath = GetZ3DependencyPath(z3);

            if (!File.Exists(z3DependencyPath))
            {
                Console.WriteLine($"Z3 does not exist under {this.settings.Name}");
                Console.WriteLine($"Copying {z3.Command} to {z3DependencyPath}");
                File.Copy(z3.Command, z3DependencyPath);
            }
            else
            {
                Console.WriteLine($"Z3 already exists under {this.settings.Name}");
                Console.WriteLine("Skip copying");
            }
        }

        private string GetZ3DependencyPath(ToolSourceManager z3)
        {
            return this.DependencyTargetPath + z3.ExeName;
        }
    }
}