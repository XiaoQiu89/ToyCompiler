using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 类型转换表达式
    /// </summary>
    public class AstCastExpression : AstExpression
    {
        public string CastType { get; set; }
        public AstExpression Expr { get; set; }

        public AstCastExpression(string type, AstExpression expr)
        {
            this.CastType = type;
            this.Expr = expr;
        }
    }
}
