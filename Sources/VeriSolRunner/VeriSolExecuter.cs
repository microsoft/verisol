

namespace VeriSolOutOfBandsSpecsRunner
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

    internal class VeriSolExecuterWithSpecs
    {
        private string SpecFilePath;
        private string SpecFileDir;
        private string ContractName;
        private string ContractPath;
        private string ContractDir;
        private string CorralPath;
        private string SolcPath;
        private string SolcName;
        private ILogger Logger;
        private readonly string outFileName = "__SolToBoogieTest_out.bpl";
        private readonly int CorralRecursionLimit;
        private readonly int CorralContextBound = 1; // always 1 for solidity

        public VeriSolExecuterWithSpecs(string specFilePath, string contractName, string contractPath, string corralPath, string solcPath, string solcName, int corralRecursionLimit, ILogger logger)
        {
            this.SpecFilePath = specFilePath;
            this.ContractName = contractName;
            this.SpecFileDir = Path.GetDirectoryName(specFilePath);
            this.ContractPath = contractPath;
            this.ContractDir = Path.GetDirectoryName(contractPath);
            this.CorralPath = corralPath;
            this.SolcPath = solcPath;
            this.SolcName = solcName;
            this.CorralRecursionLimit = corralRecursionLimit;
            this.Logger = logger;
        }

        public int Execute()
        {
            // copy contractDir folder to specFileDir
            CopyTargetContractFolder();

            // replace "private " with "internal " in all sol files [HACK!!!]
            RewritePrivateVariables();

            // call SolToBoogie on specFilePath
            ExecuteSolToBoogie();

            // run Corral on outFile
            var corralArgs = new List<string>
            {
                // recursion bound
                $"/recursionBound:{CorralRecursionLimit}",
                // context bound (k)
                $"/k:{CorralContextBound}",
                // main method
                $"/main:{ContractName}",
                // Boogie file
                outFileName
            };

            var corralOut = RunCorral(string.Join(" ", corralArgs));

            // compare Corral output against expected output
            if (CompareCorralOutput("Program has no bugs", corralOut))
            {
                return 0;
            }

            return 1;
            throw new NotImplementedException();
        }

        private bool CopyTargetContractFolder()
        {
            throw new NotImplementedException();
        }

        private bool RewritePrivateVariables()
        {
            throw new NotImplementedException();
        }

        private bool ExecuteSolToBoogie()
        {
            // compile the program
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(SolcPath, SpecFilePath);

            if (compilerOutput.ContainsError())
            {
                compilerOutput.PrintErrorsToConsole();
                throw new SystemException("Compilation Error");
            }

            // build the Solidity AST from solc output
            AST solidityAST = new AST(compilerOutput, Path.GetDirectoryName(SpecFilePath));

            // translate Solidity to Boogie
            try
            {
                BoogieTranslator translator = new BoogieTranslator();
                BoogieAST boogieAST = translator.Translate(solidityAST, new HashSet<Tuple<string, string>>(), true);

                // dump the Boogie program to a file
                var outFilePath = Path.Combine(SpecFileDir, outFileName);
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

        private string RunCorral(string corralArguments)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                p.StartInfo.FileName = CorralPath;
                p.StartInfo.Arguments = corralArguments;
            }
            else
            {
                p.StartInfo.FileName = "mono";
                p.StartInfo.Arguments = $"{CorralPath} {corralArguments}";
                Console.WriteLine(p.StartInfo.Arguments);
            }
            p.Start();

            string corralOutput = p.StandardOutput.ReadToEnd();
            string errorMsg = p.StandardError.ReadToEnd();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                Console.WriteLine($"Error: {errorMsg}");
            }
            p.StandardOutput.Close();
            p.StandardError.Close();

            // TODO: should set up a timeout here
            // but it seems there is a problem if we execute corral using mono

            return corralOutput;
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
                if (actualList[0].TrimEnd().EndsWith(expected))
                {
                    return true;
                }
            }
            return false;
        }


    }
}