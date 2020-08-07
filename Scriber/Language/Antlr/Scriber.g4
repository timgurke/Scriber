grammar Scriber;

/* 
 * Parser Rules
 */

root                : line* EOF;
line                : lineContent? NEWLINE;
lineContent         : (environment | quotation | text)+;

environment         : inlineEnvironment | multilineEnvironment;
inlineEnvironment   : BRACKETOPEN text* BRACKETCLOSE AT identifier PARANTHESESOPEN arguments? PARANTHESESCLOSE;
multilineEnvironment: AT identifier PARANTHESESOPEN arguments? PARANTHESESCLOSE ((SPACE | NEWLINE)* CURLYOPEN (lineContent | NEWLINE)* CURLYCLOSE)?;

arguments           : argument (COMMA SPACE argument)*;
argument            : (jsonObject | standard | quotation);

jsonObject          : CURLYOPEN SPACE? (keyValuePair (COMMA SPACE keyValuePair)*)? SPACE? CURLYCLOSE;
keyValuePair        : key COLON SPACE value;
key                 : identifier;
value               : identifier;

quotation           : QUOTATION quoated? QUOTATION;

identifier          : TEXT+;
quoated             : (text | AT | CURLYOPEN | CURLYCLOSE | BRACKETOPEN | BRACKETCLOSE)+;
text                : (standard | COMMA)+;
standard            : (TEXT | PERCENT | CURRENCY | AMPERSAND | TILDE | UNDERSCORE | PARANTHESESOPEN | PARANTHESESCLOSE | COLON | COMMA | CARET | BACKSLASH | SPACE)+;

/*
 * Lexer Rules
 */
 
COMMENT             : '/*' .*? '*/' -> skip;
LINE_COMMENT        : '//' ~[\r\n]* -> skip;

NEWLINE             : ('\r'? '\n' | '\r')+ ;
AT                  : '@'+;
QUOTATION           : '"';
PERCENT             : '%';
CURRENCY            : '$';
AMPERSAND           : '&';
TILDE               : '~';
UNDERSCORE          : '_'+;
PARANTHESESOPEN     : '(';
PARANTHESESCLOSE    : ')';
CURLYOPEN           : '{';
CURLYCLOSE          : '}';
BRACKETOPEN         : '[';
BRACKETCLOSE        : ']';
COLON               : ':';
COMMA               : ',';
CARET               : '^'+;
BACKSLASH           : '\\'+;
SPACE               : ' ';

TEXT                : ([a-zA-Z0-9] | '-' | '.' | ';' | '?' | '!')+;