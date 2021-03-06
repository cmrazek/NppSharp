#pragma once

using namespace System::Windows::Forms;
using namespace System::Drawing;

namespace NppSharp
{
	class OutputWindow;
	class DockWnd;
	ref class DockWindow;
	ref class LexerInfo;

	public ref class NppInterface : public INpp
	{
	public:
		NppInterface(HWND hNppHandle, HWND hScHandle1, HWND hScHandle2);
		virtual ~NppInterface();

		virtual property NativeWindow^ Window { NativeWindow^ get(); }
		virtual property NativeWindow^ EditorWindow1 { NativeWindow^ get(); }
		virtual property NativeWindow^ EditorWindow2 { NativeWindow^ get(); }
		virtual property NativeWindow^ EditorWindow { NativeWindow^ get(); }
		virtual void FocusEditor();
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
		virtual IEnumerable<String^>^ GetFileNames(EditorView view);
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
		virtual String^ GetText(TextLocation start, int numChars);
		virtual String^ GetText(TextLocation start, TextLocation end);
		String^ GetText(int startOffset, int lengthBytes);
		virtual void Append(String^ text);
		virtual void ClearAll();
		virtual void Cut();
		virtual void Copy();
		virtual void Copy(TextLocation start, int numChars);
		virtual void Copy(TextLocation start, TextLocation end);
		virtual void Copy(String^ text);
		virtual void CopyAllowLine();
		virtual void Paste();
		virtual void Clear();
		virtual property int Length { int get(); }
		virtual property int FirstVisibleLine { int get(); void set(int); }
		virtual property int LinesOnScreen { int get(); }
		virtual property bool FileModified { bool get(); }
		virtual void SetSelection(TextLocation anchorPos, TextLocation currentPos);
		virtual void GoToPos(TextLocation pos);
		virtual void GoToLine(int line);
		virtual property TextLocation CurrentPos { TextLocation get(); void set(TextLocation); }
		virtual property int CurrentLine { int get(); void set(int); }
		virtual property TextLocation AnchorPos { TextLocation get(); void set(TextLocation); }
		virtual property TextLocation SelectionStart { TextLocation get(); void set(TextLocation); }
		virtual property TextLocation SelectionEnd { TextLocation get(); void set(TextLocation); }
		virtual void SetEmptySelection(TextLocation pos);
		virtual void SelectAll();
		virtual int GetLineFromPos(int pos);
		virtual int GetLineStartPos(int line);
		virtual int GetLineEndPos(int line);
		virtual int GetLineLength(int line);
		virtual property String^ SelectedText { String^ get(); void set(String^); }
		virtual property NppSharp::SelectionMode SelectionMode { NppSharp::SelectionMode get(); }
		virtual void MoveCaretInsideView();
		virtual TextLocation GetWordEndPos(TextLocation pos, bool onlyWordChars);
		virtual TextLocation GetWordStartPos(TextLocation pos, bool onlyWordChars);
		virtual int GetColumn(int pos);
		virtual int FindColumn(int line, int column);
		virtual int PointToPos(Point pt);
		virtual int PointToPosClose(Point pt);
		virtual Point PosToPoint(int pos);
		virtual void MoveSelectedLinesUp();
		virtual void MoveSelectedLinesDown();
		virtual int TextLocationToOffset(TextLocation tl);
		virtual TextLocation OffsetToTextLocation(int offset);
		virtual int MoveOffsetByChars(int offset, int numChars);
		virtual int GetLineState(int line);
		virtual property unsigned int CurrentBufferId { unsigned int get(); }

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
		void	OnCharAdded(int ch);
		void	OnDoubleClick(int pos, bool ctrl, bool alt, bool shift);
		void	OnModified(npp::SCNotification *pNotify);
		void	OnUpdateUI(npp::SCNotification *pNotify);

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
		virtual event CharAddedEventHandler^		CharAdded;
		virtual event DoubleClickEventHandler^		DoubleClick;
		virtual event ModifiedEventHandler^			Modification;
		virtual event NppEventHandler^				SelectionChanged;
		virtual event NppEventHandler^				ScrolledVertically;
		virtual event NppEventHandler^				ScrolledHorizontally;

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

		// Lexer functions
		virtual int		AddLexer(Type^ lexerType, String^ name, String^ description, String^ blockCommentStart, String^ blockCommentEnd, String^ lineComment);
		virtual void	RefreshCustomLexers();
		int				GetLexerCount();
		String^			GetLexerName(int index);
		String^			GetLexerDescription(int index);
		npp::ILexer*	GetLexer(int index);
		void			GenerateLexerConfigFile();
		void			LoadOldLexerConfigFile();
		String^			CleanLexerExtension(String^ ext);
		String^			BuildExtensionList(IEnumerable<String^>^ extList);
		String^			ColorToWebHex(Color color);
		Color			WebHexToColor(String^ str);

		// AutoCompletion
		virtual void					ShowAutoCompletion(int lengthEntered, IEnumerable<String^>^ list, bool ignoreCase);
		virtual void					CancelAutoCompletion();
		virtual property bool			AutoCompletionIsActive { bool get(); }
		virtual void					ShowFunctionSignature(TextLocation location, String^ funcSignature);
		virtual void					SetFunctionSignatureHighlight(int startIndex, int length);
		virtual void					CancelFunctionSignature();
		virtual property bool			FunctionSignatureIsActive { bool get(); }
		virtual property TextLocation	FunctionSignatureLocation { TextLocation get(); }

	private:
		String^	GetFileNameByBufferId(unsigned int bufferId);
		void	DockWindow_Shutdown();
		HBITMAP	MakeCompatibleBitmap(HBITMAP hUserBitmap);
		void	CreateCommandMenus();
		HMENU	GetCommandMenu(String^ menuName, String^ insertBefore, bool &newMenuOut);
		HMENU	FindSubMenu(HMENU hParentMenu, String^ subMenuName);
		HMENU	FindNppSharpMenu();

		HWND								_nppHandle;
		HWND								_scHandle1;
		HWND								_scHandle2;
		HWND								_scHandle;
		int									_currentScView;
		NativeWindow^						_nppWindow;
		NativeWindow^						_scWindow1;
		NativeWindow^						_scWindow2;
		NativeWindow^						_scWindow;
		OutputWindow*						_outputWindow;
		int									_outputWindowCmdIndex;
		List<PluginCommand^>^				_commands;
		String^								_curFileName;
		Dictionary<unsigned int, String^>^	_bufferIds;
		npp::FuncItem*						_funcItems;
		int									_numFuncItems;
		List<DockWindow^>^					_dockWindows;
		List<LexerInfo^>^					_lexers;
		bool								_lexerFileCreated;
	};
}
