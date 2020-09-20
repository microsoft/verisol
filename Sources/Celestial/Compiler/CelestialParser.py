# Generated from .\Compiler\CelestialParser.g4 by ANTLR 4.8
# encoding: utf-8
from antlr4 import *
from io import StringIO
import sys
if sys.version_info[1] > 5:
	from typing import TextIO
else:
	from typing.io import TextIO


def serializedATN():
    with StringIO() as buf:
        buf.write("\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3j")
        buf.write("\u037f\4\2\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7")
        buf.write("\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t\13\4\f\t\f\4\r\t\r\4\16")
        buf.write("\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22\4\23\t\23")
        buf.write("\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31")
        buf.write("\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36")
        buf.write("\4\37\t\37\4 \t \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\3\2\7")
        buf.write("\2L\n\2\f\2\16\2O\13\2\3\2\3\2\3\3\3\3\3\4\3\4\3\4\3\4")
        buf.write("\3\4\3\4\3\4\3\4\3\4\3\4\3\4\3\4\3\4\3\4\5\4c\n\4\3\4")
        buf.write("\3\4\3\4\3\4\3\4\3\4\3\4\3\4\5\4m\n\4\3\4\3\4\3\4\7\4")
        buf.write("r\n\4\f\4\16\4u\13\4\3\5\3\5\3\5\7\5z\n\5\f\5\16\5}\13")
        buf.write("\5\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\b\3\b\6\b\u0089\n")
        buf.write("\b\r\b\16\b\u008a\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\t\3\t")
        buf.write("\3\t\5\t\u0097\n\t\3\n\3\n\3\n\3\n\3\n\3\n\7\n\u009f\n")
        buf.write("\n\f\n\16\n\u00a2\13\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13")
        buf.write("\3\13\3\13\3\13\3\13\3\13\7\13\u00b0\n\13\f\13\16\13\u00b3")
        buf.write("\13\13\3\13\3\13\3\f\3\f\3\f\3\f\5\f\u00bb\n\f\3\f\3\f")
        buf.write("\3\f\3\r\3\r\3\r\7\r\u00c3\n\r\f\r\16\r\u00c6\13\r\3\16")
        buf.write("\3\16\3\16\3\17\3\17\3\17\3\17\3\20\3\20\3\20\3\20\3\21")
        buf.write("\3\21\3\21\3\21\3\22\3\22\3\22\3\22\3\22\3\22\7\22\u00dd")
        buf.write("\n\22\f\22\16\22\u00e0\13\22\5\22\u00e2\n\22\3\22\3\22")
        buf.write("\3\22\3\23\3\23\3\23\5\23\u00ea\n\23\3\23\3\23\5\23\u00ee")
        buf.write("\n\23\3\23\3\23\3\23\3\23\5\23\u00f4\n\23\3\23\5\23\u00f7")
        buf.write("\n\23\3\23\3\23\3\23\5\23\u00fc\n\23\3\23\5\23\u00ff\n")
        buf.write("\23\3\23\3\23\3\24\3\24\5\24\u0105\n\24\3\24\3\24\5\24")
        buf.write("\u0109\n\24\3\24\5\24\u010c\n\24\3\24\5\24\u010f\n\24")
        buf.write("\3\24\3\24\5\24\u0113\n\24\3\24\5\24\u0116\n\24\3\24\5")
        buf.write("\24\u0119\n\24\3\24\3\24\5\24\u011d\n\24\3\24\3\24\5\24")
        buf.write("\u0121\n\24\3\24\3\24\5\24\u0125\n\24\3\24\3\24\5\24\u0129")
        buf.write("\n\24\3\24\3\24\5\24\u012d\n\24\3\24\5\24\u0130\n\24\3")
        buf.write("\24\5\24\u0133\n\24\3\24\3\24\5\24\u0137\n\24\3\24\3\24")
        buf.write("\5\24\u013b\n\24\3\24\3\24\5\24\u013f\n\24\3\24\3\24\5")
        buf.write("\24\u0143\n\24\3\24\5\24\u0146\n\24\3\24\5\24\u0149\n")
        buf.write("\24\5\24\u014b\n\24\3\25\3\25\3\25\3\25\5\25\u0151\n\25")
        buf.write("\3\25\3\25\5\25\u0155\n\25\3\25\3\25\3\25\3\25\5\25\u015b")
        buf.write("\n\25\3\25\5\25\u015e\n\25\3\25\3\25\3\25\5\25\u0163\n")
        buf.write("\25\3\25\5\25\u0166\n\25\3\25\3\25\3\25\3\25\5\25\u016c")
        buf.write("\n\25\3\25\3\25\5\25\u0170\n\25\3\25\3\25\3\26\3\26\3")
        buf.write("\26\7\26\u0177\n\26\f\26\16\26\u017a\13\26\3\27\3\27\3")
        buf.write("\27\3\30\3\30\3\30\7\30\u0182\n\30\f\30\16\30\u0185\13")
        buf.write("\30\3\30\3\30\3\30\3\31\3\31\5\31\u018c\n\31\3\31\3\31")
        buf.write("\3\32\3\32\3\32\3\32\5\32\u0194\n\32\3\32\3\32\3\33\3")
        buf.write("\33\3\33\3\33\3\33\3\33\3\33\3\33\3\33\5\33\u01a1\n\33")
        buf.write("\3\34\3\34\7\34\u01a5\n\34\f\34\16\34\u01a8\13\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u01bf")
        buf.write("\n\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u01c8\n")
        buf.write("\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u01d2")
        buf.write("\n\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\5\34\u01f9\n\34\3\34\3\34\3")
        buf.write("\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u0205\n\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\5\34\u0215\n\34\3\34\3\34\3\34\3\34\3")
        buf.write("\34\7\34\u021c\n\34\f\34\16\34\u021f\13\34\3\34\3\34\3")
        buf.write("\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u022c")
        buf.write("\n\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\7\34\u0244\n\34\f\34\16\34\u0247\13\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u0251\n\34\3\34\3")
        buf.write("\34\5\34\u0255\n\34\3\35\3\35\3\35\3\36\3\36\3\36\3\36")
        buf.write("\3\36\3\36\3\36\3\36\3\36\3\36\3\36\7\36\u0265\n\36\f")
        buf.write("\36\16\36\u0268\13\36\3\37\3\37\3\37\3\37\3\37\3\37\3")
        buf.write("\37\3\37\7\37\u0272\n\37\f\37\16\37\u0275\13\37\3\37\3")
        buf.write("\37\3\37\3\37\3\37\3\37\3\37\3\37\3\37\3\37\5\37\u0281")
        buf.write("\n\37\3 \3 \3 \3 \3 \3 \3 \3 \3 \5 \u028c\n \3 \3 \3 ")
        buf.write("\3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \3 \3 \3 \3 \5 \u02ce\n \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \5 \u02da\n \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \7 \u02f1\n \f \16 \u02f4")
        buf.write("\13 \3 \3 \3 \3 \5 \u02fa\n \3 \3 \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3")
        buf.write(" \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \3 \7 \u0326")
        buf.write("\n \f \16 \u0329\13 \7 \u032b\n \f \16 \u032e\13 \3!\3")
        buf.write("!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3")
        buf.write("!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\3!\5!\u0354")
        buf.write("\n!\3\"\3\"\3\"\3\"\3\"\3\"\6\"\u035c\n\"\r\"\16\"\u035d")
        buf.write("\5\"\u0360\n\"\3#\3#\3#\3#\3#\3#\3#\3#\3#\3#\3#\3#\3#")
        buf.write("\6#\u036f\n#\r#\16#\u0370\5#\u0373\n#\3$\3$\3$\7$\u0378")
        buf.write("\n$\f$\16$\u037b\13$\3%\3%\3%\2\5\6:>&\2\4\6\b\n\f\16")
        buf.write("\20\22\24\26\30\32\34\36 \"$&(*,.\60\62\64\668:<>@BDF")
        buf.write("H\2\t\3\2\61\62\4\2HHYY\3\2Z\\\3\2XY\4\2\37\37PS\3\2N")
        buf.write("O\3\2LM\2\u03f7\2M\3\2\2\2\4R\3\2\2\2\6l\3\2\2\2\bv\3")
        buf.write("\2\2\2\n~\3\2\2\2\f\u0082\3\2\2\2\16\u0086\3\2\2\2\20")
        buf.write("\u0096\3\2\2\2\22\u0098\3\2\2\2\24\u00a5\3\2\2\2\26\u00b6")
        buf.write("\3\2\2\2\30\u00bf\3\2\2\2\32\u00c7\3\2\2\2\34\u00ca\3")
        buf.write("\2\2\2\36\u00ce\3\2\2\2 \u00d2\3\2\2\2\"\u00d6\3\2\2\2")
        buf.write("$\u00e6\3\2\2\2&\u014a\3\2\2\2(\u014c\3\2\2\2*\u0173\3")
        buf.write("\2\2\2,\u017b\3\2\2\2.\u017e\3\2\2\2\60\u0189\3\2\2\2")
        buf.write("\62\u018f\3\2\2\2\64\u01a0\3\2\2\2\66\u0254\3\2\2\28\u0256")
        buf.write("\3\2\2\2:\u0259\3\2\2\2<\u0280\3\2\2\2>\u02f9\3\2\2\2")
        buf.write("@\u0353\3\2\2\2B\u035f\3\2\2\2D\u0372\3\2\2\2F\u0374\3")
        buf.write("\2\2\2H\u037c\3\2\2\2JL\5\f\7\2KJ\3\2\2\2LO\3\2\2\2MK")
        buf.write("\3\2\2\2MN\3\2\2\2NP\3\2\2\2OM\3\2\2\2PQ\7\2\2\3Q\3\3")
        buf.write("\2\2\2RS\7g\2\2S\5\3\2\2\2TU\b\4\1\2UV\7\r\2\2VW\7a\2")
        buf.write("\2WX\5\6\4\2XY\7K\2\2YZ\5\6\4\2Z[\7b\2\2[m\3\2\2\2\\m")
        buf.write("\7\4\2\2]m\7\n\2\2^m\7\b\2\2_m\7\13\2\2`b\7\3\2\2ac\7")
        buf.write(",\2\2ba\3\2\2\2bc\3\2\2\2cm\3\2\2\2dm\7\7\2\2em\7\6\2")
        buf.write("\2fm\5\4\3\2gh\7\t\2\2hi\7R\2\2ij\5\4\3\2jk\7S\2\2km\3")
        buf.write("\2\2\2lT\3\2\2\2l\\\3\2\2\2l]\3\2\2\2l^\3\2\2\2l_\3\2")
        buf.write("\2\2l`\3\2\2\2ld\3\2\2\2le\3\2\2\2lf\3\2\2\2lg\3\2\2\2")
        buf.write("ms\3\2\2\2no\f\r\2\2op\7_\2\2pr\7`\2\2qn\3\2\2\2ru\3\2")
        buf.write("\2\2sq\3\2\2\2st\3\2\2\2t\7\3\2\2\2us\3\2\2\2v{\5\n\6")
        buf.write("\2wx\7d\2\2xz\5\n\6\2yw\3\2\2\2z}\3\2\2\2{y\3\2\2\2{|")
        buf.write("\3\2\2\2|\t\3\2\2\2}{\3\2\2\2~\177\5\4\3\2\177\u0080\7")
        buf.write("f\2\2\u0080\u0081\5\6\4\2\u0081\13\3\2\2\2\u0082\u0083")
        buf.write("\7\f\2\2\u0083\u0084\5\4\3\2\u0084\u0085\5\16\b\2\u0085")
        buf.write("\r\3\2\2\2\u0086\u0088\7]\2\2\u0087\u0089\5\20\t\2\u0088")
        buf.write("\u0087\3\2\2\2\u0089\u008a\3\2\2\2\u008a\u0088\3\2\2\2")
        buf.write("\u008a\u008b\3\2\2\2\u008b\u008c\3\2\2\2\u008c\u008d\7")
        buf.write("^\2\2\u008d\17\3\2\2\2\u008e\u0097\5\62\32\2\u008f\u0097")
        buf.write("\5\22\n\2\u0090\u0097\5\24\13\2\u0091\u0097\5\26\f\2\u0092")
        buf.write("\u0097\5\36\20\2\u0093\u0097\5\"\22\2\u0094\u0097\5$\23")
        buf.write("\2\u0095\u0097\5(\25\2\u0096\u008e\3\2\2\2\u0096\u008f")
        buf.write("\3\2\2\2\u0096\u0090\3\2\2\2\u0096\u0091\3\2\2\2\u0096")
        buf.write("\u0092\3\2\2\2\u0096\u0093\3\2\2\2\u0096\u0094\3\2\2\2")
        buf.write("\u0096\u0095\3\2\2\2\u0097\21\3\2\2\2\u0098\u0099\7\5")
        buf.write("\2\2\u0099\u009a\5\4\3\2\u009a\u009b\7]\2\2\u009b\u00a0")
        buf.write("\5\4\3\2\u009c\u009d\7d\2\2\u009d\u009f\5\4\3\2\u009e")
        buf.write("\u009c\3\2\2\2\u009f\u00a2\3\2\2\2\u00a0\u009e\3\2\2\2")
        buf.write("\u00a0\u00a1\3\2\2\2\u00a1\u00a3\3\2\2\2\u00a2\u00a0\3")
        buf.write("\2\2\2\u00a3\u00a4\7^\2\2\u00a4\23\3\2\2\2\u00a5\u00a6")
        buf.write("\7?\2\2\u00a6\u00a7\5\4\3\2\u00a7\u00a8\7]\2\2\u00a8\u00a9")
        buf.write("\5\6\4\2\u00a9\u00aa\5\4\3\2\u00aa\u00b1\7c\2\2\u00ab")
        buf.write("\u00ac\5\6\4\2\u00ac\u00ad\5\4\3\2\u00ad\u00ae\7c\2\2")
        buf.write("\u00ae\u00b0\3\2\2\2\u00af\u00ab\3\2\2\2\u00b0\u00b3\3")
        buf.write("\2\2\2\u00b1\u00af\3\2\2\2\u00b1\u00b2\3\2\2\2\u00b2\u00b4")
        buf.write("\3\2\2\2\u00b3\u00b1\3\2\2\2\u00b4\u00b5\7^\2\2\u00b5")
        buf.write("\25\3\2\2\2\u00b6\u00b7\7>\2\2\u00b7\u00b8\5\4\3\2\u00b8")
        buf.write("\u00ba\7a\2\2\u00b9\u00bb\5\30\r\2\u00ba\u00b9\3\2\2\2")
        buf.write("\u00ba\u00bb\3\2\2\2\u00bb\u00bc\3\2\2\2\u00bc\u00bd\7")
        buf.write("b\2\2\u00bd\u00be\5\34\17\2\u00be\27\3\2\2\2\u00bf\u00c4")
        buf.write("\5\32\16\2\u00c0\u00c1\7d\2\2\u00c1\u00c3\5\32\16\2\u00c2")
        buf.write("\u00c0\3\2\2\2\u00c3\u00c6\3\2\2\2\u00c4\u00c2\3\2\2\2")
        buf.write("\u00c4\u00c5\3\2\2\2\u00c5\31\3\2\2\2\u00c6\u00c4\3\2")
        buf.write("\2\2\u00c7\u00c8\5\6\4\2\u00c8\u00c9\5\4\3\2\u00c9\33")
        buf.write("\3\2\2\2\u00ca\u00cb\7]\2\2\u00cb\u00cc\5> \2\u00cc\u00cd")
        buf.write("\7^\2\2\u00cd\35\3\2\2\2\u00ce\u00cf\7#\2\2\u00cf\u00d0")
        buf.write("\5\4\3\2\u00d0\u00d1\5 \21\2\u00d1\37\3\2\2\2\u00d2\u00d3")
        buf.write("\7]\2\2\u00d3\u00d4\5> \2\u00d4\u00d5\7^\2\2\u00d5!\3")
        buf.write("\2\2\2\u00d6\u00d7\7\6\2\2\u00d7\u00d8\5\4\3\2\u00d8\u00e1")
        buf.write("\7a\2\2\u00d9\u00de\5\6\4\2\u00da\u00db\7d\2\2\u00db\u00dd")
        buf.write("\5\6\4\2\u00dc\u00da\3\2\2\2\u00dd\u00e0\3\2\2\2\u00de")
        buf.write("\u00dc\3\2\2\2\u00de\u00df\3\2\2\2\u00df\u00e2\3\2\2\2")
        buf.write("\u00e0\u00de\3\2\2\2\u00e1\u00d9\3\2\2\2\u00e1\u00e2\3")
        buf.write("\2\2\2\u00e2\u00e3\3\2\2\2\u00e3\u00e4\7b\2\2\u00e4\u00e5")
        buf.write("\7c\2\2\u00e5#\3\2\2\2\u00e6\u00e7\7\22\2\2\u00e7\u00e9")
        buf.write("\7a\2\2\u00e8\u00ea\5*\26\2\u00e9\u00e8\3\2\2\2\u00e9")
        buf.write("\u00ea\3\2\2\2\u00ea\u00eb\3\2\2\2\u00eb\u00ed\7b\2\2")
        buf.write("\u00ec\u00ee\t\2\2\2\u00ed\u00ec\3\2\2\2\u00ed\u00ee\3")
        buf.write("\2\2\2\u00ee\u00ef\3\2\2\2\u00ef\u00f6\5&\24\2\u00f0\u00f1")
        buf.write("\7(\2\2\u00f1\u00f3\7_\2\2\u00f2\u00f4\5F$\2\u00f3\u00f2")
        buf.write("\3\2\2\2\u00f3\u00f4\3\2\2\2\u00f4\u00f5\3\2\2\2\u00f5")
        buf.write("\u00f7\7`\2\2\u00f6\u00f0\3\2\2\2\u00f6\u00f7\3\2\2\2")
        buf.write("\u00f7\u00fe\3\2\2\2\u00f8\u00f9\7)\2\2\u00f9\u00fb\7")
        buf.write("_\2\2\u00fa\u00fc\5F$\2\u00fb\u00fa\3\2\2\2\u00fb\u00fc")
        buf.write("\3\2\2\2\u00fc\u00fd\3\2\2\2\u00fd\u00ff\7`\2\2\u00fe")
        buf.write("\u00f8\3\2\2\2\u00fe\u00ff\3\2\2\2\u00ff\u0100\3\2\2\2")
        buf.write("\u0100\u0101\5.\30\2\u0101%\3\2\2\2\u0102\u0103\7/\2\2")
        buf.write("\u0103\u0105\5> \2\u0104\u0102\3\2\2\2\u0104\u0105\3\2")
        buf.write("\2\2\u0105\u0108\3\2\2\2\u0106\u0107\7.\2\2\u0107\u0109")
        buf.write("\5> \2\u0108\u0106\3\2\2\2\u0108\u0109\3\2\2\2\u0109\u010b")
        buf.write("\3\2\2\2\u010a\u010c\7\24\2\2\u010b\u010a\3\2\2\2\u010b")
        buf.write("\u010c\3\2\2\2\u010c\u010e\3\2\2\2\u010d\u010f\7\25\2")
        buf.write("\2\u010e\u010d\3\2\2\2\u010e\u010f\3\2\2\2\u010f\u0112")
        buf.write("\3\2\2\2\u0110\u0111\7A\2\2\u0111\u0113\5> \2\u0112\u0110")
        buf.write("\3\2\2\2\u0112\u0113\3\2\2\2\u0113\u014b\3\2\2\2\u0114")
        buf.write("\u0116\7\24\2\2\u0115\u0114\3\2\2\2\u0115\u0116\3\2\2")
        buf.write("\2\u0116\u0118\3\2\2\2\u0117\u0119\7\25\2\2\u0118\u0117")
        buf.write("\3\2\2\2\u0118\u0119\3\2\2\2\u0119\u011c\3\2\2\2\u011a")
        buf.write("\u011b\7/\2\2\u011b\u011d\5> \2\u011c\u011a\3\2\2\2\u011c")
        buf.write("\u011d\3\2\2\2\u011d\u0120\3\2\2\2\u011e\u011f\7.\2\2")
        buf.write("\u011f\u0121\5> \2\u0120\u011e\3\2\2\2\u0120\u0121\3\2")
        buf.write("\2\2\u0121\u0124\3\2\2\2\u0122\u0123\7A\2\2\u0123\u0125")
        buf.write("\5> \2\u0124\u0122\3\2\2\2\u0124\u0125\3\2\2\2\u0125\u014b")
        buf.write("\3\2\2\2\u0126\u0127\7/\2\2\u0127\u0129\5> \2\u0128\u0126")
        buf.write("\3\2\2\2\u0128\u0129\3\2\2\2\u0129\u012c\3\2\2\2\u012a")
        buf.write("\u012b\7A\2\2\u012b\u012d\5> \2\u012c\u012a\3\2\2\2\u012c")
        buf.write("\u012d\3\2\2\2\u012d\u012f\3\2\2\2\u012e\u0130\7\24\2")
        buf.write("\2\u012f\u012e\3\2\2\2\u012f\u0130\3\2\2\2\u0130\u0132")
        buf.write("\3\2\2\2\u0131\u0133\7\25\2\2\u0132\u0131\3\2\2\2\u0132")
        buf.write("\u0133\3\2\2\2\u0133\u0136\3\2\2\2\u0134\u0135\7.\2\2")
        buf.write("\u0135\u0137\5> \2\u0136\u0134\3\2\2\2\u0136\u0137\3\2")
        buf.write("\2\2\u0137\u014b\3\2\2\2\u0138\u0139\7/\2\2\u0139\u013b")
        buf.write("\5> \2\u013a\u0138\3\2\2\2\u013a\u013b\3\2\2\2\u013b\u013e")
        buf.write("\3\2\2\2\u013c\u013d\7A\2\2\u013d\u013f\5> \2\u013e\u013c")
        buf.write("\3\2\2\2\u013e\u013f\3\2\2\2\u013f\u0142\3\2\2\2\u0140")
        buf.write("\u0141\7.\2\2\u0141\u0143\5> \2\u0142\u0140\3\2\2\2\u0142")
        buf.write("\u0143\3\2\2\2\u0143\u0145\3\2\2\2\u0144\u0146\7\24\2")
        buf.write("\2\u0145\u0144\3\2\2\2\u0145\u0146\3\2\2\2\u0146\u0148")
        buf.write("\3\2\2\2\u0147\u0149\7\25\2\2\u0148\u0147\3\2\2\2\u0148")
        buf.write("\u0149\3\2\2\2\u0149\u014b\3\2\2\2\u014a\u0104\3\2\2\2")
        buf.write("\u014a\u0115\3\2\2\2\u014a\u0128\3\2\2\2\u014a\u013a\3")
        buf.write("\2\2\2\u014b\'\3\2\2\2\u014c\u014d\7\35\2\2\u014d\u014e")
        buf.write("\5\4\3\2\u014e\u0150\7a\2\2\u014f\u0151\5*\26\2\u0150")
        buf.write("\u014f\3\2\2\2\u0150\u0151\3\2\2\2\u0151\u0152\3\2\2\2")
        buf.write("\u0152\u0154\7b\2\2\u0153\u0155\t\2\2\2\u0154\u0153\3")
        buf.write("\2\2\2\u0154\u0155\3\2\2\2\u0155\u0156\3\2\2\2\u0156\u015d")
        buf.write("\5&\24\2\u0157\u0158\7(\2\2\u0158\u015a\7_\2\2\u0159\u015b")
        buf.write("\5F$\2\u015a\u0159\3\2\2\2\u015a\u015b\3\2\2\2\u015b\u015c")
        buf.write("\3\2\2\2\u015c\u015e\7`\2\2\u015d\u0157\3\2\2\2\u015d")
        buf.write("\u015e\3\2\2\2\u015e\u0165\3\2\2\2\u015f\u0160\7)\2\2")
        buf.write("\u0160\u0162\7_\2\2\u0161\u0163\5F$\2\u0162\u0161\3\2")
        buf.write("\2\2\u0162\u0163\3\2\2\2\u0163\u0164\3\2\2\2\u0164\u0166")
        buf.write("\7`\2\2\u0165\u015f\3\2\2\2\u0165\u0166\3\2\2\2\u0166")
        buf.write("\u016f\3\2\2\2\u0167\u0168\7\65\2\2\u0168\u0169\7a\2\2")
        buf.write("\u0169\u016b\5\6\4\2\u016a\u016c\5\4\3\2\u016b\u016a\3")
        buf.write("\2\2\2\u016b\u016c\3\2\2\2\u016c\u016d\3\2\2\2\u016d\u016e")
        buf.write("\7b\2\2\u016e\u0170\3\2\2\2\u016f\u0167\3\2\2\2\u016f")
        buf.write("\u0170\3\2\2\2\u0170\u0171\3\2\2\2\u0171\u0172\5.\30\2")
        buf.write("\u0172)\3\2\2\2\u0173\u0178\5,\27\2\u0174\u0175\7d\2\2")
        buf.write("\u0175\u0177\5,\27\2\u0176\u0174\3\2\2\2\u0177\u017a\3")
        buf.write("\2\2\2\u0178\u0176\3\2\2\2\u0178\u0179\3\2\2\2\u0179+")
        buf.write("\3\2\2\2\u017a\u0178\3\2\2\2\u017b\u017c\5\6\4\2\u017c")
        buf.write("\u017d\5\4\3\2\u017d-\3\2\2\2\u017e\u0183\7]\2\2\u017f")
        buf.write("\u0182\5\62\32\2\u0180\u0182\5\66\34\2\u0181\u017f\3\2")
        buf.write("\2\2\u0181\u0180\3\2\2\2\u0182\u0185\3\2\2\2\u0183\u0181")
        buf.write("\3\2\2\2\u0183\u0184\3\2\2\2\u0184\u0186\3\2\2\2\u0185")
        buf.write("\u0183\3\2\2\2\u0186\u0187\5\60\31\2\u0187\u0188\7^\2")
        buf.write("\2\u0188/\3\2\2\2\u0189\u018b\7\64\2\2\u018a\u018c\5>")
        buf.write(" \2\u018b\u018a\3\2\2\2\u018b\u018c\3\2\2\2\u018c\u018d")
        buf.write("\3\2\2\2\u018d\u018e\7c\2\2\u018e\61\3\2\2\2\u018f\u0190")
        buf.write("\5\6\4\2\u0190\u0193\5\4\3\2\u0191\u0192\7U\2\2\u0192")
        buf.write("\u0194\5> \2\u0193\u0191\3\2\2\2\u0193\u0194\3\2\2\2\u0194")
        buf.write("\u0195\3\2\2\2\u0195\u0196\7c\2\2\u0196\63\3\2\2\2\u0197")
        buf.write("\u0198\5\6\4\2\u0198\u0199\5\4\3\2\u0199\u019a\7U\2\2")
        buf.write("\u019a\u019b\5> \2\u019b\u01a1\3\2\2\2\u019c\u019d\5\4")
        buf.write("\3\2\u019d\u019e\7U\2\2\u019e\u019f\5> \2\u019f\u01a1")
        buf.write("\3\2\2\2\u01a0\u0197\3\2\2\2\u01a0\u019c\3\2\2\2\u01a1")
        buf.write("\65\3\2\2\2\u01a2\u01a6\7]\2\2\u01a3\u01a5\5\66\34\2\u01a4")
        buf.write("\u01a3\3\2\2\2\u01a5\u01a8\3\2\2\2\u01a6\u01a4\3\2\2\2")
        buf.write("\u01a6\u01a7\3\2\2\2\u01a7\u01a9\3\2\2\2\u01a8\u01a6\3")
        buf.write("\2\2\2\u01a9\u0255\7^\2\2\u01aa\u01ab\5:\36\2\u01ab\u01ac")
        buf.write("\7e\2\2\u01ac\u01ad\7\63\2\2\u01ad\u01ae\7a\2\2\u01ae")
        buf.write("\u01af\5> \2\u01af\u01b0\7b\2\2\u01b0\u01b1\7c\2\2\u01b1")
        buf.write("\u0255\3\2\2\2\u01b2\u01b3\5:\36\2\u01b3\u01b4\7e\2\2")
        buf.write("\u01b4\u01b5\7-\2\2\u01b5\u01b6\7a\2\2\u01b6\u01b7\7b")
        buf.write("\2\2\u01b7\u01b8\7c\2\2\u01b8\u0255\3\2\2\2\u01b9\u01ba")
        buf.write("\7\27\2\2\u01ba\u01bb\7a\2\2\u01bb\u01be\5:\36\2\u01bc")
        buf.write("\u01bd\7d\2\2\u01bd\u01bf\5> \2\u01be\u01bc\3\2\2\2\u01be")
        buf.write("\u01bf\3\2\2\2\u01bf\u01c0\3\2\2\2\u01c0\u01c1\7b\2\2")
        buf.write("\u01c1\u01c2\7c\2\2\u01c2\u0255\3\2\2\2\u01c3\u01c4\7")
        buf.write("\17\2\2\u01c4\u01c7\5> \2\u01c5\u01c6\7d\2\2\u01c6\u01c8")
        buf.write("\7G\2\2\u01c7\u01c5\3\2\2\2\u01c7\u01c8\3\2\2\2\u01c8")
        buf.write("\u01c9\3\2\2\2\u01c9\u01ca\7c\2\2\u01ca\u0255\3\2\2\2")
        buf.write("\u01cb\u01cc\5:\36\2\u01cc\u01cd\7U\2\2\u01cd\u01ce\7")
        buf.write("*\2\2\u01ce\u01cf\5\4\3\2\u01cf\u01d1\7a\2\2\u01d0\u01d2")
        buf.write("\5F$\2\u01d1\u01d0\3\2\2\2\u01d1\u01d2\3\2\2\2\u01d2\u01d3")
        buf.write("\3\2\2\2\u01d3\u01d4\7b\2\2\u01d4\u01d5\7c\2\2\u01d5\u0255")
        buf.write("\3\2\2\2\u01d6\u01d7\5> \2\u01d7\u01d8\7e\2\2\u01d8\u01d9")
        buf.write("\7\21\2\2\u01d9\u01da\7a\2\2\u01da\u01db\5F$\2\u01db\u01dc")
        buf.write("\7b\2\2\u01dc\u01dd\7c\2\2\u01dd\u0255\3\2\2\2\u01de\u01df")
        buf.write("\7\4\2\2\u01df\u01e0\5\4\3\2\u01e0\u01e1\7U\2\2\u01e1")
        buf.write("\u01e2\5> \2\u01e2\u01e3\7e\2\2\u01e3\u01e4\7\21\2\2\u01e4")
        buf.write("\u01e5\7a\2\2\u01e5\u01e6\5F$\2\u01e6\u01e7\7b\2\2\u01e7")
        buf.write("\u01e8\7c\2\2\u01e8\u0255\3\2\2\2\u01e9\u01ea\5:\36\2")
        buf.write("\u01ea\u01eb\7U\2\2\u01eb\u01ec\5> \2\u01ec\u01ed\7e\2")
        buf.write("\2\u01ed\u01ee\7\21\2\2\u01ee\u01ef\7a\2\2\u01ef\u01f0")
        buf.write("\5F$\2\u01f0\u01f1\7b\2\2\u01f1\u01f2\7c\2\2\u01f2\u0255")
        buf.write("\3\2\2\2\u01f3\u01f4\5:\36\2\u01f4\u01f5\7e\2\2\u01f5")
        buf.write("\u01f6\5\4\3\2\u01f6\u01f8\7a\2\2\u01f7\u01f9\5F$\2\u01f8")
        buf.write("\u01f7\3\2\2\2\u01f8\u01f9\3\2\2\2\u01f9\u01fa\3\2\2\2")
        buf.write("\u01fa\u01fb\7b\2\2\u01fb\u01fc\7c\2\2\u01fc\u0255\3\2")
        buf.write("\2\2\u01fd\u01fe\5:\36\2\u01fe\u01ff\7U\2\2\u01ff\u0200")
        buf.write("\5:\36\2\u0200\u0201\7e\2\2\u0201\u0202\5\4\3\2\u0202")
        buf.write("\u0204\7a\2\2\u0203\u0205\5F$\2\u0204\u0203\3\2\2\2\u0204")
        buf.write("\u0205\3\2\2\2\u0205\u0206\3\2\2\2\u0206\u0207\7b\2\2")
        buf.write("\u0207\u0208\7c\2\2\u0208\u0255\3\2\2\2\u0209\u020a\5")
        buf.write(":\36\2\u020a\u020b\7U\2\2\u020b\u020c\5H%\2\u020c\u020d")
        buf.write("\7c\2\2\u020d\u0255\3\2\2\2\u020e\u020f\7\36\2\2\u020f")
        buf.write("\u0210\7a\2\2\u0210\u0211\5> \2\u0211\u0212\7b\2\2\u0212")
        buf.write("\u0214\5\66\34\2\u0213\u0215\58\35\2\u0214\u0213\3\2\2")
        buf.write("\2\u0214\u0215\3\2\2\2\u0215\u0255\3\2\2\2\u0216\u0217")
        buf.write("\7\33\2\2\u0217\u0218\7a\2\2\u0218\u021d\5\64\33\2\u0219")
        buf.write("\u021a\7d\2\2\u021a\u021c\5\64\33\2\u021b\u0219\3\2\2")
        buf.write("\2\u021c\u021f\3\2\2\2\u021d\u021b\3\2\2\2\u021d\u021e")
        buf.write("\3\2\2\2\u021e\u0220\3\2\2\2\u021f\u021d\3\2\2\2\u0220")
        buf.write("\u0221\7c\2\2\u0221\u0222\5> \2\u0222\u0223\7c\2\2\u0223")
        buf.write("\u0224\5> \2\u0224\u0225\7c\2\2\u0225\u0226\7b\2\2\u0226")
        buf.write("\u0227\5\66\34\2\u0227\u0255\3\2\2\2\u0228\u0229\5\4\3")
        buf.write("\2\u0229\u022b\7a\2\2\u022a\u022c\5F$\2\u022b\u022a\3")
        buf.write("\2\2\2\u022b\u022c\3\2\2\2\u022c\u022d\3\2\2\2\u022d\u022e")
        buf.write("\7b\2\2\u022e\u022f\7c\2\2\u022f\u0255\3\2\2\2\u0230\u0231")
        buf.write("\7<\2\2\u0231\u0232\7a\2\2\u0232\u0233\5> \2\u0233\u0234")
        buf.write("\7d\2\2\u0234\u0235\7\31\2\2\u0235\u0236\7d\2\2\u0236")
        buf.write("\u0237\5> \2\u0237\u0238\7b\2\2\u0238\u0239\7c\2\2\u0239")
        buf.write("\u0255\3\2\2\2\u023a\u023b\7<\2\2\u023b\u023c\7a\2\2\u023c")
        buf.write("\u023d\5> \2\u023d\u023e\7d\2\2\u023e\u023f\5\4\3\2\u023f")
        buf.write("\u0240\7d\2\2\u0240\u0245\5> \2\u0241\u0242\7d\2\2\u0242")
        buf.write("\u0244\5> \2\u0243\u0241\3\2\2\2\u0244\u0247\3\2\2\2\u0245")
        buf.write("\u0243\3\2\2\2\u0245\u0246\3\2\2\2\u0246\u0248\3\2\2\2")
        buf.write("\u0247\u0245\3\2\2\2\u0248\u0249\7b\2\2\u0249\u024a\7")
        buf.write("c\2\2\u024a\u0255\3\2\2\2\u024b\u024c\7\66\2\2\u024c\u024d")
        buf.write("\7a\2\2\u024d\u0250\7G\2\2\u024e\u024f\7d\2\2\u024f\u0251")
        buf.write("\5F$\2\u0250\u024e\3\2\2\2\u0250\u0251\3\2\2\2\u0251\u0252")
        buf.write("\3\2\2\2\u0252\u0253\7b\2\2\u0253\u0255\7c\2\2\u0254\u01a2")
        buf.write("\3\2\2\2\u0254\u01aa\3\2\2\2\u0254\u01b2\3\2\2\2\u0254")
        buf.write("\u01b9\3\2\2\2\u0254\u01c3\3\2\2\2\u0254\u01cb\3\2\2\2")
        buf.write("\u0254\u01d6\3\2\2\2\u0254\u01de\3\2\2\2\u0254\u01e9\3")
        buf.write("\2\2\2\u0254\u01f3\3\2\2\2\u0254\u01fd\3\2\2\2\u0254\u0209")
        buf.write("\3\2\2\2\u0254\u020e\3\2\2\2\u0254\u0216\3\2\2\2\u0254")
        buf.write("\u0228\3\2\2\2\u0254\u0230\3\2\2\2\u0254\u023a\3\2\2\2")
        buf.write("\u0254\u024b\3\2\2\2\u0255\67\3\2\2\2\u0256\u0257\7\30")
        buf.write("\2\2\u0257\u0258\5\66\34\2\u02589\3\2\2\2\u0259\u025a")
        buf.write("\b\36\1\2\u025a\u025b\5\4\3\2\u025b\u0266\3\2\2\2\u025c")
        buf.write("\u025d\f\4\2\2\u025d\u025e\7e\2\2\u025e\u0265\5\4\3\2")
        buf.write("\u025f\u0260\f\3\2\2\u0260\u0261\7_\2\2\u0261\u0262\5")
        buf.write("> \2\u0262\u0263\7`\2\2\u0263\u0265\3\2\2\2\u0264\u025c")
        buf.write("\3\2\2\2\u0264\u025f\3\2\2\2\u0265\u0268\3\2\2\2\u0266")
        buf.write("\u0264\3\2\2\2\u0266\u0267\3\2\2\2\u0267;\3\2\2\2\u0268")
        buf.write("\u0266\3\2\2\2\u0269\u026a\7a\2\2\u026a\u026b\5> \2\u026b")
        buf.write("\u026c\7d\2\2\u026c\u026d\5\4\3\2\u026d\u026e\7d\2\2\u026e")
        buf.write("\u0273\5> \2\u026f\u0270\7d\2\2\u0270\u0272\5> \2\u0271")
        buf.write("\u026f\3\2\2\2\u0272\u0275\3\2\2\2\u0273\u0271\3\2\2\2")
        buf.write("\u0273\u0274\3\2\2\2\u0274\u0276\3\2\2\2\u0275\u0273\3")
        buf.write("\2\2\2\u0276\u0277\7b\2\2\u0277\u0281\3\2\2\2\u0278\u0279")
        buf.write("\7a\2\2\u0279\u027a\5> \2\u027a\u027b\7d\2\2\u027b\u027c")
        buf.write("\7\31\2\2\u027c\u027d\7d\2\2\u027d\u027e\5> \2\u027e\u027f")
        buf.write("\7b\2\2\u027f\u0281\3\2\2\2\u0280\u0269\3\2\2\2\u0280")
        buf.write("\u0278\3\2\2\2\u0281=\3\2\2\2\u0282\u0283\b \1\2\u0283")
        buf.write("\u02fa\5@!\2\u0284\u0285\7a\2\2\u0285\u0286\5> \2\u0286")
        buf.write("\u0287\7b\2\2\u0287\u02fa\3\2\2\2\u0288\u0289\5\4\3\2")
        buf.write("\u0289\u028b\7a\2\2\u028a\u028c\5F$\2\u028b\u028a\3\2")
        buf.write("\2\2\u028b\u028c\3\2\2\2\u028c\u028d\3\2\2\2\u028d\u028e")
        buf.write("\7b\2\2\u028e\u02fa\3\2\2\2\u028f\u0290\7\34\2\2\u0290")
        buf.write("\u0291\7a\2\2\u0291\u0292\5\30\r\2\u0292\u0293\7b\2\2")
        buf.write("\u0293\u0294\7a\2\2\u0294\u0295\5> \2\u0295\u0296\7b\2")
        buf.write("\2\u0296\u02fa\3\2\2\2\u0297\u0298\7\32\2\2\u0298\u0299")
        buf.write("\7a\2\2\u0299\u029a\5\30\r\2\u029a\u029b\7b\2\2\u029b")
        buf.write("\u029c\7a\2\2\u029c\u029d\5> \2\u029d\u029e\7b\2\2\u029e")
        buf.write("\u02fa\3\2\2\2\u029f\u02a0\t\3\2\2\u02a0\u02fa\5> \26")
        buf.write("\u02a1\u02a2\79\2\2\u02a2\u02a3\7a\2\2\u02a3\u02a4\5>")
        buf.write(" \2\u02a4\u02a5\7d\2\2\u02a5\u02a6\5> \2\u02a6\u02a7\7")
        buf.write("b\2\2\u02a7\u02fa\3\2\2\2\u02a8\u02a9\78\2\2\u02a9\u02aa")
        buf.write("\7a\2\2\u02aa\u02ab\5> \2\u02ab\u02ac\7d\2\2\u02ac\u02ad")
        buf.write("\5> \2\u02ad\u02ae\7b\2\2\u02ae\u02fa\3\2\2\2\u02af\u02b0")
        buf.write("\7:\2\2\u02b0\u02b1\7a\2\2\u02b1\u02b2\5> \2\u02b2\u02b3")
        buf.write("\7d\2\2\u02b3\u02b4\5> \2\u02b4\u02b5\7b\2\2\u02b5\u02fa")
        buf.write("\3\2\2\2\u02b6\u02b7\7\67\2\2\u02b7\u02b8\7a\2\2\u02b8")
        buf.write("\u02b9\5> \2\u02b9\u02ba\7d\2\2\u02ba\u02bb\5> \2\u02bb")
        buf.write("\u02bc\7b\2\2\u02bc\u02fa\3\2\2\2\u02bd\u02be\7;\2\2\u02be")
        buf.write("\u02bf\7a\2\2\u02bf\u02c0\5> \2\u02c0\u02c1\7d\2\2\u02c1")
        buf.write("\u02c2\5> \2\u02c2\u02c3\7b\2\2\u02c3\u02fa\3\2\2\2\u02c4")
        buf.write("\u02c5\5\4\3\2\u02c5\u02c6\7a\2\2\u02c6\u02c7\5> \2\u02c7")
        buf.write("\u02c8\7b\2\2\u02c8\u02fa\3\2\2\2\u02c9\u02ca\7*\2\2\u02ca")
        buf.write("\u02cb\5\4\3\2\u02cb\u02cd\7a\2\2\u02cc\u02ce\5F$\2\u02cd")
        buf.write("\u02cc\3\2\2\2\u02cd\u02ce\3\2\2\2\u02ce\u02cf\3\2\2\2")
        buf.write("\u02cf\u02d0\7b\2\2\u02d0\u02fa\3\2\2\2\u02d1\u02d2\5")
        buf.write("\4\3\2\u02d2\u02d3\7e\2\2\u02d3\u02d4\7\16\2\2\u02d4\u02d5")
        buf.write("\7a\2\2\u02d5\u02d6\7*\2\2\u02d6\u02d7\5\4\3\2\u02d7\u02d9")
        buf.write("\7a\2\2\u02d8\u02da\5F$\2\u02d9\u02d8\3\2\2\2\u02d9\u02da")
        buf.write("\3\2\2\2\u02da\u02db\3\2\2\2\u02db\u02dc\7b\2\2\u02dc")
        buf.write("\u02dd\7b\2\2\u02dd\u02fa\3\2\2\2\u02de\u02df\7\"\2\2")
        buf.write("\u02df\u02e0\7a\2\2\u02e0\u02e1\5> \2\u02e1\u02e2\7d\2")
        buf.write("\2\u02e2\u02e3\5> \2\u02e3\u02e4\7d\2\2\u02e4\u02e5\5")
        buf.write("> \2\u02e5\u02e6\7b\2\2\u02e6\u02fa\3\2\2\2\u02e7\u02e8")
        buf.write("\7\26\2\2\u02e8\u02e9\7a\2\2\u02e9\u02ea\5\6\4\2\u02ea")
        buf.write("\u02eb\7b\2\2\u02eb\u02fa\3\2\2\2\u02ec\u02f2\5<\37\2")
        buf.write("\u02ed\u02ee\7f\2\2\u02ee\u02ef\7f\2\2\u02ef\u02f1\5<")
        buf.write("\37\2\u02f0\u02ed\3\2\2\2\u02f1\u02f4\3\2\2\2\u02f2\u02f0")
        buf.write("\3\2\2\2\u02f2\u02f3\3\2\2\2\u02f3\u02f5\3\2\2\2\u02f4")
        buf.write("\u02f2\3\2\2\2\u02f5\u02f6\7f\2\2\u02f6\u02f7\7f\2\2\u02f7")
        buf.write("\u02f8\5@!\2\u02f8\u02fa\3\2\2\2\u02f9\u0282\3\2\2\2\u02f9")
        buf.write("\u0284\3\2\2\2\u02f9\u0288\3\2\2\2\u02f9\u028f\3\2\2\2")
        buf.write("\u02f9\u0297\3\2\2\2\u02f9\u029f\3\2\2\2\u02f9\u02a1\3")
        buf.write("\2\2\2\u02f9\u02a8\3\2\2\2\u02f9\u02af\3\2\2\2\u02f9\u02b6")
        buf.write("\3\2\2\2\u02f9\u02bd\3\2\2\2\u02f9\u02c4\3\2\2\2\u02f9")
        buf.write("\u02c9\3\2\2\2\u02f9\u02d1\3\2\2\2\u02f9\u02de\3\2\2\2")
        buf.write("\u02f9\u02e7\3\2\2\2\u02f9\u02ec\3\2\2\2\u02fa\u032c\3")
        buf.write("\2\2\2\u02fb\u02fc\f\25\2\2\u02fc\u02fd\t\4\2\2\u02fd")
        buf.write("\u032b\5> \26\u02fe\u02ff\f\21\2\2\u02ff\u0300\t\5\2\2")
        buf.write("\u0300\u032b\5> \22\u0301\u0302\f\16\2\2\u0302\u0303\t")
        buf.write("\6\2\2\u0303\u032b\5> \17\u0304\u0305\f\r\2\2\u0305\u0306")
        buf.write("\t\7\2\2\u0306\u032b\5> \16\u0307\u0308\f\f\2\2\u0308")
        buf.write("\u0309\7I\2\2\u0309\u032b\5> \r\u030a\u030b\f\13\2\2\u030b")
        buf.write("\u030c\7J\2\2\u030c\u032b\5> \f\u030d\u030e\f\n\2\2\u030e")
        buf.write("\u030f\t\b\2\2\u030f\u032b\5> \13\u0310\u0311\f\34\2\2")
        buf.write("\u0311\u0312\7e\2\2\u0312\u032b\5\4\3\2\u0313\u0314\f")
        buf.write("\33\2\2\u0314\u0315\7_\2\2\u0315\u0316\5> \2\u0316\u0317")
        buf.write("\7`\2\2\u0317\u032b\3\2\2\2\u0318\u0319\f\32\2\2\u0319")
        buf.write("\u031a\7e\2\2\u031a\u031b\7&\2\2\u031b\u031c\7a\2\2\u031c")
        buf.write("\u032b\7b\2\2\u031d\u031e\f\t\2\2\u031e\u031f\7K\2\2\u031f")
        buf.write("\u0327\5> \2\u0320\u0321\7d\2\2\u0321\u0322\5> \2\u0322")
        buf.write("\u0323\7K\2\2\u0323\u0324\5> \2\u0324\u0326\3\2\2\2\u0325")
        buf.write("\u0320\3\2\2\2\u0326\u0329\3\2\2\2\u0327\u0325\3\2\2\2")
        buf.write("\u0327\u0328\3\2\2\2\u0328\u032b\3\2\2\2\u0329\u0327\3")
        buf.write("\2\2\2\u032a\u02fb\3\2\2\2\u032a\u02fe\3\2\2\2\u032a\u0301")
        buf.write("\3\2\2\2\u032a\u0304\3\2\2\2\u032a\u0307\3\2\2\2\u032a")
        buf.write("\u030a\3\2\2\2\u032a\u030d\3\2\2\2\u032a\u0310\3\2\2\2")
        buf.write("\u032a\u0313\3\2\2\2\u032a\u0318\3\2\2\2\u032a\u031d\3")
        buf.write("\2\2\2\u032b\u032e\3\2\2\2\u032c\u032a\3\2\2\2\u032c\u032d")
        buf.write("\3\2\2\2\u032d?\3\2\2\2\u032e\u032c\3\2\2\2\u032f\u0354")
        buf.write("\5\4\3\2\u0330\u0354\7C\2\2\u0331\u0354\7\20\2\2\u0332")
        buf.write("\u0354\7=\2\2\u0333\u0354\7\'\2\2\u0334\u0354\7 \2\2\u0335")
        buf.write("\u0354\7!\2\2\u0336\u0354\7B\2\2\u0337\u0338\7*\2\2\u0338")
        buf.write("\u0339\7a\2\2\u0339\u033a\5\4\3\2\u033a\u033b\7b\2\2\u033b")
        buf.write("\u0354\3\2\2\2\u033c\u033d\7*\2\2\u033d\u033e\7a\2\2\u033e")
        buf.write("\u033f\7\20\2\2\u033f\u0354\7b\2\2\u0340\u0341\7*\2\2")
        buf.write("\u0341\u0342\7a\2\2\u0342\u0343\7\'\2\2\u0343\u0354\7")
        buf.write("b\2\2\u0344\u0354\7D\2\2\u0345\u0354\7E\2\2\u0346\u0354")
        buf.write("\7F\2\2\u0347\u0354\7G\2\2\u0348\u0354\7@\2\2\u0349\u0354")
        buf.write("\7+\2\2\u034a\u034b\7\3\2\2\u034b\u034c\7a\2\2\u034c\u034d")
        buf.write("\7@\2\2\u034d\u0354\7b\2\2\u034e\u034f\7\3\2\2\u034f\u0350")
        buf.write("\7a\2\2\u0350\u0351\5\4\3\2\u0351\u0352\7b\2\2\u0352\u0354")
        buf.write("\3\2\2\2\u0353\u032f\3\2\2\2\u0353\u0330\3\2\2\2\u0353")
        buf.write("\u0331\3\2\2\2\u0353\u0332\3\2\2\2\u0353\u0333\3\2\2\2")
        buf.write("\u0353\u0334\3\2\2\2\u0353\u0335\3\2\2\2\u0353\u0336\3")
        buf.write("\2\2\2\u0353\u0337\3\2\2\2\u0353\u033c\3\2\2\2\u0353\u0340")
        buf.write("\3\2\2\2\u0353\u0344\3\2\2\2\u0353\u0345\3\2\2\2\u0353")
        buf.write("\u0346\3\2\2\2\u0353\u0347\3\2\2\2\u0353\u0348\3\2\2\2")
        buf.write("\u0353\u0349\3\2\2\2\u0353\u034a\3\2\2\2\u0353\u034e\3")
        buf.write("\2\2\2\u0354A\3\2\2\2\u0355\u0356\5H%\2\u0356\u0357\7")
        buf.write("d\2\2\u0357\u0360\3\2\2\2\u0358\u035b\5H%\2\u0359\u035a")
        buf.write("\7d\2\2\u035a\u035c\5H%\2\u035b\u0359\3\2\2\2\u035c\u035d")
        buf.write("\3\2\2\2\u035d\u035b\3\2\2\2\u035d\u035e\3\2\2\2\u035e")
        buf.write("\u0360\3\2\2\2\u035f\u0355\3\2\2\2\u035f\u0358\3\2\2\2")
        buf.write("\u0360C\3\2\2\2\u0361\u0362\5\4\3\2\u0362\u0363\7U\2\2")
        buf.write("\u0363\u0364\5H%\2\u0364\u0365\7d\2\2\u0365\u0373\3\2")
        buf.write("\2\2\u0366\u0367\5\4\3\2\u0367\u0368\7U\2\2\u0368\u036e")
        buf.write("\5H%\2\u0369\u036a\7d\2\2\u036a\u036b\5\4\3\2\u036b\u036c")
        buf.write("\7U\2\2\u036c\u036d\5H%\2\u036d\u036f\3\2\2\2\u036e\u0369")
        buf.write("\3\2\2\2\u036f\u0370\3\2\2\2\u0370\u036e\3\2\2\2\u0370")
        buf.write("\u0371\3\2\2\2\u0371\u0373\3\2\2\2\u0372\u0361\3\2\2\2")
        buf.write("\u0372\u0366\3\2\2\2\u0373E\3\2\2\2\u0374\u0379\5H%\2")
        buf.write("\u0375\u0376\7d\2\2\u0376\u0378\5H%\2\u0377\u0375\3\2")
        buf.write("\2\2\u0378\u037b\3\2\2\2\u0379\u0377\3\2\2\2\u0379\u037a")
        buf.write("\3\2\2\2\u037aG\3\2\2\2\u037b\u0379\3\2\2\2\u037c\u037d")
        buf.write("\5> \2\u037dI\3\2\2\2VMbls{\u008a\u0096\u00a0\u00b1\u00ba")
        buf.write("\u00c4\u00de\u00e1\u00e9\u00ed\u00f3\u00f6\u00fb\u00fe")
        buf.write("\u0104\u0108\u010b\u010e\u0112\u0115\u0118\u011c\u0120")
        buf.write("\u0124\u0128\u012c\u012f\u0132\u0136\u013a\u013e\u0142")
        buf.write("\u0145\u0148\u014a\u0150\u0154\u015a\u015d\u0162\u0165")
        buf.write("\u016b\u016f\u0178\u0181\u0183\u018b\u0193\u01a0\u01a6")
        buf.write("\u01be\u01c7\u01d1\u01f8\u0204\u0214\u021d\u022b\u0245")
        buf.write("\u0250\u0254\u0264\u0266\u0273\u0280\u028b\u02cd\u02d9")
        buf.write("\u02f2\u02f9\u0327\u032a\u032c\u0353\u035d\u035f\u0370")
        buf.write("\u0372\u0379")
        return buf.getvalue()


