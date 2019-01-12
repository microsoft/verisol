// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System.Diagnostics;
    using SolidityAST;

    public class MapArrayCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        // current contract that the visitor is visiting
        private ContractDefinition currentContract = null;

        public MapArrayCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(ContractDefinition node)
        {
            currentContract = node;
            return true;
        }

        public override void EndVisit(ContractDefinition node)
        {
            currentContract = null;
        }

        public override bool Visit(VariableDeclaration node)
        {
            Debug.Assert(currentContract != null);

            if (node.TypeName is Mapping)
            {
                context.AddMappingtoContract(currentContract, node);
            }
            else if (node.TypeName is ArrayTypeName)
            {
                context.AddArrayToContract(currentContract, node);
            }
            return false;
        }
    }
}
