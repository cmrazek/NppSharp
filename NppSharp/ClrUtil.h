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

#ifndef DOTNET4
	bool	IsStringNullOrWhiteSpace(String^ str);
#endif

	wchar_t	ReadUtf8Char(const char** ptr);
	int		Utf8CharWidth(const char* ptr);
	wchar_t	PreviousUtf8Char(const char** ptr);
}
