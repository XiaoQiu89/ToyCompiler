using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstParameterTypeList : AstNode
    {
        public IList<AstParameterDeclaration> ParamDecls { get; set; }

        public AstParameterTypeList()
        {
            ParamDecls = new List<AstParameterDeclaration>();
        }

        public void Add(AstParameterDeclaration decl)
        {
            ParamDecls.Add(decl);
        }

        public override void Dump(int n)
        {
            print(n);
            Console.WriteLine("函数的参数列表为：");
            foreach (var param in ParamDecls)
            {
                param.Dump(n+1);
            }
        }
    }

    public class AstParameterDeclaration:AstNode
    {
        public AstSpecifiers Specifiers { get; set; }
        public AstDeclarator Declarator { get; set; }

        public override void Dump(int n)
        {
            Specifiers.Dump(n + 1);
            Declarator.Dump(n + 1);
        }
    }
}
