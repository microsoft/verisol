
namespace SolToBoogieTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SolidityAST;
    using BoogieAST;
    using SolToBoogie;

    class RegressionExecutor
    {
        private string solcPath;

        private string corralPath;

        private string testDirectory;

        private string configDirectory;

        private string recordsDir;

        private ILogger logger;

        private static readonly int corralTimeoutInMilliseconds = TimeSpan.FromSeconds(60).Seconds * 1000;

        private static readonly string outFile = "__SolToBoogieTest_out.bpl";

        private static Dictionary<string, bool> filesToRun = new Dictionary<string, bool>();

        public RegressionExecutor(string solcPath, string corralPath, string testDirectory, string configDirectory, string recordsDir, ILogger logger)
        {
            this.solcPath = solcPath;
            this.corralPath = corralPath;
            this.testDirectory = testDirectory;
            this.configDirectory = configDirectory;
            this.recordsDir = recordsDir;
            this.logger = logger;
        }

        public bool BatchExecute()
        {
            string[] filePaths = Directory.GetFiles(testDirectory);
            int passedCount = 0;
            int failedCount = 0;
            readRecord();
            foreach (string filePath in filePaths)
            {
                string filename = Path.GetFileName(filePath);
                if (!filesToRun.ContainsKey(filename))
                {
                    logger.LogWarning($"{filename} not found in {Path.Combine(recordsDir, "records.txt")}");
                    continue;
                }

                if (!filesToRun[filename])
                {
                    continue;
                }

                logger.LogInformation($"Running {filename}");

                bool success = false;
                try
                {
                    success = Execute(filename);
                }
                catch (Exception exception)
                {
                    logger.LogCritical(exception, $"Exception occurred in {filename}");
                }

                if (success)
                {
                    ++passedCount;
                    logger.LogInformation($"Passed - {filename}");
                }
                else
                {
                    ++failedCount;
                    logger.LogError($"Failed - {filename}");
                }
            }

            logger.LogInformation($"{passedCount} passed {failedCount} failed");
            DeleteTemporaryFiles();
            return (failedCount == 0);
        }

        public bool Execute(string filename)
        {
            string filePath = Path.Combine(testDirectory, filename);

            // compile the program
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(solcPath, filePath);

            if (compilerOutput.ContainsError())
            {
                compilerOutput.PrintErrorsToConsole();
                throw new SystemException("Compilation Error");
            }

            // build the Solidity AST from solc output
            AST solidityAST = new AST(compilerOutput, Path.GetDirectoryName(filePath));

            // translate Solidity to Boogie
            BoogieTranslator translator = new BoogieTranslator();
            BoogieAST boogieAST = translator.Translate(solidityAST);

            // dump the Boogie program to a file
            using (var outWriter = new StreamWriter(outFile))
            {
                outWriter.WriteLine(boogieAST.GetRoot());
            }

            // read the corral configuration from Json
            string configJsonPath = Path.Combine(configDirectory, Path.GetFileNameWithoutExtension(filename) + ".json");
            string jsonString = File.ReadAllText(configJsonPath);
            CorralConfiguration corralConfig = JsonConvert.DeserializeObject<CorralConfiguration>(jsonString);

            string corralOutput = RunCorral(corralConfig);
            return CompareCorralOutput(corralConfig.ExpectedResult, corralOutput);
        }

        private string RunCorral(CorralConfiguration corralConfig)
        {
            string corralArguments = GenerateCorralArguments(corralConfig);

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                p.StartInfo.FileName = corralPath;
                p.StartInfo.Arguments = corralArguments;
            }
            else
            {
                p.StartInfo.FileName = "mono";
                p.StartInfo.Arguments = $"{corralPath} {corralArguments}";
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

        private string GenerateCorralArguments(CorralConfiguration corralConfig)
        {
            List<string> commands = new List<string>();
            // recursion bound
            commands.Add($"/recursionBound:{corralConfig.RecursionBound}");
            // context bound (k)
            commands.Add($"/k:{corralConfig.K}");
            // main method
            commands.Add($"/main:{corralConfig.Main}");
            // Boogie file
            commands.Add(outFile);
            return String.Join(" ", commands);
        }

        private void readRecord()
        {
            StreamReader records = new StreamReader(Path.Combine(recordsDir, "records.txt"));
            string line;
            while((line = records.ReadLine()) != null)
            {
                string fileName = line.TrimEnd();
                if (fileName.EndsWith('#'))
                {
                    filesToRun[fileName.TrimEnd('#')] = false;
                }
                else
                {
                    filesToRun[fileName] = true;
                }
            }
        }

        private void DeleteTemporaryFiles()
        {
            File.Delete(outFile);
            File.Delete("corral_out.bpl");
            File.Delete("corral_out_trace.txt");
        }
    }
}
