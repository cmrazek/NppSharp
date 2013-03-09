#include "StdAfx.h"
#include "LexerLine.h"
#include "ClrUtil.h"

namespace NppSharp
{
	LexerLine::LexerLine()
		: _sb(gcnew StringBuilder())
		, _foldStarts(0)
		, _foldEnds(0)
		, _lineStart(NULL)
		, _lineEnd(NULL)
		, _codePage(0)
		, _lineText(nullptr)
		, _styles(NULL)
		, _stylesLen(0)
		, _charLen(-1)
	{
	}

	LexerLine::~LexerLine()
	{
		if (_styles) delete [] _styles;
	}

	String^ LexerLine::Text::get()
	{
		if (_lineText == nullptr)
		{
			if (_codePage == SC_CP_UTF8)
			{
				StringBuilder^	sb = gcnew StringBuilder();
				const char*		pos = _lineStart;
				while (pos < _lineEnd)
				{
					sb->Append(ReadUtf8Char(&pos));
				}
				_lineText = sb->ToString();
			}
			else
			{
				_lineText = gcnew String(_lineStart, 0, _lineEnd - _lineStart);
			}
		}
		return _lineText;
	}

	void LexerLine::Start(const char* lineStart, const char* lineEnd, int codePage)
	{
		_lineStart = lineStart;
		_lineEnd = lineEnd;
		_codePage = codePage;
		_rawPos = _lineStart;
		_charLen = -1;

		_foldStarts = _foldEnds = 0;

		int len = lineEnd - lineStart;
		if (_stylesLen < len || _styles == NULL)
		{
			if (_styles) delete [] _styles;
			_styles = new byte[len];
			_stylesLen = len;
		}
		memset(_styles, 0, len);
	}

	int LexerLine::Length::get()
	{
		if (_charLen == -1)
		{
			_charLen = 0;

			if (_codePage == SC_CP_UTF8)
			{
				const char*	pos = _lineStart;
				while (pos < _lineEnd)
				{
					pos += Utf8CharWidth(pos);
					_charLen++;
				}
			}
			else
			{
				_charLen = _lineStart - _lineEnd;
			}
		}
		return _charLen;
	}

	int LexerLine::Position::get()
	{
		if (_codePage == SC_CP_UTF8)
		{
			int			charPos = 0;
			const char*	pos = _lineStart;

			while (pos < _rawPos)
			{
				pos += Utf8CharWidth(pos);
				charPos++;
			}

			return charPos;
		}
		else
		{
			return _rawPos - _lineStart;
		}
	}

	void LexerLine::Position::set(int pos)
	{
		if (pos < 0)
		{
			_rawPos = _lineStart;
		}
		else if (_codePage == SC_CP_UTF8)
		{
			_rawPos = _lineStart;
			for (int i = 0; i < pos && _rawPos < _lineEnd; ++i) _rawPos += Utf8CharWidth(_rawPos);
		}
		else
		{
			_rawPos = _lineStart + pos;
			if (_rawPos > _lineEnd) _rawPos = _lineEnd;
		}
	}

	bool LexerLine::EOL::get()
	{
		return _rawPos >= _lineEnd;
	}

