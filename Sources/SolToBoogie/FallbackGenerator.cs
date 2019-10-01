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

            List<BoogieAttribute> attributes = new List<BoogieAttribute>()
            {
                new BoogieAttribute("inline", 1),
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

            if (context.TranslateFlags.ModelStubsAsSkips)
            {
                // a stub is just a skip
            }
            else if (context.TranslateFlags.ModelStubsAsCallbacks)
            {
                fbBody.AppendStmtList(CreateBodyOfUnknownFallback(fbLocalVars, inParams));
            }
            else
            {
                // default
                foreach (var global in modSet)
                {
                    fbBody.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr(global.Name)));
                }
            }

            BoogieImplementation unknownFbImpl = new BoogieImplementation(fbUnknownProcName, inParams, outParams, fbLocalVars, fbBody);
            context.Program.AddDeclaration(unknownFbImpl);

        }

        private BoogieStmtList CreateBodyOfUnknownFallback(List<BoogieVariable> fbLocalVars, List<BoogieVariable> inParams)
        {
            var fbBody = new BoogieStmtList();
            fbLocalVars.AddRange(TransUtils.CollectLocalVars(context.ContractDefinitions.ToList(), context));
            fbBody.AddStatement(TransUtils.GenerateChoiceBlock(context.ContractDefinitions.ToList(), context, Tuple.Create(inParams[1].Name, inParams[0].Name)));
            return fbBody;
        }

        private BoogieStmtList GenerateBodyOfFallbackDispatch(List<BoogieVariable> inParams, string fbUnknownProcName)
        {
            //
            // foreach contract C that is not Lib/VeriSol
            //    if (DT[this] == C)
            //       if C has a fallback f
            //            call ret := fallBack_C(this=to, sender=from, msg.value)
            //       else 
            //            assume msg.value == 0; 
            // else
            //     call fallBack_unknownType(to, from, msg.value) //we switch the order of first two parameters to ease callbacks
            BoogieIfCmd ifCmd = null;

            Debug.Assert(context.ContractDefinitions.Count >= 1, "There should be at least one contract");

            List<BoogieExpr> arguments = new List<BoogieExpr>()
                {
                    new BoogieIdentifierExpr(inParams[1].Name),
                    new BoogieIdentifierExpr(inParams[0].Name),
                    new BoogieIdentifierExpr(inParams[2].Name)
                };
            List<BoogieIdentifierExpr> outParams = new List<BoogieIdentifierExpr>();

            BoogieStmtList noMatchCase = BoogieStmtList.MakeSingletonStmtList(
                new BoogieCallCmd(fbUnknownProcName, arguments, outParams));

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
                    string callee = TransUtils.GetCanonicalFunctionName(function, context);
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
