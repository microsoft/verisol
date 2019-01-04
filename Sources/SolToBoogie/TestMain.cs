
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using BoogieAST;
    using SolidityAST;

    class TestMain
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: SolToBoogie <relative path to filename.sol> <workingdir> <relative path to outfile.bpl> [options]");
                Console.WriteLine("\t Options:");
                Console.WriteLine("\t\t /break: Opens the debugger");
                return;
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), args[0]);
            string workingDirectory = args[1];
            string outFile = Path.Combine(Directory.GetCurrentDirectory(), args[2]);

            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string solcName = isWindows ? "solc.exe" : "solc-static-linux";
            string solcPath = Path.Combine(workingDirectory, "Tool", solcName);
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(solcPath, filePath);

            if (args.Any(x => x.Equals("/break")))
            {
                Debugger.Launch();
            }

            if (compilerOutput.ContainsError())
            {
                PrintErrors(compilerOutput.Errors);
                throw new SystemException("Compilation Error");
            }
            else
            {
                AST solidityAST = new AST(compilerOutput, Path.GetDirectoryName(filePath));

                BoogieTranslator translator = new BoogieTranslator();
                BoogieAST boogieAST = translator.Translate(solidityAST);

                using (var outWriter = new StreamWriter(outFile))
                {
                    outWriter.WriteLine(boogieAST.GetRoot());
                }
            }
        }

        public static void PrintErrors(List<CompilerError> errorsAndWarnings)
        {
            foreach (CompilerError error in errorsAndWarnings)
            {
                if (error.Severity.Equals("error"))
                {
                    Console.WriteLine(error.FormattedMessage);
                }
            }
        }
    }
}
