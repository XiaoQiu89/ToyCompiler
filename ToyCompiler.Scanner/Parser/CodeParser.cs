using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyCompiler.Scanner
{
    public class CodeParser
    {
        private Token _token;
        public bool EndOfFile { get { return Context.EndOfFile; } }

        public ParserContext Context
        {
            get;
            set;
        }

        public Token CurrentToken
        {
            get
            {
                return _token;
            }
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

        protected void NextToken()
        {
            _token = Context.GetNextToken();
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

        public AstTranslationUnit ParseTranslationUnit()
        {
            AstTranslationUnit unit = new AstTranslationUnit();
            AstNode tail = unit.ExtDecls;

            NextToken();
            while (CurrentToken.Kind != TokenKind.TK_END)
            {
                if (CurrentToken.Kind == TokenKind.TK_SEMICOLON)
                {
                    NextToken();
                    continue;
                }

                tail = ParseExternalDeclaration();
                tail = tail.Next;
            }


            return unit;
        }

        public AstNode ParseExternalDeclaration()
        {
            AstDeclaration decl = new AstDeclaration();

            return decl;
        }

        public AstDeclaration ParseCommonHeader()
        {
            AstDeclaration decl = new AstDeclaration();
            decl.Specifiers = ParserDeclarationSpecifiers();

            if (CurrentToken.Kind != TokenKind.TK_SEMICOLON)
            {

            }

            return decl;
        }

        public AstSpecifiers ParserDeclarationSpecifiers()
        {
            AstSpecifiers specifiers = new AstSpecifiers();

            switch (CurrentToken.Kind)
            {
                case TokenKind.TK_AUTO:
                case TokenKind.TK_REGISTER:
                case TokenKind.TK_STATIC:
                case TokenKind.TK_EXTERN:
                case TokenKind.TK_TYPEDEF:
                    AstSpecifier spec = new AstSpecifier
                    {
                        Kind = NodeKind.NK_Specifiers,
                        Token = CurrentToken
                    };
                    specifiers.StorageSpecifier = spec;
                    NextToken();
                    break;

                case TokenKind.TK_CONST:
                case TokenKind.TK_VOLATILE:
                    AstSpecifier tq = new AstSpecifier
                    {
                        Kind = NodeKind.NK_Specifiers,
                        Token = CurrentToken
                    };
                    specifiers.TypeQualifier = tq;
                    NextToken();
                    break;

                case TokenKind.TK_VOID:
                case TokenKind.TK_CHAR:
                case TokenKind.TK_SHORT:
                case TokenKind.TK_INT:
                case TokenKind.TK_INT64:
                case TokenKind.TK_LONG:
                case TokenKind.TK_FLOAT:
                case TokenKind.TK_DOUBLE:
                case TokenKind.TK_SIGNED:
                case TokenKind.TK_UNSIGNED:
                    AstSpecifier ts = new AstSpecifier
                    {
                        Kind = NodeKind.NK_Specifiers,
                        Token = CurrentToken
                    };
                    specifiers.TypeSpecifier = ts;
                    NextToken();
                    break;
                case TokenKind.TK_ID:
                    return specifiers;
                default:
                    return specifiers;
                    
            }
            return specifiers;

        }
    }
}
