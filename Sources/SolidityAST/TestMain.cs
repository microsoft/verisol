// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    class TestMain
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: SolidityAST <workingdir>");
                return;
            }

            string workingDirectory = args[0];
            string solcName = GetSolcNameByOSPlatform();
            string solcPath = Path.Combine(workingDirectory, "Tool", solcName);
            string testDir = Path.Combine(workingDirectory, "Test", "regression");

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(LogLevel.Trace);
            ILogger logger = loggerFactory.CreateLogger("SolidityAST.RegressionExecutor");

            RegressionExecutor executor = new RegressionExecutor(solcPath, testDir, logger);
            executor.BatchExecute();
        }

        // Legacy entry point for testing
        private static void LegacyMain(string[] args)
        {
            DirectoryInfo debugDirectoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            string workingDirectory = debugDirectoryInfo.Parent.Parent.Parent.Parent.FullName;
            string filename = "AssertTrue.sol";
            string solcName = GetSolcNameByOSPlatform();
            string solcPath = Path.Combine(workingDirectory, "Tool", solcName);
            string filePath = Path.Combine(workingDirectory, "Test", "regression", filename);
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(solcPath, filePath);
            AST ast = new AST(compilerOutput);

            Console.WriteLine(ast.GetSourceUnits());
            Console.ReadLine();
        }

        private static string GetSolcNameByOSPlatform()
        {
            string solcName = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                solcName = "solc.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                solcName = "solc-static-linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                solcName = "solc-mac";
            }
            else
            {
                throw new SystemException("Cannot recognize OS platform");
            }
            return solcName;
        }
    }
}
