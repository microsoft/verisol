namespace VeriSolRunner.ExternalTools
{
    using System.IO;
    using System.Runtime.InteropServices;

    public abstract class ToolManager
    {
        protected static string OsName
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "windows";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "osx";
                }
                else
                {
                    return "linux";
                }
            }
        }
        protected readonly ToolSourceSettings settings;

        public string ExeName
        {
            get
            {
                if (OsName == "windows")
                {
                   return this.settings.Name + ".exe";
                }
                else
                {
                    return this.settings.Name;
                }
            }
        }

        public string Command
        {
            get
            {
                return Path.Combine(this.settings.CommandPath, this.ExeName);
            }
        }

        internal ToolManager(ToolSourceSettings settings)
        {
            this.settings = settings;
        }

        internal abstract void EnsureExisted();

        protected bool Exists()
        {
            return File.Exists(this.Command);
        }

        protected void EnsureCommandPathExisted()
        {
            if (!Directory.Exists(this.settings.CommandPath))
            {
                Directory.CreateDirectory(this.settings.CommandPath);
            }
        }
    }
}
