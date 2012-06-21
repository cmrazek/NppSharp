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

namespace NppSharp
{
#define FAST_BUF_LEN	256

	template<class T>
	class StringBuf
	{
	public:
		StringBuf()
			: _buf(NULL)
			, _cap(FAST_BUF_LEN)
			, _len(0)
		{
			_fast[0] = 0;
			_ptr = _fast;
		}

		StringBuf(int size)
			: _buf(NULL)
			, _cap(FAST_BUF_LEN)
			, _len(0)
		{
			_fast[0] = 0;
			_ptr = _fast;
			Init(size);
		}

		~StringBuf()
		{
			if (_buf) delete [] _buf;
		}

		void Init(int len)
		{
			Grow(len, true, false);
			memset(_ptr, 0, sizeof(T) * _cap);
			_len = len;
		}

		void Release()
		{
			_fast[0] = 0;
			_len = 0;
			_cap = FAST_BUF_LEN;
			_ptr = _fast;

			if (_buf)
			{
				delete [] _buf;
				_buf = null;
			}
		}

		void Append(const T* str)
		{
			int len = 0;
			const T* pos = str;
			while (*pos++) len++;
			if (len == 0) return;

			Grow(_len + len, false, true);
			pos = str;
			while (*pos) _ptr[_len++] = *pos++;
			_ptr[_len] = 0;
		}

		int			BufSize() const { return _cap; }
		void		Clear() { _len = 0; }
		const T*	Ptr() const { return _ptr; }
		T*			Ptr() { return _ptr; }
		int			Length() const { return _len; }

		T&			operator [] (int index) { return *(_ptr + index); }
		const T&	operator [] (int index) const { return *(_ptr + index); }

	private:
		void Grow(int size, bool exact, bool preserve)
		{
			// Always includes an extra char for the null terminator.

			if (size >= _cap)
			{
				int newSize = exact ? (size + 1) : (size + 1 + size / 2);
				T*	newBuf = new T[newSize];

				if (preserve && _len > 0) memcpy(newBuf, _ptr, sizeof(T) * _len + 1);
				else _len = 0;

				if (_buf) delete [] _buf;
				_buf = newBuf;
				_ptr = newBuf;
				_cap = newSize;
			}
		}

		T*	_ptr;
		T	_fast[FAST_BUF_LEN];
		T*	_buf;
		int	_cap;
		int	_len;
	};

	typedef StringBuf<char>		StringBufA;
	typedef StringBuf<wchar_t>	StringBufW;
}
