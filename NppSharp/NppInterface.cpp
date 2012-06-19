#include "StdAfx.h"
#include "NppInterface.h"
#include <vcclr.h>
#include "OutputWindow.h"

namespace NppSharp
{
	NppInterface::NppInterface(HWND nppHandle, HWND scHandle1, HWND scHandle2)
		: _nppHandle(nppHandle)
		, _scHandle1(scHandle1)
		, _scHandle2(scHandle2)
		, _scHandle(scHandle1)
		, _currentScView(0)
		, _outputWindow(NULL)
		, _outputWindowCmdIndex(0)
		, _commands(gcnew List<PluginCommand^>())
		, _funcItems(NULL)
		, _numFuncItems(0)
	{
		_nppWindow = gcnew NativeWindow();
		_nppWindow->AssignHandle((IntPtr)_nppHandle);
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

	//void NppInterface::Log(String^ message)
	//{
	//	pin_ptr<const wchar_t> str = PtrToStringChars(message);
	//	logWrite(str);
	//}

	//void NppInterface::Log(String^ format, array<Object^>^ args)
	//{
	//	Log(String::Format(format, args));
	//}

	void NppInterface::OnReady()
	{
		Ready(this, gcnew EventArgs());
	}

	void NppInterface::OnShutdown()
	{
		Shutdown(this, gcnew EventArgs());
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
		toolbarIcons tbi;
		tbi.hToolbarIcon = NULL;

		Bitmap^ bm = cmd->ToolbarIcon;
		if (bm != nullptr)
		{
			tbi.hToolbarBmp = (HBITMAP)bm->GetHbitmap().ToInt32();

			::SendMessageW(_nppHandle, NPPM_ADDTOOLBARICON, GetPluginCommandId(cmd), (LPARAM)&tbi);
		}
	}

	void NppInterface::OnTbModification()
	{
		RegisterToolbarIcons(this, gcnew EventArgs());
	}

	int NppInterface::GetPluginCommandId(PluginCommand^ cmd)
	{
		int index = cmd->Index;
		if (index >= 0 && index < _numFuncItems) return _funcItems[index]._cmdID;
		return 0;
	}
}
