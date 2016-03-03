using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// TextReader的读取扩展方法
    /// </summary>
    public static class ReaderExtention
    {
        public static string ReadUtil(this TextReader reader,Predicate<char> condition)
        {
            StringBuilder _primaryBuffer = new StringBuilder();
            int ch = -1;
            while (((ch = reader.Peek()) != -1) && condition((char)ch))
            {
                reader.Read();
                _primaryBuffer.Append((char)ch);
            }
            return _primaryBuffer.ToString();
        }

        public static string ReadWhile(this TextReader reader, Predicate<char> condition)
        {
            return reader.ReadUtil(ch => !condition(ch));
        }

        public static string ReadWriteSpace(this TextReader reader)
        {
            return reader.ReadUtil(ch => Char.IsWhiteSpace(ch));
        }
    }
}
