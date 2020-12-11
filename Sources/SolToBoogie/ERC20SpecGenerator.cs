using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using SolidityAST;

namespace SolToBoogie
{
    public class ERC20SpecGenerator
    {
        private TranslatorContext context;
        private AST solidityAST;
        private List<String> ERC20Vars = new List<string>() {"totalSupply", "balanceOf", "allowance"};
        private List<String> ERC20fns = new List<string>() {"totalSupply", "balanceOf", "allowance", "approve", "transfer", "transferFrom"};
        private ContractDefinition entryContract;
        private Dictionary<String, VariableDeclaration> varDecls;
        private Dictionary<String, ContractDefinition> fnContracts;
        private Dictionary<VariableDeclaration, int> declToContractInd;
        private List<VariableDeclaration> otherVars;
        
        public ERC20SpecGenerator(TranslatorContext context, AST solidityAST, String entryPoint)
        {
            this.context = context;
            this.solidityAST = solidityAST;
            varDecls = new Dictionary<string, VariableDeclaration>();
            fnContracts = new Dictionary<string, ContractDefinition>();
            otherVars = new List<VariableDeclaration>();
            declToContractInd = new Dictionary<VariableDeclaration, int>();
            
            foreach (ContractDefinition def in context.ContractDefinitions)
            {
                if (def.Name.Equals(entryPoint))
                {
                    entryContract = def;
                }
            }

            int contractInd = 0;
            foreach (int id in entryContract.LinearizedBaseContracts)
            {
                contractInd++;
                ContractDefinition contract = context.GetASTNodeById(id) as ContractDefinition;

                if (context.ContractToStateVarsMap.ContainsKey(contract))
                {
                    otherVars.AddRange(context.ContractToStateVarsMap[contract]);
                    foreach (VariableDeclaration decl in context.ContractToStateVarsMap[contract])
                    {
                        declToContractInd[decl] = contractInd;
                    }
                }

                if (!context.ContractToFunctionsMap.ContainsKey(contract))
                {
                    continue;
                }

                HashSet<FunctionDefinition> fnDefs = context.ContractToFunctionsMap[contract];

                foreach (FunctionDefinition fnDef in fnDefs)
                {
                    if (ERC20fns.Contains(fnDef.Name) && !fnContracts.ContainsKey(fnDef.Name))
                    {
                        fnContracts[fnDef.Name] = contract;
                    }

                    if (ERC20Vars.Contains(fnDef.Name) && !varDecls.ContainsKey(fnDef.Name))
                    {
                        ReturnDeclarationFinder declFinder = new ReturnDeclarationFinder(context);
                        VariableDeclaration decl = declFinder.findDecl(contract, fnDef);
                                                
                        if (decl != null)
                        {
                            varDecls[fnDef.Name] = decl;
                        }
                    }
                }
            }

            foreach (VariableDeclaration decl in varDecls.Values)
            {
                otherVars.Remove(decl);
            }

            otherVars.RemoveAll(v => v.Constant);
        }

        private int CompareVars(VariableDeclaration v1, VariableDeclaration v2)
        {
            if (declToContractInd[v1] != declToContractInd[v2])
            {
                return declToContractInd[v1] > declToContractInd[v2] ? -1 : 1;
            }
            
            string[] v1Tokens = v1.Src.Split(':');
            int v1Pos = int.Parse(v1Tokens[0]);
            string[] v2Tokens = v2.Src.Split(':');
            int v2Pos = int.Parse(v2Tokens[0]);
            
            if (v1Pos != v2Pos)
            {
                return v1Pos < v2Pos ? -1 : 1;
            }

            throw new Exception("Two variables at the same position");
            /*int v1No = context.ASTNodeToSourceLineNumberMap[v1];
            int v2No = context.ASTNodeToSourceLineNumberMap[v2];

            if (v1No != v2No)
            {
                return v1No < v2No ? -1 : 1;
            }
            
            throw new Exception("Two variables declared on the same line");*/
        }

