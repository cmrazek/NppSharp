#pragma once

#include <vcclr.h>
#include "StringBuf.h"

namespace NppSharp
{
	wstring	ClrStringToWString(String^ str);
	string	ClrStringToAString(String^ str);
	String^	NativeUtf8ToClrString(void *buf, int len);
	void	NativeWToBufA(const wchar_t *wide, StringBufA &buf);
	void	NativeWToUtf8BufA(const wchar_t* wide, StringBufA &buf);
}
