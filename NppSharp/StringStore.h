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
