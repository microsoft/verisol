import sys
from antlr4 import *
from CelestialListener import CelestialListener
from CelestialParser import CelestialParser
form CelestialParserVisitor import CelestialParserVisitor

class MyVisitor(CelestialParserVisitor):
    # def visitContractDecl(self, ctx:CelestialParser.ContractDeclContext):

    #     return self.visitChildren(ctx)

    def visitContractBody(self, ctx:CelestialParser.ContractBodyContext):
        for c in ctx.contractContents():
            print (c);