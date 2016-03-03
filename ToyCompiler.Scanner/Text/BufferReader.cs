using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToyCompiler.Scanner
{
    public class BufferReader : TextReader
    {
        private SourceLocation location;
        private int _currentCharacter;
        private TextReader InnerReader;

        public BufferReader(TextReader reader)
        {
            this.InnerReader = reader;
            location = new SourceLocation(1, 0);

            UpdateCurrentCharacter();
        }

        public int CurrentCharacter
        {
            get
            {
                return _currentCharacter;
            }
        }

        public override int Peek()
        {
            return CurrentCharacter;
        }

        public override int Read()
        {
            int ch = CurrentCharacter;
            NextCharacter();
            return ch;
        }

        private void NextCharacter()
        {
            int prevChar = CurrentCharacter;
            // 检测是否到达文件末尾
            if (prevChar == -1)
                return;

            // 更新当前字符
            _currentCharacter = InnerReader.Read();
            // 更新读取文件的当期位置
            location.UpdateLocation(prevChar, () => (char)_currentCharacter);
        }

        private void UpdateCurrentCharacter()
        {
            _currentCharacter = InnerReader.Read();
        }
    }
}