	void LexerLine::Style(LexerStyle^ style)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		if (_rawPos < _lineEnd)
		{
			if (_codePage == SC_CP_UTF8)
			{
				const char* oldPos = _rawPos;
				_rawPos += Utf8CharWidth(_rawPos);
				while (oldPos < _rawPos) _styles[oldPos++ - _lineStart] = styleNum;
			}
			else
			{
				_styles[_rawPos - _lineStart] = styleNum;
				_rawPos++;
			}
		}
	}

	void LexerLine::Style(LexerStyle^ style, int length)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		if (_codePage == SC_CP_UTF8)
		{
			const char* oldPos = _rawPos;
			for (int i = 0; i < length && _rawPos < _lineEnd; ++i) _rawPos += Utf8CharWidth(_rawPos);
			while (oldPos < _rawPos) _styles[oldPos++ - _lineStart] = styleNum;
		}
		else
		{
			for (int i = 0; i < length && _rawPos < _lineEnd; ++i)
			{
				_styles[_rawPos - _lineStart] = styleNum;
				_rawPos++;
			}
		}
	}

	void LexerLine::StyleRemainder(LexerStyle^ style)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		if (_rawPos < _lineEnd)
		{
			memset(_styles + (_rawPos - _lineStart), styleNum, _lineEnd - _rawPos);
			_rawPos = _lineEnd;
		}
	}

	void LexerLine::StyleRange(LexerStyle^ style, int startPos, int length)
	{
		byte styleNum = style != nullptr ? style->Index : 0;

		if (startPos < 0)
		{
			length += startPos;
			startPos = 0;
		}

		if (_codePage == SC_CP_UTF8)
		{
			const char*	pos = _lineStart;
			int			charPos = 0;
			while (charPos < startPos && pos < _lineEnd)
			{
				pos += Utf8CharWidth(pos);
				charPos++;
			}

			for (int i = 0; i < length && pos < _lineEnd; ++i)
			{
				pos += Utf8CharWidth(pos);
				_styles[pos - _lineStart] = styleNum;
			}
		}
		else
		{
			if (startPos + length > _lineEnd - _lineStart) length = _lineEnd - _lineStart;
			if (length > 0) memset(_styles + startPos, styleNum, length);
		}
	}

	String^ LexerLine::Peek(int length)
	{
		if (_rawPos >= _lineEnd) return String::Empty;

		if (_codePage == SC_CP_UTF8)
		{
			#ifdef DOTNET4
			_sb->Clear();
			#else
			_sb->Remove(0, _sb->Length);
			#endif

			const char* pos = _rawPos;
			while (length-- && pos < _lineEnd) _sb->Append((wchar_t)*(pos++));
			return _sb->ToString();
		}
		else
		{
			if (_rawPos + length > _lineEnd) length = _lineEnd - _rawPos;
			return gcnew String(_rawPos, 0, length);
		}
	}

	wchar_t LexerLine::NextChar::get()
	{
		if (_rawPos >= _lineEnd) return L'\0';

		if (_codePage == SC_CP_UTF8)
		{
			const char* pos = _rawPos;
			return ReadUtf8Char(&pos);
		}
		else
		{
			return (wchar_t)*_rawPos;
		}
	}

	String^ LexerLine::Peek(LexerReadDelegate^ readFunc)
	{
		if (readFunc == nullptr) throw gcnew ArgumentNullException("readFunc");

		#ifdef DOTNET4
		_sb->Clear();
		#else
		_sb->Remove(0, _sb->Length);
		#endif

		if (_codePage == SC_CP_UTF8)
		{
			const char*	pos = _rawPos;
			wchar_t		ch;
			while (pos < _lineEnd)
			{
				ch = ReadUtf8Char(&pos);
				if (readFunc(ch)) _sb->Append(ch);
				else break;
			}
		}
		else
		{
			const char*	pos = _rawPos;
			wchar_t		ch;
			while (pos < _lineEnd)
			{
				ch = (wchar_t)*(pos++);
				if (readFunc(ch)) _sb->Append(ch);
				else break;
			}
		}

		return _sb->ToString();
	}

	wchar_t LexerLine::PeekChar(int offset)
	{
		if (_codePage == SC_CP_UTF8)
		{
			const char* pos = _rawPos;
			if (offset > 0)
			{
			}
			else if (offset < 0)
			{
			}
			else // offset == 0
			{
				const char* pos = _rawPos;
			}

			if (pos < _lineStart || pos >= _lineEnd) return L'\0';
			return ReadUtf8Char(&pos);
		}
		else
		{
			const char*	pos = _rawPos + offset;
			if (pos < _lineStart || pos >= _lineEnd) return L'\0';
			return (wchar_t)*pos;
		}
	}

	void LexerLine::Style(LexerStyle^ style, LexerReadDelegate^ readFunc)
	{
		if (readFunc == nullptr) throw gcnew ArgumentNullException("readFunc");

		byte styleNum = style != nullptr ? style->Index : 0;

		if (_codePage == SC_CP_UTF8)
		{
			const char*	pos = _rawPos;
			wchar_t		ch;
			while (_rawPos < _lineEnd)
			{
				ch = ReadUtf8Char(&pos);
				if (readFunc(ch))
				{
					while (_rawPos < pos) _styles[_rawPos++ - _lineStart] = styleNum;
				}
				else break;
			}
		}
		else
		{
			while (_rawPos < _lineEnd)
			{
				if (readFunc((wchar_t)*_rawPos))
				{
					_styles[_rawPos - _lineStart] = styleNum;
					_rawPos++;
				}
				else break;
			}
		}
	}

	void LexerLine::FoldStart()
	{
		_foldStarts++;
	}

	void LexerLine::FoldEnd()
	{
		if (_foldStarts > 0)
		{
			// Ending a fold that was started on the same line.
			// Don't create a fold point here.
			_foldStarts--;
		}
		else
		{
			_foldEnds++;
		}
	}

	bool LexerLine::Match(String^ str)
	{
		if (_codePage == SC_CP_UTF8)
		{
			const char* pos = _rawPos;
			for each (wchar_t ch in str)
			{
				if (pos >= _lineEnd || ReadUtf8Char(&pos) != ch) return false;
			}
		}
		else
		{
			const char*	pos = _rawPos;
			for each (wchar_t ch in str)
			{
				if (pos >= _lineEnd || (wchar_t)*pos != ch) return false;
				pos++;
			}
		}

		return true;
	}

	bool LexerLine::Match(String^ str, bool ignoreCase)
	{
		if (_codePage == SC_CP_UTF8)
		{
			const char* pos = _rawPos;
			for each (wchar_t ch in str)
			{
				if (pos >= _lineEnd || Char::ToLower(ReadUtf8Char(&pos)) != Char::ToLower(ch)) return false;
			}
		}
		else
		{
			const char*	pos = _rawPos;
			for each (wchar_t ch in str)
			{
				if (pos >= _lineEnd || Char::ToLower((wchar_t)*pos) != Char::ToLower(ch)) return false;
				pos++;
			}
		}

		return true;
	}

	bool LexerLine::IsBlank::get()
	{
		if (_codePage == SC_CP_UTF8)
		{
			const char*	pos = _lineStart;
			while (pos < _lineEnd)
			{
				if (!Char::IsWhiteSpace(ReadUtf8Char(&pos))) return false;
			}
		}
		else
		{
			const char* pos = _lineStart;
			while (pos < _lineEnd)
			{
				if (!Char::IsWhiteSpace((wchar_t)*(pos++))) return false;
			}
		}

		return true;
	}
}
