#include "StdAfx.h"
#include "Main.h"
#include "NppInterface.h"
#include "ClrUtil.h"

#define EXT_LEXER_DECL __declspec( dllexport ) __stdcall

namespace NppSharp
{
	struct Globals
	{
	public:
		bool						initialized;
		gcroot<NppInterface^>		npp;
		npp::FuncItem*				funcList;
		std::wstring				strConfigDir;
		std::list<npp::ShortcutKey>	shortcuts;

		Globals()
			: initialized(false)
			, funcList(NULL)
		{ }
	};
	Globals g;

	#pragma unmanaged

	extern "C" __declspec(dllexport) void setInfo(npp::NppData nppData)
	{
		SetPluginInfo(nppData);
	}

	extern "C" __declspec(dllexport) const TCHAR * getName()
	{
		return PLUGIN_NAME;
	}

	extern "C" __declspec(dllexport) npp::FuncItem* getFuncsArray(int *pNumFuncsOut)
	{
		return GetFuncList(pNumFuncsOut);
	}

	extern "C" __declspec(dllexport) void beNotified(npp::SCNotification *pNotify)
	{
		OnNotify(pNotify);
	}

	extern "C" __declspec(dllexport) LRESULT messageProc(UINT uiMsg, WPARAM wParam, LPARAM lParam)
	{
		switch (uiMsg)
		{
		case WM_COMMAND:
			OnCommand(wParam);
			break;
		}
		return TRUE;
	}

	extern "C" __declspec(dllexport) BOOL isUnicode()
	{
		return TRUE;
	}

	int EXT_LEXER_DECL GetLexerCount()
	{
		return OnGetLexerCount();
	}

	void EXT_LEXER_DECL GetLexerName(int iNum, char *pszName, int iNameLen)
	{
		OnGetLexerName(iNum, pszName, iNameLen);
	}

	void EXT_LEXER_DECL GetLexerStatusText(int iNum, TCHAR *pszDesc, int iDescLen)
	{
		OnGetLexerStatusText(iNum, pszDesc, iDescLen);
	}

	LexerFactoryFunction EXT_LEXER_DECL GetLexerFactory(int iIndex)
	{
		return OnGetLexerFactory(iIndex);
	}


	#pragma managed

#include "CmdList.h"

