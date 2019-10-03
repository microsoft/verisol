// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
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
                Console.WriteLine("\t\t /ignoreMethod:<method>@<contract>: Ignores translation of the method within contract, and only generates a declaration");
                Console.WriteLine("\t\t\t\t multiple such pairs can be specified, ignored set is the union");
                Console.WriteLine("\t\t\t\t a wild card '*' can be used for method, would mean all the methods of the contract");
                Console.WriteLine("\t\t /noInlineAttrs: do not generate any {:inline x} attributes, to speed Corral");
                return;
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), args[0]);
            string workingDirectory = args[1];
            string outFile = Path.Combine(Directory.GetCurrentDirectory(), args[2]);

            string solcName = GetSolcNameByOSPlatform();
            string solcPath = Path.Combine(workingDirectory, "Binaries", solcName);
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(solcPath, filePath);
            HashSet<Tuple<string, string>> ignoredMethods = new HashSet<Tuple<string, string>>();

            // Issue with run_verisol_win.cmd it passes "/ignoreMethod:a@b /ignoreMethod:c@d .." as a single string
            var splitArgs = new List<string>();
            args.Select(arg => arg.Split(" "))
            .ToList()
            .ForEach(a => a.ToList().ForEach(b => splitArgs.Add(b)));

            if (splitArgs.Any(x => x.Equals("/break")))
            {
                Debugger.Launch();
            }
            foreach (var arg in splitArgs.Where(x => x.StartsWith("/ignoreMethod:")))
            {
                Debug.Assert(arg.Contains("@"), $"Error: incorrect use of /ignoreMethod in {arg}");
                Debug.Assert(arg.LastIndexOf("@") == arg.IndexOf("@"), $"Error: incorrect use of /ignoreMethod in {arg}");
                var str = arg.Substring("/ignoreMethod:".Length);
                var method = str.Substring(0, str.IndexOf("@"));
                var contract = str.Substring(str.IndexOf("@")+1);
                ignoredMethods.Add(Tuple.Create(method, contract));
            }
            Console.WriteLine($"Ignored method/contract pairs ==> \n\t {string.Join(",", ignoredMethods.Select(x => x.Item1 + "@" + x.Item2))}");

            bool genInlineAttributesInBpl = true; 
            if (splitArgs.Any(x => x.Equals("/noInlineAttrs")))
            {
                genInlineAttributesInBpl = false;
            }

            if (compilerOutput.ContainsError())
            {
                PrintErrors(compilerOutput.Errors);
                throw new SystemException("Compilation Error");
            }
            else
            {
                AST solidityAST = new AST(compilerOutput, Path.GetDirectoryName(filePath));

                try
                {
                    BoogieTranslator translator = new BoogieTranslator();
                    var translatorFlags = new TranslatorFlags();
                    translatorFlags.GenerateInlineAttributes = genInlineAttributesInBpl;
                    BoogieAST boogieAST = translator.Translate(solidityAST, ignoredMethods, translatorFlags);

                    using (var outWriter = new StreamWriter(outFile))
                    {
                        outWriter.WriteLine(boogieAST.GetRoot());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Verisol translation exception: {e.Message}");
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
                solcName = "solc.exe";
            }
            else
            {
                throw new SystemException("Cannot recognize OS platform");
            }
            return solcName;
        }
    }
}
