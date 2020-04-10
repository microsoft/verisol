

namespace VeriSolRunner
{

    using Microsoft.Extensions.Logging;
    using SolidityAST;
    using BoogieAST;
    using SolToBoogie;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using System.Linq;
    using VeriSolRunner.ExternalTools;
    // using Microsoft.Boogie.ExprExtensions;

    internal class VeriSolExecutor
    {
        private string SolidityFilePath;
        private string SolidityFileDir;
        private string ContractName;
        private string CorralPath;
        private string BoogiePath;
        private string SolcPath;
        private bool TryProof;
        private bool TryRefutation;
        // private bool GenInlineAttrs;
        private ILogger Logger;
        private readonly string outFileName = "__SolToBoogieTest_out.bpl";
        private readonly string corralTraceFileName = "corral_out_trace.txt";
        private readonly string counterexampleFileName = "corral_counterex.txt";
        private readonly int CorralRecursionLimit;
        private readonly int CorralContextBound = 1; // always 1 for solidity
        private HashSet<Tuple<string, string>> ignoreMethods;
        private TranslatorFlags translatorFlags;
        private bool printTransactionSequence = false; 

        public VeriSolExecutor(string solidityFilePath, string contractName, int corralRecursionLimit, HashSet<Tuple<string, string>> ignoreMethods, bool tryRefutation, bool tryProofFlag, ILogger logger, bool _printTransactionSequence, TranslatorFlags _translatorFlags = null)
        {
            this.SolidityFilePath = solidityFilePath;
            this.ContractName = contractName;
            this.SolidityFileDir = Path.GetDirectoryName(solidityFilePath);
            Console.WriteLine($"SpecFilesDir = {SolidityFileDir}");
            this.CorralPath = ExternalToolsManager.Corral.Command;
            this.BoogiePath = ExternalToolsManager.Boogie.Command;
            this.SolcPath = ExternalToolsManager.Solc.Command;
            this.CorralRecursionLimit = corralRecursionLimit;
            this.ignoreMethods = new HashSet<Tuple<string, string>>(ignoreMethods);
            this.Logger = logger;
            this.TryProof = tryProofFlag;
            this.TryRefutation = tryRefutation;
            this.printTransactionSequence = _printTransactionSequence;
            //this.GenInlineAttrs = genInlineAttrs;
            this.translatorFlags = _translatorFlags;
        }

        public int Execute()
        {
            // call SolToBoogie on specFilePath
            if (!ExecuteSolToBoogie())
            {
                return 1;
            }

            // try to prove first
            if (TryProof && FindProof())
            {
                return 0;
            }

            // run Corral on outFile
            if (TryRefutation && !RunCorralForRefutation())
            {
                return 1;
            }

            return 0;
        }

        private bool FindProof()
        {
            var boogieArgs = new List<string>
            {
                //-doModSetAnalysis -inline:spec (was assert) -noinfer -contractInfer -proc:BoogieEntry_* out.bpl
                //
                $"-doModSetAnalysis",
                $"-inline:spec", //was assert to before to fail when reaching recursive functions
                $"-noinfer",
                translatorFlags.PerformContractInferce? $"-contractInfer" : "",
                $"-inlineDepth:{translatorFlags.InlineDepthForBoogie}", //contractInfer can perform inlining as well
                // main method
                $"-proc:BoogieEntry_*",
                // Boogie file
                outFileName
            };

            var boogieArgString = string.Join(" ", boogieArgs);
            Console.WriteLine($"... running {BoogiePath} {boogieArgString}");
            var boogieOut = RunBinary(BoogiePath, boogieArgString);
            var boogieOutFile = "boogie.txt";
            using (var bFile = new StreamWriter(boogieOutFile))
            {
                bFile.Write(boogieOut);
            }
            // Console.WriteLine($"\tFinished Boogie, output in {boogieOutFile}....\n");

            // compare Corral output against expected output
            if (CompareBoogieOutput(boogieOut))
            {
                Console.WriteLine($"\t*** Proof found! Formal Verification successful! (see {boogieOutFile})");
                return true;
            }
            else
            {
                Console.WriteLine($"\t*** Did not find a proof (see {boogieOutFile})");
                return false;
            }
        }

