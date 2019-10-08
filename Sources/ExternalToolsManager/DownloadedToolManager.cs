namespace VeriSolRunner.ExternalTools
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Runtime.InteropServices;

    internal class DownloadedToolManager : ToolManager
    {
        private string DownloadURL
        {
            get
            {
                return this.settings.DownloadURLs[OsName];
            }
        }

        private string ExePathWithinZip
        {
            get
            {
                return this.settings.ExePathsWithinZip[OsName];
            }
        }

        private string ZipFileName
        {
            get
            {
                return this.settings.Name + ".zip";
            }
        }

        private string TempDirectory
        {
            get
            {
                return this.settings.CommandPath + "Temp/";
            }
        }

        private string ZipFilePath
        {
            get
            {
                return this.TempDirectory + this.ZipFileName;
            }
        }

        private string ExtractPath
        {
            get
            {
                return this.TempDirectory + this.settings.Name + "/";
            }
        }

        private string ExeTempPath
        {
            get
            {
                return this.ExtractPath + this.ExePathWithinZip;
            }
        }

        internal DownloadedToolManager(ToolSourceSettings settings) : base(settings)
        {
        }

        internal override void EnsureExisted()
        {
            EnsureCommandPathExisted();

            if (!Exists())
            {
                Install();
                ChangePermission();
            }
            else
            {
                ExternalToolsManager.Log($"Skip installing tool {this.settings.Name} as we could find it under {this.settings.CommandPath}.");
            }
        }

        private void ChangePermission()
        {
            if (OsName == "linux" || OsName == "osx")
            {
                RunCmd("chmod", $"+x {Command}");
            }
        }

        private void RunCmd(string cmdName, string arguments)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = cmdName;
            p.StartInfo.Arguments = $"{arguments}";
            p.Start();

            string outputBinary = p.StandardOutput.ReadToEnd();
            string errorMsg = p.StandardError.ReadToEnd();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                ExternalToolsManager.Log($"Error: {errorMsg}");
            }

            ExternalToolsManager.Log(outputBinary);
            p.StandardOutput.Close();
            p.StandardError.Close();
        }

        protected virtual void Install()
        {
            DownloadAndUnZip();
        }

        protected void DownloadAndUnZip()
        {
            PrepareTempDirectory();
            Download();

            ExternalToolsManager.Log($"Extracting {ZipFileName} to {ExtractPath}");
            ZipFile.ExtractToDirectory(ZipFilePath, ExtractPath);

            CopyToCommand(ExeTempPath);

            ExternalToolsManager.Log($"Cleaning up");
            File.Delete(ZipFilePath);
            Directory.Delete(ExtractPath, true);
            ExternalToolsManager.Log($"Done");
        }

        protected void DownloadAndCopy()
        {
            PrepareTempDirectory();
            Download();
            CopyToCommand(ZipFilePath);

            ExternalToolsManager.Log($"Cleaning up");
            File.Delete(ZipFilePath);
            ExternalToolsManager.Log($"Done");
        }

        protected void CreateSymbolicLink()
        {
            if (OsName == "osx")
            {
                RunCmd("ln", $"-s {DownloadURL} {Command}");
            }
        }

        private void PrepareTempDirectory()
        {
            if (!Directory.Exists(TempDirectory))
            {
                ExternalToolsManager.Log($"Creating temporary directory {TempDirectory}");
                Directory.CreateDirectory(TempDirectory);
            }
            else
            {
                if (File.Exists(ZipFilePath))
                {
                    ExternalToolsManager.Log($"Deleting file {ZipFilePath}");
                    File.Delete(ZipFilePath);
                }

                if (Directory.Exists(ExtractPath))
                {
                    ExternalToolsManager.Log($"Deleting directory {ExtractPath}");
                    Directory.Delete(ExtractPath, true);
                }
            }
        }

        private void CopyToCommand(string exeTempPath)
        {
            ExternalToolsManager.Log($"Copying {exeTempPath} to {Command}");
            File.Copy(exeTempPath, Command);
        }

        private void Download()
        {
            using (var client = new WebClient())
            {
                var logStr = $"... Downloading {ZipFileName} from {DownloadURL} to {TempDirectory}";
                Console.WriteLine(logStr); // until we have better verbosity
                ExternalToolsManager.Log(logStr);
                client.DownloadFile(DownloadURL, ZipFilePath);
            }
        }
    }
}