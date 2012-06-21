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

#include "StringBuf.h"

namespace NppSharp
{
#define OW_CLASS_NAME	TEXT("NppSharpOutputWindow")
#define OW_TITLE		TEXT("NppSharp Output")

#define OW_RGB(r,g,b)	(r | (g << 8) | (b << 16))	// Scintilla's method of rgb storage.

#define OW_DEFAULT_FONT_NAME	"Courier New"
#define OW_DEFAULT_SIZE			9
#define OW_DEFAULT_BOLD			false
#define OW_DEFAULT_ITALIC		false
#define OW_DEFAULT_UNDERLINE	false
#define OW_DEFAULT_FORECOLOR	OW_RGB(0, 0, 0)
#define OW_DEFAULT_BACKCOLOR	OW_RGB(255, 255, 255)

	enum OutputItemAction
	{
		OUTPUTITEM_WRITE,
		OUTPUTITEM_WRITELINE,
		OUTPUTITEM_GOTOTOP,
		OUTPUTITEM_GOTOBOTTOM
	};

	struct OutputItem
	{
		OutputItemAction	action;
		int					style;
		wstring				text;

		OutputItem(OutputItemAction action, int style = 0, const wstring &text = L"")
			: action(action)
			, style(style)
			, text(text)
		{ }
	};
	typedef list<OutputItem> OutputItemList;

	struct OutputStyleRec
	{
		int		style;
		string	fontName;
		int		size;
		bool	bold;
		bool	italic;
		bool	underline;
		int		foreColor;
		int		backColor;
	};
	typedef list<OutputStyleRec*> OutputStyleList;

	class OutputWindow
	{
	public:
		OutputWindow(HWND nppHandle);
		virtual ~OutputWindow();

		void	Clear();
		bool	GetDocked() { return _docked; }
		HWND	GetHandle();
		int		GetStyle() const { return _style; }
		bool	GetVisible() const { return _visible; }
		void	GoToTop();
		void	GoToBottom();
		LRESULT	WndProc(HWND hWindow, UINT uiMsg, WPARAM wParam, LPARAM lParam);
		void	SetDocked(bool docked) { _docked = docked; }
		void	SetStyle(int style) { _style = style; }
		void	SetVisible(bool visible) { _visible = visible; }
		void	Write(const wchar_t *pszText);
		void	WriteLine(const wchar_t *pszText);

		void	SetStyleFont(int style, const char *pszFontName);
		void	SetStyleSize(int style, int size);
		void	SetStyleBold(int style, bool bold);
		void	SetStyleItalic(int style, bool italic);
		void	SetStyleUnderline(int style, bool underline);
		void	SetStyleForeColor(int style, int foreColor);
		void	SetStyleBackColor(int style, int backColor);

	private:
		OutputStyleRec*	GetStyleRec(int style);
		void			InitStyles();
		void			OnContextMenu(int x, int y);
		void			OnLoad();
		void			OnResize(int width, int height);
		void			OnShowWindow(bool show);
		void			ProcessQueue();
		void			WriteBuffer();

		HWND			_hWindow;
		bool			_docked;
		HWND			_hNpp;
		HWND			_hScintilla;
		StringBufA		_bufA;
		int				_style;
		bool			_visible;
		OutputItemList	_queue;
		bool			_forceQueue;
		OutputStyleList	_styles;
	};

	
}