        public void GenerateSpec()
        {
            List<VariableDeclaration> allVars = new List<VariableDeclaration>(otherVars);
            String filename = context.ASTNodeToSourcePathMap[entryContract];
            StreamWriter writer = new StreamWriter($"{filename.Substring(0, filename.Length - 4)}.config");
            
            string totSupply = varDecls.ContainsKey("totalSupply") ? $"{varDecls["totalSupply"].Name}" : "";
            if (String.IsNullOrEmpty(totSupply))
            {
                Console.WriteLine("Warning: Could not find totalSupply variable");
            }
            else
            {
                allVars.Add(varDecls["totalSupply"]);
            }
            string bal = varDecls.ContainsKey("balanceOf") ? $"{varDecls["balanceOf"].Name}" : "";
            if (String.IsNullOrEmpty(bal))
            {
                Console.WriteLine("Warning: Could not find balance variable");
            }
            else
            {
                allVars.Add(varDecls["balanceOf"]);
            }
            string allowances = varDecls.ContainsKey("allowance") ? $"{varDecls["allowance"].Name}" : "";
            if (String.IsNullOrEmpty(allowances))
            {
                Console.WriteLine("Warning: Could not find allowances variable");
            }
            else
            {
                allVars.Add(varDecls["allowance"]);
            }
            
            allVars.Sort(CompareVars);
            
            string totContract = fnContracts.ContainsKey("totalSupply") ? fnContracts["totalSupply"].Name : "";
            string balContract = fnContracts.ContainsKey("balanceOf") ? fnContracts["balanceOf"].Name : "";
            string allowanceContract = fnContracts.ContainsKey("allowance") ? fnContracts["allowance"].Name : "";
            string approveContract = fnContracts.ContainsKey("approve") ? fnContracts["approve"].Name : "";
            string transferContract = fnContracts.ContainsKey("transfer") ? fnContracts["transfer"].Name : "";
            string transferFromContract =
                fnContracts.ContainsKey("transferFrom") ? fnContracts["transferFrom"].Name : "";
            string extraVars = String.Join(" ", otherVars.Select(v => $"this.{v.Name}"));

            writer.WriteLine($"FILE_NAME={filename}");
            writer.WriteLine($"CONTRACT_NAME={entryContract.Name}");
            writer.WriteLine($"TOTAL={totSupply}");
            writer.WriteLine($"BALANCES={bal}");
            writer.WriteLine($"ALLOWANCES={allowances}");
            writer.WriteLine($"TOT_SUP_CONTRACT={totContract}");
            writer.WriteLine($"BAL_OF_CONTRACT={balContract}");
            writer.WriteLine($"ALLOWANCE_CONTRACT={allowanceContract}");
            writer.WriteLine($"APPROVE_CONTRACT={approveContract}");
            writer.WriteLine($"TRANSFER_CONTRACT={transferContract}");
            writer.WriteLine($"TRANSFER_FROM_CONTRACT={transferFromContract}");
            writer.WriteLine($"EXTRA_VARS=({extraVars})");
            for (int i = 0; i < allVars.Count; i++)
            {
                writer.WriteLine($"{allVars[i].Name}={i}");
            }
            writer.Close();
        }


        private class ReturnDeclarationFinder : BasicASTVisitor
        {
            private VariableDeclaration retDecl;
            private String findVar;
            private TranslatorContext context;
            private ContractDefinition curContract;
            
            public ReturnDeclarationFinder(TranslatorContext context)
            {
                this.context = context;
                retDecl = null;
            }

            public VariableDeclaration findDecl(ContractDefinition curContract, FunctionDefinition def)
            {
                if (def.Body == null)
                {
                    return null;
                }
                
                if (def.ReturnParameters.Parameters.Count != 1)
                {
                    return null;
                }

                if (!String.IsNullOrEmpty(def.ReturnParameters.Parameters[0].Name))
                {
                    findVar = def.ReturnParameters.Parameters[0].Name;
                }

                this.curContract = curContract;
                
                def.Body.Accept(this);
                return retDecl;
            }

            public override bool Visit(ArrayTypeName node)
            {
                return false;
            }

            public override bool Visit(Assignment node)
            {
                if (node.LeftHandSide is Identifier ident)
                {
                    if (ident.Name.Equals(findVar))
                    {
                        node.RightHandSide.Accept(this);
                    }
                }
                return false;
            }

            public override bool Visit(SourceUnit node)
            {
                return false;
            }

            public override bool Visit(BinaryOperation node)
            {
                return false;
            }

            public override bool Visit(Block node)
            {
                for (int i = node.Statements.Count - 1; i >= 0; i--)
                {
                    node.Statements[i].Accept(this);
                }
                return false;
            }

            public override bool Visit(Break node)
            {
                return false;
            }

            public override bool Visit(Conditional node)
            {
                return false;
            }

            public override bool Visit(Continue node)
            {
                return false;
            }

            public override bool Visit(ContractDefinition node)
            {
                return false;
            }

