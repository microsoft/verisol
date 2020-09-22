# Generated from .\Compiler\CelestialLexer.g4 by ANTLR 4.8
from antlr4 import *
from io import StringIO
from typing.io import TextIO
import sys



def serializedATN():
    with StringIO() as buf:
        buf.write("\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2n")
        buf.write("\u0365\b\1\4\2\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7")
        buf.write("\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t\13\4\f\t\f\4\r\t\r")
        buf.write("\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22\4\23")
        buf.write("\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30")
        buf.write("\4\31\t\31\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36")
        buf.write("\t\36\4\37\t\37\4 \t \4!\t!\4\"\t\"\4#\t#\4$\t$\4%\t%")
        buf.write("\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4,\t,\4-\t-\4.")
        buf.write("\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64")
        buf.write("\t\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:")
        buf.write("\4;\t;\4<\t<\4=\t=\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\t")
        buf.write("C\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I\tI\4J\tJ\4K\tK\4L\t")
        buf.write("L\4M\tM\4N\tN\4O\tO\4P\tP\4Q\tQ\4R\tR\4S\tS\4T\tT\4U\t")
        buf.write("U\4V\tV\4W\tW\4X\tX\4Y\tY\4Z\tZ\4[\t[\4\\\t\\\4]\t]\4")
        buf.write("^\t^\4_\t_\4`\t`\4a\ta\4b\tb\4c\tc\4d\td\4e\te\4f\tf\4")
        buf.write("g\tg\4h\th\4i\ti\4j\tj\4k\tk\4l\tl\4m\tm\4n\tn\4o\to\4")
        buf.write("p\tp\4q\tq\4r\tr\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3")
        buf.write("\3\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5")
        buf.write("\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3")
        buf.write("\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\t")
        buf.write("\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13")
        buf.write("\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\r\3\r")
        buf.write("\3\r\3\r\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\16\3\16\3\16")
        buf.write("\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\20\3\20\3\20")
        buf.write("\3\20\3\20\3\20\3\20\3\20\3\21\3\21\3\21\3\21\3\22\3\22")
        buf.write("\3\22\3\22\3\22\3\22\3\22\3\23\3\23\3\23\3\23\3\23\3\23")
        buf.write("\3\23\3\23\3\24\3\24\3\24\3\24\3\24\3\25\3\25\3\25\3\25")
        buf.write("\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\26\3\26\3\26")
        buf.write("\3\26\3\26\3\26\3\26\3\26\3\26\3\27\3\27\3\27\3\27\3\27")
        buf.write("\3\27\3\27\3\30\3\30\3\30\3\30\3\30\3\30\3\31\3\31\3\31")
        buf.write("\3\31\3\31\3\31\3\31\3\31\3\32\3\32\3\32\3\32\3\32\3\32")
        buf.write("\3\32\3\33\3\33\3\33\3\33\3\33\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\35\3\35\3\35\3\35\3\35\3\35")
        buf.write("\3\35\3\36\3\36\3\36\3\36\3\37\3\37\3\37\3\37\3\37\3\37")
        buf.write("\3\37\3 \3 \3 \3 \3 \3 \3 \3 \3 \3!\3!\3!\3\"\3\"\3\"")
        buf.write("\3#\3#\3#\3#\3#\3#\3#\3#\3$\3$\3$\3$\3$\3$\3$\3$\3%\3")
        buf.write("%\3%\3%\3&\3&\3&\3&\3&\3&\3&\3&\3&\3&\3\'\3\'\3\'\3\'")
        buf.write("\3\'\3(\3(\3(\3(\3(\3(\3)\3)\3)\3)\3)\3)\3)\3*\3*\3*\3")
        buf.write("*\3+\3+\3+\3+\3+\3+\3+\3+\3+\3,\3,\3,\3,\3,\3,\3,\3,\3")
        buf.write(",\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3-\3-\3-\3-\3.\3.\3.\3")
        buf.write(".\3/\3/\3/\3/\3/\3/\3/\3/\3\60\3\60\3\60\3\60\3\61\3\61")
        buf.write("\3\61\3\61\3\61\3\62\3\62\3\62\3\62\3\63\3\63\3\63\3\63")
        buf.write("\3\63\3\63\3\64\3\64\3\64\3\64\3\64\3\64\3\64\3\64\3\65")
        buf.write("\3\65\3\65\3\65\3\65\3\65\3\65\3\66\3\66\3\66\3\66\3\66")
        buf.write("\3\67\3\67\3\67\3\67\3\67\3\67\3\67\38\38\38\38\38\38")
        buf.write("\38\38\39\39\39\39\39\39\39\3:\3:\3:\3:\3:\3:\3:\3:\3")
        buf.write(":\3;\3;\3;\3;\3;\3;\3;\3;\3;\3<\3<\3<\3<\3<\3<\3<\3<\3")
        buf.write("<\3=\3=\3=\3=\3=\3=\3=\3=\3=\3>\3>\3>\3>\3>\3>\3>\3>\3")
        buf.write(">\3?\3?\3?\3?\3?\3@\3@\3@\3@\3@\3@\3@\3A\3A\3A\3A\3A\3")
        buf.write("B\3B\3B\3B\3B\3B\3B\3C\3C\3C\3C\3C\3D\3D\3D\3D\3D\3D\3")
        buf.write("D\3D\3D\3D\3D\3E\3E\3E\3E\3E\3E\3E\3E\3E\3F\3F\3F\3F\3")
        buf.write("F\3F\3G\3G\3G\3G\3G\3G\3G\3G\3G\5G\u02d0\nG\3H\6H\u02d3")
        buf.write("\nH\rH\16H\u02d4\3I\3I\3I\3I\3I\3J\3J\5J\u02de\nJ\3J\3")
        buf.write("J\3K\6K\u02e3\nK\rK\16K\u02e4\3L\3L\5L\u02e9\nL\3M\3M")
        buf.write("\3M\3N\3N\3O\3O\3O\3P\3P\3P\3Q\3Q\3Q\3R\3R\3R\3R\3S\3")
        buf.write("S\3S\3S\3S\3T\3T\3T\3U\3U\3U\3V\3V\3V\3W\3W\3W\3X\3X\3")
        buf.write("Y\3Y\3Z\3Z\3Z\3[\3[\3\\\3\\\3\\\3]\3]\3]\3^\3^\3_\3_\3")
        buf.write("`\3`\3a\3a\3b\3b\3c\3c\3d\3d\3e\3e\3f\3f\3g\3g\3h\3h\3")
        buf.write("i\3i\3j\3j\3k\3k\3l\3l\3m\3m\7m\u033d\nm\fm\16m\u0340")
        buf.write("\13m\3n\3n\3o\3o\3p\6p\u0347\np\rp\16p\u0348\3p\3p\3q")
        buf.write("\3q\3q\3q\7q\u0351\nq\fq\16q\u0354\13q\3q\3q\3q\3q\3q")
        buf.write("\3r\3r\3r\3r\7r\u035f\nr\fr\16r\u0362\13r\3r\3r\3\u0352")
        buf.write("\2s\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r")
        buf.write("\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30")
        buf.write("/\31\61\32\63\33\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'")
        buf.write("M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63e\64g\65i\66k\67m8o9q")
        buf.write(":s;u<w=y>{?}@\177A\u0081B\u0083C\u0085D\u0087E\u0089F")
        buf.write("\u008bG\u008dH\u008fI\u0091J\u0093K\u0095\2\u0097\2\u0099")
        buf.write("\2\u009bL\u009dM\u009fN\u00a1O\u00a3P\u00a5Q\u00a7R\u00a9")
        buf.write("S\u00abT\u00adU\u00afV\u00b1W\u00b3X\u00b5Y\u00b7Z\u00b9")
        buf.write("[\u00bb\\\u00bd]\u00bf^\u00c1_\u00c3`\u00c5a\u00c7b\u00c9")
        buf.write("c\u00cbd\u00cde\u00cff\u00d1g\u00d3h\u00d5i\u00d7j\u00d9")
        buf.write("k\u00db\2\u00dd\2\u00dfl\u00e1m\u00e3n\3\2\b\3\2\62;\4")
        buf.write("\2$$^^\5\2C\\aac|\6\2\62;C\\aac|\5\2\13\f\16\17\"\"\4")
        buf.write("\2\f\f\17\17\2\u0368\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2")
        buf.write("\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2")
        buf.write("\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31")
        buf.write("\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2")
        buf.write("\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3")
        buf.write("\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2")
        buf.write("\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3")
        buf.write("\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G")
        buf.write("\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2")
        buf.write("Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2Y\3\2\2\2")
        buf.write("\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3\2\2")
        buf.write("\2\2e\3\2\2\2\2g\3\2\2\2\2i\3\2\2\2\2k\3\2\2\2\2m\3\2")
        buf.write("\2\2\2o\3\2\2\2\2q\3\2\2\2\2s\3\2\2\2\2u\3\2\2\2\2w\3")
        buf.write("\2\2\2\2y\3\2\2\2\2{\3\2\2\2\2}\3\2\2\2\2\177\3\2\2\2")
        buf.write("\2\u0081\3\2\2\2\2\u0083\3\2\2\2\2\u0085\3\2\2\2\2\u0087")
        buf.write("\3\2\2\2\2\u0089\3\2\2\2\2\u008b\3\2\2\2\2\u008d\3\2\2")
        buf.write("\2\2\u008f\3\2\2\2\2\u0091\3\2\2\2\2\u0093\3\2\2\2\2\u009b")
        buf.write("\3\2\2\2\2\u009d\3\2\2\2\2\u009f\3\2\2\2\2\u00a1\3\2\2")
        buf.write("\2\2\u00a3\3\2\2\2\2\u00a5\3\2\2\2\2\u00a7\3\2\2\2\2\u00a9")
        buf.write("\3\2\2\2\2\u00ab\3\2\2\2\2\u00ad\3\2\2\2\2\u00af\3\2\2")
        buf.write("\2\2\u00b1\3\2\2\2\2\u00b3\3\2\2\2\2\u00b5\3\2\2\2\2\u00b7")
        buf.write("\3\2\2\2\2\u00b9\3\2\2\2\2\u00bb\3\2\2\2\2\u00bd\3\2\2")
        buf.write("\2\2\u00bf\3\2\2\2\2\u00c1\3\2\2\2\2\u00c3\3\2\2\2\2\u00c5")
        buf.write("\3\2\2\2\2\u00c7\3\2\2\2\2\u00c9\3\2\2\2\2\u00cb\3\2\2")
        buf.write("\2\2\u00cd\3\2\2\2\2\u00cf\3\2\2\2\2\u00d1\3\2\2\2\2\u00d3")
        buf.write("\3\2\2\2\2\u00d5\3\2\2\2\2\u00d7\3\2\2\2\2\u00d9\3\2\2")
        buf.write("\2\2\u00df\3\2\2\2\2\u00e1\3\2\2\2\2\u00e3\3\2\2\2\3\u00e5")
        buf.write("\3\2\2\2\5\u00ed\3\2\2\2\7\u00f2\3\2\2\2\t\u00f7\3\2\2")
        buf.write("\2\13\u00fd\3\2\2\2\r\u0106\3\2\2\2\17\u010b\3\2\2\2\21")
        buf.write("\u0111\3\2\2\2\23\u011a\3\2\2\2\25\u011e\3\2\2\2\27\u0125")
        buf.write("\3\2\2\2\31\u012e\3\2\2\2\33\u0136\3\2\2\2\35\u013c\3")
        buf.write("\2\2\2\37\u0144\3\2\2\2!\u014c\3\2\2\2#\u0150\3\2\2\2")
        buf.write("%\u0157\3\2\2\2\'\u015f\3\2\2\2)\u0164\3\2\2\2+\u0170")
        buf.write("\3\2\2\2-\u0179\3\2\2\2/\u0180\3\2\2\2\61\u0186\3\2\2")
        buf.write("\2\63\u018e\3\2\2\2\65\u0195\3\2\2\2\67\u019a\3\2\2\2")
        buf.write("9\u01a4\3\2\2\2;\u01ab\3\2\2\2=\u01af\3\2\2\2?\u01b6\3")
        buf.write("\2\2\2A\u01bf\3\2\2\2C\u01c2\3\2\2\2E\u01c5\3\2\2\2G\u01cd")
        buf.write("\3\2\2\2I\u01d5\3\2\2\2K\u01d9\3\2\2\2M\u01e3\3\2\2\2")
        buf.write("O\u01e8\3\2\2\2Q\u01ee\3\2\2\2S\u01f5\3\2\2\2U\u01f9\3")
        buf.write("\2\2\2W\u0202\3\2\2\2Y\u0215\3\2\2\2[\u0219\3\2\2\2]\u021d")
        buf.write("\3\2\2\2_\u0225\3\2\2\2a\u0229\3\2\2\2c\u022e\3\2\2\2")
        buf.write("e\u0232\3\2\2\2g\u0238\3\2\2\2i\u0240\3\2\2\2k\u0247\3")
        buf.write("\2\2\2m\u024c\3\2\2\2o\u0253\3\2\2\2q\u025b\3\2\2\2s\u0262")
        buf.write("\3\2\2\2u\u026b\3\2\2\2w\u0274\3\2\2\2y\u027d\3\2\2\2")
        buf.write("{\u0286\3\2\2\2}\u028f\3\2\2\2\177\u0294\3\2\2\2\u0081")
        buf.write("\u029b\3\2\2\2\u0083\u02a0\3\2\2\2\u0085\u02a7\3\2\2\2")
        buf.write("\u0087\u02ac\3\2\2\2\u0089\u02b7\3\2\2\2\u008b\u02c0\3")
        buf.write("\2\2\2\u008d\u02cf\3\2\2\2\u008f\u02d2\3\2\2\2\u0091\u02d6")
        buf.write("\3\2\2\2\u0093\u02db\3\2\2\2\u0095\u02e2\3\2\2\2\u0097")
        buf.write("\u02e8\3\2\2\2\u0099\u02ea\3\2\2\2\u009b\u02ed\3\2\2\2")
        buf.write("\u009d\u02ef\3\2\2\2\u009f\u02f2\3\2\2\2\u00a1\u02f5\3")
        buf.write("\2\2\2\u00a3\u02f8\3\2\2\2\u00a5\u02fc\3\2\2\2\u00a7\u0301")
        buf.write("\3\2\2\2\u00a9\u0304\3\2\2\2\u00ab\u0307\3\2\2\2\u00ad")
        buf.write("\u030a\3\2\2\2\u00af\u030d\3\2\2\2\u00b1\u030f\3\2\2\2")
        buf.write("\u00b3\u0311\3\2\2\2\u00b5\u0314\3\2\2\2\u00b7\u0316\3")
        buf.write("\2\2\2\u00b9\u0319\3\2\2\2\u00bb\u031c\3\2\2\2\u00bd\u031e")
        buf.write("\3\2\2\2\u00bf\u0320\3\2\2\2\u00c1\u0322\3\2\2\2\u00c3")
        buf.write("\u0324\3\2\2\2\u00c5\u0326\3\2\2\2\u00c7\u0328\3\2\2\2")
        buf.write("\u00c9\u032a\3\2\2\2\u00cb\u032c\3\2\2\2\u00cd\u032e\3")
        buf.write("\2\2\2\u00cf\u0330\3\2\2\2\u00d1\u0332\3\2\2\2\u00d3\u0334")
        buf.write("\3\2\2\2\u00d5\u0336\3\2\2\2\u00d7\u0338\3\2\2\2\u00d9")
        buf.write("\u033a\3\2\2\2\u00db\u0341\3\2\2\2\u00dd\u0343\3\2\2\2")
        buf.write("\u00df\u0346\3\2\2\2\u00e1\u034c\3\2\2\2\u00e3\u035a\3")
        buf.write("\2\2\2\u00e5\u00e6\7c\2\2\u00e6\u00e7\7f\2\2\u00e7\u00e8")
        buf.write("\7f\2\2\u00e8\u00e9\7t\2\2\u00e9\u00ea\7g\2\2\u00ea\u00eb")
        buf.write("\7u\2\2\u00eb\u00ec\7u\2\2\u00ec\4\3\2\2\2\u00ed\u00ee")
        buf.write("\7d\2\2\u00ee\u00ef\7q\2\2\u00ef\u00f0\7q\2\2\u00f0\u00f1")
        buf.write("\7n\2\2\u00f1\6\3\2\2\2\u00f2\u00f3\7g\2\2\u00f3\u00f4")
        buf.write("\7p\2\2\u00f4\u00f5\7w\2\2\u00f5\u00f6\7o\2\2\u00f6\b")
        buf.write("\3\2\2\2\u00f7\u00f8\7g\2\2\u00f8\u00f9\7x\2\2\u00f9\u00fa")
        buf.write("\7g\2\2\u00fa\u00fb\7p\2\2\u00fb\u00fc\7v\2\2\u00fc\n")
        buf.write("\3\2\2\2\u00fd\u00fe\7g\2\2\u00fe\u00ff\7x\2\2\u00ff\u0100")
        buf.write("\7g\2\2\u0100\u0101\7p\2\2\u0101\u0102\7v\2\2\u0102\u0103")
        buf.write("\7n\2\2\u0103\u0104\7q\2\2\u0104\u0105\7i\2\2\u0105\f")
        buf.write("\3\2\2\2\u0106\u0107\7w\2\2\u0107\u0108\7k\2\2\u0108\u0109")
        buf.write("\7p\2\2\u0109\u010a\7v\2\2\u010a\16\3\2\2\2\u010b\u010c")
        buf.write("\7w\2\2\u010c\u010d\7k\2\2\u010d\u010e\7p\2\2\u010e\u010f")
        buf.write("\7v\2\2\u010f\u0110\7:\2\2\u0110\20\3\2\2\2\u0111\u0112")
        buf.write("\7k\2\2\u0112\u0113\7p\2\2\u0113\u0114\7u\2\2\u0114\u0115")
        buf.write("\7v\2\2\u0115\u0116\7a\2\2\u0116\u0117\7o\2\2\u0117\u0118")
        buf.write("\7c\2\2\u0118\u0119\7r\2\2\u0119\22\3\2\2\2\u011a\u011b")
        buf.write("\7k\2\2\u011b\u011c\7p\2\2\u011c\u011d\7v\2\2\u011d\24")
        buf.write("\3\2\2\2\u011e\u011f\7u\2\2\u011f\u0120\7v\2\2\u0120\u0121")
        buf.write("\7t\2\2\u0121\u0122\7k\2\2\u0122\u0123\7p\2\2\u0123\u0124")
        buf.write("\7i\2\2\u0124\26\3\2\2\2\u0125\u0126\7e\2\2\u0126\u0127")
        buf.write("\7q\2\2\u0127\u0128\7p\2\2\u0128\u0129\7v\2\2\u0129\u012a")
        buf.write("\7t\2\2\u012a\u012b\7c\2\2\u012b\u012c\7e\2\2\u012c\u012d")
        buf.write("\7v\2\2\u012d\30\3\2\2\2\u012e\u012f\7o\2\2\u012f\u0130")
        buf.write("\7c\2\2\u0130\u0131\7r\2\2\u0131\u0132\7r\2\2\u0132\u0133")
        buf.write("\7k\2\2\u0133\u0134\7p\2\2\u0134\u0135\7i\2\2\u0135\32")
        buf.write("\3\2\2\2\u0136\u0137\7d\2\2\u0137\u0138\7{\2\2\u0138\u0139")
        buf.write("\7v\2\2\u0139\u013a\7g\2\2\u013a\u013b\7u\2\2\u013b\34")
        buf.write("\3\2\2\2\u013c\u013d\7d\2\2\u013d\u013e\7{\2\2\u013e\u013f")
        buf.write("\7v\2\2\u013f\u0140\7g\2\2\u0140\u0141\7u\2\2\u0141\u0142")
        buf.write("\7\64\2\2\u0142\u0143\7\62\2\2\u0143\36\3\2\2\2\u0144")
        buf.write("\u0145\7d\2\2\u0145\u0146\7{\2\2\u0146\u0147\7v\2\2\u0147")
        buf.write("\u0148\7g\2\2\u0148\u0149\7u\2\2\u0149\u014a\7\65\2\2")
        buf.write("\u014a\u014b\7\64\2\2\u014b \3\2\2\2\u014c\u014d\7c\2")
        buf.write("\2\u014d\u014e\7f\2\2\u014e\u014f\7f\2\2\u014f\"\3\2\2")
        buf.write("\2\u0150\u0151\7c\2\2\u0151\u0152\7u\2\2\u0152\u0153\7")
        buf.write("u\2\2\u0153\u0154\7g\2\2\u0154\u0155\7t\2\2\u0155\u0156")
        buf.write("\7v\2\2\u0156$\3\2\2\2\u0157\u0158\7d\2\2\u0158\u0159")
        buf.write("\7c\2\2\u0159\u015a\7n\2\2\u015a\u015b\7c\2\2\u015b\u015c")
        buf.write("\7p\2\2\u015c\u015d\7e\2\2\u015d\u015e\7g\2\2\u015e&\3")
        buf.write("\2\2\2\u015f\u0160\7e\2\2\u0160\u0161\7c\2\2\u0161\u0162")
        buf.write("\7n\2\2\u0162\u0163\7n\2\2\u0163(\3\2\2\2\u0164\u0165")
        buf.write("\7e\2\2\u0165\u0166\7q\2\2\u0166\u0167\7p\2\2\u0167\u0168")
        buf.write("\7u\2\2\u0168\u0169\7v\2\2\u0169\u016a\7t\2\2\u016a\u016b")
        buf.write("\7w\2\2\u016b\u016c\7e\2\2\u016c\u016d\7v\2\2\u016d\u016e")
        buf.write("\7q\2\2\u016e\u016f\7t\2\2\u016f*\3\2\2\2\u0170\u0171")
        buf.write("\7e\2\2\u0171\u0172\7q\2\2\u0172\u0173\7p\2\2\u0173\u0174")
        buf.write("\7v\2\2\u0174\u0175\7c\2\2\u0175\u0176\7k\2\2\u0176\u0177")
        buf.write("\7p\2\2\u0177\u0178\7u\2\2\u0178,\3\2\2\2\u0179\u017a")
        buf.write("\7e\2\2\u017a\u017b\7t\2\2\u017b\u017c\7g\2\2\u017c\u017d")
        buf.write("\7f\2\2\u017d\u017e\7k\2\2\u017e\u017f\7v\2\2\u017f.\3")
        buf.write("\2\2\2\u0180\u0181\7f\2\2\u0181\u0182\7g\2\2\u0182\u0183")
        buf.write("\7d\2\2\u0183\u0184\7k\2\2\u0184\u0185\7v\2\2\u0185\60")
        buf.write("\3\2\2\2\u0186\u0187\7f\2\2\u0187\u0188\7g\2\2\u0188\u0189")
        buf.write("\7h\2\2\u0189\u018a\7c\2\2\u018a\u018b\7w\2\2\u018b\u018c")
        buf.write("\7n\2\2\u018c\u018d\7v\2\2\u018d\62\3\2\2\2\u018e\u018f")
        buf.write("\7f\2\2\u018f\u0190\7g\2\2\u0190\u0191\7n\2\2\u0191\u0192")
        buf.write("\7g\2\2\u0192\u0193\7v\2\2\u0193\u0194\7g\2\2\u0194\64")
        buf.write("\3\2\2\2\u0195\u0196\7g\2\2\u0196\u0197\7n\2\2\u0197\u0198")
        buf.write("\7u\2\2\u0198\u0199\7g\2\2\u0199\66\3\2\2\2\u019a\u019b")
        buf.write("\7g\2\2\u019b\u019c\7V\2\2\u019c\u019d\7t\2\2\u019d\u019e")
        buf.write("\7c\2\2\u019e\u019f\7p\2\2\u019f\u01a0\7u\2\2\u01a0\u01a1")
        buf.write("\7h\2\2\u01a1\u01a2\7g\2\2\u01a2\u01a3\7t\2\2\u01a38\3")
        buf.write("\2\2\2\u01a4\u01a5\7g\2\2\u01a5\u01a6\7z\2\2\u01a6\u01a7")
        buf.write("\7k\2\2\u01a7\u01a8\7u\2\2\u01a8\u01a9\7v\2\2\u01a9\u01aa")
        buf.write("\7u\2\2\u01aa:\3\2\2\2\u01ab\u01ac\7h\2\2\u01ac\u01ad")
        buf.write("\7q\2\2\u01ad\u01ae\7t\2\2\u01ae<\3\2\2\2\u01af\u01b0")
        buf.write("\7h\2\2\u01b0\u01b1\7q\2\2\u01b1\u01b2\7t\2\2\u01b2\u01b3")
        buf.write("\7c\2\2\u01b3\u01b4\7n\2\2\u01b4\u01b5\7n\2\2\u01b5>\3")
        buf.write("\2\2\2\u01b6\u01b7\7h\2\2\u01b7\u01b8\7w\2\2\u01b8\u01b9")
        buf.write("\7p\2\2\u01b9\u01ba\7e\2\2\u01ba\u01bb\7v\2\2\u01bb\u01bc")
        buf.write("\7k\2\2\u01bc\u01bd\7q\2\2\u01bd\u01be\7p\2\2\u01be@\3")
        buf.write("\2\2\2\u01bf\u01c0\7k\2\2\u01c0\u01c1\7h\2\2\u01c1B\3")
        buf.write("\2\2\2\u01c2\u01c3\7k\2\2\u01c3\u01c4\7p\2\2\u01c4D\3")
        buf.write("\2\2\2\u01c5\u01c6\7k\2\2\u01c6\u01c7\7p\2\2\u01c7\u01c8")
        buf.write("\7v\2\2\u01c8\u01c9\7a\2\2\u01c9\u01ca\7o\2\2\u01ca\u01cb")
        buf.write("\7k\2\2\u01cb\u01cc\7p\2\2\u01ccF\3\2\2\2\u01cd\u01ce")
        buf.write("\7k\2\2\u01ce\u01cf\7p\2\2\u01cf\u01d0\7v\2\2\u01d0\u01d1")
        buf.write("\7a\2\2\u01d1\u01d2\7o\2\2\u01d2\u01d3\7c\2\2\u01d3\u01d4")
        buf.write("\7z\2\2\u01d4H\3\2\2\2\u01d5\u01d6\7k\2\2\u01d6\u01d7")
        buf.write("\7v\2\2\u01d7\u01d8\7g\2\2\u01d8J\3\2\2\2\u01d9\u01da")
        buf.write("\7k\2\2\u01da\u01db\7p\2\2\u01db\u01dc\7x\2\2\u01dc\u01dd")
        buf.write("\7c\2\2\u01dd\u01de\7t\2\2\u01de\u01df\7k\2\2\u01df\u01e0")
        buf.write("\7c\2\2\u01e0\u01e1\7p\2\2\u01e1\u01e2\7v\2\2\u01e2L\3")
        buf.write("\2\2\2\u01e3\u01e4\7m\2\2\u01e4\u01e5\7g\2\2\u01e5\u01e6")
        buf.write("\7{\2\2\u01e6\u01e7\7u\2\2\u01e7N\3\2\2\2\u01e8\u01e9")
        buf.write("\7n\2\2\u01e9\u01ea\7g\2\2\u01ea\u01eb\7o\2\2\u01eb\u01ec")
        buf.write("\7o\2\2\u01ec\u01ed\7c\2\2\u01edP\3\2\2\2\u01ee\u01ef")
        buf.write("\7n\2\2\u01ef\u01f0\7g\2\2\u01f0\u01f1\7p\2\2\u01f1\u01f2")
        buf.write("\7i\2\2\u01f2\u01f3\7v\2\2\u01f3\u01f4\7j\2\2\u01f4R\3")
        buf.write("\2\2\2\u01f5\u01f6\7n\2\2\u01f6\u01f7\7q\2\2\u01f7\u01f8")
        buf.write("\7i\2\2\u01f8T\3\2\2\2\u01f9\u01fa\7o\2\2\u01fa\u01fb")
        buf.write("\7q\2\2\u01fb\u01fc\7f\2\2\u01fc\u01fd\7k\2\2\u01fd\u01fe")
        buf.write("\7h\2\2\u01fe\u01ff\7k\2\2\u01ff\u0200\7g\2\2\u0200\u0201")
        buf.write("\7u\2\2\u0201V\3\2\2\2\u0202\u0203\7o\2\2\u0203\u0204")
        buf.write("\7q\2\2\u0204\u0205\7f\2\2\u0205\u0206\7k\2\2\u0206\u0207")
        buf.write("\7h\2\2\u0207\u0208\7k\2\2\u0208\u0209\7g\2\2\u0209\u020a")
        buf.write("\7u\2\2\u020a\u020b\7a\2\2\u020b\u020c\7c\2\2\u020c\u020d")
        buf.write("\7f\2\2\u020d\u020e\7f\2\2\u020e\u020f\7t\2\2\u020f\u0210")
        buf.write("\7g\2\2\u0210\u0211\7u\2\2\u0211\u0212\7u\2\2\u0212\u0213")
        buf.write("\7g\2\2\u0213\u0214\7u\2\2\u0214X\3\2\2\2\u0215\u0216")
        buf.write("\7p\2\2\u0216\u0217\7g\2\2\u0217\u0218\7y\2\2\u0218Z\3")
        buf.write("\2\2\2\u0219\u021a\7p\2\2\u021a\u021b\7q\2\2\u021b\u021c")
        buf.write("\7y\2\2\u021c\\\3\2\2\2\u021d\u021e\7r\2\2\u021e\u021f")
        buf.write("\7c\2\2\u021f\u0220\7{\2\2\u0220\u0221\7c\2\2\u0221\u0222")
        buf.write("\7d\2\2\u0222\u0223\7n\2\2\u0223\u0224\7g\2\2\u0224^\3")
        buf.write("\2\2\2\u0225\u0226\7r\2\2\u0226\u0227\7q\2\2\u0227\u0228")
        buf.write("\7r\2\2\u0228`\3\2\2\2\u0229\u022a\7r\2\2\u022a\u022b")
        buf.write("\7q\2\2\u022b\u022c\7u\2\2\u022c\u022d\7v\2\2\u022db\3")
        buf.write("\2\2\2\u022e\u022f\7r\2\2\u022f\u0230\7t\2\2\u0230\u0231")
        buf.write("\7g\2\2\u0231d\3\2\2\2\u0232\u0233\7r\2\2\u0233\u0234")
        buf.write("\7t\2\2\u0234\u0235\7k\2\2\u0235\u0236\7p\2\2\u0236\u0237")
        buf.write("\7v\2\2\u0237f\3\2\2\2\u0238\u0239\7r\2\2\u0239\u023a")
        buf.write("\7t\2\2\u023a\u023b\7k\2\2\u023b\u023c\7x\2\2\u023c\u023d")
        buf.write("\7c\2\2\u023d\u023e\7v\2\2\u023e\u023f\7g\2\2\u023fh\3")
        buf.write("\2\2\2\u0240\u0241\7r\2\2\u0241\u0242\7w\2\2\u0242\u0243")
        buf.write("\7d\2\2\u0243\u0244\7n\2\2\u0244\u0245\7k\2\2\u0245\u0246")
        buf.write("\7e\2\2\u0246j\3\2\2\2\u0247\u0248\7r\2\2\u0248\u0249")
        buf.write("\7w\2\2\u0249\u024a\7u\2\2\u024a\u024b\7j\2\2\u024bl\3")
        buf.write("\2\2\2\u024c\u024d\7t\2\2\u024d\u024e\7g\2\2\u024e\u024f")
        buf.write("\7v\2\2\u024f\u0250\7w\2\2\u0250\u0251\7t\2\2\u0251\u0252")
        buf.write("\7p\2\2\u0252n\3\2\2\2\u0253\u0254\7t\2\2\u0254\u0255")
        buf.write("\7g\2\2\u0255\u0256\7v\2\2\u0256\u0257\7w\2\2\u0257\u0258")
        buf.write("\7t\2\2\u0258\u0259\7p\2\2\u0259\u025a\7u\2\2\u025ap\3")
        buf.write("\2\2\2\u025b\u025c\7t\2\2\u025c\u025d\7g\2\2\u025d\u025e")
        buf.write("\7x\2\2\u025e\u025f\7g\2\2\u025f\u0260\7t\2\2\u0260\u0261")
        buf.write("\7v\2\2\u0261r\3\2\2\2\u0262\u0263\7u\2\2\u0263\u0264")
        buf.write("\7c\2\2\u0264\u0265\7h\2\2\u0265\u0266\7g\2\2\u0266\u0267")
        buf.write("\7a\2\2\u0267\u0268\7c\2\2\u0268\u0269\7f\2\2\u0269\u026a")
        buf.write("\7f\2\2\u026at\3\2\2\2\u026b\u026c\7u\2\2\u026c\u026d")
        buf.write("\7c\2\2\u026d\u026e\7h\2\2\u026e\u026f\7g\2\2\u026f\u0270")
        buf.write("\7a\2\2\u0270\u0271\7f\2\2\u0271\u0272\7k\2\2\u0272\u0273")
        buf.write("\7x\2\2\u0273v\3\2\2\2\u0274\u0275\7u\2\2\u0275\u0276")
        buf.write("\7c\2\2\u0276\u0277\7h\2\2\u0277\u0278\7g\2\2\u0278\u0279")
        buf.write("\7a\2\2\u0279\u027a\7o\2\2\u027a\u027b\7q\2\2\u027b\u027c")
        buf.write("\7f\2\2\u027cx\3\2\2\2\u027d\u027e\7u\2\2\u027e\u027f")
        buf.write("\7c\2\2\u027f\u0280\7h\2\2\u0280\u0281\7g\2\2\u0281\u0282")
        buf.write("\7a\2\2\u0282\u0283\7o\2\2\u0283\u0284\7w\2\2\u0284\u0285")
        buf.write("\7n\2\2\u0285z\3\2\2\2\u0286\u0287\7u\2\2\u0287\u0288")
        buf.write("\7c\2\2\u0288\u0289\7h\2\2\u0289\u028a\7g\2\2\u028a\u028b")
        buf.write("\7a\2\2\u028b\u028c\7u\2\2\u028c\u028d\7w\2\2\u028d\u028e")
        buf.write("\7d\2\2\u028e|\3\2\2\2\u028f\u0290\7u\2\2\u0290\u0291")
        buf.write("\7g\2\2\u0291\u0292\7p\2\2\u0292\u0293\7f\2\2\u0293~\3")
        buf.write("\2\2\2\u0294\u0295\7u\2\2\u0295\u0296\7g\2\2\u0296\u0297")
        buf.write("\7p\2\2\u0297\u0298\7f\2\2\u0298\u0299\7g\2\2\u0299\u029a")
        buf.write("\7t\2\2\u029a\u0080\3\2\2\2\u029b\u029c\7u\2\2\u029c\u029d")
        buf.write("\7r\2\2\u029d\u029e\7g\2\2\u029e\u029f\7e\2\2\u029f\u0082")
        buf.write("\3\2\2\2\u02a0\u02a1\7u\2\2\u02a1\u02a2\7v\2\2\u02a2\u02a3")
        buf.write("\7t\2\2\u02a3\u02a4\7w\2\2\u02a4\u02a5\7e\2\2\u02a5\u02a6")
        buf.write("\7v\2\2\u02a6\u0084\3\2\2\2\u02a7\u02a8\7v\2\2\u02a8\u02a9")
        buf.write("\7j\2\2\u02a9\u02aa\7k\2\2\u02aa\u02ab\7u\2\2\u02ab\u0086")
        buf.write("\3\2\2\2\u02ac\u02ad\7v\2\2\u02ad\u02ae\7z\2\2\u02ae\u02af")
        buf.write("\7a\2\2\u02af\u02b0\7t\2\2\u02b0\u02b1\7g\2\2\u02b1\u02b2")
        buf.write("\7x\2\2\u02b2\u02b3\7g\2\2\u02b3\u02b4\7t\2\2\u02b4\u02b5")
        buf.write("\7v\2\2\u02b5\u02b6\7u\2\2\u02b6\u0088\3\2\2\2\u02b7\u02b8")
        buf.write("\7w\2\2\u02b8\u02b9\7k\2\2\u02b9\u02ba\7p\2\2\u02ba\u02bb")
        buf.write("\7v\2\2\u02bb\u02bc\7a\2\2\u02bc\u02bd\7o\2\2\u02bd\u02be")
        buf.write("\7c\2\2\u02be\u02bf\7z\2\2\u02bf\u008a\3\2\2\2\u02c0\u02c1")
        buf.write("\7x\2\2\u02c1\u02c2\7c\2\2\u02c2\u02c3\7n\2\2\u02c3\u02c4")
        buf.write("\7w\2\2\u02c4\u02c5\7g\2\2\u02c5\u008c\3\2\2\2\u02c6\u02c7")
        buf.write("\7v\2\2\u02c7\u02c8\7t\2\2\u02c8\u02c9\7w\2\2\u02c9\u02d0")
        buf.write("\7g\2\2\u02ca\u02cb\7h\2\2\u02cb\u02cc\7c\2\2\u02cc\u02cd")
        buf.write("\7n\2\2\u02cd\u02ce\7u\2\2\u02ce\u02d0\7g\2\2\u02cf\u02c6")
        buf.write("\3\2\2\2\u02cf\u02ca\3\2\2\2\u02d0\u008e\3\2\2\2\u02d1")
        buf.write("\u02d3\t\2\2\2\u02d2\u02d1\3\2\2\2\u02d3\u02d4\3\2\2\2")
        buf.write("\u02d4\u02d2\3\2\2\2\u02d4\u02d5\3\2\2\2\u02d5\u0090\3")
        buf.write("\2\2\2\u02d6\u02d7\7p\2\2\u02d7\u02d8\7w\2\2\u02d8\u02d9")
        buf.write("\7n\2\2\u02d9\u02da\7n\2\2\u02da\u0092\3\2\2\2\u02db\u02dd")
        buf.write("\7$\2\2\u02dc\u02de\5\u0095K\2\u02dd\u02dc\3\2\2\2\u02dd")
        buf.write("\u02de\3\2\2\2\u02de\u02df\3\2\2\2\u02df\u02e0\7$\2\2")
        buf.write("\u02e0\u0094\3\2\2\2\u02e1\u02e3\5\u0097L\2\u02e2\u02e1")
        buf.write("\3\2\2\2\u02e3\u02e4\3\2\2\2\u02e4\u02e2\3\2\2\2\u02e4")
        buf.write("\u02e5\3\2\2\2\u02e5\u0096\3\2\2\2\u02e6\u02e9\n\3\2\2")
        buf.write("\u02e7\u02e9\5\u0099M\2\u02e8\u02e6\3\2\2\2\u02e8\u02e7")
        buf.write("\3\2\2\2\u02e9\u0098\3\2\2\2\u02ea\u02eb\7^\2\2\u02eb")
        buf.write("\u02ec\13\2\2\2\u02ec\u009a\3\2\2\2\u02ed\u02ee\7#\2\2")
        buf.write("\u02ee\u009c\3\2\2\2\u02ef\u02f0\7(\2\2\u02f0\u02f1\7")
        buf.write("(\2\2\u02f1\u009e\3\2\2\2\u02f2\u02f3\7~\2\2\u02f3\u02f4")
        buf.write("\7~\2\2\u02f4\u00a0\3\2\2\2\u02f5\u02f6\7?\2\2\u02f6\u02f7")
        buf.write("\7@\2\2\u02f7\u00a2\3\2\2\2\u02f8\u02f9\7?\2\2\u02f9\u02fa")
        buf.write("\7?\2\2\u02fa\u02fb\7@\2\2\u02fb\u00a4\3\2\2\2\u02fc\u02fd")
        buf.write("\7>\2\2\u02fd\u02fe\7?\2\2\u02fe\u02ff\7?\2\2\u02ff\u0300")
        buf.write("\7@\2\2\u0300\u00a6\3\2\2\2\u0301\u0302\7?\2\2\u0302\u0303")
        buf.write("\7?\2\2\u0303\u00a8\3\2\2\2\u0304\u0305\7#\2\2\u0305\u0306")
        buf.write("\7?\2\2\u0306\u00aa\3\2\2\2\u0307\u0308\7>\2\2\u0308\u0309")
        buf.write("\7?\2\2\u0309\u00ac\3\2\2\2\u030a\u030b\7@\2\2\u030b\u030c")
        buf.write("\7?\2\2\u030c\u00ae\3\2\2\2\u030d\u030e\7>\2\2\u030e\u00b0")
        buf.write("\3\2\2\2\u030f\u0310\7@\2\2\u0310\u00b2\3\2\2\2\u0311")
        buf.write("\u0312\7/\2\2\u0312\u0313\7@\2\2\u0313\u00b4\3\2\2\2\u0314")
        buf.write("\u0315\7?\2\2\u0315\u00b6\3\2\2\2\u0316\u0317\7-\2\2\u0317")
        buf.write("\u0318\7?\2\2\u0318\u00b8\3\2\2\2\u0319\u031a\7/\2\2\u031a")
        buf.write("\u031b\7?\2\2\u031b\u00ba\3\2\2\2\u031c\u031d\7-\2\2\u031d")
        buf.write("\u00bc\3\2\2\2\u031e\u031f\7/\2\2\u031f\u00be\3\2\2\2")
        buf.write("\u0320\u0321\7,\2\2\u0321\u00c0\3\2\2\2\u0322\u0323\7")
        buf.write("\61\2\2\u0323\u00c2\3\2\2\2\u0324\u0325\7\'\2\2\u0325")
        buf.write("\u00c4\3\2\2\2\u0326\u0327\7}\2\2\u0327\u00c6\3\2\2\2")
        buf.write("\u0328\u0329\7\177\2\2\u0329\u00c8\3\2\2\2\u032a\u032b")
        buf.write("\7]\2\2\u032b\u00ca\3\2\2\2\u032c\u032d\7_\2\2\u032d\u00cc")
        buf.write("\3\2\2\2\u032e\u032f\7*\2\2\u032f\u00ce\3\2\2\2\u0330")
        buf.write("\u0331\7+\2\2\u0331\u00d0\3\2\2\2\u0332\u0333\7=\2\2\u0333")
        buf.write("\u00d2\3\2\2\2\u0334\u0335\7.\2\2\u0335\u00d4\3\2\2\2")
        buf.write("\u0336\u0337\7\60\2\2\u0337\u00d6\3\2\2\2\u0338\u0339")
        buf.write("\7<\2\2\u0339\u00d8\3\2\2\2\u033a\u033e\5\u00dbn\2\u033b")
        buf.write("\u033d\5\u00ddo\2\u033c\u033b\3\2\2\2\u033d\u0340\3\2")
        buf.write("\2\2\u033e\u033c\3\2\2\2\u033e\u033f\3\2\2\2\u033f\u00da")
        buf.write("\3\2\2\2\u0340\u033e\3\2\2\2\u0341\u0342\t\4\2\2\u0342")
        buf.write("\u00dc\3\2\2\2\u0343\u0344\t\5\2\2\u0344\u00de\3\2\2\2")
        buf.write("\u0345\u0347\t\6\2\2\u0346\u0345\3\2\2\2\u0347\u0348\3")
        buf.write("\2\2\2\u0348\u0346\3\2\2\2\u0348\u0349\3\2\2\2\u0349\u034a")
        buf.write("\3\2\2\2\u034a\u034b\bp\2\2\u034b\u00e0\3\2\2\2\u034c")
        buf.write("\u034d\7\61\2\2\u034d\u034e\7,\2\2\u034e\u0352\3\2\2\2")
        buf.write("\u034f\u0351\13\2\2\2\u0350\u034f\3\2\2\2\u0351\u0354")
        buf.write("\3\2\2\2\u0352\u0353\3\2\2\2\u0352\u0350\3\2\2\2\u0353")
        buf.write("\u0355\3\2\2\2\u0354\u0352\3\2\2\2\u0355\u0356\7,\2\2")
        buf.write("\u0356\u0357\7\61\2\2\u0357\u0358\3\2\2\2\u0358\u0359")
        buf.write("\bq\3\2\u0359\u00e2\3\2\2\2\u035a\u035b\7\61\2\2\u035b")
        buf.write("\u035c\7\61\2\2\u035c\u0360\3\2\2\2\u035d\u035f\n\7\2")
        buf.write("\2\u035e\u035d\3\2\2\2\u035f\u0362\3\2\2\2\u0360\u035e")
        buf.write("\3\2\2\2\u0360\u0361\3\2\2\2\u0361\u0363\3\2\2\2\u0362")
        buf.write("\u0360\3\2\2\2\u0363\u0364\br\3\2\u0364\u00e4\3\2\2\2")
        buf.write("\f\2\u02cf\u02d4\u02dd\u02e4\u02e8\u033e\u0348\u0352\u0360")
        buf.write("\4\b\2\2\2\3\2")
        return buf.getvalue()


