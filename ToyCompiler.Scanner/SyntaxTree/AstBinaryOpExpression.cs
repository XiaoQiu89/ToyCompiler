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
        public TokenKind Operator { get; set; }
        public AstExpression LeftExpr { get; set; }
        public AstExpression RightExpr { get; set; }

        public AstBinaryOpExpression(TokenKind op, AstExpression le, AstExpression re)
        {
            this.Operator = op;
            this.LeftExpr = le;
            this.RightExpr = re;
        }

        public AstBinaryOpExpression()
            : this(TokenKind.TK_END, null, null) { }
    }
}
