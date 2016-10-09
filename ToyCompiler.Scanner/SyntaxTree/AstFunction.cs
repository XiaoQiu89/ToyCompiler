using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstFunction : AstNode
    {
        public AstSpecifiers Specifiers { get; set; }
        public AstFunctionDeclarator Declarator { get; set; }
        // 语句体暂时没写
    }
}
