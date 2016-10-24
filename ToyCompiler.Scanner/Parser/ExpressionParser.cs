using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class ExpressionParser
    {
        private ParserContext Context;
        private CodeParser codeParser;
        private IDictionary<TokenKind, OpPrecedence> Prec =
            new Dictionary<TokenKind, OpPrecedence>
            {
                {TokenKind.TK_COMMA,new OpPrecedence(TokenKind.TK_COMMA,1,",")},
                {TokenKind.TK_ASSIGN,new OpPrecedence(TokenKind.TK_ASSIGN,2,"=")},
                {TokenKind.TK_BITOR_ASSIGN,new OpPrecedence(TokenKind.TK_BITOR_ASSIGN,2,"|=")},
                {TokenKind.TK_BITXOR_ASSIGN,new OpPrecedence(TokenKind.TK_BITXOR_ASSIGN,2,"^=")},
                {TokenKind.TK_BITAND_ASSIGN,new OpPrecedence(TokenKind.TK_BITAND_ASSIGN,2,"&=")},
                {TokenKind.TK_LSHIFT_ASSIGN,new OpPrecedence(TokenKind.TK_LSHIFT_ASSIGN,2,"<<=")},
                {TokenKind.TK_RSHIFT_ASSIGN,new OpPrecedence(TokenKind.TK_RSHIFT_ASSIGN,2,">>=")},
                {TokenKind.TK_ADD_ASSIGN,new OpPrecedence(TokenKind.TK_ADD_ASSIGN,2,"+=")},
                {TokenKind.TK_SUB_ASSIGN,new OpPrecedence(TokenKind.TK_SUB_ASSIGN,2,"-=")},
                {TokenKind.TK_MUL_ASSIGN,new OpPrecedence(TokenKind.TK_MUL_ASSIGN,2,"*=")},
                {TokenKind.TK_DIV_ASSIGN,new OpPrecedence(TokenKind.TK_DIV_ASSIGN,2,"/=")},
                {TokenKind.TK_MOD_ASSIGN,new OpPrecedence(TokenKind.TK_MOD_ASSIGN,2,"%=")},
                {TokenKind.TK_QUESTION,new OpPrecedence(TokenKind.TK_QUESTION,3,"?")},
                {TokenKind.TK_COLON,new OpPrecedence(TokenKind.TK_COLON,3,":")},
                {TokenKind.TK_OR,new OpPrecedence(TokenKind.TK_OR,4,"||")},
                {TokenKind.TK_AND,new OpPrecedence(TokenKind.TK_AND,5,"&&")},
                {TokenKind.TK_BITOR,new OpPrecedence(TokenKind.TK_BITOR,6,"|")},
                {TokenKind.TK_BITXOR,new OpPrecedence(TokenKind.TK_BITXOR,7,"^")},
                {TokenKind.TK_BITAND,new OpPrecedence(TokenKind.TK_BITAND,8,"&")},
                {TokenKind.TK_EQUAL,new OpPrecedence(TokenKind.TK_EQUAL,9,"==")},
                {TokenKind.TK_UNEQUAL,new OpPrecedence(TokenKind.TK_UNEQUAL,9,"!=")},
                {TokenKind.TK_GREAT,new OpPrecedence(TokenKind.TK_GREAT,10,">")},
                {TokenKind.TK_LESS,new OpPrecedence(TokenKind.TK_LESS,10,"<")},
                {TokenKind.TK_GREAT_EQ,new OpPrecedence(TokenKind.TK_GREAT_EQ,10,">=")},
                {TokenKind.TK_LESS_EQ,new OpPrecedence(TokenKind.TK_LESS_EQ,10,"<=")},
                {TokenKind.TK_LSHIFT,new OpPrecedence(TokenKind.TK_LSHIFT,11,"<<")},
                {TokenKind.TK_RSHIFT,new OpPrecedence(TokenKind.TK_RSHIFT,11,">>")},
                {TokenKind.TK_ADD,new OpPrecedence(TokenKind.TK_ADD,12,"+")},
                {TokenKind.TK_SUB,new OpPrecedence(TokenKind.TK_SUB,12,"-")},
                {TokenKind.TK_MUL,new OpPrecedence(TokenKind.TK_MUL,13,"*")},
                {TokenKind.TK_DIV,new OpPrecedence(TokenKind.TK_DIV,13,"/")},
                {TokenKind.TK_MOD,new OpPrecedence(TokenKind.TK_MOD,13,"%")},
                {TokenKind.TK_INC,new OpPrecedence(TokenKind.TK_INC,14,"++")},
                {TokenKind.TK_DEC,new OpPrecedence(TokenKind.TK_DEC,14,"--")}
            };

        public ExpressionParser(ParserContext context,CodeParser parser)
        {
            this.Context = context;
            this.codeParser = parser;
        }

        private void NextToken()
        {
            Context.GetNextToken();
        }

        public Token CurrentToken
        {
            get
            {
                return Context.CurrentToken;
            }
        }

        public AstExpression ParseExpression()
        {
            AstExpression expr = ParseAssignmentExpression();
            //AstBinaryOpExpression exprBin;

            while (CurrentToken.Kind == TokenKind.TK_COMMA)
            {
                NextToken();
                expr = new AstBinaryOpExpression
                {
                    Operator = TokenKind.TK_COMMA,
                    Token = Context.CurrentToken,
                    LeftExpr = expr,
                    RightExpr = ParseAssignmentExpression()
                };
            }

            return expr;
        }

        public AstExpression ParseAssignmentExpression()
        {
            return ParseConditionExpression();
        }

        public AstExpression ParseConditionExpression()
        {
            AstExpression expr = ParseBinaryExpression(Prec[TokenKind.TK_OR]);
            // 检测三目运算符
            if (CurrentToken.Kind == TokenKind.TK_QUESTION)
            {
                NextToken();
                AstExpression Then = ParseConditionExpression();
                NextToken();
                Context.DoExpect(TokenKind.TK_COLON);
                expr = new AstCondExpression
                {
                    Cond = expr,
                    Then = Then,
                    Else = ParseConditionExpression()
                };
            }
            return expr;
        }

        public AstExpression ParseBinaryExpression(OpPrecedence op)
        {
            AstExpression expr = ParseUnaryExpression();
            //AstExpression binExpr;
            NextToken();

            OpPrecedence newOp;
            while (IsBinaryOP(CurrentToken.Kind) && ((newOp= Prec[CurrentToken.Kind]) >= op))
            {
                Token token = CurrentToken;
                NextToken();
                expr = new AstBinaryOpExpression()
                {
                    Operator = token.Kind,
                    Token = token,
                    LeftExpr = expr,
                    RightExpr = ParseBinaryExpression(newOp)
                };
            }
            return expr;
        }

        public AstExpression ParseUnaryExpression()
        {
            AstExpression expr;
            switch (CurrentToken.Kind)
            {
                case TokenKind.TK_INC:
                case TokenKind.TK_DEC:
                case TokenKind.TK_BITAND:
                case TokenKind.TK_MUL:
                case TokenKind.TK_ADD:
                case TokenKind.TK_SUB:
                case TokenKind.TK_NOT:
                case TokenKind.TK_COMP:
                    TokenKind kind =CurrentToken.Kind;
                    NextToken();
                    expr = new AstUnaryOpExpression()
                    {
                        Operator = kind,
                        Token = Context.CurrentToken,
                        Expr = ParseUnaryExpression()
                    };
                    return expr;
                default:
                    return ParsePostfixExpression();
            }
        }

        public AstExpression ParsePostfixExpression()
        {
            AstExpression expr = ParsePrimaryExpression();

            // 检测变量后面的符号
            while (true)
            {
                switch (CurrentToken.Kind)
                {
                    case TokenKind.TK_LBRACKET:
                        NextToken();
                        expr = new AstArrayExpression
                        {
                            Expr = expr,
                            Token = Context.CurrentToken,
                            Index = ParseExpression()
                        };
                        Context.DoExpect(TokenKind.TK_RBRACKET);
                        break;
                    case TokenKind.TK_LPAREN:
                        NextToken();
                        IList<AstExpression> args = new List<AstExpression>();
                        args.Add(ParseAssignmentExpression());
                        while (CurrentToken.Kind == TokenKind.TK_COMMA)
                        {
                            NextToken();
                            args.Add(ParseAssignmentExpression());
                        }
                        expr = new AstFuncallExpression
                        {
                            Expr = expr,
                            Args = args
                        };
                        break;

                    default:
                        return expr;
                }
            }
                        
        }

        public AstExpression ParsePrimaryExpression()
        {
            AstExpression expr;
            switch (CurrentToken.Kind)
            {
                case TokenKind.TK_ID: // 标示符变量
                    expr = new AstViriableExpression(CurrentToken.Value)
                    {
                        Token = Context.CurrentToken
                    };
                    return expr;
                case TokenKind.TK_INTCONST: //int型常量
                    expr = new AstIntegerLiteralExpression
                    {
                        Kind = CurrentToken.Kind,
                        Token = Context.CurrentToken,
                        Value = int.Parse(CurrentToken.Value)
                    };
                    return expr;
                default:
                    return null;
            }
        }

        private bool IsBinaryOP(TokenKind kind)
        {
            return kind > TokenKind.TK_OR && kind < TokenKind.TK_MOD;
        }


        public List<AstStatement> ParseCompoundStatement()
        {
            List<AstStatement> stmts = new List<AstStatement>();
            NextToken();

            while (CurrentToken.Kind != TokenKind.TK_RBRACE && CurrentToken.Kind != TokenKind.TK_END)
            {
                if (ParserHelper.IsDeclaration(CurrentToken.Kind))
                {
                    AstExprStmt stmt = new AstExprStmt()
                    {
                        Token = CurrentToken,
                        Expr = codeParser.ParseExternalDeclaration()
                    };
                    stmts.Add(stmt);
                    NextToken();
                }

                if (!ParserHelper.IsDeclaration(CurrentToken.Kind))
                {
                    AstStatement stmt = ParseStatement();
                    if (stmt.Token != null)
                    {
                        stmts.Add(stmt);
                    }
                }
            }
            NextToken();

            return stmts;

        }

        public AstStatement ParseStatement()
        {
            AstStatement stmt = new AstStatement();
            switch (CurrentToken.Kind)
            {
                case TokenKind.TK_CASE:
                    stmt = ParseCaseStatement();
                    break;
                case TokenKind.TK_BREAK:
                    stmt = ParseBreakStatement();
                    break;
                case TokenKind.TK_DO:
                    stmt = ParseDoStatement();
                    break;
                case TokenKind.TK_DEFAULT:
                    stmt = ParseDefaultStatement();
                    break;
                case TokenKind.TK_IF:
                    stmt = ParseIfStatement();
                    break;
                case TokenKind.TK_FOR:
                    stmt = ParseForStatement();
                    break;
                case TokenKind.TK_WHILE:
                    stmt = ParseWhileStatement();
                    break;
                case TokenKind.TK_CONTINUE:
                    stmt = ParseContinueStatement();
                    break;
                case TokenKind.TK_RETURN:
                    stmt = ParseReturnStatement();
                    break;
                case TokenKind.TK_SWITCH:
                    stmt = ParseSwitchStatement();
                    break;
                //case TokenKind.TK_LBRACE:

                //    break;
                default:
                    return stmt;
            }
            return stmt;
        }

        public AstStatement ParseCaseStatement()
        {
            AstCaseStmt stmt = new AstCaseStmt()
            {
                Token = CurrentToken
            };

            NextToken();
            stmt.LabelExpr = ParseExpression();
            stmt.Body.AddRange(ParseCompoundStatement());

            return stmt;
        }

        public AstStatement ParseBreakStatement()
        {
            AstBreakStmt stmt = new AstBreakStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_BREAK);
            Context.DoExpect(TokenKind.TK_SEMICOLON);
            return stmt;
        }

        public AstStatement ParseContinueStatement()
        {
            AstContinueStmt stmt = new AstContinueStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_CONTINUE);
            Context.DoExpect(TokenKind.TK_SEMICOLON);
            return stmt;
        }

        public AstStatement ParseDoStatement()
        {
            AstDoStmt stmt = new AstDoStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_DO);
            Context.DoExpect(TokenKind.TK_LPAREN);
            stmt.Expr = ParseExpression();
            Context.DoExpect(TokenKind.TK_RPAREN);
            stmt.Stmts = ParseCompoundStatement();
            return stmt;
        }


        public AstStatement ParseIfStatement()
        {
            AstIfStmt stmt = new AstIfStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_LPAREN);
            stmt.Expr = ParseExpression();
            Context.DoExpect(TokenKind.TK_RPAREN);
            stmt.ThenStmt = ParseCompoundStatement();
            if (CurrentToken.Kind == TokenKind.TK_ELSE)
            {
                Context.DoExpect(TokenKind.TK_ELSE);
                stmt.ElseStmt = ParseCompoundStatement();
            }
            return stmt;
        }

        public AstStatement ParseForStatement()
        {
            AstForStmt stmt = new AstForStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_LPAREN);
            stmt.Init = ParseExpression();
            Context.DoExpect(TokenKind.TK_SEMICOLON);
            stmt.Cond = ParseExpression();
            Context.DoExpect(TokenKind.TK_SEMICOLON);
            stmt.Incr = ParseExpression();
            Context.DoExpect(TokenKind.TK_RPAREN);
            stmt.Stmts = ParseCompoundStatement();
            return stmt;
        }

        public AstStatement ParseWhileStatement()
        {
            AstWhileStmt stmt = new AstWhileStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_LPAREN);
            stmt.Expr = ParseExpression();
            Context.DoExpect(TokenKind.TK_RPAREN);
            stmt.Stmts = ParseCompoundStatement();
            return stmt;
        }

        public AstStatement ParseReturnStatement()
        {
            AstReturnStmt stmt = new AstReturnStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            stmt.Expr = ParseExpression();
            Context.DoExpect(TokenKind.TK_SEMICOLON);
            return stmt;
        }

        public AstStatement ParseSwitchStatement()
        {
            AstSwitchStmt stmt = new AstSwitchStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_LPAREN);
            stmt.Expr = ParseExpression();
            Context.DoExpect(TokenKind.TK_RPAREN);
            stmt.Stmts = ParseCompoundStatement();
            return stmt;
        }

        public AstStatement ParseDefaultStatement()
        {
            AstDefaultStmt stmt = new AstDefaultStmt()
            {
                Token = CurrentToken
            };
            NextToken();
            Context.DoExpect(TokenKind.TK_COLON);
            stmt.Body = ParseCompoundStatement();
            return stmt;
        }

    }
}
