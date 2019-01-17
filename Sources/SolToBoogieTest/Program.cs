
namespace SolToBoogieTest
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: SolToBoogieTest <path-to-corral> <workingdir>");
                return;
            }

            string corralPath = args[0];
            string workingDirectory = args[1];
            string solcName = GetSolcNameByOSPlatform();
            string solcPath = Path.Combine(workingDirectory, "Tool", solcName);
            string testDir = Path.Combine(workingDirectory, "Test", "regression");
            string configDir = Path.Combine(workingDirectory, "Test", "config");

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            ILogger logger = loggerFactory.CreateLogger("SolToBoogieTest.RegressionExecutor");

            RegressionExecutor executor = new RegressionExecutor(solcPath, corralPath, testDir, configDir, logger);
            executor.BatchExecute();
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
                solcName = "solc-mac";
            }
            else
            {
                throw new SystemException("Cannot recognize OS platform");
            }
            return solcName;
        }
    }
}
