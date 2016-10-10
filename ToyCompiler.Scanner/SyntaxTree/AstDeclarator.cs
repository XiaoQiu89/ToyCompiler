using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstDeclarator : AstNode
    {
        //public AstDeclarator Declarator { get; set; }
    }

    public class AstPointerDeclarator : AstDeclarator
    {
        public IList<AstSpecifier> TypeQualifiers { get; set; }
    }

    public class AstArrayDeclarator : AstDeclarator
    {
        public AstDeclarator Declarator { get; set; }
        public AstExpression Expr { get; set; }
    }

    public class AstFunctionDeclarator : AstDeclarator
    {
        public AstDeclarator Declarator { get; set; }
        public AstParameterTypeList ParamList { get; set; }
    }
    
    public class AstNameDeclarator:AstDeclarator
    {
        public string Name{ get; set; }
    }
}
