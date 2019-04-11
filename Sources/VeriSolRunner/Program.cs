
namespace VerisolRunnerWithSpecs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: VeriSolRunner <specification.sol> <relative-path-from-specification-to-root-contract.sol> <path-to-corral> <path-to-solc>");
                return;
            }

            string specFilePath = args[0];
            string contractPath = args[1];
            string contractDir = Path.GetDirectoryName(args[1]);
            string corralPath = args[2];
            string solcPath = args[3];
            string solcName = GetSolcNameByOSPlatform();

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            ILogger logger = loggerFactory.CreateLogger("VeriSolRunner");

            var verisolExecuter = new VeriSolExecuterWithSpecs(specFilePath, contractPath, contractDir, corralPath, solcPath, solcName, logger);
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
