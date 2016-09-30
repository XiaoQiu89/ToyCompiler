using System;

namespace ToyCompiler.Scanner.Parser.SyntaxTree
{
    public enum Token
    {
        /// <summary>
        /// auto关键字
        /// </summary>
        TK_AUTO,
        /// <summary>
        /// extern关键字
        /// </summary>
        TK_EXTERN,
        /// <summary>
        /// regester关键字
        /// </summary>
        TK_REGISTER,
        /// <summary>
        /// static关键字
        /// </summary>
        TK_STATIC,
        /// <summary>
        /// typedef关键字
        /// </summary>
        TK_TYPEDEF,
        /// <summary>
        /// const关键字
        /// </summary>
        TK_CONST,
        /// <summary>
        /// volatile关键字
        /// </summary>
        TK_VOLATILE,
        /// <summary>
        /// signed关键字
        /// </summary>
        TK_SIGNED,
        /// <summary>
        /// unsigned关键字
        /// </summary>
        TK_UNSIGNED,
        /// <summary>
        /// short关键字
        /// </summary>
        TK_SHORT,
        /// <summary>
        /// long关键字
        /// </summary>
        TK_LONG,
        /// <summary>
        /// char关键字
        /// </summary>
        TK_CHAR,
        /// <summary>
        /// int关键字
        /// </summary>
        TK_INT,
        /// <summary>
        /// int64关键字
        /// </summary>
        TK_INT64,
        /// <summary>
        /// float关键字
        /// </summary>
        TK_FLOAT,
        /// <summary>
        /// double关键字
        /// </summary>
        TK_DOUBLE,
        /// <summary>
        /// enum关键字
        /// </summary>
        TK_ENUM,
        /// <summary>
        /// struct关键字
        /// </summary>
        TK_STRUCT,
        /// <summary>
        /// union关键字
        /// </summary>
        TK_UNION,
        /// <summary>
        /// void关键字
        /// </summary>
        TK_VOID,
        /// <summary>
        /// break关键字
        /// </summary>
        TK_BREAK,
        /// <summary>
        /// case关键字
        /// </summary>
        TK_CASE,
        /// <summary>
        /// continue关键字
        /// </summary>
        TK_CONTINUE,
        /// <summary>
        /// default关键字
        /// </summary>
        TK_DEFAULT,
        /// <summary>
        /// do关键字
        /// </summary>
        TK_DO,
        /// <summary>
        /// else关键字
        /// </summary>
        TK_ELSE,
        /// <summary>
        /// for关键字
        /// </summary>
        TK_FOR,
        /// <summary>
        /// goto关键字
        /// </summary>
        TK_GOTO,
        /// <summary>
        /// if关键字
        /// </summary>
        TK_IF,
        /// <summary>
        /// return关键字
        /// </summary>
        TK_RETURN,
        /// <summary>
        /// swicth关键字
        /// </summary>
        TK_SWITCH,
        /// <summary>
        /// while关键字
        /// </summary>
        TK_WHILE,
        /// <summary>
        /// sizeof关键字
        /// </summary>
        TK_SIZEOF,

        /// <summary>
        /// 标示符
        /// </summary>
        TK_ID,
        /// <summary>
        /// int
        /// </summary>
        TK_INTCONST,
        /// <summary>
        /// unsigned int
        /// </summary>
        TK_UINTCONST,
        /// <summary>
        /// long
        /// </summary>
        TK_LONGCONST,
        /// <summary>
        /// unsigned long
        /// </summary>
        TK_ULONGCONST,
        /// <summary>
        /// long long
        /// </summary>
        TK_LLONGCONST,
        /// <summary>
        /// unsigned long long
        /// </summary>
        TK_ULLONGCONST,
        /// <summary>
        /// float
        /// </summary>
        TK_FLOATCONST,
        /// <summary>
        /// double
        /// </summary>
        TK_DOUBLECONST,
        /// <summary>
        /// long double
        /// </summary>
        TK_LDOUBLECONST,
        /// <summary>
        /// STR
        /// </summary>
        TK_STRING,
        /// <summary>
        /// WSTR
        /// </summary>
        TK_WIDESTRING,

        /// <summary>
        /// ","标点符号
        /// </summary>
        TK_COMMA, // ,
        /// <summary>
        /// "？"标点符号
        /// </summary>
        TK_QUESTION, // ?

        /// <summary>
        /// "："标点符号
        /// </summary>
        TK_COLON, // :

        /// <summary>
        /// "="运算符
        /// </summary>
        TK_ASSIGN, // =

