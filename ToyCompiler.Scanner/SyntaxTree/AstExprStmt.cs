using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstExprStmt : AstStatement
    {
        public AstDeclaration Expr { get; set; }
    }
}
