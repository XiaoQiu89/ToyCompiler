using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ToyCompiler.Scanner
{
    [DebuggerDisplay("Kind({Kind}),Value({Value})")]
    public class Token
    {
        public TokenKind Kind { get; set; }

        public string Value { get; set; }

        public SourceLocation Location { get; set; }
    }
}
