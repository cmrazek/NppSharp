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

namespace NppSharp
{
	LexerWrapper::LexerWrapper(NppSharp::ILexer^ clrLexer)
		: _clrLexer(clrLexer)
	{
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
			_doc = pAccess;

			const char* pszBuf = _doc->BufferPointer();
			const char* pszPos = pszBuf + startPos;
			const char* pszEnd = pszPos + (unsigned int)lengthDoc;
			const char* pszLineStart = pszPos;
			const char*	pszLineEnd;
			int			line = _doc->LineFromPosition(startPos);
			int			lineLen;
			int			state = line > 0 ? _doc->GetLineState(line - 1) : 0;
			int			codePage = _doc->CodePage();
			LexerLine^	lineObj = gcnew LexerLine();

			while (pszPos < pszEnd)
			{
				_doc->StartStyling(pszPos - pszBuf, 31);
				pszLineEnd = pszBuf + _doc->LineStart(line + 1);
				lineLen = pszLineEnd - pszPos;

				// Get the line text as a CLR string.
				String^ lineStr;
				if (codePage == SC_CP_UTF8)
				{
					lineStr = NativeUtf8ToClrString(pszPos, lineLen);
				}
				else
				{
					lineStr = gcnew String(pszPos, 0, lineLen);
				}

				// Tell script to style the line.
				lineObj->Start(lineStr);
				try
				{
					state = _clrLexer->StyleLine(lineObj, state);
				}
				catch (Exception^ ex)
				{
					NppSharp::WriteOutputLine(NppSharp::OutputStyle::Error, String::Concat("Exception in lexer:\r\n", ex));
				}

				// Apply the styles
				// CLR styler uses unicode chars, but Scintilla uses ANSI or UTF-8.
				// Need to adjust for multi-byte chars.
				array<byte>^ styles = lineObj->StylesBuf;
				if (codePage == SC_CP_UTF8)
				{
					byte ch;
					int charIndex = 0;
					for (int bufIndex = 0; bufIndex < lineLen; ++bufIndex)
					{
						ch = (byte)pszPos[bufIndex];
						if (ch & 0x80)
						{
							// Char is spread across multiple bytes.
							// All lead bytes will be given same style as the final byte.

							// Lead-bytes
							while (ch & 0x40)
							{
								_doc->SetStyleFor(1, styles[charIndex]);
								ch <<= 1;
								bufIndex++;
							}

							// Final-byte
							_doc->SetStyleFor(1, styles[charIndex++]);
						}
						else
						{
							// Single-byte char
							_doc->SetStyleFor(1, styles[charIndex++]);
						}
					}
				}
				else
				{
					for (int i = 0; i < lineLen; ++i)
					{
						_doc->SetStyleFor(1, styles[i]);
					}
				}

				// Advance to next line.
				_doc->SetLineState(line++, state);
				pszPos = pszLineEnd;
			}
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
}
