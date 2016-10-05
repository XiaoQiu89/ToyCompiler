using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public abstract class LookaheadTextReader : TextReader
    {
        public abstract SourceLocation CurrentLocation { get; }
    }
}
