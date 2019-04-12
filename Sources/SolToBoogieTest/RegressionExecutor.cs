
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

        private string testPrefix;

        private static readonly int corralTimeoutInMilliseconds = TimeSpan.FromSeconds(60).Seconds * 1000;

        private static readonly string outFile = "__SolToBoogieTest_out.bpl";

        private static Dictionary<string, bool> filesToRun = new Dictionary<string, bool>();

        public RegressionExecutor(string solcPath, string corralPath, string testDirectory, string configDirectory, string recordsDir, ILogger logger, string testPrefix = "")
        {
            this.solcPath = solcPath;
            this.corralPath = corralPath;
            this.testDirectory = testDirectory;
            this.configDirectory = configDirectory;
            this.recordsDir = recordsDir;
            this.logger = logger;
            this.testPrefix = testPrefix;
        }

        public bool BatchExecute()
        {
            string[] filePaths = Directory.GetFiles(testDirectory);
            int passedCount = 0;
            int failedCount = 0;
            ReadRecord();
            foreach (string filePath in filePaths)
            {
                string filename = Path.GetFileName(filePath);
                if (!filename.StartsWith(testPrefix))
                    continue;
                //silently ignore non .sol files
                if (!filename.EndsWith(".sol"))
                    continue;
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
                string expectedCorralOutput = "", currentCorralOutput = "";
                try
                {
                    success = Execute(filename, out expectedCorralOutput, out currentCorralOutput);
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
                    logger.LogError($"\t Expected - {expectedCorralOutput}");
                    logger.LogError($"\t Corral detailed Output - {currentCorralOutput}");
                }
            }

            logger.LogInformation($"{passedCount} passed {failedCount} failed");
            DeleteTemporaryFiles();
            return (failedCount == 0);
        }

        public bool Execute(string filename, out string expected, out string current)
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
            try
            {
                BoogieTranslator translator = new BoogieTranslator();
                BoogieAST boogieAST = translator.Translate(solidityAST, new HashSet<Tuple<string, string>>(), true);

                // dump the Boogie program to a file
                using (var outWriter = new StreamWriter(outFile))
                {
                    outWriter.WriteLine(boogieAST.GetRoot());
                }
            } catch (Exception e)
            {
                Console.WriteLine($"VeriSol translation error: {e.Message}");
                expected = current = null;
                return false;
            }

            // read the corral configuration from Json
            string configJsonPath = Path.Combine(configDirectory, Path.GetFileNameWithoutExtension(filename) + ".json");
            string jsonString = File.ReadAllText(configJsonPath);
            CorralConfiguration corralConfig = JsonConvert.DeserializeObject<CorralConfiguration>(jsonString);

            string corralOutput = RunCorral(corralConfig);
            expected = corralConfig.ExpectedResult;
            current = corralOutput;
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
                // This check will not work in the presence of loops 
                // Corral ends with something about Recursion bound being reached
                // See LoopFor.sol regression
                // if (actualList[0].TrimEnd().EndsWith(expected))
                if (actualList[0].Contains(expected))
                {
                    return true;
                }
            }
            return false;
        }

        private string GenerateCorralArguments(CorralConfiguration corralConfig)
        {
            List<string> commands = new List<string>
            {
                // recursion bound
                $"/recursionBound:{corralConfig.RecursionBound}",
                // context bound (k)
                $"/k:{corralConfig.K}",
                // main method
                $"/main:{corralConfig.Main}",
                // Boogie file
                outFile
            };
            return String.Join(" ", commands);
        }

        private void ReadRecord()
        {
            StreamReader records = new StreamReader(Path.Combine(recordsDir, "records.txt"));
            string line;
            while((line = records.ReadLine()) != null)
            {
                string fileName = line.TrimEnd();
                if (fileName.StartsWith('#'))
                {
                    filesToRun[fileName.TrimStart('#')] = false;
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
