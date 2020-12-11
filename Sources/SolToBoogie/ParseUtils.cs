using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BoogieAST;

namespace SolToBoogie
{
    /// <summary>
    /// Helper class for parsing command line arguments
    /// </summary>
    public static class ParseUtils
    {
        // TODO: extract into a VerificationFlags structure 
        public static void ParseCommandLineArgs(string[] args, out string solidityFile, out string entryPointContractName, out bool tryProofFlag, out bool tryRefutation, out int recursionBound, out ILogger logger, out HashSet<Tuple<string, string>> ignoredMethods, out bool printTransactionSeq, ref TranslatorFlags translatorFlags)
        {
            //Console.WriteLine($"Command line args = {{{string.Join(", ", args.ToList())}}}");
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

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()); //  new LoggerFactory().AddConsole(LogLevel.Information);
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

            foreach (var arg in args.Where(x => x.StartsWith("/SliceFunctions:")))
            {
                var str = arg.Substring("/SliceFunctions:".Length);
                String[] fns = str.Split(",");
                translatorFlags.PerformFunctionSlice = true;
                foreach (String fn in fns)
                {
                    translatorFlags.SliceFunctionNames.Add(fn);
                }
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
            if (args.Any(x => x.Equals("/useModularArithmetic")))
            {
                translatorFlags.UseModularArithmetic = true;
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

            if (args.Any(x => x.Equals("/omitBoogieHarness")))
            {
                translatorFlags.NoBoogieHarness = true;
            }

            if (args.Any(x => x.Equals("/createMainHarness")))
            {
                translatorFlags.CreateMainHarness = true;
            }

            if (args.Any(x => x.Equals("/noCustomTypes")))
            {
                translatorFlags.NoCustomTypes = true;
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
                Debug.Assert(model.Equals("skip") || model.Equals("havoc") || model.Equals("callback") || model.Equals("multipleCallbacks"),
                    $"The argument to /stubModel: can be either {{skip, havoc, callback, multipleCallbacks}}, found {model}");
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
            if (args.Any(x => x.Equals("/removeScopeInVarName")))
            {
                // do not document this option, only needed for equivalence checking in Symdiff
                translatorFlags.RemoveScopeInVarName = true;
            }

            if (args.Any(x => x.Equals("/LazyNestedAlloc")))
            {
                translatorFlags.LazyNestedAlloc = true;
            }

            if (args.Any(x => x.Equals("/LazyAllocNoMod")))
            {
                translatorFlags.LazyNestedAlloc = true;
                translatorFlags.LazyAllocNoMod = true;
                translatorFlags.QuantFreeAllocs = true;
            }

            if (args.Any(x => x.Equals("/OmitAssumeFalseForDynDispatch")))
            {
                translatorFlags.OmitAssumeFalseForDynDispatch = true;
            }

            if (args.Any(x => x.Equals("/QuantFreeAllocs")))
            {
                translatorFlags.QuantFreeAllocs = true;
                
                // Turn LazyNestedAlloc on by default if QuantFreeAllocs is provided.
                if (!translatorFlags.LazyNestedAlloc)
                    translatorFlags.LazyNestedAlloc = true;
            }

            if (args.Any(x => x.Equals("/allowTxnsFromContract")))
            {
                translatorFlags.NoTxnsFromContract = false;
            }

            if (args.Any(x => x.Equals("/instrumentSums")))
            {
                translatorFlags.InstrumentSums = true;
            }

            if (args.Any(x => x.Equals("/alias")))
            {
                translatorFlags.RunAliasAnalysis = true;
            }
            
            if (args.Any(x => x.Equals("/useMultiDim")))
            {
                translatorFlags.RunAliasAnalysis = true;
                translatorFlags.UseMultiDim = true;
            }

            if (args.Any(x => x.Equals("/txnsOnFields")))
            {
                translatorFlags.TxnsOnFields = true;
            }

            if (args.Any(x => x.Equals("/noNonlinearArith")))
            {
                translatorFlags.NoNonlinearArith = true;
            }

            if (args.Any(x => x.Equals("/useNumericOperators")))
            {
                translatorFlags.UseNumericOperators = true;
                BoogieBinaryOperation.USE_ARITH_OPS = true;
            }

            if (args.Any(x => x.Equals("/prePostHarness")))
            {
                translatorFlags.PrePostHarness = true;
            }

            if (args.Any(x => x.Equals("/generateGetters")))
            {
                translatorFlags.GenerateGetters = true;
            }

            if (args.Any(x => x.Equals("/generateERC20Spec")))
            {
                translatorFlags.GenerateERC20Spec = true;
            }

            if (args.Any(x => x.Equals("/modelAssemblyAsHavoc")))
            {
                translatorFlags.AssemblyAsHavoc = true;
            }

            translatorFlags.PerformContractInferce = args.Any(x => x.StartsWith("/contractInfer"));

            // don't perform verification for some of these omitFlags
            if (tryProofFlag || tryRefutation)
            {
                Debug.Assert(!translatorFlags.NoHarness &&
                    !translatorFlags.NoAxiomsFlag &&
                    !translatorFlags.NoUnsignedAssumesFlag &&
                    !translatorFlags.NoDataValuesInfoFlag &&
                    !translatorFlags.NoSourceLineInfoFlag &&
                    !translatorFlags.RemoveScopeInVarName,
                    "Cannot perform verification when any of " +
                    "/omitSourceLineInfo, " +
                    "/omitDataValuesInTrace, " +
                    "/omitAxioms, " +
                    "/omitHarness, " +
                    "/omitUnsignedSemantics, " +
                    "/removeScopeInVarName are specified");
            }

            printTransactionSeq = !args.Any(x => x.Equals("/noTxSeq"));
        }

    }
}
