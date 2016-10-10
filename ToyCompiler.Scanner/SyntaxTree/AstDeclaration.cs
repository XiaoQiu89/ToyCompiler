using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstDeclaration : AstNode
    {
        public AstSpecifiers Specifiers { get; set; }
        public IList<AstInitDeclarator> Init { get; set; }

        public AstDeclaration()
        {
            Init = new List<AstInitDeclarator>();
        }
    }
}
