// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Numerics;

namespace SolToBoogie
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BoogieAST;
    using SolidityAST;

    /**
     * Generate harness for each contract
     */
    public class HarnessGenerator
    {
        private TranslatorContext context;
        private Dictionary<string, List<BoogieExpr>> contractInvariants;

        public HarnessGenerator(TranslatorContext context, Dictionary<string, List<BoogieExpr>> _contractInvariants)
        {
            this.context = context;
            this.contractInvariants = _contractInvariants;
        }

        public void Generate()
        {
            if (!context.TranslateFlags.NoBoogieHarness)
            {
                foreach (ContractDefinition contract in context.ContractDefinitions)
                {
                    if (contract.ContractKind == EnumContractKind.LIBRARY &&
                        contract.Name.Equals("VeriSol"))
                    {
                        continue;
                    }

                    Dictionary<int, BoogieExpr> houdiniVarMap =
                        HoudiniHelper.GenerateHoudiniVarMapping(contract, context);
                    GenerateHoudiniVarsForContract(contract, houdiniVarMap);
                    GenerateBoogieHarnessForContract(contract, houdiniVarMap);
                }
            }

            GenerateModifiers();

            foreach (ContractDefinition contract in context.ContractDefinitions)
            {
                if (contract.ContractKind == EnumContractKind.LIBRARY &&
                    contract.Name.Equals("VeriSol"))
                {
                    continue;
                }

                GenerateCorralChoiceProcForContract(contract);
                GenerateCorralHarnessForContract(contract);
            }
        }

        private void GenerateHoudiniVarsForContract(ContractDefinition contract, Dictionary<int, BoogieExpr> houdiniVarMap)
        {
            foreach (int id in houdiniVarMap.Keys)
            {
                string varName = GetHoudiniVarName(id, contract);
                BoogieConstant houdiniVar = new BoogieConstant(new BoogieTypedIdent(varName, BoogieType.Bool));
                houdiniVar.Attributes = new List<BoogieAttribute>()
                {
                    new BoogieAttribute("existential", true)
                };
                context.Program.AddDeclaration(houdiniVar);
            }
        }

        private void GenerateModifiers()
        {
            foreach (string modifier in context.ModifierToBoogiePreProc.Keys)
            {
                if (context.ModifierToBoogiePreImpl.ContainsKey(modifier))
                {
                    context.Program.AddDeclaration(context.ModifierToBoogiePreProc[modifier]);
                    context.Program.AddDeclaration(context.ModifierToBoogiePreImpl[modifier]);
                }
            }

            foreach (string modifier in context.ModifierToBoogiePostProc.Keys)
            {
                if (context.ModifierToBoogiePostImpl.ContainsKey(modifier))
                {
                    context.Program.AddDeclaration(context.ModifierToBoogiePostProc[modifier]);
                    context.Program.AddDeclaration(context.ModifierToBoogiePostImpl[modifier]);
                }
            }
        }

        private void GenerateBoogieHarnessForContract(ContractDefinition contract, Dictionary<int, BoogieExpr> houdiniVarMap)
        {
            string harnessName = "BoogieEntry_" + contract.Name;
            List<BoogieVariable> inParams = new List<BoogieVariable>();
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            BoogieProcedure harness = new BoogieProcedure(harnessName, inParams, outParams);
            context.Program.AddDeclaration(harness);

            //List<BoogieVariable> localVars = TransUtils.CollectLocalVars(new List<ContractDefinition>() { contract }, context);
            List<BoogieVariable> localVars = CollectLocalVars(contract);
            localVars.Add(new BoogieLocalVariable(new BoogieTypedIdent("tmpNow", BoogieType.Int)));
            BoogieStmtList harnessBody = new BoogieStmtList();
            harnessBody.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("now"), new BoogieLiteralExpr(0))));
            harnessBody.AddStatement(GenerateDynamicTypeAssumes(contract));
            GenerateConstructorCall(contract).ForEach(x => harnessBody.AddStatement(x));
            if (context.TranslateFlags.ModelReverts)
            {
                BoogieExpr assumePred = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, new BoogieIdentifierExpr("revert"));
                if (context.TranslateFlags.InstrumentGas)
                {
                    assumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, assumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));    
                }
                
                harnessBody.AddStatement(new BoogieAssumeCmd(assumePred));
            }
            harnessBody.AddStatement(GenerateWhileLoop(contract, houdiniVarMap, localVars));
            BoogieImplementation harnessImpl = new BoogieImplementation(harnessName, inParams, outParams, localVars, harnessBody);
            context.Program.AddDeclaration(harnessImpl);
        }

        private BoogieAssumeCmd GenerateDynamicTypeAssumes(ContractDefinition contract)
        {
            BoogieExpr assumeLhs = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), new BoogieIdentifierExpr("this"));

            List<ContractDefinition> subtypes = new List<ContractDefinition>(context.GetSubTypesOfContract(contract));
            Debug.Assert(subtypes.Count > 0);

            BoogieExpr assumeExpr0 = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, assumeLhs,
                new BoogieIdentifierExpr(subtypes[0].Name));
            var assumeExpr = assumeExpr0;

            for (int i = 1; i < subtypes.Count; ++i)
            {
                BoogieExpr rhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, assumeLhs,
                    new BoogieIdentifierExpr(subtypes[i].Name));
                assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, assumeExpr, rhs);
            }

            if (context.TranslateFlags.OmitAssumeFalseForDynDispatch)
            {
                return new BoogieAssumeCmd(new BoogieLiteralExpr(true)); //assumeExpr0);
            }
            return new BoogieAssumeCmd(assumeExpr);
        }

      
        private List<BoogieCmd> GenerateConstructorCall(ContractDefinition contract)
        {
            List<BoogieCmd> localStmtList = new List<BoogieCmd>();
            string callee = TransUtils.GetCanonicalConstructorName(contract);
            List<BoogieExpr> inputs = new List<BoogieExpr>()
            {
                new BoogieIdentifierExpr("this"),
                new BoogieIdentifierExpr("msgsender_MSG"),
                new BoogieIdentifierExpr("msgvalue_MSG"),
            };
            if (context.IsConstructorDefined(contract))
            {
                FunctionDefinition ctor = context.GetConstructorByContract(contract);
                if (ctor.StateMutability.Equals(EnumStateMutability.PAYABLE))
                {
                    BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE,
                        new BoogieIdentifierExpr("msgvalue_MSG"), new BoogieLiteralExpr(BigInteger.Zero));
                    localStmtList.Add(new BoogieAssumeCmd(assumeExpr));
                    
                    localStmtList.Add(new BoogieCommentCmd("---- Logic for payable function START "));
                    var balnSender = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr("msgsender_MSG"));
                    var balnThis = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr("this"));
                    var msgVal = new BoogieIdentifierExpr("msgvalue_MSG");
                    //assume Balance[msg.sender] >= msg.value
                    localStmtList.Add(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, balnSender, msgVal)));
                    //balance[msg.sender] = balance[msg.sender] - msg.value
                    localStmtList.Add(new BoogieAssignCmd(balnSender, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, balnSender, msgVal)));
                    //balance[this] = balance[this] + msg.value
                    localStmtList.Add(new BoogieAssignCmd(balnThis, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, balnThis, msgVal)));
                    localStmtList.Add(new BoogieCommentCmd("---- Logic for payable function END "));
                }
                else
                {
                    BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ,
                        new BoogieIdentifierExpr("msgvalue_MSG"), new BoogieLiteralExpr(BigInteger.Zero));
                    localStmtList.Add(new BoogieAssumeCmd(assumeExpr));
                }
                
                foreach (VariableDeclaration param in ctor.Parameters.Parameters)
                {
                    string name = TransUtils.GetCanonicalLocalVariableName(param, context);
                    inputs.Add(new BoogieIdentifierExpr(name));

                    if (param.TypeName is ArrayTypeName array)
                    {
                        localStmtList.Add(new BoogieCallCmd(
                            "FreshRefGenerator",
                            new List<BoogieExpr>(), new List<BoogieIdentifierExpr>() {new BoogieIdentifierExpr(name)}));
                    }
                }
            }
            else
            {
                BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ,
                    new BoogieIdentifierExpr("msgvalue_MSG"), new BoogieLiteralExpr(BigInteger.Zero));
                localStmtList.Add(new BoogieAssumeCmd(assumeExpr));
            }

            if (context.TranslateFlags.InstrumentGas)
            {
                var gasVar = new BoogieIdentifierExpr("gas");
                localStmtList.Add(new BoogieAssignCmd(gasVar, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, gasVar, new BoogieLiteralExpr(TranslatorContext.CREATE_GAS_COST))));
            }

            localStmtList.Add(new BoogieCallCmd(callee, inputs, null));
            return localStmtList;
        }

        private BoogieWhileCmd GenerateWhileLoop(ContractDefinition contract, Dictionary<int, BoogieExpr> houdiniVarMap, List<BoogieVariable> localVars)
        {
            // havoc all local variables except `this'
            BoogieStmtList body = GenerateHavocBlock(contract, localVars);

            // generate the choice block
            //body.AddStatement(TransUtils.GenerateChoiceBlock(new List<ContractDefinition>() { contract }, context));
            body.AddStatement(GenerateChoices(contract));

            // generate candidate invariants for Houdini
            List<BoogiePredicateCmd> candidateInvs = new List<BoogiePredicateCmd>();
            foreach (int id in houdiniVarMap.Keys)
            {
                BoogieIdentifierExpr houdiniVar = new BoogieIdentifierExpr(GetHoudiniVarName(id, contract));
                BoogieExpr candidateInv = houdiniVarMap[id];
                BoogieExpr invExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.IMP, houdiniVar, candidateInv);
                BoogieLoopInvCmd invCmd = new BoogieLoopInvCmd(invExpr);
                candidateInvs.Add(invCmd);
            }

            // add the contract invariant if present
            if (contractInvariants.ContainsKey(contract.Name))
            {
                contractInvariants[contract.Name].ForEach(x => candidateInvs.Add(new BoogieLoopInvCmd(x)));
            }

            return new BoogieWhileCmd(new BoogieLiteralExpr(true), body, candidateInvs);
        }

        private BoogieStmtList GenerateHavocBlock(ContractDefinition contract, List<BoogieVariable> localVars)
        {
            BoogieStmtList stmtList = new BoogieStmtList();
            foreach (BoogieVariable localVar in localVars)
            {
                string varName = localVar.TypedIdent.Name;
                if (!varName.Equals("this"))
                {
                    stmtList.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr(varName)));
                }
            }
            
            if (context.TranslateFlags.InstrumentGas)
            {
                TransUtils.havocGas(stmtList);
            }

            var nowVar = new BoogieIdentifierExpr("now");
            var tmpNowVar = new BoogieIdentifierExpr("tmpNow");
            stmtList.AddStatement(new BoogieAssignCmd(tmpNowVar, nowVar));
            stmtList.AddStatement(new BoogieHavocCmd(nowVar));
            stmtList.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GT, nowVar, tmpNowVar)));
            stmtList.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.NEQ, new BoogieIdentifierExpr("msgsender_MSG"), new BoogieIdentifierExpr("null"))));

            if (context.TranslateFlags.NoTxnsFromContract)
            {
                foreach (var contractDef in context.ContractDefinitions)
                {
                    BoogieIdentifierExpr contractIdent = new BoogieIdentifierExpr(contractDef.Name);
                    stmtList.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(
                        BoogieBinaryOperation.Opcode.NEQ,
                        new BoogieMapSelect(new BoogieIdentifierExpr("DType"),
                            new BoogieIdentifierExpr("msgsender_MSG")),
                        contractIdent)));
                }
                
                stmtList.AddStatement((new BoogieAssignCmd(new BoogieMapSelect(new BoogieIdentifierExpr("Alloc"), new BoogieIdentifierExpr("msgsender_MSG")), new BoogieLiteralExpr(true))));
            }

            return stmtList;
        }

        
        private string GetHoudiniVarName(int id, ContractDefinition contract)
        {
            return "HoudiniB" + id.ToString() + "_" + contract.Name;
        }

        private void GenerateCorralChoiceProcForContract(ContractDefinition contract)
        {
            string procName = "CorralChoice_" + contract.Name;
            List<BoogieVariable> inParams = new List<BoogieVariable>()
            {
                new BoogieFormalParam(new BoogieTypedIdent("this", BoogieType.Ref)),
            };
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            BoogieProcedure harness = new BoogieProcedure(procName, inParams, outParams);
            context.Program.AddDeclaration(harness);

            List<BoogieVariable> localVars = RemoveThisFromVariables(CollectLocalVars(contract));
            localVars.Add(new BoogieLocalVariable(new BoogieTypedIdent("tmpNow", BoogieType.Int)));
            BoogieStmtList procBody = GenerateHavocBlock(contract, localVars);
            //procBody.AddStatement(TransUtils.GenerateChoiceBlock(new List<ContractDefinition>() { contract }, context));
            procBody.AddStatement(GenerateChoices(contract));
            BoogieImplementation procImpl = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(procImpl);
        }

        private List<BoogieVariable> CollectLocalVars(ContractDefinition contract)
        {
            List<ContractDefinition> contracts = new List<ContractDefinition>() {contract};

            if (context.TranslateFlags.TxnsOnFields)
            {
                HashSet<VariableDeclaration> contractFields = context.GetStateVarsByContract(contract);
                foreach (VariableDeclaration contractField in contractFields)
                {
                    if (contractField.TypeDescriptions.IsContract() && contractField.TypeName is UserDefinedTypeName)
                    {
                        String fieldContractName = contractField.TypeName.ToString();
                        ContractDefinition fieldDef = context.GetContractByName(fieldContractName);
                        contracts.Add(fieldDef);
                    }
                }
            }

            return TransUtils.CollectLocalVars(contracts, context);
        }

        private BoogieIfCmd GenerateChoices(ContractDefinition contract)
        {
            BoogieExpr thisVal = new BoogieIdentifierExpr("this");
            Tuple<BoogieIfCmd, int> curChoices = TransUtils.GeneratePartialChoiceBlock(new List<ContractDefinition>() {contract}, context, thisVal, 0, context.TranslateFlags.ModelReverts);
            if (context.TranslateFlags.TxnsOnFields)
            {
                HashSet<VariableDeclaration> contractFields = context.GetStateVarsByContract(contract);

                foreach (VariableDeclaration contractField in contractFields)
                {
                    if (contractField.TypeDescriptions.IsContract() && contractField.TypeName is UserDefinedTypeName)
                    {
                        BoogieExpr fieldInstance = new BoogieMapSelect(new BoogieIdentifierExpr(TransUtils.GetCanonicalVariableName(contractField, context)), thisVal);
                        String fieldContractName = contractField.TypeName.ToString();
                        ContractDefinition fieldDef = context.GetContractByName(fieldContractName);
                        curChoices = TransUtils.GeneratePartialChoiceBlock(new List<ContractDefinition>() {fieldDef},
                            context, fieldInstance, curChoices.Item2, context.TranslateFlags.ModelReverts, curChoices.Item1);
                    }
                }
            }

            return curChoices.Item1;
        }

        private void GenerateCorralHarnessForContract(ContractDefinition contract)
        {
            string harnessName = "CorralEntry_" + contract.Name;
            if (context.TranslateFlags.CreateMainHarness && contract.Name.Equals(context.EntryPointContract))
            {
                harnessName = "main";
            }

            List<BoogieVariable> inParams = new List<BoogieVariable>();
            List<BoogieVariable> outParams = new List<BoogieVariable>();
            BoogieProcedure harness = new BoogieProcedure(harnessName, inParams, outParams);
            context.Program.AddDeclaration(harness);

            List<BoogieVariable> localVars = new List<BoogieVariable>
            {
                new BoogieLocalVariable(new BoogieTypedIdent("this", BoogieType.Ref)),
                new BoogieLocalVariable(new BoogieTypedIdent("msgsender_MSG", BoogieType.Ref)),
                new BoogieLocalVariable(new BoogieTypedIdent("msgvalue_MSG", BoogieType.Int)),
            };
            if (context.IsConstructorDefined(contract))
            {
                FunctionDefinition ctor = context.GetConstructorByContract(contract);
                localVars.AddRange(GetParamsOfFunction(ctor));
            }
            BoogieStmtList harnessBody = new BoogieStmtList();
            //harnessBody.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr("this"), ));
            if (context.TranslateFlags.NoCustomTypes)
            {
                harnessBody.AddStatement((new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, new BoogieIdentifierExpr("null"), new BoogieLiteralExpr(0)))));
            }
            harnessBody.AddStatement(new BoogieCallCmd("FreshRefGenerator", new List<BoogieExpr>(), new List<BoogieIdentifierExpr>() {new BoogieIdentifierExpr("this")}));
            harnessBody.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("now"), new BoogieLiteralExpr(0))));
            harnessBody.AddStatement(GenerateDynamicTypeAssumes(contract));
            if (!context.TranslateFlags.PrePostHarness)
            {
                GenerateConstructorCall(contract).ForEach(x => harnessBody.AddStatement(x));
            }
            
            if (context.TranslateFlags.ModelReverts)
            {
                BoogieExpr assumePred = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, new BoogieIdentifierExpr("revert"));
                if (context.TranslateFlags.InstrumentGas)
                {
                    assumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, assumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));    
                }
                
                harnessBody.AddStatement(new BoogieAssumeCmd(assumePred));
            }

            if (context.TranslateFlags.PrePostHarness)
            {
                harnessBody.AddStatement(GenerateCorralCall(contract));
                BoogieStmtList body = new BoogieStmtList();
                harnessBody.AddStatement(new BoogieWhileCmd(new BoogieLiteralExpr(true), body));
            }
            else
            {
                harnessBody.AddStatement(GenerateCorralWhileLoop(contract));
            }
            
            BoogieImplementation harnessImpl = new BoogieImplementation(harnessName, inParams, outParams, localVars, harnessBody);
            context.Program.AddDeclaration(harnessImpl);
        }

        private BoogieCallCmd GenerateCorralCall(ContractDefinition contract)
        {
            string callee = "CorralChoice_" + contract.Name;
            List<BoogieExpr> inputs = new List<BoogieExpr>()
            {
                new BoogieIdentifierExpr("this"),
            };
            
            return new BoogieCallCmd(callee, inputs, null);
        }
        private BoogieWhileCmd GenerateCorralWhileLoop(ContractDefinition contract)
        {
            BoogieStmtList body = new BoogieStmtList();
            body.AddStatement(GenerateCorralCall(contract));

            List<BoogiePredicateCmd> candidateInvs = new List<BoogiePredicateCmd>();
            // add the contract invariant if present
            if (contractInvariants.ContainsKey(contract.Name))
            {
                contractInvariants[contract.Name].ForEach(x => candidateInvs.Add(new BoogieLoopInvCmd(x)));
            }

            return new BoogieWhileCmd(new BoogieLiteralExpr(true), body, candidateInvs);
        }

        private List<BoogieVariable> RemoveThisFromVariables(List<BoogieVariable> variables)
        {
            List<BoogieVariable> ret = new List<BoogieVariable>();
            foreach (BoogieVariable variable in variables)
            {
                if (!variable.TypedIdent.Name.Equals("this"))
                {
                    ret.Add(variable);
                }
            }
            return ret;
        }

        private List<BoogieVariable> GetParamsOfFunction(FunctionDefinition funcDef)
        {
            List<BoogieVariable> parameters = new List<BoogieVariable>();

            var inpParamCount = 0;
            foreach (VariableDeclaration param in funcDef.Parameters.Parameters)
            {
                string name = $"__arg1_{inpParamCount++}_" + funcDef.Name;
                if (!string.IsNullOrEmpty(param.Name))
                {
                    name = TransUtils.GetCanonicalLocalVariableName(param, context);
                }
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(param.TypeName);
                BoogieVariable localVar = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                parameters.Add(localVar);
            }

            var retParamCount = 0;
            foreach (VariableDeclaration param in funcDef.ReturnParameters.Parameters)
            {
                string name = $"__ret1_{retParamCount++}_" + funcDef.Name;

                if (!string.IsNullOrEmpty(param.Name))
                {
                    name = TransUtils.GetCanonicalLocalVariableName(param, context);
                }
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(param.TypeName);
                BoogieVariable localVar = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                parameters.Add(localVar);
            }

            return parameters;
        }
    }
}
