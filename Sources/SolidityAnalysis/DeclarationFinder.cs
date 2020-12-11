using System;
using SolidityAST;

namespace solidityAnalysis
{
    public class DeclarationFinder : BasicASTVisitor
    {
        private Expression start;
        private int numAccesses;
        private VariableDeclaration result;
        private AST solidityAST;
    
        public DeclarationFinder(Expression access, AST solidityAst)
        {
            this.solidityAST = solidityAst;
            this.start = access;
            this.numAccesses = 0;
            start.Accept(this);
        }
        public override bool Visit(IndexAccess access)
        {
            numAccesses++;
            access.BaseExpression.Accept(this);
            return false;
        }

        public override bool Visit(MemberAccess access)
        {
            if (access.ReferencedDeclaration.HasValue)
            {
                ASTNode node = solidityAST.GetASTNodeByID(access.ReferencedDeclaration.Value);
                if (node is VariableDeclaration decl)
                {
                    result = decl;
                    return false;
                }
                    
                throw new Exception("Analysis Exception: Expected member access declaration to be of VariableDeclaration type, not " + node.GetType());
            }

            access.Expression.Accept(this);
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
    
            throw new Exception("Analysis Exception: Expected identifier declaration to be of VariableDeclaration type, not " + node.GetType());
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
}