        private bool RunCorralForRefutation()
        {
            var corralArgs = new List<string>
            {
                // recursion bound
                $"/recursionBound:{CorralRecursionLimit}",
                // context bound (k)
                $"/k:{CorralContextBound}",
                // main method
                $"/main:CorralEntry_{ContractName}",
                // printing info
                $"/tryCTrace /printDataValues:1",
                // Boogie file
                outFileName
            };

            var corralArgString = string.Join(" ", corralArgs);
            Console.WriteLine($"... running {CorralPath} {corralArgString}");
            var corralOut = RunBinary(CorralPath, corralArgString);
            var corralOutFile = "corral.txt";
            using (var bFile = new StreamWriter(corralOutFile))
            {
                bFile.Write(corralOut);
            }
            // Console.WriteLine($"\tFinished corral, output in {corralOutFile}....\n");

            // compare Corral output against expected output
            if (CompareCorralOutput("Program has no bugs", corralOut))
            {
                Console.WriteLine($"\t*** Formal Verification successful upto {CorralRecursionLimit} transactions (see {corralOutFile})");
                return true;
            }
            else if (corralOut.Contains("Execution trace:"))
            {
                Console.WriteLine($"\t*** Found a counterexample (see {corralOutFile})");

                if (printTransactionSequence)
                {
                    Console.WriteLine("\t-----Transaction Sequence for Defect ------");
                    PrintCounterexample();
                    Console.WriteLine("\t---------------");
                }

                //Console.WriteLine($"\n\tAlso, see the counterexample in {counterexampleFileName}");

                Console.WriteLine($"\n\tSee full execution trace inside {corralOutFile}");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    DisplayTraceUsingConcurrencyExplorer();
                }
                return false;
            }
            else 
            {
                Console.WriteLine($"\t*** Corral may have aborted abnormally (see {corralOutFile})");
                return false;
            }

        }

