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
        public string Operator { get; set; }
        public AstExpression Expr { get; set; }

        public AstUnaryOpExpression(string op, AstExpression expr)
        {
            this.Operator = op;
            this.Expr = expr;
        }
    }
}
