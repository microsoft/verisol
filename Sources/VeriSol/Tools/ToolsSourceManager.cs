using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace VeriSolRunner.Tools
{
    internal static class ToolsSourceManager
    {
        internal static ToolSourceManager Solc { get; private set; }
        internal static ToolSourceManager Z3 { get; private set; }
        internal static ToolSourceManager Boogie { get; private set; }
        internal static ToolSourceManager Corral { get; private set; }

        static ToolsSourceManager()
        {
            IConfiguration toolSourceConfig = new ConfigurationBuilder()
                .AddJsonFile("toolsourcesettings.json", true, true)
                .Build();

            var solcSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("solc").Bind(solcSourceSettings);
            Solc = new DownloadedToolSourceManager(solcSourceSettings);

            var z3SourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("z3").Bind(z3SourceSettings);
            Z3 = new DownloadedToolSourceManager(z3SourceSettings);

            var boogieSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("boogie").Bind(boogieSourceSettings);
            Boogie = new DotnetCliToolSourceManager(boogieSourceSettings);

            var corralSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("corral").Bind(corralSourceSettings);
            Corral = new DotnetCliToolSourceManager(corralSourceSettings);
        }

        internal static void EnsureAllExisted()
        {
            Solc.EnsureExisted();

            Z3.EnsureExisted();

            Boogie.EnsureExisted();
            (Boogie as DotnetCliToolSourceManager).EnsureLinkedToZ3(Z3);

            Corral.EnsureExisted();
            (Corral as DotnetCliToolSourceManager).EnsureLinkedToZ3(Z3);
        }
    }
}
