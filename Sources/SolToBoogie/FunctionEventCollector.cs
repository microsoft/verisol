// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Linq;

namespace SolToBoogie
{
    using System.Diagnostics;
    using SolidityAST;

    /**
     * Collect all function/event definitions and put them in the translator context.
     * The result map only contains functions/events directly defined in each contract,
     * but does not contain inherited functions/events.
     */
    public class FunctionEventCollector : BasicASTVisitor
    {
        private TranslatorContext context;

        // current contract that the visitor is visiting
        private ContractDefinition currentContract = null;
        private List<ASTNode> nodesToAdd = null;

        public FunctionEventCollector(TranslatorContext context)
        {
            this.context = context;
        }

        public override bool Visit(ContractDefinition node)
        {
            currentContract = node;
            nodesToAdd = new List<ASTNode>();
            return true;
        }

        public override void EndVisit(ContractDefinition node)
        {
            currentContract.Nodes.AddRange(nodesToAdd);
            currentContract = null;
            nodesToAdd = null;
        }

        public override bool Visit(EventDefinition node)
        {
            Debug.Assert(currentContract != null);
            context.AddEventToContract(currentContract, node);
            return false;
        }

        public override bool Visit(FunctionDefinition node)
        {
            Debug.Assert(currentContract != null);
            context.AddFunctionToContract(currentContract, node);
            return false;
        }

        public FunctionDefinition GenerateGetter(VariableDeclaration varDecl)
        {
            FunctionDefinition fnDef = new FunctionDefinition();
            Block body = new Block();
            body.Scope = 0;
            body.Statements = new List<Statement>();
            ParameterList fnParams = new ParameterList();
            fnParams.Parameters = new List<VariableDeclaration>();
            ParameterList rets = new ParameterList();
            rets.Parameters = new List<VariableDeclaration>();

            fnDef.Visibility = EnumVisibility.PUBLIC;
            fnDef.Implemented = true;
            fnDef.Name = varDecl.Name;
            fnDef.StateMutability = EnumStateMutability.VIEW;
            fnDef.Body = body;
            fnDef.Parameters = fnParams;
            fnDef.ReturnParameters = rets;
            fnDef.Modifiers = new List<ModifierInvocation>();

            TypeName curType = varDecl.TypeName;
            
            Identifier ident = new Identifier();
            ident.Name = varDecl.Name;
            ident.ReferencedDeclaration = varDecl.Id;
            ident.OverloadedDeclarations = new List<int>();
            ident.TypeDescriptions = varDecl.TypeDescriptions;
            
            int id = context.IdToNodeMap.Keys.Max() + 1;

            Expression curExpr = ident;
            List<int> localIds = new List<int>();
            
            while (curType is Mapping || curType is ArrayTypeName)
            {
                VariableDeclaration paramDecl = new VariableDeclaration();
                paramDecl.Name = "arg" + id;
                paramDecl.Visibility = EnumVisibility.DEFAULT;
                paramDecl.StorageLocation = EnumLocation.DEFAULT;
                paramDecl.Id = id;
                context.IdToNodeMap.Add(id, paramDecl);
                fnParams.Parameters.Add(paramDecl);
                
                if (curType is Mapping map)
                {
                    paramDecl.TypeName = map.KeyType;
                    paramDecl.TypeDescriptions = map.KeyType.TypeDescriptions;
                    curType = map.ValueType;
                }
                else if (curType is ArrayTypeName arr)
                {
                    TypeDescription intDescription = new TypeDescription();
                    intDescription.TypeString = "uint256";
                    ElementaryTypeName intTypeName = new ElementaryTypeName();
                    intTypeName.TypeDescriptions = intDescription;
                    paramDecl.TypeName = intTypeName;
                    paramDecl.TypeDescriptions = intDescription;
                    curType = arr.BaseType;
                }
                
                Identifier paramIdent = new Identifier();
                paramIdent.Name = paramDecl.Name;
                paramIdent.OverloadedDeclarations = new List<int>();
                paramIdent.TypeDescriptions = paramDecl.TypeDescriptions;
                paramIdent.ReferencedDeclaration = paramDecl.Id;
                
                IndexAccess access = new IndexAccess();
                access.BaseExpression = curExpr;
                access.IndexExpression = paramIdent;
                access.TypeDescriptions = TransUtils.TypeNameToTypeDescription(curType);

                curExpr = access;
                id++;
            }
            
            VariableDeclaration retVar = new VariableDeclaration();
            retVar = new VariableDeclaration();
            retVar.Name = null;
            retVar.TypeDescriptions = TransUtils.TypeNameToTypeDescription(curType);
            retVar.TypeName = curType;
            rets.Parameters.Add(retVar);

            Return ret = new Return();
            ret.Expression = curExpr;

            context.ASTNodeToSourcePathMap[ret] = context.ASTNodeToSourcePathMap[varDecl];
            context.ASTNodeToSourceLineNumberMap[ret] = context.ASTNodeToSourceLineNumberMap[varDecl];
            context.ASTNodeToSourcePathMap[body] = context.ASTNodeToSourcePathMap[varDecl];
            context.ASTNodeToSourceLineNumberMap[body] = context.ASTNodeToSourceLineNumberMap[varDecl];
            
            body.Statements.Add(ret);

            return fnDef;
        }
        
        public override bool Visit(VariableDeclaration varDecl)
        {
            if (context.TranslateFlags.GenerateGetters && varDecl.Visibility.Equals(EnumVisibility.PUBLIC))
            {
                FunctionDefinition getter = GenerateGetter(varDecl);
                nodesToAdd.Add(getter);
                context.AddFunctionToContract(currentContract, getter);
            }

            return false;
        }
    }
}
