

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

        public VeriSolExecutor(string solidityFilePath, string contractName, string corralPath, string boogiePath, string solcPath, string solcName, int corralRecursionLimit, HashSet<Tuple<string, string>> ignoreMethods, bool tryRefutation, bool tryProofFlag, ILogger logger, bool _printTransactionSequence, TranslatorFlags _translatorFlags = null)
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
                Console.WriteLine($"\t*** Found a counterexample");

                if (printTransactionSequence)
                {
                    Console.WriteLine("\t-----Transaction Sequence for Defect ------");
                    PrintCounterexample();
                    Console.WriteLine("\t---------------");
                }
                // DisplayTraceOnConsole();

                Console.WriteLine($"\n\tSee full execution trace inside {corralOutFile}");

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
            return trace.Where(s => s.Contains("Trace: Thread=1")).ToArray();
        }

        // Problem: processing one line of corral.txt file at a time doesn't work, since CALL
        // and its arguments could be located on diff lines
        // Preprocess corral.txt: append arguments to the lines where their call is; since 
        // the line number for the arguments doesn't matter, this should be OK
        private string[] PreprocessCorralTrace(string[] corralTrace)
        {
            List<string> resTrace = new List<string>();
            string curFuncName = null;
            string curUnfinishedLine = null;

            // TODO: CHECK IF WE ARE REMOVING relevant LINES
            // skip lines that do not start with {CALL, RETURN, ASSERTION}
            // skip lines that start with {CALL CorralChoice_}
            List<string> tmpTrace = corralTrace.Where(x => (x.Contains("CALL ") || x.Contains("RETURN ") || x.Contains("ASSERTION FAILS "))).ToList();
            tmpTrace = tmpTrace.Where(x => !x.Contains("CALL CorralChoice_")).ToList();

            foreach (string line in tmpTrace)
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
                string[] restSplit = rest.Split(", ");
                if (restSplit.Length  == 1 && !restSplit[0].StartsWith("ASSERTION FAILS") && !restSplit[0].StartsWith("CALL ") &&
                        !restSplit[0].StartsWith("RETURN") && !restSplit[0].StartsWith("this = T@Ref!val"))
                {
                    // Skip the lines like: "(x = 1)", "()", "(Done)", 
                    //curLine = null;
                    curFuncName = null;
                    curUnfinishedLine = null;
                    continue;
                }
                else
                {
                    if (restSplit[0].StartsWith("this = ") && curFuncName != null)
                    {
                        // These are arguments for a function CALL from the previous line:
                        // Append the arguments to the previous line and emit the trace line into the resulting trace:
                        curUnfinishedLine = curUnfinishedLine + ", " + splitLine[1];
                        resTrace.Add(curUnfinishedLine);
                        //curLine = null;
                        curFuncName = null;
                        curUnfinishedLine = null;
                        continue;
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
                            curFuncName = lastCallElem.Substring("CALL ".Length + 1);
                            continue;
                        }
                        else
                        {
                            // All calls have arguments, append the line to the result:
                            curUnfinishedLine = curUnfinishedLine + ", " + splitLine[1];
                            resTrace.Add(curUnfinishedLine);
                            //curLine = null;
                            curFuncName = null;
                            curUnfinishedLine = null;
                        }
                    }
                }

            }

            return resTrace.ToArray();
        }

        private string[] PreprocessCorralTraceForDemo(string[] corralTrace)
        {
            List<string> resTrace = new List<string>();

            // TODO: CHECK IF WE ARE REMOVING relevant LINES
            // skip lines that do not start with {CALL, RETURN, ASSERTION}
            // skip lines that start with {CALL CorralChoice_}
            List<string> tmpTrace = corralTrace.Where(x => (x.Contains("CALL ") || x.Contains("RETURN ") || x.Contains("ASSERTION FAILS "))).ToList();
            tmpTrace = tmpTrace.Where(x => !x.Contains("CALL CorralChoice_")).ToList();

            string foundBottomCallOnStack = null;
            foreach (string line in tmpTrace)
            {
                string[] splitLine = line.Split("Trace: Thread=1  ");
                string curUnfinishedLine = splitLine[0];
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
                string[] restSplit = rest.Split(", ");
                List<string> args = new List<string>();
                if (foundBottomCallOnStack == null)
                {
                    if (restSplit[0].StartsWith("CALL "))
                    {
                        foundBottomCallOnStack = restSplit[0].Substring("CALL ".Length);
                        for(int i = 1; i < restSplit.Length; ++i)
                        {
                            if (restSplit[i].StartsWith("CALL "))
                                break;
                            args.Add(restSplit[i]);
                        }
                        resTrace.Add($"{curUnfinishedLine} {foundBottomCallOnStack} ({string.Join(", ", args)})");
                    } 
                } else if(rest.Contains("RETURN from " + foundBottomCallOnStack))
                {
                    foundBottomCallOnStack = null;
                } else if (rest.Contains("ASSERTION FAIL"))
                {
                    resTrace.Add($"{curUnfinishedLine} {"ASSERTION fails!"}");
                }
            }

            resTrace.ForEach(x => Console.WriteLine($"\t{x}"));

            return resTrace.ToArray();
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
        private void ProcessCall(string input, string output, string curFunctionName)
        {
            // Example: "CALL withdraw_SimpleDAO"
            string functionName = input.Substring("CALL ".Length + 1, input.Length - 1);
            if (functionName.StartsWith("Corral_Choice")) return;
            string[] nameSplit = functionName.Split("_");
            if (nameSplit[0] == nameSplit[1] && nameSplit.Length == 1)
            {
                //top level constructor "XX_XX":
                Debug.Assert(curFunctionName == null);
                output = output + "CALL " + nameSplit[0] + "::Constructor" + "(";
                curFunctionName = nameSplit[0] + "::Constructor";
            }
            else
            {
                // Regular function call: FunctionName_ConstractName
                // Example: CALL Modifier::plusOne(this = T@Ref!val!0, msg.sender = T@Ref!val!3)
                if (curFunctionName == null)
                {
                    // Top level function call:
                    output = output + "CALL " + nameSplit[1] + "::" + nameSplit[0] + "(";
                    curFunctionName = nameSplit[1] + "::" + nameSplit[0];
                }               
            }
        }

        private void ProcessReturn(string input, string output, string curFunctionName)
        {
            // Example: RETURN from Modifier::plusOne
            string functionName = input.Substring("RETURN from ".Length + 1, input.Length - 1);
            string[] nameSplit = functionName.Split("_");
            if (nameSplit[0] == nameSplit[1] && nameSplit.Length == 1)
            {
                // Return from the top level constructor:
                Debug.Assert(curFunctionName == nameSplit[0] + "::Constructor");
                output = output + "RETURN from " + nameSplit[1] + "::" + nameSplit[0];
                curFunctionName = null;
            }
            else
            {
                string name = nameSplit[1] + "::" + nameSplit[0];
                if (curFunctionName != null && curFunctionName == name)
                {
                    // Return from the Top level function:
                    output = output + "RETURN from " + name;
                    curFunctionName = null;
                }
            }
        }

        private void ProcessCallReturnAssert(string input, string output, string curFunctionName)
        {
            if (input.StartsWith("CALL"))
            {
                ProcessCall(input, output, curFunctionName);
            }
            else if (input.StartsWith("RETURN"))
            {
                ProcessReturn(input, output, curFunctionName);
                return;
            }
            else if (input.StartsWith("ASSERTION FAILS"))
            {
                // Example: "ASSERTION FAILS <assertion>"
                output = output + input;
            }
            else
            {
                // TODO: ERROR: not expected, print message
            }
        }
        private void ProcessArgs(string[] elements, int ind, string output)
        {
            // Append all arguments from "elements" into "output"
            // starting from index "ind"
            // Arguments end when one of the following elements found:
            // "this = T@Ref!val...", "CALL...", "RETURN..."
            output = output + elements[ind];
            // Number of arguments is always more then 1:
            for (int i = ind + 1; i < elements.Count(); i++)
            {
                if (elements[i].StartsWith("CALL") || elements[i].StartsWith("RETURN") || elements[i].StartsWith("this = T@Ref!val"))
                {
                    return;
                }
                else
                {
                    output = output + elements[i];
                }
            }
        }

        private void PrintCounterexample()
        {
            string[] corralTrace = File.ReadAllLines("corral.txt");
            // Find relevant trace lines in the full trace:
            corralTrace = FilterCorralTrace(corralTrace);
            // Preprocess the trace:
            // - append the line with function argument values to the line with the function call;
            // - clean up the trace from the irrelevant lines
            corralTrace = PreprocessCorralTraceForDemo(corralTrace);
            PrintCounterexampleHelper(corralTrace);
            }

        private void PrintCounterexampleHelper(string[] corralTrace)
        {
            return;
            string curFunctionName = null;

            using (var outWriter = new StreamWriter(counterexampleFileName))
            {
                foreach (string corralLine in corralTrace)
                {
                    string[] splitRes = corralLine.Split(":");
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
                    foreach (string resi in restSplit)
                    {
                        if (resi.StartsWith("CALL") || resi.StartsWith("RETURN") || resi.StartsWith("ASSERTION FAILS"))
                        {
                            ProcessCallReturnAssert(resi, counterexLine, curFunctionName);
                        }
                        else if (resi.StartsWith("this = T@Ref!val"))
                        {
                            ProcessArgs(restSplit, Array.IndexOf(restSplit, resi), counterexLine);
                        }
                        else continue;
                    }
                    outWriter.WriteLine(counterexLine);
                }
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
                BoogieAST boogieAST = translator.Translate(solidityAST, ignoreMethods, translatorFlags);

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