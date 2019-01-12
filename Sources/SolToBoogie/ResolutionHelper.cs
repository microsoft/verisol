// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using SolidityAST;

    public class ResolutionHelper
    {
        private TranslatorContext context;

        public ResolutionHelper(TranslatorContext context)
        {
            this.context = context;
        }

        
        // May be we don't need it
        public List<ContractDefinition> TopologicalSortByDependency(HashSet<ContractDefinition> contracts)
        {
            // reverse order of topological sorting
            List<ContractDefinition> result = new List<ContractDefinition>();

            HashSet<ContractDefinition> visited = new HashSet<ContractDefinition>();
            foreach (ContractDefinition contract in contracts)
            {
                if (!visited.Contains(contract))
                {
                    TopologicalSortImpl(visited, contract, result);
                }
            }
            Debug.Assert(result.Count == contracts.Count);
            return result;
        }

        private void TopologicalSortImpl(HashSet<ContractDefinition> visited, ContractDefinition contract, List<ContractDefinition> result)
        {
            visited.Add(contract);
            foreach (int id in contract.ContractDependencies)
            {
                ContractDefinition dependency = context.GetASTNodeById(id) as ContractDefinition;
                Debug.Assert(dependency != null);
                if (!visited.Contains(dependency))
                {
                    TopologicalSortImpl(visited, dependency, result);
                }
            }
            if (AllDependenciesVisited(visited, contract))
            {
                result.Add(contract);
            }
        }

        private bool AllDependenciesVisited(HashSet<ContractDefinition> visited, ContractDefinition contract)
        {
            foreach (int id in contract.ContractDependencies)
            {
                ContractDefinition dependency = context.GetASTNodeById(id) as ContractDefinition;
                Debug.Assert(dependency != null);
                if (!visited.Contains(dependency))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
