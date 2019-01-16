// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Logging;

    class RegressionExecutor
    {
        private string solcPath;

        private string testDirectory;

        private ILogger logger;

        public RegressionExecutor(string solcPath, string testDirectory, ILogger logger)
        {
            this.solcPath = solcPath;
            this.testDirectory = testDirectory;
            this.logger = logger;
        }

        public bool BatchExecute()
        {
            string[] filePaths = Directory.GetFiles(testDirectory);

            int passedCount = 0;
            int failedCount = 0;
            foreach (string filePath in filePaths)
            {
                string filename = Path.GetFileName(filePath);
                logger.LogDebug($"Running {filename}");

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
            return (failedCount == 0);
        }

        public bool Execute(string filename)
        {
            string filePath = Path.Combine(testDirectory, filename);

            // read the program text
            string programText = File.ReadAllText(filePath);

            // get the text of AST traversal
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(solcPath, filePath);

            if (compilerOutput.ContainsError())
            {
                compilerOutput.PrintErrorsToConsole();
                throw new SystemException("Compilation Error");
            }

            AST ast = new AST(compilerOutput);
            ASTNode sourceUnits = ast.GetSourceUnits();

            return TextCompare(programText, sourceUnits.ToString());
        }

        private bool TextCompare(string originalText, string astText)
        {
            // remove line comments from original text
            string lineCommentsRegex = @"//(.*?)\r?\n";
            string textWithoutComments = Regex.Replace(originalText, lineCommentsRegex, "");
            // remove block comments
            string blockCommentsRegex = @"(/\*(.|[\r\n])*?\*/)";
            textWithoutComments = Regex.Replace(textWithoutComments, blockCommentsRegex, "");

            string[] separator = { "{", "}", ",", ".", ";", "(", ")", "^", "[", "]", " ", "\n", "\r", "\t", "'", "\""};
            
            string[] originalWords = textWithoutComments.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string[] astWords = astText.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            string[] normalizedWords = NormalizeKeywords(originalWords);

            List<string> enumKeywords = new List<string>() {
                "CONTRACT", "LIBRARY", "INTERFACE",
                "PUBLIC", "EXTERNAL", "INTERNAL", "PRIVATE", "DEFAULT",
                "STORAGE", "MEMORY", "CALLDATA",
                "PURE", "VIEW", "NONPAYABLE", "PAYABLE",
            };

            // the compiler will add/remove/reorder certain keywords, which need to be skipped for comparison
            int normalizedIndex = 0;
            int astIndex = 0;
            int normalizedLength = normalizedWords.Length;
            int astLength = astWords.Length;

            List<string> normalizedSkip = new List<string>() {
                "public", "private", "view", "internal", "constant", "var",
                "years", "days", "hours", "minutes", "ether", "finney", "wei",
                "storage", "memory"
            };
            List<string> astSkip = new List<string>() {
                "CONSTANT", "PUBLIC", "PRIVATE", "VIEW", "INTERNAL", "memory"
            };

            while (normalizedIndex < normalizedLength)
            {
                if (normalizedSkip.Contains(normalizedWords[normalizedIndex]))
                {
                    normalizedWords[normalizedIndex] = "";
                }
                normalizedIndex++;
            }

            while (astIndex < astLength)
            {
                if (astSkip.Contains(astWords[astIndex]))
                {
                    astWords[astIndex] = "";
                    astIndex++;
                    continue;
                }
                
                if (enumKeywords.Contains(astWords[astIndex]))
                {
                    astWords[astIndex] = astWords[astIndex].ToLower();
                }
                astIndex++;
            }

            logger.LogDebug($"Normalized program words: {ConcatStringArray(normalizedWords, "")}");
            logger.LogDebug($"AST traversal prog words: {ConcatStringArray(astWords, "")}");

            if (string.Equals(ConcatStringArray(normalizedWords, ""), ConcatStringArray(astWords, "")))
            {
                return true;
            }

            return false;
        }

        private string ConcatStringArray(string[] array, string delim)
        {
            if (array.Length == 0)
            {
                return "";
            }
            else if (array.Length == 1)
            {
                return array[0];
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < array.Length - 1; ++i)
                {
                    builder.Append(array[i]).Append(delim);
                }
                builder.Append(array[array.Length - 1]);
                return builder.ToString();
            }
        }

        private string[] NormalizeKeywords(string[] words)
        {
            string[] ret = new string[words.Length];

            for (int i = 0; i < words.Length; ++i)
            {
                switch (words[i])
                {
                    case "uint":
                        ret[i] = "uint256";
                        break;
                    case "int":
                        ret[i] = "int256";
                        break;
                    default:
                        ret[i] = words[i];
                        break;
                }
            }

            return ret;
        }
    }
}
