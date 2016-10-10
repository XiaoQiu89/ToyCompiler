using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstTranslationUnit
    {
        public IList<AstDeclaration> ExtDecls { get; set; }

        public AstTranslationUnit()
        {
            ExtDecls = new List<AstDeclaration>();
        }
    }
}
