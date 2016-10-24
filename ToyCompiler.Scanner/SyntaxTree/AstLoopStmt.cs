using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstLoopStmt : AstStatement
    {
        public AstExpression Expr { get; set; }
        public List<AstStatement> Stmts { get; set; }

        public AstLoopStmt()
        {
            Stmts = new List<AstStatement>();
        }
    }
}
