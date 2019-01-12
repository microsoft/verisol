// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using SolidityAST;

    public class ContractCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        public ContractCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(ContractDefinition node)
        {
            context.AddContract(node);
            return false;
        }
    }
}
