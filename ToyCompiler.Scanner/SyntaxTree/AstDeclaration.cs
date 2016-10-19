using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstDeclaration : AstNode
    {
        public AstSpecifiers Specifiers { get; set; }
        public IList<AstInitDeclarator> Init { get; set; }

        public AstDeclaration()
        {
            Init = new List<AstInitDeclarator>();
        }

        public override void Dump(int n)
        {
            foreach (var init in Init)
            {
                print(n);
                Console.WriteLine("变量声明");
                init.Declarator.Dump(n+1);
                if (init.Initializer != null)
                {
                    print(++n);
                    Console.WriteLine("声明赋值语句");
                    init.Initializer.Initializer.Dump(n+1);
                }
            }
        }
    }
}
