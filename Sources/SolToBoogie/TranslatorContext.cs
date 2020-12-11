// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using SolidityAnalysis;

namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using BoogieAST;
    using SolidityAST;

    public class TranslatorContext
    {
        // TODO: Make these configurable.
        public static int MAX_GAS_LIMIT = 8000000;

        public static int MIN_GAS_LIMIT = 4000000;

        public static int TX_GAS_COST = 21000;

        public static int CREATE_GAS_COST = 53000;

        public static int CALL_GAS_STIPEND = 2300;

        public BoogieProgram Program { get; private set; }

        public Dictionary<int, ASTNode> IdToNodeMap { get; set; }

        public string SourceDirectory { get; set; }

        // source file path of ASTNode
        public Dictionary<ASTNode, string> ASTNodeToSourcePathMap { get; private set; }

        // source file line number of ASTNode
        public Dictionary<ASTNode, int> ASTNodeToSourceLineNumberMap { get; private set; }

        // all contracts defined in the program
        public HashSet<ContractDefinition> ContractDefinitions { get; private set; }

        // map from each contract to its sub types (including itself)
        public Dictionary<ContractDefinition, HashSet<ContractDefinition>> ContractToSubTypesMap { get; private set; }

        // all state variables explicitly defined in each contract
        public Dictionary<ContractDefinition, HashSet<VariableDeclaration>> ContractToStateVarsMap { get; private set; }
        public Dictionary<VariableDeclaration, ContractDefinition> StateVarToContractMap { get; private set; }

        // all mappings explicitly defined in each contract
        public Dictionary<ContractDefinition, HashSet<VariableDeclaration>> ContractToMappingsMap { get; private set; }
        // all arrays explicityly defined in each contract
        public Dictionary<ContractDefinition, HashSet<VariableDeclaration>> ContractToArraysMap { get; private set; }

        // explicit constructor defined in each contract
        // solidity only allows at most one constructor for each contract
        public Dictionary<ContractDefinition, FunctionDefinition> ContractToConstructorMap { get; private set; }

        // explicit fallback defined in each contract
        // solidity only allows at most one fallback for each contract
        public Dictionary<ContractDefinition, FunctionDefinition> ContractToFallbackMap { get; private set; }

        // all events explicitly defined in each contract
        public Dictionary<ContractDefinition, HashSet<EventDefinition>> ContractToEventsMap { get; private set; }
        public Dictionary<EventDefinition, ContractDefinition> EventToContractMap { get; private set; }

        // all functions explicitly defined in each contract
        // FIXME: getters for public state variables
        public Dictionary<ContractDefinition, HashSet<FunctionDefinition>> ContractToFunctionsMap { get; private set; }
        public Dictionary<ContractDefinition, HashSet<string>> ContractToFuncSigsMap { get; private set; }
        public Dictionary<FunctionDefinition, ContractDefinition> FunctionToContractMap { get; private set; }

        // FunctionSignature -> (DynamicType -> FunctionDefinition)
        public Dictionary<string, Dictionary<ContractDefinition, FunctionDefinition>> FuncSigResolutionMap { get; private set; }
        // all functions (including private ones) visible in each contract
        public Dictionary<ContractDefinition, HashSet<FunctionDefinition>> ContractToVisibleFunctionsMap { get; private set; }

        // StateVarName -> (Dynamictype -> StateVariableDeclaration)
        public Dictionary<string, Dictionary<ContractDefinition, VariableDeclaration>> StateVarNameResolutionMap { get; private set; }
        // all state variables (including private ones) visible in each contract
        public Dictionary<ContractDefinition, HashSet<VariableDeclaration>> ContractToVisibleStateVarsMap { get; private set; }

        // M -> BoogieProcedure(A) for Modifier M {Stmt A; _; Stmt B}
        public Dictionary<string, BoogieProcedure> ModifierToBoogiePreProc { get; private set;}

        // M -> BoogieProcedure(B) for Modifier M {Stmt A; _; Stmt B}
        public Dictionary<string, BoogieProcedure> ModifierToBoogiePostProc { get; private set;}

        // M -> BoogieImplementation(A) for Modifier M {Stmt A; _; Stmt B}
        public Dictionary<string, BoogieImplementation> ModifierToBoogiePreImpl { get; private set;}

        // M -> BoogieImplementation(B) for Modifier M {Stmt A; _; Stmt B}
        public Dictionary<string, BoogieImplementation> ModifierToBoogiePostImpl { get; private set;}

        // M -> the set of local variables declared in the prelude (will become outputs to prelude call)
        public Dictionary<string, List<BoogieLocalVariable>> ModifierToPreludeLocalVars { get; private set; }

        public String EntryPointContract { get; private set; }
        
        public SolidityAnalyzer Analysis { get; private set; }

        // Options flags
        public TranslatorFlags TranslateFlags { get; private set; }

        public Dictionary<ContractDefinition, Dictionary<UserDefinedTypeName, List<TypeName>>> UsingMap => usingMap;

        // num of fresh identifiers, should be incremented when making new fresh id
        private int freshIdentifierCount = 0;

        // methods whose translation has to be skipped
        private HashSet<Tuple<string, string>> IgnoreMethods;

        // do we generate inline attributes (required for unbounded verification)
        private bool genInlineAttrInBpl;

        // data structures for using
        // maps Contract C --> (source, dest), where source is a library type
        private readonly Dictionary<ContractDefinition, Dictionary<UserDefinedTypeName, List<TypeName>>> usingMap;
        
        public HashSet<String> initFns { get; set; }

        public TranslatorContext(AST solidityAST, HashSet<Tuple<string, string>> ignoreMethods, bool _genInlineAttrInBpl, TranslatorFlags _translateFlags = null, String entryPointContract = "")
        {
            Program = new BoogieProgram();
            ContractDefinitions = new HashSet<ContractDefinition>();
            ASTNodeToSourcePathMap = new Dictionary<ASTNode, string>();
            ASTNodeToSourceLineNumberMap = new Dictionary<ASTNode, int>();
            ContractToSubTypesMap = new Dictionary<ContractDefinition, HashSet<ContractDefinition>>();
            ContractToStateVarsMap = new Dictionary<ContractDefinition, HashSet<VariableDeclaration>>();
            ContractToMappingsMap = new Dictionary<ContractDefinition, HashSet<VariableDeclaration>>();
            ContractToArraysMap = new Dictionary<ContractDefinition, HashSet<VariableDeclaration>>();
            StateVarToContractMap = new Dictionary<VariableDeclaration, ContractDefinition>();
            ContractToConstructorMap = new Dictionary<ContractDefinition, FunctionDefinition>();
            ContractToFallbackMap = new Dictionary<ContractDefinition, FunctionDefinition>();
            ContractToEventsMap = new Dictionary<ContractDefinition, HashSet<EventDefinition>>();
            EventToContractMap = new Dictionary<EventDefinition, ContractDefinition>();
            ContractToFunctionsMap = new Dictionary<ContractDefinition, HashSet<FunctionDefinition>>();
            ContractToFuncSigsMap = new Dictionary<ContractDefinition, HashSet<string>>();
            FunctionToContractMap = new Dictionary<FunctionDefinition, ContractDefinition>();
            FuncSigResolutionMap = new Dictionary<string, Dictionary<ContractDefinition, FunctionDefinition>>();
            ContractToVisibleFunctionsMap = new Dictionary<ContractDefinition, HashSet<FunctionDefinition>>();
            StateVarNameResolutionMap = new Dictionary<string, Dictionary<ContractDefinition, VariableDeclaration>>();
            ContractToVisibleStateVarsMap = new Dictionary<ContractDefinition, HashSet<VariableDeclaration>>();
            ModifierToBoogiePreProc = new Dictionary<string, BoogieProcedure>();
            ModifierToBoogiePostProc = new Dictionary<string, BoogieProcedure>();
            ModifierToBoogiePreImpl = new Dictionary<string, BoogieImplementation>();
            ModifierToBoogiePostImpl = new Dictionary<string, BoogieImplementation>();
            ModifierToPreludeLocalVars = new Dictionary<string, List<BoogieLocalVariable>>();
            usingMap = new Dictionary<ContractDefinition, Dictionary<UserDefinedTypeName, List<TypeName>>>();
            IgnoreMethods = ignoreMethods;
            genInlineAttrInBpl = _genInlineAttrInBpl;
            TranslateFlags = _translateFlags;
            EntryPointContract = entryPointContract;
            Analysis = new SolidityAnalyzer(solidityAST, ignoreMethods, entryPointContract);
            initFns = new HashSet<String>();
        }

        public bool HasASTNodeId(int id)
        {
            return IdToNodeMap.ContainsKey(id);
        }

        public ASTNode GetASTNodeById(int id)
        {
            Debug.Assert(IdToNodeMap.ContainsKey(id), $"Unknown id: {id}");
            return IdToNodeMap[id];
        }

        public void AddSourceInfoForASTNode(ASTNode node, string absolutePath, int lineNumber)
        {
            Debug.Assert(!ASTNodeToSourcePathMap.ContainsKey(node));
            Debug.Assert(!ASTNodeToSourceLineNumberMap.ContainsKey(node));
            ASTNodeToSourcePathMap[node] = absolutePath;
            ASTNodeToSourceLineNumberMap[node] = lineNumber;
        }

        public string GetAbsoluteSourcePathOfASTNode(ASTNode node)
        {
            Debug.Assert(ASTNodeToSourcePathMap.ContainsKey(node));
            return ASTNodeToSourcePathMap[node];
        }

        public int GetLineNumberOfASTNode(ASTNode node)
        {
            Debug.Assert(ASTNodeToSourceLineNumberMap.ContainsKey(node));
            return ASTNodeToSourceLineNumberMap[node];
        }

        public HashSet<FunctionDefinition> GetFuncDefintionsInContract(ContractDefinition contract)
        {
            if (ContractToFunctionsMap.ContainsKey(contract))
            {
                return ContractToFunctionsMap[contract];
            }
            return new HashSet<FunctionDefinition>();
        }
        public HashSet<EventDefinition> GetEventDefintionsInContract(ContractDefinition contract)
        {
            if (ContractToEventsMap.ContainsKey(contract))
            {
                return ContractToEventsMap[contract];
            }
            return new HashSet<EventDefinition>();
        }


        public void AddContract(ContractDefinition contract)
        {
            ContractDefinitions.Add(contract);
        }

        public bool HasContractName(string contractName)
        {
            foreach (ContractDefinition contract in ContractDefinitions)
            {
                if (contract.Name.Equals(contractName))
                {
                    return true;
                }
            }
            return false;
        }

        public ContractDefinition GetContractByName(string contractName)
        {
            foreach (ContractDefinition contract in ContractDefinitions)
            {
                if (contract.Name.Equals(contractName))
                {
                    return contract;
                }
            }
            Debug.Assert(false, $"Cannot find contract: {contractName}");
            return null;
        }

        public void AddSubTypeToContract(ContractDefinition contract, ContractDefinition subtype)
        {
            if (!ContractToSubTypesMap.ContainsKey(contract))
            {
                ContractToSubTypesMap[contract] = new HashSet<ContractDefinition>();
            }
            ContractToSubTypesMap[contract].Add(subtype);
        }

        public HashSet<ContractDefinition> GetSubTypesOfContract(ContractDefinition contract)
        {
            Debug.Assert(ContractToSubTypesMap.ContainsKey(contract), $"Cannot find {contract.Name} in the sub type map");
            return ContractToSubTypesMap[contract];
        }

        public void AddStateVarToContract(ContractDefinition contract, VariableDeclaration varDecl)
        {
            Debug.Assert(varDecl.StateVariable, $"{varDecl.Name} is not a state variable");
            if (!ContractToStateVarsMap.ContainsKey(contract))
            {
                ContractToStateVarsMap[contract] = new HashSet<VariableDeclaration>();
            }

            Debug.Assert(!ContractToStateVarsMap[contract].Contains(varDecl), $"Duplicated state variable: {varDecl.Name}");
            ContractToStateVarsMap[contract].Add(varDecl);

            Debug.Assert(!StateVarToContractMap.ContainsKey(varDecl), $"Duplicated state variable: {varDecl.Name}");
            StateVarToContractMap[varDecl] = contract;
        }

        public void AddMappingtoContract(ContractDefinition contract, VariableDeclaration mappingDecl)
        {
            Debug.Assert(mappingDecl.TypeName is Mapping, $"{mappingDecl.Name} is not a mapping");
            if (!ContractToMappingsMap.ContainsKey(contract))
            {
                ContractToMappingsMap[contract] = new HashSet<VariableDeclaration>();
            }
            Debug.Assert(!ContractToMappingsMap[contract].Contains(mappingDecl), $"Duplicated state mapping: {mappingDecl.Name}");
            ContractToMappingsMap[contract].Add(mappingDecl);
        }

        public void AddArrayToContract(ContractDefinition contract, VariableDeclaration arrayDecl)
        {
            Debug.Assert(arrayDecl.TypeName is ArrayTypeName, $"{arrayDecl.Name} is not an array");
            if (!ContractToArraysMap.ContainsKey(contract))
            {
                ContractToArraysMap[contract] = new HashSet<VariableDeclaration>();
            }
            Debug.Assert(!ContractToArraysMap[contract].Contains(arrayDecl), $"Duplicated state array: {arrayDecl.Name}");
            ContractToArraysMap[contract].Add(arrayDecl);
        }

        public bool HasStateVarInContract(ContractDefinition contract, VariableDeclaration varDecl)
        {
            if (!ContractToStateVarsMap.ContainsKey(contract))
            {
                return false;
            }
            return ContractToStateVarsMap[contract].Contains(varDecl);
        }

        public HashSet<VariableDeclaration> GetStateVarsByContract(ContractDefinition contract)
        {
            return ContractToStateVarsMap.ContainsKey(contract) ? ContractToStateVarsMap[contract] : new HashSet<VariableDeclaration>();
        }

        public void AddConstructorToContract(ContractDefinition contract, FunctionDefinition ctor)
        {
            Debug.Assert(ctor.IsConstructor, $"{ctor.Name} is not a constructor");
            Debug.Assert(!ContractToConstructorMap.ContainsKey(contract), $"Multiple constructors are defined for {contract.Name}");
            ContractToConstructorMap[contract] = ctor;
        }

        public void AddFallbackToContract(ContractDefinition contract, FunctionDefinition fallback)
        {
            Debug.Assert(fallback.IsFallback, $"{fallback.Name} is not a fallback function");
            Debug.Assert(!ContractToFallbackMap.ContainsKey(contract), $"Multiple fallbacks are defined for {contract.Name}");
            ContractToFallbackMap[contract] = fallback;
        }


        public bool IsConstructorDefined(ContractDefinition contract)
        {
            return ContractToConstructorMap.ContainsKey(contract);
        }

        public FunctionDefinition GetConstructorByContract(ContractDefinition contract)
        {
            if (ContractToConstructorMap.ContainsKey(contract))
            {
                return ContractToConstructorMap[contract];
            }
            throw new System.Exception($"Contract {contract.Name} does not have any constructors");
        }

        public void AddEventToContract(ContractDefinition contract, EventDefinition eventDef)
        {
            if (!ContractToEventsMap.ContainsKey(contract))
            {
                ContractToEventsMap[contract] = new HashSet<EventDefinition>();
            }

            //Debug.Assert(!ContractToEventsMap[contract].Contains(eventDef), $"Duplicated event definition: {eventDef.Name} in {contract.Name}");
            // For the case A <: B <: C and A has event E, it will be added to B and then to A through both B and C
            if (!ContractToEventsMap[contract].Contains(eventDef))
            {
                ContractToEventsMap[contract].Add(eventDef);
            }
        }

        public bool HasEventNameInContract(ContractDefinition contract, string eventName)
        {
            if (!ContractToEventsMap.ContainsKey(contract))
            {
                return false;
            }
            foreach (EventDefinition eventDef in ContractToEventsMap[contract])
            {
                if (eventName.Equals(eventDef.Name))
                {
                    return true;
                }
            }
            return false;
        }


        public void AddFunctionToContract(ContractDefinition contract, FunctionDefinition funcDef)
        {
            if (!ContractToFunctionsMap.ContainsKey(contract))
            {
                Debug.Assert(!ContractToFuncSigsMap.ContainsKey(contract));
                ContractToFunctionsMap[contract] = new HashSet<FunctionDefinition>();
                ContractToFuncSigsMap[contract] = new HashSet<string>();
            }

            Debug.Assert(!ContractToFunctionsMap[contract].Contains(funcDef), $"Duplicated function definition: {funcDef.Name}");
            ContractToFunctionsMap[contract].Add(funcDef);

            string signature = TransUtils.ComputeFunctionSignature(funcDef);
            Debug.Assert(!ContractToFuncSigsMap[contract].Contains(signature), $"Duplicated function signature: {signature}");
            ContractToFuncSigsMap[contract].Add(signature);

            Debug.Assert(!FunctionToContractMap.ContainsKey(funcDef), $"Duplicated function: {funcDef.Name}");
            FunctionToContractMap[funcDef] = contract;
        }

        public bool HasFuncSigInContract(ContractDefinition contract, string signature)
        {
            return ContractToFuncSigsMap.ContainsKey(contract);
        }

        public FunctionDefinition GetFunctionBySignature(ContractDefinition contract, string signature)
        {
            foreach (FunctionDefinition funcDef in ContractToFunctionsMap[contract])
            {
                if (TransUtils.ComputeFunctionSignature(funcDef).Equals(signature))
                {
                    return funcDef;
                }
            }
            Debug.Assert(false, $"Cannot find function signature: {signature}");
            return null;
        }

        public ContractDefinition GetContractByFunction(FunctionDefinition funcDef)
        {
            Debug.Assert(FunctionToContractMap.ContainsKey(funcDef));
            return FunctionToContractMap[funcDef];
        }

        public bool HasFuncSigInDynamicType(string funcSig, ContractDefinition dynamicType)
        {
            if (FuncSigResolutionMap.ContainsKey(funcSig) && FuncSigResolutionMap[funcSig].ContainsKey(dynamicType))
            {
                return true;
            }
            return false;
        }

        public void AddFunctionToDynamicType(string funcSig, ContractDefinition dynamicType, FunctionDefinition funcDef)
        {
            if (!FuncSigResolutionMap.ContainsKey(funcSig))
            {
                FuncSigResolutionMap[funcSig] = new Dictionary<ContractDefinition, FunctionDefinition>();
            }

            // may potentially override the previous value due to inheritance
            FuncSigResolutionMap[funcSig][dynamicType] = funcDef;
        }

        public bool HasFuncSignature(string funcSig)
        {
            return FuncSigResolutionMap.ContainsKey(funcSig);
        }

        public Dictionary<ContractDefinition, FunctionDefinition> GetAllFuncDefinitions(string funcSig)
        {
            return FuncSigResolutionMap[funcSig];
        }

        public FunctionDefinition GetFunctionByDynamicType(string funcSig, ContractDefinition dynamicType)
        {
            return FuncSigResolutionMap[funcSig][dynamicType];
        }

        public void AddStateVarToDynamicType(string varName, ContractDefinition dynamicType, VariableDeclaration varDecl)
        {
            Debug.Assert(varDecl.StateVariable);

            if (!StateVarNameResolutionMap.ContainsKey(varName))
            {
                StateVarNameResolutionMap[varName] = new Dictionary<ContractDefinition, VariableDeclaration>();
            }

            // may potentially override the previous value due to inheritance
            StateVarNameResolutionMap[varName][dynamicType] = varDecl;
        }

        public void AddVisibleFunctionToContract(FunctionDefinition funcDef, ContractDefinition contract)
        {
            if (!ContractToVisibleFunctionsMap.ContainsKey(contract))
            {
                ContractToVisibleFunctionsMap[contract] = new HashSet<FunctionDefinition>();
            }
            ContractToVisibleFunctionsMap[contract].Add(funcDef);
        }

        public HashSet<FunctionDefinition> GetVisibleFunctionsByContract(ContractDefinition contract)
        {
            return ContractToVisibleFunctionsMap.ContainsKey(contract) ?
                ContractToVisibleFunctionsMap[contract] :
                new HashSet<FunctionDefinition>();
        }

        public ContractDefinition GetContractByStateVarDecl(VariableDeclaration varDecl)
        {
            Debug.Assert(varDecl.StateVariable, $"{varDecl.Name} is not a state variable");
            return StateVarToContractMap[varDecl];
        }

        public bool HasStateVarName(string varName)
        {
            return StateVarNameResolutionMap.ContainsKey(varName);
        }

        public bool HasStateVar(string varName, ContractDefinition dynamicType)
        {
            return StateVarNameResolutionMap.ContainsKey(varName) &&
                   StateVarNameResolutionMap[varName].ContainsKey(dynamicType);
        }

        public VariableDeclaration GetStateVarByDynamicType(string varName, ContractDefinition dynamicType)
        {
            return StateVarNameResolutionMap[varName][dynamicType];
        }

        public void AddVisibleStateVarToContract(VariableDeclaration varDecl, ContractDefinition contract)
        {
            if (!ContractToVisibleStateVarsMap.ContainsKey(contract))
            {
                ContractToVisibleStateVarsMap[contract] = new HashSet<VariableDeclaration>();
            }
            ContractToVisibleStateVarsMap[contract].Add(varDecl);
        }

        public HashSet<VariableDeclaration> GetVisibleStateVarsByContract(ContractDefinition contract)
        {
            return ContractToVisibleStateVarsMap.ContainsKey(contract) ?
                ContractToVisibleStateVarsMap[contract] :
                new HashSet<VariableDeclaration>();
        }

        public BoogieTypedIdent MakeFreshTypedIdent(BoogieType type)
        {
            int index = ++freshIdentifierCount;
            string name = "__var_" + index;
            return new BoogieTypedIdent(name, type);
        }

        public void AddModiferToPreProc(string modifier, BoogieProcedure procedure)
        {
            if (ModifierToBoogiePreProc.ContainsKey(modifier))
            {
                throw new SystemException("duplicated modifier");
            }
            else
            {
                ModifierToBoogiePreProc[modifier] = procedure;
            }
        }

        public void AddModiferToPostProc(string modifier, BoogieProcedure procedure)
        {
            if (ModifierToBoogiePostProc.ContainsKey(modifier))
            {
                throw new SystemException("duplicated modifier");
            }
            else
            {
                ModifierToBoogiePostProc[modifier] = procedure;
            }
        }

        public void AddModiferToPreImpl(string modifier, BoogieImplementation procedure)
        {
            if (ModifierToBoogiePreImpl.ContainsKey(modifier))
            {
                throw new SystemException("duplicated modifier");
            }
            else
            {
                ModifierToBoogiePreImpl[modifier] = procedure;
            }
        }

        public void AddModiferToPostImpl(string modifier, BoogieImplementation procedure)
        {
            if (ModifierToBoogiePostImpl.ContainsKey(modifier))
            {
                throw new SystemException("duplicated modifier");
            }
            else
            {
                ModifierToBoogiePostImpl[modifier] = procedure;
            }
        }

        public void AddPreludeLocalsToModifier(string modifier, List<BoogieLocalVariable> localVars)
        {
            if (ModifierToPreludeLocalVars.ContainsKey(modifier))
            {
                throw new SystemException("duplicated modifier");
            }
            else
            {
                ModifierToPreludeLocalVars[modifier] = localVars;
            }
        }

        public bool IsMethodInIgnoredSet(FunctionDefinition func, ContractDefinition contract)
        {
            return IgnoreMethods.Any(x => x.Item2.Equals(contract.Name) && (x.Item1.Equals("*") || x.Item1.Equals(func.Name))) ;
        }
    }
}
