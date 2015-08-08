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
		std::wstring wstr = ClrStringToWString(str);

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
