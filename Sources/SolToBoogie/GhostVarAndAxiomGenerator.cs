// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolToBoogie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BoogieAST;
    using SolidityAST;

    public class GhostVarAndAxiomGenerator
    {
        // require the ContractDefintions member is populated
        private TranslatorContext context;

        private BoogieCtorType contractType = new BoogieCtorType("ContractName");

        public GhostVarAndAxiomGenerator(TranslatorContext context)
        {
            this.context = context;
        }

        public void Generate()
        {
            GenerateTypes();
            GenerateConstants();
            GenerateFunctions();
            GenerateGlobalVariables();
            GenerateGlobalImplementations();
            GenerateAxioms();
        }

        private void GenerateFunctions()
        {
            context.Program.AddDeclaration(GenerateConstToRefFunction());
            context.Program.AddDeclaration(GenerateKeccakFunction());
        }

        private BoogieFunction GenerateKeccakFunction()
        {
            //function for Int to Ref
            var inVar = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "keccak256",
                new List<BoogieVariable>() { inVar },
                new List<BoogieVariable>() { outVar },
                null);
        }
        private BoogieFunction GenerateConstToRefFunction()
        {
            //function for Int to Ref
            var inVar = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Ref));
            return new BoogieFunction(
                "ConstantToRef",
                new List<BoogieVariable>() { inVar },
                new List<BoogieVariable>() { outVar },
                null);
        }

        private void GenerateTypes()
        {
            context.Program.AddDeclaration(new BoogieTypeCtorDecl("Ref"));
            context.Program.AddDeclaration(new BoogieTypeCtorDecl("ContractName"));
        }

        private void GenerateConstants()
        {
            BoogieConstant nullConstant = new BoogieConstant(new BoogieTypedIdent("null", BoogieType.Ref), true);
            context.Program.AddDeclaration(nullConstant);

            // constants for contract names
            BoogieCtorType tnameType = new BoogieCtorType("ContractName");
            foreach (ContractDefinition contract in context.ContractDefinitions)
            {
                BoogieTypedIdent typedIdent = new BoogieTypedIdent(contract.Name, tnameType);
                BoogieConstant contractNameConstant = new BoogieConstant(typedIdent, true);
                context.Program.AddDeclaration(contractNameConstant);
            }
        }

        private void GenerateGlobalVariables()
        {
            BoogieTypedIdent dtypeId = new BoogieTypedIdent("DType", new BoogieMapType(BoogieType.Ref, contractType));
            BoogieGlobalVariable dtype = new BoogieGlobalVariable(dtypeId);
            context.Program.AddDeclaration(dtype);

            BoogieTypedIdent allocId = new BoogieTypedIdent("Alloc", new BoogieMapType(BoogieType.Ref, BoogieType.Bool));
            BoogieGlobalVariable alloc = new BoogieGlobalVariable(allocId);
            context.Program.AddDeclaration(alloc);

            BoogieTypedIdent addrBalanceId = new BoogieTypedIdent("balance_ADDR", new BoogieMapType(BoogieType.Ref, BoogieType.Int));
            BoogieGlobalVariable addrBalance = new BoogieGlobalVariable(addrBalanceId);
            context.Program.AddDeclaration(addrBalance);

            // generate global variables for each array/mapping type to model memory
            GenerateMemoryVariables();

            BoogieMapType type = new BoogieMapType(BoogieType.Ref, BoogieType.Int);
            BoogieTypedIdent arrayLengthId = new BoogieTypedIdent("Length", type);
            BoogieGlobalVariable arrayLength = new BoogieGlobalVariable(arrayLengthId);
            context.Program.AddDeclaration(arrayLength);
        }

        private void GenerateGlobalImplementations()
        {
            GenerateGlobalProcedureFresh();
            GenerateGlobalProcedureAllocMany();
            GenerateBoogieRecord("int", BoogieType.Int);
            GenerateBoogieRecord("ref", BoogieType.Ref);
            GenerateBoogieRecord("bool", BoogieType.Bool);
        }

        private void GenerateBoogieRecord(string typeName, BoogieType btype)
        {
            // generate the internal one without base constructors
            string procName = "boogie_si_record_sol2Bpl_" + typeName;
            var inVar = new BoogieFormalParam(new BoogieTypedIdent("x", btype));
            List<BoogieVariable> inParams = new List<BoogieVariable>() { inVar };
            List<BoogieVariable> outParams = new List<BoogieVariable>();

            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, null);
            context.Program.AddDeclaration(procedure);
        }

        private void GenerateGlobalProcedureFresh()
        {
            // generate the internal one without base constructors
            string procName = "FreshRefGenerator";
            List<BoogieVariable> inParams = new List<BoogieVariable>();

            var outVar = new BoogieFormalParam(new BoogieTypedIdent("newRef", BoogieType.Ref));
            List<BoogieVariable> outParams = new List<BoogieVariable>()
            {
                outVar
            };
            List<BoogieAttribute> attributes = new List<BoogieAttribute>()
            {
                new BoogieAttribute("inline", 10),
            };
            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            List<BoogieVariable> localVars = new List<BoogieVariable>();
            BoogieStmtList procBody = new BoogieStmtList();

            var outVarIdentifier = new BoogieIdentifierExpr("newRef");
            BoogieIdentifierExpr allocIdentExpr = new BoogieIdentifierExpr("Alloc");
            // havoc tmp;
            procBody.AddStatement(new BoogieHavocCmd(outVarIdentifier));
            // assume Alloc[tmp] == false;
            BoogieMapSelect allocMapSelect = new BoogieMapSelect(allocIdentExpr, outVarIdentifier);
            BoogieExpr allocAssumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, allocMapSelect, new BoogieLiteralExpr(false));
            procBody.AddStatement(new BoogieAssumeCmd(allocAssumeExpr));
            // Alloc[tmp] := true;
            procBody.AddStatement(new BoogieAssignCmd(allocMapSelect, new BoogieLiteralExpr(true)));

            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(implementation);
        }

        private void GenerateGlobalProcedureAllocMany()
        {
            // generate the internal one without base constructors
            string procName = "HavocAllocMany";
            List<BoogieVariable> inParams = new List<BoogieVariable>();
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            List<BoogieAttribute> attributes = new List<BoogieAttribute>()
            {
                new BoogieAttribute("inline", 10),
            };
            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            var oldAlloc = new BoogieLocalVariable(new BoogieTypedIdent("oldAlloc", new BoogieMapType(BoogieType.Ref, BoogieType.Bool)));
            List<BoogieVariable> localVars = new List<BoogieVariable>() {oldAlloc};
            BoogieStmtList procBody = new BoogieStmtList();
            BoogieIdentifierExpr oldAllocIdentExpr = new BoogieIdentifierExpr("oldAlloc");
            BoogieIdentifierExpr allocIdentExpr = new BoogieIdentifierExpr("Alloc");
            // oldAlloc = Alloc
            procBody.AddStatement(new BoogieAssignCmd(oldAllocIdentExpr, allocIdentExpr));            
            // havoc Alloc
            procBody.AddStatement(new BoogieHavocCmd(allocIdentExpr));
            // assume forall i:ref oldAlloc[i] ==> alloc[i]
            var qVar = QVarGenerator.NewQVar(0, 0);
            BoogieMapSelect allocMapSelect = new BoogieMapSelect(allocIdentExpr, qVar);
            BoogieMapSelect oldAllocMapSelect = new BoogieMapSelect(oldAllocIdentExpr, qVar);
            BoogieExpr allocAssumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.IMP, oldAllocMapSelect, allocMapSelect);
            procBody.AddStatement(new BoogieAssumeCmd(new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() {qVar}, new List<BoogieType>() { BoogieType.Ref }, allocAssumeExpr)));

            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(implementation);
        }

        private void GenerateAxioms()
        {
            context.Program.AddDeclaration(GenerateConstToRefAxiom());
        }

        private BoogieAxiom GenerateConstToRefAxiom()
        {

            var qVar1 = QVarGenerator.NewQVar(0, 0);
            var qVar2 = QVarGenerator.NewQVar(0, 1);
            var eqVar12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar1, qVar2);
            var qVar1Func = new BoogieFuncCallExpr("ConstantToRef", new List<BoogieExpr>() { qVar1 });
            var qVar2Func = new BoogieFuncCallExpr("ConstantToRef", new List<BoogieExpr>() { qVar2 });
            var eqFunc12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, qVar1Func, qVar2Func);
            var bodyExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, eqVar12, eqFunc12);

            // forall q1:int, q2:int :: q1 == q2 || ConstantToRef(q1) != ConstantToRef(q2) 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1, qVar2 }, new List<BoogieType>() { BoogieType.Int, BoogieType.Int }, bodyExpr);

            return new BoogieAxiom(qExpr);
        }

        private void GenerateMemoryVariables()
        {
            HashSet<KeyValuePair<BoogieType, BoogieType>> generatedTypes = new HashSet<KeyValuePair<BoogieType, BoogieType>>();
            // mappings
            foreach (ContractDefinition contract in context.ContractToMappingsMap.Keys)
            {
                foreach (VariableDeclaration varDecl in context.ContractToMappingsMap[contract])
                {
                    Debug.Assert(varDecl.TypeName is Mapping);
                    Mapping mapping = varDecl.TypeName as Mapping;
                    GenerateMemoryVariablesForMapping(mapping, generatedTypes);
                }
            }
            // arrays
            foreach (ContractDefinition contract in context.ContractToArraysMap.Keys)
            {
                foreach (VariableDeclaration varDecl in context.ContractToArraysMap[contract])
                {
                    Debug.Assert(varDecl.TypeName is ArrayTypeName);
                    ArrayTypeName array = varDecl.TypeName as ArrayTypeName;
                    GenerateMemoryVariablesForArray(array, generatedTypes);
                }
            }
        }

        private void GenerateMemoryVariablesForMapping(Mapping mapping, HashSet<KeyValuePair<BoogieType, BoogieType>> generatedTypes)
        {
            BoogieType boogieKeyType = TransUtils.GetBoogieTypeFromSolidityTypeName(mapping.KeyType);
            BoogieType boogieValType = null;
            if (mapping.ValueType is Mapping submapping)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForMapping(submapping, generatedTypes);
            }
            else if (mapping.ValueType is ArrayTypeName array)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForArray(array, generatedTypes);
            }
            else
            {
                boogieValType = TransUtils.GetBoogieTypeFromSolidityTypeName(mapping.ValueType);
            }

            KeyValuePair<BoogieType, BoogieType> pair = new KeyValuePair<BoogieType,BoogieType>(boogieKeyType, boogieValType);
            if (!generatedTypes.Contains(pair))
            {
                generatedTypes.Add(pair);
                GenerateSingleMemoryVariable(boogieKeyType, boogieValType);
            }
        }

        private void GenerateMemoryVariablesForArray(ArrayTypeName array, HashSet<KeyValuePair<BoogieType, BoogieType>> generatedTypes)
        {
            BoogieType boogieKeyType = BoogieType.Int;
            BoogieType boogieValType = null;
            if (array.BaseType is ArrayTypeName subarray)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForArray(subarray, generatedTypes);
            }
            else if (array.BaseType is Mapping mapping)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForMapping(mapping, generatedTypes);
            }
            else
            {
                boogieValType = TransUtils.GetBoogieTypeFromSolidityTypeName(array.BaseType);
            }

            KeyValuePair<BoogieType, BoogieType> pair = new KeyValuePair<BoogieType, BoogieType>(boogieKeyType, boogieValType);
            if (!generatedTypes.Contains(pair))
            {
                generatedTypes.Add(pair);
                GenerateSingleMemoryVariable(boogieKeyType, boogieValType);
            }
        }

        private void GenerateSingleMemoryVariable(BoogieType keyType, BoogieType valType)
        {
            BoogieMapType map = new BoogieMapType(keyType, valType);
            map = new BoogieMapType(BoogieType.Ref, map);

            string name = MapArrayHelper.GetMemoryMapName(keyType, valType);
            context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(name, map)));
        }
    }
}
