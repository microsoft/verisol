
namespace SolToBoogieTest
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Usage: SolToBoogieTest <path-to-corral> <test-dir> [<test-prefix>]");
                Console.WriteLine("\t: if <test-prefix> is specified, we only run subset of tests that have the string as prefix");
                return;
            }

            string corralPath = args[0];
            string testDir = args[1];
            string testPrefix = args.Length >= 3 ? args[2] : ""; 
            string solcName = GetSolcNameByOSPlatform();
            //string solcPath = Path.Combine(workingDirectory, "Tool", solcName);
            string solcPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), solcName);
            string regressionDir = Path.Combine(testDir, "regressions");
            string configDir = Path.Combine(testDir, "config");
            string recordsDir = Path.Combine(testDir);

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            ILogger logger = loggerFactory.CreateLogger("SolToBoogieTest.RegressionExecutor");

            if (!testPrefix.Equals(string.Empty))
            {
                Console.WriteLine($"Warning!!! only running tests that have a prefix {testPrefix}....");
            }
            RegressionExecutor executor = new RegressionExecutor(solcPath, corralPath, regressionDir, configDir, recordsDir, logger, testPrefix);
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
