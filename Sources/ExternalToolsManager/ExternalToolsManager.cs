
namespace VeriSolRunner.ExternalTools
{
    using Microsoft.Extensions.Configuration;

    public static class ExternalToolsManager
    {
        public static ToolManager Solc { get; private set; }
        public static ToolManager Z3 { get; private set; }
        public static ToolManager Boogie { get; private set; }
        public static ToolManager Corral { get; private set; }

        static ExternalToolsManager()
        {
            IConfiguration toolSourceConfig = new ConfigurationBuilder()
                .AddJsonFile("toolsourcesettings.json", true, true)
                .Build();

            var solcSourceSettings = new ToolSourceSettings();
            toolSourceConfig.GetSection("solc").Bind(solcSourceSettings);
            Solc = new DownloadedToolManager(solcSourceSettings);

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
