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
#include "LexerWrapper.h"
#include "Main.h"
#include "ClrUtil.h"
#include "LexerLine.h"

//#define BENCHMARK

namespace NppSharp
{
	std::list<LexerWrapper*> LexerWrapper::_activeLexers;

	LexerWrapper::LexerWrapper(NppSharp::ILexer^ clrLexer)
		: _clrLexer(clrLexer)
	{
		_activeLexers.push_back(this);
	}

	LexerWrapper::~LexerWrapper()
	{
	}

	int LexerWrapper::Version() const
	{
		return 0;
	}

	void LexerWrapper::Release()
	{
		_activeLexers.remove(this);
		delete this;
	}

	const char* LexerWrapper::PropertyNames()
	{
		return "";
	}

	int LexerWrapper::PropertyType(const char *name)
	{
		return SC_TYPE_BOOLEAN;
	}

	const char* LexerWrapper::DescribeProperty(const char *name)
	{
		return "";
	}

	int LexerWrapper::PropertySet(const char *key, const char *val)
	{
		return 0;
	}

	const char * LexerWrapper::DescribeWordListSets()
	{
		return "";
	}

	int LexerWrapper::WordListSet(int n, const char *wl)
	{
		return 0;
	}

	void LexerWrapper::Lex(unsigned int startPos, int lengthDoc, int initStyle, npp::IDocument *pAccess)
	{
		try
		{
#ifdef BENCHMARK
			unsigned __int64 benchFreq, benchStart, benchEnd;
			unsigned int benchLines = 0;
			if (!::QueryPerformanceFrequency((LARGE_INTEGER*)&benchFreq))
			{
				benchFreq = 0;
				WriteOutputLine("Failed to get timer frequency.");
			}
			else if (!::QueryPerformanceCounter((LARGE_INTEGER*)&benchStart))
			{
				benchFreq = 0;
				WriteOutputLine("Failed to get timer counter (start).");
			}
#endif
			_doc = pAccess;

			const char* pszBuf = _doc->BufferPointer();
			const char* pszPos = pszBuf + startPos;
			const char* pszEnd = pszPos + (unsigned int)lengthDoc;
			const char*	pszLineEnd;
			int			line = _doc->LineFromPosition(startPos);
			int			lineLen;
			int			state = line > 0 ? _doc->GetLineState(line - 1) : 0;
			int			codePage = _doc->CodePage();
			LexerLine^	lineObj = gcnew LexerLine();
			int			foldLevel = line > 0 ? (_doc->GetLevel(line - 1) >> 16) : SC_FOLDLEVELBASE;
			int			nextFoldLevel = foldLevel;

			while (pszPos < pszEnd)
			{
				_doc->StartStyling(pszPos - pszBuf, 31);
				pszLineEnd = pszBuf + _doc->LineStart(line + 1);
				lineLen = pszLineEnd - pszPos;

				lineObj->Start(pszPos, pszLineEnd, codePage);

				// Tell script to style the line.
				try
				{
					state = _clrLexer->StyleLine(lineObj, state);
				}
				catch (Exception^ ex)
				{
					NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, String::Concat("Exception in lexer:\r\n", ex));
				}

				_doc->SetStyles(pszLineEnd - pszPos, (const char*)lineObj->StyleBuf);

				// Apply line state
				_doc->SetLineState(line, state);

				// Apply fold level state
				// High 16-bits contains the fold level for the next line, so when the user starts typing on a line,
				// it knows what the fold level should be without having to go back and look.
				int foldStarts = lineObj->FoldStarts;
				int foldEnds = lineObj->FoldEnds;
				nextFoldLevel = foldLevel + foldStarts - foldEnds;
				if (nextFoldLevel < SC_FOLDLEVELBASE) nextFoldLevel = SC_FOLDLEVELBASE;

				foldLevel = (nextFoldLevel << 16) | foldLevel;
				if (foldStarts) foldLevel |= SC_FOLDLEVELHEADERFLAG;
				_doc->SetLevel(line, foldLevel);
				foldLevel = nextFoldLevel;

				// Advance to next line.
				line++;
				pszPos = pszLineEnd;
#ifdef BENCHMARK
				benchLines++;
#endif
			}

#ifdef BENCHMARK
			if (benchFreq != 0)
			{
				if (!::QueryPerformanceCounter((LARGE_INTEGER*)&benchEnd))
				{
					benchFreq = 0;
					WriteOutputLine("Failed to get timer counter (end).");
				}
				else
				{
					unsigned __int64 benchTime = (benchEnd - benchStart) % benchFreq * (unsigned __int64)1000 / benchFreq;
					WriteOutputLine(String::Format("Lexer processed {0} line(s) in {1} msec.", benchLines, benchTime));
				}
			}
#endif
		}
		catch (Exception^ ex)
		{
			NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, String::Concat("CLR exception in lexer:\r\n", ex));
		}
		catch (exception ex)
		{
			NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, String::Concat("STD exception in lexer:\r\n", gcnew String(ex.what())));
		}
		catch (...)
		{
			NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, "Unknown exception in lexer.");
		}
	}

	void LexerWrapper::Fold(unsigned int startPos, int lengthDoc, int initStyle, npp::IDocument *pAccess)
	{
	}

	void* LexerWrapper::PrivateCall(int operation, void *pointer)
	{
		return NULL;
	}

	void LexerWrapper::Refresh()
	{
		if (_doc)
		{
			_doc->StartStyling(0, 31);
			_doc->ChangeLexerState(0, _doc->Length());
		}
	}

	void LexerWrapper::RefreshAllLexers()
	{
		for (std::list<LexerWrapper*>::iterator i = _activeLexers.begin(), ii = _activeLexers.end(); i != ii; ++i)
		{
			try
			{
				(*i)->Refresh();
			}
#ifdef _DEBUG
			catch (std::exception ex)
			{
				WriteOutputLine(OutputStyle::Error, String::Format("STD exception when refreshing lexer: {0}", gcnew String(ex.what())));
			}
#endif
			catch (...)
			{
#ifdef _DEBUG
				WriteOutputLine(OutputStyle::Error, "Exception when refreshing lexer.");
#endif
			}
		}
	}
}
