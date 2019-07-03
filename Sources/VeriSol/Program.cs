
namespace VeriSolOutOfBandsSpecsRunner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Top level application to run VeriSol to target proofs as well as scalable counterexamples
    /// </summary>
    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return 1;
            }

            string solcName = GetSolcNameByOSPlatform();
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            ILogger logger = loggerFactory.CreateLogger("VeriSol");

            string specFilePath = args[0];
            string contractName = args[1]; // contract Name, often C_VeriSol
            string contractPath = args[2];
            int corralRecursionBound = int.Parse(args[3]); // need validation

            HashSet<Tuple<string, string>> ignoredMethods = new HashSet<Tuple<string, string>>();
            foreach (var arg in args.Where(x => x.StartsWith("/ignoreMethod:")))
            {
                Debug.Assert(arg.Contains("@"), $"Error: incorrect use of /ignoreMethod in {arg}");
                Debug.Assert(arg.LastIndexOf("@") == arg.IndexOf("@"), $"Error: incorrect use of /ignoreMethod in {arg}");
                var str = arg.Substring("/ignoreMethod:".Length);
                var method = str.Substring(0, str.IndexOf("@"));
                var contract = str.Substring(str.IndexOf("@") + 1);
                ignoredMethods.Add(Tuple.Create(method, contract));
            }
            Console.WriteLine($"Ignored method/contract pairs ==> \n\t {string.Join(",", ignoredMethods.Select(x => x.Item1 + "@" + x.Item2))}");


            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string solcPath = 
                Path.Combine(
                    Directory.GetParent(Path.GetDirectoryName(assemblyLocation)).FullName, 
                    solcName);
            if (!File.Exists(solcPath))
            {
                ShowUsage();
                Console.WriteLine($"Cannot find {solcName} at {solcPath}");
                return 1;
            }

            string corralPath = Path.Combine(
                    Directory.GetParent(Path.GetDirectoryName(assemblyLocation)).FullName,
                    "corral.exe");
            if (!File.Exists(corralPath))
            {
                ShowUsage();
                Console.WriteLine($"Cannot find Corral.exe at {corralPath}");
                return 1;
            }


            var verisolExecuter = 
                new VeriSolExecuterWithSpecs(
                    specFilePath, 
                    contractName, 
                    contractPath, 
                    corralPath, 
                    solcPath, 
                    solcName, 
                    corralRecursionBound, 
                    ignoredMethods,
                    logger);
            return verisolExecuter.Execute();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("VeriSol: Formal specification and verification tool for Solidity smart contracts");
            Console.WriteLine("Usage:  VeriSol <relative-path-to-solidity-file> <top-level-contractName> <relative-path-to-output-file.bpl> <verisol-root> [options]");
            Console.WriteLine("\t options:");
            Console.WriteLine("\t\t /tryProof               \ttry inductive proofs, default: false");
            Console.WriteLine("\t\t /tryRefutation          \ttry to obtain counterexamples, default: false");
            Console.WriteLine("\t\t /recursionBound:k       \tmax number transactions and loop unrollings per transaction, default: 2");
            Console.WriteLine("\t\t /bplPrelude:<foo.bpl>   \tany additional Boogie file to be added for axioms or user-supplied boogie invariants");
            Console.WriteLine("\t\t /ignoreMethod:<method>@<contract>: Ignores translation of the method within contract, and only generates a declaration");
            Console.WriteLine("\t\t\t\t multiple such pairs can be specified, ignored set is the union");
            Console.WriteLine("\t\t\t\t a wild card '*' can be used for method, would mean all the methods of the contract");
            Console.WriteLine("\t\t /noInlineAttrs          \tdo not generate any {:inline x} attributes, to speed Corral (cannot use with /tryProof)");
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