	void SetPluginInfo(npp::NppData nppData)
	{
		try
		{
			// Initialize the main plugin object.
			g.npp = gcnew NppInterface(nppData._nppHandle, nppData._scintillaMainHandle, nppData._scintillaSecondHandle);
			Plugin::Initialize(g.npp);
			g.initialized = true;
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in SetPluginInfo:\n", ex));
			::MessageBox(nppData._nppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in SetPluginInfo:\n", gcnew String(ex.what())));
			::MessageBox(nppData._nppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (...)
		{
			::MessageBox(nppData._nppHandle, L"Unknown exception in SetPluginInfo.", L"Error", MB_OK | MB_ICONERROR);
		}
	}

	npp::FuncItem* GetFuncList(int *pNumFuncsOut)
	{
		try
		{
			List<PluginCommand^>^ funcList = g.npp->GetCommandList();

			g.funcList = new npp::FuncItem[funcList->Count];
			memset(g.funcList, 0, sizeof(npp::FuncItem) * funcList->Count);

			int i = 0;
			for each (PluginCommand ^f in funcList)
			{
				npp::FuncItem *pItem = g.funcList + i;

				String^ funcName = f->Name;
				if (funcName->Length >= npp::nbChar) funcName = funcName->Substring(0, npp::nbChar - 1);
				pin_ptr<const wchar_t> cstr = PtrToStringChars(funcName);

				lstrcpy(pItem->_itemName, cstr);
				if (lstrcmp(cstr, L"-") != 0) pItem->_pFunc = GetCommand(i);
				else pItem->_pFunc = NULL;

				NppShortcut^ nppShortcut = f->Shortcut;
				if (nppShortcut != nullptr)
				{
					npp::ShortcutKey sc;
					sc._isCtrl = nppShortcut->Control;
					sc._isAlt = nppShortcut->Alt;
					sc._isShift = nppShortcut->Shift;
					sc._key = (UCHAR)nppShortcut->KeyCode;
					g.shortcuts.push_back(sc);
					pItem->_pShKey = &(*g.shortcuts.rbegin());
				}

				f->Index = i;
				i++;
			}

			g.npp->SetFuncItems(g.funcList, funcList->Count);
			*pNumFuncsOut = i;
			return g.funcList;
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in GetFuncList:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in GetFuncList:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in GetFuncList.", L"Error", MB_OK | MB_ICONERROR);
		}

		*pNumFuncsOut = 0;
		return NULL;
	}

	void OnNotify(npp::SCNotification *pNotify)
	{
		try
		{
			if (!g.initialized) return;

			switch (pNotify->nmhdr.code)
			{
			case NPPN_READY:
				g.npp->OnReady();
				break;

			case NPPN_SHUTDOWN:
				g.npp->OnShutdown();
				//logWrite(L"Received shutdown notification, closing log.");
				//logClose();
				break;

			case NPPN_TBMODIFICATION:
				g.npp->OnTbModification();
				break;

			case NPPN_FILEBEFORECLOSE:
				g.npp->OnFileClosing(pNotify->nmhdr.idFrom);
				break;

			case NPPN_FILECLOSED:
				g.npp->OnFileClosed(pNotify->nmhdr.idFrom);
				break;

			case NPPN_FILEBEFOREOPEN:
				g.npp->OnFileOpening(pNotify->nmhdr.idFrom);
				break;

			case NPPN_FILEOPENED:
				g.npp->OnFileOpened(pNotify->nmhdr.idFrom);
				break;

			case NPPN_FILEBEFORESAVE:
				g.npp->OnFileSaving(pNotify->nmhdr.idFrom);
				break;

			case NPPN_FILESAVED:
				g.npp->OnFileSaved(pNotify->nmhdr.idFrom);
				break;

			case NPPN_BUFFERACTIVATED:
				g.npp->OnFileActivated(pNotify->nmhdr.idFrom);
				break;

			case NPPN_LANGCHANGED:
				g.npp->OnLanguageChanged(pNotify->nmhdr.idFrom);
				break;

			case NPPN_WORDSTYLESUPDATED:
				g.npp->OnStyleUpdate(pNotify->nmhdr.idFrom);
				break;

			case NPPN_FILEBEFORELOAD:
				g.npp->OnFileLoading();
				break;

			case NPPN_FILELOADFAILED:
				g.npp->OnFileLoadFailed();
				break;

			case NPPN_DOCORDERCHANGED:
				g.npp->OnFileOrderChanged(pNotify->nmhdr.idFrom);
				break;

			case SCN_CHARADDED:
				g.npp->OnCharAdded(pNotify->ch);
				break;

			case SCN_DOUBLECLICK:
				g.npp->OnDoubleClick(
					pNotify->position,
					(pNotify->modifiers & SCMOD_CTRL) != 0,
					(pNotify->modifiers & SCMOD_ALT) != 0,
					(pNotify->modifiers & SCMOD_SHIFT) != 0);
				break;

			case SCN_MODIFIED:
				g.npp->OnModified(pNotify);
				break;
			}
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in OnNotify:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in OnNotify:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in OnNotify.", L"Error", MB_OK | MB_ICONERROR);
		}
	}

	void OnCommand(int cmdId)
	{
		try
		{
			if (!g.initialized) return;
			g.npp->ExecuteCommandById(cmdId);
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in OnCommand:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in OnCommand:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in OnCommand.", L"Error", MB_OK | MB_ICONERROR);
		}
	}

	int OnGetLexerCount()
	{
		try
		{
			g.npp->GenerateLexerConfigFile();
			return g.npp->GetLexerCount();
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in OnGetLexerCount:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
			return 0;
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in OnGetLexerCount:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
			return 0;
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in OnGetLexerCount.", L"Error", MB_OK | MB_ICONERROR);
			return 0;
		}
	}

	void OnGetLexerName(int num, char *buf, int bufLen)
	{
		try
		{
			std::string str = ClrStringToAString(g.npp->GetLexerName(num));
			if ((int)str.length() < bufLen)
			{
				strcpy(buf, str.c_str());
			}
			else
			{
				strncpy(buf, str.c_str(), bufLen - 1);
				buf[bufLen - 1] = 0;
			}
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in OnGetLexerName:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in OnGetLexerName:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in OnGetLexerName.", L"Error", MB_OK | MB_ICONERROR);
		}
	}

	void OnGetLexerStatusText(int num, wchar_t *buf, int bufLen)
	{
		try
		{
			std::wstring str = ClrStringToWString(g.npp->GetLexerDescription(num));
			if ((int)str.length() < bufLen)
			{
				wcscpy(buf, str.c_str());
			}
			else
			{
				wcsncpy(buf, str.c_str(), bufLen - 1);
				buf[bufLen - 1] = 0;
			}
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in OnGetLexerStatusText:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in OnGetLexerStatusText:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in OnGetLexerStatusText.", L"Error", MB_OK | MB_ICONERROR);
		}
	}

#include "LexerFactories.h"

	LexerFactoryFunction OnGetLexerFactory(int index)
	{
		try
		{
			return GetLexerFactoryByIndex(index);
		}
		catch (Exception^ ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("CLR Exception in OnGetLexerFactory:\n", ex));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
			return NULL;
		}
		catch (std::exception ex)
		{
			std::wstring str = ClrStringToWString(String::Concat("STD Exception in OnGetLexerFactory:\n", gcnew String(ex.what())));
			::MessageBox(g.npp->NppHandle, str.c_str(), L"Error", MB_OK | MB_ICONERROR);
			return NULL;
		}
		catch (...)
		{
			::MessageBox(g.npp->NppHandle, L"Unknown exception in OnGetLexerFactory.", L"Error", MB_OK | MB_ICONERROR);
			return NULL;
		}
	}

	void WriteOutputLine(String^ message)
	{
		g.npp->WriteOutputLine(message);
	}

	void WriteOutputLine(OutputStyle style, String^ message)
	{
		OutputStyle oldStyle = style;
		g.npp->OutputStyle = style;
		g.npp->WriteOutputLine(message);
		g.npp->OutputStyle = oldStyle;
	}
}
