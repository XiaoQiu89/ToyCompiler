using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstIntegerLiteralExpression : AstLiteralExpression
    {
        public long Value { get; set; }
    }
}
