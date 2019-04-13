

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
        private HashSet<Tuple<string, string>> ignoreMethods;

        public VeriSolExecuterWithSpecs(string specFilePath, string contractName, string contractPath, string corralPath, string solcPath, string solcName, int corralRecursionLimit, HashSet<Tuple<string, string>> ignoreMethods, ILogger logger)
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
            this.ignoreMethods = new HashSet<Tuple<string, string>>(ignoreMethods);
            this.Logger = logger;
        }

        public int Execute()
        {
            // copy contractDir folder to specFileDir
            CopyTargetContractFolder();

            // replace "private " with "internal " in all sol files [HACK!!!]
            RewritePrivateVariables();

            // call SolToBoogie on specFilePath
            if (!ExecuteSolToBoogie())
            {
                return 1;
            }

            // run Corral on outFile
            var corralArgs = new List<string>
            {
                // recursion bound
                $"/recursionBound:{CorralRecursionLimit}",
                // context bound (k)
                $"/k:{CorralContextBound}",
                // main method
                $"/main:CorralEntry_{ContractName}",
                // Boogie file
                outFileName
            };

            var corralArgString = string.Join(" ", corralArgs);
            Console.WriteLine($"\n-----------Running {CorralPath} {corralArgString} ....");
            var corralOut = RunCorral(corralArgString);
            Console.WriteLine($"Finished Corral....\n{corralOut}");

            // compare Corral output against expected output
            if (CompareCorralOutput("Program has no bugs", corralOut))
            {
                Console.WriteLine("\n-----Formal Verification successful!!");
                return 0;
            }

            Console.WriteLine("\n-----Formal Verification unsuccessful!!");
            return 1;
        }

        private void CopyTargetContractFolder()
        {
            // replicates the content of contractDir\* into specFileDir\
            DirectoryCopy(ContractDir, SpecFileDir, true);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        /// </summary>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void RewritePrivateVariables()
        {
            //recurse down to each Solidity file
            var solidityFiles = Directory.EnumerateFiles(SpecFileDir, "*.sol", SearchOption.AllDirectories);

            //replace "private " with "internal " in the file
            foreach (var solFile in solidityFiles)
            {
                var tmpFile = solFile + ".tmp";
                using (StreamReader sr = new StreamReader(solFile))
                {
                    using (StreamWriter sw = new StreamWriter(tmpFile))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            // brute force
                            string replacedLine =  line.Replace(" private ", " internal ");
                            replacedLine = replacedLine.Replace(" private(", " internal("); // private(returns bool)
                            replacedLine = replacedLine.Replace(" private{", " internal{"); // private{body}
                            replacedLine = replacedLine.Replace(")private ", ")internal "); // )private {body}
                            replacedLine = replacedLine.Replace(")private(", ")internal("); // )private(returns bool)
                            replacedLine = replacedLine.Replace(")private{", ")internal{"); // )private{body}
                            sw.WriteLine(replacedLine);
                        }
                    }
                }
                File.Delete(solFile);
                File.Move(tmpFile, solFile);
            }
        }

        private bool ExecuteSolToBoogie()
        {
            // compile the program
            Console.WriteLine($"\n----- Running Solc on {SpecFilePath}....");

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
                Console.WriteLine($"\n----- Running SolToBoogie....");
                BoogieAST boogieAST = translator.Translate(solidityAST, ignoreMethods, true);

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
                if (actualList[0].Contains(expected))
                {
                    return true;
                }
            }
            return false;
        }


    }
}