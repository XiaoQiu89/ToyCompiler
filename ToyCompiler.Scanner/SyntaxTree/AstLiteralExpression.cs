using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstLiteralExpression : AstExpression
    {
        public TokenKind Kind { get; set; }
    }
}
