using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyCompiler.Scanner
{
    public class AstInitializer
    {
        public AstExpression Initializer { get; set; }

        public AstInitializer(AstExpression init)
        {
            this.Initializer = init;
        }

        public AstInitializer() { }
    }
}
