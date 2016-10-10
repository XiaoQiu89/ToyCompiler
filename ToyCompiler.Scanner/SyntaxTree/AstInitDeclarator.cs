using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstInitDeclarator : AstNode
    {
        public AstDeclarator Declarator { get; set; }
        public AstInitializer Initializer { get; set; }
        public AstInitDeclarator Next { get; set; }
    }
}
