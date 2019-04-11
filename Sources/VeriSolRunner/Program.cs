
namespace VeriSolOutOfBandsSpecsRunner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Running VeriSol when the specifications are provided out of band in a separate Solidity file
    /// </summary>
    class Program
    {

        public static int Main(string[] args)
        {
            if (args.Length < 6)
            {
                Console.WriteLine("Usage:  VeriSolOutOfBandsSpecsRunner <specification.sol> <contractName> <relative-path-from-specification-to-root-contract.sol> <path-to-corral> <path-to-solc> <recusion-bound>");
                return 1;
            }

            string specFilePath = args[0];
            string contractName = args[1]; // contract Name, often C_VeriSol
            string contractPath = args[2];
            string corralPath = args[3];
            string solcPath = args[4];
            int corralRecursionBound = int.Parse(args[5]); // need validation
            string solcName = GetSolcNameByOSPlatform();

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            ILogger logger = loggerFactory.CreateLogger("VeriSolRunner");

            var verisolExecuter = 
                new VeriSolExecuterWithSpecs(
                    specFilePath, 
                    contractName, 
                    contractPath, 
                    corralPath, 
                    solcPath, 
                    solcName, 
                    corralRecursionBound, 
                    logger);
            try
            {
                return verisolExecuter.Execute();
            }
            catch (Exception e)
            {
                logger.LogError($"VeriSol did not run successfully {e.Message}");
                return 1;
            }
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
