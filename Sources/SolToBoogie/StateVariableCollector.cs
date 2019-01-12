// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System.Diagnostics;
    using SolidityAST;

    public class StateVariableCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        public StateVariableCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(ContractDefinition node)
        {
            foreach (ASTNode child in node.Nodes)
            {
                if (child is VariableDeclaration varDecl)
                {
                    Debug.Assert(varDecl.StateVariable, $"{varDecl.Name} is not a state variable");
                    // add all state variables to the context
                    context.AddStateVarToContract(node, varDecl);
                }
            }
            return false;
        }
    }
}