class CelestialLexer(Lexer):

    atn = ATNDeserializer().deserialize(serializedATN())

    decisionsToDFA = [ DFA(ds, i) for i, ds in enumerate(atn.decisionToState) ]

    ADDR = 1
    BOOL = 2
    ENUM = 3
    EVENT = 4
    EVENTLOG = 5
    UINT = 6
    UINT8 = 7
    INSTMAP = 8
    INT = 9
    STRING = 10
    CONTRACT = 11
    MAP = 12
    BYTES = 13
    BYTES20 = 14
    BYTES32 = 15
    ADD = 16
    ASSERT = 17
    BALANCE = 18
    CALL = 19
    CONSTR = 20
    CONTAINS = 21
    CREDIT = 22
    DEBIT = 23
    DEFAULT = 24
    DELETE = 25
    ELSE = 26
    ETRANSFER = 27
    EXISTS = 28
    FOR = 29
    FORALL = 30
    FUNCTION = 31
    IF = 32
    IN = 33
    INT_MIN = 34
    INT_MAX = 35
    ITE = 36
    INVARIANT = 37
    KEYS = 38
    LEMMA = 39
    LENGTH = 40
    LOG = 41
    MODIFIES = 42
    MODIFIESA = 43
    NEW = 44
    NOW = 45
    PAYABLE = 46
    POP = 47
    POST = 48
    PRE = 49
    PRINT = 50
    PRIVATE = 51
    PUBLIC = 52
    PUSH = 53
    RETURN = 54
    RETURNS = 55
    REVERT = 56
    SAFEADD = 57
    SAFEDIV = 58
    SAFEMOD = 59
    SAFEMUL = 60
    SAFESUB = 61
    SEND = 62
    SENDER = 63
    SPEC = 64
    STRUCT = 65
    THIS = 66
    TXREVERTS = 67
    UINT_MAX = 68
    VALUE = 69
    BoolLiteral = 70
    IntLiteral = 71
    NullLiteral = 72
    StringLiteral = 73
    LNOT = 74
    LAND = 75
    LOR = 76
    MAPUPD = 77
    IMPL = 78
    BIMPL = 79
    EQ = 80
    NE = 81
    LE = 82
    GE = 83
    LT = 84
    GT = 85
    RARROW = 86
    ASSIGN = 87
    INSERT = 88
    REMOVE = 89
    PLUS = 90
    SUB = 91
    MUL = 92
    DIV = 93
    MOD = 94
    LBRACE = 95
    RBRACE = 96
    LBRACK = 97
    RBRACK = 98
    LPAREN = 99
    RPAREN = 100
    SEMI = 101
    COMMA = 102
    DOT = 103
    COLON = 104
    Iden = 105
    Whitespace = 106
    BlockComment = 107
    LineComment = 108

    channelNames = [ u"DEFAULT_TOKEN_CHANNEL", u"HIDDEN" ]

    modeNames = [ "DEFAULT_MODE" ]

    literalNames = [ "<INVALID>",
            "'address'", "'bool'", "'enum'", "'event'", "'eventlog'", "'uint'", 
            "'uint8'", "'inst_map'", "'int'", "'string'", "'contract'", 
            "'mapping'", "'bytes'", "'bytes20'", "'bytes32'", "'add'", "'assert'", 
            "'balance'", "'call'", "'constructor'", "'contains'", "'credit'", 
            "'debit'", "'default'", "'delete'", "'else'", "'eTransfer'", 
            "'exists'", "'for'", "'forall'", "'function'", "'if'", "'in'", 
            "'int_min'", "'int_max'", "'ite'", "'invariant'", "'keys'", 
            "'lemma'", "'length'", "'log'", "'modifies'", "'modifies_addresses'", 
            "'new'", "'now'", "'payable'", "'pop'", "'post'", "'pre'", "'print'", 
            "'private'", "'public'", "'push'", "'return'", "'returns'", 
            "'revert'", "'safe_add'", "'safe_div'", "'safe_mod'", "'safe_mul'", 
            "'safe_sub'", "'send'", "'sender'", "'spec'", "'struct'", "'this'", 
            "'tx_reverts'", "'uint_max'", "'value'", "'null'", "'!'", "'&&'", 
            "'||'", "'=>'", "'==>'", "'<==>'", "'=='", "'!='", "'<='", "'>='", 
            "'<'", "'>'", "'->'", "'='", "'+='", "'-='", "'+'", "'-'", "'*'", 
            "'/'", "'%'", "'{'", "'}'", "'['", "']'", "'('", "')'", "';'", 
            "','", "'.'", "':'" ]

    symbolicNames = [ "<INVALID>",
            "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", "UINT", "UINT8", 
            "INSTMAP", "INT", "STRING", "CONTRACT", "MAP", "BYTES", "BYTES20", 
            "BYTES32", "ADD", "ASSERT", "BALANCE", "CALL", "CONSTR", "CONTAINS", 
            "CREDIT", "DEBIT", "DEFAULT", "DELETE", "ELSE", "ETRANSFER", 
            "EXISTS", "FOR", "FORALL", "FUNCTION", "IF", "IN", "INT_MIN", 
            "INT_MAX", "ITE", "INVARIANT", "KEYS", "LEMMA", "LENGTH", "LOG", 
            "MODIFIES", "MODIFIESA", "NEW", "NOW", "PAYABLE", "POP", "POST", 
            "PRE", "PRINT", "PRIVATE", "PUBLIC", "PUSH", "RETURN", "RETURNS", 
            "REVERT", "SAFEADD", "SAFEDIV", "SAFEMOD", "SAFEMUL", "SAFESUB", 
            "SEND", "SENDER", "SPEC", "STRUCT", "THIS", "TXREVERTS", "UINT_MAX", 
            "VALUE", "BoolLiteral", "IntLiteral", "NullLiteral", "StringLiteral", 
            "LNOT", "LAND", "LOR", "MAPUPD", "IMPL", "BIMPL", "EQ", "NE", 
            "LE", "GE", "LT", "GT", "RARROW", "ASSIGN", "INSERT", "REMOVE", 
            "PLUS", "SUB", "MUL", "DIV", "MOD", "LBRACE", "RBRACE", "LBRACK", 
            "RBRACK", "LPAREN", "RPAREN", "SEMI", "COMMA", "DOT", "COLON", 
            "Iden", "Whitespace", "BlockComment", "LineComment" ]

    ruleNames = [ "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", "UINT", "UINT8", 
                  "INSTMAP", "INT", "STRING", "CONTRACT", "MAP", "BYTES", 
                  "BYTES20", "BYTES32", "ADD", "ASSERT", "BALANCE", "CALL", 
                  "CONSTR", "CONTAINS", "CREDIT", "DEBIT", "DEFAULT", "DELETE", 
                  "ELSE", "ETRANSFER", "EXISTS", "FOR", "FORALL", "FUNCTION", 
                  "IF", "IN", "INT_MIN", "INT_MAX", "ITE", "INVARIANT", 
                  "KEYS", "LEMMA", "LENGTH", "LOG", "MODIFIES", "MODIFIESA", 
                  "NEW", "NOW", "PAYABLE", "POP", "POST", "PRE", "PRINT", 
                  "PRIVATE", "PUBLIC", "PUSH", "RETURN", "RETURNS", "REVERT", 
                  "SAFEADD", "SAFEDIV", "SAFEMOD", "SAFEMUL", "SAFESUB", 
                  "SEND", "SENDER", "SPEC", "STRUCT", "THIS", "TXREVERTS", 
                  "UINT_MAX", "VALUE", "BoolLiteral", "IntLiteral", "NullLiteral", 
                  "StringLiteral", "StringCharacters", "StringCharacter", 
                  "EscapeSequence", "LNOT", "LAND", "LOR", "MAPUPD", "IMPL", 
                  "BIMPL", "EQ", "NE", "LE", "GE", "LT", "GT", "RARROW", 
                  "ASSIGN", "INSERT", "REMOVE", "PLUS", "SUB", "MUL", "DIV", 
                  "MOD", "LBRACE", "RBRACE", "LBRACK", "RBRACK", "LPAREN", 
                  "RPAREN", "SEMI", "COMMA", "DOT", "COLON", "Iden", "PLetter", 
                  "PLetterOrDigit", "Whitespace", "BlockComment", "LineComment" ]

    grammarFileName = "CelestialLexer.g4"

    def __init__(self, input=None, output:TextIO = sys.stdout):
        super().__init__(input, output)
        self.checkVersion("4.8")
        self._interp = LexerATNSimulator(self, self.atn, self.decisionsToDFA, PredictionContextCache())
        self._actions = None
        self._predicates = None


