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

#include "Stdafx.h"
#include "NppInterface.h"
#include "ClrUtil.h"
#include "Docking.h"
#include "DockWindow.h"

namespace NppSharp
{
	IDockWindow^ NppInterface::CreateDockWindow(IWin32Window^ window, String^ title, DockWindowAlignment alignment, Icon^ tabIcon, int id)
	{
		if (window == nullptr) throw gcnew ArgumentNullException("window");
		if (id <= 0) throw gcnew ArgumentException("id must be greater than zero.");

		for each (DockWindow^ w in _dockWindows)
		{
			if (w->Id == id)
			{
				if (!w->Visible) w->Show();
				return w;
			}
		}

		DockWindow^ wnd = gcnew DockWindow();
		wnd->Init(_nppHandle, window, title, alignment, tabIcon, id);
		_dockWindows->Add(wnd);
		return wnd;
	}

	void NppInterface::DockWindow_Shutdown()
	{
		_dockWindows->Clear();
		DockWindow::OnShutdown();
	}

	IDockWindow^ NppInterface::GetDockWindow(int id)
	{
		for each (DockWindow^ wnd in _dockWindows)
		{
			if (wnd->Id == id) return wnd;
		}
		return nullptr;
	}
}
