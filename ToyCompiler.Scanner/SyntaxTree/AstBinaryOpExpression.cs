using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 二元表达式
    /// </summary>
    public class AstBinaryOpExpression : AstExpression
    {
        public string Operator { get; set; }
        public AstExpression LeftExpr { get; set; }
        public AstExpression RightExpr { get; set; }

        public AstBinaryOpExpression(string op, AstExpression le, AstExpression re)
        {
            this.Operator = op;
            this.LeftExpr = le;
            this.RightExpr = re;
        }
    }
}
