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
        private string Path;
        private SourceLocation location;
        //private CodeParser parser;
        private StringBuilder _primaryBuffer = new StringBuilder();
        private IDictionary<char, TokenParser> parserHandlers 
            = new Dictionary<char, TokenParser>();
        public delegate TokenKind TokenParser();
        private const int END_OF_FILE = 255;
        private TokenKind _currentKind;
        private Token _token;

        private IDictionary<int, TokenKind> assignOP = new Dictionary<int, TokenKind>
        {
            {'+',TokenKind.TK_ADD_ASSIGN},{'-',TokenKind.TK_SUB_ASSIGN},
            {'*',TokenKind.TK_MUL_ASSIGN},{'/',TokenKind.TK_DIV_ASSIGN},
            {'%',TokenKind.TK_MOD_ASSIGN},{'<',TokenKind.TK_LESS_EQ},
            {'>',TokenKind.TK_GREAT_EQ},{'&',TokenKind.TK_BITAND_ASSIGN},
            {'^',TokenKind.TK_BITXOR_ASSIGN},{'|',TokenKind.TK_BITOR_ASSIGN}
        };

        public ParserContext(LookaheadTextReader reader, string path)
        {
            this.reader = reader;
            //this.parser = parser;
            this.Path = path;
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

            //parserHandlers['\''] = ParseOperatorSymbol('\'', TokenKind.TK_INTCONST); // 解析字符
            //parserHandlers['\"'] = ParseOperatorSymbol('\"', TokenKind.TK_STRING); // 解析字符串
            parserHandlers['+'] = ParseOperatorSymbol('+', TokenKind.TK_ADD);
            parserHandlers['-'] = ParseOperatorSymbol('-', TokenKind.TK_SUB);
            parserHandlers['*'] = ParseOperatorSymbol('*', TokenKind.TK_MUL);
            parserHandlers['/'] = ParseOperatorSymbol('/', TokenKind.TK_DIV);
            parserHandlers['%'] = ParseOperatorSymbol('%', TokenKind.TK_MOD);
            parserHandlers['<'] = ParseOperatorSymbol('<', TokenKind.TK_LESS);
            parserHandlers['>'] = ParseOperatorSymbol('>', TokenKind.TK_GREAT);
            parserHandlers['!'] = ParseOperatorSymbol('!', TokenKind.TK_NOT);
            parserHandlers['='] = ParseOperatorSymbol('=', TokenKind.TK_ASSIGN);
            parserHandlers['|'] = ParseOperatorSymbol('|', TokenKind.TK_BITOR);
            parserHandlers['&'] = ParseOperatorSymbol('&', TokenKind.TK_BITAND);
            parserHandlers['^'] = ParseOperatorSymbol('^', TokenKind.TK_BITXOR);
            parserHandlers['.'] = ParseOperatorSymbol('.', TokenKind.TK_DOT);

            parserHandlers['{'] = ParseOperatorSymbol('{', TokenKind.TK_LBRACE);
            parserHandlers['}'] = ParseOperatorSymbol('}', TokenKind.TK_RBRACE);
            parserHandlers['['] = ParseOperatorSymbol('[', TokenKind.TK_LBRACKET);
            parserHandlers[']'] = ParseOperatorSymbol(']', TokenKind.TK_RBRACKET);
            parserHandlers['('] = ParseOperatorSymbol('(', TokenKind.TK_LPAREN);
            parserHandlers[')'] = ParseOperatorSymbol(')', TokenKind.TK_RPAREN);
            parserHandlers[','] = ParseOperatorSymbol(',', TokenKind.TK_COMMA);
            parserHandlers[';'] = ParseOperatorSymbol(';', TokenKind.TK_SEMICOLON);
            parserHandlers['~'] = ParseOperatorSymbol('~', TokenKind.TK_COMP);
            parserHandlers['?'] = ParseOperatorSymbol('?', TokenKind.TK_QUESTION);
            parserHandlers[':'] = ParseOperatorSymbol(':', TokenKind.TK_COLON);
        }

        /// <summary>
        /// 当前解析到的token词法单元
        /// </summary>
        public Token CurrentToken
        {
            get
            {
                return _token;
            }
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
            return TokenKind.TK_INTCONST;
        }

        public TokenKind ParseBadChar()
        {
            SkipCurrent();
            //NextToken();
            return TokenKind.TK_END;
        }

        public void NextToken()
        {
            ResetBuffers();
            _currentKind = parserHandlers[CurrentCharacter]();
            // 关键字检测
            //return token;
        }

        public void GetNextToken()
        {
            NextToken();
            _token = new Token
            {
                Kind = _currentKind,
                Value = _primaryBuffer.ToString(),
                Location = location
            };
        }

        public bool DoExpect(TokenKind kind)
        {
            if (CurrentToken.Kind == kind)
            {
                GetNextToken();
                return true;
            }
            return false;
        }


        public TokenParser ParseOperatorSymbol(char symbol, TokenKind kind)
        {
            return () =>
            {
                int ch = reader.Read();
                _primaryBuffer.Append((char)ch);
                if (ch == symbol)
                {
                    int nextCh = reader.Peek();
                    if (nextCh == '=') // 符合赋值操作符
                    {
                        reader.Read();
                        if (assignOP.Keys.Contains(ch))
                            return assignOP[ch];
                        return TokenKind.TK_END;
                    }
                    if (nextCh == symbol) // ++、--
                    {
                        reader.Read();
                        if (symbol == '+') return TokenKind.TK_INC; // ++
                        if (symbol == '-') return TokenKind.TK_DEC; // --
                        if (symbol == '=') return TokenKind.TK_EQUAL; // ==
                        bool hasAssign = false;
                        if (reader.Peek() == '=') // 检测>>,>>=,<<,<<=
                        {
                            hasAssign = true;
                            reader.Read();
                        }
                        if (symbol == '>') return hasAssign ? TokenKind.TK_RSHIFT_ASSIGN : TokenKind.TK_RSHIFT;
                        if (symbol == '<') return hasAssign ? TokenKind.TK_LSHIFT_ASSIGN : TokenKind.TK_LSHIFT;
                    }
                    return kind;
                }
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
