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

#include <vcclr.h>
#include "StringBuf.h"

namespace NppSharp
{
	wstring	ClrStringToWString(String^ str);
	string	ClrStringToAString(String^ str);
	String^	NativeUtf8ToClrString(const void *buf, int len);
	void	NativeWToBufA(const wchar_t *wide, StringBufA &buf);
	void	NativeWToUtf8BufA(const wchar_t* wide, StringBufA &buf);
	String^	GetLastErrorClrString();
}
