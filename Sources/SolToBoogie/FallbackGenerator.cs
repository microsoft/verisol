// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace SolToBoogie
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Numerics;
    using System.Security.Cryptography.X509Certificates;
    using BoogieAST;
    using SolidityAST;

    public class FallbackGenerator
    {
        private TranslatorContext context;

        public FallbackGenerator(TranslatorContext _context)
        {
            context = _context;
        }

        public void Generate()
        {
            List<BoogieVariable> inParams = new List<BoogieVariable>()
            {
                new BoogieFormalParam(new BoogieTypedIdent("from", BoogieType.Ref)),
                new BoogieFormalParam(new BoogieTypedIdent("to", BoogieType.Ref)),
                new BoogieFormalParam(new BoogieTypedIdent("amount", BoogieType.Int))
            };
            List<BoogieVariable> outParams = new List<BoogieVariable>();

            List<BoogieAttribute> attributes = new List<BoogieAttribute>();
            if (context.TranslateFlags.GenerateInlineAttributes)
            {
                attributes.Add(new BoogieAttribute("inline", 1));
            };
            var procName = "FallbackDispatch";
            BoogieProcedure procedure = new BoogieProcedure(procName, inParams, outParams, attributes);
            context.Program.AddDeclaration(procedure);

            List<BoogieVariable> localVars = new List<BoogieVariable>();
            var fbUnknownProcName = "Fallback_UnknownType";
            BoogieStmtList procBody = GenerateBodyOfFallbackDispatch(inParams, fbUnknownProcName);

            BoogieImplementation implementation = new BoogieImplementation(procName, inParams, outParams, localVars, procBody);
            context.Program.AddDeclaration(implementation);

            // let us havoc all the global variables when fallback_unknowntype is called
            var modSet = context.Program.Declarations.Where(x => x is BoogieGlobalVariable).Select(x => (BoogieGlobalVariable)x).ToList();
            BoogieProcedure unknownFbProc = new BoogieProcedure(fbUnknownProcName, inParams, outParams, attributes);
            context.Program.AddDeclaration(unknownFbProc);

            // we need to create an implementation as Corral seem to ignore modifies on declarations 
            // https://github.com/boogie-org/corral/issues/98
            BoogieStmtList fbBody = new BoogieStmtList();
            var fbLocalVars = new List<BoogieVariable>();

            if (context.TranslateFlags.ModelStubsAsSkips() || context.TranslateFlags.ModelStubsAsCallbacks() || context.TranslateFlags.ModelStubsAsMultipleCallbacks())
            {
                fbBody.AppendStmtList(CreateBodyOfUnknownFallback(fbLocalVars, inParams));
            }
            else 
            {
                Debug.Assert(context.TranslateFlags.ModelStubsAsHavocs(), "Unknown option for modeling stubs");
                foreach (var global in modSet)
                {
                    fbBody.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr(global.Name)));
                }
            } 

            BoogieImplementation unknownFbImpl = new BoogieImplementation(fbUnknownProcName, inParams, outParams, fbLocalVars, fbBody);
            context.Program.AddDeclaration(unknownFbImpl);

            var sendProcName = "send";
            
            var sendOutParams = new List<BoogieVariable>(){ new BoogieFormalParam(new BoogieTypedIdent("success", BoogieType.Bool))};
            BoogieProcedure sendProc = new BoogieProcedure(sendProcName, inParams, sendOutParams, attributes);

            context.Program.AddDeclaration(sendProc);

            BoogieStmtList sendBody = CreateBodyOfSend(inParams, sendOutParams, procName);
            context.Program.AddDeclaration(new BoogieImplementation(sendProcName, inParams, sendOutParams, localVars, sendBody));
        }

        private BoogieStmtList CreateBodyOfSend(List<BoogieVariable> inParams, List<BoogieVariable> outParams, string fbProcName)
        {
            var fromIdExp = new BoogieIdentifierExpr(inParams[0].Name);
            var amtIdExp = new BoogieIdentifierExpr(inParams[2].Name);
            var guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE,
                new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), fromIdExp),
                amtIdExp);

            // call FallbackDispatch(from, to, amount)
            var toIdExpr = new BoogieIdentifierExpr(inParams[1].Name);
            var callStmt = new BoogieCallCmd(
                fbProcName,
                new List<BoogieExpr>() { fromIdExp, toIdExpr, amtIdExp},
                new List<BoogieIdentifierExpr>()
            );

            var thenBody = new BoogieStmtList();
            var successIdExp = new BoogieIdentifierExpr(outParams[0].Name);
            
            thenBody.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
            var balFrom = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr(inParams[0].Name));
            var balTo = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr(inParams[1].Name));
            var msgVal = new BoogieIdentifierExpr(inParams[2].Name);
            //balance[msg.sender] = balance[msg.sender] - msg.value
            thenBody.AddStatement(new BoogieAssignCmd(balFrom, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, balFrom, msgVal)));
            //balance[this] = balance[this] + msg.value
            thenBody.AddStatement(new BoogieAssignCmd(balTo, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, balTo, msgVal)));
            thenBody.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
            
            thenBody.AddStatement(callStmt); 
            thenBody.AddStatement(new BoogieAssignCmd(successIdExp, new BoogieLiteralExpr(true)));

            var elseBody = new BoogieAssignCmd(successIdExp, new BoogieLiteralExpr(false));

            return BoogieStmtList.MakeSingletonStmtList(new BoogieIfCmd(guard, thenBody,
                BoogieStmtList.MakeSingletonStmtList(elseBody)));
        }

        private BoogieStmtList HavocLocals(List<BoogieVariable> locals)
        {
            BoogieStmtList stmtList = new BoogieStmtList();
            foreach (BoogieVariable localVar in locals)
            {
                string varName = localVar.TypedIdent.Name;
                if (!varName.Equals("this"))
                {
                    stmtList.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr(varName)));
                }
            }

            return stmtList;
        }
        private BoogieStmtList CreateBodyOfUnknownFallback(List<BoogieVariable> fbLocalVars, List<BoogieVariable> inParams)
        {

            Debug.Assert(context.TranslateFlags.ModelStubsAsSkips() || context.TranslateFlags.ModelStubsAsCallbacks() || context.TranslateFlags.ModelStubsAsMultipleCallbacks(),
                "CreateBodyOfUnknownFallback called in unexpected context");
            var procBody = new BoogieStmtList();

            /*procBody.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
            var balnSender = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr(inParams[0].Name));
            var balnThis = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr(inParams[1].Name));
            var msgVal = new BoogieIdentifierExpr("amount");
            //assume Balance[msg.sender] >= msg.value
            procBody.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, balnSender, msgVal)));
            //balance[from] = balance[from] - msg.value
            procBody.AddStatement(new BoogieAssignCmd(balnSender, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, balnSender, msgVal)));
            //balance[to] = balance[to] + msg.value
            procBody.AddStatement(new BoogieAssignCmd(balnThis, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, balnThis, msgVal)));
            procBody.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));*/

            BoogieStmtList body = procBody;
            if (context.TranslateFlags.ModelStubsAsCallbacks() ||
                context.TranslateFlags.ModelStubsAsMultipleCallbacks())
            {
                if (context.TranslateFlags.ModelReverts)
                {
                    BoogieBinaryOperation revertGuard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, new BoogieIdentifierExpr("choice"), new BoogieLiteralExpr(BigInteger.Zero));
                    BoogieStmtList thenBody = new BoogieStmtList();
                    thenBody.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr("revert"), new BoogieLiteralExpr(true)));
                    thenBody.AddStatement(new BoogieReturnCmd());
                    BoogieIfCmd earlyExitCmd = new BoogieIfCmd(revertGuard, thenBody, null);
                    body.AddStatement(earlyExitCmd);
                }
                if (context.TranslateFlags.InstrumentGas)
                {
                    BoogieBinaryOperation gasGuard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.LT, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(TranslatorContext.TX_GAS_COST));
                    BoogieIfCmd earlyExitCmd = new BoogieIfCmd(gasGuard, BoogieStmtList.MakeSingletonStmtList(new BoogieReturnCmd()), null);
                    body.AddStatement(earlyExitCmd);
                }
                
            }
            
            if (context.TranslateFlags.ModelStubsAsMultipleCallbacks())
            { 
                fbLocalVars.Add(new BoogieLocalVariable(new BoogieTypedIdent("iterate", BoogieType.Bool)));
                BoogieBinaryOperation gasGuard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(TranslatorContext.TX_GAS_COST));
                BoogieBinaryOperation guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, new BoogieIdentifierExpr("iterate"), gasGuard);
                BoogieStmtList loopBody = new BoogieStmtList();
                procBody.AddStatement(new BoogieWhileCmd(guard, loopBody, new List<BoogieExpr>()));
                body = loopBody;
            }

            if (context.TranslateFlags.ModelStubsAsCallbacks() || context.TranslateFlags.ModelStubsAsMultipleCallbacks())
            {
                List<BoogieVariable> localVars = TransUtils.CollectLocalVars(context.ContractDefinitions.ToList(), context);
                fbLocalVars.AddRange(localVars);
                // if (*) fn1(from, *, ...) 
                // we only redirect the calling contract, but the msg.sender need not be to, as it can call into anohter contract that calls 
                // into from 

                if (context.TranslateFlags.ModelStubsAsMultipleCallbacks())
                {
                    body.AppendStmtList(HavocLocals(localVars));
                    body.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr("iterate")));
                }


                BoogieIfCmd typeIf = null;
                foreach (ContractDefinition curDef in context.ContractDefinitions.ToList())
                {
                    BoogieIfCmd reentrantCalls = TransUtils.GenerateChoiceBlock(new List<ContractDefinition>() {curDef},
                        context, false, Tuple.Create(inParams[0].Name, inParams[1].Name));
                    
                    if (reentrantCalls == null)
                    {
                        continue;
                    }
                    
                    BoogieExpr dtype = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), new BoogieIdentifierExpr(inParams[0].Name));
                    BoogieBinaryOperation guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, dtype, new BoogieIdentifierExpr(curDef.Name));
                    typeIf = new BoogieIfCmd(guard, BoogieStmtList.MakeSingletonStmtList(reentrantCalls), typeIf == null ? null : BoogieStmtList.MakeSingletonStmtList(typeIf));
                }
                
                /*BoogieIfCmd ifCmd = null;

                if (context.TranslateFlags.ModelReverts)
                {
                    BoogieExpr guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ,
                                        new BoogieIdentifierExpr("choice"),
                                        new BoogieLiteralExpr(choices.Item2 + 1));
                                    
                    BoogieAssignCmd assign = new BoogieAssignCmd(new BoogieIdentifierExpr("revert"), new BoogieLiteralExpr(true));
                    ifCmd = new BoogieIfCmd(guard, BoogieStmtList.MakeSingletonStmtList(assign), BoogieStmtList.MakeSingletonStmtList(choices.Item1));
                }
                else
                {
                    ifCmd = choices.Item1;
                }*/

                body.AddStatement(typeIf);
            }
            
            
            
            return procBody;
        }

        private BoogieStmtList GenerateBodyOfFallbackDispatch(List<BoogieVariable> inParams, string fbUnknownProcName)
        {
            // Perform the payable logic to transfer balance
            //
            // Fallback(from, to, amount)
            // 
            // foreach contract C that is not Lib/VeriSol
            //    if (DT[this] == C)
            //       if C has a fallback f
            //            call ret := fallBack_C(this=to, sender=from, msg.value)
            //       else 
            //            assume msg.value == 0; 
            // else
            //    if stubModel == callback
            //     call fallBack_unknownType(from, to, amout) //we switch the order of first two parameters to ease callbacks
            //    else if stubmodel == havoc
            //      havoc all global state, except local state of this contract
            //    else 
            //      skip //default
            BoogieIfCmd ifCmd = null;

            Debug.Assert(context.ContractDefinitions.Count >= 1, "There should be at least one contract");

            List<BoogieIdentifierExpr> outParams = new List<BoogieIdentifierExpr>();

            List<BoogieExpr> unkwnFnArgs = new List<BoogieExpr>()
                {
                    new BoogieIdentifierExpr(inParams[0].Name),
                    new BoogieIdentifierExpr(inParams[1].Name),
                    new BoogieIdentifierExpr(inParams[2].Name)
                };

            // fbUnknown(from, to, amount) 
            BoogieStmtList noMatchCase = BoogieStmtList.MakeSingletonStmtList(
                new BoogieCallCmd(fbUnknownProcName, unkwnFnArgs, outParams));

            foreach (var contract in context.ContractDefinitions)
            {
                if (contract.ContractKind == EnumContractKind.LIBRARY) continue;

                FunctionDefinition function = context.ContractToFallbackMap.ContainsKey(contract) ?
                    context.ContractToFallbackMap[contract] : null;
                BoogieExpr lhs = new BoogieMapSelect(new BoogieIdentifierExpr("DType"), new BoogieIdentifierExpr(inParams[1].Name));
                BoogieExpr rhs = new BoogieIdentifierExpr(contract.Name);
                BoogieExpr guard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.EQ, lhs, rhs);


                BoogieStmtList thenBody = null;

                if (function != null)
                {
                    if (context.TranslateFlags.PerformFunctionSlice &&
                        !context.TranslateFlags.SliceFunctions.Contains(function))
                    {
                        continue;
                    }
                    
                    List<BoogieExpr> arguments = new List<BoogieExpr>()
                    {
                        new BoogieIdentifierExpr(inParams[1].Name),
                        new BoogieIdentifierExpr(inParams[0].Name),
                        new BoogieIdentifierExpr(inParams[2].Name)
                    };

                    string callee = TransUtils.GetCanonicalFunctionName(function, context);
                    // to.fallback(from, amount), thus need to switch param0 and param1
                    thenBody = BoogieStmtList.MakeSingletonStmtList(new BoogieCallCmd(callee, arguments, outParams));
                }
                else
                {
                    // No fallback means not payable (amount == 0)
                    thenBody = BoogieStmtList.MakeSingletonStmtList(
                        new BoogieAssumeCmd(
                            new BoogieBinaryOperation(
                                BoogieBinaryOperation.Opcode.EQ,
                                new BoogieIdentifierExpr(inParams[2].Name),
                                new BoogieLiteralExpr(BigInteger.Zero)))
                        );
                }

                BoogieStmtList elseBody = ifCmd == null ? noMatchCase : BoogieStmtList.MakeSingletonStmtList(ifCmd);

                ifCmd = new BoogieIfCmd(guard, thenBody, elseBody);
            }

            return BoogieStmtList.MakeSingletonStmtList(ifCmd);
        }
    }
}
