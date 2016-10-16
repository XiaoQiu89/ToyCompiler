using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstStringLiteralExpression : AstLiteralExpression
    {
        public string Value { get; set; }
    }
}
