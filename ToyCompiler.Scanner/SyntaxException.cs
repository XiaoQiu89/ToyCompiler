using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class SyntaxException : Exception
    {
        public TokenKind Target { get; set; }
        public TokenKind Source { get; set; }

        public SyntaxException(TokenKind s, TokenKind t)
        {
            this.Target = t;
            this.Source = s;
        }
    }
}
