// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System.Diagnostics;
    using SolidityAST;

    public class InheritanceCollector
    {
        // require the ContractDefinitions is populated
        private TranslatorContext context;

        public InheritanceCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public void Collect()
        {
            foreach (ContractDefinition contract in context.ContractDefinitions)
            {
                foreach (int baseId in contract.LinearizedBaseContracts)
                {
                    ContractDefinition baseContract = context.GetASTNodeById(baseId) as ContractDefinition;
                    Debug.Assert(baseContract != null);
                    context.AddSubTypeToContract(baseContract, contract);
                }
            }
        }
    }
}
