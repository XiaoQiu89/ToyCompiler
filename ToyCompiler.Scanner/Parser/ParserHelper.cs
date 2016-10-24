using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace ToyCompiler.Scanner
{
    public static class ParserHelper
    {
        private static IDictionary<string, TokenKind> dic =
            new Dictionary<string, TokenKind>
            {
                {"auto",TokenKind.TK_AUTO},
                {"break",TokenKind.TK_BREAK},
                {"case",TokenKind.TK_CASE},
                {"char",TokenKind.TK_CHAR},
                {"const",TokenKind.TK_CONST},
                {"continue",TokenKind.TK_CONTINUE},
                {"default",TokenKind.TK_DEFAULT},
                {"do",TokenKind.TK_DO},
                {"double",TokenKind.TK_DOUBLE},
                {"else",TokenKind.TK_ELSE},
                {"enum",TokenKind.TK_ENUM},
                {"extern",TokenKind.TK_EXTERN},
                {"float",TokenKind.TK_FLOAT},
                {"for",TokenKind.TK_FOR},
                {"goto",TokenKind.TK_GOTO},
                {"if",TokenKind.TK_IF},
                {"int",TokenKind.TK_INT},
                {"int64",TokenKind.TK_INT64},
                {"long",TokenKind.TK_LONG},
                {"register",TokenKind.TK_REGISTER},
                {"return",TokenKind.TK_RETURN},
                {"short",TokenKind.TK_SHORT},
                {"signed",TokenKind.TK_SIGNED},
                {"sizeof",TokenKind.TK_SIZEOF},
                {"static",TokenKind.TK_STATIC},
                {"struct",TokenKind.TK_STRUCT},
                {"switch",TokenKind.TK_SWITCH},
                {"typedef",TokenKind.TK_TYPEDEF},
                {"union",TokenKind.TK_UNION},
                {"unsigned",TokenKind.TK_UNSIGNED},
                {"void",TokenKind.TK_VOID},
                {"volatile",TokenKind.TK_VOLATILE},
                {"while",TokenKind.TK_WHILE}
            };

        /// <summary>
        /// 检测字符是否是标示符的一部分
        /// </summary>
        public static bool IsIdentifierPart(char value)
        {
            return IsLetter(value)
                || IsDicimalDigit(value)
                || IsFormatting(value)
                || IsCombining(value)
                || IsConnecting(value);
        }

        /// <summary>
        /// 检测字符是不是字母
        /// </summary>
        public static bool IsLetter(char value)
        {
            var ch = Char.GetUnicodeCategory(value);
            return ch == UnicodeCategory.UppercaseLetter // 大写字母
                || ch == UnicodeCategory.LowercaseLetter // 小写字母
                || ch == UnicodeCategory.TitlecaseLetter // 单词开头的首字母大写
                || ch == UnicodeCategory.ModifierLetter // 修饰符
                || ch == UnicodeCategory.OtherLetter
                || ch == UnicodeCategory.LetterNumber;
        }

        /// <summary>
        /// 检测字符是否是数字
        /// </summary>
        public static bool IsDicimalDigit(char value)
        {
            return Char.GetUnicodeCategory(value) == UnicodeCategory.DecimalDigitNumber;
        }

        /// <summary>
        /// 是否是一个换行符
        /// </summary>
        public static bool IsNewLine(char value)
        {
            return value == 0x000d
                || value == 0x000a
                || value == 0x2028
                || value == 0x2029;
        }

        /// <summary>
        /// 检测字符串是否是换行符
        /// </summary>
        public static bool IsNewLine(string value)
        {
            return (value.Length == 1 && ParserHelper.IsNewLine(value[0]))
                || (string.Equals(value, "\r\n", StringComparison.Ordinal));
        }

        public static bool IsFormatting(char value)
        {
            return Char.GetUnicodeCategory(value) == UnicodeCategory.Format;
        }

        public static bool IsCombining(char value)
        {
            var ch = Char.GetUnicodeCategory(value);
            return ch == UnicodeCategory.SpacingCombiningMark || ch == UnicodeCategory.NonSpacingMark;
        }

        public static bool IsConnecting(char value)
        {
            return Char.GetUnicodeCategory(value) == UnicodeCategory.ConnectorPunctuation;
        }

        public static TokenKind KeywordKind(string value)
        {
            string val = value.ToLower();
            if (dic.Keys.Contains(val))
                return dic[val];
            return TokenKind.TK_ID;
        }

        public static bool IsDeclaration(TokenKind kind)
        {
            return (kind > TokenKind.TK_AUTO && kind < TokenKind.TK_VOID) ||
                (kind > TokenKind.TK_ID && kind < TokenKind.TK_LDOUBLECONST);
        }
    }
}
