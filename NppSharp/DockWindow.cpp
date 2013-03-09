#include "StdAfx.h"
#include <vcclr.h>
#include "DockWindow.h"
#include "StringStore.h"
#include "Docking.h"
#include "ClrUtil.h"

namespace NppSharp
{
	LRESULT CALLBACK DockWindowProc(HWND hWindow, UINT msg, WPARAM wParam, LPARAM lParam);

	DockWindow::DockWindow()
		: _nppWindow(NULL)
		, _window(nullptr)
		, _hContainer(NULL)
		, _title(NULL)
		, _tabIcon(NULL)
		, _ptr(NULL)
		, _id(0)
	{
	}

	DockWindow::~DockWindow()
	{
		if (_ptr)
		{
			_ptr->dockWindow = nullptr;
			delete _ptr;
			_ptr = NULL;
		}

		if (_title) g_strings.ReleaseString(_title);
	}

	void DockWindow::RegisterWindowClass()
	{
		if (!_registered)
		{
			WNDCLASSEX	wc;
			memset(&wc, 0, sizeof(wc));
			wc.cbSize = sizeof(wc);
			wc.style = CS_HREDRAW | CS_VREDRAW | CS_SAVEBITS;
			wc.lpfnWndProc = DockWindowProc;
			wc.hInstance = ::GetModuleHandle(NULL);
			wc.hbrBackground = (HBRUSH)(COLOR_BTNFACE + 1);
			wc.lpszClassName = DOCKWINDOW_CLASSNAME;

			if (!::RegisterClassEx(&wc))
			{
				throw gcnew NppException(String::Format("Failed to register dock window container class: {0}", GetLastErrorClrString()));
			}
			_registered = true;
		}
	}

	void DockWindow::UnregisterWindowClass()
	{
		if (_registered && _dockWindowCount == 0)
		{
			::UnregisterClass(DOCKWINDOW_CLASSNAME, ::GetModuleHandle(NULL));
			_registered = false;
		}
	}

	void DockWindow::OnShutdown()
	{
		_shuttingDown = true;
		UnregisterWindowClass();
	}

	void DockWindow::Init(HWND nppWindow, IWin32Window^ window, String^ title, DockWindowAlignment alignment, Icon^ tabIcon, int id)
	{
		_nppWindow = nppWindow;
		_window = window;
		_title = g_strings.AddString(title);
		_id = id;

		RegisterWindowClass();

		unsigned int align = DWS_DF_CONT_LEFT;
		switch (alignment)
		{
		case DockWindowAlignment::Left:
			align = DWS_DF_CONT_LEFT;
			break;
		case DockWindowAlignment::Right:
			align = DWS_DF_CONT_RIGHT;
			break;
		case DockWindowAlignment::Top:
			align = DWS_DF_CONT_TOP;
			break;
		case DockWindowAlignment::Bottom:
			align = DWS_DF_CONT_BOTTOM;
			break;
		case DockWindowAlignment::Floating:
			align = DWS_DF_FLOATING;
			break;
		default:
			throw gcnew ArgumentException("Invalid dock window alignment.");
		}

		HWND hDockWindow = (HWND)window->Handle.ToInt32();
		RECT windowRect;
		::GetWindowRect(hDockWindow, &windowRect);

		_ptr = new DockWindowPtr;
		_ptr->dockWindow = this;

		_hContainer = ::CreateWindowEx(0, DOCKWINDOW_CLASSNAME, TEXT(""),  WS_CHILD | WS_CLIPCHILDREN,
			0, 0, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top,
			_nppWindow, NULL, ::GetModuleHandle(NULL), _ptr);
		if (!_hContainer)
		{
			throw gcnew NppException(String::Format("Failed to create dock window container: {0}", GetLastErrorClrString()));
		}
		_dockWindowCount++;
		
		::SetWindowLongPtr(_hContainer, GWLP_USERDATA, (LONG_PTR)_ptr);
		::SetParent(hDockWindow, _hContainer);

		tTbData	tb;
		memset(&tb, 0, sizeof(tb));
		tb.hClient = _hContainer;
		tb.pszName = const_cast<TCHAR*>(_title);
		tb.pszAddInfo = TEXT("");
		tb.dlgID = GetDialogId();
		tb.pszModuleName = MODULE_NAME;
		tb.uMask = align;
		tb.hIconTab = tabIcon == nullptr ? NULL : (HICON)tabIcon->Handle.ToInt32();

		::SendMessageW(_nppWindow, NPPM_DMMREGASDCKDLG, 0, (LPARAM)&tb);
		::SendMessageW(_nppWindow, NPPM_DMMSHOW, 0, (LPARAM)_hContainer);

		// Notepad++ has problems with drawing over top of docked windows.
		// Turn on the WS_CLIPCHILDREN style for the parent window.
		HWND hParent = ::GetParent(_hContainer);
		if (hParent)
		{
			LONG style = ::GetWindowLong(hParent, GWL_STYLE);
			style |= WS_CLIPCHILDREN;
			::SetWindowLong(hParent, GWL_STYLE, style);
		}
		else throw gcnew NppException(String::Format("Failed to get dock window parent: {0}", GetLastErrorClrString()));
	}

	LRESULT CALLBACK DockWindowProc(HWND hWindow, UINT msg, WPARAM wParam, LPARAM lParam)
	{
		switch (msg)
		{
		case WM_CREATE:
			return 0;

		default:
			{
				DockWindowPtr* ptr = (DockWindowPtr*)::GetWindowLongPtr(hWindow, GWLP_USERDATA);
				if (ptr) return ptr->dockWindow->WndProc(hWindow, msg, wParam, lParam);
				return ::DefWindowProc(hWindow, msg, wParam, lParam);
			}
		}
	}

	LRESULT DockWindow::WndProc(HWND hWindow, UINT msg, WPARAM wParam, LPARAM lParam)
	{
		switch (msg)
		{
		case WM_SIZE:
			::MoveWindow((HWND)_window->Handle.ToInt32(), 0, 0, LOWORD(lParam), HIWORD(lParam), TRUE);
			return 0;

		case WM_SHOWWINDOW:
			_visible = wParam != 0;
			return 0;

		case WM_ERASEBKGND:
			return 1;

		case WM_DESTROY:
			_dockWindowCount--;
			if (_shuttingDown && !_dockWindowCount) UnregisterWindowClass();
			return 0;

		default:
			return ::DefWindowProc(hWindow, msg, wParam, lParam);
		}
	}

	void DockWindow::Show()
	{
		_visible = true;
		::SendMessageW(_nppWindow, NPPM_DMMSHOW, 0, (LPARAM)_hContainer);
	}

	void DockWindow::Hide()
	{
		_visible = false;
		::SendMessageW(_nppWindow, NPPM_DMMHIDE, 0, (LPARAM)_hContainer);
	}

	int DockWindow::Id::get()
	{
		return _id;
	}

	bool DockWindow::Visible::get()
	{
		return _visible;
	}

	void DockWindow::Visible::set(bool value)
	{
		if (_visible != value)
		{
			if (value) Show();
			else Hide();
		}
	}
}
