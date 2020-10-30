# Generated from .\CelestialParser.g4 by ANTLR 4.7.2
from antlr4 import *
if __name__ is not None and "." in __name__:
    from .CelestialParser import CelestialParser
else:
    from CelestialParser import CelestialParser

# This class defines a complete generic visitor for a parse tree produced by CelestialParser.

class CelestialParserVisitor(ParseTreeVisitor):

    # Visit a parse tree produced by CelestialParser#program.
    def visitProgram(self, ctx:CelestialParser.ProgramContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#iden.
    def visitIden(self, ctx:CelestialParser.IdenContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#datatype.
    def visitDatatype(self, ctx:CelestialParser.DatatypeContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#idenTypeList.
    def visitIdenTypeList(self, ctx:CelestialParser.IdenTypeListContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#idenType.
    def visitIdenType(self, ctx:CelestialParser.IdenTypeContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#contractDecl.
    def visitContractDecl(self, ctx:CelestialParser.ContractDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#contractBody.
    def visitContractBody(self, ctx:CelestialParser.ContractBodyContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#contractContents.
    def visitContractContents(self, ctx:CelestialParser.ContractContentsContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#FDecl.
    def visitFDecl(self, ctx:CelestialParser.FDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#funParamList.
    def visitFunParamList(self, ctx:CelestialParser.FunParamListContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#funParam.
    def visitFunParam(self, ctx:CelestialParser.FunParamContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#functionBody.
    def visitFunctionBody(self, ctx:CelestialParser.FunctionBodyContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#IDecl.
    def visitIDecl(self, ctx:CelestialParser.IDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#eventDecl.
    def visitEventDecl(self, ctx:CelestialParser.EventDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#KDecl.
    def visitKDecl(self, ctx:CelestialParser.KDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#MDecl.
    def visitMDecl(self, ctx:CelestialParser.MDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#methodParamList.
    def visitMethodParamList(self, ctx:CelestialParser.MethodParamListContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#methodParam.
    def visitMethodParam(self, ctx:CelestialParser.MethodParamContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#methodBody.
    def visitMethodBody(self, ctx:CelestialParser.MethodBodyContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#varDecl.
    def visitVarDecl(self, ctx:CelestialParser.VarDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#loopVarDecl.
    def visitLoopVarDecl(self, ctx:CelestialParser.LoopVarDeclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#CompoundStmt.
    def visitCompoundStmt(self, ctx:CelestialParser.CompoundStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#PushStmt.
    def visitPushStmt(self, ctx:CelestialParser.PushStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#DeleteStmt.
    def visitDeleteStmt(self, ctx:CelestialParser.DeleteStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#AssertStmt.
    def visitAssertStmt(self, ctx:CelestialParser.AssertStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#PrintStmt.
    def visitPrintStmt(self, ctx:CelestialParser.PrintStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#ReturnStmt.
    def visitReturnStmt(self, ctx:CelestialParser.ReturnStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#AssignStmt.
    def visitAssignStmt(self, ctx:CelestialParser.AssignStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#IfStmt.
    def visitIfStmt(self, ctx:CelestialParser.IfStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#CreateStmt.
    def visitCreateStmt(self, ctx:CelestialParser.CreateStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#ForStmt.
    def visitForStmt(self, ctx:CelestialParser.ForStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#MethodCallStmt.
    def visitMethodCallStmt(self, ctx:CelestialParser.MethodCallStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#SendStmt.
    def visitSendStmt(self, ctx:CelestialParser.SendStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#RevertStmt.
    def visitRevertStmt(self, ctx:CelestialParser.RevertStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#NamedTupleLvalue.
    def visitNamedTupleLvalue(self, ctx:CelestialParser.NamedTupleLvalueContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#MapOrArrayLvalue.
    def visitMapOrArrayLvalue(self, ctx:CelestialParser.MapOrArrayLvalueContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#VarLvalue.
    def visitVarLvalue(self, ctx:CelestialParser.VarLvalueContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#CreateExpr.
    def visitCreateExpr(self, ctx:CelestialParser.CreateExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#ArrayMapAccessExpr.
    def visitArrayMapAccessExpr(self, ctx:CelestialParser.ArrayMapAccessExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#PrimitiveExpr.
    def visitPrimitiveExpr(self, ctx:CelestialParser.PrimitiveExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#CastExpr.
    def visitCastExpr(self, ctx:CelestialParser.CastExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#FieldAccessExpr.
    def visitFieldAccessExpr(self, ctx:CelestialParser.FieldAccessExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#BinExpr.
    def visitBinExpr(self, ctx:CelestialParser.BinExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#ParenExpr.
    def visitParenExpr(self, ctx:CelestialParser.ParenExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#ArrayLengthExpr.
    def visitArrayLengthExpr(self, ctx:CelestialParser.ArrayLengthExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#UnaryExpr.
    def visitUnaryExpr(self, ctx:CelestialParser.UnaryExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#MethodCallExpr.
    def visitMethodCallExpr(self, ctx:CelestialParser.MethodCallExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#IdenPrimitive.
    def visitIdenPrimitive(self, ctx:CelestialParser.IdenPrimitiveContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#BoolPrimitive.
    def visitBoolPrimitive(self, ctx:CelestialParser.BoolPrimitiveContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#IntPrimitive.
    def visitIntPrimitive(self, ctx:CelestialParser.IntPrimitiveContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#NullPrimitive.
    def visitNullPrimitive(self, ctx:CelestialParser.NullPrimitiveContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#ThisPrimitive.
    def visitThisPrimitive(self, ctx:CelestialParser.ThisPrimitiveContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#unnamedTupleBody.
    def visitUnnamedTupleBody(self, ctx:CelestialParser.UnnamedTupleBodyContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#namedTupleBody.
    def visitNamedTupleBody(self, ctx:CelestialParser.NamedTupleBodyContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#rvalueList.
    def visitRvalueList(self, ctx:CelestialParser.RvalueListContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by CelestialParser#rvalue.
    def visitRvalue(self, ctx:CelestialParser.RvalueContext):
        return self.visitChildren(ctx)



del CelestialParser