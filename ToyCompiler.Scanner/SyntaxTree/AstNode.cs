using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public abstract class AstNode
    {
        public NodeKind Kind { get; set; }
        public Token Token { get; set; }
        public AstNode NextNode { get; set; }
    }
}
