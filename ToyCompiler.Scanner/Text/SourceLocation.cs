using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 源代码文件位置信息
    /// </summary>
    public struct SourceLocation
    {
        private int _sourceLine;
        private int _sourceIndex;
        private string _path;

        /// <summary>
        /// 源代码中的位置信息
        /// </summary>
        /// <param name="line">行号</param>
        /// <param name="index">列数</param>
        public SourceLocation(int line, int index,string path)
        {
            _sourceLine = line;
            _sourceIndex = index;
            _path = path;
        }

        public int SourceLine
        {
            get { return _sourceLine; }
            set { _sourceLine = value; }
        }

        public int SourceIndex
        {
            get { return _sourceIndex; }
            set { _sourceIndex = value; }
        }

        public void UpdateLocation(int currentChar, Func<char> nextChar)
        {
            if (currentChar == '\n' || currentChar == '\r' && nextChar() == '\n')
            {
                _sourceLine++;
                _sourceIndex = 0;
            }
            else
            {
                _sourceIndex++;
            }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public override string ToString()
        {
            return "(" + Path + " " + SourceLine + "行)"; 
        }
    }
}
