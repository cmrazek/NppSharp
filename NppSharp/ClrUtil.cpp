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
#include "ClrUtil.h"

using namespace System::Text;
using namespace System::Runtime::InteropServices;

namespace NppSharp
{
	wstring ClrStringToWString(String^ str)
	{
		pin_ptr<const wchar_t> ret = PtrToStringChars(str);
		return ret;
	}

	string ClrStringToAString(String^ str)
	{
		char *pszText = (char*)(void*)Marshal::StringToHGlobalAnsi(str);
		string ret = pszText;
		Marshal::FreeHGlobal((IntPtr)pszText);
		return ret;
	}

	String^ NativeUtf8ToClrString(void* buf, int len)
	{
		array<unsigned char>^ arr = gcnew array<unsigned char>(len);
		for (int i = 0; i < len; i++) arr[i] = *(((unsigned char*)buf) + i);
		return Encoding::UTF8->GetString(arr);
	}

	void NativeWToBufA(const wchar_t* wide, StringBufA &buf)
	{
		int len = ::WideCharToMultiByte(CP_ACP, 0, wide, -1, NULL, 0, NULL, NULL) - 1;
		if (len <= 0)
		{
			buf.Clear();
			return;
		}

		buf.Init(len);
		::WideCharToMultiByte(CP_ACP, 0, wide, -1, buf.Ptr(), buf.BufSize(), NULL, NULL);
	}

	void NativeWToUtf8BufA(const wchar_t* wide, StringBufA &buf)
	{
		int len = ::WideCharToMultiByte(CP_UTF8, 0, wide, -1, NULL, 0, NULL, NULL) - 1;
		if (len <= 0)
		{
			buf.Clear();
			return;
		}

		buf.Init(len);
		::WideCharToMultiByte(CP_UTF8, 0, wide, -1, buf.Ptr(), buf.BufSize(), NULL, NULL);
	}

	String^ GetLastErrorClrString()
	{
		String^	ret;
		DWORD	err = GetLastError();
		LPTSTR	buf = NULL;

		if (!::FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
			NULL, err, 0, (LPTSTR)&buf, 0, NULL))
		{
			ret = String::Format("0x{0:X8}", err);
		}
		else
		{
			ret = gcnew String(buf);
			::LocalFree(buf);
		}

		return ret;
	}
}
