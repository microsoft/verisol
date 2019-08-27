

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
    using System.Text.RegularExpressions;
    using System.Linq;

    internal class VeriSolExecutor
    {
        private string SolidityFilePath;
        private string SolidityFileDir;
        private string ContractName;
        private string CorralPath;
        private string BoogiePath;
        private string SolcPath;
        private string SolcName;
        private bool TryProof;
        private bool TryRefutation;
        private bool GenInlineAttrs;
        private ILogger Logger;
        private readonly string outFileName = "__SolToBoogieTest_out.bpl";
        private readonly string corralTraceFileName = "corral_out_trace.txt";
        private readonly string counterexampleFileName = "corral_counterex.txt";
        private readonly int CorralRecursionLimit;
        private readonly int CorralContextBound = 1; // always 1 for solidity
        private HashSet<Tuple<string, string>> ignoreMethods;

        public VeriSolExecutor(string solidityFilePath, string contractName, string corralPath, string boogiePath, string solcPath, string solcName, int corralRecursionLimit, HashSet<Tuple<string, string>> ignoreMethods, bool tryRefutation, bool tryProofFlag, bool genInlineAttrs, ILogger logger)
        {
            this.SolidityFilePath = solidityFilePath;
            this.ContractName = contractName;
            this.SolidityFileDir = Path.GetDirectoryName(solidityFilePath);
            Console.WriteLine($"SpecFilesDir = {SolidityFileDir}");
            this.CorralPath = corralPath;
            this.BoogiePath = boogiePath;
            this.SolcPath = solcPath;
            this.SolcName = solcName;
            this.CorralRecursionLimit = corralRecursionLimit;
            this.ignoreMethods = new HashSet<Tuple<string, string>>(ignoreMethods);
            this.Logger = logger;
            this.TryProof = tryProofFlag;
            this.TryRefutation = tryRefutation;
            this.GenInlineAttrs = genInlineAttrs;
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
                $"-inline:assert",
                $"-noinfer",
                $"-contractInfer",
                // main method
                $"-proc:BoogieEntry_*",
                // Boogie file
                outFileName
            };

            var boogieArgString = string.Join(" ", boogieArgs);
            Console.WriteLine($"\n++ Running {BoogiePath} {boogieArgString} ....");
            var boogieOut = RunBinary(BoogiePath, boogieArgString);
            var boogieOutFile = "boogie.txt";
            using (var bFile = new StreamWriter(boogieOutFile))
            {
                bFile.Write(boogieOut);
            }
            Console.WriteLine($"\tFinished Boogie, output in {boogieOutFile}....\n");

            // compare Corral output against expected output
            if (CompareBoogieOutput(boogieOut))
            {
                Console.WriteLine($"\t*** Proof found! Formal Verification successful!");
                return true;
            }
            else
            {
                Console.WriteLine($"\t*** Did not find a proof");
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
            Console.WriteLine($"\n++ Running {CorralPath} {corralArgString} ....");
            var corralOut = RunBinary(CorralPath, corralArgString);
            var corralOutFile = "corral.txt";
            using (var bFile = new StreamWriter(corralOutFile))
            {
                bFile.Write(corralOut);
            }
            Console.WriteLine($"\tFinished Corral, output in {corralOutFile}....\n");

            // compare Corral output against expected output
            if (CompareCorralOutput("Program has no bugs", corralOut))
            {
                Console.WriteLine($"\t*** Formal Verification successful upto {CorralRecursionLimit} transactions");
                return true;
            }
            else
            {
                Console.WriteLine($"\t*** Found a counterexample:");

                DisplayTraceOnConsole();

                Console.WriteLine($"\tSee full execution trace inside {corralOutFile}");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    DisplayTraceUsingConcurrencyExplorer();
                }
                return false;
            }
        }

        private void DisplayTraceUsingConcurrencyExplorer()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string concExplorerWindowsPath = Path.Combine(
                Path.GetDirectoryName(assemblyLocation),
                "concurrencyExplorer",
                "ConcurrencyExplorer.exe");
            if (!File.Exists(concExplorerWindowsPath))
            {
                throw new Exception($"Cannot find ConcurrencyExplorer.exe at {concExplorerWindowsPath}");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine($"\t{concExplorerWindowsPath} {corralTraceFileName}");
            }
        }

        private string[] FilterCorralTrace(string[] trace)
        {
            // Only get lines that contain ".sol":
            return trace.Where(s => s.Contains(".sol")).ToArray();
        }

        // Problem: processing one line of corral.txt file at a time doesn't work, since CALL
        // and its arguments could be located on diff lines
        // TODO: preprocess corral.txt: append arguments to the lines where their call is; since 
        // the line number for the arguments doesn't matter, this should be OK
        private string[] PreprocessCorralTrace(string[] corralTrace)
        {
            string[] resTrace = null;
            string curFuncName = null;
            string curUnfinishedLine = null;
            
            foreach (string line in corralTrace)
            {
                string[] splitLine = line.Split("Trace: Thread=1  ");
                curUnfinishedLine = splitLine[0];
                string rest = splitLine[1];
                // Strip braces from the "rest" string:
                if (rest.Length > 2)
                {
                    if (rest.StartsWith("(") && rest.EndsWith(")"))
                    {
                        rest = rest.Substring(1, rest.Length - 2);
                    }
                }

                // Split the rest of the line to get separate CALLs/RETURNs/args etc.
                string[] restSplit = splitLine[1].Split(", ");
                if (restSplit.Length  == 1 && !restSplit[0].StartsWith("ASSERTION FAILS") && !restSplit[0].StartsWith("CALL ") &&
                        !restSplit[0].StartsWith("RETURN") && !restSplit[0].StartsWith("this = T@Ref!val"))
                {
                    // Skip the lines like: "(x = 1)", "()", "(Done)", 
                    //curLine = null;
                    curFuncName = null;
                    curUnfinishedLine = null;
                }
                else
                {
                    if (restSplit[0].StartsWith("this = ") && curFuncName != null)
                    {
                        // These are arguments for a function CALL from the previous line:
                        // Append the arguments to the previous line and emit the trace line into the resulting trace:
                        curUnfinishedLine = curUnfinishedLine + ", " + splitLine[1];
                        resTrace.Append(curUnfinishedLine);
                        //curLine = null;
                        curFuncName = null;
                        curUnfinishedLine = null;
                    }
                    else
                    {
                        // restSplit should be included into the resulting trace, unless it's unfinished:
                        ///////////////stopped here:
                        // Find the rightmost CALL element in restSplit and check if it has argument list:
                        int lastCallInd = Array.FindLastIndex(restSplit, elem => elem.StartsWith("CALL "));
                        // TODO: assuming that no other elements except first argument start with "this = T@Ref!val":
                        int lastArgsInd = Array.FindLastIndex(restSplit, elem => elem.StartsWith("this = T@Ref!val"));
                        if (lastCallInd > lastArgsInd)
                        {
                            // This line is unfinished - do not append it to the result:
                            curUnfinishedLine = curUnfinishedLine + ", " + splitLine[1];
                            string lastCallElem = restSplit[lastCallInd];
                            curFuncName = lastCallElem.Substring("CALL ".Length + 1, lastCallElem.Length - 1);
                        }
                        else
                        {
                            // All calls have arguments, append the line to the result:
                            curUnfinishedLine = curUnfinishedLine + ", " + splitLine[1];
                            resTrace.Append(curUnfinishedLine);
                            //curLine = null;
                            curFuncName = null;
                            curUnfinishedLine = null;
                        }
                    }
                }

            }

            return resTrace;
        }
        private string GetArgsInSingleLine(string[] inputArray)
        {
            // Skip any elements that are not arguments (for example: FreshGenerator calls/returns)
            string res = String.Empty;
            bool argsFound = false;
            foreach (string elem in inputArray)
            {
                if (elem.StartsWith("this =") && !argsFound)
                {
                    argsFound = true;
                    res = res + elem + ", ";
                }
                else if (elem.Contains("=") && argsFound)
                {
                    res = res + elem + ", ";
                }
                else if (argsFound)
                {
                    return res;
                }
            }
            return res;
        }
        private void ProcessCall(string input, string output)
        {
            // Example: "CALL withdraw_SimpleDAO"
            string[] inputSplit = input.Split(", ");
            string functionName = input.Substring("CALL ".Length + 1, inputSplit[0].Length);
            if (functionName.StartsWith("Corral_Choice")) return;
            string[] nameSplit = functionName.Split("_");
            if (nameSplit[0] == nameSplit[1] && nameSplit.Length == 1)
            {
                //top level constructor "XX_XX":
                output = output + "CALL " + nameSplit[0] + "::Constructor" + "(";
                // TODO: refactor next 10 lines into a method and use it in both branches of "if":
                var rest = inputSplit.ToList().GetRange(1, inputSplit.Length - 1).ToArray();
                string args = GetArgsInSingleLine(rest);
                if (args == String.Empty)
                {
                    //TODO: ERROR: not expected (arguments are supposed to be on the same line)
                }
                else
                {
                    output = output + args + ")";
                }
            }
            else
            {
                // Regular function call: FunctionName_ConstractName
                // Example: CALL Modifier::plusOne(this = T@Ref!val!0, msg.sender = T@Ref!val!3)
                output = output + "CALL " + "nameSplit[1]" + "::" +  nameSplit[0] + "(";
                var rest = inputSplit.ToList().GetRange(1, inputSplit.Length - 1).ToArray();
                string args = GetArgsInSingleLine(rest);
                if (args == String.Empty)
                {
                    //TODO: ERROR: not expected, print message (arguments are supposed to be on the same line)
                }
                else
                {
                    output = output + args + ")";
                }
            }
        }

        private void ProcessReturn(string input, string output)
        {
            // Example: RETURN from Modifier::plusOne
        }

        private void ProcessArgs(string input, string output)
        {

        }

        private void ProcessTraceElement(string input, string output)
        {
            if (input.StartsWith("CALL"))
            {
                ProcessCall(input, output);
            }
            else if (input.StartsWith("RETURN"))
            {
                ProcessReturn(input, output);
                return;
            }
            else if (input.StartsWith("this = T@Ref!val"))
            {
                ProcessArgs(input, output);
                return;
            }
            else if (input.StartsWith("ASSERTION FAILS"))
            {
                //TODO: print out assertion failure
            }
            else
            {
                // TODO: ERROR: not expected, print message
            }
        }

        private void PrintCounterexample()
        {
            string[] corralTrace = File.ReadAllLines(corralTraceFileName);
            // Find relevant trace lines in the full trace:
            corralTrace = FilterCorralTrace(corralTrace);
            // Preprocess the trace to:
            // - append the line with function argument values to the line with the function call;
            // - clean up the trace from the irrelevant lines
            corralTrace = PreprocessCorralTrace(corralTrace);
            
            using (var outWriter = new StreamWriter(counterexampleFileName))
            {
                foreach (string corralLine in corralTrace)
                {
                    string[] splitRes = corralLine.Split("Trace: Thread=1  ");                  
                    string counterexLine = splitRes[0];
                    string rest = splitRes[1];
                    // Strip braces from the "rest" string:
                    if (rest.Length > 2)
                    {
                        if (rest.StartsWith("(") && rest.EndsWith(")"))
                        {
                            rest = rest.Substring(1, rest.Length - 2);
                        }
                    }
                    string[] restSplit = splitRes[1].Split(", ");
 
                    if (restSplit.Length  > 1)
                    {
                        // multiple calls and returns on the same line, or arguments on a separate line:
                        foreach (string resi in restSplit)
                        {
                            ProcessTraceElement(resi, counterexLine);
                        }
                    }
                    else
                    {
                        ProcessTraceElement(restSplit[0], counterexLine);
                    }
                   
                    outWriter.WriteLine(counterexLine);
                }

                //TODO: write the result into a file "corral_counterex.txt": 
            }
        }
        private void DisplayTraceOnConsole()
        {
            string corralTrace = File.ReadAllText(corralTraceFileName);
            string[] tracePerChoice = corralTrace.Split($"CALL CorralChoice_{ContractName}");  

            foreach(string choiceTrace in tracePerChoice)
            {
                Match functionNameMatch = Regex.Match(choiceTrace, @"(?<=\ )\S*?(?=_)");
                Console.WriteLine($"\t{functionNameMatch.Value}");
            }          
        }

        private bool ExecuteSolToBoogie()
        {
            // compile the program
            Console.WriteLine($"\n++ Running Solc on {SolidityFilePath}....");

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
                Console.WriteLine($"\n++ Running SolToBoogie to translate Solidity to Boogie....");
                BoogieAST boogieAST = translator.Translate(solidityAST, ignoreMethods, GenInlineAttrs);

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

        private string RunBinary(string binaryPath, string binaryArguments)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                p.StartInfo.FileName = binaryPath;
                p.StartInfo.Arguments = binaryArguments;
            }
            else
            {
                p.StartInfo.FileName = "mono";
                p.StartInfo.Arguments = $"{binaryPath} {binaryArguments}";
                Console.WriteLine(p.StartInfo.Arguments);
            }
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