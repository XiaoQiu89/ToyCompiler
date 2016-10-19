using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstIntegerLiteralExpression : AstLiteralExpression
    {
        public long Value { get; set; }

        public override void Dump(int n)
        {
            print(n);
            Console.WriteLine("整型常数为:{0}，位置为{1}",Value,Token.Location);
        }
    }
}
