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

namespace NppSharp
{
	class OutputWindow;
	class DockWnd;
	ref class DockWindow;

	public ref class NppInterface : public INpp
	{
	public:
		NppInterface(HWND hNppHandle, HWND hScHandle1, HWND hScHandle2);
		virtual ~NppInterface();

		virtual property NativeWindow^ Window { NativeWindow^ get(); }
		virtual property String^ NppDir { String^ get(); }
		virtual property String^ ConfigDir { String^ get(); }

		property HWND NppHandle { HWND get() { return _nppHandle; } }
		
		virtual void OnCommandStart();
		virtual void OnCommandEnd();

		virtual property EditorView CurrentView { EditorView get(); }
		virtual property int LanguageId { int get(); void set(int); }
		virtual property String^ LanguageName { String^ get(); }
		virtual String^ GetLanguageName(int langId);
		virtual property String^ FileName { String^ get(); }
		virtual property int FileCount { int get(); }
		virtual property IEnumerable<String^>^ FileNames { IEnumerable<String^>^ get(); }
		virtual bool OpenFile(String^ fileName);
		virtual property int ActiveFileIndex { int get(); void set(int); }
		virtual int GetActiveFileIndex(EditorView view);
		virtual void SetActiveFileIndex(EditorView view, int index);
		virtual void ReloadFile(bool withAlert);
		virtual void ReloadFile(String^ fileName, bool withAlert);
		virtual bool SwitchToFile(String^ fileName);
		virtual bool SaveFile();
		virtual bool SaveFileAs(String^ fileName);
		virtual bool SaveFileCopyAs(String^ fileName);
		virtual bool SaveAllFiles();
		virtual void LaunchFindInFiles(String^ dirs, String^ filters);
		virtual void MenuCommand(int commandId);
		virtual String^ GetLineText(int line, bool includeLineEndChars);
		virtual property int LineCount { int get(); }
		virtual void Insert(String^ text);
		virtual String^ GetText(int startPos, int length);
		virtual void Append(String^ text);
		virtual void ClearAll();
		virtual void Cut();
		virtual void Copy();
		virtual void Copy(int startPos, int length);
		virtual void Copy(String^ text);
		virtual void CopyAllowLine();
		virtual void Paste();
		virtual void Clear();
		virtual property int Length { int get(); }
		virtual property int FirstVisibleLine { int get(); void set(int); }
		virtual property int LinesOnScreen { int get(); }
		virtual property bool FileModified { bool get(); }
		virtual void SetSelection(int anchorPos, int currentPos);
		virtual void GoToPos(int pos);
		virtual void GoToLine(int line);
		virtual property int CurrentPos { int get(); void set(int); }
		virtual property int CurrentLine { int get(); void set(int); }
		virtual property int AnchorPos { int get(); void set(int); }
		virtual property int SelectionStart { int get(); void set(int); }
		virtual property int SelectionEnd { int get(); void set(int); }
		virtual void SetEmptySelection(int pos);
		virtual void SelectAll();
		virtual int GetLineFromPos(int pos);
		virtual int GetLineStartPos(int line);
		virtual int GetLineEndPos(int line);
		virtual int GetLineLength(int line);
		virtual property String^ SelectedText { String^ get(); void set(String^); }
		virtual property NppSharp::SelectionMode SelectionMode { NppSharp::SelectionMode get(); }	//void set(NppSharp::SelectionMode); }
		virtual void MoveCaretInsideView();
		virtual int GetWordEndPos(int pos, bool onlyWordChars);
		virtual int GetWordStartPos(int pos, bool onlyWordChars);
		virtual int GetColumn(int pos);
		virtual int FindColumn(int line, int column);
		virtual int PointToPos(Point pt);
		virtual int PointToPosClose(Point pt);
		virtual Point PosToPoint(int pos);
		virtual void MoveSelectedLinesUp();
		virtual void MoveSelectedLinesDown();

		void	OnReady();
		void	OnShutdown();
		void	OnFileClosing(unsigned int bufferId);
		void	OnFileClosed(unsigned int bufferId);
		void	OnFileOpening(unsigned int bufferId);
		void	OnFileOpened(unsigned int bufferId);
		void	OnFileSaving(unsigned int bufferId);
		void	OnFileSaved(unsigned int bufferId);
		void	OnFileActivated(unsigned int bufferId);
		void	OnLanguageChanged(unsigned int bufferId);
		void	OnStyleUpdate(unsigned int bufferId);
		void	OnFileLoading();
		void	OnFileLoadFailed();
		void	OnFileOrderChanged(unsigned int bufferId);

		virtual event NppEventHandler^				GetCommands;
		virtual event NppEventHandler^				RegisterToolbarIcons;
		virtual event NppEventHandler^				Ready;
		virtual event NppEventHandler^				Shutdown;
		virtual event FileEventHandler^				FileClosing;
		virtual event FileEventHandler^				FileClosed;
		virtual event FileEventHandler^				FileOpening;
		virtual event FileEventHandler^				FileOpened;
		virtual event FileEventHandler^				FileSaving;
		virtual event FileEventHandler^				FileSaved;
		virtual event FileEventHandler^				FileActivated;
		virtual event LanguageTypeEventHandler^		LanguageChanged;
		virtual event FileEventHandler^				StyleUpdate;
		virtual event NppEventHandler^				FileLoading;
		virtual event NppEventHandler^				FileLoadFailed;
		virtual event FileEventHandler^				FileOrderChanged;
		virtual event ExecuteCommandEventHandler^	CommandExecuted;

		virtual void ShowOutputWindow();
		virtual void HideOutputWindow();
		virtual property bool OutputWindowVisible { bool get(); }
		virtual void WriteOutput(String^ text);
		virtual void WriteOutputLine(String^ text);
		virtual property NppSharp::OutputStyle OutputStyle { NppSharp::OutputStyle get(); void set(NppSharp::OutputStyle); }
		virtual void SetShowOutputWindowCommandIndex(int cmdIndex);
		virtual void ClearOutputWindow();
		virtual void OutputWindowGoToTop();
		virtual void OutputWindowGoToBottom();
		virtual void SetOutputStyleDef(OutputStyleDef ^osd, OutputStyleDef^ defaultOsd);

		List<PluginCommand^>^ GetCommandList();
		virtual void AddCommand(PluginCommand^ cmd);
		void ExecuteCommandByIndex(int cmdIndex);
		void ExecuteCommandById(int cmdId);
		virtual int AllocateCommandId();

		virtual void AddToolbarIcon(PluginCommand^ cmd);
		void OnTbModification();
		void SetFuncItems(npp::FuncItem* funcItems, int numFuncItems) { _funcItems = funcItems; _numFuncItems = numFuncItems; }
		int GetPluginCommandId(PluginCommand^ cmd);

		virtual IDockWindow^ CreateDockWindow(IWin32Window^ window, String^ title, DockWindowAlignment alignment, Icon^ tabIcon, int id);
		virtual IDockWindow^ GetDockWindow(int id);

		virtual int AddLexer(ILexer^ lexer);
		int GetLexerCount();
		String^ GetLexerName(int index);
		String^ GetLexerDescription(int index);
		npp::ILexer* GetLexer(int index);
		

	private:
		bool	ClampTextRange(int *pStartPos, int *pLength);
		String^	GetFileNameByBufferId(unsigned int bufferId);
		void	DockWindow_Shutdown();

		HWND								_nppHandle;
		HWND								_scHandle1;
		HWND								_scHandle2;
		HWND								_scHandle;
		int									_currentScView;
		NativeWindow^						_nppWindow;
		OutputWindow*						_outputWindow;
		int									_outputWindowCmdIndex;
		List<PluginCommand^>^				_commands;
		String^								_curFileName;
		Dictionary<unsigned int, String^>^	_bufferIds;
		npp::FuncItem*						_funcItems;
		int									_numFuncItems;
		List<DockWindow^>^					_dockWindows;
		List<IntPtr>						_lexers;	// Points to LexerWrapper object.
	};
}