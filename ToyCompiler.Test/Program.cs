using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ToyCompiler.Scanner;

namespace ToyCompiler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ParserEngine.Parse(args[0]);
        }
    }
}
