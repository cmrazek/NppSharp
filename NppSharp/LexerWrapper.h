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

#include <vcclr.h>

namespace NppSharp
{
	class LexerWrapper : public npp::ILexer
	{
	public:
		LexerWrapper(NppSharp::ILexer^ clrLexer);
		virtual ~LexerWrapper();

		virtual int SCI_METHOD Version() const;
		virtual void SCI_METHOD Release();
		virtual const char * SCI_METHOD PropertyNames();
		virtual int SCI_METHOD PropertyType(const char *name);
		virtual const char * SCI_METHOD DescribeProperty(const char *name);
		virtual int SCI_METHOD PropertySet(const char *key, const char *val);
		virtual const char * SCI_METHOD DescribeWordListSets();
		virtual int SCI_METHOD WordListSet(int n, const char *wl);
		virtual void SCI_METHOD Lex(unsigned int startPos, int lengthDoc, int initStyle, npp::IDocument *pAccess);
		virtual void SCI_METHOD Fold(unsigned int startPos, int lengthDoc, int initStyle, npp::IDocument *pAccess);
		virtual void * SCI_METHOD PrivateCall(int operation, void *pointer);

		NppSharp::ILexer^ GetLexer() { return _clrLexer; }

	private:
		gcroot<NppSharp::ILexer^>	_clrLexer;
		npp::IDocument*				_doc;
	};

	ref class LexerInfo
	{
	public:
		Type^				type;
		String^				name;
		String^				description;
		NppSharp::ILexer^	instance;

		// Config file customizations
		String^				addExt;
		List<LexerStyle^>^	styles;
	};
}
