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
