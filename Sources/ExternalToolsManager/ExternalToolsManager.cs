
namespace VeriSolRunner.ExternalTools
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public static class ExternalToolsManager
    {
        private static ILogger logger;

        public static ToolManager Solc { get; private set; }
        public static ToolManager Z3 { get; private set; }
        public static ToolManager Boogie { get; private set; }
        public static ToolManager Corral { get; private set; }

        static ExternalToolsManager()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()); // AddConsole(LogLevel.Information)
            logger = loggerFactory.CreateLogger("VeriSol.ExternalToolsManager");

            IConfiguration toolSourceConfig = new ConfigurationBuilder()
                .AddJsonFile("toolsourcesettings.json", true, true)
                .Build();

            var solcSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("solc").Bind(solcSourceSettings);
            Solc = new SolcManager(solcSourceSettings);

            var z3SourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("z3").Bind(z3SourceSettings);
            Z3 = new DownloadedToolManager(z3SourceSettings);

            var boogieSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("boogie").Bind(boogieSourceSettings);
            Boogie = new DotnetCliToolManager(boogieSourceSettings);

            var corralSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("corral").Bind(corralSourceSettings);
            Corral = new DotnetCliToolManager(corralSourceSettings);
        }

        internal static void Log(string v)
        {
            logger.LogDebug(v);
        }

        public static void EnsureAllExisted()
        {
            Solc.EnsureExisted();

            Z3.EnsureExisted();

            Boogie.EnsureExisted();
            (Boogie as DotnetCliToolManager).EnsureLinkedToZ3(Z3);

            Corral.EnsureExisted();
            (Corral as DotnetCliToolManager).EnsureLinkedToZ3(Z3);
        }
    }
}
