
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
            SolToBoogie.ParseUtils.ParseCommandLineArgs(args,
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
            Console.WriteLine("   /useModularArithmetic   uses modular arithmetic for unsigned integers (experimental), default unbounded integers");

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
