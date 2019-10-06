
namespace VeriSolRunner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using SolToBoogie;
    using VeriSolRunner.ExternalTools;

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

            ExternalToolsManager.EnsureAllExisted();

            string solidityFile, entryPointContractName;
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
                out logger,
                out ignoredMethods,
                out printTransactionSequence, 
                ref translatorFlags);

            var verisolExecuter =
                new VeriSolExecutor(
                    Path.Combine(Directory.GetCurrentDirectory(), solidityFile), 
                    entryPointContractName,
                    recursionBound,
                    ignoredMethods,
                    tryRefutation,
                    tryProofFlag,
                    logger,
                    printTransactionSequence,
                    translatorFlags);
            return verisolExecuter.Execute();
        }

        private static void ParseCommandLineArgs(string[] args, out string solidityFile, out string entryPointContractName, out bool tryProofFlag, out bool tryRefutation, out int recursionBound, out ILogger logger, out HashSet<Tuple<string, string>> ignoredMethods,  out bool printTransactionSeq, ref TranslatorFlags translatorFlags)
        {
            Console.WriteLine($"Command line args = {{{string.Join(", ", args.ToList())}}}");
            solidityFile = args[0];
            // Debug.Assert(!solidityFile.Contains("/"), $"Illegal solidity file name {solidityFile}"); //the file name can be foo/bar/baz.sol
            entryPointContractName = args[1];
            Debug.Assert(!entryPointContractName.Contains("/"), $"Illegal contract name {entryPointContractName}");

            tryProofFlag = !(args.Any(x => x.Equals("/noPrf")) || args.Any(x => x.Equals("/noChk"))); //args.Any(x => x.Equals("/tryProof"));
            tryRefutation = !args.Any(x => x.Equals("/noChk"));
            recursionBound = 4;
            var txBounds = args.Where(x => x.StartsWith("/txBound:"));
            if (txBounds.Count() > 0)
            {
                Debug.Assert(txBounds.Count() == 1, $"At most 1 /txBound:k expected, found {txBounds.Count()}");
                recursionBound = int.Parse(txBounds.First().Substring("/txBound:".Length));
                Debug.Assert(recursionBound > 0, $"Argument of /txBound:k should be positive, found {recursionBound}");
            }

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

            if (args.Any(x => x.Equals("/modelReverts")))
            {
                translatorFlags.ModelReverts = true;
            }

            if (args.Any(x => x.Equals("/instrumentGas")))
            {
                translatorFlags.InstrumentGas = true;
            }

            var stubModels = args.Where(x => x.StartsWith("/stubModel:"));
            if (stubModels.Count() > 0)
            {
                Debug.Assert(stubModels.Count() == 1, "Multiple instances of /stubModel:");
                var model = stubModels.First().Substring("/stubModel:".Length);
                Debug.Assert(model.Equals("skip") || model.Equals("havoc") || model.Equals("callback"),
                    $"The argument to /stubModel: can be either {{skip, havoc, callback}}, found {model}");
                translatorFlags.ModelOfStubs = model;
            }
            if (args.Any(x => x.StartsWith("/inlineDepth:")))
            {
                var depth = args.Where(x => x.StartsWith("/inlineDepth:")).First();
                translatorFlags.InlineDepthForBoogie = int.Parse(depth.Substring("/inlineDepth:".Length));
            }
            if (args.Any(x => x.Equals("/doModSet")))
            {
                translatorFlags.DoModSetAnalysis = true;
            }

            translatorFlags.PerformContractInferce = args.Any(x => x.StartsWith("/contractInfer")) ;

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

            printTransactionSeq = !args.Any(x => x.Equals("/noTxSeq"));
        }

        private static void ShowUsage()
        {
            Console.WriteLine("VeriSol: Formal specification and verification tool for Solidity smart contracts");
            Console.WriteLine("Usage:  VeriSol <relative-path-to-solidity-file> <top-level-contractName> [options]");
            Console.WriteLine("options:");

            // Console.WriteLine("\n------ Controls input/output files --------\n");

            // Console.WriteLine("   /outBpl:<out.bpl>        persist the output Boogie file");
            // Console.WriteLine("   /bplPrelude:<foo.bpl>    any additional Boogie file to be added for axioms or user-supplied boogie invariants");


            Console.WriteLine("\n------ Controls verification flags --------\n");

            Console.WriteLine("   /noChk                  don't perform verification, default: false");
            Console.WriteLine("   /noPrf                  don't perform inductive verification, default: false");
            Console.WriteLine("   /txBound:k              only explore counterexamples with at most k transactions/loop unrollings, default: 4");
            Console.WriteLine("   /noTxSeq                don't print the transaction sequence on console, default: false");
            Console.WriteLine("   /contractInfer          perform Houdini based module invariant inference, default off");
            Console.WriteLine("   /inlineDepth:k          inline nested calls upto depth k when performing modular proof and inference, default 4");


            Console.WriteLine("\n------ Controls translation --------\n");

            Console.WriteLine("   /ignoreMethod:<method>@<contract>: Ignores translation of the method within contract, and only generates a declaration");
            Console.WriteLine("                           multiple such pairs can be specified, ignored set is the union");
            Console.WriteLine("                           a wild card '*' can be used for method, would mean all the methods of the contract");
            Console.WriteLine("   /noInlineAttrs          do not generate any {:inline x} attributes, to speed Corral (cannot use with /tryProof)");
            Console.WriteLine("   /stubModel:<s>          the model of an unknown procedure or fallback. <s> can be either");
            Console.WriteLine("                           skip      // treated as noop");
            Console.WriteLine("                           havoc     // completely scramble the entire global state");
            Console.WriteLine("                           callback  // treated as a non-deterministic callback into any of the methods of any contract");

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
