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
                        calleeName = calleeName + (isFail ? "__fail" : "__success");
                        emitCheckRevertLogic = !inHarness;
                    }
                }

                var newIns = callCmd.Ins != null ? dupAndReplaceExprList(callCmd.Ins) : null;
                var newOuts = callCmd.Outs != null ? callCmd.Outs.Select(e => (BoogieIdentifierExpr) dupAndReplaceExpr(e, isFail, inHarness)).ToList() : null;
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
            return procName.StartsWith("Boogie") || procName.StartsWith("Corral");
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
                context.Program.AddDeclaration(createFailImplementation(proc.Name, originalImpl));
                context.Program.AddDeclaration(createSuccessImplementation(originalProcName + "__success", originalImpl));

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
    }
}
