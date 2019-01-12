// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using SolidityAST;

    /**
    * Determine the visible state variables for each contract considering
    * the inheritance hierarchy, and put the information in the context.
    */
    public class StateVariableResolver
    {
        private TranslatorContext context;

        public StateVariableResolver(TranslatorContext context)
        {
            this.context = context;
        }

        public void Resolve()
        {
            ResolveStateVariables();
            ComputeVisibleStateVariables();
        }

        private void ResolveStateVariables()
        {
            ResolutionHelper resolutionHelper = new ResolutionHelper(context);
            List<ContractDefinition> sortedContracts = resolutionHelper.TopologicalSortByDependency(context.ContractDefinitions);

            foreach (ContractDefinition contract in sortedContracts)
            {
                // create a deep copy
                List<int> linearizedBaseContractIds = new List<int>(contract.LinearizedBaseContracts);
                linearizedBaseContractIds.Reverse();

                foreach (int id in linearizedBaseContractIds)
                {
                    ContractDefinition baseContract = context.GetASTNodeById(id) as ContractDefinition;
                    Debug.Assert(baseContract != null);

                    if (baseContract == contract)
                    {
                        HashSet<VariableDeclaration> stateVars = context.GetStateVarsByContract(contract);
                        foreach (VariableDeclaration stateVar in stateVars)
                        {
                            string name = stateVar.Name;
                            context.AddStateVarToDynamicType(name, contract, stateVar);
                        }
                    }
                    else
                    {
                        HashSet<VariableDeclaration> stateVars = context.GetStateVarsByContract(baseContract);
                        foreach (VariableDeclaration stateVar in stateVars)
                        {
                            if (stateVar.Visibility == EnumVisibility.PRIVATE) continue;

                            string name = stateVar.Name;
                            context.AddStateVarToDynamicType(name, contract, stateVar);
                        }
                    }
                }
            }

            // PrintStateVarResolutionMap();
        }

        private void ComputeVisibleStateVariables()
        {
            foreach (string varName in context.StateVarNameResolutionMap.Keys) 
            {
                foreach (ContractDefinition contract in context.StateVarNameResolutionMap[varName].Keys)
                {
                    VariableDeclaration varDecl = context.StateVarNameResolutionMap[varName][contract];
                    context.AddVisibleStateVarToContract(varDecl, contract);
                }
            }

            // PrintVisibleStateVars();
        }

        private void PrintStateVarResolutionMap()
        {
            foreach (string name in context.StateVarNameResolutionMap.Keys)
            {
                Console.WriteLine("-- " + name);
                foreach (ContractDefinition dynamicType in context.StateVarNameResolutionMap[name].Keys)
                {
                    VariableDeclaration varDecl = context.GetStateVarByDynamicType(name, dynamicType);
                    ContractDefinition contract = context.GetContractByStateVarDecl(varDecl);
                    Console.WriteLine(dynamicType.Name + " --> " + contract.Name + "." + varDecl.Name);
                }
                Console.WriteLine();
            }
        }

        private void PrintVisibleStateVars()
        {
            foreach (ContractDefinition contract in context.ContractToVisibleStateVarsMap.Keys)
            {
                Console.WriteLine(contract.Name + ":");
                foreach (VariableDeclaration varDecl in context.ContractToVisibleStateVarsMap[contract])
                {
                    Console.WriteLine(varDecl);
                }
                Console.WriteLine();
            }
        }
    }
}
