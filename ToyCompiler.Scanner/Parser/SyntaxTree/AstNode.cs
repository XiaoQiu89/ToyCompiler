using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public abstract class AstNode
    {
        public SourceLocation StartLocation { get; set; }

        public string Name { get; set; }
    }
}
