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

#include "StdAfx.h"
#include "LexerLine.h"

namespace NppSharp
{
	LexerLine::LexerLine()
		: _text(nullptr)
		, _styles(nullptr)
		, _len(0)
		, _pos(0)
	{
	}

	String^ LexerLine::Text::get()
	{
		return _text;
	}

	void LexerLine::Start(String^ str)
	{
		_text = str;
		_len = str->Length;
		_pos = 0;

		if (_styles == nullptr || _styles->Length < _len) _styles = gcnew array<byte>(_len);
		for (int i = 0; i < _len; ++i) _styles[i] = 0;
	}

	array<byte>^ LexerLine::StylesBuf::get()
	{
		return _styles;
	}


	int LexerLine::Length::get()
	{
		return _text->Length;
	}

	int LexerLine::Position::get()
	{
		return _pos;
	}

	void LexerLine::Position::set(int pos)
	{
		if (pos < 0) _pos = 0;
		else if (pos > _len) _pos = _len;
		else _pos = pos;
	}

	bool LexerLine::EOL::get()
	{
		return _pos >= _len;
	}

	void LexerLine::Style(LexerStyle^ style)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		if (_pos < _len) _styles[_pos++] = styleNum;
	}

	void LexerLine::Style(LexerStyle^ style, int length)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		for (int i = 0; i < length; ++i)
		{
			if (_pos >= _len) return;
			_styles[_pos++] = styleNum;
		}
	}

	void LexerLine::StyleRemainder(LexerStyle^ style)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		while (_pos < _len) _styles[_pos++] = styleNum;
	}

	void LexerLine::StyleRange(LexerStyle^ style, int startPos, int length)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		if (startPos < 0)
		{
			length += startPos;
			startPos = 0;
		}
		else if (startPos > _len) startPos = _len;

		if (length < 0) length = 0;
		else if (startPos + length > _len) length = _len - startPos;

		for (int i = 0; i < length; ++i)
		{
			_styles[startPos + i] = styleNum;
		}
		_pos = startPos + length;
	}

	String^ LexerLine::Peek(int length)
	{
		if (_pos + length > _len) length = _len - _pos;
		return _text->Substring(_pos, length);
	}

	wchar_t LexerLine::NextChar::get()
	{
		if (_pos >= _len) return L'\0';
		return _text[_pos];
	}
}
