using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public class ParserEngine
    {
        public static void Parse(string path)
        {
            FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.None);
            TextReader tr = new StreamReader(fs);
            ParserContext parserContext = new ParserContext(new BufferReader(tr,path), path);
            CodeParser parser = new CodeParser(parserContext); 
            parser.ParseFile();
        }
    }
}
