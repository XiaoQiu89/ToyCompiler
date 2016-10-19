using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstViriableExpression : AstExpression
    {
        public string Name { get; set; }

        public AstViriableExpression(string name)
        {
            this.Name = name;
        }

        public override void Dump(int n)
        {
            print(n);
            Console.WriteLine("变量为:{0},位置为：{1}",Name,Token.Location);
            
        }
    }
}
