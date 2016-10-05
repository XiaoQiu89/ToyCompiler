using System;
using System.Linq;
using System.Globalization;

namespace ToyCompiler.Scanner
{
    public static class ParserHelper
    {

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

    }
}
