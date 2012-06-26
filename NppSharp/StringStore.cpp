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

#include "StdAfx.h"
#include "StringStore.h"
#include "ClrUtil.h"

namespace NppSharp
{
	StringStore g_strings;

	StringStore::StringStore()
	{
	}

	StringStore::~StringStore()
	{
		for (StringStoreList::iterator i = _strings.begin(), ii = _strings.end(); i != ii; ++i)
		{
			delete [] (*i)->pString;
			delete *i;
		}
		_strings.clear();
	}

	const wchar_t* StringStore::AddString(String ^str)
	{
		wstring wstr = ClrStringToWString(str);

		for (StringStoreList::iterator i = _strings.begin(), ii = _strings.end(); i != ii; ++i)
		{
			if (wcscmp((*i)->pString, wstr.c_str()) == 0)
			{
				return (*i)->pString;
			}
		}

		StringStoreDef *pDef = new StringStoreDef;
		pDef->pString = new wchar_t[wstr.length() + 1];
		wcscpy(pDef->pString, wstr.c_str());
		pDef->refCount = 1;

		_strings.push_back(pDef);
		return pDef->pString;
	}

	void StringStore::ReleaseString(const wchar_t *str)
	{
		for (StringStoreList::iterator i = _strings.begin(), ii = _strings.end(); i != ii; ++i)
		{
			StringStoreDef *pDef = *i;
			if (pDef->pString == str)
			{
				if (--(pDef->refCount) == 0)
				{
					_strings.remove(pDef);
					delete [] pDef->pString;
					delete pDef;
				}
				break;
			}
		}
	}
}
