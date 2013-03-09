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
