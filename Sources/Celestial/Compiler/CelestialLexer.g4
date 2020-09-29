lexer grammar CelestialLexer;

options {language=Python3;}

// Type names

ADDR	  : 'address' ;
BOOL      : 'bool' ;
ENUM      : 'enum' ;
EVENT     : 'event' ;
EVENTLOG  : 'eventlog' ;
UINT	  : 'uint' ;
UINT8     : 'uint8' ;
INSTMAP   : 'inst_map' ;
INT       : 'int' ;
STRING    : 'string' ;
CONTRACT  : 'contract' ;
MAP       : 'mapping' ;
BYTES     : 'bytes' ;
BYTES20   : 'bytes20' ;
BYTES32   : 'bytes32' ;

// Keywords

ADD       : 'add' ;
ASSERT    : 'assert' ;
BALANCE   : 'balance' ;
BCOINBASE : 'block.coinbase' ;
BDIFF     : 'block.difficulty' ;
BGASLIMIT : 'block.gaslimit' ;
BNUMBER   : 'block.number' ;
BTIMESTAMP: 'block.timestamp' ;
CALL      : 'call' ;
CONSTR    : 'constructor' ;
CONTAINS  : 'contains' ;
CREDIT    : 'credit' ;
DEBIT     : 'debit' ;
DEFAULT   : 'default' ;
DELETE	  : 'delete' ;
ELSE      : 'else' ;
EMIT      : 'emit' ;
ETRANSFER : 'eTransfer' ;
EXISTS    : 'exists' ;
FOR       : 'for' ;
FORALL    : 'forall';
FUNCTION  : 'function' ;
IF        : 'if' ;
IN        : 'in' ;
INT_MIN   : 'int_min' ;
INT_MAX   : 'int_max' ;
ITE       : 'ite' ;
INVARIANT : 'invariant' ;
KEYS      : 'keys' ;
LEMMA     : 'lemma' ;
LENGTH    : 'length' ;
LOG       : 'log' ;
MODIFIES  : 'modifies' ;
MODIFIESA : 'modifies_addresses' ;
NEW       : 'new' ;
PAYABLE   : 'payable' ;
POP       : 'pop' ;
POST      : 'post' ;
PRE       : 'pre' ;
PRINT     : 'print' ;
PRIVATE   : 'private' ;
PUBLIC    : 'public' ;
PUSH      : 'push' ;
RETURN    : 'return' ;
RETURNS   : 'returns' ;
REVERT	  : 'revert' ;
RREVERTS  : 'r_reverts' ;
SAFEADD   : 'safe_add' ;
SAFEDIV   : 'safe_div' ;
SAFEMOD   : 'safe_mod' ;
SAFEMUL   : 'safe_mul' ;
SAFESUB   : 'safe_sub' ;
SEND      : 'send' ;
SENDER    : 'sender' ;
SPEC      : 'spec' ;
STRUCT    : 'struct' ;
// SUMEQ     : 'sumEquals' ;
THIS      : 'this' ;
TXREVERTS : 'tx_reverts' ;
TXGASPRICE: 'tx.gasprice' ;
TXORIGIN  : 'tx.origin' ;
UINT_MAX  : 'uint_max' ;
VALUE     : 'value' ;

// Literals

BoolLiteral : 'true' | 'false' ;

IntLiteral : [0-9]+ ;

NullLiteral : 'null';

StringLiteral : '"' StringCharacters? '"' ;
fragment StringCharacters : StringCharacter+ ;
fragment StringCharacter : ~["\\] | EscapeSequence ;
fragment EscapeSequence : '\\' . ;

// Symbols

LNOT   : '!' ;
LAND   : '&&' ;
LOR    : '||' ;
MAPUPD : '=>' ;
IMPL   : '==>' ;
BIMPL  : '<==>' ;

EQ     : '==' ;
NE     : '!=' ;
LE     : '<=' ;
GE     : '>=' ;
LT     : '<'  ;
GT     : '>'  ;
RARROW : '->' ;

ASSIGN : '=' ;
INSERT : '+=' ;
REMOVE : '-=' ;

PLUS   : '+' ;
SUB    : '-' ;
MUL    : '*' ;
DIV    : '/' ;
MOD    : '%' ;

LBRACE : '{' ;
RBRACE : '}' ;
LBRACK : '[' ;
RBRACK : ']' ;
LPAREN : '(' ;
RPAREN : ')' ;
SEMI   : ';' ;
COMMA  : ',' ;
DOT    : '.' ;
COLON  : ':' ;

// Identifiers

Iden : PLetter PLetterOrDigit* ;
fragment PLetter : [a-zA-Z_] ;
fragment PLetterOrDigit : [a-zA-Z0-9_] ;

// Non-code regions

Whitespace : [ \t\r\n\f]+ -> skip ;
BlockComment : '/*' .*? '*/' -> channel(HIDDEN) ;
LineComment : '//' ~[\r\n]* -> channel(HIDDEN) ;
