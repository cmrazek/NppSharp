#include "StdAfx.h"
#include "OutputWindow.h"
#include "ClrUtil.h"

namespace NppSharp
{
	LRESULT CALLBACK OutputWindowProc(HWND hWindow, UINT uiMsg, WPARAM wParam, LPARAM lParam);

	OutputWindow::OutputWindow(HWND hNpp)
		: _hWindow(NULL)
		, _docked(false)
		, _hNpp(hNpp)
		, _hScintilla(NULL)
		, _style((int)OutputStyle::Normal)
		, _visible(false)
		, _forceQueue(true)
	{
	}

	OutputWindow::~OutputWindow()
	{
		if (_hScintilla)
		{
			::SendMessageW(_hNpp, NPPM_DESTROYSCINTILLAHANDLE, 0, (LPARAM)_hScintilla);
		}

		if (_hWindow)
		{
			::DestroyWindow(_hWindow);
			_hWindow = NULL;
		}

		for (OutputStyleList::iterator s = _styles.begin(), ss = _styles.end(); s != ss; ++s)
		{
			delete *s;
		}
		_styles.clear();
	}

	HWND OutputWindow::GetHandle()
	{
		if (!_hWindow)
		{
			HINSTANCE hInst = ::GetModuleHandle(NULL);

			WNDCLASSEX wc;
			memset(&wc, 0, sizeof(wc));
			wc.cbSize = sizeof(wc);
			wc.style = CS_DBLCLKS | CS_HREDRAW | CS_VREDRAW;
			wc.lpfnWndProc = OutputWindowProc;
			wc.hInstance = hInst;
			wc.hCursor = ::LoadCursor(NULL, IDC_ARROW);
			wc.hbrBackground = (HBRUSH)(COLOR_BTNFACE + 1);
			wc.lpszClassName = OW_CLASS_NAME;

			if (!::RegisterClassEx(&wc))
			{
				throw gcnew NppException(String::Format("Failed to register output window class: {0}", GetLastErrorClrString()));
				return NULL;
			}

			_hWindow = ::CreateWindowEx(WS_EX_CLIENTEDGE, OW_CLASS_NAME, OW_TITLE,
				WS_CHILD | WS_VISIBLE, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
				_hNpp, NULL, hInst, NULL);
			if (!_hWindow)
			{
				throw gcnew NppException(String::Format("Failed to create output window: {0}", GetLastErrorClrString()));
				return NULL;
			}

			::SetWindowLongPtr(_hWindow, GWLP_USERDATA, (LONG_PTR)this);

			OnLoad();
		}

		return _hWindow;
	}

	LRESULT CALLBACK OutputWindowProc(HWND hWindow, UINT uiMsg, WPARAM wParam, LPARAM lParam)
	{
		switch (uiMsg)
		{
		case WM_CREATE:
			return 0;
		default:
			{
				OutputWindow *pOW = (OutputWindow*)::GetWindowLongPtr(hWindow, GWLP_USERDATA);
				if (pOW) return pOW->WndProc(hWindow, uiMsg, wParam, lParam);
				return DefWindowProc(hWindow, uiMsg, wParam, lParam);
			}
		}
	}

	LRESULT OutputWindow::WndProc(HWND hWindow, UINT uiMsg, WPARAM wParam, LPARAM lParam)
	{
		switch (uiMsg)
		{
		case WM_SIZE:
			OnResize(LOWORD(lParam), HIWORD(lParam));
			break;
		case WM_SHOWWINDOW:
			OnShowWindow(wParam != 0);
			break;
		case WM_CONTEXTMENU:
			OnContextMenu(LOWORD(lParam), HIWORD(lParam));
			break;
		default:
			return DefWindowProc(hWindow, uiMsg, wParam, lParam);
		}
		return TRUE;
	}

	void OutputWindow::OnLoad()
	{
		_hScintilla = (HWND)::SendMessageW(_hNpp, NPPM_CREATESCINTILLAHANDLE, 0, (LPARAM)_hWindow);
		if (!_hScintilla)
		{
			throw gcnew NppException("Failed to create scintilla window for output.");
		}

		::SendMessageA(_hScintilla, SCI_USEPOPUP, 0, 0);
		::SendMessageA(_hScintilla, SCI_SETREADONLY, 1, 0);
		::SendMessageA(_hScintilla, SCI_SETCODEPAGE, SC_CP_UTF8, 0);

		RECT clientRect;
		::GetClientRect(_hWindow, &clientRect);
		OnResize(clientRect.right - clientRect.left, clientRect.bottom - clientRect.top);

		::ShowWindow(_hScintilla, SW_SHOW);

		InitStyles();
		ProcessQueue();
	}

