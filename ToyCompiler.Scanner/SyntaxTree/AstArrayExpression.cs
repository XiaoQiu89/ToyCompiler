using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    class AstArrayExpression : AstExpression
    {
        public AstExpression Expr { get; set; }
        public AstExpression Index { get; set; }
    }
}
