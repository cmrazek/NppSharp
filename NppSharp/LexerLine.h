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

#pragma once

using namespace System::Text;

namespace NppSharp
{
	ref class LexerLine : public ILexerLine
	{
	public:
		LexerLine();

		property bool		EOL { virtual bool get(); }
		property int		Length { virtual int get(); }
		property wchar_t	NextChar { virtual wchar_t get(); }
		property int		Position { virtual int get(); virtual void set(int); }
		property String^	Text { virtual String^ get(); }

		virtual String^	Peek(int length);
		virtual String^	Peek(LexerReadDelegate^ readFunc);
		virtual void	Style(LexerStyle^ style);
		virtual void	Style(LexerStyle^ style, int length);
		virtual void	Style(LexerStyle^ style, LexerReadDelegate^ readFunc);
		virtual void	StyleRemainder(LexerStyle^ style);
		virtual void	StyleRange(LexerStyle^ style, int startPos, int length);

		virtual void	FoldStart();
		virtual void	FoldEnd();
		int				GetFoldLevel() { return _foldLevel; }

		void Start(String^ text);
		property array<byte>^ StylesBuf { array<byte>^ get(); }

	private:
		String^			_text;
		array<byte>^	_styles;
		int				_len;
		int				_pos;
		StringBuilder^	_sb;
		int				_foldLevel;
	};
}
