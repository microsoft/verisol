namespace VeriSolRunner.ExternalTools
{
    using System.IO;

    public abstract class ToolManager
    {
        protected readonly ToolSourceSettings settings;

        public string ExeName
        {
            get
            {
                return this.settings.Name + ".exe";
            }
        }

        public string Command
        {
            get
            {
                return this.settings.CommandPath + this.ExeName;
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
