using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstDefaultStmt : AstStatement
    {
        public List<AstStatement> Body { get; set; }

        public AstDefaultStmt()
        {
            Body = new List<AstStatement>();
        }
    }
}
