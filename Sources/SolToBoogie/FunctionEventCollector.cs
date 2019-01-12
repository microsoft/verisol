// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System.Diagnostics;
    using SolidityAST;

    /**
     * Collect all function/event definitions and put them in the translator context.
     * The result map only contains functions/events directly defined in each contract,
     * but does not contain inherited functions/events.
     */
    public class FunctionEventCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        // current contract that the visitor is visiting
        private ContractDefinition currentContract = null;

        public FunctionEventCollector(TranslatorContext context)
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

        public override bool Visit(EventDefinition node)
        {
            Debug.Assert(currentContract != null);
            context.AddEventToContract(currentContract, node);
            return false;
        }

        public override bool Visit(FunctionDefinition node)
        {
            Debug.Assert(currentContract != null);
            context.AddFunctionToContract(currentContract, node);
            return false;
        }
    }
}
