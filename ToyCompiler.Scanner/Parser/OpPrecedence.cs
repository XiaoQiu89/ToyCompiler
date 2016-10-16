using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class OpPrecedence
    {
        public TokenKind Kind { get; set; }
        public int Precedence { get; set; }
        public string Name { get; set; }

        public OpPrecedence(TokenKind kind, int prec, string name)
        {
            this.Kind = kind;
            this.Precedence = prec;
            this.Name = name;
        }

        public static bool operator >=(OpPrecedence op1, OpPrecedence op2)
        {
            return op1.Precedence >= op1.Precedence;
        }

        public static bool operator <=(OpPrecedence op1, OpPrecedence op2)
        {
            return op1.Precedence <= op1.Precedence;
        }
    }
}
