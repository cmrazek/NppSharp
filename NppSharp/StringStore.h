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

// Notepad++ requires we pass it a string pointer, and it will use it later on assuming the pointer
// is still valid (e.g. Docking window titles).  This does not work well with .NET strings.
// This class is intended to be a holding place for native wide string pointers that don't change.

namespace NppSharp
{
	class StringStore
	{
	public:
		StringStore();
		~StringStore();

		const wchar_t*	AddString(String^ str);
		void			ReleaseString(const wchar_t* str);

	private:
		struct StringStoreDef
		{
			wchar_t*	pString;
			int			refCount;
		};
		typedef list<StringStoreDef*> StringStoreList;

		StringStoreList	_strings;
	};

	extern StringStore g_strings;
}
