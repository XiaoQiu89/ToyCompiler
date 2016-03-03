using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ToyCompiler.Scanner
{
    /// <summary>
    /// 源代码文件位置信息
    /// </summary>
    public class SourceLocation
    {
        private int _sourceLine;
        private int _sourceIndex;

        /// <summary>
        /// 源代码中的位置信息
        /// </summary>
        /// <param name="line">行号</param>
        /// <param name="index">列数</param>
        public SourceLocation(int line, int index)
        {
            _sourceLine = line;
            _sourceIndex = index;
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
    }
}