class CelestialParser ( Parser ):

    grammarFileName = "CelestialParser.g4"

    atn = ATNDeserializer().deserialize(serializedATN())

    decisionsToDFA = [ DFA(ds, i) for i, ds in enumerate(atn.decisionToState) ]

    sharedContextCache = PredictionContextCache()

    literalNames = [ "<INVALID>", "'address'", "'bool'", "'enum'", "'event'", 
                     "'eventlog'", "'uint'", "'inst_map'", "'int'", "'string'", 
                     "'contract'", "'mapping'", "'add'", "'assert'", "'balance'", 
                     "'call'", "'constructor'", "'contains'", "'credit'", 
                     "'debit'", "'default'", "'delete'", "'else'", "'eTransfer'", 
                     "'exists'", "'for'", "'forall'", "'function'", "'if'", 
                     "'in'", "'int_min'", "'int_max'", "'ite'", "'invariant'", 
                     "'keys'", "'lemma'", "'length'", "'log'", "'modifies'", 
                     "'modifies_addresses'", "'new'", "'now'", "'payable'", 
                     "'pop'", "'post'", "'pre'", "'print'", "'private'", 
                     "'public'", "'push'", "'return'", "'returns'", "'revert'", 
                     "'safe_add'", "'safe_div'", "'safe_mod'", "'safe_mul'", 
                     "'safe_sub'", "'send'", "'sender'", "'spec'", "'struct'", 
                     "'this'", "'tx_reverts'", "'uint_max'", "'value'", 
                     "<INVALID>", "<INVALID>", "'null'", "<INVALID>", "'!'", 
                     "'&&'", "'||'", "'=>'", "'==>'", "'<==>'", "'=='", 
                     "'!='", "'<='", "'>='", "'<'", "'>'", "'->'", "'='", 
                     "'+='", "'-='", "'+'", "'-'", "'*'", "'/'", "'%'", 
                     "'{'", "'}'", "'['", "']'", "'('", "')'", "';'", "','", 
                     "'.'", "':'" ]

    symbolicNames = [ "<INVALID>", "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", 
                      "UINT", "INSTMAP", "INT", "STRING", "CONTRACT", "MAP", 
                      "ADD", "ASSERT", "BALANCE", "CALL", "CONSTR", "CONTAINS", 
                      "CREDIT", "DEBIT", "DEFAULT", "DELETE", "ELSE", "ETRANSFER", 
                      "EXISTS", "FOR", "FORALL", "FUNCTION", "IF", "IN", 
                      "INT_MIN", "INT_MAX", "ITE", "INVARIANT", "KEYS", 
                      "LEMMA", "LENGTH", "LOG", "MODIFIES", "MODIFIESA", 
                      "NEW", "NOW", "PAYABLE", "POP", "POST", "PRE", "PRINT", 
                      "PRIVATE", "PUBLIC", "PUSH", "RETURN", "RETURNS", 
                      "REVERT", "SAFEADD", "SAFEDIV", "SAFEMOD", "SAFEMUL", 
                      "SAFESUB", "SEND", "SENDER", "SPEC", "STRUCT", "THIS", 
                      "TXREVERTS", "UINT_MAX", "VALUE", "BoolLiteral", "IntLiteral", 
                      "NullLiteral", "StringLiteral", "LNOT", "LAND", "LOR", 
                      "MAPUPD", "IMPL", "BIMPL", "EQ", "NE", "LE", "GE", 
                      "LT", "GT", "RARROW", "ASSIGN", "INSERT", "REMOVE", 
                      "PLUS", "SUB", "MUL", "DIV", "MOD", "LBRACE", "RBRACE", 
                      "LBRACK", "RBRACK", "LPAREN", "RPAREN", "SEMI", "COMMA", 
                      "DOT", "COLON", "Iden", "Whitespace", "BlockComment", 
                      "LineComment" ]

    RULE_program = 0
    RULE_iden = 1
    RULE_datatype = 2
    RULE_idenTypeList = 3
    RULE_idenType = 4
    RULE_contractDecl = 5
    RULE_contractBody = 6
    RULE_contractContents = 7
    RULE_enumDecl = 8
    RULE_structDecl = 9
    RULE_funDecl = 10
    RULE_funParamList = 11
    RULE_funParam = 12
    RULE_functionBody = 13
    RULE_invariantDecl = 14
    RULE_invariantBody = 15
    RULE_eventDecl = 16
    RULE_constructorDecl = 17
    RULE_spec = 18
    RULE_methodDecl = 19
    RULE_methodParamList = 20
    RULE_methodParam = 21
    RULE_methodBody = 22
    RULE_returnStatement = 23
    RULE_varDecl = 24
    RULE_loopVarDecl = 25
    RULE_statement = 26
    RULE_elseStatement = 27
    RULE_lvalue = 28
    RULE_logcheck = 29
    RULE_expr = 30
    RULE_primitive = 31
    RULE_unnamedTupleBody = 32
    RULE_namedTupleBody = 33
    RULE_rvalueList = 34
    RULE_rvalue = 35

    ruleNames =  [ "program", "iden", "datatype", "idenTypeList", "idenType", 
                   "contractDecl", "contractBody", "contractContents", "enumDecl", 
                   "structDecl", "funDecl", "funParamList", "funParam", 
                   "functionBody", "invariantDecl", "invariantBody", "eventDecl", 
                   "constructorDecl", "spec", "methodDecl", "methodParamList", 
                   "methodParam", "methodBody", "returnStatement", "varDecl", 
                   "loopVarDecl", "statement", "elseStatement", "lvalue", 
                   "logcheck", "expr", "primitive", "unnamedTupleBody", 
                   "namedTupleBody", "rvalueList", "rvalue" ]

    EOF = Token.EOF
    ADDR=1
    BOOL=2
    ENUM=3
    EVENT=4
    EVENTLOG=5
    UINT=6
    INSTMAP=7
    INT=8
    STRING=9
    CONTRACT=10
    MAP=11
    ADD=12
    ASSERT=13
    BALANCE=14
    CALL=15
    CONSTR=16
    CONTAINS=17
    CREDIT=18
    DEBIT=19
    DEFAULT=20
    DELETE=21
    ELSE=22
    ETRANSFER=23
    EXISTS=24
    FOR=25
    FORALL=26
    FUNCTION=27
    IF=28
    IN=29
    INT_MIN=30
    INT_MAX=31
    ITE=32
    INVARIANT=33
    KEYS=34
    LEMMA=35
    LENGTH=36
    LOG=37
    MODIFIES=38
    MODIFIESA=39
    NEW=40
    NOW=41
    PAYABLE=42
    POP=43
    POST=44
    PRE=45
    PRINT=46
    PRIVATE=47
    PUBLIC=48
    PUSH=49
    RETURN=50
    RETURNS=51
    REVERT=52
    SAFEADD=53
    SAFEDIV=54
    SAFEMOD=55
    SAFEMUL=56
    SAFESUB=57
    SEND=58
    SENDER=59
    SPEC=60
    STRUCT=61
    THIS=62
    TXREVERTS=63
    UINT_MAX=64
    VALUE=65
    BoolLiteral=66
    IntLiteral=67
    NullLiteral=68
    StringLiteral=69
    LNOT=70
    LAND=71
    LOR=72
    MAPUPD=73
    IMPL=74
    BIMPL=75
    EQ=76
    NE=77
    LE=78
    GE=79
    LT=80
    GT=81
    RARROW=82
    ASSIGN=83
    INSERT=84
    REMOVE=85
    PLUS=86
    SUB=87
    MUL=88
    DIV=89
    MOD=90
    LBRACE=91
    RBRACE=92
    LBRACK=93
    RBRACK=94
    LPAREN=95
    RPAREN=96
    SEMI=97
    COMMA=98
    DOT=99
    COLON=100
    Iden=101
    Whitespace=102
    BlockComment=103
    LineComment=104

    def __init__(self, input:TokenStream, output:TextIO = sys.stdout):
        super().__init__(input, output)
        self.checkVersion("4.8")
        self._interp = ParserATNSimulator(self, self.atn, self.decisionsToDFA, self.sharedContextCache)
        self._predicates = None




    class ProgramContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def EOF(self):
            return self.getToken(CelestialParser.EOF, 0)

        def contractDecl(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.ContractDeclContext)
            else:
                return self.getTypedRuleContext(CelestialParser.ContractDeclContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_program

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterProgram" ):
                listener.enterProgram(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitProgram" ):
                listener.exitProgram(self)




    def program(self):

        localctx = CelestialParser.ProgramContext(self, self._ctx, self.state)
        self.enterRule(localctx, 0, self.RULE_program)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 75
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while _la==CelestialParser.CONTRACT:
                self.state = 72
                self.contractDecl()
                self.state = 77
                self._errHandler.sync(self)
                _la = self._input.LA(1)

            self.state = 78
            self.match(CelestialParser.EOF)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class IdenContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def Iden(self):
            return self.getToken(CelestialParser.Iden, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_iden

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterIden" ):
                listener.enterIden(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitIden" ):
                listener.exitIden(self)




    def iden(self):

        localctx = CelestialParser.IdenContext(self, self._ctx, self.state)
        self.enterRule(localctx, 2, self.RULE_iden)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 80
            self.match(CelestialParser.Iden)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class DatatypeContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.arrayType = None # DatatypeContext
            self.keyType = None # DatatypeContext
            self.valueType = None # DatatypeContext
            self.name = None # IdenContext

        def MAP(self):
            return self.getToken(CelestialParser.MAP, 0)

        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)

        def MAPUPD(self):
            return self.getToken(CelestialParser.MAPUPD, 0)

        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)

        def datatype(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.DatatypeContext)
            else:
                return self.getTypedRuleContext(CelestialParser.DatatypeContext,i)


        def BOOL(self):
            return self.getToken(CelestialParser.BOOL, 0)

        def INT(self):
            return self.getToken(CelestialParser.INT, 0)

        def UINT(self):
            return self.getToken(CelestialParser.UINT, 0)

        def STRING(self):
            return self.getToken(CelestialParser.STRING, 0)

        def ADDR(self):
            return self.getToken(CelestialParser.ADDR, 0)

        def PAYABLE(self):
            return self.getToken(CelestialParser.PAYABLE, 0)

        def EVENTLOG(self):
            return self.getToken(CelestialParser.EVENTLOG, 0)

        def EVENT(self):
            return self.getToken(CelestialParser.EVENT, 0)

        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def INSTMAP(self):
            return self.getToken(CelestialParser.INSTMAP, 0)

        def LT(self):
            return self.getToken(CelestialParser.LT, 0)

        def GT(self):
            return self.getToken(CelestialParser.GT, 0)

        def LBRACK(self):
            return self.getToken(CelestialParser.LBRACK, 0)

        def RBRACK(self):
            return self.getToken(CelestialParser.RBRACK, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_datatype

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterDatatype" ):
                listener.enterDatatype(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitDatatype" ):
                listener.exitDatatype(self)



    def datatype(self, _p:int=0):
        _parentctx = self._ctx
        _parentState = self.state
        localctx = CelestialParser.DatatypeContext(self, self._ctx, _parentState)
        _prevctx = localctx
        _startState = 4
        self.enterRecursionRule(localctx, 4, self.RULE_datatype, _p)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 106
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [CelestialParser.MAP]:
                self.state = 83
                self.match(CelestialParser.MAP)
                self.state = 84
                self.match(CelestialParser.LPAREN)
                self.state = 85
                localctx.keyType = self.datatype(0)
                self.state = 86
                self.match(CelestialParser.MAPUPD)
                self.state = 87
                localctx.valueType = self.datatype(0)
                self.state = 88
                self.match(CelestialParser.RPAREN)
                pass
            elif token in [CelestialParser.BOOL]:
                self.state = 90
                self.match(CelestialParser.BOOL)
                pass
            elif token in [CelestialParser.INT]:
                self.state = 91
                self.match(CelestialParser.INT)
                pass
            elif token in [CelestialParser.UINT]:
                self.state = 92
                self.match(CelestialParser.UINT)
                pass
            elif token in [CelestialParser.STRING]:
                self.state = 93
                self.match(CelestialParser.STRING)
                pass
            elif token in [CelestialParser.ADDR]:
                self.state = 94
                self.match(CelestialParser.ADDR)
                self.state = 96
                self._errHandler.sync(self)
                la_ = self._interp.adaptivePredict(self._input,1,self._ctx)
                if la_ == 1:
                    self.state = 95
                    self.match(CelestialParser.PAYABLE)


                pass
            elif token in [CelestialParser.EVENTLOG]:
                self.state = 98
                self.match(CelestialParser.EVENTLOG)
                pass
            elif token in [CelestialParser.EVENT]:
                self.state = 99
                self.match(CelestialParser.EVENT)
                pass
            elif token in [CelestialParser.Iden]:
                self.state = 100
                localctx.name = self.iden()
                pass
            elif token in [CelestialParser.INSTMAP]:
                self.state = 101
                self.match(CelestialParser.INSTMAP)
                self.state = 102
                self.match(CelestialParser.LT)
                self.state = 103
                self.iden()
                self.state = 104
                self.match(CelestialParser.GT)
                pass
            else:
                raise NoViableAltException(self)

            self._ctx.stop = self._input.LT(-1)
            self.state = 113
            self._errHandler.sync(self)
            _alt = self._interp.adaptivePredict(self._input,3,self._ctx)
            while _alt!=2 and _alt!=ATN.INVALID_ALT_NUMBER:
                if _alt==1:
                    if self._parseListeners is not None:
                        self.triggerExitRuleEvent()
                    _prevctx = localctx
                    localctx = CelestialParser.DatatypeContext(self, _parentctx, _parentState)
                    localctx.arrayType = _prevctx
                    self.pushNewRecursionContext(localctx, _startState, self.RULE_datatype)
                    self.state = 108
                    if not self.precpred(self._ctx, 11):
                        from antlr4.error.Errors import FailedPredicateException
                        raise FailedPredicateException(self, "self.precpred(self._ctx, 11)")
                    self.state = 109
                    self.match(CelestialParser.LBRACK)
                    self.state = 110
                    self.match(CelestialParser.RBRACK) 
                self.state = 115
                self._errHandler.sync(self)
                _alt = self._interp.adaptivePredict(self._input,3,self._ctx)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.unrollRecursionContexts(_parentctx)
        return localctx


    class IdenTypeListContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def idenType(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.IdenTypeContext)
            else:
                return self.getTypedRuleContext(CelestialParser.IdenTypeContext,i)


        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_idenTypeList

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterIdenTypeList" ):
                listener.enterIdenTypeList(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitIdenTypeList" ):
                listener.exitIdenTypeList(self)




    def idenTypeList(self):

        localctx = CelestialParser.IdenTypeListContext(self, self._ctx, self.state)
        self.enterRule(localctx, 6, self.RULE_idenTypeList)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 116
            self.idenType()
            self.state = 121
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while _la==CelestialParser.COMMA:
                self.state = 117
                self.match(CelestialParser.COMMA)
                self.state = 118
                self.idenType()
                self.state = 123
                self._errHandler.sync(self)
                _la = self._input.LA(1)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class IdenTypeContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def COLON(self):
            return self.getToken(CelestialParser.COLON, 0)

        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_idenType

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterIdenType" ):
                listener.enterIdenType(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitIdenType" ):
                listener.exitIdenType(self)




    def idenType(self):

        localctx = CelestialParser.IdenTypeContext(self, self._ctx, self.state)
        self.enterRule(localctx, 8, self.RULE_idenType)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 124
            localctx.name = self.iden()
            self.state = 125
            self.match(CelestialParser.COLON)
            self.state = 126
            self.datatype(0)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ContractDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def CONTRACT(self):
            return self.getToken(CelestialParser.CONTRACT, 0)

        def contractBody(self):
            return self.getTypedRuleContext(CelestialParser.ContractBodyContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_contractDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterContractDecl" ):
                listener.enterContractDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitContractDecl" ):
                listener.exitContractDecl(self)




    def contractDecl(self):

        localctx = CelestialParser.ContractDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 10, self.RULE_contractDecl)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 128
            self.match(CelestialParser.CONTRACT)
            self.state = 129
            localctx.name = self.iden()
            self.state = 130
            self.contractBody()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ContractBodyContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def contractContents(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.ContractContentsContext)
            else:
                return self.getTypedRuleContext(CelestialParser.ContractContentsContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_contractBody

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterContractBody" ):
                listener.enterContractBody(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitContractBody" ):
                listener.exitContractBody(self)




    def contractBody(self):

        localctx = CelestialParser.ContractBodyContext(self, self._ctx, self.state)
        self.enterRule(localctx, 12, self.RULE_contractBody)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 132
            self.match(CelestialParser.LBRACE)
            self.state = 134 
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while True:
                self.state = 133
                self.contractContents()
                self.state = 136 
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if not ((((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.ENUM) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP) | (1 << CelestialParser.CONSTR) | (1 << CelestialParser.FUNCTION) | (1 << CelestialParser.INVARIANT) | (1 << CelestialParser.SPEC) | (1 << CelestialParser.STRUCT))) != 0) or _la==CelestialParser.Iden):
                    break

            self.state = 138
            self.match(CelestialParser.RBRACE)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ContractContentsContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def varDecl(self):
            return self.getTypedRuleContext(CelestialParser.VarDeclContext,0)


        def enumDecl(self):
            return self.getTypedRuleContext(CelestialParser.EnumDeclContext,0)


        def structDecl(self):
            return self.getTypedRuleContext(CelestialParser.StructDeclContext,0)


        def funDecl(self):
            return self.getTypedRuleContext(CelestialParser.FunDeclContext,0)


        def invariantDecl(self):
            return self.getTypedRuleContext(CelestialParser.InvariantDeclContext,0)


        def eventDecl(self):
            return self.getTypedRuleContext(CelestialParser.EventDeclContext,0)


        def constructorDecl(self):
            return self.getTypedRuleContext(CelestialParser.ConstructorDeclContext,0)


        def methodDecl(self):
            return self.getTypedRuleContext(CelestialParser.MethodDeclContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_contractContents

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterContractContents" ):
                listener.enterContractContents(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitContractContents" ):
                listener.exitContractContents(self)




    def contractContents(self):

        localctx = CelestialParser.ContractContentsContext(self, self._ctx, self.state)
        self.enterRule(localctx, 14, self.RULE_contractContents)
        try:
            self.state = 148
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,6,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 140
                self.varDecl()
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 141
                self.enumDecl()
                pass

            elif la_ == 3:
                self.enterOuterAlt(localctx, 3)
                self.state = 142
                self.structDecl()
                pass

            elif la_ == 4:
                self.enterOuterAlt(localctx, 4)
                self.state = 143
                self.funDecl()
                pass

            elif la_ == 5:
                self.enterOuterAlt(localctx, 5)
                self.state = 144
                self.invariantDecl()
                pass

            elif la_ == 6:
                self.enterOuterAlt(localctx, 6)
                self.state = 145
                self.eventDecl()
                pass

            elif la_ == 7:
                self.enterOuterAlt(localctx, 7)
                self.state = 146
                self.constructorDecl()
                pass

            elif la_ == 8:
                self.enterOuterAlt(localctx, 8)
                self.state = 147
                self.methodDecl()
                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class EnumDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def ENUM(self):
            return self.getToken(CelestialParser.ENUM, 0)

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def iden(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.IdenContext)
            else:
                return self.getTypedRuleContext(CelestialParser.IdenContext,i)


        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_enumDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterEnumDecl" ):
                listener.enterEnumDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitEnumDecl" ):
                listener.exitEnumDecl(self)




    def enumDecl(self):

        localctx = CelestialParser.EnumDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 16, self.RULE_enumDecl)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 150
            self.match(CelestialParser.ENUM)
            self.state = 151
            localctx.name = self.iden()
            self.state = 152
            self.match(CelestialParser.LBRACE)
            self.state = 153
            self.iden()
            self.state = 158
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while _la==CelestialParser.COMMA:
                self.state = 154
                self.match(CelestialParser.COMMA)
                self.state = 155
                self.iden()
                self.state = 160
                self._errHandler.sync(self)
                _la = self._input.LA(1)

            self.state = 161
            self.match(CelestialParser.RBRACE)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class StructDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def STRUCT(self):
            return self.getToken(CelestialParser.STRUCT, 0)

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def datatype(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.DatatypeContext)
            else:
                return self.getTypedRuleContext(CelestialParser.DatatypeContext,i)


        def iden(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.IdenContext)
            else:
                return self.getTypedRuleContext(CelestialParser.IdenContext,i)


        def SEMI(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.SEMI)
            else:
                return self.getToken(CelestialParser.SEMI, i)

        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_structDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterStructDecl" ):
                listener.enterStructDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitStructDecl" ):
                listener.exitStructDecl(self)




    def structDecl(self):

        localctx = CelestialParser.StructDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 18, self.RULE_structDecl)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 163
            self.match(CelestialParser.STRUCT)
            self.state = 164
            localctx.name = self.iden()
            self.state = 165
            self.match(CelestialParser.LBRACE)
            self.state = 166
            self.datatype(0)
            self.state = 167
            self.iden()
            self.state = 168
            self.match(CelestialParser.SEMI)
            self.state = 175
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP))) != 0) or _la==CelestialParser.Iden:
                self.state = 169
                self.datatype(0)
                self.state = 170
                self.iden()
                self.state = 171
                self.match(CelestialParser.SEMI)
                self.state = 177
                self._errHandler.sync(self)
                _la = self._input.LA(1)

            self.state = 178
            self.match(CelestialParser.RBRACE)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FunDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser


        def getRuleIndex(self):
            return CelestialParser.RULE_funDecl

     
        def copyFrom(self, ctx:ParserRuleContext):
            super().copyFrom(ctx)



    class FDeclContext(FunDeclContext):

        def __init__(self, parser, ctx:ParserRuleContext): # actually a CelestialParser.FunDeclContext
            super().__init__(parser)
            self.name = None # IdenContext
            self.copyFrom(ctx)

        def SPEC(self):
            return self.getToken(CelestialParser.SPEC, 0)
        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)
        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)
        def functionBody(self):
            return self.getTypedRuleContext(CelestialParser.FunctionBodyContext,0)

        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)

        def funParamList(self):
            return self.getTypedRuleContext(CelestialParser.FunParamListContext,0)


        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFDecl" ):
                listener.enterFDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFDecl" ):
                listener.exitFDecl(self)



    def funDecl(self):

        localctx = CelestialParser.FunDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 20, self.RULE_funDecl)
        self._la = 0 # Token type
        try:
            localctx = CelestialParser.FDeclContext(self, localctx)
            self.enterOuterAlt(localctx, 1)
            self.state = 180
            self.match(CelestialParser.SPEC)
            self.state = 181
            localctx.name = self.iden()
            self.state = 182
            self.match(CelestialParser.LPAREN)
            self.state = 184
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP))) != 0) or _la==CelestialParser.Iden:
                self.state = 183
                self.funParamList()


            self.state = 186
            self.match(CelestialParser.RPAREN)
            self.state = 187
            self.functionBody()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FunParamListContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def funParam(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.FunParamContext)
            else:
                return self.getTypedRuleContext(CelestialParser.FunParamContext,i)


        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_funParamList

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFunParamList" ):
                listener.enterFunParamList(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFunParamList" ):
                listener.exitFunParamList(self)




    def funParamList(self):

        localctx = CelestialParser.FunParamListContext(self, self._ctx, self.state)
        self.enterRule(localctx, 22, self.RULE_funParamList)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 189
            self.funParam()
            self.state = 194
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while _la==CelestialParser.COMMA:
                self.state = 190
                self.match(CelestialParser.COMMA)
                self.state = 191
                self.funParam()
                self.state = 196
                self._errHandler.sync(self)
                _la = self._input.LA(1)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FunParamContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_funParam

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFunParam" ):
                listener.enterFunParam(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFunParam" ):
                listener.exitFunParam(self)




    def funParam(self):

        localctx = CelestialParser.FunParamContext(self, self._ctx, self.state)
        self.enterRule(localctx, 24, self.RULE_funParam)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 197
            self.datatype(0)
            self.state = 198
            localctx.name = self.iden()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FunctionBodyContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_functionBody

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFunctionBody" ):
                listener.enterFunctionBody(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFunctionBody" ):
                listener.exitFunctionBody(self)




    def functionBody(self):

        localctx = CelestialParser.FunctionBodyContext(self, self._ctx, self.state)
        self.enterRule(localctx, 26, self.RULE_functionBody)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 200
            self.match(CelestialParser.LBRACE)
            self.state = 201
            self.expr(0)
            self.state = 202
            self.match(CelestialParser.RBRACE)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class InvariantDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def INVARIANT(self):
            return self.getToken(CelestialParser.INVARIANT, 0)

        def invariantBody(self):
            return self.getTypedRuleContext(CelestialParser.InvariantBodyContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_invariantDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterInvariantDecl" ):
                listener.enterInvariantDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitInvariantDecl" ):
                listener.exitInvariantDecl(self)




    def invariantDecl(self):

        localctx = CelestialParser.InvariantDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 28, self.RULE_invariantDecl)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 204
            self.match(CelestialParser.INVARIANT)
            self.state = 205
            localctx.name = self.iden()
            self.state = 206
            self.invariantBody()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class InvariantBodyContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_invariantBody

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterInvariantBody" ):
                listener.enterInvariantBody(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitInvariantBody" ):
                listener.exitInvariantBody(self)




    def invariantBody(self):

        localctx = CelestialParser.InvariantBodyContext(self, self._ctx, self.state)
        self.enterRule(localctx, 30, self.RULE_invariantBody)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 208
            self.match(CelestialParser.LBRACE)
            self.state = 209
            self.expr(0)
            self.state = 210
            self.match(CelestialParser.RBRACE)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class EventDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def EVENT(self):
            return self.getToken(CelestialParser.EVENT, 0)

        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)

        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)

        def SEMI(self):
            return self.getToken(CelestialParser.SEMI, 0)

        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def datatype(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.DatatypeContext)
            else:
                return self.getTypedRuleContext(CelestialParser.DatatypeContext,i)


        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_eventDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterEventDecl" ):
                listener.enterEventDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitEventDecl" ):
                listener.exitEventDecl(self)




    def eventDecl(self):

        localctx = CelestialParser.EventDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 32, self.RULE_eventDecl)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 212
            self.match(CelestialParser.EVENT)
            self.state = 213
            localctx.name = self.iden()
            self.state = 214
            self.match(CelestialParser.LPAREN)
            self.state = 223
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP))) != 0) or _la==CelestialParser.Iden:
                self.state = 215
                self.datatype(0)
                self.state = 220
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while _la==CelestialParser.COMMA:
                    self.state = 216
                    self.match(CelestialParser.COMMA)
                    self.state = 217
                    self.datatype(0)
                    self.state = 222
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)



            self.state = 225
            self.match(CelestialParser.RPAREN)
            self.state = 226
            self.match(CelestialParser.SEMI)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ConstructorDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.modifies = None # RvalueListContext
            self.modifies_addrs = None # RvalueListContext

        def CONSTR(self):
            return self.getToken(CelestialParser.CONSTR, 0)

        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)

        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)

        def spec(self):
            return self.getTypedRuleContext(CelestialParser.SpecContext,0)


        def methodBody(self):
            return self.getTypedRuleContext(CelestialParser.MethodBodyContext,0)


        def methodParamList(self):
            return self.getTypedRuleContext(CelestialParser.MethodParamListContext,0)


        def MODIFIES(self):
            return self.getToken(CelestialParser.MODIFIES, 0)

        def LBRACK(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.LBRACK)
            else:
                return self.getToken(CelestialParser.LBRACK, i)

        def RBRACK(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.RBRACK)
            else:
                return self.getToken(CelestialParser.RBRACK, i)

        def MODIFIESA(self):
            return self.getToken(CelestialParser.MODIFIESA, 0)

        def PUBLIC(self):
            return self.getToken(CelestialParser.PUBLIC, 0)

        def PRIVATE(self):
            return self.getToken(CelestialParser.PRIVATE, 0)

        def rvalueList(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.RvalueListContext)
            else:
                return self.getTypedRuleContext(CelestialParser.RvalueListContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_constructorDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterConstructorDecl" ):
                listener.enterConstructorDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitConstructorDecl" ):
                listener.exitConstructorDecl(self)




    def constructorDecl(self):

        localctx = CelestialParser.ConstructorDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 34, self.RULE_constructorDecl)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 228
            self.match(CelestialParser.CONSTR)
            self.state = 229
            self.match(CelestialParser.LPAREN)
            self.state = 231
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP))) != 0) or _la==CelestialParser.Iden:
                self.state = 230
                self.methodParamList()


            self.state = 233
            self.match(CelestialParser.RPAREN)
            self.state = 235
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.PRIVATE or _la==CelestialParser.PUBLIC:
                self.state = 234
                _la = self._input.LA(1)
                if not(_la==CelestialParser.PRIVATE or _la==CelestialParser.PUBLIC):
                    self._errHandler.recoverInline(self)
                else:
                    self._errHandler.reportMatch(self)
                    self.consume()


            self.state = 237
            self.spec()
            self.state = 244
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.MODIFIES:
                self.state = 238
                self.match(CelestialParser.MODIFIES)
                self.state = 239
                self.match(CelestialParser.LBRACK)
                self.state = 241
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 240
                    localctx.modifies = self.rvalueList()


                self.state = 243
                self.match(CelestialParser.RBRACK)


            self.state = 252
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.MODIFIESA:
                self.state = 246
                self.match(CelestialParser.MODIFIESA)
                self.state = 247
                self.match(CelestialParser.LBRACK)
                self.state = 249
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 248
                    localctx.modifies_addrs = self.rvalueList()


                self.state = 251
                self.match(CelestialParser.RBRACK)


            self.state = 254
            self.methodBody()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class SpecContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.pre = None # ExprContext
            self.post = None # ExprContext
            self.reverts = None # ExprContext

        def PRE(self):
            return self.getToken(CelestialParser.PRE, 0)

        def POST(self):
            return self.getToken(CelestialParser.POST, 0)

        def CREDIT(self):
            return self.getToken(CelestialParser.CREDIT, 0)

        def DEBIT(self):
            return self.getToken(CelestialParser.DEBIT, 0)

        def TXREVERTS(self):
            return self.getToken(CelestialParser.TXREVERTS, 0)

        def expr(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.ExprContext)
            else:
                return self.getTypedRuleContext(CelestialParser.ExprContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_spec

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterSpec" ):
                listener.enterSpec(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitSpec" ):
                listener.exitSpec(self)




    def spec(self):

        localctx = CelestialParser.SpecContext(self, self._ctx, self.state)
        self.enterRule(localctx, 36, self.RULE_spec)
        self._la = 0 # Token type
        try:
            self.state = 328
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,39,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 258
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.PRE:
                    self.state = 256
                    self.match(CelestialParser.PRE)
                    self.state = 257
                    localctx.pre = self.expr(0)


                self.state = 262
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.POST:
                    self.state = 260
                    self.match(CelestialParser.POST)
                    self.state = 261
                    localctx.post = self.expr(0)


                self.state = 265
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.CREDIT:
                    self.state = 264
                    self.match(CelestialParser.CREDIT)


                self.state = 268
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.DEBIT:
                    self.state = 267
                    self.match(CelestialParser.DEBIT)


                self.state = 272
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.TXREVERTS:
                    self.state = 270
                    self.match(CelestialParser.TXREVERTS)
                    self.state = 271
                    localctx.reverts = self.expr(0)


                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 275
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.CREDIT:
                    self.state = 274
                    self.match(CelestialParser.CREDIT)


                self.state = 278
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.DEBIT:
                    self.state = 277
                    self.match(CelestialParser.DEBIT)


                self.state = 282
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.PRE:
                    self.state = 280
                    self.match(CelestialParser.PRE)
                    self.state = 281
                    localctx.pre = self.expr(0)


                self.state = 286
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.POST:
                    self.state = 284
                    self.match(CelestialParser.POST)
                    self.state = 285
                    localctx.post = self.expr(0)


                self.state = 290
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.TXREVERTS:
                    self.state = 288
                    self.match(CelestialParser.TXREVERTS)
                    self.state = 289
                    localctx.reverts = self.expr(0)


                pass

            elif la_ == 3:
                self.enterOuterAlt(localctx, 3)
                self.state = 294
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.PRE:
                    self.state = 292
                    self.match(CelestialParser.PRE)
                    self.state = 293
                    localctx.pre = self.expr(0)


                self.state = 298
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.TXREVERTS:
                    self.state = 296
                    self.match(CelestialParser.TXREVERTS)
                    self.state = 297
                    localctx.reverts = self.expr(0)


                self.state = 301
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.CREDIT:
                    self.state = 300
                    self.match(CelestialParser.CREDIT)


                self.state = 304
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.DEBIT:
                    self.state = 303
                    self.match(CelestialParser.DEBIT)


                self.state = 308
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.POST:
                    self.state = 306
                    self.match(CelestialParser.POST)
                    self.state = 307
                    localctx.post = self.expr(0)


                pass

            elif la_ == 4:
                self.enterOuterAlt(localctx, 4)
                self.state = 312
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.PRE:
                    self.state = 310
                    self.match(CelestialParser.PRE)
                    self.state = 311
                    localctx.pre = self.expr(0)


                self.state = 316
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.TXREVERTS:
                    self.state = 314
                    self.match(CelestialParser.TXREVERTS)
                    self.state = 315
                    localctx.reverts = self.expr(0)


                self.state = 320
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.POST:
                    self.state = 318
                    self.match(CelestialParser.POST)
                    self.state = 319
                    localctx.post = self.expr(0)


                self.state = 323
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.CREDIT:
                    self.state = 322
                    self.match(CelestialParser.CREDIT)


                self.state = 326
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.DEBIT:
                    self.state = 325
                    self.match(CelestialParser.DEBIT)


                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class MethodDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser


        def getRuleIndex(self):
            return CelestialParser.RULE_methodDecl

     
        def copyFrom(self, ctx:ParserRuleContext):
            super().copyFrom(ctx)



    class MDeclContext(MethodDeclContext):

        def __init__(self, parser, ctx:ParserRuleContext): # actually a CelestialParser.MethodDeclContext
            super().__init__(parser)
            self.name = None # IdenContext
            self.modifies = None # RvalueListContext
            self.modifies_addrs = None # RvalueListContext
            self.returnval = None # IdenContext
            self.copyFrom(ctx)

        def FUNCTION(self):
            return self.getToken(CelestialParser.FUNCTION, 0)
        def LPAREN(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.LPAREN)
            else:
                return self.getToken(CelestialParser.LPAREN, i)
        def RPAREN(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.RPAREN)
            else:
                return self.getToken(CelestialParser.RPAREN, i)
        def spec(self):
            return self.getTypedRuleContext(CelestialParser.SpecContext,0)

        def methodBody(self):
            return self.getTypedRuleContext(CelestialParser.MethodBodyContext,0)

        def iden(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.IdenContext)
            else:
                return self.getTypedRuleContext(CelestialParser.IdenContext,i)

        def methodParamList(self):
            return self.getTypedRuleContext(CelestialParser.MethodParamListContext,0)

        def MODIFIES(self):
            return self.getToken(CelestialParser.MODIFIES, 0)
        def LBRACK(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.LBRACK)
            else:
                return self.getToken(CelestialParser.LBRACK, i)
        def RBRACK(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.RBRACK)
            else:
                return self.getToken(CelestialParser.RBRACK, i)
        def MODIFIESA(self):
            return self.getToken(CelestialParser.MODIFIESA, 0)
        def RETURNS(self):
            return self.getToken(CelestialParser.RETURNS, 0)
        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)

        def PUBLIC(self):
            return self.getToken(CelestialParser.PUBLIC, 0)
        def PRIVATE(self):
            return self.getToken(CelestialParser.PRIVATE, 0)
        def rvalueList(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.RvalueListContext)
            else:
                return self.getTypedRuleContext(CelestialParser.RvalueListContext,i)


        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterMDecl" ):
                listener.enterMDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitMDecl" ):
                listener.exitMDecl(self)



    def methodDecl(self):

        localctx = CelestialParser.MethodDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 38, self.RULE_methodDecl)
        self._la = 0 # Token type
        try:
            localctx = CelestialParser.MDeclContext(self, localctx)
            self.enterOuterAlt(localctx, 1)
            self.state = 330
            self.match(CelestialParser.FUNCTION)
            self.state = 331
            localctx.name = self.iden()
            self.state = 332
            self.match(CelestialParser.LPAREN)
            self.state = 334
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP))) != 0) or _la==CelestialParser.Iden:
                self.state = 333
                self.methodParamList()


            self.state = 336
            self.match(CelestialParser.RPAREN)
            self.state = 338
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.PRIVATE or _la==CelestialParser.PUBLIC:
                self.state = 337
                _la = self._input.LA(1)
                if not(_la==CelestialParser.PRIVATE or _la==CelestialParser.PUBLIC):
                    self._errHandler.recoverInline(self)
                else:
                    self._errHandler.reportMatch(self)
                    self.consume()


            self.state = 340
            self.spec()
            self.state = 347
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.MODIFIES:
                self.state = 341
                self.match(CelestialParser.MODIFIES)
                self.state = 342
                self.match(CelestialParser.LBRACK)
                self.state = 344
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 343
                    localctx.modifies = self.rvalueList()


                self.state = 346
                self.match(CelestialParser.RBRACK)


            self.state = 355
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.MODIFIESA:
                self.state = 349
                self.match(CelestialParser.MODIFIESA)
                self.state = 350
                self.match(CelestialParser.LBRACK)
                self.state = 352
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 351
                    localctx.modifies_addrs = self.rvalueList()


                self.state = 354
                self.match(CelestialParser.RBRACK)


            self.state = 365
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.RETURNS:
                self.state = 357
                self.match(CelestialParser.RETURNS)
                self.state = 358
                self.match(CelestialParser.LPAREN)
                self.state = 359
                self.datatype(0)
                self.state = 361
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.Iden:
                    self.state = 360
                    localctx.returnval = self.iden()


                self.state = 363
                self.match(CelestialParser.RPAREN)


            self.state = 367
            self.methodBody()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class MethodParamListContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def methodParam(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.MethodParamContext)
            else:
                return self.getTypedRuleContext(CelestialParser.MethodParamContext,i)


        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_methodParamList

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterMethodParamList" ):
                listener.enterMethodParamList(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitMethodParamList" ):
                listener.exitMethodParamList(self)




    def methodParamList(self):

        localctx = CelestialParser.MethodParamListContext(self, self._ctx, self.state)
        self.enterRule(localctx, 40, self.RULE_methodParamList)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 369
            self.methodParam()
            self.state = 374
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while _la==CelestialParser.COMMA:
                self.state = 370
                self.match(CelestialParser.COMMA)
                self.state = 371
                self.methodParam()
                self.state = 376
                self._errHandler.sync(self)
                _la = self._input.LA(1)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class MethodParamContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext

        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_methodParam

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterMethodParam" ):
                listener.enterMethodParam(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitMethodParam" ):
                listener.exitMethodParam(self)




    def methodParam(self):

        localctx = CelestialParser.MethodParamContext(self, self._ctx, self.state)
        self.enterRule(localctx, 42, self.RULE_methodParam)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 377
            self.datatype(0)
            self.state = 378
            localctx.name = self.iden()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class MethodBodyContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def returnStatement(self):
            return self.getTypedRuleContext(CelestialParser.ReturnStatementContext,0)


        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def varDecl(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.VarDeclContext)
            else:
                return self.getTypedRuleContext(CelestialParser.VarDeclContext,i)


        def statement(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.StatementContext)
            else:
                return self.getTypedRuleContext(CelestialParser.StatementContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_methodBody

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterMethodBody" ):
                listener.enterMethodBody(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitMethodBody" ):
                listener.exitMethodBody(self)




    def methodBody(self):

        localctx = CelestialParser.MethodBodyContext(self, self._ctx, self.state)
        self.enterRule(localctx, 44, self.RULE_methodBody)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 380
            self.match(CelestialParser.LBRACE)
            self.state = 385
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.EVENT) | (1 << CelestialParser.EVENTLOG) | (1 << CelestialParser.UINT) | (1 << CelestialParser.INSTMAP) | (1 << CelestialParser.INT) | (1 << CelestialParser.STRING) | (1 << CelestialParser.MAP) | (1 << CelestialParser.ASSERT) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.DELETE) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FOR) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.IF) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.REVERT) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SEND) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LBRACE - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                self.state = 383
                self._errHandler.sync(self)
                la_ = self._interp.adaptivePredict(self._input,49,self._ctx)
                if la_ == 1:
                    self.state = 381
                    self.varDecl()
                    pass

                elif la_ == 2:
                    self.state = 382
                    self.statement()
                    pass


                self.state = 387
                self._errHandler.sync(self)
                _la = self._input.LA(1)

            self.state = 388
            self.returnStatement()
            self.state = 389
            self.match(CelestialParser.RBRACE)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ReturnStatementContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def RETURN(self):
            return self.getToken(CelestialParser.RETURN, 0)

        def SEMI(self):
            return self.getToken(CelestialParser.SEMI, 0)

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_returnStatement

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterReturnStatement" ):
                listener.enterReturnStatement(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitReturnStatement" ):
                listener.exitReturnStatement(self)




    def returnStatement(self):

        localctx = CelestialParser.ReturnStatementContext(self, self._ctx, self.state)
        self.enterRule(localctx, 46, self.RULE_returnStatement)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 391
            self.match(CelestialParser.RETURN)
            self.state = 393
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                self.state = 392
                self.expr(0)


            self.state = 395
            self.match(CelestialParser.SEMI)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class VarDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def SEMI(self):
            return self.getToken(CelestialParser.SEMI, 0)

        def ASSIGN(self):
            return self.getToken(CelestialParser.ASSIGN, 0)

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_varDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterVarDecl" ):
                listener.enterVarDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitVarDecl" ):
                listener.exitVarDecl(self)




    def varDecl(self):

        localctx = CelestialParser.VarDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 48, self.RULE_varDecl)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 397
            self.datatype(0)
            self.state = 398
            self.iden()
            self.state = 401
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            if _la==CelestialParser.ASSIGN:
                self.state = 399
                self.match(CelestialParser.ASSIGN)
                self.state = 400
                self.expr(0)


            self.state = 403
            self.match(CelestialParser.SEMI)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LoopVarDeclContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def ASSIGN(self):
            return self.getToken(CelestialParser.ASSIGN, 0)

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_loopVarDecl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLoopVarDecl" ):
                listener.enterLoopVarDecl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLoopVarDecl" ):
                listener.exitLoopVarDecl(self)




    def loopVarDecl(self):

        localctx = CelestialParser.LoopVarDeclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 50, self.RULE_loopVarDecl)
        try:
            self.state = 414
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,53,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 405
                self.datatype(0)
                self.state = 406
                self.iden()
                self.state = 407
                self.match(CelestialParser.ASSIGN)
                self.state = 408
                self.expr(0)
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 410
                self.iden()
                self.state = 411
                self.match(CelestialParser.ASSIGN)
                self.state = 412
                self.expr(0)
                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class StatementContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.arrayName = None # LvalueContext
            self.value = None # ExprContext
            self.toDelete = None # LvalueContext
            self.assignTo = None # LvalueContext
            self.otherContractInstance = None # LvalueContext
            self.method = None # IdenContext
            self.assignment = None # Token
            self.thenBranch = None # StatementContext
            self.loopBody = None # StatementContext
            self.contract = None # ExprContext
            self.payload = None # ExprContext
            self.event = None # IdenContext

        def LBRACE(self):
            return self.getToken(CelestialParser.LBRACE, 0)

        def RBRACE(self):
            return self.getToken(CelestialParser.RBRACE, 0)

        def statement(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.StatementContext)
            else:
                return self.getTypedRuleContext(CelestialParser.StatementContext,i)


        def DOT(self):
            return self.getToken(CelestialParser.DOT, 0)

        def PUSH(self):
            return self.getToken(CelestialParser.PUSH, 0)

        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)

        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)

        def SEMI(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.SEMI)
            else:
                return self.getToken(CelestialParser.SEMI, i)

        def lvalue(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.LvalueContext)
            else:
                return self.getTypedRuleContext(CelestialParser.LvalueContext,i)


        def expr(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.ExprContext)
            else:
                return self.getTypedRuleContext(CelestialParser.ExprContext,i)


        def POP(self):
            return self.getToken(CelestialParser.POP, 0)

        def DELETE(self):
            return self.getToken(CelestialParser.DELETE, 0)

        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def ASSERT(self):
            return self.getToken(CelestialParser.ASSERT, 0)

        def StringLiteral(self):
            return self.getToken(CelestialParser.StringLiteral, 0)

        def ASSIGN(self):
            return self.getToken(CelestialParser.ASSIGN, 0)

        def NEW(self):
            return self.getToken(CelestialParser.NEW, 0)

        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def rvalueList(self):
            return self.getTypedRuleContext(CelestialParser.RvalueListContext,0)


        def CALL(self):
            return self.getToken(CelestialParser.CALL, 0)

        def BOOL(self):
            return self.getToken(CelestialParser.BOOL, 0)

        def rvalue(self):
            return self.getTypedRuleContext(CelestialParser.RvalueContext,0)


        def IF(self):
            return self.getToken(CelestialParser.IF, 0)

        def elseStatement(self):
            return self.getTypedRuleContext(CelestialParser.ElseStatementContext,0)


        def FOR(self):
            return self.getToken(CelestialParser.FOR, 0)

        def loopVarDecl(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.LoopVarDeclContext)
            else:
                return self.getTypedRuleContext(CelestialParser.LoopVarDeclContext,i)


        def SEND(self):
            return self.getToken(CelestialParser.SEND, 0)

        def ETRANSFER(self):
            return self.getToken(CelestialParser.ETRANSFER, 0)

        def REVERT(self):
            return self.getToken(CelestialParser.REVERT, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_statement

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterStatement" ):
                listener.enterStatement(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitStatement" ):
                listener.exitStatement(self)




    def statement(self):

        localctx = CelestialParser.StatementContext(self, self._ctx, self.state)
        self.enterRule(localctx, 52, self.RULE_statement)
        self._la = 0 # Token type
        try:
            self.state = 594
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,65,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 416
                self.match(CelestialParser.LBRACE)
                self.state = 420
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BOOL) | (1 << CelestialParser.ASSERT) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.DELETE) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FOR) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.IF) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.REVERT) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SEND) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LBRACE - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 417
                    self.statement()
                    self.state = 422
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)

                self.state = 423
                self.match(CelestialParser.RBRACE)
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 424
                localctx.arrayName = self.lvalue(0)
                self.state = 425
                self.match(CelestialParser.DOT)
                self.state = 426
                self.match(CelestialParser.PUSH)
                self.state = 427
                self.match(CelestialParser.LPAREN)
                self.state = 428
                localctx.value = self.expr(0)
                self.state = 429
                self.match(CelestialParser.RPAREN)
                self.state = 430
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 3:
                self.enterOuterAlt(localctx, 3)
                self.state = 432
                localctx.arrayName = self.lvalue(0)
                self.state = 433
                self.match(CelestialParser.DOT)
                self.state = 434
                self.match(CelestialParser.POP)
                self.state = 435
                self.match(CelestialParser.LPAREN)
                self.state = 436
                self.match(CelestialParser.RPAREN)
                self.state = 437
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 4:
                self.enterOuterAlt(localctx, 4)
                self.state = 439
                self.match(CelestialParser.DELETE)
                self.state = 440
                self.match(CelestialParser.LPAREN)
                self.state = 441
                localctx.toDelete = self.lvalue(0)
                self.state = 444
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.COMMA:
                    self.state = 442
                    self.match(CelestialParser.COMMA)
                    self.state = 443
                    localctx.value = self.expr(0)


                self.state = 446
                self.match(CelestialParser.RPAREN)
                self.state = 447
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 5:
                self.enterOuterAlt(localctx, 5)
                self.state = 449
                self.match(CelestialParser.ASSERT)
                self.state = 450
                self.expr(0)
                self.state = 453
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.COMMA:
                    self.state = 451
                    self.match(CelestialParser.COMMA)
                    self.state = 452
                    self.match(CelestialParser.StringLiteral)


                self.state = 455
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 6:
                self.enterOuterAlt(localctx, 6)
                self.state = 457
                localctx.assignTo = self.lvalue(0)
                self.state = 458
                self.match(CelestialParser.ASSIGN)
                self.state = 459
                self.match(CelestialParser.NEW)
                self.state = 460
                self.iden()
                self.state = 461
                self.match(CelestialParser.LPAREN)
                self.state = 463
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 462
                    self.rvalueList()


                self.state = 465
                self.match(CelestialParser.RPAREN)
                self.state = 466
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 7:
                self.enterOuterAlt(localctx, 7)
                self.state = 468
                self.expr(0)
                self.state = 469
                self.match(CelestialParser.DOT)
                self.state = 470
                self.match(CelestialParser.CALL)
                self.state = 471
                self.match(CelestialParser.LPAREN)
                self.state = 472
                self.rvalueList()
                self.state = 473
                self.match(CelestialParser.RPAREN)
                self.state = 474
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 8:
                self.enterOuterAlt(localctx, 8)
                self.state = 476
                self.match(CelestialParser.BOOL)
                self.state = 477
                self.iden()
                self.state = 478
                self.match(CelestialParser.ASSIGN)
                self.state = 479
                self.expr(0)
                self.state = 480
                self.match(CelestialParser.DOT)
                self.state = 481
                self.match(CelestialParser.CALL)
                self.state = 482
                self.match(CelestialParser.LPAREN)
                self.state = 483
                self.rvalueList()
                self.state = 484
                self.match(CelestialParser.RPAREN)
                self.state = 485
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 9:
                self.enterOuterAlt(localctx, 9)
                self.state = 487
                self.lvalue(0)
                self.state = 488
                self.match(CelestialParser.ASSIGN)
                self.state = 489
                self.expr(0)
                self.state = 490
                self.match(CelestialParser.DOT)
                self.state = 491
                self.match(CelestialParser.CALL)
                self.state = 492
                self.match(CelestialParser.LPAREN)
                self.state = 493
                self.rvalueList()
                self.state = 494
                self.match(CelestialParser.RPAREN)
                self.state = 495
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 10:
                self.enterOuterAlt(localctx, 10)
                self.state = 497
                localctx.otherContractInstance = self.lvalue(0)
                self.state = 498
                self.match(CelestialParser.DOT)
                self.state = 499
                localctx.method = self.iden()
                self.state = 500
                self.match(CelestialParser.LPAREN)
                self.state = 502
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 501
                    self.rvalueList()


                self.state = 504
                self.match(CelestialParser.RPAREN)
                self.state = 505
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 11:
                self.enterOuterAlt(localctx, 11)
                self.state = 507
                localctx.assignTo = self.lvalue(0)
                self.state = 508
                self.match(CelestialParser.ASSIGN)
                self.state = 509
                localctx.otherContractInstance = self.lvalue(0)
                self.state = 510
                self.match(CelestialParser.DOT)
                self.state = 511
                localctx.method = self.iden()
                self.state = 512
                self.match(CelestialParser.LPAREN)
                self.state = 514
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 513
                    self.rvalueList()


                self.state = 516
                self.match(CelestialParser.RPAREN)
                self.state = 517
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 12:
                self.enterOuterAlt(localctx, 12)
                self.state = 519
                localctx.assignTo = self.lvalue(0)
                self.state = 520
                localctx.assignment = self.match(CelestialParser.ASSIGN)
                self.state = 521
                self.rvalue()
                self.state = 522
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 13:
                self.enterOuterAlt(localctx, 13)
                self.state = 524
                self.match(CelestialParser.IF)
                self.state = 525
                self.match(CelestialParser.LPAREN)
                self.state = 526
                self.expr(0)
                self.state = 527
                self.match(CelestialParser.RPAREN)
                self.state = 528
                localctx.thenBranch = self.statement()
                self.state = 530
                self._errHandler.sync(self)
                la_ = self._interp.adaptivePredict(self._input,60,self._ctx)
                if la_ == 1:
                    self.state = 529
                    self.elseStatement()


                pass

            elif la_ == 14:
                self.enterOuterAlt(localctx, 14)
                self.state = 532
                self.match(CelestialParser.FOR)
                self.state = 533
                self.match(CelestialParser.LPAREN)
                self.state = 534
                self.loopVarDecl()
                self.state = 539
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while _la==CelestialParser.COMMA:
                    self.state = 535
                    self.match(CelestialParser.COMMA)
                    self.state = 536
                    self.loopVarDecl()
                    self.state = 541
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)

                self.state = 542
                self.match(CelestialParser.SEMI)
                self.state = 543
                self.expr(0)
                self.state = 544
                self.match(CelestialParser.SEMI)
                self.state = 545
                self.expr(0)
                self.state = 546
                self.match(CelestialParser.SEMI)
                self.state = 547
                self.match(CelestialParser.RPAREN)
                self.state = 548
                localctx.loopBody = self.statement()
                pass

            elif la_ == 15:
                self.enterOuterAlt(localctx, 15)
                self.state = 550
                localctx.method = self.iden()
                self.state = 551
                self.match(CelestialParser.LPAREN)
                self.state = 553
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 552
                    self.rvalueList()


                self.state = 555
                self.match(CelestialParser.RPAREN)
                self.state = 556
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 16:
                self.enterOuterAlt(localctx, 16)
                self.state = 558
                self.match(CelestialParser.SEND)
                self.state = 559
                self.match(CelestialParser.LPAREN)
                self.state = 560
                localctx.contract = self.expr(0)
                self.state = 561
                self.match(CelestialParser.COMMA)
                self.state = 562
                self.match(CelestialParser.ETRANSFER)
                self.state = 563
                self.match(CelestialParser.COMMA)
                self.state = 564
                localctx.payload = self.expr(0)
                self.state = 565
                self.match(CelestialParser.RPAREN)
                self.state = 566
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 17:
                self.enterOuterAlt(localctx, 17)
                self.state = 568
                self.match(CelestialParser.SEND)
                self.state = 569
                self.match(CelestialParser.LPAREN)
                self.state = 570
                localctx.contract = self.expr(0)
                self.state = 571
                self.match(CelestialParser.COMMA)
                self.state = 572
                localctx.event = self.iden()
                self.state = 573
                self.match(CelestialParser.COMMA)
                self.state = 574
                localctx.payload = self.expr(0)
                self.state = 579
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while _la==CelestialParser.COMMA:
                    self.state = 575
                    self.match(CelestialParser.COMMA)
                    self.state = 576
                    localctx.payload = self.expr(0)
                    self.state = 581
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)

                self.state = 582
                self.match(CelestialParser.RPAREN)
                self.state = 583
                self.match(CelestialParser.SEMI)
                pass

            elif la_ == 18:
                self.enterOuterAlt(localctx, 18)
                self.state = 585
                self.match(CelestialParser.REVERT)
                self.state = 586
                self.match(CelestialParser.LPAREN)
                self.state = 587
                self.match(CelestialParser.StringLiteral)
                self.state = 590
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if _la==CelestialParser.COMMA:
                    self.state = 588
                    self.match(CelestialParser.COMMA)
                    self.state = 589
                    self.rvalueList()


                self.state = 592
                self.match(CelestialParser.RPAREN)
                self.state = 593
                self.match(CelestialParser.SEMI)
                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ElseStatementContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ELSE(self):
            return self.getToken(CelestialParser.ELSE, 0)

        def statement(self):
            return self.getTypedRuleContext(CelestialParser.StatementContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_elseStatement

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterElseStatement" ):
                listener.enterElseStatement(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitElseStatement" ):
                listener.exitElseStatement(self)




    def elseStatement(self):

        localctx = CelestialParser.ElseStatementContext(self, self._ctx, self.state)
        self.enterRule(localctx, 54, self.RULE_elseStatement)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 596
            self.match(CelestialParser.ELSE)
            self.state = 597
            self.statement()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LvalueContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.name = None # IdenContext
            self.field = None # IdenContext

        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def lvalue(self):
            return self.getTypedRuleContext(CelestialParser.LvalueContext,0)


        def DOT(self):
            return self.getToken(CelestialParser.DOT, 0)

        def LBRACK(self):
            return self.getToken(CelestialParser.LBRACK, 0)

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def RBRACK(self):
            return self.getToken(CelestialParser.RBRACK, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_lvalue

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLvalue" ):
                listener.enterLvalue(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLvalue" ):
                listener.exitLvalue(self)



    def lvalue(self, _p:int=0):
        _parentctx = self._ctx
        _parentState = self.state
        localctx = CelestialParser.LvalueContext(self, self._ctx, _parentState)
        _prevctx = localctx
        _startState = 56
        self.enterRecursionRule(localctx, 56, self.RULE_lvalue, _p)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 600
            localctx.name = self.iden()
            self._ctx.stop = self._input.LT(-1)
            self.state = 612
            self._errHandler.sync(self)
            _alt = self._interp.adaptivePredict(self._input,67,self._ctx)
            while _alt!=2 and _alt!=ATN.INVALID_ALT_NUMBER:
                if _alt==1:
                    if self._parseListeners is not None:
                        self.triggerExitRuleEvent()
                    _prevctx = localctx
                    self.state = 610
                    self._errHandler.sync(self)
                    la_ = self._interp.adaptivePredict(self._input,66,self._ctx)
                    if la_ == 1:
                        localctx = CelestialParser.LvalueContext(self, _parentctx, _parentState)
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_lvalue)
                        self.state = 602
                        if not self.precpred(self._ctx, 2):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 2)")
                        self.state = 603
                        self.match(CelestialParser.DOT)
                        self.state = 604
                        localctx.field = self.iden()
                        pass

                    elif la_ == 2:
                        localctx = CelestialParser.LvalueContext(self, _parentctx, _parentState)
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_lvalue)
                        self.state = 605
                        if not self.precpred(self._ctx, 1):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 1)")
                        self.state = 606
                        self.match(CelestialParser.LBRACK)
                        self.state = 607
                        self.expr(0)
                        self.state = 608
                        self.match(CelestialParser.RBRACK)
                        pass

             
                self.state = 614
                self._errHandler.sync(self)
                _alt = self._interp.adaptivePredict(self._input,67,self._ctx)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.unrollRecursionContexts(_parentctx)
        return localctx


    class LogcheckContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.to = None # ExprContext
            self.event = None # IdenContext
            self.payload = None # ExprContext

        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)

        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)

        def expr(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.ExprContext)
            else:
                return self.getTypedRuleContext(CelestialParser.ExprContext,i)


        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def ETRANSFER(self):
            return self.getToken(CelestialParser.ETRANSFER, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_logcheck

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLogcheck" ):
                listener.enterLogcheck(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLogcheck" ):
                listener.exitLogcheck(self)




    def logcheck(self):

        localctx = CelestialParser.LogcheckContext(self, self._ctx, self.state)
        self.enterRule(localctx, 58, self.RULE_logcheck)
        self._la = 0 # Token type
        try:
            self.state = 638
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,69,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 615
                self.match(CelestialParser.LPAREN)
                self.state = 616
                localctx.to = self.expr(0)
                self.state = 617
                self.match(CelestialParser.COMMA)
                self.state = 618
                localctx.event = self.iden()
                self.state = 619
                self.match(CelestialParser.COMMA)
                self.state = 620
                localctx.payload = self.expr(0)
                self.state = 625
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while _la==CelestialParser.COMMA:
                    self.state = 621
                    self.match(CelestialParser.COMMA)
                    self.state = 622
                    localctx.payload = self.expr(0)
                    self.state = 627
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)

                self.state = 628
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 630
                self.match(CelestialParser.LPAREN)
                self.state = 631
                localctx.to = self.expr(0)
                self.state = 632
                self.match(CelestialParser.COMMA)
                self.state = 633
                self.match(CelestialParser.ETRANSFER)
                self.state = 634
                self.match(CelestialParser.COMMA)
                self.state = 635
                localctx.payload = self.expr(0)
                self.state = 636
                self.match(CelestialParser.RPAREN)
                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ExprContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self.array = None # ExprContext
            self.lhs = None # ExprContext
            self.method = None # IdenContext
            self.op = None # Token
            self.rhs = None # ExprContext
            self.contractName = None # IdenContext
            self.instmap = None # IdenContext
            self.condition = None # ExprContext
            self.thenBranch = None # ExprContext
            self.elseBranch = None # ExprContext
            self.logName = None # PrimitiveContext
            self.field = None # IdenContext
            self.index = None # ExprContext

        def primitive(self):
            return self.getTypedRuleContext(CelestialParser.PrimitiveContext,0)


        def LPAREN(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.LPAREN)
            else:
                return self.getToken(CelestialParser.LPAREN, i)

        def expr(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.ExprContext)
            else:
                return self.getTypedRuleContext(CelestialParser.ExprContext,i)


        def RPAREN(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.RPAREN)
            else:
                return self.getToken(CelestialParser.RPAREN, i)

        def iden(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.IdenContext)
            else:
                return self.getTypedRuleContext(CelestialParser.IdenContext,i)


        def rvalueList(self):
            return self.getTypedRuleContext(CelestialParser.RvalueListContext,0)


        def FORALL(self):
            return self.getToken(CelestialParser.FORALL, 0)

        def funParamList(self):
            return self.getTypedRuleContext(CelestialParser.FunParamListContext,0)


        def EXISTS(self):
            return self.getToken(CelestialParser.EXISTS, 0)

        def SUB(self):
            return self.getToken(CelestialParser.SUB, 0)

        def LNOT(self):
            return self.getToken(CelestialParser.LNOT, 0)

        def SAFEMOD(self):
            return self.getToken(CelestialParser.SAFEMOD, 0)

        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def SAFEDIV(self):
            return self.getToken(CelestialParser.SAFEDIV, 0)

        def SAFEMUL(self):
            return self.getToken(CelestialParser.SAFEMUL, 0)

        def SAFEADD(self):
            return self.getToken(CelestialParser.SAFEADD, 0)

        def SAFESUB(self):
            return self.getToken(CelestialParser.SAFESUB, 0)

        def NEW(self):
            return self.getToken(CelestialParser.NEW, 0)

        def DOT(self):
            return self.getToken(CelestialParser.DOT, 0)

        def ADD(self):
            return self.getToken(CelestialParser.ADD, 0)

        def ITE(self):
            return self.getToken(CelestialParser.ITE, 0)

        def DEFAULT(self):
            return self.getToken(CelestialParser.DEFAULT, 0)

        def datatype(self):
            return self.getTypedRuleContext(CelestialParser.DatatypeContext,0)


        def logcheck(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.LogcheckContext)
            else:
                return self.getTypedRuleContext(CelestialParser.LogcheckContext,i)


        def COLON(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COLON)
            else:
                return self.getToken(CelestialParser.COLON, i)

        def MUL(self):
            return self.getToken(CelestialParser.MUL, 0)

        def DIV(self):
            return self.getToken(CelestialParser.DIV, 0)

        def MOD(self):
            return self.getToken(CelestialParser.MOD, 0)

        def PLUS(self):
            return self.getToken(CelestialParser.PLUS, 0)

        def LT(self):
            return self.getToken(CelestialParser.LT, 0)

        def GT(self):
            return self.getToken(CelestialParser.GT, 0)

        def GE(self):
            return self.getToken(CelestialParser.GE, 0)

        def LE(self):
            return self.getToken(CelestialParser.LE, 0)

        def IN(self):
            return self.getToken(CelestialParser.IN, 0)

        def EQ(self):
            return self.getToken(CelestialParser.EQ, 0)

        def NE(self):
            return self.getToken(CelestialParser.NE, 0)

        def LAND(self):
            return self.getToken(CelestialParser.LAND, 0)

        def LOR(self):
            return self.getToken(CelestialParser.LOR, 0)

        def IMPL(self):
            return self.getToken(CelestialParser.IMPL, 0)

        def BIMPL(self):
            return self.getToken(CelestialParser.BIMPL, 0)

        def LBRACK(self):
            return self.getToken(CelestialParser.LBRACK, 0)

        def RBRACK(self):
            return self.getToken(CelestialParser.RBRACK, 0)

        def LENGTH(self):
            return self.getToken(CelestialParser.LENGTH, 0)

        def MAPUPD(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.MAPUPD)
            else:
                return self.getToken(CelestialParser.MAPUPD, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_expr

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterExpr" ):
                listener.enterExpr(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitExpr" ):
                listener.exitExpr(self)



    def expr(self, _p:int=0):
        _parentctx = self._ctx
        _parentState = self.state
        localctx = CelestialParser.ExprContext(self, self._ctx, _parentState)
        _prevctx = localctx
        _startState = 60
        self.enterRecursionRule(localctx, 60, self.RULE_expr, _p)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 759
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,74,self._ctx)
            if la_ == 1:
                self.state = 641
                self.primitive()
                pass

            elif la_ == 2:
                self.state = 642
                self.match(CelestialParser.LPAREN)
                self.state = 643
                self.expr(0)
                self.state = 644
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 3:
                self.state = 646
                localctx.method = self.iden()
                self.state = 647
                self.match(CelestialParser.LPAREN)
                self.state = 649
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 648
                    self.rvalueList()


                self.state = 651
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 4:
                self.state = 653
                self.match(CelestialParser.FORALL)
                self.state = 654
                self.match(CelestialParser.LPAREN)
                self.state = 655
                self.funParamList()
                self.state = 656
                self.match(CelestialParser.RPAREN)
                self.state = 657
                self.match(CelestialParser.LPAREN)
                self.state = 658
                self.expr(0)
                self.state = 659
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 5:
                self.state = 661
                self.match(CelestialParser.EXISTS)
                self.state = 662
                self.match(CelestialParser.LPAREN)
                self.state = 663
                self.funParamList()
                self.state = 664
                self.match(CelestialParser.RPAREN)
                self.state = 665
                self.match(CelestialParser.LPAREN)
                self.state = 666
                self.expr(0)
                self.state = 667
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 6:
                self.state = 669
                localctx.op = self._input.LT(1)
                _la = self._input.LA(1)
                if not(_la==CelestialParser.LNOT or _la==CelestialParser.SUB):
                    localctx.op = self._errHandler.recoverInline(self)
                else:
                    self._errHandler.reportMatch(self)
                    self.consume()
                self.state = 670
                self.expr(20)
                pass

            elif la_ == 7:
                self.state = 671
                self.match(CelestialParser.SAFEMOD)
                self.state = 672
                self.match(CelestialParser.LPAREN)
                self.state = 673
                localctx.lhs = self.expr(0)
                self.state = 674
                self.match(CelestialParser.COMMA)
                self.state = 675
                localctx.rhs = self.expr(0)
                self.state = 676
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 8:
                self.state = 678
                self.match(CelestialParser.SAFEDIV)
                self.state = 679
                self.match(CelestialParser.LPAREN)
                self.state = 680
                localctx.lhs = self.expr(0)
                self.state = 681
                self.match(CelestialParser.COMMA)
                self.state = 682
                localctx.rhs = self.expr(0)
                self.state = 683
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 9:
                self.state = 685
                self.match(CelestialParser.SAFEMUL)
                self.state = 686
                self.match(CelestialParser.LPAREN)
                self.state = 687
                localctx.lhs = self.expr(0)
                self.state = 688
                self.match(CelestialParser.COMMA)
                self.state = 689
                localctx.rhs = self.expr(0)
                self.state = 690
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 10:
                self.state = 692
                self.match(CelestialParser.SAFEADD)
                self.state = 693
                self.match(CelestialParser.LPAREN)
                self.state = 694
                localctx.lhs = self.expr(0)
                self.state = 695
                self.match(CelestialParser.COMMA)
                self.state = 696
                localctx.rhs = self.expr(0)
                self.state = 697
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 11:
                self.state = 699
                self.match(CelestialParser.SAFESUB)
                self.state = 700
                self.match(CelestialParser.LPAREN)
                self.state = 701
                localctx.lhs = self.expr(0)
                self.state = 702
                self.match(CelestialParser.COMMA)
                self.state = 703
                localctx.rhs = self.expr(0)
                self.state = 704
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 12:
                self.state = 706
                self.iden()
                self.state = 707
                self.match(CelestialParser.LPAREN)
                self.state = 708
                self.expr(0)
                self.state = 709
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 13:
                self.state = 711
                self.match(CelestialParser.NEW)
                self.state = 712
                localctx.contractName = self.iden()
                self.state = 713
                self.match(CelestialParser.LPAREN)
                self.state = 715
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 714
                    self.rvalueList()


                self.state = 717
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 14:
                self.state = 719
                localctx.instmap = self.iden()
                self.state = 720
                self.match(CelestialParser.DOT)
                self.state = 721
                self.match(CelestialParser.ADD)
                self.state = 722
                self.match(CelestialParser.LPAREN)
                self.state = 723
                self.match(CelestialParser.NEW)
                self.state = 724
                localctx.contractName = self.iden()
                self.state = 725
                self.match(CelestialParser.LPAREN)
                self.state = 727
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                if (((_la) & ~0x3f) == 0 and ((1 << _la) & ((1 << CelestialParser.ADDR) | (1 << CelestialParser.BALANCE) | (1 << CelestialParser.DEFAULT) | (1 << CelestialParser.EXISTS) | (1 << CelestialParser.FORALL) | (1 << CelestialParser.INT_MIN) | (1 << CelestialParser.INT_MAX) | (1 << CelestialParser.ITE) | (1 << CelestialParser.LOG) | (1 << CelestialParser.NEW) | (1 << CelestialParser.NOW) | (1 << CelestialParser.SAFEADD) | (1 << CelestialParser.SAFEDIV) | (1 << CelestialParser.SAFEMOD) | (1 << CelestialParser.SAFEMUL) | (1 << CelestialParser.SAFESUB) | (1 << CelestialParser.SENDER) | (1 << CelestialParser.THIS))) != 0) or ((((_la - 64)) & ~0x3f) == 0 and ((1 << (_la - 64)) & ((1 << (CelestialParser.UINT_MAX - 64)) | (1 << (CelestialParser.VALUE - 64)) | (1 << (CelestialParser.BoolLiteral - 64)) | (1 << (CelestialParser.IntLiteral - 64)) | (1 << (CelestialParser.NullLiteral - 64)) | (1 << (CelestialParser.StringLiteral - 64)) | (1 << (CelestialParser.LNOT - 64)) | (1 << (CelestialParser.SUB - 64)) | (1 << (CelestialParser.LPAREN - 64)) | (1 << (CelestialParser.Iden - 64)))) != 0):
                    self.state = 726
                    self.rvalueList()


                self.state = 729
                self.match(CelestialParser.RPAREN)
                self.state = 730
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 15:
                self.state = 732
                self.match(CelestialParser.ITE)
                self.state = 733
                self.match(CelestialParser.LPAREN)
                self.state = 734
                localctx.condition = self.expr(0)
                self.state = 735
                self.match(CelestialParser.COMMA)
                self.state = 736
                localctx.thenBranch = self.expr(0)
                self.state = 737
                self.match(CelestialParser.COMMA)
                self.state = 738
                localctx.elseBranch = self.expr(0)
                self.state = 739
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 16:
                self.state = 741
                self.match(CelestialParser.DEFAULT)
                self.state = 742
                self.match(CelestialParser.LPAREN)
                self.state = 743
                self.datatype(0)
                self.state = 744
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 17:
                self.state = 746
                self.logcheck()
                self.state = 752
                self._errHandler.sync(self)
                _alt = self._interp.adaptivePredict(self._input,73,self._ctx)
                while _alt!=2 and _alt!=ATN.INVALID_ALT_NUMBER:
                    if _alt==1:
                        self.state = 747
                        self.match(CelestialParser.COLON)
                        self.state = 748
                        self.match(CelestialParser.COLON)
                        self.state = 749
                        self.logcheck() 
                    self.state = 754
                    self._errHandler.sync(self)
                    _alt = self._interp.adaptivePredict(self._input,73,self._ctx)

                self.state = 755
                self.match(CelestialParser.COLON)
                self.state = 756
                self.match(CelestialParser.COLON)
                self.state = 757
                localctx.logName = self.primitive()
                pass


            self._ctx.stop = self._input.LT(-1)
            self.state = 810
            self._errHandler.sync(self)
            _alt = self._interp.adaptivePredict(self._input,77,self._ctx)
            while _alt!=2 and _alt!=ATN.INVALID_ALT_NUMBER:
                if _alt==1:
                    if self._parseListeners is not None:
                        self.triggerExitRuleEvent()
                    _prevctx = localctx
                    self.state = 808
                    self._errHandler.sync(self)
                    la_ = self._interp.adaptivePredict(self._input,76,self._ctx)
                    if la_ == 1:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 761
                        if not self.precpred(self._ctx, 19):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 19)")
                        self.state = 762
                        localctx.op = self._input.LT(1)
                        _la = self._input.LA(1)
                        if not(((((_la - 88)) & ~0x3f) == 0 and ((1 << (_la - 88)) & ((1 << (CelestialParser.MUL - 88)) | (1 << (CelestialParser.DIV - 88)) | (1 << (CelestialParser.MOD - 88)))) != 0)):
                            localctx.op = self._errHandler.recoverInline(self)
                        else:
                            self._errHandler.reportMatch(self)
                            self.consume()
                        self.state = 763
                        localctx.rhs = self.expr(20)
                        pass

                    elif la_ == 2:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 764
                        if not self.precpred(self._ctx, 15):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 15)")
                        self.state = 765
                        localctx.op = self._input.LT(1)
                        _la = self._input.LA(1)
                        if not(_la==CelestialParser.PLUS or _la==CelestialParser.SUB):
                            localctx.op = self._errHandler.recoverInline(self)
                        else:
                            self._errHandler.reportMatch(self)
                            self.consume()
                        self.state = 766
                        localctx.rhs = self.expr(16)
                        pass

                    elif la_ == 3:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 767
                        if not self.precpred(self._ctx, 12):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 12)")
                        self.state = 768
                        localctx.op = self._input.LT(1)
                        _la = self._input.LA(1)
                        if not(((((_la - 29)) & ~0x3f) == 0 and ((1 << (_la - 29)) & ((1 << (CelestialParser.IN - 29)) | (1 << (CelestialParser.LE - 29)) | (1 << (CelestialParser.GE - 29)) | (1 << (CelestialParser.LT - 29)) | (1 << (CelestialParser.GT - 29)))) != 0)):
                            localctx.op = self._errHandler.recoverInline(self)
                        else:
                            self._errHandler.reportMatch(self)
                            self.consume()
                        self.state = 769
                        localctx.rhs = self.expr(13)
                        pass

                    elif la_ == 4:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 770
                        if not self.precpred(self._ctx, 11):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 11)")
                        self.state = 771
                        localctx.op = self._input.LT(1)
                        _la = self._input.LA(1)
                        if not(_la==CelestialParser.EQ or _la==CelestialParser.NE):
                            localctx.op = self._errHandler.recoverInline(self)
                        else:
                            self._errHandler.reportMatch(self)
                            self.consume()
                        self.state = 772
                        localctx.rhs = self.expr(12)
                        pass

                    elif la_ == 5:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 773
                        if not self.precpred(self._ctx, 10):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 10)")
                        self.state = 774
                        localctx.op = self.match(CelestialParser.LAND)
                        self.state = 775
                        localctx.rhs = self.expr(11)
                        pass

                    elif la_ == 6:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 776
                        if not self.precpred(self._ctx, 9):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 9)")
                        self.state = 777
                        localctx.op = self.match(CelestialParser.LOR)
                        self.state = 778
                        localctx.rhs = self.expr(10)
                        pass

                    elif la_ == 7:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.lhs = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 779
                        if not self.precpred(self._ctx, 8):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 8)")
                        self.state = 780
                        localctx.op = self._input.LT(1)
                        _la = self._input.LA(1)
                        if not(_la==CelestialParser.IMPL or _la==CelestialParser.BIMPL):
                            localctx.op = self._errHandler.recoverInline(self)
                        else:
                            self._errHandler.reportMatch(self)
                            self.consume()
                        self.state = 781
                        localctx.rhs = self.expr(9)
                        pass

                    elif la_ == 8:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 782
                        if not self.precpred(self._ctx, 26):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 26)")
                        self.state = 783
                        self.match(CelestialParser.DOT)
                        self.state = 784
                        localctx.field = self.iden()
                        pass

                    elif la_ == 9:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.array = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 785
                        if not self.precpred(self._ctx, 25):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 25)")
                        self.state = 786
                        self.match(CelestialParser.LBRACK)
                        self.state = 787
                        localctx.index = self.expr(0)
                        self.state = 788
                        self.match(CelestialParser.RBRACK)
                        pass

                    elif la_ == 10:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        localctx.array = _prevctx
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 790
                        if not self.precpred(self._ctx, 24):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 24)")
                        self.state = 791
                        self.match(CelestialParser.DOT)
                        self.state = 792
                        self.match(CelestialParser.LENGTH)
                        self.state = 793
                        self.match(CelestialParser.LPAREN)
                        self.state = 794
                        self.match(CelestialParser.RPAREN)
                        pass

                    elif la_ == 11:
                        localctx = CelestialParser.ExprContext(self, _parentctx, _parentState)
                        self.pushNewRecursionContext(localctx, _startState, self.RULE_expr)
                        self.state = 795
                        if not self.precpred(self._ctx, 7):
                            from antlr4.error.Errors import FailedPredicateException
                            raise FailedPredicateException(self, "self.precpred(self._ctx, 7)")
                        self.state = 796
                        self.match(CelestialParser.MAPUPD)
                        self.state = 797
                        self.expr(0)
                        self.state = 805
                        self._errHandler.sync(self)
                        _alt = self._interp.adaptivePredict(self._input,75,self._ctx)
                        while _alt!=2 and _alt!=ATN.INVALID_ALT_NUMBER:
                            if _alt==1:
                                self.state = 798
                                self.match(CelestialParser.COMMA)
                                self.state = 799
                                self.expr(0)
                                self.state = 800
                                self.match(CelestialParser.MAPUPD)
                                self.state = 801
                                self.expr(0) 
                            self.state = 807
                            self._errHandler.sync(self)
                            _alt = self._interp.adaptivePredict(self._input,75,self._ctx)

                        pass

             
                self.state = 812
                self._errHandler.sync(self)
                _alt = self._interp.adaptivePredict(self._input,77,self._ctx)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.unrollRecursionContexts(_parentctx)
        return localctx


    class PrimitiveContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def iden(self):
            return self.getTypedRuleContext(CelestialParser.IdenContext,0)


        def VALUE(self):
            return self.getToken(CelestialParser.VALUE, 0)

        def BALANCE(self):
            return self.getToken(CelestialParser.BALANCE, 0)

        def SENDER(self):
            return self.getToken(CelestialParser.SENDER, 0)

        def LOG(self):
            return self.getToken(CelestialParser.LOG, 0)

        def INT_MIN(self):
            return self.getToken(CelestialParser.INT_MIN, 0)

        def INT_MAX(self):
            return self.getToken(CelestialParser.INT_MAX, 0)

        def UINT_MAX(self):
            return self.getToken(CelestialParser.UINT_MAX, 0)

        def NEW(self):
            return self.getToken(CelestialParser.NEW, 0)

        def LPAREN(self):
            return self.getToken(CelestialParser.LPAREN, 0)

        def RPAREN(self):
            return self.getToken(CelestialParser.RPAREN, 0)

        def BoolLiteral(self):
            return self.getToken(CelestialParser.BoolLiteral, 0)

        def IntLiteral(self):
            return self.getToken(CelestialParser.IntLiteral, 0)

        def NullLiteral(self):
            return self.getToken(CelestialParser.NullLiteral, 0)

        def StringLiteral(self):
            return self.getToken(CelestialParser.StringLiteral, 0)

        def THIS(self):
            return self.getToken(CelestialParser.THIS, 0)

        def NOW(self):
            return self.getToken(CelestialParser.NOW, 0)

        def ADDR(self):
            return self.getToken(CelestialParser.ADDR, 0)

        def getRuleIndex(self):
            return CelestialParser.RULE_primitive

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterPrimitive" ):
                listener.enterPrimitive(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitPrimitive" ):
                listener.exitPrimitive(self)




    def primitive(self):

        localctx = CelestialParser.PrimitiveContext(self, self._ctx, self.state)
        self.enterRule(localctx, 62, self.RULE_primitive)
        try:
            self.state = 849
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,78,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 813
                self.iden()
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 814
                self.match(CelestialParser.VALUE)
                pass

            elif la_ == 3:
                self.enterOuterAlt(localctx, 3)
                self.state = 815
                self.match(CelestialParser.BALANCE)
                pass

            elif la_ == 4:
                self.enterOuterAlt(localctx, 4)
                self.state = 816
                self.match(CelestialParser.SENDER)
                pass

            elif la_ == 5:
                self.enterOuterAlt(localctx, 5)
                self.state = 817
                self.match(CelestialParser.LOG)
                pass

            elif la_ == 6:
                self.enterOuterAlt(localctx, 6)
                self.state = 818
                self.match(CelestialParser.INT_MIN)
                pass

            elif la_ == 7:
                self.enterOuterAlt(localctx, 7)
                self.state = 819
                self.match(CelestialParser.INT_MAX)
                pass

            elif la_ == 8:
                self.enterOuterAlt(localctx, 8)
                self.state = 820
                self.match(CelestialParser.UINT_MAX)
                pass

            elif la_ == 9:
                self.enterOuterAlt(localctx, 9)
                self.state = 821
                self.match(CelestialParser.NEW)
                self.state = 822
                self.match(CelestialParser.LPAREN)
                self.state = 823
                self.iden()
                self.state = 824
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 10:
                self.enterOuterAlt(localctx, 10)
                self.state = 826
                self.match(CelestialParser.NEW)
                self.state = 827
                self.match(CelestialParser.LPAREN)
                self.state = 828
                self.match(CelestialParser.BALANCE)
                self.state = 829
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 11:
                self.enterOuterAlt(localctx, 11)
                self.state = 830
                self.match(CelestialParser.NEW)
                self.state = 831
                self.match(CelestialParser.LPAREN)
                self.state = 832
                self.match(CelestialParser.LOG)
                self.state = 833
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 12:
                self.enterOuterAlt(localctx, 12)
                self.state = 834
                self.match(CelestialParser.BoolLiteral)
                pass

            elif la_ == 13:
                self.enterOuterAlt(localctx, 13)
                self.state = 835
                self.match(CelestialParser.IntLiteral)
                pass

            elif la_ == 14:
                self.enterOuterAlt(localctx, 14)
                self.state = 836
                self.match(CelestialParser.NullLiteral)
                pass

            elif la_ == 15:
                self.enterOuterAlt(localctx, 15)
                self.state = 837
                self.match(CelestialParser.StringLiteral)
                pass

            elif la_ == 16:
                self.enterOuterAlt(localctx, 16)
                self.state = 838
                self.match(CelestialParser.THIS)
                pass

            elif la_ == 17:
                self.enterOuterAlt(localctx, 17)
                self.state = 839
                self.match(CelestialParser.NOW)
                pass

            elif la_ == 18:
                self.enterOuterAlt(localctx, 18)
                self.state = 840
                self.match(CelestialParser.ADDR)
                self.state = 841
                self.match(CelestialParser.LPAREN)
                self.state = 842
                self.match(CelestialParser.THIS)
                self.state = 843
                self.match(CelestialParser.RPAREN)
                pass

            elif la_ == 19:
                self.enterOuterAlt(localctx, 19)
                self.state = 844
                self.match(CelestialParser.ADDR)
                self.state = 845
                self.match(CelestialParser.LPAREN)
                self.state = 846
                self.iden()
                self.state = 847
                self.match(CelestialParser.RPAREN)
                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class UnnamedTupleBodyContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self._rvalue = None # RvalueContext
            self.fields = list() # of RvalueContexts

        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def rvalue(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.RvalueContext)
            else:
                return self.getTypedRuleContext(CelestialParser.RvalueContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_unnamedTupleBody

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterUnnamedTupleBody" ):
                listener.enterUnnamedTupleBody(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitUnnamedTupleBody" ):
                listener.exitUnnamedTupleBody(self)




    def unnamedTupleBody(self):

        localctx = CelestialParser.UnnamedTupleBodyContext(self, self._ctx, self.state)
        self.enterRule(localctx, 64, self.RULE_unnamedTupleBody)
        self._la = 0 # Token type
        try:
            self.state = 861
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,80,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 851
                localctx._rvalue = self.rvalue()
                localctx.fields.append(localctx._rvalue)
                self.state = 852
                self.match(CelestialParser.COMMA)
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 854
                localctx._rvalue = self.rvalue()
                localctx.fields.append(localctx._rvalue)
                self.state = 857 
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while True:
                    self.state = 855
                    self.match(CelestialParser.COMMA)
                    self.state = 856
                    localctx._rvalue = self.rvalue()
                    localctx.fields.append(localctx._rvalue)
                    self.state = 859 
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)
                    if not (_la==CelestialParser.COMMA):
                        break

                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class NamedTupleBodyContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser
            self._iden = None # IdenContext
            self.names = list() # of IdenContexts
            self._rvalue = None # RvalueContext
            self.values = list() # of RvalueContexts

        def ASSIGN(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.ASSIGN)
            else:
                return self.getToken(CelestialParser.ASSIGN, i)

        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def iden(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.IdenContext)
            else:
                return self.getTypedRuleContext(CelestialParser.IdenContext,i)


        def rvalue(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.RvalueContext)
            else:
                return self.getTypedRuleContext(CelestialParser.RvalueContext,i)


        def getRuleIndex(self):
            return CelestialParser.RULE_namedTupleBody

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterNamedTupleBody" ):
                listener.enterNamedTupleBody(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitNamedTupleBody" ):
                listener.exitNamedTupleBody(self)




    def namedTupleBody(self):

        localctx = CelestialParser.NamedTupleBodyContext(self, self._ctx, self.state)
        self.enterRule(localctx, 66, self.RULE_namedTupleBody)
        self._la = 0 # Token type
        try:
            self.state = 880
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,82,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 863
                localctx._iden = self.iden()
                localctx.names.append(localctx._iden)
                self.state = 864
                self.match(CelestialParser.ASSIGN)
                self.state = 865
                localctx._rvalue = self.rvalue()
                localctx.values.append(localctx._rvalue)
                self.state = 866
                self.match(CelestialParser.COMMA)
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 868
                localctx._iden = self.iden()
                localctx.names.append(localctx._iden)
                self.state = 869
                self.match(CelestialParser.ASSIGN)
                self.state = 870
                localctx._rvalue = self.rvalue()
                localctx.values.append(localctx._rvalue)
                self.state = 876 
                self._errHandler.sync(self)
                _la = self._input.LA(1)
                while True:
                    self.state = 871
                    self.match(CelestialParser.COMMA)
                    self.state = 872
                    localctx._iden = self.iden()
                    localctx.names.append(localctx._iden)
                    self.state = 873
                    self.match(CelestialParser.ASSIGN)
                    self.state = 874
                    localctx._rvalue = self.rvalue()
                    localctx.values.append(localctx._rvalue)
                    self.state = 878 
                    self._errHandler.sync(self)
                    _la = self._input.LA(1)
                    if not (_la==CelestialParser.COMMA):
                        break

                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class RvalueListContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def rvalue(self, i:int=None):
            if i is None:
                return self.getTypedRuleContexts(CelestialParser.RvalueContext)
            else:
                return self.getTypedRuleContext(CelestialParser.RvalueContext,i)


        def COMMA(self, i:int=None):
            if i is None:
                return self.getTokens(CelestialParser.COMMA)
            else:
                return self.getToken(CelestialParser.COMMA, i)

        def getRuleIndex(self):
            return CelestialParser.RULE_rvalueList

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterRvalueList" ):
                listener.enterRvalueList(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitRvalueList" ):
                listener.exitRvalueList(self)




    def rvalueList(self):

        localctx = CelestialParser.RvalueListContext(self, self._ctx, self.state)
        self.enterRule(localctx, 68, self.RULE_rvalueList)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 882
            self.rvalue()
            self.state = 887
            self._errHandler.sync(self)
            _la = self._input.LA(1)
            while _la==CelestialParser.COMMA:
                self.state = 883
                self.match(CelestialParser.COMMA)
                self.state = 884
                self.rvalue()
                self.state = 889
                self._errHandler.sync(self)
                _la = self._input.LA(1)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class RvalueContext(ParserRuleContext):

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def expr(self):
            return self.getTypedRuleContext(CelestialParser.ExprContext,0)


        def getRuleIndex(self):
            return CelestialParser.RULE_rvalue

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterRvalue" ):
                listener.enterRvalue(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitRvalue" ):
                listener.exitRvalue(self)




    def rvalue(self):

        localctx = CelestialParser.RvalueContext(self, self._ctx, self.state)
        self.enterRule(localctx, 70, self.RULE_rvalue)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 890
            self.expr(0)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx



    def sempred(self, localctx:RuleContext, ruleIndex:int, predIndex:int):
        if self._predicates == None:
            self._predicates = dict()
        self._predicates[2] = self.datatype_sempred
        self._predicates[28] = self.lvalue_sempred
        self._predicates[30] = self.expr_sempred
        pred = self._predicates.get(ruleIndex, None)
        if pred is None:
            raise Exception("No predicate with index:" + str(ruleIndex))
        else:
            return pred(localctx, predIndex)

    def datatype_sempred(self, localctx:DatatypeContext, predIndex:int):
            if predIndex == 0:
                return self.precpred(self._ctx, 11)
         

    def lvalue_sempred(self, localctx:LvalueContext, predIndex:int):
            if predIndex == 1:
                return self.precpred(self._ctx, 2)
         

            if predIndex == 2:
                return self.precpred(self._ctx, 1)
         

    def expr_sempred(self, localctx:ExprContext, predIndex:int):
            if predIndex == 3:
                return self.precpred(self._ctx, 19)
         

            if predIndex == 4:
                return self.precpred(self._ctx, 15)
         

            if predIndex == 5:
                return self.precpred(self._ctx, 12)
         

            if predIndex == 6:
                return self.precpred(self._ctx, 11)
         

            if predIndex == 7:
                return self.precpred(self._ctx, 10)
         

            if predIndex == 8:
                return self.precpred(self._ctx, 9)
         

            if predIndex == 9:
                return self.precpred(self._ctx, 8)
         

            if predIndex == 10:
                return self.precpred(self._ctx, 26)
         

            if predIndex == 11:
                return self.precpred(self._ctx, 25)
         

            if predIndex == 12:
                return self.precpred(self._ctx, 24)
         

            if predIndex == 13:
                return self.precpred(self._ctx, 7)
         