        private void DisplayTraceUsingConcurrencyExplorer()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string concExplorerName = "ConcurrencyExplorer.exe";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine($"\tRun the command below to see the trace in a viewer (only supported on Windows):");
                Console.WriteLine($"\t{concExplorerName} {corralTraceFileName}");
            }
        }

        private string[] FilterCorralTrace(string[] trace)
        {
            // Only get lines that contain "Trace: Thread=1":
            var res = trace.Where(s => s.Contains("Trace: Thread=1"));
            // Remove irrelevant lines: "()", "(Done)", "(x = 0)":
            res = res.Where(x => x.Contains("CALL ") || x.Contains("RETURN ") || x.Contains("ASSERTION FAILS ") || x.Contains("_verisolFirstArg"));
            return res.ToArray();
        }

        private List<Tuple<string, string>> ConvertTrace(string[] corralTrace)
        {
            var res = new List<Tuple<string, string>>();
            foreach (string line in corralTrace)
            {
                var strSplit = line.Split("Trace: Thread=1  ");
                // This should never happen; TODO: Debug.Assert(false)?
                if (strSplit.Count() == 0) continue;
                // Strip braces from the 2nd string:
                if (strSplit[1].Length > 2)
                {
                    if (strSplit[1].StartsWith("(ASSERTION FAILS"))
                    {
                        // To avoid splitting the assert (which could contain "=="
                        // in the "ASSERT FAILS" line;
                        strSplit[1] = strSplit[1].Replace(", ", ",");
                    }
                    else if (strSplit[1].StartsWith("(") && strSplit[1].EndsWith(")"))
                    {
                        strSplit[1] = strSplit[1].Substring(1, strSplit[1].Length - 2);
                    }
                }
                var tuples = strSplit[1].Split(", ").ToList();
                tuples.ForEach(x => res.Add(Tuple.Create(strSplit[0], x)));
            }
            return res;
        }
        
        private string ConvertFunctionName(string origName)
        {
            string[] nameSplit = origName.Split("_");
            if (nameSplit.Count() > 2)
            {
                // More than one underscore in the name: leave it as it is:
                //Console.WriteLine($"Name {origName} has more than one underscore and will not be converted into a user-friendly format");
                return origName;
            }
            else if (nameSplit.Count() == 2)
            {
                if (nameSplit[0] == nameSplit[1])
                {
                    // Top level constructor name "XX_XX":
                    return nameSplit[0] + "::Constructor";
                }
                else
                {
                    // Regular function call: functionName_ConstractName
                    return nameSplit[1] + "::" + nameSplit[0];
                }
            }
            else
            {
                Debug.Assert(false, $"Unreachable: Function name {origName} does not contain underscores");
                return String.Empty;
            }
        }

        private void PrintCounterexample()
        {
            string[] corralTrace = File.ReadAllLines("corral.txt");
            // Find relevant trace lines in the full trace:
            corralTrace = FilterCorralTrace(corralTrace);
            // Convert array of strings into a list of tuples:
            var tracePrime = ConvertTrace(corralTrace);
            PrintCounterexampleHelper(tracePrime);
        }

        private void PrintCounterexampleHelper(List<Tuple<string, string>> trace)
        {
            Stack<string> callStack = new Stack<string>();
            string currentArgs = "";
            bool collectArgs = false;
            List<string> resultArray = new List<string>();

            // this is a list of (line#, element)
            // element \in {CALL foo, RETURN from foo, x = e, ASSERTION FAILS, ...}
            for (int i = 0; i < trace.Count(); i++)
            {
                var elem = trace[i].Item2;
                if (elem.StartsWith("CALL CorralChoice_"))
                {
                    continue;
                }
                else if (elem.StartsWith("RETURN from CorralChoice_"))
                {
                    continue;
                }
                else if (elem.StartsWith("CALL "))
                {
                    var func = elem.Substring("CALL ".Length);
                    callStack.Push(func);
                    currentArgs = "";
                }
                else if (elem.StartsWith("RETURN from "))
                {
                    var func = elem.Substring("RETURN from ".Length);
                    Debug.Assert(callStack.Count > 0, "Call stack cannot be empty");
                    Debug.Assert(func.TrimEnd().Equals(callStack.Peek().TrimEnd()), $"Top of stack {callStack.Peek()} does not match with return {func}");
                    callStack.Pop();
                }
                else if (elem.StartsWith("_verisolFirstArg"))
                {
                    collectArgs = true;
                }
                else if (elem.StartsWith("_verisolLastArg"))
                {
                    Debug.Assert(callStack.Count > 0, "callstack cannot be empty");
                    collectArgs = false;
                    if (callStack.Count == 1)
                    {
                        resultArray.Add($"{trace[i].Item1}: {ConvertFunctionName(callStack.Peek())} ({currentArgs.Substring(", ".Length)})");
                    }
                }
                else if (elem.StartsWith("(ASSERTION FAILS"))
                {
                    resultArray.Add($"{trace[i].Item1}: ASSERTION FAILS!");
                }
                else if (elem.Contains("=") && !elem.Contains("=="))
                {

                    if (collectArgs)
                    {
                        // Replace "T@Ref!val!0" with "address!0"
                        string[] splitElem = elem.Split(" = ");
                        Debug.Assert(splitElem.Count() == 2);
                        string[] rhsSplit = splitElem[1].Split("!");
                        if (rhsSplit.Count() > 0 && rhsSplit[0].Equals("T@Ref"))
                        {
                            currentArgs += ", " + elem.Replace("T@Ref!val", "address");
                        }
                        else
                        {
                            currentArgs += ", " + elem;
                        }                       
                    }
                }
                else
                {
                    Debug.Assert(false, $"This should be unreachable, found a new class of statement {elem}");
                }
            }

            resultArray.ForEach(x => Console.WriteLine(x));
            // Only printing counterexample on the command line:
            File.WriteAllLines(counterexampleFileName, resultArray);
            return;
        }

        private bool ExecuteSolToBoogie()
        {
            // compile the program
            Console.WriteLine($"... running Solc on {SolidityFilePath}");

            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(SolcPath, SolidityFilePath);

            if (compilerOutput.ContainsError())
            {
                compilerOutput.PrintErrorsToConsole();
                throw new SystemException("Compilation Error");
            }

            // build the Solidity AST from solc output
            AST solidityAST = new AST(compilerOutput, Path.GetDirectoryName(SolidityFilePath));

            // translate Solidity to Boogie
            try
            {
                BoogieTranslator translator = new BoogieTranslator();
                Console.WriteLine($"... running SolToBoogie to translate Solidity to Boogie");
                BoogieAST boogieAST = translator.Translate(solidityAST, ignoreMethods, translatorFlags, ContractName);

                // dump the Boogie program to a file
                var outFilePath = Path.Combine(SolidityFileDir, outFileName);
                using (var outWriter = new StreamWriter(outFileName))
                {
                    outWriter.WriteLine(boogieAST.GetRoot());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"VeriSol translation error: {e.Message}");
                return false;
            }
            return true;
        }

        private string RunBinary(string cmdName, string arguments)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = cmdName;
            p.StartInfo.Arguments = $"{arguments}";
            p.Start();

            string outputBinary = p.StandardOutput.ReadToEnd();
            string errorMsg = p.StandardError.ReadToEnd();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                Console.WriteLine($"Error: {errorMsg}");
            }
            p.StandardOutput.Close();
            p.StandardError.Close();

            // TODO: should set up a timeout here
            // but it seems there is a problem if we execute corral using mono

            return outputBinary;
        }

        private bool CompareCorralOutput(string expected, string actual)
        {
            if (actual == null)
            {
                return false;
            }
            string[] actualList = actual.Split("Boogie verification time");
            if (actualList.Length == 2)
            {
                if (actualList[0].Contains(expected))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CompareBoogieOutput(string actual)
        {
            if (actual == null)
            {
                return false;
            }
            // Boogie program verifier finished with x verified, 0 errors
            if (actual.Contains("Boogie program verifier finished with ") &&
                actual.Contains(" verified, 0 errors"))
            {
                return true;
            }
            return false;
        }

    }
}