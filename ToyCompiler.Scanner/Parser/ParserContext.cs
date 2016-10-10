using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public class ParserContext
    {
        internal LookaheadTextReader reader;
        private SourceLocation location;
        private CodeParser parser;
        private StringBuilder _primaryBuffer = new StringBuilder();
        private IDictionary<char, TokenParser> parserHandlers 
            = new Dictionary<char, TokenParser>();
        public delegate TokenKind TokenParser();
        private const int END_OF_FILE = 255;
        private TokenKind _currentKind;

        public ParserContext(LookaheadTextReader reader, CodeParser parser)
        {
            this.reader = reader;
            this.parser = parser;
            //location = new SourceLocation(1, 0);
            InitParser();
        }

        private void InitParser()
        {
            for (int i = 0; i < END_OF_FILE+1; i++)
            {
                char ch = (char)i;
                if (ParserHelper.IsLetter(ch))
                {
                    parserHandlers[ch] = ParseIdentifier;
                }

                else if (ParserHelper.IsDicimalDigit(ch))
                {
                    parserHandlers[ch] = ParseNumber;
                }

                else
                {
                    parserHandlers[ch] = ParseBadChar;
                }
            }

            parserHandlers['\''] = ParseSingleSynbol('\'', TokenKind.TK_INTCONST); // 解析字符
            parserHandlers['\"'] = ParseSingleSynbol('\"', TokenKind.TK_STRING); // 解析字符串
            parserHandlers['+'] = ParseSingleSynbol('+', TokenKind.TK_ADD);
            parserHandlers['-'] = ParseSingleSynbol('-', TokenKind.TK_SUB);
            parserHandlers['*'] = ParseSingleSynbol('*', TokenKind.TK_MUL);
            parserHandlers['/'] = ParseSingleSynbol('/', TokenKind.TK_DIV);
            parserHandlers['%'] = ParseSingleSynbol('%', TokenKind.TK_MOD);
            parserHandlers['<'] = ParseSingleSynbol('<', TokenKind.TK_LESS);
            parserHandlers['>'] = ParseSingleSynbol('>', TokenKind.TK_GREAT);
            parserHandlers['!'] = ParseSingleSynbol('!', TokenKind.TK_NOT);
            parserHandlers['='] = ParseSingleSynbol('=', TokenKind.TK_ASSIGN);
            parserHandlers['|'] = ParseSingleSynbol('|', TokenKind.TK_BITOR);
            parserHandlers['&'] = ParseSingleSynbol('&', TokenKind.TK_BITAND);
            parserHandlers['^'] = ParseSingleSynbol('^', TokenKind.TK_BITXOR);
            parserHandlers['.'] = ParseSingleSynbol('.', TokenKind.TK_DOT);

            parserHandlers['{'] = ParseSingleSynbol('{', TokenKind.TK_LBRACE);
            parserHandlers['}'] = ParseSingleSynbol('}', TokenKind.TK_RBRACE);
            parserHandlers['['] = ParseSingleSynbol('[', TokenKind.TK_LBRACKET);
            parserHandlers[']'] = ParseSingleSynbol(']', TokenKind.TK_RBRACKET);
            parserHandlers['('] = ParseSingleSynbol('(', TokenKind.TK_LPAREN);
            parserHandlers[')'] = ParseSingleSynbol(')', TokenKind.TK_RPAREN);
            parserHandlers[','] = ParseSingleSynbol(',', TokenKind.TK_COMMA);
            parserHandlers[';'] = ParseSingleSynbol(';', TokenKind.TK_SEMICOLON);
            parserHandlers['~'] = ParseSingleSynbol('~', TokenKind.TK_COMP);
            parserHandlers['?'] = ParseSingleSynbol('?', TokenKind.TK_QUESTION);
            parserHandlers[':'] = ParseSingleSynbol(':', TokenKind.TK_COLON);
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
            private set { location = value; }
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

        public TokenKind ParseIdentifier()
        {
            //if (ParserHelper.IsIdentifierPart(CurrentCharacter))
            //{
            //    return reader.ReadUtil(ch => ParserHelper.IsIdentifierPart(ch));
            //}
            //return null;
            string identifier = reader.ReadUtil(ch => ParserHelper.IsIdentifierPart(ch));
            _primaryBuffer.Append(identifier);
            return ParserHelper.KeywordKind(identifier);
        }

        public TokenKind ParseNumber()
        {
            string Num = reader.ReadUtil(ch => ParserHelper.IsDicimalDigit(ch));
            _primaryBuffer.Append(Num);
            return TokenKind.TK_INT;
        }

        public TokenKind ParseBadChar()
        {
            SkipCurrent();
            NextToken();
            return TokenKind.TK_END;
        }

        public void NextToken()
        {
            ResetBuffers();
            _currentKind = parserHandlers[CurrentCharacter]();
            // 关键字检测
            //return token;
        }

        public Token GetNextToken()
        {
            NextToken();
            return new Token
            {
                Kind = _currentKind,
                Value = _primaryBuffer.ToString(),
                Location = location
            };
        }


        public TokenParser ParseSingleSynbol(char symbol, TokenKind kind)
        {
            return () =>
            {
                int ch = reader.Read();
                _primaryBuffer.Append((char)ch);
                if (ch == symbol) return kind;
                return TokenKind.TK_END;
            };
        }

        private void ResetBuffers()
        {
            _primaryBuffer.Clear();
            reader.ReadWriteSpace();
            CurrentLocation = reader.CurrentLocation;
        }
    }
}
