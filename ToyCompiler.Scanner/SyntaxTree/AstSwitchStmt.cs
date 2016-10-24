using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstSwitchStmt : AstStatement 
    {
        public AstExpression Expr { get; set; }
        public IList<AstStatement> Stmts { get; set; }
    }
}
