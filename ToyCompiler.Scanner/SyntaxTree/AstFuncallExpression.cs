using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 函数调用表达式
    /// </summary>
    public class AstFuncallExpression : AstExpression
    {
        public AstExpression Expr { get; set; }
        public IList<AstExpression> Args { get; set; }

        public AstFuncallExpression(AstExpression expr, IList<AstExpression> args)
        {
            this.Expr = expr;
            this.Args = args;
        }
    }
}
