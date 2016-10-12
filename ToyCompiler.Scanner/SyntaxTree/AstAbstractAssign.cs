using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 赋值表达式
    /// </summary>
    public class AstAbstractAssign : AstExpression
    {
        public AstExpression LE { get; set; }
        public AstExpression RE { get; set; }

        public AstAbstractAssign(AstExpression le, AstExpression re)
        {
            this.LE = le;
            this.RE = re;
        }
    }
}
