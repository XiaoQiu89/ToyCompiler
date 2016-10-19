using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public class AstNode 
    {
        public NodeKind Kind { get; set; }
        public Token Token { get; set; }

        string space = "    ";

        public virtual void Dump(int n) { }

        public void print(int n)
        {
            while (n > 0)
            {
                Console.Write(space);
                n--;
            }
        }
    }
}
