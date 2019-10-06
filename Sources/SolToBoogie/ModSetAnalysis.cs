using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SolToBoogie
{
    using BoogieAST;
    
    public class ModSetAnalysis
    {
        private TranslatorContext context;
        
        Dictionary<string, HashSet<BoogieGlobalVariable>> modSets = new Dictionary<string, HashSet<BoogieGlobalVariable>>();
        
        Dictionary<string, List<BoogieCmd>> flattenedCmdLists = new Dictionary<string, List<BoogieCmd>>();

        public ModSetAnalysis(TranslatorContext context)
        {
            this.context = context;
        }

        public void PerformModSetAnalysis()
        {
            BoogieProgram prog = context.Program;
            
            List<BoogieImplementation> impls = new List<BoogieImplementation>();
            Dictionary<string, BoogieGlobalVariable> globals = new Dictionary<string, BoogieGlobalVariable>();
            Dictionary<string, BoogieProcedure> procedures = new Dictionary<string, BoogieProcedure>();

            foreach (var decl in prog.Declarations)
            {
                if (decl is BoogieImplementation)
                {
                    impls.Add((BoogieImplementation) decl);
                }
                else if (decl is BoogieProcedure)
                {
                    BoogieProcedure proc = (BoogieProcedure) decl;
                    procedures.Add(proc.Name, proc);
                }
                else if (decl is BoogieGlobalVariable)
                {
                    var globalVar = (BoogieGlobalVariable) decl;
                    globals.Add(globalVar.Name, globalVar);
                }
            }
            
            calculateFlattenCmdLists(impls);

            foreach (var impl in impls)
            {
                HashSet<BoogieGlobalVariable> modSet = new HashSet<BoogieGlobalVariable>();
                modSets.Add(impl.Name, modSet);

                foreach (var stmt in flattenedCmdLists[impl.Name])
                {
                    switch (stmt)
                    {
                        case BoogieHavocCmd havoc:
                            foreach (var identifier in havoc.Vars)
                            {
                                var idName = identifier.Name;
                                if (globals.ContainsKey(idName))
                                {
                                    modSet.Add(globals[idName]);
                                }
                            }
                            break;
                        case BoogieAssignCmd assign:
                            switch (assign.Lhs)
                            {
                                case BoogieIdentifierExpr idExpr:
                                {
                                    var idName = idExpr.Name;
                                    if (globals.ContainsKey(idName))
                                        modSet.Add(globals[idName]);
                                    break;
                                }
                                case BoogieMapSelect mapSelect:
                                {
                                    var idName = findOutermostIdentifierExpr(mapSelect).Name;
                                    if (globals.ContainsKey(idName))
                                        modSet.Add(globals[idName]);
                                    break;
                                }
                                default:
                                    throw new RuntimeWrappedException("unexpected LHS");
                            }
                            break;
                    }
                }
            }

            bool modSetChange;
            do
            {
                modSetChange = false;

                foreach (var impl in impls)
                {
                    HashSet<BoogieGlobalVariable> currModSet = modSets[impl.Name];
                    int oldSize = currModSet.Count;
                    foreach (var stmt in flattenedCmdLists[impl.Name])
                    {
                        if (stmt is BoogieCallCmd)
                        {
                            BoogieCallCmd callCmd = (BoogieCallCmd) stmt;

                            if (modSets.ContainsKey(callCmd.Callee))
                            {
                                currModSet.UnionWith(modSets[callCmd.Callee]);
                            }
                        }
                    }

                    if (currModSet.Count > oldSize)
                        modSetChange = true;
                }
            } while (modSetChange);

            foreach (var impl in impls)
            {
                procedures[impl.Name].ModSet = modSets[impl.Name].ToList();
            }
        }

        private BoogieIdentifierExpr findOutermostIdentifierExpr(BoogieMapSelect select)
        {
            if (select.BaseExpr is BoogieIdentifierExpr)
                return (BoogieIdentifierExpr) select.BaseExpr;

            return findOutermostIdentifierExpr((BoogieMapSelect) select.BaseExpr);
        }

        private void calculateFlattenCmdLists(List<BoogieImplementation> impls)
        {
            foreach (var impl in impls)
            {
                List<BoogieCmd> flattenedCmdList = new List<BoogieCmd>();
                flattenedCmdLists.Add(impl.Name, flattenedCmdList);
                foreach (var cmd in impl.StructuredStmts.BigBlocks[0].SimpleCmds)
                {
                    collectBoogieCmds(cmd, flattenedCmdList);
                }
            }
        }

        private void collectBoogieCmds(BoogieCmd cmd, List<BoogieCmd> cmds)
        {
            if (cmd is BoogieStructuredCmd)
            {
                if (cmd is BoogieIfCmd)
                {
                    BoogieIfCmd ifCmd = (BoogieIfCmd) cmd;
                    foreach (var c in ifCmd.ThenBody.BigBlocks[0].SimpleCmds)
                    {
                        collectBoogieCmds(c, cmds);
                    }

                    if (ifCmd.ElseBody != null)
                    {
                        foreach (var c in ifCmd.ElseBody.BigBlocks[0].SimpleCmds)
                        {
                            collectBoogieCmds(c, cmds);
                        }
                    }
                }
                else if (cmd is BoogieWhileCmd)
                {
                    BoogieWhileCmd whileCmd = (BoogieWhileCmd) cmd;
                    foreach (var c in whileCmd.Body.BigBlocks[0].SimpleCmds)
                    {
                        collectBoogieCmds(c, cmds);
                    }
                }
                else
                {
                    cmds.Add(cmd);
                }
            }
            else
            {
                cmds.Add(cmd);
            }
        }
    }
}