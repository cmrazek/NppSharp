using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	internal class CodeParser
	{
		private string _code;
		private int _pos;
		private int _len;
		private StringBuilder _sb = new StringBuilder();

		public CodeParser(string code)
		{
			_code = code;
			_pos = 0;
			_len = _code.Length;
		}

		public string ReadLine()
		{
			_sb.Remove(0, _sb.Length);

			char ch;
			while (_pos < _len)
			{
				ch = _code[_pos++];
				if (ch == '\n') break;
				if (ch != '\r') _sb.Append(ch);
			}

			return _sb.ToString();
		}

		public bool EndOfFile
		{
			get { return _pos >= _len; }
		}

	}
}
