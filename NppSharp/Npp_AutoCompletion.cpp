#include "Stdafx.h"
#include "NppInterface.h"
#include "ClrUtil.h"

using namespace System::Text;

namespace NppSharp
{
	void NppInterface::ShowAutoCompletion(int lengthEntered, IEnumerable<String^>^ list, bool ignoreCase)
	{
		StringBuilder^ sb = gcnew StringBuilder();
		for each (String^ word in list)
		{
			if (sb->Length > 0) sb->Append(" ");
			sb->Append(word);
		}

		if (sb->Length == 0) return;

		std::string listStr = ClrStringToAString(sb->ToString());

		::SendMessageA(_scHandle, SCI_AUTOCSETSEPARATOR, (WPARAM)' ', 0);
		::SendMessageA(_scHandle, SCI_AUTOCSETIGNORECASE, ignoreCase ? 1 : 0, 0);
		::SendMessageA(_scHandle, SCI_AUTOCSHOW, lengthEntered, (LPARAM)listStr.c_str());
	}

	void NppInterface::CancelAutoCompletion()
	{
		::SendMessageA(_scHandle, SCI_AUTOCCANCEL, 0, 0);
	}

	bool NppInterface::AutoCompletionIsActive::get()
	{
		return ::SendMessageA(_scHandle, SCI_AUTOCACTIVE, 0, 0) != 0;
	}

	void NppInterface::ShowFunctionSignature(TextLocation location, String^ funcSignature)
	{
		int pos = TextLocationToOffset(location);
		std::string sig = ClrStringToAString(funcSignature);
		::SendMessageA(_scHandle, SCI_CALLTIPSHOW, pos, (LPARAM)sig.c_str());
	}

	void NppInterface::SetFunctionSignatureHighlight(int startIndex, int length)
	{
		if (startIndex < 0 || length <= 0) return;

		::SendMessageA(_scHandle, SCI_CALLTIPSETHLT, startIndex, startIndex + length);
	}

	void NppInterface::CancelFunctionSignature()
	{
		::SendMessageA(_scHandle, SCI_CALLTIPCANCEL, 0, 0);
	}

	bool NppInterface::FunctionSignatureIsActive::get()
	{
		return ::SendMessageA(_scHandle, SCI_CALLTIPACTIVE, 0, 0) != 0;
	}

	TextLocation NppInterface::FunctionSignatureLocation::get()
	{
		return OffsetToTextLocation(::SendMessageA(_scHandle, SCI_CALLTIPPOSSTART, 0, 0));
	}
}
