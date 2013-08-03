#include "StdAfx.h"
#include "NppInterface.h"
#include <vcclr.h>
#include "OutputWindow.h"
#include "TextPtrA.h"
#include "ClrUtil.h"

namespace NppSharp
{
	NppInterface::NppInterface(HWND nppHandle, HWND scHandle1, HWND scHandle2)
		: _nppHandle(nppHandle)
		, _scHandle1(scHandle1)
		, _scHandle2(scHandle2)
		, _scHandle(scHandle1)
		, _scWindow(nullptr)
		, _currentScView(0)
		, _outputWindow(NULL)
		, _outputWindowCmdIndex(0)
		, _commands(gcnew List<PluginCommand^>())
		, _funcItems(NULL)
		, _numFuncItems(0)
		, _dockWindows(gcnew List<DockWindow^>())
		, _lexers(gcnew List<LexerInfo^>())
		, _lexerFileCreated(false)
	{
		Application::EnableVisualStyles();
		Application::SetCompatibleTextRenderingDefault(false);

		_nppWindow = gcnew NativeWindow();
		_nppWindow->AssignHandle((IntPtr)_nppHandle);

		_scWindow1 = gcnew NativeWindow();
		_scWindow1->AssignHandle((IntPtr)_scHandle1);

		_scWindow2 = gcnew NativeWindow();
		_scWindow2->AssignHandle((IntPtr)_scHandle2);

		_outputWindow = new OutputWindow(_nppHandle);
		_bufferIds = gcnew Dictionary<unsigned int, String^>();
	}

	NppInterface::~NppInterface()
	{
		if (_outputWindow) delete _outputWindow;
	}

	NativeWindow^ NppInterface::Window::get()
	{
		return _nppWindow;
	}

	NativeWindow^ NppInterface::EditorWindow1::get()
	{
		return _scWindow1;
	}

	NativeWindow^ NppInterface::EditorWindow2::get()
	{
		return _scWindow2;
	}

	NativeWindow^ NppInterface::EditorWindow::get()
	{
		return _scWindow;
	}

	void NppInterface::FocusEditor()
	{
		if (_scHandle != NULL) ::SetFocus(_scHandle);
	}

	String^ NppInterface::ConfigDir::get()
	{
		wchar_t	szBuf[MAX_PATH] = L"";
		if (!::SendMessageW(_nppHandle, NPPM_GETPLUGINSCONFIGDIR, MAX_PATH, (LPARAM)szBuf)) return "";
		return gcnew String(szBuf);
	}

	List<PluginCommand^>^ NppInterface::GetCommandList()
	{
		GetCommands(this, gcnew EventArgs());
		return _commands;
	}

	void NppInterface::AddCommand(PluginCommand^ cmd)
	{
		_commands->Add(cmd);
	}

	void NppInterface::OnReady()
	{
		Ready(this, gcnew EventArgs());
	}

	void NppInterface::OnShutdown()
	{
		Shutdown(this, gcnew EventArgs());
		DockWindow_Shutdown();
	}

