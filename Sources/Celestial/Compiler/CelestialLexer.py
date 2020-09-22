# Generated from .\Compiler\CelestialLexer.g4 by ANTLR 4.8
from antlr4 import *
from io import StringIO
from typing.io import TextIO
import sys



def serializedATN():
    with StringIO() as buf:
        buf.write("\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2o")
        buf.write("\u036e\b\1\4\2\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7")
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
        buf.write("p\tp\4q\tq\4r\tr\4s\ts\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2")
        buf.write("\3\3\3\3\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3")
        buf.write("\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7")
        buf.write("\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3")
        buf.write("\t\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13")
        buf.write("\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3")
        buf.write("\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\16\3\16")
        buf.write("\3\16\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\20\3\20")
        buf.write("\3\20\3\20\3\20\3\20\3\20\3\20\3\21\3\21\3\21\3\21\3\22")
        buf.write("\3\22\3\22\3\22\3\22\3\22\3\22\3\23\3\23\3\23\3\23\3\23")
        buf.write("\3\23\3\23\3\23\3\24\3\24\3\24\3\24\3\24\3\25\3\25\3\25")
        buf.write("\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\26\3\26")
        buf.write("\3\26\3\26\3\26\3\26\3\26\3\26\3\26\3\27\3\27\3\27\3\27")
        buf.write("\3\27\3\27\3\27\3\30\3\30\3\30\3\30\3\30\3\30\3\31\3\31")
        buf.write("\3\31\3\31\3\31\3\31\3\31\3\31\3\32\3\32\3\32\3\32\3\32")
        buf.write("\3\32\3\32\3\33\3\33\3\33\3\33\3\33\3\34\3\34\3\34\3\34")
        buf.write("\3\34\3\34\3\34\3\34\3\34\3\34\3\35\3\35\3\35\3\35\3\35")
        buf.write("\3\35\3\35\3\36\3\36\3\36\3\36\3\37\3\37\3\37\3\37\3\37")
        buf.write("\3\37\3\37\3 \3 \3 \3 \3 \3 \3 \3 \3 \3!\3!\3!\3\"\3\"")
        buf.write("\3\"\3#\3#\3#\3#\3#\3#\3#\3#\3$\3$\3$\3$\3$\3$\3$\3$\3")
        buf.write("%\3%\3%\3%\3&\3&\3&\3&\3&\3&\3&\3&\3&\3&\3\'\3\'\3\'\3")
        buf.write("\'\3\'\3(\3(\3(\3(\3(\3(\3)\3)\3)\3)\3)\3)\3)\3*\3*\3")
        buf.write("*\3*\3+\3+\3+\3+\3+\3+\3+\3+\3+\3,\3,\3,\3,\3,\3,\3,\3")
        buf.write(",\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3,\3-\3-\3-\3-\3.\3.\3")
        buf.write(".\3.\3/\3/\3/\3/\3/\3/\3/\3\60\3\60\3\60\3\60\3\60\3\60")
        buf.write("\3\60\3\60\3\61\3\61\3\61\3\61\3\62\3\62\3\62\3\62\3\62")
        buf.write("\3\63\3\63\3\63\3\63\3\64\3\64\3\64\3\64\3\64\3\64\3\65")
        buf.write("\3\65\3\65\3\65\3\65\3\65\3\65\3\65\3\66\3\66\3\66\3\66")
        buf.write("\3\66\3\66\3\66\3\67\3\67\3\67\3\67\3\67\38\38\38\38\3")
        buf.write("8\38\38\39\39\39\39\39\39\39\39\3:\3:\3:\3:\3:\3:\3:\3")
        buf.write(";\3;\3;\3;\3;\3;\3;\3;\3;\3<\3<\3<\3<\3<\3<\3<\3<\3<\3")
        buf.write("=\3=\3=\3=\3=\3=\3=\3=\3=\3>\3>\3>\3>\3>\3>\3>\3>\3>\3")
        buf.write("?\3?\3?\3?\3?\3?\3?\3?\3?\3@\3@\3@\3@\3@\3A\3A\3A\3A\3")
        buf.write("A\3A\3A\3B\3B\3B\3B\3B\3C\3C\3C\3C\3C\3C\3C\3D\3D\3D\3")
        buf.write("D\3D\3E\3E\3E\3E\3E\3E\3E\3E\3E\3E\3E\3F\3F\3F\3F\3F\3")
        buf.write("F\3F\3F\3F\3G\3G\3G\3G\3G\3G\3H\3H\3H\3H\3H\3H\3H\3H\3")
        buf.write("H\5H\u02d9\nH\3I\6I\u02dc\nI\rI\16I\u02dd\3J\3J\3J\3J")
        buf.write("\3J\3K\3K\5K\u02e7\nK\3K\3K\3L\6L\u02ec\nL\rL\16L\u02ed")
        buf.write("\3M\3M\5M\u02f2\nM\3N\3N\3N\3O\3O\3P\3P\3P\3Q\3Q\3Q\3")
        buf.write("R\3R\3R\3S\3S\3S\3S\3T\3T\3T\3T\3T\3U\3U\3U\3V\3V\3V\3")
        buf.write("W\3W\3W\3X\3X\3X\3Y\3Y\3Z\3Z\3[\3[\3[\3\\\3\\\3]\3]\3")
        buf.write("]\3^\3^\3^\3_\3_\3`\3`\3a\3a\3b\3b\3c\3c\3d\3d\3e\3e\3")
        buf.write("f\3f\3g\3g\3h\3h\3i\3i\3j\3j\3k\3k\3l\3l\3m\3m\3n\3n\7")
        buf.write("n\u0346\nn\fn\16n\u0349\13n\3o\3o\3p\3p\3q\6q\u0350\n")
        buf.write("q\rq\16q\u0351\3q\3q\3r\3r\3r\3r\7r\u035a\nr\fr\16r\u035d")
        buf.write("\13r\3r\3r\3r\3r\3r\3s\3s\3s\3s\7s\u0368\ns\fs\16s\u036b")
        buf.write("\13s\3s\3s\3\u035b\2t\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21")
        buf.write("\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24")
        buf.write("\'\25)\26+\27-\30/\31\61\32\63\33\65\34\67\359\36;\37")
        buf.write("= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63e\64")
        buf.write("g\65i\66k\67m8o9q:s;u<w=y>{?}@\177A\u0081B\u0083C\u0085")
        buf.write("D\u0087E\u0089F\u008bG\u008dH\u008fI\u0091J\u0093K\u0095")
        buf.write("L\u0097\2\u0099\2\u009b\2\u009dM\u009fN\u00a1O\u00a3P")
        buf.write("\u00a5Q\u00a7R\u00a9S\u00abT\u00adU\u00afV\u00b1W\u00b3")
        buf.write("X\u00b5Y\u00b7Z\u00b9[\u00bb\\\u00bd]\u00bf^\u00c1_\u00c3")
        buf.write("`\u00c5a\u00c7b\u00c9c\u00cbd\u00cde\u00cff\u00d1g\u00d3")
        buf.write("h\u00d5i\u00d7j\u00d9k\u00dbl\u00dd\2\u00df\2\u00e1m\u00e3")
        buf.write("n\u00e5o\3\2\b\3\2\62;\4\2$$^^\5\2C\\aac|\6\2\62;C\\a")
        buf.write("ac|\5\2\13\f\16\17\"\"\4\2\f\f\17\17\2\u0371\2\3\3\2\2")
        buf.write("\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2")
        buf.write("\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25")
        buf.write("\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3")
        buf.write("\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2")
        buf.write("\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2")
        buf.write("\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\2")
        buf.write("9\3\2\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2")
        buf.write("\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2")
        buf.write("\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2")
        buf.write("\2\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3")
        buf.write("\2\2\2\2a\3\2\2\2\2c\3\2\2\2\2e\3\2\2\2\2g\3\2\2\2\2i")
        buf.write("\3\2\2\2\2k\3\2\2\2\2m\3\2\2\2\2o\3\2\2\2\2q\3\2\2\2\2")
        buf.write("s\3\2\2\2\2u\3\2\2\2\2w\3\2\2\2\2y\3\2\2\2\2{\3\2\2\2")
        buf.write("\2}\3\2\2\2\2\177\3\2\2\2\2\u0081\3\2\2\2\2\u0083\3\2")
        buf.write("\2\2\2\u0085\3\2\2\2\2\u0087\3\2\2\2\2\u0089\3\2\2\2\2")
        buf.write("\u008b\3\2\2\2\2\u008d\3\2\2\2\2\u008f\3\2\2\2\2\u0091")
        buf.write("\3\2\2\2\2\u0093\3\2\2\2\2\u0095\3\2\2\2\2\u009d\3\2\2")
        buf.write("\2\2\u009f\3\2\2\2\2\u00a1\3\2\2\2\2\u00a3\3\2\2\2\2\u00a5")
        buf.write("\3\2\2\2\2\u00a7\3\2\2\2\2\u00a9\3\2\2\2\2\u00ab\3\2\2")
        buf.write("\2\2\u00ad\3\2\2\2\2\u00af\3\2\2\2\2\u00b1\3\2\2\2\2\u00b3")
        buf.write("\3\2\2\2\2\u00b5\3\2\2\2\2\u00b7\3\2\2\2\2\u00b9\3\2\2")
        buf.write("\2\2\u00bb\3\2\2\2\2\u00bd\3\2\2\2\2\u00bf\3\2\2\2\2\u00c1")
        buf.write("\3\2\2\2\2\u00c3\3\2\2\2\2\u00c5\3\2\2\2\2\u00c7\3\2\2")
        buf.write("\2\2\u00c9\3\2\2\2\2\u00cb\3\2\2\2\2\u00cd\3\2\2\2\2\u00cf")
        buf.write("\3\2\2\2\2\u00d1\3\2\2\2\2\u00d3\3\2\2\2\2\u00d5\3\2\2")
        buf.write("\2\2\u00d7\3\2\2\2\2\u00d9\3\2\2\2\2\u00db\3\2\2\2\2\u00e1")
        buf.write("\3\2\2\2\2\u00e3\3\2\2\2\2\u00e5\3\2\2\2\3\u00e7\3\2\2")
        buf.write("\2\5\u00ef\3\2\2\2\7\u00f4\3\2\2\2\t\u00f9\3\2\2\2\13")
        buf.write("\u00ff\3\2\2\2\r\u0108\3\2\2\2\17\u010d\3\2\2\2\21\u0113")
        buf.write("\3\2\2\2\23\u011c\3\2\2\2\25\u0120\3\2\2\2\27\u0127\3")
        buf.write("\2\2\2\31\u0130\3\2\2\2\33\u0138\3\2\2\2\35\u013e\3\2")
        buf.write("\2\2\37\u0146\3\2\2\2!\u014e\3\2\2\2#\u0152\3\2\2\2%\u0159")
        buf.write("\3\2\2\2\'\u0161\3\2\2\2)\u0166\3\2\2\2+\u0172\3\2\2\2")
        buf.write("-\u017b\3\2\2\2/\u0182\3\2\2\2\61\u0188\3\2\2\2\63\u0190")
        buf.write("\3\2\2\2\65\u0197\3\2\2\2\67\u019c\3\2\2\29\u01a6\3\2")
        buf.write("\2\2;\u01ad\3\2\2\2=\u01b1\3\2\2\2?\u01b8\3\2\2\2A\u01c1")
        buf.write("\3\2\2\2C\u01c4\3\2\2\2E\u01c7\3\2\2\2G\u01cf\3\2\2\2")
        buf.write("I\u01d7\3\2\2\2K\u01db\3\2\2\2M\u01e5\3\2\2\2O\u01ea\3")
        buf.write("\2\2\2Q\u01f0\3\2\2\2S\u01f7\3\2\2\2U\u01fb\3\2\2\2W\u0204")
        buf.write("\3\2\2\2Y\u0217\3\2\2\2[\u021b\3\2\2\2]\u021f\3\2\2\2")
        buf.write("_\u0226\3\2\2\2a\u022e\3\2\2\2c\u0232\3\2\2\2e\u0237\3")
        buf.write("\2\2\2g\u023b\3\2\2\2i\u0241\3\2\2\2k\u0249\3\2\2\2m\u0250")
        buf.write("\3\2\2\2o\u0255\3\2\2\2q\u025c\3\2\2\2s\u0264\3\2\2\2")
        buf.write("u\u026b\3\2\2\2w\u0274\3\2\2\2y\u027d\3\2\2\2{\u0286\3")
        buf.write("\2\2\2}\u028f\3\2\2\2\177\u0298\3\2\2\2\u0081\u029d\3")
        buf.write("\2\2\2\u0083\u02a4\3\2\2\2\u0085\u02a9\3\2\2\2\u0087\u02b0")
        buf.write("\3\2\2\2\u0089\u02b5\3\2\2\2\u008b\u02c0\3\2\2\2\u008d")
        buf.write("\u02c9\3\2\2\2\u008f\u02d8\3\2\2\2\u0091\u02db\3\2\2\2")
        buf.write("\u0093\u02df\3\2\2\2\u0095\u02e4\3\2\2\2\u0097\u02eb\3")
        buf.write("\2\2\2\u0099\u02f1\3\2\2\2\u009b\u02f3\3\2\2\2\u009d\u02f6")
        buf.write("\3\2\2\2\u009f\u02f8\3\2\2\2\u00a1\u02fb\3\2\2\2\u00a3")
        buf.write("\u02fe\3\2\2\2\u00a5\u0301\3\2\2\2\u00a7\u0305\3\2\2\2")
        buf.write("\u00a9\u030a\3\2\2\2\u00ab\u030d\3\2\2\2\u00ad\u0310\3")
        buf.write("\2\2\2\u00af\u0313\3\2\2\2\u00b1\u0316\3\2\2\2\u00b3\u0318")
        buf.write("\3\2\2\2\u00b5\u031a\3\2\2\2\u00b7\u031d\3\2\2\2\u00b9")
        buf.write("\u031f\3\2\2\2\u00bb\u0322\3\2\2\2\u00bd\u0325\3\2\2\2")
        buf.write("\u00bf\u0327\3\2\2\2\u00c1\u0329\3\2\2\2\u00c3\u032b\3")
        buf.write("\2\2\2\u00c5\u032d\3\2\2\2\u00c7\u032f\3\2\2\2\u00c9\u0331")
        buf.write("\3\2\2\2\u00cb\u0333\3\2\2\2\u00cd\u0335\3\2\2\2\u00cf")
        buf.write("\u0337\3\2\2\2\u00d1\u0339\3\2\2\2\u00d3\u033b\3\2\2\2")
        buf.write("\u00d5\u033d\3\2\2\2\u00d7\u033f\3\2\2\2\u00d9\u0341\3")
        buf.write("\2\2\2\u00db\u0343\3\2\2\2\u00dd\u034a\3\2\2\2\u00df\u034c")
        buf.write("\3\2\2\2\u00e1\u034f\3\2\2\2\u00e3\u0355\3\2\2\2\u00e5")
        buf.write("\u0363\3\2\2\2\u00e7\u00e8\7c\2\2\u00e8\u00e9\7f\2\2\u00e9")
        buf.write("\u00ea\7f\2\2\u00ea\u00eb\7t\2\2\u00eb\u00ec\7g\2\2\u00ec")
        buf.write("\u00ed\7u\2\2\u00ed\u00ee\7u\2\2\u00ee\4\3\2\2\2\u00ef")
        buf.write("\u00f0\7d\2\2\u00f0\u00f1\7q\2\2\u00f1\u00f2\7q\2\2\u00f2")
        buf.write("\u00f3\7n\2\2\u00f3\6\3\2\2\2\u00f4\u00f5\7g\2\2\u00f5")
        buf.write("\u00f6\7p\2\2\u00f6\u00f7\7w\2\2\u00f7\u00f8\7o\2\2\u00f8")
        buf.write("\b\3\2\2\2\u00f9\u00fa\7g\2\2\u00fa\u00fb\7x\2\2\u00fb")
        buf.write("\u00fc\7g\2\2\u00fc\u00fd\7p\2\2\u00fd\u00fe\7v\2\2\u00fe")
        buf.write("\n\3\2\2\2\u00ff\u0100\7g\2\2\u0100\u0101\7x\2\2\u0101")
        buf.write("\u0102\7g\2\2\u0102\u0103\7p\2\2\u0103\u0104\7v\2\2\u0104")
        buf.write("\u0105\7n\2\2\u0105\u0106\7q\2\2\u0106\u0107\7i\2\2\u0107")
        buf.write("\f\3\2\2\2\u0108\u0109\7w\2\2\u0109\u010a\7k\2\2\u010a")
        buf.write("\u010b\7p\2\2\u010b\u010c\7v\2\2\u010c\16\3\2\2\2\u010d")
        buf.write("\u010e\7w\2\2\u010e\u010f\7k\2\2\u010f\u0110\7p\2\2\u0110")
        buf.write("\u0111\7v\2\2\u0111\u0112\7:\2\2\u0112\20\3\2\2\2\u0113")
        buf.write("\u0114\7k\2\2\u0114\u0115\7p\2\2\u0115\u0116\7u\2\2\u0116")
        buf.write("\u0117\7v\2\2\u0117\u0118\7a\2\2\u0118\u0119\7o\2\2\u0119")
        buf.write("\u011a\7c\2\2\u011a\u011b\7r\2\2\u011b\22\3\2\2\2\u011c")
        buf.write("\u011d\7k\2\2\u011d\u011e\7p\2\2\u011e\u011f\7v\2\2\u011f")
        buf.write("\24\3\2\2\2\u0120\u0121\7u\2\2\u0121\u0122\7v\2\2\u0122")
        buf.write("\u0123\7t\2\2\u0123\u0124\7k\2\2\u0124\u0125\7p\2\2\u0125")
        buf.write("\u0126\7i\2\2\u0126\26\3\2\2\2\u0127\u0128\7e\2\2\u0128")
        buf.write("\u0129\7q\2\2\u0129\u012a\7p\2\2\u012a\u012b\7v\2\2\u012b")
        buf.write("\u012c\7t\2\2\u012c\u012d\7c\2\2\u012d\u012e\7e\2\2\u012e")
        buf.write("\u012f\7v\2\2\u012f\30\3\2\2\2\u0130\u0131\7o\2\2\u0131")
        buf.write("\u0132\7c\2\2\u0132\u0133\7r\2\2\u0133\u0134\7r\2\2\u0134")
        buf.write("\u0135\7k\2\2\u0135\u0136\7p\2\2\u0136\u0137\7i\2\2\u0137")
        buf.write("\32\3\2\2\2\u0138\u0139\7d\2\2\u0139\u013a\7{\2\2\u013a")
        buf.write("\u013b\7v\2\2\u013b\u013c\7g\2\2\u013c\u013d\7u\2\2\u013d")
        buf.write("\34\3\2\2\2\u013e\u013f\7d\2\2\u013f\u0140\7{\2\2\u0140")
        buf.write("\u0141\7v\2\2\u0141\u0142\7g\2\2\u0142\u0143\7u\2\2\u0143")
        buf.write("\u0144\7\64\2\2\u0144\u0145\7\62\2\2\u0145\36\3\2\2\2")
        buf.write("\u0146\u0147\7d\2\2\u0147\u0148\7{\2\2\u0148\u0149\7v")
        buf.write("\2\2\u0149\u014a\7g\2\2\u014a\u014b\7u\2\2\u014b\u014c")
        buf.write("\7\65\2\2\u014c\u014d\7\64\2\2\u014d \3\2\2\2\u014e\u014f")
        buf.write("\7c\2\2\u014f\u0150\7f\2\2\u0150\u0151\7f\2\2\u0151\"")
        buf.write("\3\2\2\2\u0152\u0153\7c\2\2\u0153\u0154\7u\2\2\u0154\u0155")
        buf.write("\7u\2\2\u0155\u0156\7g\2\2\u0156\u0157\7t\2\2\u0157\u0158")
        buf.write("\7v\2\2\u0158$\3\2\2\2\u0159\u015a\7d\2\2\u015a\u015b")
        buf.write("\7c\2\2\u015b\u015c\7n\2\2\u015c\u015d\7c\2\2\u015d\u015e")
        buf.write("\7p\2\2\u015e\u015f\7e\2\2\u015f\u0160\7g\2\2\u0160&\3")
        buf.write("\2\2\2\u0161\u0162\7e\2\2\u0162\u0163\7c\2\2\u0163\u0164")
        buf.write("\7n\2\2\u0164\u0165\7n\2\2\u0165(\3\2\2\2\u0166\u0167")
        buf.write("\7e\2\2\u0167\u0168\7q\2\2\u0168\u0169\7p\2\2\u0169\u016a")
        buf.write("\7u\2\2\u016a\u016b\7v\2\2\u016b\u016c\7t\2\2\u016c\u016d")
        buf.write("\7w\2\2\u016d\u016e\7e\2\2\u016e\u016f\7v\2\2\u016f\u0170")
        buf.write("\7q\2\2\u0170\u0171\7t\2\2\u0171*\3\2\2\2\u0172\u0173")
        buf.write("\7e\2\2\u0173\u0174\7q\2\2\u0174\u0175\7p\2\2\u0175\u0176")
        buf.write("\7v\2\2\u0176\u0177\7c\2\2\u0177\u0178\7k\2\2\u0178\u0179")
        buf.write("\7p\2\2\u0179\u017a\7u\2\2\u017a,\3\2\2\2\u017b\u017c")
        buf.write("\7e\2\2\u017c\u017d\7t\2\2\u017d\u017e\7g\2\2\u017e\u017f")
        buf.write("\7f\2\2\u017f\u0180\7k\2\2\u0180\u0181\7v\2\2\u0181.\3")
        buf.write("\2\2\2\u0182\u0183\7f\2\2\u0183\u0184\7g\2\2\u0184\u0185")
        buf.write("\7d\2\2\u0185\u0186\7k\2\2\u0186\u0187\7v\2\2\u0187\60")
        buf.write("\3\2\2\2\u0188\u0189\7f\2\2\u0189\u018a\7g\2\2\u018a\u018b")
        buf.write("\7h\2\2\u018b\u018c\7c\2\2\u018c\u018d\7w\2\2\u018d\u018e")
        buf.write("\7n\2\2\u018e\u018f\7v\2\2\u018f\62\3\2\2\2\u0190\u0191")
        buf.write("\7f\2\2\u0191\u0192\7g\2\2\u0192\u0193\7n\2\2\u0193\u0194")
        buf.write("\7g\2\2\u0194\u0195\7v\2\2\u0195\u0196\7g\2\2\u0196\64")
        buf.write("\3\2\2\2\u0197\u0198\7g\2\2\u0198\u0199\7n\2\2\u0199\u019a")
        buf.write("\7u\2\2\u019a\u019b\7g\2\2\u019b\66\3\2\2\2\u019c\u019d")
        buf.write("\7g\2\2\u019d\u019e\7V\2\2\u019e\u019f\7t\2\2\u019f\u01a0")
        buf.write("\7c\2\2\u01a0\u01a1\7p\2\2\u01a1\u01a2\7u\2\2\u01a2\u01a3")
        buf.write("\7h\2\2\u01a3\u01a4\7g\2\2\u01a4\u01a5\7t\2\2\u01a58\3")
        buf.write("\2\2\2\u01a6\u01a7\7g\2\2\u01a7\u01a8\7z\2\2\u01a8\u01a9")
        buf.write("\7k\2\2\u01a9\u01aa\7u\2\2\u01aa\u01ab\7v\2\2\u01ab\u01ac")
        buf.write("\7u\2\2\u01ac:\3\2\2\2\u01ad\u01ae\7h\2\2\u01ae\u01af")
        buf.write("\7q\2\2\u01af\u01b0\7t\2\2\u01b0<\3\2\2\2\u01b1\u01b2")
        buf.write("\7h\2\2\u01b2\u01b3\7q\2\2\u01b3\u01b4\7t\2\2\u01b4\u01b5")
        buf.write("\7c\2\2\u01b5\u01b6\7n\2\2\u01b6\u01b7\7n\2\2\u01b7>\3")
        buf.write("\2\2\2\u01b8\u01b9\7h\2\2\u01b9\u01ba\7w\2\2\u01ba\u01bb")
        buf.write("\7p\2\2\u01bb\u01bc\7e\2\2\u01bc\u01bd\7v\2\2\u01bd\u01be")
        buf.write("\7k\2\2\u01be\u01bf\7q\2\2\u01bf\u01c0\7p\2\2\u01c0@\3")
        buf.write("\2\2\2\u01c1\u01c2\7k\2\2\u01c2\u01c3\7h\2\2\u01c3B\3")
        buf.write("\2\2\2\u01c4\u01c5\7k\2\2\u01c5\u01c6\7p\2\2\u01c6D\3")
        buf.write("\2\2\2\u01c7\u01c8\7k\2\2\u01c8\u01c9\7p\2\2\u01c9\u01ca")
        buf.write("\7v\2\2\u01ca\u01cb\7a\2\2\u01cb\u01cc\7o\2\2\u01cc\u01cd")
        buf.write("\7k\2\2\u01cd\u01ce\7p\2\2\u01ceF\3\2\2\2\u01cf\u01d0")
        buf.write("\7k\2\2\u01d0\u01d1\7p\2\2\u01d1\u01d2\7v\2\2\u01d2\u01d3")
        buf.write("\7a\2\2\u01d3\u01d4\7o\2\2\u01d4\u01d5\7c\2\2\u01d5\u01d6")
        buf.write("\7z\2\2\u01d6H\3\2\2\2\u01d7\u01d8\7k\2\2\u01d8\u01d9")
        buf.write("\7v\2\2\u01d9\u01da\7g\2\2\u01daJ\3\2\2\2\u01db\u01dc")
        buf.write("\7k\2\2\u01dc\u01dd\7p\2\2\u01dd\u01de\7x\2\2\u01de\u01df")
        buf.write("\7c\2\2\u01df\u01e0\7t\2\2\u01e0\u01e1\7k\2\2\u01e1\u01e2")
        buf.write("\7c\2\2\u01e2\u01e3\7p\2\2\u01e3\u01e4\7v\2\2\u01e4L\3")
        buf.write("\2\2\2\u01e5\u01e6\7m\2\2\u01e6\u01e7\7g\2\2\u01e7\u01e8")
        buf.write("\7{\2\2\u01e8\u01e9\7u\2\2\u01e9N\3\2\2\2\u01ea\u01eb")
        buf.write("\7n\2\2\u01eb\u01ec\7g\2\2\u01ec\u01ed\7o\2\2\u01ed\u01ee")
        buf.write("\7o\2\2\u01ee\u01ef\7c\2\2\u01efP\3\2\2\2\u01f0\u01f1")
        buf.write("\7n\2\2\u01f1\u01f2\7g\2\2\u01f2\u01f3\7p\2\2\u01f3\u01f4")
        buf.write("\7i\2\2\u01f4\u01f5\7v\2\2\u01f5\u01f6\7j\2\2\u01f6R\3")
        buf.write("\2\2\2\u01f7\u01f8\7n\2\2\u01f8\u01f9\7q\2\2\u01f9\u01fa")
        buf.write("\7i\2\2\u01faT\3\2\2\2\u01fb\u01fc\7o\2\2\u01fc\u01fd")
        buf.write("\7q\2\2\u01fd\u01fe\7f\2\2\u01fe\u01ff\7k\2\2\u01ff\u0200")
        buf.write("\7h\2\2\u0200\u0201\7k\2\2\u0201\u0202\7g\2\2\u0202\u0203")
        buf.write("\7u\2\2\u0203V\3\2\2\2\u0204\u0205\7o\2\2\u0205\u0206")
        buf.write("\7q\2\2\u0206\u0207\7f\2\2\u0207\u0208\7k\2\2\u0208\u0209")
        buf.write("\7h\2\2\u0209\u020a\7k\2\2\u020a\u020b\7g\2\2\u020b\u020c")
        buf.write("\7u\2\2\u020c\u020d\7a\2\2\u020d\u020e\7c\2\2\u020e\u020f")
        buf.write("\7f\2\2\u020f\u0210\7f\2\2\u0210\u0211\7t\2\2\u0211\u0212")
        buf.write("\7g\2\2\u0212\u0213\7u\2\2\u0213\u0214\7u\2\2\u0214\u0215")
        buf.write("\7g\2\2\u0215\u0216\7u\2\2\u0216X\3\2\2\2\u0217\u0218")
        buf.write("\7p\2\2\u0218\u0219\7g\2\2\u0219\u021a\7y\2\2\u021aZ\3")
        buf.write("\2\2\2\u021b\u021c\7p\2\2\u021c\u021d\7q\2\2\u021d\u021e")
        buf.write("\7y\2\2\u021e\\\3\2\2\2\u021f\u0220\7q\2\2\u0220\u0221")
        buf.write("\7t\2\2\u0221\u0222\7k\2\2\u0222\u0223\7i\2\2\u0223\u0224")
        buf.write("\7k\2\2\u0224\u0225\7p\2\2\u0225^\3\2\2\2\u0226\u0227")
        buf.write("\7r\2\2\u0227\u0228\7c\2\2\u0228\u0229\7{\2\2\u0229\u022a")
        buf.write("\7c\2\2\u022a\u022b\7d\2\2\u022b\u022c\7n\2\2\u022c\u022d")
        buf.write("\7g\2\2\u022d`\3\2\2\2\u022e\u022f\7r\2\2\u022f\u0230")
        buf.write("\7q\2\2\u0230\u0231\7r\2\2\u0231b\3\2\2\2\u0232\u0233")
        buf.write("\7r\2\2\u0233\u0234\7q\2\2\u0234\u0235\7u\2\2\u0235\u0236")
        buf.write("\7v\2\2\u0236d\3\2\2\2\u0237\u0238\7r\2\2\u0238\u0239")
        buf.write("\7t\2\2\u0239\u023a\7g\2\2\u023af\3\2\2\2\u023b\u023c")
        buf.write("\7r\2\2\u023c\u023d\7t\2\2\u023d\u023e\7k\2\2\u023e\u023f")
        buf.write("\7p\2\2\u023f\u0240\7v\2\2\u0240h\3\2\2\2\u0241\u0242")
        buf.write("\7r\2\2\u0242\u0243\7t\2\2\u0243\u0244\7k\2\2\u0244\u0245")
        buf.write("\7x\2\2\u0245\u0246\7c\2\2\u0246\u0247\7v\2\2\u0247\u0248")
        buf.write("\7g\2\2\u0248j\3\2\2\2\u0249\u024a\7r\2\2\u024a\u024b")
        buf.write("\7w\2\2\u024b\u024c\7d\2\2\u024c\u024d\7n\2\2\u024d\u024e")
        buf.write("\7k\2\2\u024e\u024f\7e\2\2\u024fl\3\2\2\2\u0250\u0251")
        buf.write("\7r\2\2\u0251\u0252\7w\2\2\u0252\u0253\7u\2\2\u0253\u0254")
        buf.write("\7j\2\2\u0254n\3\2\2\2\u0255\u0256\7t\2\2\u0256\u0257")
        buf.write("\7g\2\2\u0257\u0258\7v\2\2\u0258\u0259\7w\2\2\u0259\u025a")
        buf.write("\7t\2\2\u025a\u025b\7p\2\2\u025bp\3\2\2\2\u025c\u025d")
        buf.write("\7t\2\2\u025d\u025e\7g\2\2\u025e\u025f\7v\2\2\u025f\u0260")
        buf.write("\7w\2\2\u0260\u0261\7t\2\2\u0261\u0262\7p\2\2\u0262\u0263")
        buf.write("\7u\2\2\u0263r\3\2\2\2\u0264\u0265\7t\2\2\u0265\u0266")
        buf.write("\7g\2\2\u0266\u0267\7x\2\2\u0267\u0268\7g\2\2\u0268\u0269")
        buf.write("\7t\2\2\u0269\u026a\7v\2\2\u026at\3\2\2\2\u026b\u026c")
        buf.write("\7u\2\2\u026c\u026d\7c\2\2\u026d\u026e\7h\2\2\u026e\u026f")
        buf.write("\7g\2\2\u026f\u0270\7a\2\2\u0270\u0271\7c\2\2\u0271\u0272")
        buf.write("\7f\2\2\u0272\u0273\7f\2\2\u0273v\3\2\2\2\u0274\u0275")
        buf.write("\7u\2\2\u0275\u0276\7c\2\2\u0276\u0277\7h\2\2\u0277\u0278")
        buf.write("\7g\2\2\u0278\u0279\7a\2\2\u0279\u027a\7f\2\2\u027a\u027b")
        buf.write("\7k\2\2\u027b\u027c\7x\2\2\u027cx\3\2\2\2\u027d\u027e")
        buf.write("\7u\2\2\u027e\u027f\7c\2\2\u027f\u0280\7h\2\2\u0280\u0281")
        buf.write("\7g\2\2\u0281\u0282\7a\2\2\u0282\u0283\7o\2\2\u0283\u0284")
        buf.write("\7q\2\2\u0284\u0285\7f\2\2\u0285z\3\2\2\2\u0286\u0287")
        buf.write("\7u\2\2\u0287\u0288\7c\2\2\u0288\u0289\7h\2\2\u0289\u028a")
        buf.write("\7g\2\2\u028a\u028b\7a\2\2\u028b\u028c\7o\2\2\u028c\u028d")
        buf.write("\7w\2\2\u028d\u028e\7n\2\2\u028e|\3\2\2\2\u028f\u0290")
        buf.write("\7u\2\2\u0290\u0291\7c\2\2\u0291\u0292\7h\2\2\u0292\u0293")
        buf.write("\7g\2\2\u0293\u0294\7a\2\2\u0294\u0295\7u\2\2\u0295\u0296")
        buf.write("\7w\2\2\u0296\u0297\7d\2\2\u0297~\3\2\2\2\u0298\u0299")
        buf.write("\7u\2\2\u0299\u029a\7g\2\2\u029a\u029b\7p\2\2\u029b\u029c")
        buf.write("\7f\2\2\u029c\u0080\3\2\2\2\u029d\u029e\7u\2\2\u029e\u029f")
        buf.write("\7g\2\2\u029f\u02a0\7p\2\2\u02a0\u02a1\7f\2\2\u02a1\u02a2")
        buf.write("\7g\2\2\u02a2\u02a3\7t\2\2\u02a3\u0082\3\2\2\2\u02a4\u02a5")
        buf.write("\7u\2\2\u02a5\u02a6\7r\2\2\u02a6\u02a7\7g\2\2\u02a7\u02a8")
        buf.write("\7e\2\2\u02a8\u0084\3\2\2\2\u02a9\u02aa\7u\2\2\u02aa\u02ab")
        buf.write("\7v\2\2\u02ab\u02ac\7t\2\2\u02ac\u02ad\7w\2\2\u02ad\u02ae")
        buf.write("\7e\2\2\u02ae\u02af\7v\2\2\u02af\u0086\3\2\2\2\u02b0\u02b1")
        buf.write("\7v\2\2\u02b1\u02b2\7j\2\2\u02b2\u02b3\7k\2\2\u02b3\u02b4")
        buf.write("\7u\2\2\u02b4\u0088\3\2\2\2\u02b5\u02b6\7v\2\2\u02b6\u02b7")
        buf.write("\7z\2\2\u02b7\u02b8\7a\2\2\u02b8\u02b9\7t\2\2\u02b9\u02ba")
        buf.write("\7g\2\2\u02ba\u02bb\7x\2\2\u02bb\u02bc\7g\2\2\u02bc\u02bd")
        buf.write("\7t\2\2\u02bd\u02be\7v\2\2\u02be\u02bf\7u\2\2\u02bf\u008a")
        buf.write("\3\2\2\2\u02c0\u02c1\7w\2\2\u02c1\u02c2\7k\2\2\u02c2\u02c3")
        buf.write("\7p\2\2\u02c3\u02c4\7v\2\2\u02c4\u02c5\7a\2\2\u02c5\u02c6")
        buf.write("\7o\2\2\u02c6\u02c7\7c\2\2\u02c7\u02c8\7z\2\2\u02c8\u008c")
        buf.write("\3\2\2\2\u02c9\u02ca\7x\2\2\u02ca\u02cb\7c\2\2\u02cb\u02cc")
        buf.write("\7n\2\2\u02cc\u02cd\7w\2\2\u02cd\u02ce\7g\2\2\u02ce\u008e")
        buf.write("\3\2\2\2\u02cf\u02d0\7v\2\2\u02d0\u02d1\7t\2\2\u02d1\u02d2")
        buf.write("\7w\2\2\u02d2\u02d9\7g\2\2\u02d3\u02d4\7h\2\2\u02d4\u02d5")
        buf.write("\7c\2\2\u02d5\u02d6\7n\2\2\u02d6\u02d7\7u\2\2\u02d7\u02d9")
        buf.write("\7g\2\2\u02d8\u02cf\3\2\2\2\u02d8\u02d3\3\2\2\2\u02d9")
        buf.write("\u0090\3\2\2\2\u02da\u02dc\t\2\2\2\u02db\u02da\3\2\2\2")
        buf.write("\u02dc\u02dd\3\2\2\2\u02dd\u02db\3\2\2\2\u02dd\u02de\3")
        buf.write("\2\2\2\u02de\u0092\3\2\2\2\u02df\u02e0\7p\2\2\u02e0\u02e1")
        buf.write("\7w\2\2\u02e1\u02e2\7n\2\2\u02e2\u02e3\7n\2\2\u02e3\u0094")
        buf.write("\3\2\2\2\u02e4\u02e6\7$\2\2\u02e5\u02e7\5\u0097L\2\u02e6")
        buf.write("\u02e5\3\2\2\2\u02e6\u02e7\3\2\2\2\u02e7\u02e8\3\2\2\2")
        buf.write("\u02e8\u02e9\7$\2\2\u02e9\u0096\3\2\2\2\u02ea\u02ec\5")
        buf.write("\u0099M\2\u02eb\u02ea\3\2\2\2\u02ec\u02ed\3\2\2\2\u02ed")
        buf.write("\u02eb\3\2\2\2\u02ed\u02ee\3\2\2\2\u02ee\u0098\3\2\2\2")
        buf.write("\u02ef\u02f2\n\3\2\2\u02f0\u02f2\5\u009bN\2\u02f1\u02ef")
        buf.write("\3\2\2\2\u02f1\u02f0\3\2\2\2\u02f2\u009a\3\2\2\2\u02f3")
        buf.write("\u02f4\7^\2\2\u02f4\u02f5\13\2\2\2\u02f5\u009c\3\2\2\2")
        buf.write("\u02f6\u02f7\7#\2\2\u02f7\u009e\3\2\2\2\u02f8\u02f9\7")
        buf.write("(\2\2\u02f9\u02fa\7(\2\2\u02fa\u00a0\3\2\2\2\u02fb\u02fc")
        buf.write("\7~\2\2\u02fc\u02fd\7~\2\2\u02fd\u00a2\3\2\2\2\u02fe\u02ff")
        buf.write("\7?\2\2\u02ff\u0300\7@\2\2\u0300\u00a4\3\2\2\2\u0301\u0302")
        buf.write("\7?\2\2\u0302\u0303\7?\2\2\u0303\u0304\7@\2\2\u0304\u00a6")
        buf.write("\3\2\2\2\u0305\u0306\7>\2\2\u0306\u0307\7?\2\2\u0307\u0308")
        buf.write("\7?\2\2\u0308\u0309\7@\2\2\u0309\u00a8\3\2\2\2\u030a\u030b")
        buf.write("\7?\2\2\u030b\u030c\7?\2\2\u030c\u00aa\3\2\2\2\u030d\u030e")
        buf.write("\7#\2\2\u030e\u030f\7?\2\2\u030f\u00ac\3\2\2\2\u0310\u0311")
        buf.write("\7>\2\2\u0311\u0312\7?\2\2\u0312\u00ae\3\2\2\2\u0313\u0314")
        buf.write("\7@\2\2\u0314\u0315\7?\2\2\u0315\u00b0\3\2\2\2\u0316\u0317")
        buf.write("\7>\2\2\u0317\u00b2\3\2\2\2\u0318\u0319\7@\2\2\u0319\u00b4")
        buf.write("\3\2\2\2\u031a\u031b\7/\2\2\u031b\u031c\7@\2\2\u031c\u00b6")
        buf.write("\3\2\2\2\u031d\u031e\7?\2\2\u031e\u00b8\3\2\2\2\u031f")
        buf.write("\u0320\7-\2\2\u0320\u0321\7?\2\2\u0321\u00ba\3\2\2\2\u0322")
        buf.write("\u0323\7/\2\2\u0323\u0324\7?\2\2\u0324\u00bc\3\2\2\2\u0325")
        buf.write("\u0326\7-\2\2\u0326\u00be\3\2\2\2\u0327\u0328\7/\2\2\u0328")
        buf.write("\u00c0\3\2\2\2\u0329\u032a\7,\2\2\u032a\u00c2\3\2\2\2")
        buf.write("\u032b\u032c\7\61\2\2\u032c\u00c4\3\2\2\2\u032d\u032e")
        buf.write("\7\'\2\2\u032e\u00c6\3\2\2\2\u032f\u0330\7}\2\2\u0330")
        buf.write("\u00c8\3\2\2\2\u0331\u0332\7\177\2\2\u0332\u00ca\3\2\2")
        buf.write("\2\u0333\u0334\7]\2\2\u0334\u00cc\3\2\2\2\u0335\u0336")
        buf.write("\7_\2\2\u0336\u00ce\3\2\2\2\u0337\u0338\7*\2\2\u0338\u00d0")
        buf.write("\3\2\2\2\u0339\u033a\7+\2\2\u033a\u00d2\3\2\2\2\u033b")
        buf.write("\u033c\7=\2\2\u033c\u00d4\3\2\2\2\u033d\u033e\7.\2\2\u033e")
        buf.write("\u00d6\3\2\2\2\u033f\u0340\7\60\2\2\u0340\u00d8\3\2\2")
        buf.write("\2\u0341\u0342\7<\2\2\u0342\u00da\3\2\2\2\u0343\u0347")
        buf.write("\5\u00ddo\2\u0344\u0346\5\u00dfp\2\u0345\u0344\3\2\2\2")
        buf.write("\u0346\u0349\3\2\2\2\u0347\u0345\3\2\2\2\u0347\u0348\3")
        buf.write("\2\2\2\u0348\u00dc\3\2\2\2\u0349\u0347\3\2\2\2\u034a\u034b")
        buf.write("\t\4\2\2\u034b\u00de\3\2\2\2\u034c\u034d\t\5\2\2\u034d")
        buf.write("\u00e0\3\2\2\2\u034e\u0350\t\6\2\2\u034f\u034e\3\2\2\2")
        buf.write("\u0350\u0351\3\2\2\2\u0351\u034f\3\2\2\2\u0351\u0352\3")
        buf.write("\2\2\2\u0352\u0353\3\2\2\2\u0353\u0354\bq\2\2\u0354\u00e2")
        buf.write("\3\2\2\2\u0355\u0356\7\61\2\2\u0356\u0357\7,\2\2\u0357")
        buf.write("\u035b\3\2\2\2\u0358\u035a\13\2\2\2\u0359\u0358\3\2\2")
        buf.write("\2\u035a\u035d\3\2\2\2\u035b\u035c\3\2\2\2\u035b\u0359")
        buf.write("\3\2\2\2\u035c\u035e\3\2\2\2\u035d\u035b\3\2\2\2\u035e")
        buf.write("\u035f\7,\2\2\u035f\u0360\7\61\2\2\u0360\u0361\3\2\2\2")
        buf.write("\u0361\u0362\br\3\2\u0362\u00e4\3\2\2\2\u0363\u0364\7")
        buf.write("\61\2\2\u0364\u0365\7\61\2\2\u0365\u0369\3\2\2\2\u0366")
        buf.write("\u0368\n\7\2\2\u0367\u0366\3\2\2\2\u0368\u036b\3\2\2\2")
        buf.write("\u0369\u0367\3\2\2\2\u0369\u036a\3\2\2\2\u036a\u036c\3")
        buf.write("\2\2\2\u036b\u0369\3\2\2\2\u036c\u036d\bs\3\2\u036d\u00e6")
        buf.write("\3\2\2\2\f\2\u02d8\u02dd\u02e6\u02ed\u02f1\u0347\u0351")
        buf.write("\u035b\u0369\4\b\2\2\2\3\2")
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
    ORIGIN = 46
    PAYABLE = 47
    POP = 48
    POST = 49
    PRE = 50
    PRINT = 51
    PRIVATE = 52
    PUBLIC = 53
    PUSH = 54
    RETURN = 55
    RETURNS = 56
    REVERT = 57
    SAFEADD = 58
    SAFEDIV = 59
    SAFEMOD = 60
    SAFEMUL = 61
    SAFESUB = 62
    SEND = 63
    SENDER = 64
    SPEC = 65
    STRUCT = 66
    THIS = 67
    TXREVERTS = 68
    UINT_MAX = 69
    VALUE = 70
    BoolLiteral = 71
    IntLiteral = 72
    NullLiteral = 73
    StringLiteral = 74
    LNOT = 75
    LAND = 76
    LOR = 77
    MAPUPD = 78
    IMPL = 79
    BIMPL = 80
    EQ = 81
    NE = 82
    LE = 83
    GE = 84
    LT = 85
    GT = 86
    RARROW = 87
    ASSIGN = 88
    INSERT = 89
    REMOVE = 90
    PLUS = 91
    SUB = 92
    MUL = 93
    DIV = 94
    MOD = 95
    LBRACE = 96
    RBRACE = 97
    LBRACK = 98
    RBRACK = 99
    LPAREN = 100
    RPAREN = 101
    SEMI = 102
    COMMA = 103
    DOT = 104
    COLON = 105
    Iden = 106
    Whitespace = 107
    BlockComment = 108
    LineComment = 109

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
            "'new'", "'now'", "'origin'", "'payable'", "'pop'", "'post'", 
            "'pre'", "'print'", "'private'", "'public'", "'push'", "'return'", 
            "'returns'", "'revert'", "'safe_add'", "'safe_div'", "'safe_mod'", 
            "'safe_mul'", "'safe_sub'", "'send'", "'sender'", "'spec'", 
            "'struct'", "'this'", "'tx_reverts'", "'uint_max'", "'value'", 
            "'null'", "'!'", "'&&'", "'||'", "'=>'", "'==>'", "'<==>'", 
            "'=='", "'!='", "'<='", "'>='", "'<'", "'>'", "'->'", "'='", 
            "'+='", "'-='", "'+'", "'-'", "'*'", "'/'", "'%'", "'{'", "'}'", 
            "'['", "']'", "'('", "')'", "';'", "','", "'.'", "':'" ]

    symbolicNames = [ "<INVALID>",
            "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", "UINT", "UINT8", 
            "INSTMAP", "INT", "STRING", "CONTRACT", "MAP", "BYTES", "BYTES20", 
            "BYTES32", "ADD", "ASSERT", "BALANCE", "CALL", "CONSTR", "CONTAINS", 
            "CREDIT", "DEBIT", "DEFAULT", "DELETE", "ELSE", "ETRANSFER", 
            "EXISTS", "FOR", "FORALL", "FUNCTION", "IF", "IN", "INT_MIN", 
            "INT_MAX", "ITE", "INVARIANT", "KEYS", "LEMMA", "LENGTH", "LOG", 
            "MODIFIES", "MODIFIESA", "NEW", "NOW", "ORIGIN", "PAYABLE", 
            "POP", "POST", "PRE", "PRINT", "PRIVATE", "PUBLIC", "PUSH", 
            "RETURN", "RETURNS", "REVERT", "SAFEADD", "SAFEDIV", "SAFEMOD", 
            "SAFEMUL", "SAFESUB", "SEND", "SENDER", "SPEC", "STRUCT", "THIS", 
            "TXREVERTS", "UINT_MAX", "VALUE", "BoolLiteral", "IntLiteral", 
            "NullLiteral", "StringLiteral", "LNOT", "LAND", "LOR", "MAPUPD", 
            "IMPL", "BIMPL", "EQ", "NE", "LE", "GE", "LT", "GT", "RARROW", 
            "ASSIGN", "INSERT", "REMOVE", "PLUS", "SUB", "MUL", "DIV", "MOD", 
            "LBRACE", "RBRACE", "LBRACK", "RBRACK", "LPAREN", "RPAREN", 
            "SEMI", "COMMA", "DOT", "COLON", "Iden", "Whitespace", "BlockComment", 
            "LineComment" ]

    ruleNames = [ "ADDR", "BOOL", "ENUM", "EVENT", "EVENTLOG", "UINT", "UINT8", 
                  "INSTMAP", "INT", "STRING", "CONTRACT", "MAP", "BYTES", 
                  "BYTES20", "BYTES32", "ADD", "ASSERT", "BALANCE", "CALL", 
                  "CONSTR", "CONTAINS", "CREDIT", "DEBIT", "DEFAULT", "DELETE", 
                  "ELSE", "ETRANSFER", "EXISTS", "FOR", "FORALL", "FUNCTION", 
                  "IF", "IN", "INT_MIN", "INT_MAX", "ITE", "INVARIANT", 
                  "KEYS", "LEMMA", "LENGTH", "LOG", "MODIFIES", "MODIFIESA", 
                  "NEW", "NOW", "ORIGIN", "PAYABLE", "POP", "POST", "PRE", 
                  "PRINT", "PRIVATE", "PUBLIC", "PUSH", "RETURN", "RETURNS", 
                  "REVERT", "SAFEADD", "SAFEDIV", "SAFEMOD", "SAFEMUL", 
                  "SAFESUB", "SEND", "SENDER", "SPEC", "STRUCT", "THIS", 
                  "TXREVERTS", "UINT_MAX", "VALUE", "BoolLiteral", "IntLiteral", 
                  "NullLiteral", "StringLiteral", "StringCharacters", "StringCharacter", 
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


