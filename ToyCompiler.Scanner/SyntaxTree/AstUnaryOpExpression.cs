using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 一元表达式
    /// </summary>
    public class AstUnaryOpExpression : AstExpression
    {
        public TokenKind Operator { get; set; }
        public AstExpression Expr { get; set; }

        public AstUnaryOpExpression(TokenKind op, AstExpression expr)
        {
            this.Operator = op;
            this.Expr = expr;
        }

        public AstUnaryOpExpression(TokenKind op)
            : this(op, null)
        {
        }

        public AstUnaryOpExpression()
            : this(TokenKind.TK_END, null) { }

        public override void Dump(int n)
        {
            Console.WriteLine("一元表达式操作符为:{0},位置:{1}",Operator.ToString(),Expr.Token.Location);
            Console.Write("表达式为:");
            Expr.Dump(n+1);
        }
    }
}
