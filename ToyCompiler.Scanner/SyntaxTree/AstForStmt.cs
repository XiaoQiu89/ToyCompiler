using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstForStmt : AstStatement
    {
        public AstExpression Init { get; set; }
        public AstExpression Cond { get; set; }
        public AstExpression Incr { get; set; }
        public List<AstStatement> Stmts { get; set; }

        public AstForStmt()
        {
            Stmts = new List<AstStatement>();
        }
    }
}
