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
