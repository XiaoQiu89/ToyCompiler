using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstIfStmt : AstStatement
    {
        public AstExpression Expr { get; set; }
        public List<AstStatement> ThenStmt { get; set; }
        public List<AstStatement> ElseStmt { get; set; }

        public AstIfStmt()
        {
            ThenStmt = new List<AstStatement>();
            ElseStmt = new List<AstStatement>();
        }
    }
}
