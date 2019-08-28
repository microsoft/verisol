
namespace VeriSolRunner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using SolToBoogie;

    /// <summary>
    /// Top level application to run VeriSol to target proofs as well as scalable counterexamples
    /// </summary>
    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                ShowUsage();
                return 1;
            }

            string solidityFile, entryPointContractName, solcName;
            bool tryProofFlag, tryRefutation;
            int recursionBound;
            ILogger logger;
            HashSet<Tuple<string, string>> ignoredMethods;
            bool printTransactionSequence = false;
            TranslatorFlags translatorFlags = new TranslatorFlags();
            ParseCommandLineArgs(args,
                out solidityFile,
                out entryPointContractName,
                out tryProofFlag,
                out tryRefutation,
                out recursionBound,
                out solcName,
                out logger,
                out ignoredMethods,
                out printTransactionSequence, 
                ref translatorFlags);

            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string solcPath = Path.Combine(
                    Path.GetDirectoryName(assemblyLocation),
                    "solc",
                    solcName);
            if (!File.Exists(solcPath))
            {
                ShowUsage();
                Console.WriteLine($"Cannot find {solcName} at {solcPath}");
                return 1;
            }

            string corralPath = Path.Combine(
                    Path.GetDirectoryName(assemblyLocation),
                    "corral",
                    "corral.exe");
            if (!File.Exists(corralPath))
            {
                ShowUsage();
                Console.WriteLine($"Cannot find Corral.exe at {corralPath}");
                return 1;
            }

            string boogiePath = Path.Combine(
                    Path.GetDirectoryName(assemblyLocation),
                    "boogie",
                    "Boogie.exe");
            if (!File.Exists(boogiePath))
            {
                ShowUsage();
                Console.WriteLine($"Cannot find Boogie.exe at {boogiePath}");
                return 1;
            }

            var verisolExecuter =
                new VeriSolExecutor(
                    Path.Combine(Directory.GetCurrentDirectory(), solidityFile), 
                    entryPointContractName,
                    corralPath,
                    boogiePath,
                    solcPath,
                    solcName,
                    recursionBound,
                    ignoredMethods,
                    tryRefutation,
                    tryProofFlag,
                    logger,
                    printTransactionSequence,
                    translatorFlags);
            return verisolExecuter.Execute();
        }

        private static void ParseCommandLineArgs(string[] args, out string solidityFile, out string entryPointContractName, out bool tryProofFlag, out bool tryRefutation, out int recursionBound, out string solcName, out ILogger logger, out HashSet<Tuple<string, string>> ignoredMethods,  out bool printTransactionSeq, ref TranslatorFlags translatorFlags)
        {
            solidityFile = args[0];
            Debug.Assert(!solidityFile.Contains("/"), $"Illegal solidity file name {solidityFile}");
            entryPointContractName = args[1];
            Debug.Assert(!entryPointContractName.Contains("/"), $"Illegal contract name {entryPointContractName}");

            tryProofFlag = args.Any(x => x.Equals("/tryProof"));
            tryRefutation = false;
            recursionBound = 2;
            if (args.Any(x => x.StartsWith("/tryRefutation:")))
            {
                Debug.Assert(!tryRefutation, "Multiple declaration of /tryRefutation:k in command line");
                var arg = args.First(x => x.StartsWith("/tryRefutation:"));
                tryRefutation = true;
                recursionBound = int.Parse(arg.Substring("/tryRefutation:".Length));
            }

            solcName = GetSolcNameByOSPlatform();
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Information);
            logger = loggerFactory.CreateLogger("VeriSol");
            ignoredMethods = new HashSet<Tuple<string, string>>();
            foreach (var arg in args.Where(x => x.StartsWith("/ignoreMethod:")))
            {
                Debug.Assert(arg.Contains("@"), $"Error: incorrect use of /ignoreMethod in {arg}");
                Debug.Assert(arg.LastIndexOf("@") == arg.IndexOf("@"), $"Error: incorrect use of /ignoreMethod in {arg}");
                var str = arg.Substring("/ignoreMethod:".Length);
                var method = str.Substring(0, str.IndexOf("@"));
                var contract = str.Substring(str.IndexOf("@") + 1);
                ignoredMethods.Add(Tuple.Create(method, contract));
            }
            if (args.Any(x => x.StartsWith("/ignoreMethod:")))
            {
                Console.WriteLine($"Ignored method/contract pairs ==> \n\t {string.Join(",", ignoredMethods.Select(x => x.Item1 + "@" + x.Item2))}");
            }
            translatorFlags.GenerateInlineAttributes = true;
            if (args.Any(x => x.Equals("/noInlineAttrs")))
            {
                translatorFlags.GenerateInlineAttributes = false;
                if (tryProofFlag)
                    throw new Exception("/noInlineAttrs cannot be used when /tryProof is used");
            }
            if (args.Any(x => x.Equals("/break")))
            {
                Debugger.Launch();
            }
            if (args.Any(x => x.Equals("/omitSourceLineInfo")))
            {
                translatorFlags.NoSourceLineInfoFlag = true;
            }
            if (args.Any(x => x.Equals("/omitDataValuesInTrace")))
            {
                translatorFlags.NoDataValuesInfoFlag = true;
            }
            if (args.Any(x => x.Equals("/omitUnsignedSemantics")))
            {
                translatorFlags.NoUnsignedAssumesFlag = true;
            }
            if (args.Any(x => x.Equals("/omitAxioms")))
            {
                translatorFlags.NoAxiomsFlag = true;
            }
            if (args.Any(x => x.Equals("/omitHarness")))
            {
                translatorFlags.NoHarness = true;
            }

            // don't perform verification for some of these omitFlags
            if (tryProofFlag || tryRefutation)
            {
                Debug.Assert(!translatorFlags.NoHarness &&
                    !translatorFlags.NoAxiomsFlag &&
                    !translatorFlags.NoUnsignedAssumesFlag &&
                    !translatorFlags.NoDataValuesInfoFlag &&
                    !translatorFlags.NoSourceLineInfoFlag,
                    "Cannot perform verification when any of " +
                    "/omitSourceLineInfo, " +
                    "/omitDataValuesInTrace, " +
                    "/omitAxioms, " +
                    "/omitHarness, " +
                    "/omitUnsignedSemantics are specified");
            }

            printTransactionSeq = args.Any(x => x.Equals("/printTransactionSequence"));
        }

        private static void ShowUsage()
        {
            Console.WriteLine("VeriSol: Formal specification and verification tool for Solidity smart contracts");
            Console.WriteLine("Usage:  VeriSol <relative-path-to-solidity-file> <top-level-contractName> [options]");
            Console.WriteLine("\t options:");
            Console.WriteLine("\t\t /tryProof               \ttry inductive proofs, default: false");
            Console.WriteLine("\t\t /tryRefutation:k        \ttry to obtain counterexamples for upto k transactions, default: false");
            //Console.WriteLine("\t\t /recursionBound:k       \tmax number transactions and loop unrollings per transaction, default: 2");
            Console.WriteLine("\t\t /bplPrelude:<foo.bpl>   \tany additional Boogie file to be added for axioms or user-supplied boogie invariants");
            Console.WriteLine("\t\t /ignoreMethod:<method>@<contract>: Ignores translation of the method within contract, and only generates a declaration");
            Console.WriteLine("\t\t\t\t multiple such pairs can be specified, ignored set is the union");
            Console.WriteLine("\t\t\t\t a wild card '*' can be used for method, would mean all the methods of the contract");
            Console.WriteLine("\t\t /noInlineAttrs          \tdo not generate any {:inline x} attributes, to speed Corral (cannot use with /tryProof)");
            Console.WriteLine("\t\t /outBpl:<out.bpl>       \tpersist the output Boogie file");
            Console.WriteLine("\t\t /printTransactionSequence \tprints the transaction sequence on console (default false)");
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
