using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public class AstInitDeclarator : AstNode
    {
        public AstDeclarator Declarator { get; set; }
        public AstInitializer Initializer { get; set; }

        public override void Dump(int n)
        {
            print(n);
            Console.WriteLine("变量声明的变量名称：");
            Declarator.Dump(n+1);
            if (Initializer != null)
            {
                print(++n);
                Console.WriteLine("初始化语句为：");
                Initializer.Initializer.Dump(n + 1);
            }
        }
    }
}
