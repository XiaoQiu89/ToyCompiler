using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstParameterTypeList : AstNode
    {
        public AstParameterDeclaration ParamDecls { get; set; }
    }

    public class AstParameterDeclaration:AstNode
    {
        public AstSpecifiers Specifiers { get; set; }
        public AstDeclarator Declarator { get; set; }
    }
}
