using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstCaseStmt : AstStatement
    {
        public AstExpression LabelExpr { get; set; }

        public List<AstStatement> Body { get; set; }

        public AstCaseStmt()
        {
            Body = new List<AstStatement>();
        }
    }
}
