using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 条件表达式
    /// </summary>
    public class AstCondExpression : AstExpression
    {
        public AstExpression Cond { get; set; }
        public AstExpression Then { get; set; }
        public AstExpression Else { get; set; }

        public AstCondExpression(AstExpression cond, AstExpression t, AstExpression e)
        {
            this.Cond = cond;
            this.Then = t;
            this.Else = e;
        }

        public AstCondExpression()
            : this(null, null, null) { }

        public override void Dump(int n)
        {
            print(n);
            Console.WriteLine("三元表达式");
            print(++n);
            Console.WriteLine("初始化表达式：");
            Cond.Dump(n + 1);
            print(n);
            Console.WriteLine("then表达式：");
            Then.Dump(n + 1); 
            print(n);
            Console.WriteLine("else表达式：");
            Else.Dump(n + 1);
        }
    }
}
