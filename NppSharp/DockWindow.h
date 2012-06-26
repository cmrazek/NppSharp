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

using namespace System::Windows::Forms;
using namespace System::Drawing;

#define DOCKWINDOW_CLASSNAME	TEXT("NppSharpDockWindow")

namespace NppSharp
{
	class DockWnd;
	ref class DockWindow;

	struct DockWindowPtr
	{
		gcroot<DockWindow^>	dockWindow;
	};

	public ref class DockWindow : public IDockWindow
	{
	public:
		DockWindow();
		virtual ~DockWindow();

		void	Init(HWND nppWindow, IWin32Window^ window, String^ title, DockWindowAlignment alignment, Icon^ tabIcon, int id);
		LRESULT	WndProc(HWND hWindow, UINT msg, WPARAM wParam, LPARAM lParam);

		virtual void Show();
		virtual void Hide();

		virtual property int Id { int get(); }
		virtual property bool Visible { bool get(); void set(bool); }

		static void	OnShutdown();

	private:
		int		GetDialogId() { return -_id; }

		static void	RegisterWindowClass();
		static void	UnregisterWindowClass();

		HWND			_nppWindow;
		IWin32Window^	_window;
		HWND			_hContainer;
		const wchar_t*	_title;
		HICON			_tabIcon;
		DockWindowPtr*	_ptr;
		int				_id;
		bool			_visible;

		static bool		_registered = false;
		static int		_dockWindowCount = 0;
		static bool		_shuttingDown = false;
	};
}
