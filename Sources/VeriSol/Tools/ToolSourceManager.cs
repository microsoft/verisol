using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VeriSolRunner.Tools
{
    internal abstract class ToolSourceManager
    {
        protected readonly ToolSourceSettings settings;

        internal string ExeName
        {
            get
            {
                return this.settings.Name + ".exe";
            }
        }

        internal string Command
        {
            get
            {
                return this.settings.CommandPath + this.ExeName;
            }
        }

        internal ToolSourceManager(ToolSourceSettings settings)
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
