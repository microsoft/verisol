// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityCFG
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using SolidityAST;

    public class TestMain
    {
        public static void Main(string[] args)
        {
            DirectoryInfo debugDirectoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            string workingDirectory = debugDirectoryInfo.Parent.Parent.Parent.Parent.FullName;
            string filename = "Branch.sol";
            string solcPath = workingDirectory + "\\Tool\\solc.exe";
            string filePath = workingDirectory + "\\Test\\regression\\" + filename;
            SolidityCompiler compiler = new SolidityCompiler();
            CompilerOutput compilerOutput = compiler.Compile(solcPath, filePath);
            SoliditySourceFile soliditySourceFile = compilerOutput.Sources[filename];
            ASTNode root = soliditySourceFile.Ast;

            Debug.Assert(root.GetType() == typeof(SourceUnit));
            foreach (ASTNode decl in ((SourceUnit) root).Nodes)
            {
                if (decl is ContractDefinition)
                {
                    ContractDefinition contract = (ContractDefinition)decl;
                    Console.WriteLine("contract " + contract.Name);
                    foreach (ASTNode node in contract.Nodes)
                    {
                        if (node is FunctionDefinition)
                        {
                            FunctionDefinition func = (FunctionDefinition)node;
                            Console.WriteLine("function " + func.Name);
                            Test(func);
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        private static void Test(FunctionDefinition func)
        {
            CFGNodeFactory factory = new CFGNodeFactory();
            FunctionCFG functionCFG = CFGBuilder.BuildFunctionCFG(factory, func);
            PrintCFG(functionCFG.Entry);
        }

        private static void PrintCFG(CFGNode root)
        {
            HashSet<CFGNode> visited = new HashSet<CFGNode>();
            Queue<CFGNode> queue = new Queue<CFGNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                CFGNode node = queue.Dequeue();
                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    Console.WriteLine(node);
                    foreach (CFGNode exit in node.Exits)
                    {
                        queue.Enqueue(exit);
                    }
                }
            }
        }
    }
}
