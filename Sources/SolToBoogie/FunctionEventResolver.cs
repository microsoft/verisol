// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using SolidityAST;

    /**
     * Determine the visible functions/events for each contract considering
     * the inheritance hierarchy, and put the information in the context.
     */
    public class FunctionEventResolver
    {
        // require the ContractDefinitions member is populated
        // require the ContractToFunctionsMap member is populated
        private TranslatorContext context;

        public FunctionEventResolver(TranslatorContext context)
        {
            this.context = context;
        }

        // TODO: resolve events
        public void Resolve()
        {
            ResolveFunctions();
            ComputeVisibleFunctions();
        }

        private void ResolveFunctions()
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
                        HashSet<FunctionDefinition> functions = context.GetFuncDefintionsInContract(contract);
                        foreach (FunctionDefinition function in functions)
                        {
                            string signature = TransUtils.ComputeFunctionSignature(function);
                            context.AddFunctionToDynamicType(signature, contract, function);
                        }
                    }
                    else
                    {
                        HashSet<FunctionDefinition> functions = context.GetFuncDefintionsInContract(baseContract);
                        foreach (FunctionDefinition function in functions)
                        {
                            if (function.Visibility == EnumVisibility.PRIVATE) continue;

                            string signature = TransUtils.ComputeFunctionSignature(function);
                            context.AddFunctionToDynamicType(signature, contract, function);
                        }
                        // Events
                        // TODO: Do we need to lookup by signature?
                        HashSet<EventDefinition> events = context.GetEventDefintionsInContract(baseContract);
                        foreach (var evt in events)
                        {
                            context.AddEventToContract(contract, evt);
                        }
                    }
                }
            }

            // PrintFunctionResolutionMap();
        }

        private void ComputeVisibleFunctions()
        {
            foreach (string funcSig in context.FuncSigResolutionMap.Keys)
            {
                foreach (ContractDefinition contract in context.FuncSigResolutionMap[funcSig].Keys)
                {
                    FunctionDefinition funcDef = context.FuncSigResolutionMap[funcSig][contract];
                    context.AddVisibleFunctionToContract(funcDef, contract);
                }
            }

            // PrintVisibleFunctions();
        }

        private void PrintFunctionResolutionMap()
        {
            foreach (string signature in context.FuncSigResolutionMap.Keys)
            {
                Console.WriteLine("-- " + signature);
                foreach (ContractDefinition dynamicType in context.FuncSigResolutionMap[signature].Keys)
                {
                    FunctionDefinition function = context.GetFunctionByDynamicType(signature, dynamicType);
                    ContractDefinition contract = context.GetContractByFunction(function);
                    Console.WriteLine(dynamicType.Name + " --> " + contract.Name + "." + function.Name);
                }
                Console.WriteLine();
            }
        }

        private void PrintVisibleFunctions()
        {
            foreach (ContractDefinition contract in context.ContractToVisibleFunctionsMap.Keys)
            {
                Console.WriteLine(contract.Name + ":");
                foreach (FunctionDefinition funcDef in context.ContractToVisibleFunctionsMap[contract])
                {
                    string canonicalName = TransUtils.GetCanonicalFunctionName(funcDef, context);
                    Console.WriteLine(canonicalName);
                }
                Console.WriteLine();
            }
        }
    }
}