	void NppInterface::OnFileClosing(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		FileClosing(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnFileClosed(unsigned int bufferId)
	{
		String^ fileName;
		if (!_bufferIds->TryGetValue(bufferId, fileName)) fileName = "";
		else _bufferIds->Remove(bufferId);

		FileClosed(this, gcnew FileEventArgs(bufferId, fileName));
	}

	void NppInterface::OnFileOpening(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;
		
		FileOpening(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnFileOpened(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		FileOpened(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnFileSaving(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		FileSaving(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnFileSaved(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		FileSaved(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnFileActivated(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		FileActivated(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnLanguageChanged(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		int langId = ::SendMessageW(_nppHandle, NPPM_GETBUFFERLANGTYPE, bufferId, 0);

		LanguageChanged(this, gcnew LanguageTypeEventArgs(bufferId, _curFileName, langId));
	}

	void NppInterface::OnStyleUpdate(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		StyleUpdate(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnFileLoading()
	{
		FileLoading(this, gcnew EventArgs());
	}

	void NppInterface::OnFileLoadFailed()
	{
		FileLoadFailed(this, gcnew EventArgs());
	}

	void NppInterface::OnFileOrderChanged(unsigned int bufferId)
	{
		_curFileName = GetFileNameByBufferId(bufferId);
		_bufferIds[bufferId] = _curFileName;

		FileOrderChanged(this, gcnew FileEventArgs(bufferId, _curFileName));
	}

	void NppInterface::OnCharAdded(int ch)
	{
		CharAdded(this, gcnew CharAddedEventArgs((char)ch));
	}

	void NppInterface::OnDoubleClick(int pos, bool ctrl, bool alt, bool shift)
	{
		DoubleClick(this, gcnew DoubleClickEventArgs(pos, ctrl, alt, shift));
	}

	void NppInterface::OnModified(npp::SCNotification *pNotify)
	{
		if (pNotify->modificationType & (SC_MOD_INSERTTEXT | SC_MOD_DELETETEXT))
		{
			int codePage;
			if (pNotify->nmhdr.hwndFrom == NULL) codePage = 0;
			else codePage = ::SendMessageA((HWND)pNotify->nmhdr.hwndFrom, SCI_GETCODEPAGE, 0, 0);

			TextPtrA^ textPtr = gcnew TextPtrA(pNotify->text, pNotify->length, codePage);

			// ModifiedEventArgs(ModificationType type, string text, TextLocation location, bool userAction, bool undo, bool redo, int linesAdded)
			ModifiedEventArgs^ e = gcnew ModifiedEventArgs(
				(pNotify->modificationType & SC_MOD_INSERTTEXT) ? ModificationType::Insert : ModificationType::Delete,
				textPtr,
				TextLocation::FromByteOffset(pNotify->position),
				(pNotify->modificationType & SC_PERFORMED_USER) != 0,
				(pNotify->modificationType & SC_PERFORMED_UNDO) != 0,
				(pNotify->modificationType & SC_PERFORMED_REDO) != 0,
				pNotify->linesAdded);

			Modification(this, e);
		}
	}

	void NppInterface::OnUpdateUI(npp::SCNotification *pNotify)
	{
		if (pNotify->updated & SC_UPDATE_SELECTION)
		{
			SelectionChanged(this, gcnew EventArgs());
		}

		if (pNotify->updated & SC_UPDATE_V_SCROLL)
		{
			ScrolledVertically(this, gcnew EventArgs());
		}

		if (pNotify->updated & SC_UPDATE_H_SCROLL)
		{
			ScrolledHorizontally(this, gcnew EventArgs());
		}
	}

	void NppInterface::ExecuteCommandByIndex(int cmdIndex)
	{
		if (cmdIndex < 0 || cmdIndex >= _commands->Count) return;

		ExecuteCommandEventArgs^ e = gcnew ExecuteCommandEventArgs(_commands[cmdIndex]);
		CommandExecuted(this, e);
	}

	void NppInterface::ExecuteCommandById(int cmdId)
	{
		for each (PluginCommand^ cmd in _commands)
		{
			if (cmd->Id == cmdId)
			{
				ExecuteCommandEventArgs^ e = gcnew ExecuteCommandEventArgs(cmd);
				CommandExecuted(this, e);
				break;
			}
		}
	}

	int NppInterface::AllocateCommandId()
	{
		int cmdId = 0;
		if (!::SendMessage(_nppHandle, NPPM_ALLOCATECMDID, 1, (LPARAM)&cmdId))
			throw gcnew NppException(String::Format("Failed to allocate command ID."));
		return cmdId;
	}

	String^ NppInterface::GetFileNameByBufferId(unsigned int bufferId)
	{
		int len = ::SendMessageW(_nppHandle, NPPM_GETFULLPATHFROMBUFFERID, bufferId, NULL);
		if (len <= 0) return nullptr;

		StringBufW buf(len + 1);
		::SendMessageW(_nppHandle, NPPM_GETFULLPATHFROMBUFFERID, bufferId, (LPARAM)buf.Ptr());
		return gcnew String(buf.Ptr());
	}

	void NppInterface::AddToolbarIcon(PluginCommand^ cmd)
	{
		npp::toolbarIcons tbi;
		tbi.hToolbarIcon = NULL;

		Bitmap^ bm = cmd->ToolbarIcon;
		if (bm != nullptr)
		{
			HBITMAP hUserBitmap = (HBITMAP)bm->GetHbitmap().ToInt32();
			HBITMAP hBitmap = MakeCompatibleBitmap(hUserBitmap);
			if (hBitmap)
			{
				tbi.hToolbarBmp = hBitmap;
				::SendMessageW(_nppHandle, NPPM_ADDTOOLBARICON, GetPluginCommandId(cmd), (LPARAM)&tbi);
				::DeleteObject(hUserBitmap);
			}
		}
	}

	HBITMAP	NppInterface::MakeCompatibleBitmap(HBITMAP hUserBitmap)
	{
		BITMAP bmp;
		if (!::GetObject(hUserBitmap, sizeof(bmp), &bmp)) return hUserBitmap;

		HDC hSrcDC = ::CreateCompatibleDC(NULL);
		::SelectObject(hSrcDC, hUserBitmap);

		HDC hNppDC = ::GetDC(_nppHandle);
		HDC hDstDC = ::CreateCompatibleDC(hNppDC);
		HBITMAP hBitmap = ::CreateCompatibleBitmap(hNppDC, bmp.bmWidth, bmp.bmHeight);
		::SelectObject(hDstDC, hBitmap);
		
		::BitBlt(hDstDC, 0, 0, bmp.bmWidth, bmp.bmHeight, hSrcDC, 0, 0, SRCCOPY);

		::DeleteDC(hSrcDC);
		::DeleteDC(hDstDC);
		::ReleaseDC(_nppHandle, hNppDC);
		return hBitmap;
	}

	void NppInterface::OnTbModification()
	{
		RegisterToolbarIcons(this, gcnew EventArgs());
		CreateCommandMenus();
	}

	int NppInterface::GetPluginCommandId(PluginCommand^ cmd)
	{
		int index = cmd->Index;
		if (index >= 0 && index < _numFuncItems) return _funcItems[index]._cmdID;
		return 0;
	}

	void NppInterface::CreateCommandMenus()
	{
		HMENU	hNppSharpMenu = FindNppSharpMenu();

		for each(PluginCommand^ cmd in _commands)
		{
			if (String::IsNullOrWhiteSpace(cmd->MenuName)) continue;

			bool	newMenu = false;
			HMENU	hMenu = GetCommandMenu(cmd->MenuName, cmd->MenuInsertBefore, newMenu);
			if (hMenu)
			{
				if (!newMenu && cmd->Separator) ::AppendMenu(hMenu, MF_SEPARATOR, 0, NULL);

				int cmdId = GetPluginCommandId(cmd);
				wstring cmdName = L"";

				if (hNppSharpMenu && cmdId != 0)
				{
					wchar_t menuName[256];
					MENUITEMINFO info;
					memset(&info, 0, sizeof(info));
					info.cbSize = sizeof(info);
					info.fMask = MIIM_STRING;
					info.dwTypeData = menuName;
					info.cch = 255;
					if (::GetMenuItemInfo(hNppSharpMenu, cmdId, FALSE, &info))
					{
						cmdName = info.dwTypeData;
					}
				}
				
				if (cmdName.empty())
				{
					if (cmd->Shortcut != nullptr)
					{
						cmdName = ClrStringToWString(String::Concat(cmd->Name, gcnew String("\t"), cmd->Shortcut->ToString()));
					}
					else
					{
						cmdName = ClrStringToWString(cmd->Name);
					}
				}

				
				if (::AppendMenu(hMenu, MF_STRING, cmdId, cmdName.c_str()))
				{
					if (hNppSharpMenu) DeleteMenu(hNppSharpMenu, cmdId, MF_BYCOMMAND);
				}
			}
		}
	}

	HMENU NppInterface::GetCommandMenu(String^ menuPath, String^ insertBefore, bool &newMenuOut)
	{
		newMenuOut = false;

		HMENU hParentMenu = ::GetMenu(_nppHandle);
		if (!hParentMenu) return NULL;

		for each (String^ menuNameIter in menuPath->Split('|'))
		{
			if (String::IsNullOrWhiteSpace(menuNameIter)) continue;

			String^ menuName = menuNameIter;
			bool separator = false;
			if (menuNameIter->StartsWith("-"))
			{
				menuName = menuNameIter->Substring(1);
				separator = true;
			}

			if (String::IsNullOrWhiteSpace(menuName)) continue;

			wstring cmdMenuName = ClrStringToWString(menuName);
			wstring cmdMenuNameClean = ClrStringToWString(menuName->Replace("&", ""));
			wstring insertBeforeName = !String::IsNullOrWhiteSpace(insertBefore) ? ClrStringToWString(insertBefore->Replace("&", "")) : L"";

			int		menuInsertIndex = -1;
			HMENU	hMenu = NULL;

			for (int menuIndex = 0, numMenus = ::GetMenuItemCount(hParentMenu); menuIndex < numMenus; menuIndex++)
			{
				wchar_t menuName[256];
				MENUITEMINFO info;
				memset(&info, 0, sizeof(info));
				info.cbSize = sizeof(info);
				info.fMask = MIIM_STRING | MIIM_SUBMENU;
				info.dwTypeData = menuName;
				info.cch = 255;
				if (::GetMenuItemInfo(hParentMenu, menuIndex, TRUE, &info))
				{
					wstring existingMenuName = ClrStringToWString((gcnew String(info.dwTypeData))->Replace("&", ""));
					if (!existingMenuName.length()) continue;

					if (!_wcsicmp(existingMenuName.c_str(), cmdMenuNameClean.c_str()))
					{
						hMenu = info.hSubMenu;
						break;
					}
					else if (menuInsertIndex == -1)
					{
						if (insertBeforeName.length() && !_wcsicmp(existingMenuName.c_str(), insertBeforeName.c_str())) menuInsertIndex = menuIndex;
						else if (!_wcsicmp(existingMenuName.c_str(), L"X")) menuInsertIndex = menuIndex;	// The menu item used to close the current window.
					}
				}
			}

			if (hMenu == NULL)
			{
				hMenu = ::CreatePopupMenu();
				if (!hMenu) return NULL;

				if (menuInsertIndex >= 0)
				{
					if (separator) ::InsertMenu(hParentMenu, menuInsertIndex++, MF_BYPOSITION | MF_SEPARATOR, 0, NULL);

					if (!::InsertMenu(hParentMenu, menuInsertIndex, MF_BYPOSITION | MF_POPUP, (UINT_PTR)hMenu, cmdMenuName.c_str()))
					{
						::DestroyMenu(hMenu);
						return NULL;
					}
				}
				else
				{
					if (separator) ::AppendMenu(hParentMenu, MF_SEPARATOR, 0, NULL);

					if (!::AppendMenu(hParentMenu, MF_POPUP, (UINT_PTR)hMenu, cmdMenuName.c_str()))
					{
						::DestroyMenu(hMenu);
						return NULL;
					}
				}

				newMenuOut = true;
			}

			hParentMenu = hMenu;
		}

		return hParentMenu;
	}

	HMENU NppInterface::FindSubMenu(HMENU hParentMenu, String^ subMenuName)
	{
		if (!hParentMenu || String::IsNullOrWhiteSpace(subMenuName)) return NULL;

		wstring subMenuNameCstr = ClrStringToWString(subMenuName->Replace("&", ""));

		int numMenus = ::GetMenuItemCount(hParentMenu);
		for (int menuIndex = 0; menuIndex < numMenus; menuIndex++)
		{
			wchar_t menuName[256];
			MENUITEMINFO info;
			memset(&info, 0, sizeof(info));
			info.cbSize = sizeof(info);
			info.fMask = MIIM_STRING | MIIM_SUBMENU;
			info.dwTypeData = menuName;
			info.cch = 255;
			if (::GetMenuItemInfo(hParentMenu, menuIndex, TRUE, &info))
			{
				wstring existingMenuName = ClrStringToWString((gcnew String(info.dwTypeData))->Replace("&", ""));
				if (!_wcsicmp(existingMenuName.c_str(), subMenuNameCstr.c_str()))
				{
					return info.hSubMenu;
				}
			}
		}

		return NULL;
	}

	HMENU NppInterface::FindNppSharpMenu()
	{
		HMENU hMenu;
		if (!(hMenu = ::GetMenu(_nppHandle))) return NULL;
		if (!(hMenu = FindSubMenu(hMenu, NPP_PLUGIN_MENU_NAME))) return NULL;
		if (!(hMenu = FindSubMenu(hMenu, MENU_NAME))) return NULL;
		return hMenu;
	}
}
