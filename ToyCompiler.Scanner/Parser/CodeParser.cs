using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyCompiler.Scanner
{
    public class CodeParser
    {

        public bool EndOfFile { get { return Context.EndOfFile; } }

        public ParserContext Context
        {
            get;
            set;
        }

        public char CurrentCharacter { get { return Context.CurrentCharacter; } }

        public void ParseFile()
        {
            IList<Token> codes = new List<Token>();
            do
            {
                codes.Add(Context.GetNextToken());
            } while (!EndOfFile);
        }

        /// <summary>
        /// 是否是一个空格或者换行符
        /// </summary>
        public bool RequireSingleWhitespace()
        {
            if (Char.IsWhiteSpace(CurrentCharacter))
            {
                if (ParserHelper.IsNewLine(CurrentCharacter))
                {
                    Context.AcceptNewLine();
                }
                else
                {
                    Context.AcceptCurrent();
                }
                return true;
            }
            return false;
        }

        public void SkipWriteSpace()
        {
            Context.reader.ReadWriteSpace();
        }
    }
}
