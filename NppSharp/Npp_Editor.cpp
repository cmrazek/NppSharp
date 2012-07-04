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
#include "NppInterface.h"
#include "StringBuf.h"
#include "ClrUtil.h"

using namespace System::Runtime::InteropServices;

namespace NppSharp
{
	void NppInterface::OnCommandStart()
	{
		int view = 0;
		::SendMessageW(_nppHandle, NPPM_GETCURRENTSCINTILLA, 0, (LPARAM)&view);
		switch (view)
		{
		case 1:
			_currentScView = 1;
			_scHandle = _scHandle2;
			break;
		default:
			_currentScView = 0;
			_scHandle = _scHandle1;
		}
	}

	void NppInterface::OnCommandEnd()
	{
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	// Notepad++ Messages
	////////////////////////////////////////////////////////////////////////////////////////////////

	EditorView NppInterface::CurrentView::get()
	{
		switch (_currentScView)
		{
		case 1:
			return EditorView::Sub;
		default:
			return EditorView::Main;
		}
	}

	int NppInterface::LanguageId::get()
	{
		int langType = 0;
		::SendMessageW(_nppHandle, NPPM_GETCURRENTLANGTYPE, 0, (LPARAM)&langType);
		return langType;
	}

	void NppInterface::LanguageId::set(int langType)
	{
		::SendMessageW(_nppHandle, NPPM_SETCURRENTLANGTYPE, 0, (LPARAM)langType);
	}

	String^ NppInterface::LanguageName::get()
	{
		int langType = 0;
		::SendMessageW(_nppHandle, NPPM_GETCURRENTLANGTYPE, 0, (LPARAM)&langType);

		StringBufW buf(::SendMessageW(_nppHandle, NPPM_GETLANGUAGENAME, langType, 0));
		::SendMessageW(_nppHandle, NPPM_GETLANGUAGENAME, langType, (LPARAM)buf.Ptr());
		return gcnew String(buf.Ptr());
	}

	String^ NppInterface::GetLanguageName(int langId)
	{
		StringBufW buf(::SendMessageW(_nppHandle, NPPM_GETLANGUAGENAME, langId, 0));
		::SendMessageW(_nppHandle, NPPM_GETLANGUAGENAME, langId, (LPARAM)buf.Ptr());
		return gcnew String(buf.Ptr());
	}

	String^ NppInterface::FileName::get()
	{
		wchar_t	szBuf[MAX_PATH] = L"";
		if (!::SendMessageW(_nppHandle, NPPM_GETFULLCURRENTPATH, MAX_PATH, (LPARAM)szBuf)) return "";
		return gcnew String(szBuf);
	}

	String^ NppInterface::NppDir::get()
	{
		wchar_t	szBuf[MAX_PATH] = L"";
		if (!::SendMessageW(_nppHandle, NPPM_GETNPPDIRECTORY, MAX_PATH, (LPARAM)szBuf)) return "";
		return gcnew String(szBuf);
	}

	int NppInterface::FileCount::get()
	{
		return ::SendMessageW(_nppHandle, NPPM_GETNBOPENFILES, 0, ALL_OPEN_FILES);
	}

	IEnumerable<String^>^ NppInterface::FileNames::get()
	{
		int numFiles = ::SendMessageW(_nppHandle, NPPM_GETNBOPENFILES, 0, ALL_OPEN_FILES);
		if (!numFiles) return gcnew List<String^>();

		wchar_t	*pBuf = new wchar_t[numFiles * MAX_PATH];
		wchar_t	**ppFileNames = new wchar_t*[numFiles];

		for (int i = 0; i < numFiles; i++)
		{
			ppFileNames[i] = pBuf + i * MAX_PATH;
		}

		numFiles = ::SendMessageW(_nppHandle, NPPM_GETOPENFILENAMES, (WPARAM)ppFileNames, numFiles);

		List<String^>^ list = gcnew List<String^>(numFiles);
		for (int i = 0; i < numFiles; i++) list->Add(gcnew String(ppFileNames[i]));

		delete pBuf;
		delete [] ppFileNames;

		return list;
	}

	bool NppInterface::OpenFile(String^ fileName)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(fileName);
		return ::SendMessageW(_nppHandle, NPPM_DOOPEN, 0, (LPARAM)str) != 0;
	}