	void OutputWindow::ProcessQueue()
	{
		_forceQueue = false;
		if (_queue.empty()) return;

		int oldStyle = _style;

		for (OutputItemList::iterator i = _queue.begin(), ii = _queue.end(); i != ii; ++i)
		{
			_style = i->style;

			switch (i->action)
			{
			case OUTPUTITEM_WRITE:
				_style = i->style;
				Write(i->text.c_str());
				break;

			case OUTPUTITEM_WRITELINE:
				_style = i->style;
				WriteLine(i->text.c_str());
				break;

			case OUTPUTITEM_GOTOTOP:
				GoToTop();
				break;

			case OUTPUTITEM_GOTOBOTTOM:
				GoToBottom();
				break;
			}
		}

		_style = oldStyle;
		_queue.clear();
	}

	void OutputWindow::OnResize(int width, int height)
	{
		if (_hScintilla)
		{
			::MoveWindow(_hScintilla, 0, 0, width, height, TRUE);
		}
	}

	void OutputWindow::OnShowWindow(bool show)
	{
		if (!show) _visible = false;
	}

	void OutputWindow::Write(const wchar_t* pszText)
	{
		if (!pszText || !*pszText) return;

		if (!_hScintilla || _forceQueue)
		{
			_queue.push_back(OutputItem(OUTPUTITEM_WRITE, _style, pszText));
		}

		NativeWToUtf8BufA(pszText, _bufA);
		WriteBuffer();
	}

	void OutputWindow::WriteLine(const wchar_t* pszText)
	{
		if (!pszText) return;

		if (!_hScintilla || _forceQueue)
		{
			_queue.push_back(OutputItem(OUTPUTITEM_WRITELINE, _style, pszText));
			return;
		}

		NativeWToUtf8BufA(pszText, _bufA);
		_bufA.Append("\r\n");
		WriteBuffer();
	}

	void OutputWindow::WriteBuffer()
	{
		if (_bufA.Length() == 0) return;

		// Check if the current position is at the end of the document.
		int curPos = ::SendMessageA(_hScintilla, SCI_GETCURRENTPOS, 0, 0);
		int endPos = ::SendMessageA(_hScintilla, SCI_GETTEXTLENGTH, 0, 0);
		bool moveToEnd = curPos == endPos;

		// Clear read-only flag so we can add the text.
		::SendMessageA(_hScintilla, SCI_SETREADONLY, 0, 0);

		// Write the text
		::SendMessageA(_hScintilla, SCI_APPENDTEXT, _bufA.Length(), (LPARAM)_bufA.Ptr());

		// Apply styling
		int newEndPos = ::SendMessageA(_hScintilla, SCI_GETTEXTLENGTH, 0, 0);
		::SendMessageA(_hScintilla, SCI_STARTSTYLING, endPos, 31);
		::SendMessageA(_hScintilla, SCI_SETSTYLING, newEndPos - endPos, _style);

		// Set back to read-only
		::SendMessageA(_hScintilla, SCI_SETREADONLY, 1, 0);

		// Scroll to the bottom, if necessary.
		if (moveToEnd)
		{
			curPos = ::SendMessageA(_hScintilla, SCI_GETTEXTLENGTH, 0, 0);
			::SendMessageA(_hScintilla, SCI_GOTOPOS, curPos, 0);
		}
	}

	void OutputWindow::Clear()
	{
		::SendMessageA(_hScintilla, SCI_SETREADONLY, 0, 0);
		::SendMessage(_hScintilla, SCI_CLEARALL, 0, 0);
		::SendMessageA(_hScintilla, SCI_SETREADONLY, 1, 0);
	}

