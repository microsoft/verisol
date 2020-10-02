using System;
using System.Collections.Generic;
using SolidityAST;

namespace SolToBoogie
{
    public class FunctionDependencyCollector : BasicASTVisitor
    {
        private TranslatorContext context;
        private HashSet<FunctionDefinition> fnDeps;
        private HashSet<ModifierDefinition> modDeps;
        private Dictionary<String, List<FunctionDefinition>> nameToFn;
        private HashSet<FunctionDefinition> seenFns;
        private FunctionDefinition curFn;
        private LinkedList<FunctionDefinition> worklist;
        
        
        
        public FunctionDependencyCollector(TranslatorContext context, String baseContractName, HashSet<String> fnNames)
        {
            this.context = context;
            nameToFn = new Dictionary<String, List<FunctionDefinition>>();
            fnDeps = new HashSet<FunctionDefinition>();
            modDeps = new HashSet<ModifierDefinition>();
            seenFns = new HashSet<FunctionDefinition>();
            worklist = new LinkedList<FunctionDefinition>();
            ContractDefinition baseContract = null;
            
            foreach (ContractDefinition def in context.ContractDefinitions)
            {
                if (def.Name.Equals(baseContractName))
                {
                    baseContract = def;
                }

                if (context.ContractToFunctionsMap.ContainsKey(def))
                {
                    foreach (FunctionDefinition fnDef in context.ContractToFunctionsMap[def])
                    {
                        if (nameToFn.ContainsKey(fnDef.Name))
                        {
                            nameToFn[fnDef.Name].Add(fnDef);
                        }
                        else
                        {
                            nameToFn[fnDef.Name] = new List<FunctionDefinition>() {fnDef};
                        }
                    }
                }
            }

            foreach (FunctionDefinition fnDef in context.GetVisibleFunctionsByContract(baseContract))
            {
                if (fnNames.Contains(fnDef.Name))
                {
                    worklist.AddLast(fnDef);
                    seenFns.Add(fnDef);
                }
            }

            while (worklist.Count != 0)
            {
                curFn = worklist.First.Value;
                worklist.RemoveFirst();
                fnDeps.Add(curFn);

                if (curFn.Modifiers != null)
                {
                    foreach (ModifierInvocation mod in curFn.Modifiers)
                    {
                        int id = mod.ModifierName.ReferencedDeclaration;
                        ASTNode node = context.GetASTNodeById(id);

                        if (node is ModifierDefinition modDef)
                        {
                            if (!modDeps.Contains(modDef))
                            {
                                modDeps.Add(modDef);
                                modDef.Body.Accept(this);
                            }
                        }
                        else
                        {
                            throw new Exception("Modifier id does not reference a modifier declaration");
                        }
                    }
                }
                
                if (curFn.Body != null)
                {
                    curFn.Body.Accept(this);
                }
            }
        }

        public bool IsDependent(FunctionDefinition dep)
        {
            return fnDeps.Contains(dep);
        }

        public HashSet<FunctionDefinition> GetFunctionDeps()
        {
            return fnDeps;
        }

        public HashSet<ModifierDefinition> getModifierDeps()
        {
            return modDeps;
        }

        private bool isBuiltinFn(FunctionCall call)
        {
            return FunctionCallHelper.IsLowLevelCall(call) || FunctionCallHelper.isSend(call) ||
                   FunctionCallHelper.IsAssert(call) || FunctionCallHelper.isDelegateCall(call) ||
                   FunctionCallHelper.IsRequire(call) || FunctionCallHelper.IsRevert(call) ||
                   FunctionCallHelper.IsKeccakFunc(call) || FunctionCallHelper.IsAbiEncodePackedFunc(call) ||
                   FunctionCallHelper.IsTypeCast(call) || FunctionCallHelper.IsBuiltInTransferFunc(TransUtils.GetFuncNameFromFuncCall(call), call);
        }
        
        public override bool Visit(FunctionCall node)
        {
            if (FunctionCallHelper.IsLowLevelCall(node) || FunctionCallHelper.isDelegateCall(node))
            {
                worklist.Clear();
                foreach (FunctionDefinition def in context.FunctionToContractMap.Keys)
                {
                    fnDeps.Add(def);
                }
            }
            else if (isBuiltinFn(node))
            {
                return true;
            }
            else
            {
                string fnName = TransUtils.GetFuncNameFromFuncCall(node);
                if (nameToFn.ContainsKey(fnName))
                {
                    foreach (FunctionDefinition fnDef in nameToFn[fnName])
                    {
                        if (!seenFns.Contains(fnDef))
                        {
                            worklist.AddLast(fnDef);
                            seenFns.Add(fnDef);
                        }
                    }
                }
            }

            return true;
        }
    }
}