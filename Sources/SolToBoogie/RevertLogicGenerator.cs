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

        private bool mustHaveShadow(string globalName)
        {
            return !globalName.Equals("revert");
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

        private BoogieImplementation duplicateImplementation(BoogieImplementation impl, string newNameSuffix)
        {
            // Duplicate statement list.
            var dupStmtList = new BoogieStmtList();
            // Bit hack-y but the setter is private and don't want to touch the BoogieAST file.
            dupStmtList.BigBlocks.RemoveAt(0);
            dupStmtList.BigBlocks.AddRange(impl.StructuredStmts.BigBlocks);
            
            return new BoogieImplementation(impl.Name + "__" + newNameSuffix,
                new List<BoogieVariable>(impl.InParams),
                new List<BoogieVariable>(impl.OutParams),
                new List<BoogieVariable>(impl.LocalVars),
                         dupStmtList);
        }

        private BoogieExpr dupAndReplaceExpr(BoogieExpr expr, Dictionary<string, BoogieGlobalVariable> shadowGlobals,
                                             Dictionary<string, BoogieImplementation>.KeyCollection procsWiltImpl)
        {
            List<BoogieExpr> dupAndReplaceExprList(List<BoogieExpr> exprs)
            {
                return exprs.Select(e => dupAndReplaceExpr(e, shadowGlobals, procsWiltImpl)).ToList();
            }

            if (expr is BoogieIdentifierExpr)
            {
                string idName = ((BoogieIdentifierExpr) expr).Name;
                if (shadowGlobals.ContainsKey(idName))
                {
                    return new BoogieIdentifierExpr(shadowGlobals[idName].Name);
                }

                return expr;
            }
            if (expr is BoogieMapSelect)
            {
                BoogieMapSelect selectExpr = (BoogieMapSelect) expr;
                return new BoogieMapSelect(dupAndReplaceExpr(selectExpr.BaseExpr, shadowGlobals, procsWiltImpl),
                                           dupAndReplaceExprList(selectExpr.Arguments));
            }
            if (expr is BoogieMapUpdate)
            {
                BoogieMapUpdate updateExpr = (BoogieMapUpdate) expr;
                return new BoogieMapUpdate(dupAndReplaceExpr(updateExpr.BaseExpr, shadowGlobals, procsWiltImpl), 
                                           dupAndReplaceExprList(updateExpr.Arguments), 
                                           dupAndReplaceExpr(updateExpr.Value, shadowGlobals, procsWiltImpl));
            }
            if (expr is BoogieUnaryOperation)
            {
                BoogieUnaryOperation unaryOperation = (BoogieUnaryOperation) expr;
                return new BoogieUnaryOperation(unaryOperation.Op, dupAndReplaceExpr(unaryOperation.Expr, shadowGlobals, procsWiltImpl));
            }
            if (expr is BoogieBinaryOperation)
            {
                BoogieBinaryOperation binOperation = (BoogieBinaryOperation) expr;
                return new BoogieBinaryOperation(binOperation.Op,dupAndReplaceExpr(binOperation.Lhs, shadowGlobals, procsWiltImpl),
                                                dupAndReplaceExpr(binOperation.Rhs, shadowGlobals, procsWiltImpl));
            }
            if (expr is BoogieITE)
            {
                BoogieITE iteExpr = (BoogieITE) expr;
                return new BoogieITE(dupAndReplaceExpr(iteExpr.Guard, shadowGlobals, procsWiltImpl),
                                     dupAndReplaceExpr(iteExpr.ThenExpr, shadowGlobals, procsWiltImpl),
                                     dupAndReplaceExpr(iteExpr.ElseExpr, shadowGlobals, procsWiltImpl));
            }
            if (expr is BoogieQuantifiedExpr)
            {
                BoogieQuantifiedExpr quantifiedExpr = (BoogieQuantifiedExpr) expr;
                return  new BoogieQuantifiedExpr(quantifiedExpr.IsForall, quantifiedExpr.QVars, quantifiedExpr.QVarTypes,
                                                 dupAndReplaceExpr(quantifiedExpr.BodyExpr, shadowGlobals, procsWiltImpl),
                                                 quantifiedExpr.Trigger);
            }
            if (expr is BoogieFuncCallExpr)
            {
                BoogieFuncCallExpr callExpr = (BoogieFuncCallExpr) expr;

                string calledFun = callExpr.Function;
                if (!isConstructor(calledFun) && procsWiltImpl.Contains(calledFun))
                {
                    calledFun = calledFun + "__fail";

                    // This is lazy, but I don't want to generalize these methods.
                    callExpr.Function = callExpr.Function + "__succeess";
                }
                
                return new BoogieFuncCallExpr(calledFun, dupAndReplaceExprList(callExpr.Arguments));
            }
            if (expr is BoogieTupleExpr)
            {
                BoogieTupleExpr tupleExpr = (BoogieTupleExpr) expr;
                return  new BoogieTupleExpr(dupAndReplaceExprList(tupleExpr.Arguments));
            }
            
            return expr;
        }

        private BoogieCmd dupAndReplaceCmd(BoogieCmd cmd, Dictionary<string, BoogieGlobalVariable> shadowGlobals, 
                                           Dictionary<string, BoogieImplementation>.KeyCollection procsWiltImpl)
        {
            List<BoogieExpr> dupAndReplaceExprList(List<BoogieExpr> exprs)
            {
                return exprs.Select(e => dupAndReplaceExpr(e, shadowGlobals, procsWiltImpl)).ToList();
            }
            
            BoogieStmtList dupAndReplaceStmList(BoogieStmtList stmtList)
            {
                if (stmtList == null)
                    return null;

                BoogieStmtList newList = new BoogieStmtList();
                stmtList.BigBlocks[0].SimpleCmds.ForEach(c => newList.AddStatement(dupAndReplaceCmd(c, shadowGlobals, procsWiltImpl)));
                return newList;
            }
            
            if (cmd is BoogieAssignCmd)
            {
                BoogieAssignCmd assignCmd = (BoogieAssignCmd) cmd;
                return new BoogieAssignCmd(dupAndReplaceExpr(assignCmd.Lhs, shadowGlobals, procsWiltImpl),
                                           dupAndReplaceExpr(assignCmd.Rhs, shadowGlobals, procsWiltImpl));
            }
            if (cmd is BoogieCallCmd)
            {
                BoogieCallCmd callCmd = (BoogieCallCmd) cmd;

                string calleeName = callCmd.Callee;
                if (!isConstructor(calleeName) && procsWiltImpl.Contains(calleeName))
                {
                    calleeName = calleeName + "__fail";
                    
                    // This is a bit lazy, but I don't want to generalize these methods.
                    callCmd.Callee = callCmd.Callee + "__success";
                }

                return new BoogieCallCmd(calleeName, dupAndReplaceExprList(callCmd.Ins), 
                                         callCmd.Outs.Select(e => (BoogieIdentifierExpr) dupAndReplaceExpr(e, shadowGlobals, procsWiltImpl)).ToList());
            }
            if (cmd is BoogieAssertCmd)
            {
                BoogieAssertCmd assertCmd = (BoogieAssertCmd) cmd;
                return new BoogieAssertCmd(dupAndReplaceExpr(assertCmd.Expr, shadowGlobals, procsWiltImpl), assertCmd.Attributes);
            }
            if (cmd is BoogieAssumeCmd)
            {
                BoogieAssumeCmd assumeCmd = (BoogieAssumeCmd) cmd;
                return new BoogieAssumeCmd(dupAndReplaceExpr(assumeCmd.Expr, shadowGlobals, procsWiltImpl));
            }
            if (cmd is BoogieLoopInvCmd)
            {
                BoogieLoopInvCmd loopInvCmd = (BoogieLoopInvCmd) cmd;
                return new BoogieLoopInvCmd(dupAndReplaceExpr(loopInvCmd.Expr, shadowGlobals, procsWiltImpl));
            }
            if (cmd is BoogieReturnExprCmd)
            {
                // This one does not seem to be used.
                BoogieReturnExprCmd returnExprCmd = (BoogieReturnExprCmd) cmd;
                return new BoogieReturnExprCmd(dupAndReplaceExpr(returnExprCmd.Expr, shadowGlobals, procsWiltImpl));
            }
            if (cmd is BoogieHavocCmd)
            {
                BoogieHavocCmd havocCmd = (BoogieHavocCmd) cmd;
                return new BoogieHavocCmd(havocCmd.Vars.Select(id => (BoogieIdentifierExpr) dupAndReplaceExpr(id, shadowGlobals, procsWiltImpl)).ToList());
            }
            if (cmd is BoogieIfCmd)
            {
                BoogieIfCmd ifCmd = (BoogieIfCmd) cmd;
                return new BoogieIfCmd(dupAndReplaceExpr(ifCmd.Guard, shadowGlobals, procsWiltImpl),
                                       dupAndReplaceStmList(ifCmd.ThenBody),
                                       dupAndReplaceStmList(ifCmd.ElseBody));
            }
            if (cmd is BoogieWhileCmd)
            {
                BoogieWhileCmd whileCmd = (BoogieWhileCmd) cmd;
                return new BoogieWhileCmd(dupAndReplaceExpr(whileCmd.Guard, shadowGlobals, procsWiltImpl), 
                                          dupAndReplaceStmList(whileCmd.Body),
                                          whileCmd.Invariants.Select(inv => (BoogiePredicateCmd) dupAndReplaceCmd(inv, shadowGlobals, procsWiltImpl)).ToList());
            }
            return cmd;
        }

        private BoogieImplementation createFailImplementation(string name, BoogieImplementation originalImpl, 
                                                              Dictionary<string, BoogieGlobalVariable> shadowGlobals, 
                                                              Dictionary<string, BoogieImplementation>.KeyCollection procsWiltImpl)
        {
            BoogieStmtList failStmtList = new BoogieStmtList();

            foreach (var cmd in originalImpl.StructuredStmts.BigBlocks[0].SimpleCmds)
            {
                failStmtList.AddStatement(dupAndReplaceCmd(cmd, shadowGlobals, procsWiltImpl));
            }

            return new BoogieImplementation(name, originalImpl.InParams, originalImpl.OutParams, originalImpl.LocalVars, failStmtList);
        }
        
        public RevertLogicGenerator(TranslatorContext context)
        {
            this.context = context; 
            this.constructorNames = context.ContractDefinitions.Select(c => TransUtils.GetCanonicalConstructorName(c)).ToHashSet();
        }

        bool isConstructor(string funcName)
        {
            return constructorNames.Any(funcName.StartsWith);
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
            Dictionary<string, BoogieGlobalVariable> shadowGlobals = new Dictionary<string, BoogieGlobalVariable>();
            foreach (var g in globals)
            {
                var varName = g.TypedIdent.Name;
                BoogieTypedIdent shadowGlobal = new BoogieTypedIdent("__tmp__" + varName, g.TypedIdent.Type);
                BoogieGlobalVariable shadowGlobalDecl = new BoogieGlobalVariable(shadowGlobal);
                
                context.Program.AddDeclaration(shadowGlobalDecl);
                shadowGlobals.Add(varName, shadowGlobalDecl);
            }
            
            // Duplicate and rename methods.
            Dictionary<string, BoogieProcedure> proceduresInProgram = new Dictionary<string, BoogieProcedure>();
           
            foreach (var decl in context.Program.Declarations)
            {
                if (decl is BoogieProcedure)
                {
                    var procedure = (BoogieProcedure) decl;
                    proceduresInProgram.Add(procedure.Name, procedure);
                }
            }

            Dictionary<string, BoogieImplementation> proceduresWithImpl = new Dictionary<string, BoogieImplementation>();
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

                if (isPublic(proc))
                {
                    // If public maintain original definition as the wrapper.
                    BoogieProcedure successVersion = duplicateProcedure(proc, "success", true);
                    BoogieImplementation successImpl = duplicateImplementation(implPair.Value, "success");
                    
                    context.Program.AddDeclaration(successVersion);
                    context.Program.AddDeclaration(successImpl);
                    
                    BoogieProcedure failVersion = duplicateProcedure(proc, "fail", true);
                    
                    context.Program.AddDeclaration(failVersion);
                    
                    // Use original name of the procedure.
                    failProcedures.Add(implPair.Key, failVersion);
                }
                else if (!isConstructor(proc.Name))
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
                string procName = failProcedurePair.Key;
                BoogieProcedure proc = failProcedurePair.Value;
                
                context.Program.AddDeclaration(createFailImplementation(proc.Name, proceduresWithImpl[procName], shadowGlobals, proceduresWithImpl.Keys));
            }

            // A bit hack-y but will hopefully do the trick for now
            foreach (var nameImplPair in proceduresWithImpl)
            {
                var pName = nameImplPair.Key;
                
                // This is just to update calls in constructors
                if (isConstructor(pName))
                    createFailImplementation(pName, nameImplPair.Value, shadowGlobals, proceduresWithImpl.Keys);
            }
            
            // Create wrappers for public methods.

            foreach (var proc in proceduresInProgram.Values)
            {
                if (isPublic(proc))
                {
                    BoogieImplementation impl = proceduresWithImpl[proc.Name];
                    impl.StructuredStmts = new BoogieStmtList();
                    impl.LocalVars = new List<BoogieVariable>();

                    var exceptionVarName = "__exception";
                    var revertGlobalName = "revert";
                    impl.LocalVars.Add(new BoogieLocalVariable(new BoogieTypedIdent(exceptionVarName, BoogieType.Bool)));

                    var stmtList = impl.StructuredStmts;
                    stmtList.AddStatement(new BoogieHavocCmd(new BoogieIdentifierExpr(exceptionVarName)));
                    
                    // Call Successful version.
                    BoogieStmtList successCallStmtList = new BoogieStmtList();
                    successCallStmtList.AddStatement(new BoogieCallCmd(impl.Name + "__success", 
                                                                       impl.InParams.Select(inParam => (BoogieExpr)new BoogieIdentifierExpr(inParam.Name)).ToList(), 
                                                                       impl.OutParams.Select(outParam => new BoogieIdentifierExpr(outParam.Name)).ToList()));
                    successCallStmtList.AddStatement(new BoogieAssumeCmd(new BoogieUnaryOperation(BoogieUnaryOperation.Opcode.NOT, new BoogieIdentifierExpr(revertGlobalName))));
                    
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
                    failCallStmtList.AddStatement(new BoogieAssumeCmd(new BoogieIdentifierExpr(revertGlobalName)));
                    
                    stmtList.AddStatement(new BoogieIfCmd(new BoogieIdentifierExpr(exceptionVarName), failCallStmtList, successCallStmtList));
                }
            }
        }
    }
}