	void OutputWindow::OnContextMenu(int x, int y)
	{
		HMENU hMenu = ::CreatePopupMenu();
		::AppendMenu(hMenu, MF_STRING, 1, L"&Clear");

		int cmd = ::TrackPopupMenu(hMenu, TPM_RETURNCMD | TPM_NONOTIFY | TPM_RIGHTBUTTON,
			x, y, 0, _hScintilla, NULL);

		::DestroyMenu(hMenu);

		if (cmd == 1)
		{
			Clear();
		}
	}

	void OutputWindow::InitStyles()
	{
		for (OutputStyleList::iterator s = _styles.begin(), ss = _styles.end(); s != ss; ++s)
		{
			OutputStyleRec*	pStyle = *s;
			::SendMessageA(_hScintilla, SCI_STYLESETFONT, pStyle->style, (LPARAM)pStyle->fontName.c_str());
			::SendMessageA(_hScintilla, SCI_STYLESETSIZE, pStyle->style, pStyle->size);
			::SendMessageA(_hScintilla, SCI_STYLESETBOLD, pStyle->style, pStyle->bold ? 1 : 0);
			::SendMessageA(_hScintilla, SCI_STYLESETITALIC, pStyle->style, pStyle->italic ? 1 : 0);
			::SendMessageA(_hScintilla, SCI_STYLESETUNDERLINE, pStyle->style, pStyle->underline ? 1 : 0);
			::SendMessageA(_hScintilla, SCI_STYLESETFORE, pStyle->style, pStyle->foreColor);
			::SendMessageA(_hScintilla, SCI_STYLESETBACK, pStyle->style, pStyle->backColor);
		}
	}

	void OutputWindow::GoToTop()
	{
		if (!_hScintilla)
		{
			_queue.push_back(OutputItem(OUTPUTITEM_GOTOTOP));
		}
		else
		{
			::SendMessageA(_hScintilla, SCI_GOTOPOS, 0, 0);
		}
	}

	void OutputWindow::GoToBottom()
	{
		if (!_hScintilla)
		{
			_queue.push_back(OutputItem(OUTPUTITEM_GOTOBOTTOM));
		}
		else
		{
			int endPos = ::SendMessageA(_hScintilla, SCI_GETTEXTLENGTH, 0, 0);
			::SendMessageA(_hScintilla, SCI_GOTOPOS, endPos, 0);
		}
	}

	void OutputWindow::SetStyleFont(int style, const char *pszFontName)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->fontName = pszFontName;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETFONT, style, (LPARAM)pszFontName);
	}

	void OutputWindow::SetStyleSize(int style, int size)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->size = size;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETSIZE, style, size);
	}

	void OutputWindow::SetStyleBold(int style, bool bold)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->bold = bold;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETBOLD, style, bold ? 1 : 0);
	}

	void OutputWindow::SetStyleItalic(int style, bool italic)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->italic = italic;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETITALIC, style, italic ? 1 : 0);
	}

	void OutputWindow::SetStyleUnderline(int style, bool underline)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->underline = underline;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETUNDERLINE, style, underline ? 1 : 0);
	}

	void OutputWindow::SetStyleForeColor(int style, int foreColor)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->foreColor = foreColor;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETFORE, style, foreColor);
	}

	void OutputWindow::SetStyleBackColor(int style, int backColor)
	{
		OutputStyleRec* pStyle = GetStyleRec(style);
		pStyle->backColor = backColor;

		if (_hScintilla) ::SendMessageA(_hScintilla, SCI_STYLESETBACK, style, backColor);
	}

	OutputStyleRec* OutputWindow::GetStyleRec(int style)
	{
		OutputStyleRec* pStyle;
		for (OutputStyleList::iterator s = _styles.begin(), ss = _styles.end(); s != ss; ++s)
		{
			pStyle = *s;
			if (pStyle->style == style) return pStyle;
		}

		pStyle = new OutputStyleRec;
		pStyle->style = style;
		pStyle->fontName = OW_DEFAULT_FONT_NAME;
		pStyle->size = OW_DEFAULT_SIZE;
		pStyle->bold = OW_DEFAULT_BOLD;
		pStyle->italic = OW_DEFAULT_ITALIC;
		pStyle->underline = OW_DEFAULT_UNDERLINE;
		pStyle->foreColor = OW_DEFAULT_FORECOLOR;
		pStyle->backColor = OW_DEFAULT_BACKCOLOR;
		_styles.push_back(pStyle);
		return pStyle;
	}
}
