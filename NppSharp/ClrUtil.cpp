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
}
