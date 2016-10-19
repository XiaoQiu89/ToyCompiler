using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyCompiler.Scanner
{
    public class CodeParser
    {
        public bool EndOfFile { get { return Context.EndOfFile; } }
        private ExpressionParser exParser;
        public CodeParser(ParserContext context)
        {
            this.Context = context;
            exParser = new ExpressionParser(context);
        }


        public ParserContext Context
        {
            get;
            set;
        }


        public Token CurrentToken
        {
            get
            {
                return Context.CurrentToken;
            }
        }

        public char CurrentCharacter
        {
            get { return Context.CurrentCharacter; }
        }

        public AstTranslationUnit ParseFile()
        {
            //IList<Token> codes = new List<Token>();
            //do
            //{
            //    codes.Add(Context.GetNextToken());
            //} while (!EndOfFile);

            AstTranslationUnit unit = ParseTranslationUnit();
            return unit;
        }

        protected void NextToken()
        {
            Context.GetNextToken();
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

            NextToken();
            while (CurrentToken.Kind != TokenKind.TK_END)
            {
                if (CurrentToken.Kind == TokenKind.TK_SEMICOLON)
                {
                    NextToken();
                    continue;
                }

                unit.ExtDecls.Add(ParseExternalDeclaration());
            }


            return unit;
        }

        public AstDeclaration ParseExternalDeclaration()
        {
            AstDeclaration decl = ParseCommonHeader();

            return decl;
        }

        public AstDeclaration ParseCommonHeader()
        {
            AstDeclaration decl = new AstDeclaration();
            decl.Specifiers = ParserDeclarationSpecifiers();

            if (CurrentToken.Kind != TokenKind.TK_SEMICOLON)
            {
                decl.Init.Add(ParseInitDeclarator());
                while (CurrentToken.Kind == TokenKind.TK_COMMA)
                {
                    NextToken();
                    decl.Init.Add(ParseInitDeclarator());
                }
            }

            return decl;
        }

        public AstSpecifiers ParserDeclarationSpecifiers()
        {
            AstSpecifiers specifiers = new AstSpecifiers();

            while (true)
            {
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
            }
            //return specifiers;
        }

        public AstInitDeclarator ParseInitDeclarator()
        {
            AstInitDeclarator initDec = new AstInitDeclarator();
            initDec.Declarator = ParseDeclarator();
            if (CurrentToken.Kind == TokenKind.TK_ASSIGN)
            {
                NextToken();
                initDec.Initializer = new AstInitializer
                {
                    Initializer = exParser.ParseAssignmentExpression() // 解析赋值表达式
                };
            }
            return initDec;
        }

        public AstDeclarator ParseDeclarator()
        {
            if (CurrentToken.Kind == TokenKind.TK_MUL)
            {
            }
            return ParsePostfixDeclarator();
        }

        public AstDeclarator ParsePostfixDeclarator()
        {
            AstArrayDeclarator arrDecl = new AstArrayDeclarator();
            AstFunctionDeclarator funDecl = new AstFunctionDeclarator();
            AstDeclarator decl = ParseDirectDeclarator();

            while (true)
            {
                if (CurrentToken.Kind == TokenKind.TK_LBRACKET)
                {
                    arrDecl.Declarator = decl;
                    NextToken();
                    if (CurrentToken.Kind != TokenKind.TK_RBRACKET)
                    {
                        arrDecl.Expr = null; // 解析表达式
                    }
                    Context.DoExpect(TokenKind.TK_RBRACKET);
                    return arrDecl;
                }

                else if (CurrentToken.Kind == TokenKind.TK_LPAREN)
                {
                    funDecl.Declarator = decl;
                    NextToken();
                    if (IsTypeName(CurrentToken.Kind))
                    {
                        funDecl.ParamList = null; // 解析函数参数列表
                    }
                    else
                    {
                        // 解析函数调用
                    }
                    Context.DoExpect(TokenKind.TK_RPAREN);
                    return funDecl;
                }
                else
                {
                    return decl;
                }
            }
        }

        public AstDeclarator ParseDirectDeclarator()
        {
            AstNameDeclarator decl = new AstNameDeclarator();
            if (CurrentToken.Kind == TokenKind.TK_ID)
            {
                decl.Name = CurrentToken.Value;
                decl.Kind = NodeKind.NK_Token;
                decl.Token = CurrentToken;
                NextToken();
            }
            return decl;
        }

        private bool IsTypeName(TokenKind kind)
        {
            return kind > TokenKind.TK_AUTO && kind < TokenKind.TK_VOID;
        }
    }
}
