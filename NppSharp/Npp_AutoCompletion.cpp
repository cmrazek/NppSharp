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

		std::string listStr = ClrStringToAString(sb->ToString());

		::SendMessageA(_scHandle, SCI_AUTOCSETSEPARATOR, (WPARAM)' ', 0);
		::SendMessageA(_scHandle, SCI_AUTOCSETIGNORECASE, ignoreCase ? 1 : 0, 0);
		::SendMessageA(_scHandle, SCI_AUTOCSHOW, lengthEntered, (LPARAM)listStr.c_str());
	}

	void NppInterface::CancelAutoCompletion()
	{
		::SendMessageA(_scHandle, SCI_AUTOCCANCEL, 0, 0);
	}
}
