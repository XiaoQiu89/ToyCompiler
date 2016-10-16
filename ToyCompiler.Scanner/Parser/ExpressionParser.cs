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

        public ExpressionParser(ParserContext context)
        {
            this.Context = context;
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

        public AstInitializer ParseAssignmentExpression()
        {
            AstInitializer init = new AstInitializer()
            {
                Initializer = ParseConditionExpression()
            };

            return init;
        }

        public AstExpression ParseConditionExpression()
        {
            AstExpression expr = ParseBinaryExpression(Prec[TokenKind.TK_OR]);
            // 检测三目运算符
            if (CurrentToken.Kind == TokenKind.TK_QUESTION)
            {
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
                TokenKind kind = CurrentToken.Kind;
                NextToken();
                expr = new AstBinaryOpExpression()
                {
                    Operator = kind,
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

            return expr;
            
        }

        public AstExpression ParsePrimaryExpression()
        {
            AstExpression expr;
            switch (CurrentToken.Kind)
            {
                case TokenKind.TK_ID:
                    expr = new AstViriableExpression(CurrentToken.Value);
                    return expr;
                case TokenKind.TK_INTCONST:
                    expr = new AstIntegerLiteralExpression
                    {
                        Kind = CurrentToken.Kind,
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
    }
}
