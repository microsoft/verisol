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

        private MapArrayHelper mapHelper;

        private BoogieCtorType contractType = new BoogieCtorType("ContractName");

        public GhostVarAndAxiomGenerator(TranslatorContext context, MapArrayHelper mapHelper)
        {
            this.context = context;
            this.mapHelper = mapHelper;
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
            context.Program.AddDeclaration(GenerateRefToInt());
            context.Program.AddDeclaration(GenerateModFunction());
            context.Program.AddDeclaration(GenerateKeccakFunction());
            context.Program.AddDeclaration(GenerateAbiEncodedFunctionOneArg());
            context.Program.AddDeclaration(GenerateVeriSolSumFunction());
            context.Program.AddDeclaration(GenerateAbiEncodedFunctionTwoArgs());
            context.Program.AddDeclaration(GenerateAbiEncodedFunctionOneArgRef());
            context.Program.AddDeclaration(GenerateAbiEncodedFunctionTwoArgsOneRef());

            if (context.TranslateFlags.QuantFreeAllocs)
            {
                if (context.TranslateFlags.UseMultiDim)
                {
                    foreach(VariableDeclaration decl in context.Analysis.Alias.getResults())
                    {
                        TypeName type = decl.TypeName;
                        if (type is Mapping || type is ArrayTypeName)
                        {
                            BoogieFunction initFn = MapArrayHelper.GenerateMultiDimZeroFunction(decl);

                            if (!context.initFns.Contains(initFn.Name))
                            {
                                context.Program.AddDeclaration(initFn);
                                context.initFns.Add(initFn.Name);
                            }
                            
                        }
                    }
                }
                else
                {
                    context.Program.AddDeclaration(GenerateZeroRefIntArrayFunction());
                    context.Program.AddDeclaration(GenerateZeroIntIntArrayFunction());
                    context.Program.AddDeclaration(GenerateZeroRefBoolArrayFunction());
                    context.Program.AddDeclaration(GenerateZeroIntBoolArrayFunction());
                    context.Program.AddDeclaration(GenerateZeroRefRefArrayFunction());
                    context.Program.AddDeclaration(GenerateZeroIntRefArrayFunction());
                }
                

                
            }

            if (context.TranslateFlags.NoNonlinearArith)
            {
                context.Program.AddDeclaration(generateNonlinearMulFunction());
                context.Program.AddDeclaration(generateNonlinearDivFunction());
                context.Program.AddDeclaration(generateNonlinearPowFunction());
                context.Program.AddDeclaration(generateNonlinearModFunction());
            }
        }

        private BoogieFunction GenerateZeroRefIntArrayFunction()
        {
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", new BoogieMapType(BoogieType.Ref, BoogieType.Int)));
            return new BoogieFunction(
                "zeroRefIntArr",
                new List<BoogieVariable>(), 
                new List<BoogieVariable>() {outVar},
                new List<BoogieAttribute>() { new BoogieAttribute("smtdefined", "\"((as const (Array Int Int)) 0)\"")}
                );
        }
        
        private BoogieFunction GenerateZeroIntIntArrayFunction()
        {
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", new BoogieMapType(BoogieType.Int, BoogieType.Int)));
            return new BoogieFunction(
                "zeroIntIntArr",
                new List<BoogieVariable>(), 
                new List<BoogieVariable>() {outVar},
                new List<BoogieAttribute>() { new BoogieAttribute("smtdefined", "\"((as const (Array Int Int)) 0)\"")}
            );
        }
        
        private BoogieFunction GenerateZeroRefBoolArrayFunction()
        {
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", new BoogieMapType(BoogieType.Ref, BoogieType.Bool)));
            return new BoogieFunction(
                "zeroRefBoolArr",
                new List<BoogieVariable>(), 
                new List<BoogieVariable>() {outVar},
                new List<BoogieAttribute>() { new BoogieAttribute("smtdefined", "\"((as const (Array Int Bool)) false)\"")}
            );
        }
        
        private BoogieFunction GenerateZeroIntBoolArrayFunction()
        {
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", new BoogieMapType(BoogieType.Int, BoogieType.Bool)));
            return new BoogieFunction(
                "zeroIntBoolArr",
                new List<BoogieVariable>(), 
                new List<BoogieVariable>() {outVar},
                new List<BoogieAttribute>() { new BoogieAttribute("smtdefined", "\"((as const (Array Int Bool)) false)\"")}
            );
        }
        
        private BoogieFunction GenerateZeroRefRefArrayFunction()
        {
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", new BoogieMapType(BoogieType.Ref, BoogieType.Ref)));
            return new BoogieFunction(
                "zeroRefRefArr",
                new List<BoogieVariable>(), 
                new List<BoogieVariable>() {outVar},
                new List<BoogieAttribute>() { new BoogieAttribute("smtdefined", "\"((as const (Array Int Int)) 0)\"")}
            );
        }
        
        private BoogieFunction GenerateZeroIntRefArrayFunction()
        {
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", new BoogieMapType(BoogieType.Int, BoogieType.Ref)));
            return new BoogieFunction(
                "zeroIntRefArr",
                new List<BoogieVariable>(), 
                new List<BoogieVariable>() {outVar},
                new List<BoogieAttribute>() { new BoogieAttribute("smtdefined", "\"((as const (Array Int Int)) 0)\"")}
            );
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
        private BoogieFunction GenerateAbiEncodedFunctionOneArg()
        {
            //function for Int to Int
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "abiEncodePacked1",
                new List<BoogieVariable>() { inVar1},
                new List<BoogieVariable>() { outVar },
                null);
        }
        private BoogieFunction GenerateAbiEncodedFunctionTwoArgs()
        {
            //function for Int*Int to Int
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "abiEncodePacked2",
                new List<BoogieVariable>() { inVar1, inVar2 },
                new List<BoogieVariable>() { outVar },
                null);
        }

        private BoogieFunction GenerateAbiEncodedFunctionOneArgRef()
        {
            //function for Int to Int
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Ref));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "abiEncodePacked1R",
                new List<BoogieVariable>() { inVar1 },
                new List<BoogieVariable>() { outVar },
                null);
        }
        private BoogieFunction GenerateAbiEncodedFunctionTwoArgsOneRef()
        {
            //function for Int*Int to Int
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Ref));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "abiEncodePacked2R",
                new List<BoogieVariable>() { inVar1, inVar2 },
                new List<BoogieVariable>() { outVar },
                null);
        }


        private BoogieFunction GenerateConstToRefFunction()
        {
            //function for Int to Ref
            var inVar = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Ref));
            BoogieAttribute attr = new BoogieAttribute("smtdefined", "\"x\"");
            return new BoogieFunction(
                "ConstantToRef",
                new List<BoogieVariable>() { inVar },
                new List<BoogieVariable>() { outVar },
                new List<BoogieAttribute>() { attr });
        }

        private BoogieFunction GenerateRefToInt()
        {
            //function for Ref to Int
            var inVar = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Ref));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "BoogieRefToInt",
                new List<BoogieVariable>() { inVar },
                new List<BoogieVariable>() { outVar },
                null);
        }

        private BoogieFunction GenerateModFunction()
        {
            //function for arithmetic "modulo" operation for unsigned integers
            string functionName = "modBpl";
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                functionName,
                new List<BoogieVariable>() { inVar1, inVar2 },
                new List<BoogieVariable>() { outVar },
                new List<BoogieAttribute> { new BoogieAttribute("bvbuiltin", "\"" + "mod" + "\"") });
        }

        private BoogieFunction GenerateVeriSolSumFunction()
        {
            //function for [Ref]int to int
            var inVar = new BoogieFormalParam(new BoogieTypedIdent("x", new BoogieMapType(BoogieType.Ref, BoogieType.Int)));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            return new BoogieFunction(
                "_SumMapping_VeriSol",
                new List<BoogieVariable>() { inVar },
                new List<BoogieVariable>() { outVar },
                null);
        }

        private BoogieFunction generateNonlinearMulFunction()
        {
            string fnName = "nonlinearMul";
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            
            return new BoogieFunction(fnName, new List<BoogieVariable>() {inVar1, inVar2}, 
                new List<BoogieVariable>() {outVar});
        }
        
        private BoogieFunction generateNonlinearDivFunction()
        {
            string fnName = "nonlinearDiv";
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            
            return new BoogieFunction(fnName, new List<BoogieVariable>() {inVar1, inVar2}, 
                new List<BoogieVariable>() {outVar});
        }
        
        private BoogieFunction generateNonlinearPowFunction()
        {
            string fnName = "nonlinearPow";
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            
            return new BoogieFunction(fnName, new List<BoogieVariable>() {inVar1, inVar2}, 
                new List<BoogieVariable>() {outVar});
        }
        
        private BoogieFunction generateNonlinearModFunction()
        {
            string fnName = "nonlinearMod";
            var inVar1 = new BoogieFormalParam(new BoogieTypedIdent("x", BoogieType.Int));
            var inVar2 = new BoogieFormalParam(new BoogieTypedIdent("y", BoogieType.Int));
            var outVar = new BoogieFormalParam(new BoogieTypedIdent("ret", BoogieType.Int));
            
            return new BoogieFunction(fnName, new List<BoogieVariable>() {inVar1, inVar2}, 
                new List<BoogieVariable>() {outVar});
        }


        private void GenerateTypes()
        {
            if (context.TranslateFlags.NoCustomTypes)
            {
                context.Program.AddDeclaration(new BoogieTypeCtorDecl("Ref", BoogieType.Int));
                context.Program.AddDeclaration(new BoogieTypeCtorDecl("ContractName", BoogieType.Int));
            }
            else
            {
                context.Program.AddDeclaration(new BoogieTypeCtorDecl("Ref"));
                context.Program.AddDeclaration(new BoogieTypeCtorDecl("ContractName"));
            }
            
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
                foreach(var node in contract.Nodes)
                {
                    if (node is StructDefinition structDefn)
                    {
                        var structTypedIdent = new BoogieTypedIdent(structDefn.CanonicalName, tnameType);
                        context.Program.AddDeclaration(new BoogieConstant(structTypedIdent, true));
                    }
                }
            }
        }

        private void GenerateGlobalVariables()
        {
            BoogieTypedIdent balanceId = new BoogieTypedIdent("Balance", new BoogieMapType(BoogieType.Ref, BoogieType.Int));
            BoogieGlobalVariable balanceVar = new BoogieGlobalVariable(balanceId);
            context.Program.AddDeclaration(balanceVar);

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

            if (context.TranslateFlags.ModelReverts)
            {
                BoogieTypedIdent revertId = new BoogieTypedIdent("revert", BoogieType.Bool);
                BoogieGlobalVariable revert = new BoogieGlobalVariable(revertId);
                context.Program.AddDeclaration(revert);
            }

            if (context.TranslateFlags.InstrumentGas)
            {
                BoogieTypedIdent gasId = new BoogieTypedIdent("gas", BoogieType.Int);
                BoogieGlobalVariable gas = new BoogieGlobalVariable(gasId);
                context.Program.AddDeclaration(gas);
            }

            // Solidity-specific vars
            BoogieTypedIdent nowVar = new BoogieTypedIdent("now", BoogieType.Int);
            context.Program.AddDeclaration(new BoogieGlobalVariable(nowVar));
        }

        private void GenerateGlobalImplementations()
        {
            GenerateGlobalProcedureFresh();
            GenerateGlobalProcedureAllocMany();
            GenerateBoogieRecord("int", BoogieType.Int);
            GenerateBoogieRecord("ref", BoogieType.Ref);
            GenerateBoogieRecord("bool", BoogieType.Bool);
            GenerateStructConstructors();
        }

        private void GenerateStructConstructors()
        {
            foreach (ContractDefinition contract in context.ContractDefinitions)
            {
                foreach (var node in contract.Nodes)
                {
                    if (node is StructDefinition structDefn)
                    {
                        GenerateStructConstructors(contract, structDefn);
                    }
                }
            }
        }

        private void GenerateStructConstructors(ContractDefinition contract, StructDefinition structDefn)
        {
            // generate the internal one without base constructors
            string procName = structDefn.CanonicalName + "_ctor";
            List<BoogieVariable> inParams = new List<BoogieVariable>();
            inParams.AddRange(TransUtils.GetDefaultInParams());
            foreach(var member in structDefn.Members)
            {
                Debug.Assert(!member.TypeDescriptions.IsStruct(), "Do no handle nested structs yet!");
                var formalType = TransUtils.GetBoogieTypeFromSolidityTypeName(member.TypeName);
                var formalName = member.Name;
                inParams.Add(new BoogieFormalParam(new BoogieTypedIdent(formalName, formalType)));
            }

            List<BoogieVariable> outParams = new List<BoogieVariable>();
            List<BoogieAttribute> attributes = new List<BoogieAttribute>();
            if (context.TranslateFlags.GenerateInlineAttributes)
            {
                attributes.Add(new BoogieAttribute("inline", 1));
            };
            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            List<BoogieVariable> localVars = new List<BoogieVariable>();
            BoogieStmtList procBody = new BoogieStmtList();

            foreach (var member in structDefn.Members)
            {
                //f[this] = f_arg
                Debug.Assert(!member.TypeDescriptions.IsStruct(), "Do no handle nested structs yet!");
                var mapName = member.Name + "_" + structDefn.CanonicalName;
                var formalName = member.Name;
                var mapSelectExpr = new BoogieMapSelect(new BoogieIdentifierExpr(mapName), new BoogieIdentifierExpr("this"));
                procBody.AddStatement(new BoogieAssignCmd(mapSelectExpr, new BoogieIdentifierExpr(member.Name)));
            }

            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(implementation);
        }

        private void GenerateBoogieRecord(string typeName, BoogieType btype)
        {
            if (context.TranslateFlags.NoDataValuesInfoFlag)
                return; 

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
            List<BoogieAttribute> attributes = new List<BoogieAttribute>();
            if (context.TranslateFlags.GenerateInlineAttributes)
            {
                attributes.Add(new BoogieAttribute("inline", 1));
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
            // assume tmp != null
            procBody.AddStatement(new BoogieAssumeCmd(
                              new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, outVarIdentifier, new BoogieIdentifierExpr("null"))));

            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(implementation);
        }

        private void GenerateGlobalProcedureAllocMany()
        {
            if (context.TranslateFlags.LazyNestedAlloc)
                return;

            // generate the internal one without base constructors
            string procName = "HavocAllocMany";
            List<BoogieVariable> inParams = new List<BoogieVariable>();
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            List<BoogieAttribute> attributes = new List<BoogieAttribute>();
            if (context.TranslateFlags.GenerateInlineAttributes)
            {
                attributes.Add(new BoogieAttribute("inline", 1));
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
            if (context.TranslateFlags.NoAxiomsFlag)
                return;

            context.Program.AddDeclaration(GenerateConstToRefAxiom());
            context.Program.AddDeclaration(GenerateKeccakAxiom());
            context.Program.AddDeclaration(GenerateAbiEncodePackedAxiomOneArg());

            GenerateVeriSolSumAxioms().ForEach(x => context.Program.AddDeclaration(x));

            context.Program.AddDeclaration(GenerateAbiEncodePackedAxiomTwoArgs());

            context.Program.AddDeclaration(GenerateAbiEncodePackedAxiomOneArgRef());
            context.Program.AddDeclaration(GenerateAbiEncodePackedAxiomTwoArgsOneRef());

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
            var triggers = new List<BoogieExpr>() { qVar1Func, qVar2Func };

            // forall q1:int, q2:int :: q1 == q2 || ConstantToRef(q1) != ConstantToRef(q2) 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1, qVar2 }, new List<BoogieType>() { BoogieType.Int, BoogieType.Int }, bodyExpr, triggers);

            return new BoogieAxiom(qExpr);
        }
        private BoogieAxiom GenerateKeccakAxiom()
        {

            var qVar1 = QVarGenerator.NewQVar(0, 0);
            var qVar2 = QVarGenerator.NewQVar(0, 1);
            var eqVar12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar1, qVar2);
            var qVar1Func = new BoogieFuncCallExpr("keccak256", new List<BoogieExpr>() { qVar1 });
            var qVar2Func = new BoogieFuncCallExpr("keccak256", new List<BoogieExpr>() { qVar2 });
            var eqFunc12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, qVar1Func, qVar2Func);
            var bodyExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, eqVar12, eqFunc12);
            var triggers = new List<BoogieExpr>() { qVar1Func, qVar2Func };

            // forall q1:int, q2:int :: q1 == q2 || keccak256(q1) != keccak256(q2) 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1, qVar2 }, new List<BoogieType>() { BoogieType.Int, BoogieType.Int }, bodyExpr, triggers);

            return new BoogieAxiom(qExpr);
        }
        private BoogieAxiom GenerateAbiEncodePackedAxiomOneArg()
        {

            var qVar1 = QVarGenerator.NewQVar(0, 0);
            var qVar2 = QVarGenerator.NewQVar(0, 1);
            var eqVar12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar1, qVar2);
            var qVar1Func = new BoogieFuncCallExpr("abiEncodePacked1", new List<BoogieExpr>() { qVar1 });
            var qVar2Func = new BoogieFuncCallExpr("abiEncodePacked1", new List<BoogieExpr>() { qVar2 });
            var eqFunc12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, qVar1Func, qVar2Func);
            var bodyExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, eqVar12, eqFunc12);
            var triggers = new List<BoogieExpr>() { qVar1Func, qVar2Func };

            // forall q1:int, q2:int :: q1 == q2 || abiEncodePacked(q1) != abiEncodePacked(q2) 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1, qVar2 }, new List<BoogieType>() { BoogieType.Int, BoogieType.Int }, bodyExpr, triggers);

            return new BoogieAxiom(qExpr);
        }
        private BoogieAxiom GenerateAbiEncodePackedAxiomTwoArgs()
        {
            var qVar11 = QVarGenerator.NewQVar(0, 0);
            var qVar12 = QVarGenerator.NewQVar(0, 1);
            var qVar21 = QVarGenerator.NewQVar(1, 0);
            var qVar22 = QVarGenerator.NewQVar(1, 1);
            var eqVar1 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar11, qVar12);
            var eqVar2 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar21, qVar22);
            var qVar1Func = new BoogieFuncCallExpr("abiEncodePacked2", new List<BoogieExpr>() { qVar11, qVar21 });
            var qVar2Func = new BoogieFuncCallExpr("abiEncodePacked2", new List<BoogieExpr>() { qVar12, qVar22 });
            var triggers = new List<BoogieExpr>() { qVar1Func, qVar2Func };

            var eqFunc12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, qVar1Func, qVar2Func);
            var eqArgs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, eqVar1, eqVar2);
            var bodyExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, eqArgs, eqFunc12);

            // forall q1:int, q2:int, q1', q2' :: (q1 == q1' && q2 == q2') || abiEncodePacked(q1, q2) != abiEncodePacked(q1', q2') 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar11, qVar12, qVar21, qVar22 }, 
                new List<BoogieType>() { BoogieType.Int, BoogieType.Int, BoogieType.Int, BoogieType.Int }, bodyExpr, triggers);

            return new BoogieAxiom(qExpr);
        }


        private BoogieAxiom GenerateAbiEncodePackedAxiomOneArgRef()
        {

            var qVar1 = QVarGenerator.NewQVar(0, 0);
            var qVar2 = QVarGenerator.NewQVar(0, 1);
            var eqVar12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar1, qVar2);
            var qVar1Func = new BoogieFuncCallExpr("abiEncodePacked1R", new List<BoogieExpr>() { qVar1 });
            var qVar2Func = new BoogieFuncCallExpr("abiEncodePacked1R", new List<BoogieExpr>() { qVar2 });
            var eqFunc12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, qVar1Func, qVar2Func);
            var bodyExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, eqVar12, eqFunc12);
            var triggers = new List<BoogieExpr>() { qVar1Func, qVar2Func };

            // forall q1:int, q2:int :: q1 == q2 || abiEncodePacked(q1) != abiEncodePacked(q2) 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar1, qVar2 }, new List<BoogieType>() { BoogieType.Ref, BoogieType.Ref}, bodyExpr, triggers);

            return new BoogieAxiom(qExpr);
        }
        private List<BoogieAxiom> GenerateVeriSolSumAxioms()
        {
            // axiom(forall m:[Ref]int :: (exists _a: Ref::m[_a] != 0) || _SumMapping_VeriSol(m) == 0);
            var qVar1 = QVarGenerator.NewQVar(0, 0);
            var qVar2 = QVarGenerator.NewQVar(0, 1);
            var ma = new BoogieMapSelect(qVar1, qVar2);
            var maEq0 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, ma, new BoogieLiteralExpr(System.Numerics.BigInteger.Zero));
            var existsMaNeq0 = new BoogieQuantifiedExpr(false,
                new List<BoogieIdentifierExpr>() { qVar2 },
                new List<BoogieType>() { BoogieType.Ref },
                maEq0,
                null);

            var sumM = new BoogieFuncCallExpr("_SumMapping_VeriSol", new List<BoogieExpr>() { qVar1 });
            var sumMEq0 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, sumM, new BoogieLiteralExpr(System.Numerics.BigInteger.Zero));
            var maEq0SumEq0 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, existsMaNeq0, sumMEq0);
            var axiom1 = new BoogieQuantifiedExpr(true,
                new List<BoogieIdentifierExpr>() { qVar1},
                new List<BoogieType>() { new BoogieMapType(BoogieType.Ref, BoogieType.Int)},
                maEq0SumEq0,
                null);


            // axiom(forall m:[Ref]int, _a: Ref, _b: int :: _SumMapping_VeriSol(m[_a:= _b]) == _SumMapping_VeriSol(m) - m[_a] + _b);
            var qVar3 = QVarGenerator.NewQVar(0, 2);
            var subExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, sumM, ma);
            var addExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, subExpr, qVar3);
            var updExpr = new BoogieMapUpdate(qVar1, qVar2, qVar3);
            var sumUpdExpr = new BoogieFuncCallExpr("_SumMapping_VeriSol", new List<BoogieExpr>() {updExpr});
            var sumUpdEqExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, sumUpdExpr, addExpr);
            var axiom2 = new BoogieQuantifiedExpr(true,
                new List<BoogieIdentifierExpr>() { qVar1, qVar2, qVar3 },
                new List<BoogieType>() { new BoogieMapType(BoogieType.Ref, BoogieType.Int), BoogieType.Ref, BoogieType.Int },
                sumUpdEqExpr,
                null);

            return new List<BoogieAxiom>() { new BoogieAxiom(axiom1), new BoogieAxiom(axiom2) };
        }


        private BoogieAxiom GenerateAbiEncodePackedAxiomTwoArgsOneRef()
        {
            var qVar11 = QVarGenerator.NewQVar(0, 0);
            var qVar12 = QVarGenerator.NewQVar(0, 1);
            var qVar21 = QVarGenerator.NewQVar(1, 0);
            var qVar22 = QVarGenerator.NewQVar(1, 1);
            var eqVar1 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar11, qVar12);
            var eqVar2 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, qVar21, qVar22);
            var qVar1Func = new BoogieFuncCallExpr("abiEncodePacked2R", new List<BoogieExpr>() { qVar11, qVar21 });
            var qVar2Func = new BoogieFuncCallExpr("abiEncodePacked2R", new List<BoogieExpr>() { qVar12, qVar22 });
            var triggers = new List<BoogieExpr>() { qVar1Func, qVar2Func };

            var eqFunc12 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, qVar1Func, qVar2Func);
            var eqArgs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, eqVar1, eqVar2);
            var bodyExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, eqArgs, eqFunc12);

            // forall q1:int, q2:int, q1', q2' :: (q1 == q1' && q2 == q2') || abiEncodePacked(q1, q2) != abiEncodePacked(q1', q2') 
            var qExpr = new BoogieQuantifiedExpr(true, new List<BoogieIdentifierExpr>() { qVar11, qVar12, qVar21, qVar22 },
                new List<BoogieType>() { BoogieType.Ref, BoogieType.Ref, BoogieType.Int, BoogieType.Int }, bodyExpr, triggers);

            return new BoogieAxiom(qExpr);
        }


        private void GenerateMemoryVariables()
        {
            HashSet<String> generatedMaps = new HashSet<String>();
            // mappings
            foreach (ContractDefinition contract in context.ContractToMappingsMap.Keys)
            {
                foreach (VariableDeclaration varDecl in context.ContractToMappingsMap[contract])
                {
                    Debug.Assert(varDecl.TypeName is Mapping);
                    Mapping mapping = varDecl.TypeName as Mapping;
                    GenerateMemoryVariablesForMapping(varDecl, mapping, generatedMaps, 0);
                    
                    if (context.TranslateFlags.InstrumentSums)
                    {
                        String sumName = mapHelper.GetSumName(varDecl);
                        if (!generatedMaps.Contains(sumName))
                        {
                            generatedMaps.Add(sumName);
                            BoogieType sumType = new BoogieMapType(BoogieType.Ref, BoogieType.Int);
                            context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(sumName, sumType)));
                        }
                    }
                }
            }
            // arrays
            foreach (ContractDefinition contract in context.ContractToArraysMap.Keys)
            {
                foreach (VariableDeclaration varDecl in context.ContractToArraysMap[contract])
                {
                    Debug.Assert(varDecl.TypeName is ArrayTypeName);
                    ArrayTypeName array = varDecl.TypeName as ArrayTypeName;
                    GenerateMemoryVariablesForArray(varDecl, array, generatedMaps, 0);
                    
                    if (context.TranslateFlags.InstrumentSums)
                    {
                        String sumName = mapHelper.GetSumName(varDecl);
                        if (!generatedMaps.Contains(sumName))
                        {
                            generatedMaps.Add(sumName);
                            BoogieType sumType = new BoogieMapType(BoogieType.Ref, BoogieType.Int);
                            context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(sumName, sumType)));
                        }
                    }
                }
            }
        }

        private void GenerateMemoryVariablesForMapping(VariableDeclaration decl, Mapping mapping, HashSet<String> generatedMaps, int lvl)
        {
            BoogieType boogieKeyType = TransUtils.GetBoogieTypeFromSolidityTypeName(mapping.KeyType);
            BoogieType boogieValType = null;
            if (mapping.ValueType is Mapping submapping)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForMapping(decl, submapping, generatedMaps, lvl + 1);
                
                // The last level gets initialized all at once
                if (context.TranslateFlags.LazyAllocNoMod)
                {
                    BoogieMapType mapType = new BoogieMapType(BoogieType.Ref, new BoogieMapType(boogieKeyType, BoogieType.Bool));
                    context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(mapHelper.GetNestedAllocName(decl, lvl), mapType)));
                }
            }
            else if (mapping.ValueType is ArrayTypeName array)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForArray(decl, array, generatedMaps, lvl + 1);
                
                if (context.TranslateFlags.LazyAllocNoMod)
                {
                    BoogieMapType mapType = new BoogieMapType(BoogieType.Ref, new BoogieMapType(boogieKeyType, BoogieType.Bool));
                    context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(mapHelper.GetNestedAllocName(decl, lvl), mapType)));
                }
            }
            else
            {
                boogieValType = TransUtils.GetBoogieTypeFromSolidityTypeName(mapping.ValueType);
            }

            KeyValuePair<BoogieType, BoogieType> pair = new KeyValuePair<BoogieType,BoogieType>(boogieKeyType, boogieValType);

            GenerateSingleMemoryVariable(decl, boogieKeyType, boogieValType, generatedMaps);
        }

        private void GenerateMemoryVariablesForArray(VariableDeclaration decl, ArrayTypeName array, HashSet<String> generatedMaps, int lvl)
        {
            BoogieType boogieKeyType = BoogieType.Int;
            BoogieType boogieValType = null;
            if (array.BaseType is ArrayTypeName subarray)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForArray(decl, subarray, generatedMaps, lvl + 1);
                
                // The last level gets initialized all at once
                if (context.TranslateFlags.LazyAllocNoMod)
                {
                    BoogieMapType mapType = new BoogieMapType(BoogieType.Ref, new BoogieMapType(boogieKeyType, BoogieType.Bool));
                    context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(mapHelper.GetNestedAllocName(decl, lvl), mapType)));
                }
            }
            else if (array.BaseType is Mapping mapping)
            {
                boogieValType = BoogieType.Ref;
                GenerateMemoryVariablesForMapping(decl, mapping, generatedMaps, lvl + 1);
                
                if (context.TranslateFlags.LazyAllocNoMod)
                {
                    BoogieMapType mapType = new BoogieMapType(BoogieType.Ref, new BoogieMapType(boogieKeyType, BoogieType.Bool));
                    context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(mapHelper.GetNestedAllocName(decl, lvl), mapType)));
                }
            }
            else
            {
                boogieValType = TransUtils.GetBoogieTypeFromSolidityTypeName(array.BaseType);
            }
            
            

            KeyValuePair<BoogieType, BoogieType> pair = new KeyValuePair<BoogieType, BoogieType>(boogieKeyType, boogieValType);
            GenerateSingleMemoryVariable(decl, boogieKeyType, boogieValType, generatedMaps);
        }

        private void GenerateSingleMemoryVariable(VariableDeclaration decl, BoogieType keyType, BoogieType valType, HashSet<String> generatedMaps)
        {
            BoogieMapType map = new BoogieMapType(keyType, valType);
            map = new BoogieMapType(BoogieType.Ref, map);

            string name = mapHelper.GetMemoryMapName(decl, keyType, valType);
            if (!generatedMaps.Contains(name))
            {
                BoogieFunction initFn = MapArrayHelper.GenerateMultiDimZeroFunction(keyType, valType);
                if (!context.initFns.Contains(initFn.Name))
                {
                    context.initFns.Add(initFn.Name);
                    context.Program.AddDeclaration(initFn);
                }
                
                generatedMaps.Add(name);
                context.Program.AddDeclaration(new BoogieGlobalVariable(new BoogieTypedIdent(name, map)));
            }
        }
    }
}
