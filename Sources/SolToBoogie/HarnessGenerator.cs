// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
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

        public HarnessGenerator(TranslatorContext context)
        {
            this.context = context;
        }

        public void Generate()
        {
            foreach (ContractDefinition contract in context.ContractDefinitions)
            {
                Dictionary<int, BoogieExpr> houdiniVarMap = HoudiniHelper.GenerateHoudiniVarMapping(contract, context);
                GenerateHoudiniVarsForContract(contract, houdiniVarMap);
                GenerateBoogieHarnessForContract(contract, houdiniVarMap);
            }

            GenerateModifiers();

            foreach (ContractDefinition contract in context.ContractDefinitions)
            {
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

            List<BoogieVariable> localVars = CollectLocalVars(contract);
            BoogieStmtList harnessBody = new BoogieStmtList();
            harnessBody.AddStatement(GenerateDynamicTypeAssumes(contract));
            GenerateConstructorCall(contract).ForEach(x => harnessBody.AddStatement(x));
            harnessBody.AddStatement(GenerateWhileLoop(contract, houdiniVarMap, localVars));
            BoogieImplementation harnessImpl = new BoogieImplementation(harnessName, inParams, outParams, localVars, harnessBody);
            context.Program.AddDeclaration(harnessImpl);
        }

        private List<BoogieVariable> CollectLocalVars(ContractDefinition contract)
        {
            List<BoogieVariable> localVars = new List<BoogieVariable>()
            {
                new BoogieLocalVariable(new BoogieTypedIdent("this", BoogieType.Ref)),
                new BoogieLocalVariable(new BoogieTypedIdent("msgsender_MSG", BoogieType.Ref)),
                new BoogieLocalVariable(new BoogieTypedIdent("msgvalue_MSG", BoogieType.Int)),
                new BoogieLocalVariable(new BoogieTypedIdent("choice", BoogieType.Int)),
            };

            // use to remove duplicated variables by name
            HashSet<string> uniqueVarNames = new HashSet<string>() { "this", "msgsender_MSG", "msgvalue_MSG", "choice" };

            // Consider all visible functions
            HashSet<FunctionDefinition> funcDefs = context.GetVisibleFunctionsByContract(contract);
            foreach (FunctionDefinition funcDef in funcDefs)
            {
                if (funcDef.Visibility == EnumVisibility.PUBLIC || funcDef.Visibility == EnumVisibility.EXTERNAL)
                {
                    foreach (VariableDeclaration param in funcDef.Parameters.Parameters)
                    {
                        string name = TransUtils.GetCanonicalLocalVariableName(param);
                        if (!uniqueVarNames.Contains(name))
                        {
                            BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(param.TypeName);
                            BoogieVariable localVar = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                            localVars.Add(localVar);
                            uniqueVarNames.Add(name);
                        }
                    }

                    var retParamCount = 0;
                    foreach (VariableDeclaration param in funcDef.ReturnParameters.Parameters)
                    {
                        //string name = "__ret" + funcDef.Name;
                        string name = $"__ret_{retParamCount++}_" + funcDef.Name;
                        if (!string.IsNullOrEmpty(param.Name))
                        {
                            name = TransUtils.GetCanonicalLocalVariableName(param);
                        }
                        if (!uniqueVarNames.Contains(name))
                        {
                            BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(param.TypeName);
                            BoogieVariable localVar = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                            localVars.Add(localVar);
                            uniqueVarNames.Add(name);
                        }
                    }
                }
            }
            return localVars;
        }

        private BoogieAssumeCmd GenerateDynamicTypeAssumes(ContractDefinition contract)
        {
            BoogieExpr assumeLhs = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), new BoogieIdentifierExpr("this"));

            List<ContractDefinition> subtypes = new List<ContractDefinition>(context.GetSubTypesOfContract(contract));
            Debug.Assert(subtypes.Count > 0);

            BoogieExpr assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, assumeLhs,
                new BoogieIdentifierExpr(subtypes[0].Name));
            for (int i = 1; i < subtypes.Count; ++i)
            {
                BoogieExpr rhs = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, assumeLhs,
                    new BoogieIdentifierExpr(subtypes[i].Name));
                assumeExpr = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, assumeExpr, rhs);
            }

            return new BoogieAssumeCmd(assumeExpr);
        }

        private List<BoogieCallCmd> GenerateConstructorCall(ContractDefinition contract)
        {
            List<BoogieCallCmd> localStmtList = new List<BoogieCallCmd>();
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
                foreach (VariableDeclaration param in ctor.Parameters.Parameters)
                {
                    string name = TransUtils.GetCanonicalLocalVariableName(param);
                    inputs.Add(new BoogieIdentifierExpr(name));

                    if (param.TypeName is ArrayTypeName array)
                    {
                        localStmtList.Add(new BoogieCallCmd(
                            "FreshRefGenerator",
                            new List<BoogieExpr>(), new List<BoogieIdentifierExpr>() {new BoogieIdentifierExpr(name)}));
                    }
                }
            }
            localStmtList.Add(new BoogieCallCmd(callee, inputs, null));
            return localStmtList;
        }

        private BoogieWhileCmd GenerateWhileLoop(ContractDefinition contract, Dictionary<int, BoogieExpr> houdiniVarMap, List<BoogieVariable> localVars)
        {
            // havoc all local variables except `this'
            BoogieStmtList body = GenerateHavocBlock(contract, localVars);

            // generate the choice block
            body.AddStatement(GenerateChoiceBlock(contract));

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
            return stmtList;
        }

        // generate a non-deterministic choice block to call every public visible functions except constructors
        private BoogieIfCmd GenerateChoiceBlock(ContractDefinition contract)
        {
            HashSet<FunctionDefinition> funcDefs = context.GetVisibleFunctionsByContract(contract);
            List<FunctionDefinition> publicFuncDefs = new List<FunctionDefinition>();
            foreach (FunctionDefinition funcDef in funcDefs)
            {
                if (funcDef.IsConstructorForContract(contract.Name)) continue;
                if (funcDef.Visibility == EnumVisibility.PUBLIC || funcDef.Visibility == EnumVisibility.EXTERNAL)
                {
                    publicFuncDefs.Add(funcDef);
                }
            }
            BoogieIfCmd ifCmd = null;
            for (int i = publicFuncDefs.Count - 1; i >= 0; --i)
            {
                BoogieExpr guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ,
                    new BoogieIdentifierExpr("choice"),
                    new BoogieLiteralExpr(i + 1));

                BoogieStmtList thenBody = new BoogieStmtList();

                FunctionDefinition funcDef = publicFuncDefs[i];
                string callee = TransUtils.GetCanonicalFunctionName(funcDef, context);
                List<BoogieExpr> inputs = new List<BoogieExpr>()
                {
                    new BoogieIdentifierExpr("this"),
                    new BoogieIdentifierExpr("msgsender_MSG"),
                    new BoogieIdentifierExpr("msgvalue_MSG"),
                };
                foreach (VariableDeclaration param in funcDef.Parameters.Parameters)
                {
                    string name = TransUtils.GetCanonicalLocalVariableName(param);
                    inputs.Add(new BoogieIdentifierExpr(name));
                    if (param.TypeName is ArrayTypeName array)
                    {
                        thenBody.AddStatement(new BoogieCallCmd(
                            "FreshRefGenerator",
                            new List<BoogieExpr>(), new List<BoogieIdentifierExpr>() { new BoogieIdentifierExpr(name) }));
                    }

                }

                List<BoogieIdentifierExpr> outputs = new List<BoogieIdentifierExpr>();
                var retParamCount = 0;

                foreach (VariableDeclaration param in funcDef.ReturnParameters.Parameters)
                {
                    //string name = "__ret" + funcDef.Name;
                    string name = $"__ret_{retParamCount++}_" + funcDef.Name;

                    if (!string.IsNullOrEmpty(param.Name))
                    {
                        name = TransUtils.GetCanonicalLocalVariableName(param);
                    }
                    outputs.Add(new BoogieIdentifierExpr(name));
                }

                BoogieCallCmd callCmd = new BoogieCallCmd(callee, inputs, outputs);
                thenBody.AddStatement(callCmd);

                BoogieStmtList elseBody = ifCmd == null ? null : BoogieStmtList.MakeSingletonStmtList(ifCmd);
                ifCmd = new BoogieIfCmd(guard, thenBody, elseBody);
            }
            return ifCmd;
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
            BoogieStmtList procBody = GenerateHavocBlock(contract, localVars);
            procBody.AddStatement(GenerateChoiceBlock(contract));
            BoogieImplementation procImpl = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(procImpl);
        }

        private void GenerateCorralHarnessForContract(ContractDefinition contract)
        {
            string harnessName = "CorralEntry_" + contract.Name;
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
            harnessBody.AddStatement(GenerateDynamicTypeAssumes(contract));
            GenerateConstructorCall(contract).ForEach(x => harnessBody.AddStatement(x));
            harnessBody.AddStatement(GenerateCorralWhileLoop(contract));
            BoogieImplementation harnessImpl = new BoogieImplementation(harnessName, inParams, outParams, localVars, harnessBody);
            context.Program.AddDeclaration(harnessImpl);
        }

        private BoogieWhileCmd GenerateCorralWhileLoop(ContractDefinition contract)
        {
            BoogieStmtList body = new BoogieStmtList();
            string callee = "CorralChoice_" + contract.Name;
            List<BoogieExpr> inputs = new List<BoogieExpr>()
            {
                new BoogieIdentifierExpr("this"),
            };
            body.AddStatement(new BoogieCallCmd(callee, inputs, null));
            return new BoogieWhileCmd(new BoogieLiteralExpr(true), body);
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

            foreach (VariableDeclaration param in funcDef.Parameters.Parameters)
            {
                string name = TransUtils.GetCanonicalLocalVariableName(param);
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(param.TypeName);
                BoogieVariable localVar = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                parameters.Add(localVar);
            }

            var retParamCount = 0;
            foreach (VariableDeclaration param in funcDef.ReturnParameters.Parameters)
            {
                //string name = "__ret" + funcDef.Name;
                string name = $"__ret_{retParamCount++}_" + funcDef.Name;

                if (!string.IsNullOrEmpty(param.Name))
                {
                    name = TransUtils.GetCanonicalLocalVariableName(param);
                }
                BoogieType type = TransUtils.GetBoogieTypeFromSolidityTypeName(param.TypeName);
                BoogieVariable localVar = new BoogieLocalVariable(new BoogieTypedIdent(name, type));
                parameters.Add(localVar);
            }

            return parameters;
        }
    }
}
