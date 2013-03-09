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

	String^ NativeUtf8ToClrString(const void* buf, int len)
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

#ifndef DOTNET4
	bool IsStringNullOrWhiteSpace(String^ str)
	{
		if (str == nullptr) return true;
		for each (char ch in str)
		{
			if (!Char::IsWhiteSpace(ch)) return false;
		}
		return true;
	}
#endif

	wchar_t ReadUtf8Char(const char** ptr)
	{
		unsigned char ch = **ptr;
		if ((ch & 0xc0) != 0xc0)
		{
			(*ptr)++;
			return (wchar_t)ch;
		}

		wchar_t			ret = 0;
		unsigned char	firstCh = ch;
		unsigned char	firstMask = 0x3f;
		unsigned char	firstShift = 0;
		const char*		pos = *ptr;

		while (ch & 0x40)
		{
			ret = (ret << 6) | (*(++pos) & 0x3f);
			ch <<= 1;
			firstMask >>= 1;
			firstShift += 6;
		}

		*ptr = pos + 1;
		return ret | ((firstCh & firstMask) << firstShift);
	}

	int Utf8CharWidth(const char* ptr)
	{
		unsigned char ch = *ptr;
		if ((ch & 0xc0) != 0xc0) return 1;

		int len = 1;
		while (ch & 0x40)
		{
			len++;
			ch <<= 1;
		}

		return len;
	}

	wchar_t PreviousUtf8Char(const char** ptr)
	{
		const char*	pos = *ptr - 1;
		unsigned char		ch = *pos;
		if ((ch & 0x80) == 0)
		{
			*ptr = pos;
			return (wchar_t)ch;
		}

		wchar_t			ret = ch & 0x3f;
		unsigned char	mask = 0x3f;
		unsigned char	shift = 6;
		pos--;

		while (mask)
		{
			mask >>= 1;
			ch = *pos;
			if (ch & 0x40)
			{
				ret |= (ch & mask) << shift;
				break;
			}
			else
			{
				ret |= ch << shift;
				shift += 6;
				pos--;
			}
		}

		*ptr = pos;
		return ret;
	}
}