            public override bool Visit(PragmaDirective node)
            {
                return false;
            }

            public override bool Visit(DoWhileStatement node)
            {
                return false;
            }

            public override bool Visit(ElementaryTypeName node)
            {
                return false;
            }

            public override bool Visit(ElementaryTypeNameExpression node)
            {
                return false;
            }

            public override bool Visit(EmitStatement node)
            {
                return false;
            }

            public override bool Visit(EnumDefinition node)
            {
                return false;
            }

            public override bool Visit(UsingForDirective node)
            {
                return false;
            }

            public override bool Visit(ImportDirective node)
            {
                return false;
            }

            public override bool Visit(InheritanceSpecifier node)
            {
                return false;
            }

            public override bool Visit(EnumValue node)
            {
                return false;
            }

            public override bool Visit(EventDefinition node)
            {
                return false;
            }

            public override bool Visit(ExpressionStatement node)
            {
                return false;
            }

            public override bool Visit(ForStatement node)
            {
                return false;
            }

            public FunctionDefinition findFn(string fnName, bool usesSuper)
            {
                List<int> searchContracts = new List<int>(curContract.LinearizedBaseContracts);

                if (!usesSuper)
                {
                    searchContracts.Insert(0, curContract.Id);
                }
                
                foreach (int id in searchContracts)
                {
                    ContractDefinition def = context.IdToNodeMap[id] as ContractDefinition;
                    HashSet < FunctionDefinition > fnDefs = context.ContractToFunctionsMap[def];
                    Dictionary<String, FunctionDefinition> nameToFn = fnDefs.ToDictionary(v => v.Name, v => v);
                    if (nameToFn.ContainsKey(fnName))
                    {
                        return nameToFn[fnName];
                    }
                }

                return null;
            }

            public override bool Visit(FunctionCall node)
            {
                if (node.Expression is Identifier call)
                {
                    FunctionDefinition def = findFn(call.Name, false);
                    if (def != null && def.ReturnParameters.Parameters.Count != 0)
                    {
                        def.Body.Accept(this);
                    }
                }
                else if (node.Expression is MemberAccess access)
                {
                    if (access.Expression is Identifier ident && ident.Name.Equals("super"))
                    {
                        FunctionDefinition def = findFn(access.MemberName, true);
                        if (def != null && def.ReturnParameters.Parameters.Count != 0)
                        {
                            def.Body.Accept(this);
                        }
                    }
                }

                return false;
            }

            public override bool Visit(FunctionDefinition node)
            {
                return false;
            }

            public override bool Visit(Identifier node)
            {
                int id = node.ReferencedDeclaration;
                VariableDeclaration varDecl = context.IdToNodeMap[id] as VariableDeclaration;

                if (varDecl.StateVariable)
                {
                    retDecl = varDecl;
                    return false;
                }
                
                findVar = node.Name;
                return false;
            }

            public override bool Visit(ParameterList node)
            {
                return false;
            }

            public override bool Visit(ModifierDefinition node)
            {
                return false;
            }

            public override bool Visit(IfStatement node)
            {
                return false;
            }

            public override bool Visit(ModifierInvocation node)
            {
                return false;
            }

            public override bool Visit(StructDefinition node)
            {
                return false;
            }

            public override bool Visit(IndexAccess node)
            {
                node.BaseExpression.Accept(this);
                return false;
            }

            public override bool Visit(VariableDeclaration node)
            {
                return false;
            }

            public override bool Visit(UserDefinedTypeName node)
            {
                return false;
            }

            public override bool Visit(InlineAssembly node)
            {
                return false;
            }

            public override bool Visit(Literal node)
            {
                return false;
            }

            public override bool Visit(Mapping node)
            {
                return false;
            }

            public override bool Visit(MemberAccess node)
            {
                return false;
            }

            public override bool Visit(NewExpression node)
            {
                return false;
            }

            public override bool Visit(PlaceholderStatement node)
            {
                return false;
            }

            public override bool Visit(WhileStatement node)
            {
                return false;
            }

            public override bool Visit(Return node)
            {
                node.Expression.Accept(this);
                return false;
            }

            public override bool Visit(SourceUnitList node)
            {
                return false;
            }

            public override bool Visit(Throw node)
            {
                return false;
            }

            public override bool Visit(UnaryOperation node)
            {
                return false;
            }

            public override bool Visit(TupleExpression node)
            {
                return false;
            }

            public override bool Visit(VariableDeclarationStatement node)
            {
                return false;
            }
        }
    }
}
