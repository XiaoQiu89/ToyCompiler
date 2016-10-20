using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstParameterTypeList
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

        public void Dump(int n)
        {
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
    }
}
