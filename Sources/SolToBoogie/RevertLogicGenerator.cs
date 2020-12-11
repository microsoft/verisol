using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SolToBoogie
{
    using BoogieAST;
    
    public class RevertLogicGenerator
    {
        private TranslatorContext context;

        private HashSet<string> constructorNames;

        private Dictionary<string, BoogieGlobalVariable> shadowGlobals = new Dictionary<string, BoogieGlobalVariable>();

        private Dictionary<string, BoogieImplementation>.KeyCollection procsWiltImplNames;

        private Dictionary<string, BoogieProcedure> proceduresInProgram;

        private bool mustHaveShadow(string globalName)
        {
            return !globalName.Equals("revert") && !globalName.Equals("gas");
        }

        private bool isPublic(BoogieProcedure proc)
        {
            if (proc.Attributes != null)
            {
                foreach (var attr in proc.Attributes)
                {
                    if (attr.Key.Equals("public"))
                        return true;
                }
            }

            return false;
        }

        private bool isPayable(BoogieProcedure proc)
        {
            //return true;
            if (proc.Attributes != null)
            {
                foreach (var attr in proc.Attributes)
                {
                    if (attr.Key.Equals("payable"))
                        return true;
                }
            }

            return false;
        }

        private BoogieProcedure duplicateProcedure(BoogieProcedure proc, string newNameSuffix, bool dropPublic = false)
        {
            BoogieProcedure dup = new BoogieProcedure(proc.Name + "__" + newNameSuffix, 
                new List<BoogieVariable>(proc.InParams), 
                new List<BoogieVariable>(proc.OutParams),
                 proc.Attributes != null ? new List<BoogieAttribute>(proc.Attributes) : null);

            if (dropPublic && dup.Attributes != null)
                dup.Attributes.RemoveAll(attribute => attribute.Key.Equals("public"));

            return dup;
        }

        private BoogieExpr dupAndReplaceExpr(BoogieExpr expr, bool isFail, bool inHarness)
        {
            List<BoogieExpr> dupAndReplaceExprList(List<BoogieExpr> exprs)
            {
                return exprs.Select(e => dupAndReplaceExpr(e, isFail, inHarness)).ToList();
            }

            if (expr is BoogieIdentifierExpr)
            {
                string idName = ((BoogieIdentifierExpr) expr).Name;
                if (isFail && shadowGlobals.ContainsKey(idName))
                {
                    return new BoogieIdentifierExpr(shadowGlobals[idName].Name);
                }

                return expr;
            }
            if (expr is BoogieMapSelect)
            {
                BoogieMapSelect selectExpr = (BoogieMapSelect) expr;
                return new BoogieMapSelect(dupAndReplaceExpr(selectExpr.BaseExpr, isFail, inHarness),
                                           dupAndReplaceExprList(selectExpr.Arguments));
            }
            if (expr is BoogieMapUpdate)
            {
                BoogieMapUpdate updateExpr = (BoogieMapUpdate) expr;
                return new BoogieMapUpdate(dupAndReplaceExpr(updateExpr.BaseExpr, isFail, inHarness),
                                           dupAndReplaceExprList(updateExpr.Arguments), 
                                           dupAndReplaceExpr(updateExpr.Value, isFail, inHarness));
            }
            if (expr is BoogieUnaryOperation)
            {
                BoogieUnaryOperation unaryOperation = (BoogieUnaryOperation) expr;
                return new BoogieUnaryOperation(unaryOperation.Op, dupAndReplaceExpr(unaryOperation.Expr, isFail, inHarness));
            }
            if (expr is BoogieBinaryOperation)
            {
                BoogieBinaryOperation binOperation = (BoogieBinaryOperation) expr;
                return new BoogieBinaryOperation(binOperation.Op,dupAndReplaceExpr(binOperation.Lhs, isFail, inHarness),
                                                dupAndReplaceExpr(binOperation.Rhs, isFail, inHarness));
            }
            if (expr is BoogieITE)
            {
                BoogieITE iteExpr = (BoogieITE) expr;
                return new BoogieITE(dupAndReplaceExpr(iteExpr.Guard, isFail, inHarness),
                                     dupAndReplaceExpr(iteExpr.ThenExpr, isFail, inHarness),
                                     dupAndReplaceExpr(iteExpr.ElseExpr, isFail, inHarness));
            }
            if (expr is BoogieQuantifiedExpr)
            {
                BoogieQuantifiedExpr quantifiedExpr = (BoogieQuantifiedExpr) expr;
                return  new BoogieQuantifiedExpr(quantifiedExpr.IsForall, quantifiedExpr.QVars, quantifiedExpr.QVarTypes,
                                                 dupAndReplaceExpr(quantifiedExpr.BodyExpr, isFail, inHarness),
                                                 quantifiedExpr.Trigger);
            }
            if (expr is BoogieFuncCallExpr)
            {
                BoogieFuncCallExpr callExpr = (BoogieFuncCallExpr) expr;

                string calledFun = callExpr.Function;

                // TODO: handle this properly.
//                if (!isHarnessProcudure(calledFun) && procsWiltImplNames.Contains(calledFun))
//                {
//                    if (!inHarness || (!isConstructor(calledFun) && !isPublic(proceduresInProgram[calledFun])))
//                        calledFun = calledFun + (isFail ? "__fail" : "__success");
//                }
                
                return new BoogieFuncCallExpr(calledFun, dupAndReplaceExprList(callExpr.Arguments));
            }
            if (expr is BoogieTupleExpr)
            {
                BoogieTupleExpr tupleExpr = (BoogieTupleExpr) expr;
                return  new BoogieTupleExpr(dupAndReplaceExprList(tupleExpr.Arguments));
            }
            
            return expr;
        }

        bool catchesExceptions(string methodName)
        {
            return methodName.Equals("send");
        }

        private void generateRevertLogicForCmd(BoogieCmd cmd, BoogieStmtList parent, bool isFail, bool inHarness)
        {
            List<BoogieExpr> dupAndReplaceExprList(List<BoogieExpr> exprs)
            {
                return exprs.Select(e => dupAndReplaceExpr(e, isFail, inHarness)).ToList();
            }
            
            BoogieStmtList dupAndReplaceStmList(BoogieStmtList stmtList)
            {
                if (stmtList == null)
                    return null;

                BoogieStmtList newList = new BoogieStmtList();
                stmtList.BigBlocks[0].SimpleCmds.ForEach(c => generateRevertLogicForCmd(c, newList, isFail, inHarness));
                return newList;
            }
            
            if (cmd is BoogieAssignCmd assignCmd)
            {
                parent.AddStatement(new BoogieAssignCmd(dupAndReplaceExpr(assignCmd.Lhs, isFail, inHarness),
                                                        dupAndReplaceExpr(assignCmd.Rhs, isFail, inHarness)));
            }
            else if (cmd is BoogieCallCmd callCmd)
            {
                string calleeName = callCmd.Callee;
                bool emitCheckRevertLogic = false;
                if (!isHarnessProcudure(calleeName) && procsWiltImplNames.Contains(calleeName))
                {
                    if (!inHarness || !isPublic(proceduresInProgram[calleeName]))
                    {
                        emitCheckRevertLogic = !inHarness && !catchesExceptions(calleeName);
                        calleeName = calleeName + (isFail ? "__fail" : "__success");
                    }
                }

                var newIns = callCmd.Ins != null ? dupAndReplaceExprList(callCmd.Ins) : null;
                var newOuts = callCmd.Outs?.Select(e => (BoogieIdentifierExpr) dupAndReplaceExpr(e, isFail, inHarness)).ToList();
                parent.AddStatement(new BoogieCallCmd(calleeName, newIns, newOuts));

                if (emitCheckRevertLogic)
                {
                    BoogieStmtList thenBody = new BoogieStmtList();
                    thenBody.AddStatement(new BoogieReturnCmd());

                    parent.AddStatement(new BoogieIfCmd(new BoogieIdentifierExpr("revert"), thenBody, null));
                }
            }
            else if (cmd is BoogieAssertCmd assertCmd)
            {
                if (!isFail)
                {
                    parent.AddStatement(new BoogieAssertCmd(dupAndReplaceExpr(assertCmd.Expr, false, inHarness), assertCmd.Attributes));
                }
            }
            else if (cmd is BoogieAssumeCmd assumeCmd)
            {
                parent.AddStatement(new BoogieAssumeCmd(dupAndReplaceExpr(assumeCmd.Expr, isFail, inHarness)));
            }
            else if (cmd is BoogieLoopInvCmd loopInvCmd)
            {
                parent.AddStatement(new BoogieLoopInvCmd(dupAndReplaceExpr(loopInvCmd.Expr, isFail, inHarness)));
            }
            else if (cmd is BoogieReturnExprCmd returnExprCmd)
            {
                // This one does not seem to be used.
                parent.AddStatement(new BoogieReturnExprCmd(dupAndReplaceExpr(returnExprCmd.Expr, isFail, inHarness)));
            }
            else if (cmd is BoogieHavocCmd havocCmd)
            {
                parent.AddStatement(new BoogieHavocCmd(havocCmd.Vars.Select(id => (BoogieIdentifierExpr) dupAndReplaceExpr(id, isFail, inHarness)).ToList()));
            }
            else if (cmd is BoogieIfCmd ifCmd)
            {
                parent.AddStatement(new BoogieIfCmd(dupAndReplaceExpr(ifCmd.Guard, isFail, inHarness),
                                       dupAndReplaceStmList(ifCmd.ThenBody),
                                       dupAndReplaceStmList(ifCmd.ElseBody)));
            }
            else if (cmd is BoogieWhileCmd whileCmd)
            {
                var body = dupAndReplaceStmList(whileCmd.Body);
                BoogieStmtList invsAsStmtList = new BoogieStmtList();
                whileCmd.Invariants.ForEach(i => generateRevertLogicForCmd(i, invsAsStmtList, isFail, inHarness));
                List<BoogiePredicateCmd> invs = invsAsStmtList.BigBlocks[0].SimpleCmds.Select(c => (BoogiePredicateCmd) c).ToList();
                parent.AddStatement( new BoogieWhileCmd(dupAndReplaceExpr(whileCmd.Guard, isFail, inHarness), body, invs));
            }
            else
            {
                parent.AddStatement(cmd);
            }
        }

        private BoogieImplementation createFailImplementation(string name, BoogieImplementation originalImpl)
        {
            Debug.Assert(!isHarnessProcudure(originalImpl.Name));

            BoogieStmtList failStmtList = new BoogieStmtList();

            foreach (var cmd in originalImpl.StructuredStmts.BigBlocks[0].SimpleCmds)
            {
                generateRevertLogicForCmd(cmd, failStmtList,true, false);
            }

            return new BoogieImplementation(name, originalImpl.InParams, originalImpl.OutParams, originalImpl.LocalVars, failStmtList);
        }

        private bool isHarnessProcudure(string procName)
        {
            return procName.StartsWith("Boogie") || procName.StartsWith("Corral") || procName.Equals("main");
        }

        private BoogieImplementation createSuccessImplementation(string name, BoogieImplementation originalImpl)
        {
            BoogieStmtList successStmtList = new BoogieStmtList();

            foreach (var cmd in originalImpl.StructuredStmts.BigBlocks[0].SimpleCmds)
            {
                generateRevertLogicForCmd(cmd, successStmtList, false, isHarnessProcudure(originalImpl.Name));
            }

            return new BoogieImplementation(name, originalImpl.InParams, originalImpl.OutParams, originalImpl.LocalVars, successStmtList);
        }
        
        public RevertLogicGenerator(TranslatorContext context)
        {
            this.context = context; 
            this.constructorNames = context.ContractDefinitions.Select(c => TransUtils.GetCanonicalConstructorName(c)).ToHashSet();
            proceduresInProgram = context.Program.Declarations.OfType<BoogieProcedure>().ToDictionary(procedure => procedure.Name);
        }

        bool isConstructor(string funcName)
        {
            return constructorNames.Any(funcName.Equals);
        }

        public void Generate()
        {
            // Collect Global Variables.
            HashSet<BoogieGlobalVariable> globals = new HashSet<BoogieGlobalVariable>();
            foreach (var decl in context.Program.Declarations)
            {
                if (decl is BoogieGlobalVariable)
                {
                    var g = (BoogieGlobalVariable)decl;
                    
                    if (mustHaveShadow(g.Name)) 
                        globals.Add(g);
                }
            }
            
            // Generate shadow state.
            foreach (var g in globals)
            {
                var varName = g.TypedIdent.Name;
                BoogieTypedIdent shadowGlobal = new BoogieTypedIdent("__tmp__" + varName, g.TypedIdent.Type);
                BoogieGlobalVariable shadowGlobalDecl = new BoogieGlobalVariable(shadowGlobal);
                
                context.Program.AddDeclaration(shadowGlobalDecl);
                shadowGlobals.Add(varName, shadowGlobalDecl);
            }
            
            // Duplicate and rename methods.
            Dictionary<string, BoogieImplementation> proceduresWithImpl = new Dictionary<string, BoogieImplementation>();
            procsWiltImplNames = proceduresWithImpl.Keys;
            foreach (var decl in context.Program.Declarations)
            {
                if (decl is BoogieImplementation)
                {
                    var boogieImpl = ((BoogieImplementation) decl);
                    if (proceduresInProgram.ContainsKey(boogieImpl.Name))
                    {
                        proceduresWithImpl.Add(boogieImpl.Name, boogieImpl);
                    }
                }
            }

            Dictionary<string, BoogieProcedure> failProcedures = new Dictionary<string, BoogieProcedure>();
            foreach (var implPair in proceduresWithImpl)
            {
                BoogieProcedure proc = proceduresInProgram[implPair.Key];

                if (isPublic(proc) || isConstructor(proc.Name))
                {
                    // If public maintain original definition as the wrapper.
                    BoogieProcedure successVersion = duplicateProcedure(proc, "success", true);
                    //BoogieImplementation successImpl = duplicateImplementation(implPair.Value, "success");
                    
                    context.Program.AddDeclaration(successVersion);
                    //context.Program.AddDeclaration(successImpl);
                    
                    BoogieProcedure failVersion = duplicateProcedure(proc, "fail", true);
                    
                    context.Program.AddDeclaration(failVersion);
                    
                    // Use original name of the procedure.
                    failProcedures.Add(implPair.Key, failVersion);
                }
                else if (!isHarnessProcudure(proc.Name))
                {
                    // Otherwise reuse original definition/impl as the "successful" method.
                    BoogieProcedure failVersion = duplicateProcedure(proc, "fail");
                    
                    context.Program.AddDeclaration(failVersion);

                    // Use original name of the procedure.
                    failProcedures.Add(implPair.Key, failVersion);
                    
                    // Reuse original node
                    proc.Name = proc.Name + "__success";
                    implPair.Value.Name = implPair.Value.Name + "__success";
                }
            }

            // Create implementations for failing methods.
            foreach (var failProcedurePair in failProcedures)
            {
                string originalProcName = failProcedurePair.Key;
                BoogieProcedure proc = failProcedurePair.Value;

                var originalImpl = proceduresWithImpl[originalProcName];
                if (!originalProcName.Equals("send"))
                {
                    context.Program.AddDeclaration(createFailImplementation(proc.Name, originalImpl));
                    context.Program.AddDeclaration(createSuccessImplementation(originalProcName + "__success", originalImpl));
                }
                else
                {
                    context.Program.AddDeclaration(CreateSendFail());
                    context.Program.AddDeclaration(CreateSendSucess());
                }

                // Remove original implementation for non-public methods
                if (!isPublic(proceduresInProgram[originalProcName]) && !isConstructor(originalProcName))
                {
                    context.Program.Declarations.Remove(originalImpl);
                }
            }

            foreach (var implPair in proceduresWithImpl)
            {
                // Update non-public methods in harness methods
                if (isHarnessProcudure(implPair.Key))
                {
                    context.Program.AddDeclaration(createSuccessImplementation(implPair.Key, implPair.Value));

                    context.Program.Declarations.Remove(implPair.Value);
                }
            }

            // Create wrappers for public methods.

            foreach (var proc in proceduresInProgram.Values)
            {
                if (proceduresWithImpl.ContainsKey(proc.Name) && (isPublic(proc) || isConstructor(proc.Name)))
                {
                    BoogieImplementation impl = proceduresWithImpl[proc.Name];
                    impl.StructuredStmts = new BoogieStmtList();
                    impl.LocalVars = new List<BoogieVariable>();

                    var exceptionVarName = "__exception";
                    var revertGlobalName = "revert";
                    impl.LocalVars.Add(new BoogieLocalVariable(new BoogieTypedIdent(exceptionVarName, BoogieType.Bool)));

                    var stmtList = impl.StructuredStmts;
                    stmtList.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr(exceptionVarName)));
                    stmtList.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr(revertGlobalName), new BoogieLiteralExpr(false)));
                    
                    // Call Successful version.
                    BoogieStmtList successCallStmtList = new BoogieStmtList();
                    if (isPayable(proc))
                    {
                        successCallStmtList.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
                        var balnSender = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr("msgsender_MSG"));
                        var balnThis = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr("this"));
                        var msgVal = new BoogieIdentifierExpr("msgvalue_MSG");
                        //assume Balance[msg.sender] >= msg.value
                        successCallStmtList.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, balnSender, msgVal)));
                        //balance[msg.sender] = balance[msg.sender] - msg.value
                        successCallStmtList.AddStatement(new BoogieAssignCmd(balnSender, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, balnSender, msgVal)));
                        //balance[this] = balance[this] + msg.value
                        successCallStmtList.AddStatement(new BoogieAssignCmd(balnThis, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, balnThis, msgVal)));
                        successCallStmtList.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
                    }
                    successCallStmtList.AddStatement(new BoogieCallCmd(impl.Name + "__success", 
                                                                       impl.InParams.Select(inParam => (BoogieExpr)new BoogieIdentifierExpr(inParam.Name)).ToList(), 
                                                                       impl.OutParams.Select(outParam => new BoogieIdentifierExpr(outParam.Name)).ToList()));
                    BoogieExpr successAssumePred = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, new BoogieIdentifierExpr(revertGlobalName));

                    if (context.TranslateFlags.InstrumentGas)
                    {
                        successAssumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, successAssumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));
                    }
                    successCallStmtList.AddStatement(new BoogieAssumeCmd(successAssumePred));
                    
                    // Call fail version.
                    BoogieStmtList failCallStmtList = new BoogieStmtList();
                    // Write global variables to temps
                    foreach (var shadowGlobalPair in shadowGlobals)
                    {
                        string origVarName = shadowGlobalPair.Key;
                        string shadowName = shadowGlobalPair.Value.Name;
                        failCallStmtList.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr(shadowName), new BoogieIdentifierExpr(origVarName)));
                    }

                    if (isPayable(proc))
                    {
                        failCallStmtList.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
                        var balnSender = new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), new BoogieIdentifierExpr("msgsender_MSG"));
                        var balnThis = new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), new BoogieIdentifierExpr("this"));
                        var msgVal = new BoogieIdentifierExpr("msgvalue_MSG");
                        //assume Balance[msg.sender] >= msg.value
                        failCallStmtList.AddStatement(new BoogieAssumeCmd(new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, balnSender, msgVal)));
                        //balance[msg.sender] = balance[msg.sender] - msg.value
                        failCallStmtList.AddStatement(new BoogieAssignCmd(balnSender, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, balnSender, msgVal)));
                        //balance[this] = balance[this] + msg.value
                        failCallStmtList.AddStatement(new BoogieAssignCmd(balnThis, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, balnThis, msgVal)));
                        failCallStmtList.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
                    }
                    failCallStmtList.AddStatement(new BoogieCallCmd(impl.Name + "__fail", 
                                                 impl.InParams.Select(inParam => (BoogieExpr)new BoogieIdentifierExpr(inParam.Name)).ToList(), 
                                                impl.OutParams.Select(outParam => new BoogieIdentifierExpr(outParam.Name)).ToList()));
                    BoogieExpr failAssumePred = new BoogieIdentifierExpr(revertGlobalName);

                    if (context.TranslateFlags.InstrumentGas)
                    {
                        failAssumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, failAssumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.LT, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));
                    }
                    failCallStmtList.AddStatement(new BoogieAssumeCmd(failAssumePred));
                    
                    stmtList.AddStatement(new BoogieIfCmd(new BoogieIdentifierExpr(exceptionVarName), failCallStmtList, successCallStmtList));
                }
            }
        }

        private BoogieImplementation CreateSendFail()
        {
            // send__fail(from: Ref, to: Ref, amt: uint) returns (success: boolean)
            // {
            //    var __exception: bool;
            //    havoc __exception;
            //
            //    if(__exception)
            //    {
            //       //save current temps
            //      if ((__tmp__Balance[from]) >= (amt)) {
            //           call FallbackDispatch__fail(from, to, amt);
            //      }
            //
            //       success := false;
            //       assume(__revert);
            //
            //       // restore old temps
            //       revert := false;
            //    }
            //    else {
            //       if ((__tmp__Balance[from]) >= (amt)) {
            //           call FallbackDispatch__fail(from, to, amt);
            //           success := true;
            //       } else {
            //           success := false;
            //       }
            //
            //       assume(!(__revert));
            //    }
            // }

            List<BoogieVariable> inParams = new List<BoogieVariable>()
            {
                new BoogieFormalParam(new BoogieTypedIdent("from", BoogieType.Ref)),
                new BoogieFormalParam(new BoogieTypedIdent("to", BoogieType.Ref)),
                new BoogieFormalParam(new BoogieTypedIdent("amount", BoogieType.Int))
            };

            List<BoogieVariable> outParms = new List<BoogieVariable>()
            {
                new BoogieFormalParam(new BoogieTypedIdent("success", BoogieType.Bool))
            };

            List<BoogieVariable> locals = new List<BoogieVariable>()
            {
                new BoogieLocalVariable(new BoogieTypedIdent("__exception", BoogieType.Bool))
            };

            var fromId = new BoogieIdentifierExpr("from");
            var toId = new BoogieIdentifierExpr("to");
            var amtId = new BoogieIdentifierExpr("amount");

            var successId = new BoogieIdentifierExpr("success");

            var revertId = new BoogieIdentifierExpr("revert");

            var exceptionId = new BoogieIdentifierExpr("__exception");

            var body = new BoogieStmtList();

            body.AddStatement(new BoogieHavocCmd(exceptionId));

            var checkTmpBalGuard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE,
                new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), fromId), 
                amtId);
            var callFailDispatch = new BoogieCallCmd("FallbackDispatch__fail", new List<BoogieExpr>() {fromId, toId, amtId}, null);

            var exceptionCase = new BoogieStmtList();

            foreach (var shadowGlobalPair in shadowGlobals)
            {
                var shadowName = shadowGlobalPair.Value.Name;
                var tmpLocalName = "__snap_" + shadowName;
                locals.Add(new BoogieLocalVariable(new BoogieTypedIdent(tmpLocalName, shadowGlobalPair.Value.TypedIdent.Type)));

                exceptionCase.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr(tmpLocalName), new BoogieIdentifierExpr(shadowName)));
            }
            
            BoogieStmtList exStmtList = new BoogieStmtList();
            exStmtList.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
            var exBalFrom = new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), new BoogieIdentifierExpr(inParams[0].Name));
            var exBalTo = new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), new BoogieIdentifierExpr(inParams[1].Name));
            var exMsgVal = new BoogieIdentifierExpr(inParams[2].Name);
            //balance[msg.sender] = balance[msg.sender] - msg.value
            exStmtList.AddStatement(new BoogieAssignCmd(exBalFrom, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, exBalFrom, exMsgVal)));
            //balance[this] = balance[this] + msg.value
            exStmtList.AddStatement(new BoogieAssignCmd(exBalTo, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, exBalTo, exMsgVal)));
            exStmtList.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
            exStmtList.AddStatement(callFailDispatch);

            exceptionCase.AddStatement(new BoogieIfCmd(checkTmpBalGuard, exStmtList, null));
            exceptionCase.AddStatement(new BoogieAssignCmd(successId, new BoogieLiteralExpr(false)));
            BoogieExpr failAssumePred = revertId;
            if (context.TranslateFlags.InstrumentGas)
            {
                failAssumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, failAssumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.LT, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));
            }

            exceptionCase.AddStatement(new BoogieAssumeCmd(failAssumePred));

            foreach (var shadowGlobalPair in shadowGlobals)
            {
                var shadowName = shadowGlobalPair.Value.Name;
                var tmpLocalName = "__snap_" + shadowName;

                exceptionCase.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr(shadowName), new BoogieIdentifierExpr(tmpLocalName)));
            }

            exceptionCase.AddStatement(new BoogieAssignCmd(revertId, new BoogieLiteralExpr(false)));

            var successCase = new BoogieStmtList();
            var successDispatchCall = new BoogieStmtList();

            successDispatchCall.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
            //balance[msg.sender] = balance[msg.sender] - msg.value
            successDispatchCall.AddStatement(new BoogieAssignCmd(exBalFrom, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, exBalFrom, exMsgVal)));
            //balance[this] = balance[this] + msg.value
            successDispatchCall.AddStatement(new BoogieAssignCmd(exBalTo, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, exBalTo, exMsgVal)));
            successDispatchCall.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
            successDispatchCall.AddStatement(callFailDispatch);
            successDispatchCall.AddStatement(new BoogieAssignCmd(successId, new BoogieLiteralExpr(true)));

            successCase.AddStatement(new BoogieIfCmd(checkTmpBalGuard, successDispatchCall, BoogieStmtList.MakeSingletonStmtList(new BoogieAssignCmd(successId, new BoogieLiteralExpr(false)))));
            BoogieExpr successAssumePred = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, revertId);

            if (context.TranslateFlags.InstrumentGas)
            {
                successAssumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, successAssumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));
            }

            successCase.AddStatement(new BoogieAssumeCmd(successAssumePred));

            body.AddStatement(new BoogieIfCmd(exceptionId, exceptionCase, successCase));
            return new BoogieImplementation("send__fail", inParams, outParms, locals, body);
        }

        private BoogieImplementation CreateSendSucess()
        {
            // send__success(from: Ref, to: Ref, amt: uint) returns (success: boolean)
            // {
            //    var __exception: bool;
            //    havoc __exception;
            //
            //    if(__exception)
            //    {
            //        //set tmps
            //        if ((__tmp__Balance[from]) >= (amt)) {
            //            call FallbackDispatch__fail(from, to, amt);
            //        }
            //
            //        success := false;
            //        assume(__revert);
            //
            //        revert := false;
            //    }
            //    else {
            //        if ((Balance[from]) >= (amt)) {
            //            call FallbackDispatch__success(from, to, amt);
            //            success := true;
            //        } else {
            //            success := false;
            //        }
            //
            //        assume(!(__revert));
            //    }
            // }

            List<BoogieVariable> inParams = new List<BoogieVariable>()
            {
                new BoogieFormalParam(new BoogieTypedIdent("from", BoogieType.Ref)),
                new BoogieFormalParam(new BoogieTypedIdent("to", BoogieType.Ref)),
                new BoogieFormalParam(new BoogieTypedIdent("amount", BoogieType.Int))
            };

            List<BoogieVariable> outParms = new List<BoogieVariable>()
            {
                new BoogieFormalParam(new BoogieTypedIdent("success", BoogieType.Bool))
            };

            List<BoogieVariable> locals = new List<BoogieVariable>()
            {
                new BoogieLocalVariable(new BoogieTypedIdent("__exception", BoogieType.Bool))
            };


            var fromId = new BoogieIdentifierExpr("from");
            var toId = new BoogieIdentifierExpr("to");
            var amtId = new BoogieIdentifierExpr("amount");

            var successId = new BoogieIdentifierExpr("success");

            var revertId = new BoogieIdentifierExpr("revert");

            var exceptionId = new BoogieIdentifierExpr("__exception");

            BoogieStmtList body = new BoogieStmtList();

            body.AddStatement(new BoogieHavocCmd(exceptionId));

            BoogieStmtList exceptionCase = new BoogieStmtList();

            foreach (var shadowGlobalPair in shadowGlobals)
            {
                string origVarName = shadowGlobalPair.Key;
                string shadowName = shadowGlobalPair.Value.Name;
                exceptionCase.AddStatement(new BoogieAssignCmd(new BoogieIdentifierExpr(shadowName), new BoogieIdentifierExpr(origVarName)));
            }

            var checkTmpBalGuard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE,
                new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), fromId),
                amtId);
            
            
            BoogieStmtList exceptionThen = new BoogieStmtList();
            exceptionThen.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
            var exBalFrom = new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), new BoogieIdentifierExpr(inParams[0].Name));
            var exBalTo = new BoogieMapSelect(new BoogieIdentifierExpr(shadowGlobals["Balance"].Name), new BoogieIdentifierExpr(inParams[1].Name));
            var exMsgVal = new BoogieIdentifierExpr(inParams[2].Name);
            //balance[msg.sender] = balance[msg.sender] - msg.value
            exceptionThen.AddStatement(new BoogieAssignCmd(exBalFrom, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, exBalFrom, exMsgVal)));
            //balance[this] = balance[this] + msg.value
            exceptionThen.AddStatement(new BoogieAssignCmd(exBalTo, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, exBalTo, exMsgVal)));
            exceptionThen.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
            var callFailDispatch = new BoogieCallCmd("FallbackDispatch__fail", new List<BoogieExpr>() {fromId, toId, amtId}, null);
            exceptionThen.AddStatement(callFailDispatch);

            exceptionCase.AddStatement(new BoogieIfCmd(checkTmpBalGuard, exceptionThen, null));

            exceptionCase.AddStatement(new BoogieAssignCmd(successId, new BoogieLiteralExpr(false)));

            BoogieExpr failAssumePred = revertId;
            if (context.TranslateFlags.InstrumentGas)
            {
                failAssumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.OR, failAssumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.LT, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));
            }

            exceptionCase.AddStatement(new BoogieAssumeCmd(failAssumePred));
            exceptionCase.AddStatement(new BoogieAssignCmd(revertId, new BoogieLiteralExpr(false)));

            var successCase = new BoogieStmtList();

            var checkBalGuard = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE,
                new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), fromId),
                amtId);

            var successCaseStmts = new BoogieStmtList();
            successCaseStmts.AddStatement(new BoogieCommentCmd("---- Logic for payable function START "));
            var balFrom = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr(inParams[0].Name));
            var balTo = new BoogieMapSelect(new BoogieIdentifierExpr("Balance"), new BoogieIdentifierExpr(inParams[1].Name));
            var msgVal = new BoogieIdentifierExpr(inParams[2].Name);
            //balance[msg.sender] = balance[msg.sender] - msg.value
            successCaseStmts.AddStatement(new BoogieAssignCmd(balFrom, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.SUB, balFrom, msgVal)));
            //balance[this] = balance[this] + msg.value
            successCaseStmts.AddStatement(new BoogieAssignCmd(balTo, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.ADD, balTo, msgVal)));
            successCaseStmts.AddStatement(new BoogieCommentCmd("---- Logic for payable function END "));
            var callSuccessDispatch = new BoogieCallCmd("FallbackDispatch__success", new List<BoogieExpr>(){fromId, toId, amtId}, null);
            successCaseStmts.AddStatement(callSuccessDispatch);
            successCaseStmts.AddStatement(new BoogieAssignCmd(successId, new BoogieLiteralExpr(true)));

            successCase.AddStatement(new BoogieIfCmd(checkBalGuard, successCaseStmts, BoogieStmtList.MakeSingletonStmtList(new BoogieAssignCmd(successId, new BoogieLiteralExpr(false)))));

            BoogieExpr successAssumePred = new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, revertId);

            if (context.TranslateFlags.InstrumentGas)
            {
                successAssumePred = new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.AND, successAssumePred, new BoogieBinaryOperation(BoogieBinaryOperation.Opcode.GE, new BoogieIdentifierExpr("gas"), new BoogieLiteralExpr(0)));
            }

            successCase.AddStatement(new BoogieAssumeCmd(successAssumePred));

            body.AddStatement(new BoogieIfCmd(exceptionId, exceptionCase, successCase));
            return new BoogieImplementation("send__success", inParams, outParms, locals, body);
        }
    }
}