	int NppInterface::ActiveFileIndex::get()
	{
		return ::SendMessageW(_nppHandle, NPPM_GETCURRENTDOCINDEX, 0, _currentScView);
	}

	void NppInterface::ActiveFileIndex::set(int value)
	{
		::SendMessageW(_nppHandle, NPPM_ACTIVATEDOC, _currentScView, value);
	}

	int NppInterface::GetActiveFileIndex(EditorView view)
	{
		return ::SendMessageW(_nppHandle, NPPM_GETCURRENTDOCINDEX, 0, (LPARAM)view);
	}

	void NppInterface::SetActiveFileIndex(EditorView view, int index)
	{
		::SendMessageW(_nppHandle, NPPM_ACTIVATEDOC, (WPARAM)view, index);
	}

	void NppInterface::ReloadFile(bool withAlert)
	{
		wchar_t	szBuf[MAX_PATH] = L"";
		if (!::SendMessageW(_nppHandle, NPPM_GETFULLCURRENTPATH, MAX_PATH, (LPARAM)szBuf)) return;

		::SendMessageW(_nppHandle, NPPM_RELOADFILE, withAlert ? 1 : 0, (LPARAM)szBuf);
	}

	void NppInterface::ReloadFile(String^ fileName, bool withAlert)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(fileName);
		::SendMessageW(_nppHandle, NPPM_RELOADFILE, withAlert ? 1 : 0, (LPARAM)str);
	}

	bool NppInterface::SwitchToFile(String^ fileName)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(fileName);
		return ::SendMessageW(_nppHandle, NPPM_SWITCHTOFILE, 0, (LPARAM)str) != 0;
	}

	bool NppInterface::SaveFile()
	{
		return ::SendMessageW(_nppHandle, NPPM_SAVECURRENTFILE, 0, 0) != 0;
	}

	bool NppInterface::SaveFileAs(String^ fileName)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(fileName);
		return ::SendMessageW(_nppHandle, NPPM_SAVECURRENTFILEAS, 0, (LPARAM)str) != 0;
	}

	bool NppInterface::SaveFileCopyAs(String^ fileName)
	{
		pin_ptr<const wchar_t> str = PtrToStringChars(fileName);
		return ::SendMessageW(_nppHandle, NPPM_SAVECURRENTFILEAS, 1, (LPARAM)str) != 0;
	}

	bool NppInterface::SaveAllFiles()
	{
		return ::SendMessageW(_nppHandle, NPPM_SAVEALLFILES, 0, 0) != 0;
	}

	void NppInterface::LaunchFindInFiles(String^ dir, String^ filters)
	{
		pin_ptr<const wchar_t> dirsPtr = PtrToStringChars(dir);
		pin_ptr<const wchar_t> filtersPtr = PtrToStringChars(filters);

		::SendMessageW(_nppHandle, NPPM_LAUNCHFINDINFILESDLG, (WPARAM)dirsPtr, (LPARAM)filtersPtr);
	}

	void NppInterface::MenuCommand(int commandId)
	{
		::SendMessageW(_nppHandle, NPPM_MENUCOMMAND, 0, commandId);
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	// Scintilla Messages
	////////////////////////////////////////////////////////////////////////////////////////////////

	String^ NppInterface::GetLineText(int line, bool includeLineEndChars)
	{
		if (line <= 0) return "";

		int lineCount = ::SendMessageA(_scHandle, SCI_GETLINECOUNT, 0, 0);
		if (line > lineCount) return "";

		if (includeLineEndChars)
		{
			int len = ::SendMessageA(_scHandle, SCI_GETLINE, line - 1, 0);
			if (len == 0) return "";

			StringBufA buf(len + 1);
			::SendMessageA(_scHandle, SCI_GETLINE, line - 1, (LPARAM)buf.Ptr());

			if (::SendMessage(_scHandle, SCI_GETCODEPAGE, 0, 0) == SC_CP_UTF8)
			{
				return NativeUtf8ToClrString(buf.Ptr(), len);
			}
			else
			{
				return gcnew String(buf.Ptr());
			}
		}
		else
		{
			int lineStartPos = ::SendMessageA(_scHandle, SCI_POSITIONFROMLINE, line - 1, 0);
			int lineEndPos = ::SendMessageA(_scHandle, SCI_GETLINEENDPOSITION, line - 1, 0);
			if (lineStartPos == lineEndPos) return "";

			StringBufA buf(lineEndPos - lineStartPos);

			npp::Sci_TextRange tr;
			tr.chrg.cpMin = lineStartPos;
			tr.chrg.cpMax = lineEndPos;
			tr.lpstrText  = buf.Ptr();

			int retLen = ::SendMessageA(_scHandle, SCI_GETTEXTRANGE, 0, (LPARAM)&tr);
			buf[retLen] = 0;

			if (::SendMessage(_scHandle, SCI_GETCODEPAGE, 0, 0) == SC_CP_UTF8)
			{
				return NativeUtf8ToClrString(buf.Ptr(), lineEndPos - lineStartPos);
			}
			else
			{
				return gcnew String(buf.Ptr());
			}
		}
	}

	int NppInterface::LineCount::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETLINECOUNT, 0, 0);
	}

	void NppInterface::Insert(String^ text)
	{
		char *pszText = (char*)(void*)Marshal::StringToHGlobalAnsi(text);
		::SendMessageA(_scHandle, SCI_REPLACESEL, 0, (LPARAM)pszText);
		Marshal::FreeHGlobal((IntPtr)pszText);
	}

	bool NppInterface::ClampTextRange(int *pStartPos, int *pLength)
	{
		// Returns false if the range does not encompass any characters, or true if the range
		// is valid.
		// pStartPos and pLength are only assigned to if this function returns true;

		int startPos = *pStartPos;
		int length = *pLength;

		if (startPos < 0)
		{
			length += startPos;
			startPos = 0;
		}
		if (length <= 0) return false;

		int docLength = ::SendMessageA(_scHandle, SCI_GETTEXTLENGTH, 0, 0);
		if (startPos >= docLength) return false;
		if (startPos + length > docLength) length = docLength - startPos;

		*pStartPos = startPos;
		*pLength = length;
		return true;
	}

	String^ NppInterface::GetText(int startPos, int length)
	{
		if (!ClampTextRange(&startPos, &length)) return "";

		npp::Sci_TextRange tr;
		tr.chrg.cpMin = startPos;
		tr.chrg.cpMax = startPos + length;

		StringBufA buf(length);
		tr.lpstrText = buf.Ptr();

		int retLen = ::SendMessageA(_scHandle, SCI_GETTEXTRANGE, 0, (LPARAM)&tr);
		buf[retLen] = 0;

		if (::SendMessage(_scHandle, SCI_GETCODEPAGE, 0, 0) == SC_CP_UTF8)
		{
			return NativeUtf8ToClrString(buf.Ptr(), length);
		}
		else
		{
			return gcnew String(buf.Ptr());
		}
	}

	void NppInterface::Append(String^ text)
	{
		char *pszText = (char*)(void*)Marshal::StringToHGlobalAnsi(text);
		int len = strlen(pszText);
		::SendMessageA(_scHandle, SCI_APPENDTEXT, len, (LPARAM)pszText);
		Marshal::FreeHGlobal((IntPtr)pszText);
	}

	void NppInterface::ClearAll()
	{
		::SendMessageA(_scHandle, SCI_CLEARALL, 0, 0);
	}

	void NppInterface::Cut()
	{
		::SendMessageA(_scHandle, SCI_CUT, 0, 0);
	}

	void NppInterface::Copy()
	{
		::SendMessageA(_scHandle, SCI_COPY, 0, 0);
	}

	void NppInterface::Copy(int startPos, int length)
	{
		if (ClampTextRange(&startPos, &length))
		{
			::SendMessageA(_scHandle, SCI_COPYRANGE, startPos, startPos + length);
		}
	}

	void NppInterface::Copy(String^ text)
	{
		char *pszText = (char*)(void*)Marshal::StringToHGlobalAnsi(text);
		int len = strlen(pszText);
		::SendMessageA(_scHandle, SCI_COPYTEXT, len, (LPARAM)pszText);
		Marshal::FreeHGlobal((IntPtr)pszText);
	}

	void NppInterface::CopyAllowLine()
	{
		::SendMessageA(_scHandle, SCI_COPYALLOWLINE, 0, 0);
	}

	void NppInterface::Paste()
	{
		::SendMessageA(_scHandle, SCI_PASTE, 0, 0);
	}

	void NppInterface::Clear()
	{
		::SendMessageA(_scHandle, SCI_CLEAR, 0, 0);
	}

	int NppInterface::Length::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETTEXTLENGTH, 0, 0);
	}

	int NppInterface::FirstVisibleLine::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETFIRSTVISIBLELINE, 0, 0) + 1;
	}

	void NppInterface::FirstVisibleLine::set(int line)
	{
		if (line < 1) line = 1;
		::SendMessageA(_scHandle, SCI_SETFIRSTVISIBLELINE, line - 1, 0);
	}

	int NppInterface::LinesOnScreen::get()
	{
		return ::SendMessageA(_scHandle, SCI_LINESONSCREEN, 0, 0);
	}

	bool NppInterface::FileModified::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETMODIFY, 0, 0) != 0;
	}

	void NppInterface::SetSelection(int anchorPos, int currentPos)
	{
		::SendMessageA(_scHandle, SCI_SETSEL, anchorPos, currentPos);
	}

	void NppInterface::GoToPos(int pos)
	{
		::SendMessageA(_scHandle, SCI_GOTOPOS, pos, 0);
	}

	void NppInterface::GoToLine(int line)
	{
		if (line < 1) line = 1;
		::SendMessageA(_scHandle, SCI_GOTOLINE, line - 1, 0);
	}

	int NppInterface::CurrentPos::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETCURRENTPOS, 0, 0);
	}

	void NppInterface::CurrentPos::set(int pos)
	{
		::SendMessageA(_scHandle, SCI_SETCURRENTPOS, pos, 0);
	}

	int NppInterface::CurrentLine::get()
	{
		int curPos = ::SendMessageA(_scHandle, SCI_GETCURRENTPOS, 0, 0);
		return ::SendMessageA(_scHandle, SCI_LINEFROMPOSITION, curPos, 0) + 1;
	}

	void NppInterface::CurrentLine::set(int line)
	{
		int lineStartPos = ::SendMessageA(_scHandle, SCI_POSITIONFROMLINE, line - 1, 0);
		::SendMessageA(_scHandle, SCI_SETCURRENTPOS, lineStartPos, 0);
	}

	int NppInterface::AnchorPos::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETANCHOR, 0, 0);
	}

	void NppInterface::AnchorPos::set(int pos)
	{
		::SendMessageA(_scHandle, SCI_SETANCHOR, pos, 0);
	}

	int NppInterface::SelectionStart::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETSELECTIONSTART, 0, 0);
	}

	void NppInterface::SelectionStart::set(int value)
	{
		::SendMessageA(_scHandle, SCI_SETSELECTIONSTART, value, 0);
	}

	int NppInterface::SelectionEnd::get()
	{
		return ::SendMessageA(_scHandle, SCI_GETSELECTIONEND, 0, 0);
	}

	void NppInterface::SelectionEnd::set(int value)
	{
		::SendMessageA(_scHandle, SCI_SETSELECTIONEND, value, 0);
	}

	void NppInterface::SetEmptySelection(int pos)
	{
		::SendMessageA(_scHandle, SCI_SETEMPTYSELECTION, pos, 0);
	}

	void NppInterface::SelectAll()
	{
		::SendMessageA(_scHandle, SCI_SELECTALL, 0, 0);
	}

	int NppInterface::GetLineFromPos(int pos)
	{
		return ::SendMessageA(_scHandle, SCI_LINEFROMPOSITION, pos, 0) + 1;
	}

	int NppInterface::GetLineStartPos(int line)
	{
		return ::SendMessageA(_scHandle, SCI_POSITIONFROMLINE, line - 1, 0);
	}

	int NppInterface::GetLineEndPos(int line)
	{
		return ::SendMessageA(_scHandle, SCI_GETLINEENDPOSITION, line - 1, 0);
	}

	int NppInterface::GetLineLength(int line)
	{
		return ::SendMessageA(_scHandle, SCI_LINELENGTH, line - 1, 0);
	}

	String^ NppInterface::SelectedText::get()
	{
		int len = ::SendMessageA(_scHandle, SCI_GETSELTEXT, 0, 0);
		if (len == 0) return "";
		len--;

		StringBufA buf(len - 1);
		::SendMessageA(_scHandle, SCI_GETSELTEXT, 0, (LPARAM)buf.Ptr());

		if (::SendMessage(_scHandle, SCI_GETCODEPAGE, 0, 0) == SC_CP_UTF8)
		{
			return NativeUtf8ToClrString(buf.Ptr(), len);
		}
		else
		{
			return gcnew String(buf.Ptr());
		}
	}

	void NppInterface::SelectedText::set(String^ text)
	{
		char *pszText = (char*)(void*)Marshal::StringToHGlobalAnsi(text);
		::SendMessageA(_scHandle, SCI_REPLACESEL, 0, (LPARAM)pszText);
		Marshal::FreeHGlobal((IntPtr)pszText);
	}

	NppSharp::SelectionMode NppInterface::SelectionMode::get()
	{
		switch (::SendMessageA(_scHandle, SCI_GETSELECTIONMODE, 0, 0))
		{
		case SC_SEL_STREAM:
			return NppSharp::SelectionMode::Normal;
		case SC_SEL_RECTANGLE:
			return NppSharp::SelectionMode::Rectangle;
		case SC_SEL_LINES:
			return NppSharp::SelectionMode::Lines;
		case SC_SEL_THIN:
			return NppSharp::SelectionMode::Thin;
		default:
			return NppSharp::SelectionMode::Normal;
		}
	}

	void NppInterface::MoveCaretInsideView()
	{
		::SendMessage(_scHandle, SCI_MOVECARETINSIDEVIEW, 0, 0);
	}

	int NppInterface::GetWordEndPos(int pos, bool onlyWordChars)
	{
		return ::SendMessageA(_scHandle, SCI_WORDENDPOSITION, pos, onlyWordChars ? 1 : 0);
	}

	int NppInterface::GetWordStartPos(int pos, bool onlyWordChars)
	{
		return ::SendMessageA(_scHandle, SCI_WORDSTARTPOSITION, pos, onlyWordChars ? 1 : 0);
	}

	int NppInterface::GetColumn(int pos)
	{
		return ::SendMessageA(_scHandle, SCI_GETCOLUMN, pos, 0) + 1;
	}

	int NppInterface::FindColumn(int line, int col)
	{
		return ::SendMessageA(_scHandle, SCI_FINDCOLUMN, line - 1, col - 1);
	}

	int NppInterface::PointToPos(Point pt)
	{
		return ::SendMessage(_scHandle, SCI_POSITIONFROMPOINT, pt.X, pt.Y);
	}

	int NppInterface::PointToPosClose(Point pt)
	{
		return ::SendMessage(_scHandle, SCI_POSITIONFROMPOINTCLOSE, pt.X, pt.Y);
	}

	Point NppInterface::PosToPoint(int pos)
	{
		return Point(
			::SendMessageA(_scHandle, SCI_POINTXFROMPOSITION, 0, pos),
			::SendMessageA(_scHandle, SCI_POINTYFROMPOSITION, 0, pos));
	}

	void NppInterface::MoveSelectedLinesUp()
	{
		::SendMessageA(_scHandle, SCI_MOVESELECTEDLINESUP, 0, 0);
	}

	void NppInterface::MoveSelectedLinesDown()
	{
		::SendMessageA(_scHandle, SCI_MOVESELECTEDLINESDOWN, 0, 0);
	}
}
