using System;
using System.Collections.Generic;
using SolidityAST;

namespace SolidityAnalysis
{
    
    public class AliasAnalysis : BasicASTVisitor
    {
        private HashSet<VariableDeclaration> results;
        private Dictionary<String, StructDefinition> nameToStruct;
        private AST solidityAST;
        private HashSet<Tuple<string, string>> ignoredMethods;
        private String entryPoint;

        class DeclarationFinder : BasicASTVisitor
        {
            private IndexAccess start;
            private int numAccesses;
            private VariableDeclaration result;
            private AST solidityAST;

            public DeclarationFinder(IndexAccess access, AST solidityAst)
            {
                this.solidityAST = solidityAst;
                this.start = access;
                this.numAccesses = 1;
                start.BaseExpression.Accept(this);
            }
            public override bool Visit(IndexAccess access)
            {
                numAccesses++;
                access.BaseExpression.Accept(this);
                return false;
            }

            public override bool Visit(Identifier ident)
            {
                ASTNode node = solidityAST.GetASTNodeByID(ident.ReferencedDeclaration);
                if (node is VariableDeclaration decl)
                {
                    result = decl;
                    return false;
                }

                throw new Exception("AliasAnalysis Exception: Expected identifier declaration to be of VariableDeclaration type, not " + node.GetType());
            }

            public VariableDeclaration getDecl()
            {
                return result;
            }

            public int getNumAccesses()
            {
                return numAccesses;
            }
        }

        public AliasAnalysis(AST solidityAST, HashSet<Tuple<string, string>> ignoredMethods, String entryPointContract = "")
        {
            this.solidityAST = solidityAST;
            this.ignoredMethods = ignoredMethods;
            this.entryPoint = entryPointContract;
            this.nameToStruct = new Dictionary<string, StructDefinition>();
            this.results = null;
        }
        
        public HashSet<VariableDeclaration> getResults()
        {
            if (results == null)
            {
                runAnalysis();
            }
            
            return results;
        }

        public void runAnalysis()
        {
            results = new HashSet<VariableDeclaration>();
            solidityAST.GetSourceUnits().Accept(this);
        }

        public override bool Visit(StructDefinition def)
        {
            nameToStruct.Add(def.Name, def);
            return true;
        }

        public override bool Visit(VariableDeclaration decl)
        {
            TypeName type = decl.TypeName;
            if (decl.Name == "" || type is ElementaryTypeName || decl.StateVariable == false)
            {
                return false;
            }

            if (type is Mapping || type is ArrayTypeName)
            {
                results.Add(decl);
            }

            /* Can we add in user-defined types?
             else if (type is UserDefinedTypeName userDefined)
            {
                results.Add(decl);
            }*/

            return true;
        }

        public override bool Visit(Identifier ident)
        {
            if (!ident.IsLValue)
            {
                return true;
            }
            
            ASTNode node = solidityAST.GetASTNodeByID(ident.ReferencedDeclaration);
            assert(node is VariableDeclaration,
                "Expected identifier declaration to be of VariableDeclaration type, not " + node.GetType());
            
            VariableDeclaration decl = (VariableDeclaration) node;
            results.Remove(decl);
            return false;
        }

        public TypeName getBaseType(TypeName ty)
        {
            while (ty is Mapping || ty is ArrayTypeName)
            {
                if (ty is Mapping mapping)
                {
                    ty = mapping.ValueType;
                }
                else if (ty is ArrayTypeName arr)
                {
                    ty = arr.BaseType;
                }
            }

            return ty;
        }

        public int getIndexDimSize(TypeName ty)
        {
            int dimSize = 0;
            while (ty is Mapping || ty is ArrayTypeName)
            {
                dimSize++;
                if (ty is Mapping mapping)
                {
                    ty = mapping.ValueType;
                }
                else if (ty is ArrayTypeName arr)
                {
                    ty = arr.BaseType;
                }
            }

            return dimSize;
        }
        
        public override bool Visit(IndexAccess access)
        {
            DeclarationFinder declFinder = new DeclarationFinder(access, solidityAST);
            VariableDeclaration decl = declFinder.getDecl();

            if (results.Contains(decl))
            {
                int numAccesses = declFinder.getNumAccesses();
                int numIndexDims = getIndexDimSize(decl.TypeName);

                if (numIndexDims != numAccesses)
                {
                    results.Remove(decl);
                }
            }

            return false;
        }
        
        /*
         * Guide analysis to statements that can cause aliasing. i.e. rhs expressions, function arguments, etc
         */

        public override bool Visit(Assignment node)
        {
            if (node.Operator == "=")
            {
                node.RightHandSide.Accept(this);
            }

            return false;
        }
        
        public override bool Visit(FunctionDefinition node)
        {
            return node.Implemented;
        }

        /*
         * Nodes that cannot cause aliasing
         */

        public override bool Visit(ArrayTypeName node)
        {
            return false;
        }

        public override bool Visit(Break node)
        {
            return false;
        }

        public override bool Visit(BinaryOperation node)
        {
            return false;
        }

        public override bool Visit(EmitStatement node)
        {
            return false;
        }

        public override bool Visit(UnaryOperation node)
        {
            return false;
        }

        public override bool Visit(EventDefinition node)
        {
            return false;
        }

        public override bool Visit(UsingForDirective node)
        {
            return false;
        }

        public override bool Visit(PragmaDirective node)
        {
            return false;
        }

        public override bool Visit(ImportDirective node)
        {
            return false;
        }

        public override bool Visit(Continue node)
        {
            return false;
        }

        public void assert(bool cond, String msg)
        {
            if (!cond)
            {
                throw new Exception("AliasAnalysis Exception: " + msg);
            }
        }
    }
}