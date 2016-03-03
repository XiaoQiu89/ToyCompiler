using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public class ParserContext
    {
        internal TextReader reader;
        private SourceLocation location;
        private CodeParser parser;
        private StringBuilder _primaryBuffer = new StringBuilder();

        public ParserContext(TextReader reader, CodeParser parser)
        {
            this.reader = reader;
            this.parser = parser;
            location = new SourceLocation(1, 0);
        }

        /// <summary>
        /// 当前字符
        /// </summary>
        public char CurrentCharacter
        {
            get
            {
                int ch = reader.Peek();
                if (ch == -1)
                    return '\0';
                return (char)ch;
            }
        }

        /// <summary>
        /// 是否到达了文件的末尾
        /// </summary>
        public bool EndOfFile
        {
            get { return reader.Peek() == -1; }
        }

        /// <summary>
        /// 当前文件的读取位置
        /// </summary>
        public SourceLocation CurrentLocation
        {
            get { return location; }
        }

        public bool HasContent { get { return _primaryBuffer.Length > 0; } }

        /// <summary>
        /// 读取的内容
        /// </summary>
        public string Content { get { return _primaryBuffer.ToString(); } }

        public char AcceptCurrent()
        {
            char ch = '\0';
            if (!EndOfFile)
            {
                ch = CurrentCharacter;
                Append(ch);
                reader.Read();
            }
            return ch;
        }

        public void Append(char ch)
        {
            _primaryBuffer.Append(ch);
        }

        public bool SkipCurrent()
        {
            reader.Read();
            return reader.Peek() != -1;
        }

        public void AcceptNewLine()
        {
            if (CurrentCharacter == '\n')
            {
                AcceptCurrent();
            }
            else if (CurrentCharacter == '\r')
            {
                AcceptCurrent();
                if (CurrentCharacter == '\n')
                    AcceptCurrent();
            }
        }

        public string AcceptIdentifier()
        {
            if (ParserHelper.IsIdentifierPart(CurrentCharacter))
            {
                return reader.ReadUtil(ch => ParserHelper.IsIdentifierPart(ch));
            }
            return null;
        }
    }
}