        /// <summary>
        /// "|="运算符
        /// </summary>
        TK_BITOR_ASSIGN, // |=

        /// <summary>
        /// "^="运算符
        /// </summary>
        TK_BITXOR_ASSIGN, // ^=

        /// <summary>
        /// "&="运算符
        /// </summary>
        TK_LSHIFT_ASSIGN, // &=

        /// <summary>
        /// "<<="运算符
        /// </summary>
        TK_RSHIFT_ASSIGN, // <<=

        /// <summary>
        /// ">>="运算符
        /// </summary>
        TK_LSHIFT_ASSIGN, // >>=

        /// <summary>
        /// "+="运算符
        /// </summary>
        TK_ADD_ASSIGN, // +=

        /// <summary>
        /// "-="运算符
        /// </summary>
        TK_SUB_ASSIGN, // -=

        /// <summary>
        /// "*="运算符
        /// </summary>
        TK_MUL_ASSIGN, // *=

        /// <summary>
        /// ",/="运算符
        /// </summary>
        TK_DIV_ASSIGN, // /=

        /// <summary>
        /// "%="运算符
        /// </summary>
        TK_MOD_ASSIGN, // %=

        /// <summary>
        /// "||"运算符
        /// </summary>
        TK_OR, // ||

        /// <summary>
        /// "&&"运算符
        /// </summary>
        TK_AND, // &&

        /// <summary>
        /// "|"运算符
        /// </summary>
        TK_BITOR, // |

        /// <summary>
        /// "^"运算符
        /// </summary>
        TK_BITXOR, // ^
        /// <summary>
        /// "^"运算符
        /// </summary>
        TK_BITAND, // &
        /// <summary>
        /// "=="运算符
        /// </summary>
        TK_EQUAL, // ==
        /// <summary>
        /// "!="运算符
        /// </summary>
        TK_UNEQUAL, // !=
        /// <summary>
        /// ">"运算符
        /// </summary>
        TK_GREAT, // >
        /// <summary>
        /// "《"运算符
        /// </summary>
        TK_LESS, // <
        /// <summary>
        /// "》="运算符
        /// </summary>
        TK_GREAT_EQ, // >=
        /// <summary>
        /// "《="运算符
        /// </summary>
        TK_LESS_EQ, // <=
        /// <summary>
        /// "《《"运算符
        /// </summary>
        TK_LSHIFT, // <<
        /// <summary>
        /// "》》"运算符
        /// </summary>
        TK_RSHIFT, // >>
        /// <summary>
        /// "+"运算符
        /// </summary>
        TK_ADD, // +
        /// <summary>
        /// "-"运算符
        /// </summary>
        TK_SUB, // -
        /// <summary>
        /// "*"运算符
        /// </summary>
        TK_MUL, // *
        /// <summary>
        /// "/"运算符
        /// </summary>
        TK_DIV, // /
        /// <summary>
        /// "%"运算符
        /// </summary>
        TK_MOD, // %
        /// <summary>
        /// "++"运算符
        /// </summary>
        TK_INC, // ++
        /// <summary>
        /// "--"运算符
        /// </summary>
        TK_DEC, // --
        /// <summary>
        /// "非"运算符
        /// </summary>
        TK_NOT, // !
        /// <summary>
        /// "~"运算符
        /// </summary>
        TK_COMP, // ~
        /// <summary>
        /// "."运算符
        /// </summary>
        TK_DOT, // .
        /// <summary>
        /// "->"指针运算符
        /// </summary>
        TK_POINTER, // ->
        /// <summary>
        /// "（"符号
        /// </summary>
        TK_LPAREN, // (
        /// <summary>
        /// "）"符号
        /// </summary>
        TK_RPAREN, // )
        /// <summary>
        /// "["符号
        /// </summary>
        TK_LBRACKET, // [
        /// <summary>
        /// "]"符号
        /// </summary>
        TK_RBRACKET, // ]
        /// <summary>
        /// "{"符号
        /// </summary>
        TK_LBRACE, // {
        /// <summary>
        /// "}"符号
        /// </summary>
        TK_RBRACE, // }
        /// <summary>
        /// ";"符号
        /// </summary>
        TK_SEMICOLON, // ;
        /// <summary>
        /// "..."符号
        /// </summary>
        TK_ELLIPSIS, // ...
        /// <summary>
        /// "#"符号
        /// </summary>
        TK_POUND, // #
        /// <summary>
        /// "\n"符号
        /// </summary>
        TK_NEWLINE, // \n
        /// <summary>
        /// "EOF"符号
        /// </summary>
        TK_END // "EOF"
    }
}
