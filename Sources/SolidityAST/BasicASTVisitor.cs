// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    public class BasicASTVisitor : IASTVisitor
    {
        protected virtual bool CommonVisit(ASTNode node)
        {
            return true;
        }

        protected virtual void CommonEndVisit(ASTNode node)
        {
            // left empty
        }

        public virtual bool Visit(SourceUnitList node) { return CommonVisit(node); }

        public virtual bool Visit(SourceUnit node) { return CommonVisit(node); }

        public virtual bool Visit(PragmaDirective node) { return CommonVisit(node); }

        public virtual bool Visit(UsingForDirective node) { return CommonVisit(node); }

        public virtual bool Visit(ImportDirective node) { return CommonVisit(node); }

        public virtual bool Visit(ContractDefinition node) { return CommonVisit(node); }

        public virtual bool Visit(InheritanceSpecifier node) { return CommonVisit(node); }

        public virtual bool Visit(FunctionDefinition node) { return CommonVisit(node); }

        public virtual bool Visit(ParameterList node) { return CommonVisit(node); }

        public virtual bool Visit(ModifierDefinition node) { return CommonVisit(node); }

        public virtual bool Visit(ModifierInvocation node) { return CommonVisit(node); }

        public virtual bool Visit(EventDefinition node) { return CommonVisit(node); }

        public virtual bool Visit(StructDefinition node) { return CommonVisit(node); }

        public virtual bool Visit(EnumDefinition node) { return CommonVisit(node); }

        public virtual bool Visit(EnumValue node) { return CommonVisit(node); }

        public virtual bool Visit(VariableDeclaration node) { return CommonVisit(node); }

        public virtual bool Visit(ElementaryTypeName node) { return CommonVisit(node); }

        public virtual bool Visit(UserDefinedTypeName node) { return CommonVisit(node); }

        public virtual bool Visit(Mapping node) { return CommonVisit(node); }

        public virtual bool Visit(ArrayTypeName node) { return CommonVisit(node); }

        public virtual bool Visit(Block node) { return CommonVisit(node); }

        public virtual bool Visit(PlaceholderStatement node) { return CommonVisit(node); }

        public virtual bool Visit(IfStatement node) { return CommonVisit(node); }

        public virtual bool Visit(WhileStatement node) { return CommonVisit(node); }

        public virtual bool Visit(DoWhileStatement node) { return CommonVisit(node); }

        public virtual bool Visit(ForStatement node) { return CommonVisit(node); }

        public virtual bool Visit(Continue node) { return CommonVisit(node); }

        public virtual bool Visit(Break node) { return CommonVisit(node); }

        public virtual bool Visit(Return node) { return CommonVisit(node); }

        public virtual bool Visit(Throw node) { return CommonVisit(node); }

        public virtual bool Visit(EmitStatement node) { return CommonVisit(node); }

        public virtual bool Visit(VariableDeclarationStatement node) { return CommonVisit(node); }

        public virtual bool Visit(InlineAssembly node) { return CommonVisit(node); }

        public virtual bool Visit(ExpressionStatement node) { return CommonVisit(node); }

        public virtual bool Visit(Literal node) { return CommonVisit(node); }

        public virtual bool Visit(Identifier node) { return CommonVisit(node); }

        public virtual bool Visit(ElementaryTypeNameExpression node) { return CommonVisit(node); }

        public virtual bool Visit(UnaryOperation node) { return CommonVisit(node); }

        public virtual bool Visit(BinaryOperation node) { return CommonVisit(node); }

        public virtual bool Visit(Conditional node) { return CommonVisit(node); }

        public virtual bool Visit(Assignment node) { return CommonVisit(node); }

        public virtual bool Visit(TupleExpression node) { return CommonVisit(node); }

        public virtual bool Visit(FunctionCall node) { return CommonVisit(node); }

        public virtual bool Visit(NewExpression node) { return CommonVisit(node); }

        public virtual bool Visit(MemberAccess node) { return CommonVisit(node); }

        public virtual bool Visit(IndexAccess node) { return CommonVisit(node); }


        public virtual void EndVisit(SourceUnitList node) { CommonEndVisit(node); }

        public virtual void EndVisit(SourceUnit node) { CommonEndVisit(node); }

        public virtual void EndVisit(PragmaDirective node) { CommonEndVisit(node); }

        public virtual void EndVisit(UsingForDirective node) { CommonEndVisit(node); }

        public virtual void EndVisit(ImportDirective node) { CommonEndVisit(node); }

        public virtual void EndVisit(ContractDefinition node) { CommonEndVisit(node); }

        public virtual void EndVisit(InheritanceSpecifier node) { CommonEndVisit(node); }

        public virtual void EndVisit(FunctionDefinition node) { CommonEndVisit(node); }

        public virtual void EndVisit(ParameterList node) { CommonEndVisit(node); }

        public virtual void EndVisit(ModifierDefinition node) { CommonEndVisit(node); }

        public virtual void EndVisit(ModifierInvocation node) { CommonEndVisit(node); }

        public virtual void EndVisit(EventDefinition node) { CommonEndVisit(node); }

        public virtual void EndVisit(StructDefinition node) { CommonEndVisit(node); }

        public virtual void EndVisit(EnumDefinition node) { CommonEndVisit(node); }

        public virtual void EndVisit(EnumValue node) { CommonEndVisit(node); }

        public virtual void EndVisit(VariableDeclaration node) { CommonEndVisit(node); }

        public virtual void EndVisit(ElementaryTypeName node) { CommonEndVisit(node); }

        public virtual void EndVisit(UserDefinedTypeName node) { CommonEndVisit(node); }

        public virtual void EndVisit(Mapping node) { CommonEndVisit(node); }

        public virtual void EndVisit(ArrayTypeName node) { CommonEndVisit(node); }

        public virtual void EndVisit(Block node) { CommonEndVisit(node); }

        public virtual void EndVisit(PlaceholderStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(IfStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(WhileStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(DoWhileStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(ForStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(Continue node) { CommonEndVisit(node); }

        public virtual void EndVisit(Break node) { CommonEndVisit(node); }

        public virtual void EndVisit(Return node) { CommonEndVisit(node); }

        public virtual void EndVisit(Throw node) { CommonEndVisit(node); }

        public virtual void EndVisit(EmitStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(VariableDeclarationStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(InlineAssembly node) { CommonEndVisit(node); }

        public virtual void EndVisit(ExpressionStatement node) { CommonEndVisit(node); }

        public virtual void EndVisit(Literal node) { CommonEndVisit(node); }

        public virtual void EndVisit(Identifier node) { CommonEndVisit(node); }

        public virtual void EndVisit(ElementaryTypeNameExpression node) { CommonEndVisit(node); }

        public virtual void EndVisit(UnaryOperation node) { CommonEndVisit(node); }

        public virtual void EndVisit(BinaryOperation node) { CommonEndVisit(node); }

        public virtual void EndVisit(Conditional node) { CommonEndVisit(node); }

        public virtual void EndVisit(Assignment node) { CommonEndVisit(node); }

        public virtual void EndVisit(TupleExpression node) { CommonEndVisit(node); }

        public virtual void EndVisit(FunctionCall node) { CommonEndVisit(node); }

        public virtual void EndVisit(NewExpression node) { CommonEndVisit(node); }

        public virtual void EndVisit(MemberAccess node) { CommonEndVisit(node); }

        public virtual void EndVisit(IndexAccess node) { CommonEndVisit(node); }
    }
 }
