
namespace SolToBoogieTest
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using VeriSolRunner.ExternalTools;

    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("Usage: SolToBoogieTest <test-dir> [<test-prefix>]");
                Console.WriteLine("\t: if <test-prefix> is specified, we only run subset of tests that have the string as prefix");
                return 1;
            }

            string testDir = args[0];
            string testPrefix = args.Length >= 2 ? args[1] : "";

            ExternalToolsManager.EnsureAllExisted();

            string regressionDir = Path.Combine(testDir, "regressions");
            string configDir = Path.Combine(testDir, "config");
            string recordsDir = Path.Combine(testDir);

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()); // new LoggerFactory().AddConsole(LogLevel.Information);
            ILogger logger = loggerFactory.CreateLogger("SolToBoogieTest.RegressionExecutor");

            if (!testPrefix.Equals(string.Empty))
            {
                Console.WriteLine($"Warning!!! only running tests that have a prefix {testPrefix}....");
            }
            RegressionExecutor executor = new RegressionExecutor(regressionDir, configDir, recordsDir, logger, testPrefix);
            return executor.BatchExecute();
        }

        private static string GetCorralPathFromAssemblyPath(string location)
        {
            // from .\sources\soltoboogietest\bin\debug\netcoreapp2.2\ to .\sources\soltoboogietest\bin\debug\netcoreapp2.2\
            return Path.Combine(new string[] {Directory.GetParent(location).FullName, "corral.dll"});
        }

        private static string GetSolcNameByOSPlatform()
        {
            string solcName = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                solcName = "solc.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                solcName = "solc-static-linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                solcName = "solc";
            }
            else
            {
                throw new SystemException("Cannot recognize OS platform");
            }
            return solcName;
        }
    }
}
