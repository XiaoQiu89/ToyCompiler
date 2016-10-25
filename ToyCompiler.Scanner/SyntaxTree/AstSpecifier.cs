using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstSpecifier : AstNode
    {
        public override void Dump(int n)
        {
            print(n);
            Console.Write("类型修饰为:");
            Console.WriteLine(this.Token.Value);
        }
    }

    public class AstSpecifiers : AstNode
    {
        public AstSpecifier StorageSpecifier { get; set; }
        public AstSpecifier TypeQualifier { get; set; }
        public AstSpecifier TypeSpecifier { get; set; }

        public override void Dump(int n)
        {
            if (StorageSpecifier != null)
                StorageSpecifier.Dump(n);

            if (TypeQualifier != null)
                TypeQualifier.Dump(n);

            if (TypeSpecifier != null)
                TypeSpecifier.Dump(n);
        }
    }
}
