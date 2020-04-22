namespace SolidityAnalysis
{
    using System;
    using System.Collections.Generic;
    using SolidityAST;
    
    public class SolidityAnalyzer
    {
        public AliasAnalysis Alias { get; }
        
        public SolidityAnalyzer(AST solidityAST, HashSet<Tuple<string, string>> ignoredMethods, String entryPointContract = "")
        {
            Alias = new AliasAnalysis(solidityAST, ignoredMethods, entryPointContract);
        }
    }
}