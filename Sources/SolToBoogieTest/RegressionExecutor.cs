
namespace SolToBoogieTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SolidityAST;
    using BoogieAST;
    using SolToBoogie;
    using VeriSolRunner.ExternalTools;

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

        public enum BatchExeResult { Success, SolcError, SolToBoogieError, CorralError, OtherException };

        public RegressionExecutor(string testDirectory, string configDirectory, string recordsDir, ILogger logger, string testPrefix = "")
        {
            this.solcPath = ExternalToolsManager.Solc.Command;
            this.corralPath = ExternalToolsManager.Corral.Command;
            this.testDirectory = testDirectory;
            this.configDirectory = configDirectory;
            this.recordsDir = recordsDir;
            this.logger = logger;
            this.testPrefix = testPrefix;
        }

        public int BatchExecute()
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

                BatchExeResult batchExeResult = BatchExeResult.SolcError;
                string expectedCorralOutput = "", currentCorralOutput = "";
                try
                {
                    batchExeResult = Execute(filename, out expectedCorralOutput, out currentCorralOutput);
                }
                catch (Exception exception)
                {
                    logger.LogCritical(exception, $"Exception occurred in {filename}");
                    batchExeResult = BatchExeResult.OtherException;
                }

                if (batchExeResult == BatchExeResult.Success)
                {
                    ++passedCount;
                    logger.LogInformation($"Passed - {filename}");
                }
                else if (batchExeResult == BatchExeResult.SolcError)
                {
                    ++failedCount;
                    logger.LogError($"Failed (Solc failed) - {filename}");
                }
                else if (batchExeResult == BatchExeResult.OtherException)
                {
                    ++failedCount;
                    logger.LogError($"Failed (Other exception) - {filename}");
                }
                else if (batchExeResult == BatchExeResult.SolToBoogieError)
                {
                    ++failedCount;
                    logger.LogError($"Failed (VeriSol translation error) - {filename}");
                }
                else if (batchExeResult == BatchExeResult.CorralError)
                {
                    ++failedCount;
                    logger.LogError($"Failed (Corral regression failed) - {filename}");
                    logger.LogError($"\t Expected - {expectedCorralOutput}");
                    logger.LogError($"\t Corral detailed Output - {currentCorralOutput}");
                }
                else
                {
                    ++failedCount;
                    logger.LogError($"Failed (Tool error: unexpected failure code) - {filename}");
                }
            }

            logger.LogInformation($"{passedCount} passed {failedCount} failed");
            // To allow time for logging the last summary line:
            System.Threading.Thread.Sleep(100);
            DeleteTemporaryFiles();
            return (failedCount == 0)? 0 : 1;
        }

        private void ParseTranslatorFlags(TranslatorFlags translatorFlags, string args)
        {
            string solidityFile, entryPointContractName;
            bool tryProofFlag, tryRefutation;
            int recursionBound;
            ILogger logger;
            HashSet<Tuple<string, string>> ignoredMethods;
            bool printTransactionSequence = false;
            string verisolCmdLineArgs = "Foo Bar " + args; //Parser expects dirst two args to be present 
            SolToBoogie.ParseUtils.ParseCommandLineArgs(verisolCmdLineArgs.Split(" "),
            out solidityFile,
            out entryPointContractName,
            out tryProofFlag,
            out tryRefutation,
            out recursionBound,
            out logger,
            out ignoredMethods,
            out printTransactionSequence,
            ref translatorFlags);
        }



        public BatchExeResult Execute(string filename, out string expected, out string current)
        {
            BatchExeResult result = BatchExeResult.SolcError;
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

            // read the corral configuration from Json
            string configJsonPath = Path.Combine(configDirectory, Path.GetFileNameWithoutExtension(filename) + ".json");
            string jsonString = File.ReadAllText(configJsonPath);
            CorralConfiguration corralConfig = JsonConvert.DeserializeObject<CorralConfiguration>(jsonString);

            // translate Solidity to Boogie
            try
            {
                BoogieTranslator translator = new BoogieTranslator();
                var translatorFlags = new TranslatorFlags();
                translatorFlags.GenerateInlineAttributes = false;
                if (corralConfig.TranslatorOptions != null)
                {
                    ParseTranslatorFlags(translatorFlags, corralConfig.TranslatorOptions);
                }
                
                BoogieAST boogieAST = translator.Translate(solidityAST, new HashSet<Tuple<string, string>>(), translatorFlags);

                // dump the Boogie program to a file
                using (var outWriter = new StreamWriter(outFile))
                {
                    outWriter.WriteLine(boogieAST.GetRoot());
                }
            } catch (Exception e)
            {
                Console.WriteLine($"VeriSol translation error: {e.Message}");
                result = BatchExeResult.SolToBoogieError;
                expected = current = null;
                return result;
            }

            // read the corral configuration from Json
            //string configJsonPath = Path.Combine(configDirectory, Path.GetFileNameWithoutExtension(filename) + ".json");
            //string jsonString = File.ReadAllText(configJsonPath);
            //CorralConfiguration corralConfig = JsonConvert.DeserializeObject<CorralConfiguration>(jsonString);

            string corralOutput = RunCorral(corralConfig);
            expected = corralConfig.ExpectedResult;
            current = corralOutput;
            result = CompareCorralOutput(corralConfig.ExpectedResult, corralOutput);
            return result;
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
            p.StartInfo.FileName = this.corralPath;
            p.StartInfo.Arguments = corralArguments;
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

        private BatchExeResult CompareCorralOutput(string expected, string actual)
        {
            if (actual == null)
            {
                return BatchExeResult.CorralError;
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
                    return BatchExeResult.Success;
                }
            }
            return BatchExeResult.CorralError;
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
