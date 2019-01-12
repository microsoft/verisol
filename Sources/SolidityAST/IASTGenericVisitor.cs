// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace SolidityAST
{
    public interface IASTGenericVisitor<T>
    {
        T Visit(SourceUnitList node);
        T Visit(SourceUnit node);
        T Visit(PragmaDirective node);
        T Visit(UsingForDirective node);
        T Visit(ImportDirective node);
        T Visit(ContractDefinition node);
        T Visit(InheritanceSpecifier node);
        T Visit(FunctionDefinition node);
        T Visit(ParameterList node);
        T Visit(ModifierDefinition node);
        T Visit(ModifierInvocation node);
        T Visit(EventDefinition node);
        T Visit(StructDefinition node);
        T Visit(EnumDefinition node);
        T Visit(EnumValue node);
        T Visit(VariableDeclaration node);
        T Visit(ElementaryTypeName node);
        T Visit(UserDefinedTypeName node);
        T Visit(Mapping node);
        T Visit(ArrayTypeName node);
        T Visit(Block node);
        T Visit(PlaceholderStatement node);
        T Visit(IfStatement node);
        T Visit(WhileStatement node);
        T Visit(DoWhileStatement node);
        T Visit(ForStatement node);
        T Visit(Continue node);
        T Visit(Break node);
        T Visit(Return node);
        T Visit(Throw node);
        T Visit(EmitStatement node);
        T Visit(VariableDeclarationStatement node);
        T Visit(InlineAssembly node);
        T Visit(ExpressionStatement node);
        T Visit(Literal node);
        T Visit(Identifier node);
        T Visit(ElementaryTypeNameExpression node);
        T Visit(UnaryOperation node);
        T Visit(BinaryOperation node);
        T Visit(Conditional node);
        T Visit(Assignment node);
        T Visit(TupleExpression node);
        T Visit(FunctionCall node);
        T Visit(NewExpression node);
        T Visit(MemberAccess node);
        T Visit(IndexAccess node);
    }
}
