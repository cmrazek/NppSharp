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

#include "Stdafx.h"
#include "TextPtrA.h"
#include "ClrUtil.h"

using namespace System::Runtime::InteropServices;

namespace NppSharp
{
	TextPtrA::TextPtrA(const char* ptr, int length, int codePage)
		: _ptr(ptr)
		, _length(length)
		, _codePage(codePage)
		, _str(nullptr)
	{
	}

	String^ TextPtrA::Text::get()
	{
		if (_str != nullptr) return _str;

		if (_codePage == SC_CP_UTF8)
		{
			_str = NativeUtf8ToClrString(_ptr, _length);
		}
		else
		{
			_str = Marshal::PtrToStringAnsi((IntPtr)(void*)_ptr, _length);
		}
		return _str;
	}
}
