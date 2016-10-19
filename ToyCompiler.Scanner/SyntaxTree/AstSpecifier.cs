using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstSpecifier : AstNode
    {
        public override void Dump(int n)
        {
            
        }
    }

    public class AstSpecifiers
    {
        public AstSpecifier StorageSpecifier { get; set; }
        public AstSpecifier TypeQualifier { get; set; }
        public AstSpecifier TypeSpecifier { get; set; }
    }
}
