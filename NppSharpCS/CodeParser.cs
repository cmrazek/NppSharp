// NppSharp - C#/.NET Scripting Plugin for Notepad++
// Copyright (C) 2012  Chris Mrazek
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
