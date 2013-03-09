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
