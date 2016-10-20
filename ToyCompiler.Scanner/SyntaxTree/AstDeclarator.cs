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

        public override void Dump(int n)
        {
            Declarator.Dump(n+1);
            Expr.Dump(n+1);
        }
    }

    public class AstFunctionDeclarator : AstDeclarator
    {
        public AstDeclarator Declarator { get; set; }
        public AstParameterTypeList ParamList { get; set; }
        public bool IsFunCall { get; set; }
        public IList<AstExpression> ParamExps { get; set; }

        public override void Dump(int n)
        {
            Declarator.Dump(n+1);
            ParamList.Dump(n+1);
        }
    }
    
    public class AstNameDeclarator:AstDeclarator
    {
        public string Name{ get; set; }

        public override void Dump(int n)
        {
            print(n);
            Console.WriteLine("声明名称为:{0},位置:{1}",Name,Token.Location);
        }
    }
}
