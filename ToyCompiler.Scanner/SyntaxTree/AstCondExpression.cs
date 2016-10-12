using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 条件表达式
    /// </summary>
    public class AstCondExpression : AstExpression
    {
        public AstExpression Cond { get; set; }
        public AstExpression Then { get; set; }
        public AstExpression Else { get; set; }

        public AstCondExpression(AstExpression cond, AstExpression t, AstExpression e)
        {
            this.Cond = cond;
            this.Then = t;
            this.Else = e;
        }
    }
}
