# Generated from .\Compiler\CelestialLexer.g4 by ANTLR 4.8
from antlr4 import *
from io import StringIO
from typing.io import TextIO
import sys



def serializedATN():
    with StringIO() as buf:
        buf.write("\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2j")
        buf.write("\u0341\b\1\4\2\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7")
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
        buf.write("g\tg\4h\th\4i\ti\4j\tj\4k\tk\4l\tl\4m\tm\4n\tn\3\2\3\2")
        buf.write("\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\4\3\4\3")
        buf.write("\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6")
        buf.write("\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3")
        buf.write("\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n")
        buf.write("\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13")
        buf.write("\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\r\3\r\3\r\3\r\3\16")
        buf.write("\3\16\3\16\3\16\3\16\3\16\3\16\3\17\3\17\3\17\3\17\3\17")
        buf.write("\3\17\3\17\3\17\3\20\3\20\3\20\3\20\3\20\3\21\3\21\3\21")
        buf.write("\3\21\3\21\3\21\3\21\3\21\3\21\3\21\3\21\3\21\3\22\3\22")
        buf.write("\3\22\3\22\3\22\3\22\3\22\3\22\3\22\3\23\3\23\3\23\3\23")
        buf.write("\3\23\3\23\3\23\3\24\3\24\3\24\3\24\3\24\3\24\3\25\3\25")
        buf.write("\3\25\3\25\3\25\3\25\3\25\3\25\3\26\3\26\3\26\3\26\3\26")
        buf.write("\3\26\3\26\3\27\3\27\3\27\3\27\3\27\3\30\3\30\3\30\3\30")
        buf.write("\3\30\3\30\3\30\3\30\3\30\3\30\3\31\3\31\3\31\3\31\3\31")
        buf.write("\3\31\3\31\3\32\3\32\3\32\3\32\3\33\3\33\3\33\3\33\3\33")
        buf.write("\3\33\3\33\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34\3\34")
        buf.write("\3\35\3\35\3\35\3\36\3\36\3\36\3\37\3\37\3\37\3\37\3\37")
        buf.write("\3\37\3\37\3\37\3 \3 \3 \3 \3 \3 \3 \3 \3!\3!\3!\3!\3")
        buf.write("\"\3\"\3\"\3\"\3\"\3\"\3\"\3\"\3\"\3\"\3#\3#\3#\3#\3#")
        buf.write("\3$\3$\3$\3$\3$\3$\3%\3%\3%\3%\3%\3%\3%\3&\3&\3&\3&\3")
        buf.write("\'\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3(\3(\3(\3(\3(\3(\3")
        buf.write("(\3(\3(\3(\3(\3(\3(\3(\3(\3(\3(\3(\3(\3)\3)\3)\3)\3*\3")
        buf.write("*\3*\3*\3+\3+\3+\3+\3+\3+\3+\3+\3,\3,\3,\3,\3-\3-\3-\3")
        buf.write("-\3-\3.\3.\3.\3.\3/\3/\3/\3/\3/\3/\3\60\3\60\3\60\3\60")
        buf.write("\3\60\3\60\3\60\3\60\3\61\3\61\3\61\3\61\3\61\3\61\3\61")
        buf.write("\3\62\3\62\3\62\3\62\3\62\3\63\3\63\3\63\3\63\3\63\3\63")
        buf.write("\3\63\3\64\3\64\3\64\3\64\3\64\3\64\3\64\3\64\3\65\3\65")
        buf.write("\3\65\3\65\3\65\3\65\3\65\3\66\3\66\3\66\3\66\3\66\3\66")
        buf.write("\3\66\3\66\3\66\3\67\3\67\3\67\3\67\3\67\3\67\3\67\3\67")
        buf.write("\3\67\38\38\38\38\38\38\38\38\38\39\39\39\39\39\39\39")
        buf.write("\39\39\3:\3:\3:\3:\3:\3:\3:\3:\3:\3;\3;\3;\3;\3;\3<\3")
        buf.write("<\3<\3<\3<\3<\3<\3=\3=\3=\3=\3=\3>\3>\3>\3>\3>\3>\3>\3")
        buf.write("?\3?\3?\3?\3?\3@\3@\3@\3@\3@\3@\3@\3@\3@\3@\3@\3A\3A\3")
        buf.write("A\3A\3A\3A\3A\3A\3A\3B\3B\3B\3B\3B\3B\3C\3C\3C\3C\3C\3")
        buf.write("C\3C\3C\3C\5C\u02ac\nC\3D\6D\u02af\nD\rD\16D\u02b0\3E")
        buf.write("\3E\3E\3E\3E\3F\3F\5F\u02ba\nF\3F\3F\3G\6G\u02bf\nG\r")
        buf.write("G\16G\u02c0\3H\3H\5H\u02c5\nH\3I\3I\3I\3J\3J\3K\3K\3K")
        buf.write("\3L\3L\3L\3M\3M\3M\3N\3N\3N\3N\3O\3O\3O\3O\3O\3P\3P\3")
        buf.write("P\3Q\3Q\3Q\3R\3R\3R\3S\3S\3S\3T\3T\3U\3U\3V\3V\3V\3W\3")
        buf.write("W\3X\3X\3X\3Y\3Y\3Y\3Z\3Z\3[\3[\3\\\3\\\3]\3]\3^\3^\3")
        buf.write("_\3_\3`\3`\3a\3a\3b\3b\3c\3c\3d\3d\3e\3e\3f\3f\3g\3g\3")
        buf.write("h\3h\3i\3i\7i\u0319\ni\fi\16i\u031c\13i\3j\3j\3k\3k\3")
        buf.write("l\6l\u0323\nl\rl\16l\u0324\3l\3l\3m\3m\3m\3m\7m\u032d")
        buf.write("\nm\fm\16m\u0330\13m\3m\3m\3m\3m\3m\3n\3n\3n\3n\7n\u033b")
        buf.write("\nn\fn\16n\u033e\13n\3n\3n\3\u032e\2o\3\3\5\4\7\5\t\6")
        buf.write("\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20")
        buf.write("\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65")
        buf.write("\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60")
        buf.write("_\61a\62c\63e\64g\65i\66k\67m8o9q:s;u<w=y>{?}@\177A\u0081")
        buf.write("B\u0083C\u0085D\u0087E\u0089F\u008bG\u008d\2\u008f\2\u0091")
        buf.write("\2\u0093H\u0095I\u0097J\u0099K\u009bL\u009dM\u009fN\u00a1")
        buf.write("O\u00a3P\u00a5Q\u00a7R\u00a9S\u00abT\u00adU\u00afV\u00b1")
        buf.write("W\u00b3X\u00b5Y\u00b7Z\u00b9[\u00bb\\\u00bd]\u00bf^\u00c1")
        buf.write("_\u00c3`\u00c5a\u00c7b\u00c9c\u00cbd\u00cde\u00cff\u00d1")
        buf.write("g\u00d3\2\u00d5\2\u00d7h\u00d9i\u00dbj\3\2\b\3\2\62;\4")
        buf.write("\2$$^^\5\2C\\aac|\6\2\62;C\\aac|\5\2\13\f\16\17\"\"\4")
        buf.write("\2\f\f\17\17\2\u0344\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2")
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
        buf.write("\3\2\2\2\2\u0089\3\2\2\2\2\u008b\3\2\2\2\2\u0093\3\2\2")
        buf.write("\2\2\u0095\3\2\2\2\2\u0097\3\2\2\2\2\u0099\3\2\2\2\2\u009b")
        buf.write("\3\2\2\2\2\u009d\3\2\2\2\2\u009f\3\2\2\2\2\u00a1\3\2\2")
        buf.write("\2\2\u00a3\3\2\2\2\2\u00a5\3\2\2\2\2\u00a7\3\2\2\2\2\u00a9")
        buf.write("\3\2\2\2\2\u00ab\3\2\2\2\2\u00ad\3\2\2\2\2\u00af\3\2\2")
        buf.write("\2\2\u00b1\3\2\2\2\2\u00b3\3\2\2\2\2\u00b5\3\2\2\2\2\u00b7")
        buf.write("\3\2\2\2\2\u00b9\3\2\2\2\2\u00bb\3\2\2\2\2\u00bd\3\2\2")
        buf.write("\2\2\u00bf\3\2\2\2\2\u00c1\3\2\2\2\2\u00c3\3\2\2\2\2\u00c5")
        buf.write("\3\2\2\2\2\u00c7\3\2\2\2\2\u00c9\3\2\2\2\2\u00cb\3\2\2")
        buf.write("\2\2\u00cd\3\2\2\2\2\u00cf\3\2\2\2\2\u00d1\3\2\2\2\2\u00d7")
        buf.write("\3\2\2\2\2\u00d9\3\2\2\2\2\u00db\3\2\2\2\3\u00dd\3\2\2")
        buf.write("\2\5\u00e5\3\2\2\2\7\u00ea\3\2\2\2\t\u00ef\3\2\2\2\13")
        buf.write("\u00f5\3\2\2\2\r\u00fe\3\2\2\2\17\u0103\3\2\2\2\21\u010c")
        buf.write("\3\2\2\2\23\u0110\3\2\2\2\25\u0117\3\2\2\2\27\u0120\3")
        buf.write("\2\2\2\31\u0128\3\2\2\2\33\u012c\3\2\2\2\35\u0133\3\2")
        buf.write("\2\2\37\u013b\3\2\2\2!\u0140\3\2\2\2#\u014c\3\2\2\2%\u0155")
        buf.write("\3\2\2\2\'\u015c\3\2\2\2)\u0162\3\2\2\2+\u016a\3\2\2\2")
        buf.write("-\u0171\3\2\2\2/\u0176\3\2\2\2\61\u0180\3\2\2\2\63\u0187")
        buf.write("\3\2\2\2\65\u018b\3\2\2\2\67\u0192\3\2\2\29\u019b\3\2")
        buf.write("\2\2;\u019e\3\2\2\2=\u01a1\3\2\2\2?\u01a9\3\2\2\2A\u01b1")
        buf.write("\3\2\2\2C\u01b5\3\2\2\2E\u01bf\3\2\2\2G\u01c4\3\2\2\2")
        buf.write("I\u01ca\3\2\2\2K\u01d1\3\2\2\2M\u01d5\3\2\2\2O\u01de\3")
        buf.write("\2\2\2Q\u01f1\3\2\2\2S\u01f5\3\2\2\2U\u01f9\3\2\2\2W\u0201")
        buf.write("\3\2\2\2Y\u0205\3\2\2\2[\u020a\3\2\2\2]\u020e\3\2\2\2")
        buf.write("_\u0214\3\2\2\2a\u021c\3\2\2\2c\u0223\3\2\2\2e\u0228\3")
        buf.write("\2\2\2g\u022f\3\2\2\2i\u0237\3\2\2\2k\u023e\3\2\2\2m\u0247")
        buf.write("\3\2\2\2o\u0250\3\2\2\2q\u0259\3\2\2\2s\u0262\3\2\2\2")
        buf.write("u\u026b\3\2\2\2w\u0270\3\2\2\2y\u0277\3\2\2\2{\u027c\3")
        buf.write("\2\2\2}\u0283\3\2\2\2\177\u0288\3\2\2\2\u0081\u0293\3")
        buf.write("\2\2\2\u0083\u029c\3\2\2\2\u0085\u02ab\3\2\2\2\u0087\u02ae")
        buf.write("\3\2\2\2\u0089\u02b2\3\2\2\2\u008b\u02b7\3\2\2\2\u008d")
        buf.write("\u02be\3\2\2\2\u008f\u02c4\3\2\2\2\u0091\u02c6\3\2\2\2")
        buf.write("\u0093\u02c9\3\2\2\2\u0095\u02cb\3\2\2\2\u0097\u02ce\3")
        buf.write("\2\2\2\u0099\u02d1\3\2\2\2\u009b\u02d4\3\2\2\2\u009d\u02d8")
        buf.write("\3\2\2\2\u009f\u02dd\3\2\2\2\u00a1\u02e0\3\2\2\2\u00a3")
        buf.write("\u02e3\3\2\2\2\u00a5\u02e6\3\2\2\2\u00a7\u02e9\3\2\2\2")
        buf.write("\u00a9\u02eb\3\2\2\2\u00ab\u02ed\3\2\2\2\u00ad\u02f0\3")
        buf.write("\2\2\2\u00af\u02f2\3\2\2\2\u00b1\u02f5\3\2\2\2\u00b3\u02f8")
        buf.write("\3\2\2\2\u00b5\u02fa\3\2\2\2\u00b7\u02fc\3\2\2\2\u00b9")
        buf.write("\u02fe\3\2\2\2\u00bb\u0300\3\2\2\2\u00bd\u0302\3\2\2\2")
        buf.write("\u00bf\u0304\3\2\2\2\u00c1\u0306\3\2\2\2\u00c3\u0308\3")
        buf.write("\2\2\2\u00c5\u030a\3\2\2\2\u00c7\u030c\3\2\2\2\u00c9\u030e")
        buf.write("\3\2\2\2\u00cb\u0310\3\2\2\2\u00cd\u0312\3\2\2\2\u00cf")
        buf.write("\u0314\3\2\2\2\u00d1\u0316\3\2\2\2\u00d3\u031d\3\2\2\2")
        buf.write("\u00d5\u031f\3\2\2\2\u00d7\u0322\3\2\2\2\u00d9\u0328\3")
        buf.write("\2\2\2\u00db\u0336\3\2\2\2\u00dd\u00de\7c\2\2\u00de\u00df")
        buf.write("\7f\2\2\u00df\u00e0\7f\2\2\u00e0\u00e1\7t\2\2\u00e1\u00e2")
        buf.write("\7g\2\2\u00e2\u00e3\7u\2\2\u00e3\u00e4\7u\2\2\u00e4\4")
        buf.write("\3\2\2\2\u00e5\u00e6\7d\2\2\u00e6\u00e7\7q\2\2\u00e7\u00e8")
        buf.write("\7q\2\2\u00e8\u00e9\7n\2\2\u00e9\6\3\2\2\2\u00ea\u00eb")
        buf.write("\7g\2\2\u00eb\u00ec\7p\2\2\u00ec\u00ed\7w\2\2\u00ed\u00ee")
        buf.write("\7o\2\2\u00ee\b\3\2\2\2\u00ef\u00f0\7g\2\2\u00f0\u00f1")
        buf.write("\7x\2\2\u00f1\u00f2\7g\2\2\u00f2\u00f3\7p\2\2\u00f3\u00f4")
        buf.write("\7v\2\2\u00f4\n\3\2\2\2\u00f5\u00f6\7g\2\2\u00f6\u00f7")
        buf.write("\7x\2\2\u00f7\u00f8\7g\2\2\u00f8\u00f9\7p\2\2\u00f9\u00fa")
        buf.write("\7v\2\2\u00fa\u00fb\7n\2\2\u00fb\u00fc\7q\2\2\u00fc\u00fd")
        buf.write("\7i\2\2\u00fd\f\3\2\2\2\u00fe\u00ff\7w\2\2\u00ff\u0100")
        buf.write("\7k\2\2\u0100\u0101\7p\2\2\u0101\u0102\7v\2\2\u0102\16")
        buf.write("\3\2\2\2\u0103\u0104\7k\2\2\u0104\u0105\7p\2\2\u0105\u0106")
        buf.write("\7u\2\2\u0106\u0107\7v\2\2\u0107\u0108\7a\2\2\u0108\u0109")
        buf.write("\7o\2\2\u0109\u010a\7c\2\2\u010a\u010b\7r\2\2\u010b\20")
        buf.write("\3\2\2\2\u010c\u010d\7k\2\2\u010d\u010e\7p\2\2\u010e\u010f")
        buf.write("\7v\2\2\u010f\22\3\2\2\2\u0110\u0111\7u\2\2\u0111\u0112")
        buf.write("\7v\2\2\u0112\u0113\7t\2\2\u0113\u0114\7k\2\2\u0114\u0115")
        buf.write("\7p\2\2\u0115\u0116\7i\2\2\u0116\24\3\2\2\2\u0117\u0118")
        buf.write("\7e\2\2\u0118\u0119\7q\2\2\u0119\u011a\7p\2\2\u011a\u011b")
        buf.write("\7v\2\2\u011b\u011c\7t\2\2\u011c\u011d\7c\2\2\u011d\u011e")
        buf.write("\7e\2\2\u011e\u011f\7v\2\2\u011f\26\3\2\2\2\u0120\u0121")
        buf.write("\7o\2\2\u0121\u0122\7c\2\2\u0122\u0123\7r\2\2\u0123\u0124")
        buf.write("\7r\2\2\u0124\u0125\7k\2\2\u0125\u0126\7p\2\2\u0126\u0127")
        buf.write("\7i\2\2\u0127\30\3\2\2\2\u0128\u0129\7c\2\2\u0129\u012a")
        buf.write("\7f\2\2\u012a\u012b\7f\2\2\u012b\32\3\2\2\2\u012c\u012d")
        buf.write("\7c\2\2\u012d\u012e\7u\2\2\u012e\u012f\7u\2\2\u012f\u0130")
        buf.write("\7g\2\2\u0130\u0131\7t\2\2\u0131\u0132\7v\2\2\u0132\34")
        buf.write("\3\2\2\2\u0133\u0134\7d\2\2\u0134\u0135\7c\2\2\u0135\u0136")
        buf.write("\7n\2\2\u0136\u0137\7c\2\2\u0137\u0138\7p\2\2\u0138\u0139")
        buf.write("\7e\2\2\u0139\u013a\7g\2\2\u013a\36\3\2\2\2\u013b\u013c")
        buf.write("\7e\2\2\u013c\u013d\7c\2\2\u013d\u013e\7n\2\2\u013e\u013f")
        buf.write("\7n\2\2\u013f \3\2\2\2\u0140\u0141\7e\2\2\u0141\u0142")
        buf.write("\7q\2\2\u0142\u0143\7p\2\2\u0143\u0144\7u\2\2\u0144\u0145")
        buf.write("\7v\2\2\u0145\u0146\7t\2\2\u0146\u0147\7w\2\2\u0147\u0148")
        buf.write("\7e\2\2\u0148\u0149\7v\2\2\u0149\u014a\7q\2\2\u014a\u014b")
        buf.write("\7t\2\2\u014b\"\3\2\2\2\u014c\u014d\7e\2\2\u014d\u014e")
        buf.write("\7q\2\2\u014e\u014f\7p\2\2\u014f\u0150\7v\2\2\u0150\u0151")
        buf.write("\7c\2\2\u0151\u0152\7k\2\2\u0152\u0153\7p\2\2\u0153\u0154")
        buf.write("\7u\2\2\u0154$\3\2\2\2\u0155\u0156\7e\2\2\u0156\u0157")
        buf.write("\7t\2\2\u0157\u0158\7g\2\2\u0158\u0159\7f\2\2\u0159\u015a")
        buf.write("\7k\2\2\u015a\u015b\7v\2\2\u015b&\3\2\2\2\u015c\u015d")
        buf.write("\7f\2\2\u015d\u015e\7g\2\2\u015e\u015f\7d\2\2\u015f\u0160")
        buf.write("\7k\2\2\u0160\u0161\7v\2\2\u0161(\3\2\2\2\u0162\u0163")
        buf.write("\7f\2\2\u0163\u0164\7g\2\2\u0164\u0165\7h\2\2\u0165\u0166")
        buf.write("\7c\2\2\u0166\u0167\7w\2\2\u0167\u0168\7n\2\2\u0168\u0169")
        buf.write("\7v\2\2\u0169*\3\2\2\2\u016a\u016b\7f\2\2\u016b\u016c")
        buf.write("\7g\2\2\u016c\u016d\7n\2\2\u016d\u016e\7g\2\2\u016e\u016f")
        buf.write("\7v\2\2\u016f\u0170\7g\2\2\u0170,\3\2\2\2\u0171\u0172")
        buf.write("\7g\2\2\u0172\u0173\7n\2\2\u0173\u0174\7u\2\2\u0174\u0175")
        buf.write("\7g\2\2\u0175.\3\2\2\2\u0176\u0177\7g\2\2\u0177\u0178")
        buf.write("\7V\2\2\u0178\u0179\7t\2\2\u0179\u017a\7c\2\2\u017a\u017b")
        buf.write("\7p\2\2\u017b\u017c\7u\2\2\u017c\u017d\7h\2\2\u017d\u017e")
        buf.write("\7g\2\2\u017e\u017f\7t\2\2\u017f\60\3\2\2\2\u0180\u0181")
        buf.write("\7g\2\2\u0181\u0182\7z\2\2\u0182\u0183\7k\2\2\u0183\u0184")
        buf.write("\7u\2\2\u0184\u0185\7v\2\2\u0185\u0186\7u\2\2\u0186\62")
        buf.write("\3\2\2\2\u0187\u0188\7h\2\2\u0188\u0189\7q\2\2\u0189\u018a")
        buf.write("\7t\2\2\u018a\64\3\2\2\2\u018b\u018c\7h\2\2\u018c\u018d")
        buf.write("\7q\2\2\u018d\u018e\7t\2\2\u018e\u018f\7c\2\2\u018f\u0190")
        buf.write("\7n\2\2\u0190\u0191\7n\2\2\u0191\66\3\2\2\2\u0192\u0193")
        buf.write("\7h\2\2\u0193\u0194\7w\2\2\u0194\u0195\7p\2\2\u0195\u0196")
        buf.write("\7e\2\2\u0196\u0197\7v\2\2\u0197\u0198\7k\2\2\u0198\u0199")
        buf.write("\7q\2\2\u0199\u019a\7p\2\2\u019a8\3\2\2\2\u019b\u019c")
        buf.write("\7k\2\2\u019c\u019d\7h\2\2\u019d:\3\2\2\2\u019e\u019f")
        buf.write("\7k\2\2\u019f\u01a0\7p\2\2\u01a0<\3\2\2\2\u01a1\u01a2")
        buf.write("\7k\2\2\u01a2\u01a3\7p\2\2\u01a3\u01a4\7v\2\2\u01a4\u01a5")
        buf.write("\7a\2\2\u01a5\u01a6\7o\2\2\u01a6\u01a7\7k\2\2\u01a7\u01a8")
        buf.write("\7p\2\2\u01a8>\3\2\2\2\u01a9\u01aa\7k\2\2\u01aa\u01ab")
        buf.write("\7p\2\2\u01ab\u01ac\7v\2\2\u01ac\u01ad\7a\2\2\u01ad\u01ae")
        buf.write("\7o\2\2\u01ae\u01af\7c\2\2\u01af\u01b0\7z\2\2\u01b0@\3")
        buf.write("\2\2\2\u01b1\u01b2\7k\2\2\u01b2\u01b3\7v\2\2\u01b3\u01b4")
        buf.write("\7g\2\2\u01b4B\3\2\2\2\u01b5\u01b6\7k\2\2\u01b6\u01b7")
        buf.write("\7p\2\2\u01b7\u01b8\7x\2\2\u01b8\u01b9\7c\2\2\u01b9\u01ba")
        buf.write("\7t\2\2\u01ba\u01bb\7k\2\2\u01bb\u01bc\7c\2\2\u01bc\u01bd")
        buf.write("\7p\2\2\u01bd\u01be\7v\2\2\u01beD\3\2\2\2\u01bf\u01c0")
        buf.write("\7m\2\2\u01c0\u01c1\7g\2\2\u01c1\u01c2\7{\2\2\u01c2\u01c3")
        buf.write("\7u\2\2\u01c3F\3\2\2\2\u01c4\u01c5\7n\2\2\u01c5\u01c6")
        buf.write("\7g\2\2\u01c6\u01c7\7o\2\2\u01c7\u01c8\7o\2\2\u01c8\u01c9")
        buf.write("\7c\2\2\u01c9H\3\2\2\2\u01ca\u01cb\7n\2\2\u01cb\u01cc")
        buf.write("\7g\2\2\u01cc\u01cd\7p\2\2\u01cd\u01ce\7i\2\2\u01ce\u01cf")
        buf.write("\7v\2\2\u01cf\u01d0\7j\2\2\u01d0J\3\2\2\2\u01d1\u01d2")
        buf.write("\7n\2\2\u01d2\u01d3\7q\2\2\u01d3\u01d4\7i\2\2\u01d4L\3")
        buf.write("\2\2\2\u01d5\u01d6\7o\2\2\u01d6\u01d7\7q\2\2\u01d7\u01d8")
        buf.write("\7f\2\2\u01d8\u01d9\7k\2\2\u01d9\u01da\7h\2\2\u01da\u01db")
        buf.write("\7k\2\2\u01db\u01dc\7g\2\2\u01dc\u01dd\7u\2\2\u01ddN\3")
        buf.write("\2\2\2\u01de\u01df\7o\2\2\u01df\u01e0\7q\2\2\u01e0\u01e1")
        buf.write("\7f\2\2\u01e1\u01e2\7k\2\2\u01e2\u01e3\7h\2\2\u01e3\u01e4")
        buf.write("\7k\2\2\u01e4\u01e5\7g\2\2\u01e5\u01e6\7u\2\2\u01e6\u01e7")
        buf.write("\7a\2\2\u01e7\u01e8\7c\2\2\u01e8\u01e9\7f\2\2\u01e9\u01ea")
        buf.write("\7f\2\2\u01ea\u01eb\7t\2\2\u01eb\u01ec\7g\2\2\u01ec\u01ed")
        buf.write("\7u\2\2\u01ed\u01ee\7u\2\2\u01ee\u01ef\7g\2\2\u01ef\u01f0")
        buf.write("\7u\2\2\u01f0P\3\2\2\2\u01f1\u01f2\7p\2\2\u01f2\u01f3")
        buf.write("\7g\2\2\u01f3\u01f4\7y\2\2\u01f4R\3\2\2\2\u01f5\u01f6")
        buf.write("\7p\2\2\u01f6\u01f7\7q\2\2\u01f7\u01f8\7y\2\2\u01f8T\3")
        buf.write("\2\2\2\u01f9\u01fa\7r\2\2\u01fa\u01fb\7c\2\2\u01fb\u01fc")
        buf.write("\7{\2\2\u01fc\u01fd\7c\2\2\u01fd\u01fe\7d\2\2\u01fe\u01ff")
        buf.write("\7n\2\2\u01ff\u0200\7g\2\2\u0200V\3\2\2\2\u0201\u0202")
        buf.write("\7r\2\2\u0202\u0203\7q\2\2\u0203\u0204\7r\2\2\u0204X\3")
        buf.write("\2\2\2\u0205\u0206\7r\2\2\u0206\u0207\7q\2\2\u0207\u0208")
        buf.write("\7u\2\2\u0208\u0209\7v\2\2\u0209Z\3\2\2\2\u020a\u020b")
        buf.write("\7r\2\2\u020b\u020c\7t\2\2\u020c\u020d\7g\2\2\u020d\\")
        buf.write("\3\2\2\2\u020e\u020f\7r\2\2\u020f\u0210\7t\2\2\u0210\u0211")
        buf.write("\7k\2\2\u0211\u0212\7p\2\2\u0212\u0213\7v\2\2\u0213^\3")
        buf.write("\2\2\2\u0214\u0215\7r\2\2\u0215\u0216\7t\2\2\u0216\u0217")
        buf.write("\7k\2\2\u0217\u0218\7x\2\2\u0218\u0219\7c\2\2\u0219\u021a")
        buf.write("\7v\2\2\u021a\u021b\7g\2\2\u021b`\3\2\2\2\u021c\u021d")
        buf.write("\7r\2\2\u021d\u021e\7w\2\2\u021e\u021f\7d\2\2\u021f\u0220")
        buf.write("\7n\2\2\u0220\u0221\7k\2\2\u0221\u0222\7e\2\2\u0222b\3")
        buf.write("\2\2\2\u0223\u0224\7r\2\2\u0224\u0225\7w\2\2\u0225\u0226")
        buf.write("\7u\2\2\u0226\u0227\7j\2\2\u0227d\3\2\2\2\u0228\u0229")
        buf.write("\7t\2\2\u0229\u022a\7g\2\2\u022a\u022b\7v\2\2\u022b\u022c")
        buf.write("\7w\2\2\u022c\u022d\7t\2\2\u022d\u022e\7p\2\2\u022ef\3")
        buf.write("\2\2\2\u022f\u0230\7t\2\2\u0230\u0231\7g\2\2\u0231\u0232")
        buf.write("\7v\2\2\u0232\u0233\7w\2\2\u0233\u0234\7t\2\2\u0234\u0235")
        buf.write("\7p\2\2\u0235\u0236\7u\2\2\u0236h\3\2\2\2\u0237\u0238")
        buf.write("\7t\2\2\u0238\u0239\7g\2\2\u0239\u023a\7x\2\2\u023a\u023b")
        buf.write("\7g\2\2\u023b\u023c\7t\2\2\u023c\u023d\7v\2\2\u023dj\3")
        buf.write("\2\2\2\u023e\u023f\7u\2\2\u023f\u0240\7c\2\2\u0240\u0241")
        buf.write("\7h\2\2\u0241\u0242\7g\2\2\u0242\u0243\7a\2\2\u0243\u0244")
        buf.write("\7c\2\2\u0244\u0245\7f\2\2\u0245\u0246\7f\2\2\u0246l\3")
        buf.write("\2\2\2\u0247\u0248\7u\2\2\u0248\u0249\7c\2\2\u0249\u024a")
        buf.write("\7h\2\2\u024a\u024b\7g\2\2\u024b\u024c\7a\2\2\u024c\u024d")
        buf.write("\7f\2\2\u024d\u024e\7k\2\2\u024e\u024f\7x\2\2\u024fn\3")
        buf.write("\2\2\2\u0250\u0251\7u\2\2\u0251\u0252\7c\2\2\u0252\u0253")
        buf.write("\7h\2\2\u0253\u0254\7g\2\2\u0254\u0255\7a\2\2\u0255\u0256")
        buf.write("\7o\2\2\u0256\u0257\7q\2\2\u0257\u0258\7f\2\2\u0258p\3")
        buf.write("\2\2\2\u0259\u025a\7u\2\2\u025a\u025b\7c\2\2\u025b\u025c")
        buf.write("\7h\2\2\u025c\u025d\7g\2\2\u025d\u025e\7a\2\2\u025e\u025f")
        buf.write("\7o\2\2\u025f\u0260\7w\2\2\u0260\u0261\7n\2\2\u0261r\3")
        buf.write("\2\2\2\u0262\u0263\7u\2\2\u0263\u0264\7c\2\2\u0264\u0265")
        buf.write("\7h\2\2\u0265\u0266\7g\2\2\u0266\u0267\7a\2\2\u0267\u0268")
        buf.write("\7u\2\2\u0268\u0269\7w\2\2\u0269\u026a\7d\2\2\u026at\3")
        buf.write("\2\2\2\u026b\u026c\7u\2\2\u026c\u026d\7g\2\2\u026d\u026e")
        buf.write("\7p\2\2\u026e\u026f\7f\2\2\u026fv\3\2\2\2\u0270\u0271")
        buf.write("\7u\2\2\u0271\u0272\7g\2\2\u0272\u0273\7p\2\2\u0273\u0274")
        buf.write("\7f\2\2\u0274\u0275\7g\2\2\u0275\u0276\7t\2\2\u0276x\3")
        buf.write("\2\2\2\u0277\u0278\7u\2\2\u0278\u0279\7r\2\2\u0279\u027a")
        buf.write("\7g\2\2\u027a\u027b\7e\2\2\u027bz\3\2\2\2\u027c\u027d")
        buf.write("\7u\2\2\u027d\u027e\7v\2\2\u027e\u027f\7t\2\2\u027f\u0280")
        buf.write("\7w\2\2\u0280\u0281\7e\2\2\u0281\u0282\7v\2\2\u0282|\3")
        buf.write("\2\2\2\u0283\u0284\7v\2\2\u0284\u0285\7j\2\2\u0285\u0286")
        buf.write("\7k\2\2\u0286\u0287\7u\2\2\u0287~\3\2\2\2\u0288\u0289")
        buf.write("\7v\2\2\u0289\u028a\7z\2\2\u028a\u028b\7a\2\2\u028b\u028c")
        buf.write("\7t\2\2\u028c\u028d\7g\2\2\u028d\u028e\7x\2\2\u028e\u028f")
        buf.write("\7g\2\2\u028f\u0290\7t\2\2\u0290\u0291\7v\2\2\u0291\u0292")
        buf.write("\7u\2\2\u0292\u0080\3\2\2\2\u0293\u0294\7w\2\2\u0294\u0295")
        buf.write("\7k\2\2\u0295\u0296\7p\2\2\u0296\u0297\7v\2\2\u0297\u0298")
        buf.write("\7a\2\2\u0298\u0299\7o\2\2\u0299\u029a\7c\2\2\u029a\u029b")
        buf.write("\7z\2\2\u029b\u0082\3\2\2\2\u029c\u029d\7x\2\2\u029d\u029e")
        buf.write("\7c\2\2\u029e\u029f\7n\2\2\u029f\u02a0\7w\2\2\u02a0\u02a1")
        buf.write("\7g\2\2\u02a1\u0084\3\2\2\2\u02a2\u02a3\7v\2\2\u02a3\u02a4")
        buf.write("\7t\2\2\u02a4\u02a5\7w\2\2\u02a5\u02ac\7g\2\2\u02a6\u02a7")
        buf.write("\7h\2\2\u02a7\u02a8\7c\2\2\u02a8\u02a9\7n\2\2\u02a9\u02aa")
        buf.write("\7u\2\2\u02aa\u02ac\7g\2\2\u02ab\u02a2\3\2\2\2\u02ab\u02a6")
        buf.write("\3\2\2\2\u02ac\u0086\3\2\2\2\u02ad\u02af\t\2\2\2\u02ae")
        buf.write("\u02ad\3\2\2\2\u02af\u02b0\3\2\2\2\u02b0\u02ae\3\2\2\2")
        buf.write("\u02b0\u02b1\3\2\2\2\u02b1\u0088\3\2\2\2\u02b2\u02b3\7")
        buf.write("p\2\2\u02b3\u02b4\7w\2\2\u02b4\u02b5\7n\2\2\u02b5\u02b6")
        buf.write("\7n\2\2\u02b6\u008a\3\2\2\2\u02b7\u02b9\7$\2\2\u02b8\u02ba")
        buf.write("\5\u008dG\2\u02b9\u02b8\3\2\2\2\u02b9\u02ba\3\2\2\2\u02ba")
        buf.write("\u02bb\3\2\2\2\u02bb\u02bc\7$\2\2\u02bc\u008c\3\2\2\2")
        buf.write("\u02bd\u02bf\5\u008fH\2\u02be\u02bd\3\2\2\2\u02bf\u02c0")
        buf.write("\3\2\2\2\u02c0\u02be\3\2\2\2\u02c0\u02c1\3\2\2\2\u02c1")
        buf.write("\u008e\3\2\2\2\u02c2\u02c5\n\3\2\2\u02c3\u02c5\5\u0091")
        buf.write("I\2\u02c4\u02c2\3\2\2\2\u02c4\u02c3\3\2\2\2\u02c5\u0090")
        buf.write("\3\2\2\2\u02c6\u02c7\7^\2\2\u02c7\u02c8\13\2\2\2\u02c8")
        buf.write("\u0092\3\2\2\2\u02c9\u02ca\7#\2\2\u02ca\u0094\3\2\2\2")
        buf.write("\u02cb\u02cc\7(\2\2\u02cc\u02cd\7(\2\2\u02cd\u0096\3\2")
        buf.write("\2\2\u02ce\u02cf\7~\2\2\u02cf\u02d0\7~\2\2\u02d0\u0098")
        buf.write("\3\2\2\2\u02d1\u02d2\7?\2\2\u02d2\u02d3\7@\2\2\u02d3\u009a")
        buf.write("\3\2\2\2\u02d4\u02d5\7?\2\2\u02d5\u02d6\7?\2\2\u02d6\u02d7")
        buf.write("\7@\2\2\u02d7\u009c\3\2\2\2\u02d8\u02d9\7>\2\2\u02d9\u02da")
        buf.write("\7?\2\2\u02da\u02db\7?\2\2\u02db\u02dc\7@\2\2\u02dc\u009e")
        buf.write("\3\2\2\2\u02dd\u02de\7?\2\2\u02de\u02df\7?\2\2\u02df\u00a0")
        buf.write("\3\2\2\2\u02e0\u02e1\7#\2\2\u02e1\u02e2\7?\2\2\u02e2\u00a2")
        buf.write("\3\2\2\2\u02e3\u02e4\7>\2\2\u02e4\u02e5\7?\2\2\u02e5\u00a4")
        buf.write("\3\2\2\2\u02e6\u02e7\7@\2\2\u02e7\u02e8\7?\2\2\u02e8\u00a6")
        buf.write("\3\2\2\2\u02e9\u02ea\7>\2\2\u02ea\u00a8\3\2\2\2\u02eb")
        buf.write("\u02ec\7@\2\2\u02ec\u00aa\3\2\2\2\u02ed\u02ee\7/\2\2\u02ee")
        buf.write("\u02ef\7@\2\2\u02ef\u00ac\3\2\2\2\u02f0\u02f1\7?\2\2\u02f1")
        buf.write("\u00ae\3\2\2\2\u02f2\u02f3\7-\2\2\u02f3\u02f4\7?\2\2\u02f4")
        buf.write("\u00b0\3\2\2\2\u02f5\u02f6\7/\2\2\u02f6\u02f7\7?\2\2\u02f7")
        buf.write("\u00b2\3\2\2\2\u02f8\u02f9\7-\2\2\u02f9\u00b4\3\2\2\2")
        buf.write("\u02fa\u02fb\7/\2\2\u02fb\u00b6\3\2\2\2\u02fc\u02fd\7")
        buf.write(",\2\2\u02fd\u00b8\3\2\2\2\u02fe\u02ff\7\61\2\2\u02ff\u00ba")
        buf.write("\3\2\2\2\u0300\u0301\7\'\2\2\u0301\u00bc\3\2\2\2\u0302")
        buf.write("\u0303\7}\2\2\u0303\u00be\3\2\2\2\u0304\u0305\7\177\2")
        buf.write("\2\u0305\u00c0\3\2\2\2\u0306\u0307\7]\2\2\u0307\u00c2")
        buf.write("\3\2\2\2\u0308\u0309\7_\2\2\u0309\u00c4\3\2\2\2\u030a")
        buf.write("\u030b\7*\2\2\u030b\u00c6\3\2\2\2\u030c\u030d\7+\2\2\u030d")
        buf.write("\u00c8\3\2\2\2\u030e\u030f\7=\2\2\u030f\u00ca\3\2\2\2")
        buf.write("\u0310\u0311\7.\2\2\u0311\u00cc\3\2\2\2\u0312\u0313\7")
        buf.write("\60\2\2\u0313\u00ce\3\2\2\2\u0314\u0315\7<\2\2\u0315\u00d0")
        buf.write("\3\2\2\2\u0316\u031a\5\u00d3j\2\u0317\u0319\5\u00d5k\2")
        buf.write("\u0318\u0317\3\2\2\2\u0319\u031c\3\2\2\2\u031a\u0318\3")
        buf.write("\2\2\2\u031a\u031b\3\2\2\2\u031b\u00d2\3\2\2\2\u031c\u031a")
        buf.write("\3\2\2\2\u031d\u031e\t\4\2\2\u031e\u00d4\3\2\2\2\u031f")
        buf.write("\u0320\t\5\2\2\u0320\u00d6\3\2\2\2\u0321\u0323\t\6\2\2")
        buf.write("\u0322\u0321\3\2\2\2\u0323\u0324\3\2\2\2\u0324\u0322\3")
        buf.write("\2\2\2\u0324\u0325\3\2\2\2\u0325\u0326\3\2\2\2\u0326\u0327")
        buf.write("\bl\2\2\u0327\u00d8\3\2\2\2\u0328\u0329\7\61\2\2\u0329")
        buf.write("\u032a\7,\2\2\u032a\u032e\3\2\2\2\u032b\u032d\13\2\2\2")
        buf.write("\u032c\u032b\3\2\2\2\u032d\u0330\3\2\2\2\u032e\u032f\3")
        buf.write("\2\2\2\u032e\u032c\3\2\2\2\u032f\u0331\3\2\2\2\u0330\u032e")
        buf.write("\3\2\2\2\u0331\u0332\7,\2\2\u0332\u0333\7\61\2\2\u0333")
        buf.write("\u0334\3\2\2\2\u0334\u0335\bm\3\2\u0335\u00da\3\2\2\2")
        buf.write("\u0336\u0337\7\61\2\2\u0337\u0338\7\61\2\2\u0338\u033c")
        buf.write("\3\2\2\2\u0339\u033b\n\7\2\2\u033a\u0339\3\2\2\2\u033b")
        buf.write("\u033e\3\2\2\2\u033c\u033a\3\2\2\2\u033c\u033d\3\2\2\2")
        buf.write("\u033d\u033f\3\2\2\2\u033e\u033c\3\2\2\2\u033f\u0340\b")
        buf.write("n\3\2\u0340\u00dc\3\2\2\2\f\2\u02ab\u02b0\u02b9\u02c0")
        buf.write("\u02c4\u031a\u0324\u032e\u033c\4\b\2\2\2\3\2")
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
    INSTMAP = 7
    INT = 8
    STRING = 9
    CONTRACT = 10
    MAP = 11
    ADD = 12
    ASSERT = 13
    BALANCE = 14
    CALL = 15
    CONSTR = 16
    CONTAINS = 17
    CREDIT = 18
    DEBIT = 19
    DEFAULT = 20
    DELETE = 21
    ELSE = 22
    ETRANSFER = 23
    EXISTS = 24
    FOR = 25
    FORALL = 26
    FUNCTION = 27
    IF = 28
    IN = 29
    INT_MIN = 30
    INT_MAX = 31
    ITE = 32
    INVARIANT = 33
    KEYS = 34
    LEMMA = 35
    LENGTH = 36
    LOG = 37
    MODIFIES = 38
    MODIFIESA = 39
    NEW = 40
    NOW = 41
    PAYABLE = 42
    POP = 43
    POST = 44
    PRE = 45
    PRINT = 46
    PRIVATE = 47
    PUBLIC = 48
    PUSH = 49
    RETURN = 50
    RETURNS = 51
    REVERT = 52
    SAFEADD = 53
    SAFEDIV = 54
    SAFEMOD = 55
    SAFEMUL = 56
    SAFESUB = 57
    SEND = 58
    SENDER = 59
    SPEC = 60
    STRUCT = 61
    THIS = 62
    TXREVERTS = 63
    UINT_MAX = 64
    VALUE = 65
    BoolLiteral = 66
    IntLiteral = 67
    NullLiteral = 68
    StringLiteral = 69
    LNOT = 70
    LAND = 71
    LOR = 72
    MAPUPD = 73
    IMPL = 74
    BIMPL = 75
    EQ = 76
    NE = 77
    LE = 78
    GE = 79
    LT = 80
    GT = 81
    RARROW = 82
    ASSIGN = 83
    INSERT = 84
    REMOVE = 85
    PLUS = 86
    SUB = 87
    MUL = 88
    DIV = 89
    MOD = 90
    LBRACE = 91
    RBRACE = 92
    LBRACK = 93
    RBRACK = 94
    LPAREN = 95
    RPAREN = 96
    SEMI = 97
    COMMA = 98
    DOT = 99
    COLON = 100
    Iden = 101
    Whitespace = 102
    BlockComment = 103
    LineComment = 104

    channelNames = [ u"DEFAULT_TOKEN_CHANNEL", u"HIDDEN" ]

    modeNames = [ "DEFAULT_MODE" ]

    literalNames = [ "<INVALID>",
            "'address'", "'bool'", "'enum'", "'event'", "'eventlog'", "'uint'", 
            "'inst_map'", "'int'", "'string'", "'contract'", "'mapping'", 
            "'add'", "'assert'", "'balance'", "'call'", "'constructor'", 
            "'contains'", "'credit'", "'debit'", "'default'", "'delete'", 
            "'else'", "'eTransfer'", "'exists'", "'for'", "'forall'", "'function'", 
            "'if'", "'in'", "'int_min'", "'int_max'", "'ite'", "'invariant'", 
            "'keys'", "'lemma'", "'length'", "'log'", "'modifies'", "'modifies_addresses'", 
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
            "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", "UINT", "INSTMAP", 
            "INT", "STRING", "CONTRACT", "MAP", "ADD", "ASSERT", "BALANCE", 
            "CALL", "CONSTR", "CONTAINS", "CREDIT", "DEBIT", "DEFAULT", 
            "DELETE", "ELSE", "ETRANSFER", "EXISTS", "FOR", "FORALL", "FUNCTION", 
            "IF", "IN", "INT_MIN", "INT_MAX", "ITE", "INVARIANT", "KEYS", 
            "LEMMA", "LENGTH", "LOG", "MODIFIES", "MODIFIESA", "NEW", "NOW", 
            "PAYABLE", "POP", "POST", "PRE", "PRINT", "PRIVATE", "PUBLIC", 
            "PUSH", "RETURN", "RETURNS", "REVERT", "SAFEADD", "SAFEDIV", 
            "SAFEMOD", "SAFEMUL", "SAFESUB", "SEND", "SENDER", "SPEC", "STRUCT", 
            "THIS", "TXREVERTS", "UINT_MAX", "VALUE", "BoolLiteral", "IntLiteral", 
            "NullLiteral", "StringLiteral", "LNOT", "LAND", "LOR", "MAPUPD", 
            "IMPL", "BIMPL", "EQ", "NE", "LE", "GE", "LT", "GT", "RARROW", 
            "ASSIGN", "INSERT", "REMOVE", "PLUS", "SUB", "MUL", "DIV", "MOD", 
            "LBRACE", "RBRACE", "LBRACK", "RBRACK", "LPAREN", "RPAREN", 
            "SEMI", "COMMA", "DOT", "COLON", "Iden", "Whitespace", "BlockComment", 
            "LineComment" ]

    ruleNames = [ "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", "UINT", "INSTMAP", 
                  "INT", "STRING", "CONTRACT", "MAP", "ADD", "ASSERT", "BALANCE", 
                  "CALL", "CONSTR", "CONTAINS", "CREDIT", "DEBIT", "DEFAULT", 
                  "DELETE", "ELSE", "ETRANSFER", "EXISTS", "FOR", "FORALL", 
                  "FUNCTION", "IF", "IN", "INT_MIN", "INT_MAX", "ITE", "INVARIANT", 
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


