using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NppSharp
{
	/// <summary>
	/// Notepad++ C++/CLI interface.
	/// This interface is meant to be used internally by the plugin only.
	/// </summary>
	[System.Runtime.CompilerServices.CompilerGenerated]	// To stop it from appearing in the help file
	public interface INpp
	{
		#region Notepad++ Core
		/// <summary>
		/// Gets the Notepad++ main window.
		/// </summary>
		NativeWindow Window { get; }

		/// <summary>
		/// Gets the first editor window.
		/// </summary>
		NativeWindow EditorWindow1 { get; }

		/// <summary>
		/// Gets the second editor window.
		/// </summary>
		NativeWindow EditorWindow2 { get; }

		/// <summary>
		/// Gets the current editor window.
		/// </summary>
		NativeWindow EditorWindow { get; }

		/// <summary>
		/// Sets the focus to the current editor window.
		/// </summary>
		void FocusEditor();

		/// <summary>
		/// Gets the directory in which Notepad++.exe resides.
		/// </summary>
		string NppDir { get; }

		/// <summary>
		/// Gets the plugin configuration directory.
		/// This is the default directory from where scripts are run.
		/// </summary>
		string ConfigDir { get; }

		/// <summary>
		/// Adds a command to the plugin menu during the command registration phase.
		/// </summary>
		/// <param name="cmd">The command to be added.</param>
		void AddCommand(PluginCommand cmd);

		/// <summary>
		/// Allocates a new Notepad++ command ID.
		/// This does not need to be done for commands created with AddCommand(), as Notepad++
		/// takes care of that automatically.
		/// </summary>
		/// <returns>A new command ID.</returns>
		int AllocateCommandId();

		/// <summary>
		/// Adds a new toolbar icon.
		/// This can only be done during the toolbar icon registration phase.
		/// </summary>
		/// <param name="cmd">The command that will be executed when the toolbar icon is clicked.</param>
		void AddToolbarIcon(PluginCommand cmd);
		#endregion

		#region Events
		/// <summary>
		/// Triggered when Notepad++ needs the plugin to register the commands that should be
		/// available in the plugin menu.
		/// Commands are registered using the AddCommand function.
		/// </summary>
		event NppEventHandler GetCommands;

		/// <summary>
		/// Triggered when Notepad++ needs the plugin to register the commands that should be
		/// visible in the toolbar.
		/// Commands are registered using the AddToolbarIcon function.
		/// </summary>
		event NppEventHandler RegisterToolbarIcons;

		/// <summary>
		/// Triggered when the user executes a command.
		/// </summary>
		event ExecuteCommandEventHandler CommandExecuted;

		/// <summary>
		/// Triggered when Notepad++ has started up and is ready for processing.
		/// </summary>
		event NppEventHandler Ready;

		/// <summary>
		/// Triggered just before Notepad++ shuts down.
		/// </summary>
		event NppEventHandler Shutdown;

		/// <summary>
		/// Triggered before a file has started closing.
		/// </summary>
		event FileEventHandler FileClosing;

		/// <summary>
		/// Triggered after a file has closed.
		/// </summary>
		event FileEventHandler FileClosed;

		/// <summary>
		/// Triggered before a file has started opening.
		/// </summary>
		event FileEventHandler FileOpening;

		/// <summary>
		/// Triggered after a file has been opened.
		/// </summary>
		event FileEventHandler FileOpened;

		/// <summary>
		/// Triggered before a file has started saving.
		/// </summary>
		event FileEventHandler FileSaving;

		/// <summary>
		/// Triggered after a file has been successfully saved.
		/// </summary>
		event FileEventHandler FileSaved;

		/// <summary>
		/// Triggered when the currently visible file has changed (user clicked another file's tab).
		/// </summary>
		event FileEventHandler FileActivated;

		/// <summary>
		/// Triggered when the user changes the language for the current file.
		/// </summary>
		event LanguageTypeEventHandler LanguageChanged;

		/// <summary>
		/// Triggered when the user changes the word-styling settings.
		/// </summary>
		event FileEventHandler StyleUpdate;

		/// <summary>
		/// Triggered before a file has started loading.
		/// </summary>
		event NppEventHandler FileLoading;

		/// <summary>
		/// Triggered if a file cannot be loaded due to an error.
		/// </summary>
		event NppEventHandler FileLoadFailed;

		/// <summary>
		/// Triggered when the user reorders the file tabs.
		/// </summary>
		event FileEventHandler FileOrderChanged;

		/// <summary>
		/// Triggered when a character is added to a document.
		/// </summary>
		event CharAddedEventHandler CharAdded;

		/// <summary>
		/// Triggered when the user double-clicks inside a document.
		/// </summary>
		event DoubleClickEventHandler DoubleClick;

		/// <summary>
		/// Triggered when the active document is modified.
		/// </summary>
		event ModifiedEventHandler Modification;

		/// <summary>
		/// Triggered when the user changes the selection.
		/// </summary>
		event NppEventHandler SelectionChanged;

		/// <summary>
		/// Triggered when the user scrolls the view vertically.
		/// </summary>
		event NppEventHandler ScrolledVertically;

		/// <summary>
		/// Triggered when the user scrolls the view horizontally.
		/// </summary>
		event NppEventHandler ScrolledHorizontally;
		#endregion

		#region Editor
		/// <summary>
		/// Sent by the plugin to notify Notepad++ that a user-command that may require editor
		/// access is about to start executing.
		/// </summary>
		void OnCommandStart();

		/// <summary>
		/// Sent by the plugin to notify Notepad++ that a user-command that may require editor
		/// access has finished executing.
		/// </summary>
		void OnCommandEnd();

		/// <summary>
		/// Gets the current editor view (main or sub).
		/// </summary>
		EditorView CurrentView { get; }

		/// <summary>
		/// Gets or sets the current language ID.
		/// Note: languages for external lexers cannot be selected.
		/// </summary>
		int LanguageId { get; set; }

		/// <summary>
		/// Gets the name of the current language.
		/// </summary>
		string LanguageName { get; }

		/// <summary>
		/// Gets the name of a language, given the ID.
		/// </summary>
		/// <param name="langId">The language ID.</param>
		/// <returns>A string containing the language name.</returns>
		string GetLanguageName(int langId);

		/// <summary>
		/// Gets the full file name of the current document.
		/// </summary>
		string FileName { get; }

		/// <summary>
		/// Gets the number of open files.
		/// </summary>
		int FileCount { get; }

		/// <summary>
		/// Gets a complete list of file names for open files in both views.
		/// </summary>
		/// <remarks>To get a list of files open in a single view, you can use the GetFileNames method.</remarks>
		IEnumerable<string> FileNames { get; }

		/// <summary>
		/// Gets a list of open file names in the specified view.
		/// </summary>
		/// <param name="view">The editor view to retrieve the list of open file names.</param>
		/// <returns>A list of file names for the open files.</returns>
		/// <remarks>To get a list of all open file names in both views, you can use the 'FileNames' property.</remarks>
		IEnumerable<string> GetFileNames(EditorView view);

		/// <summary>
		/// Opens a new file.  If the file is already open, it will be made active.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be opened.</param>
		/// <returns>True if the file could be opened successfully; false otherwise.</returns>
		bool OpenFile(string fileName);

		/// <summary>
		/// Gets or sets the current document index within the current view.
		/// </summary>
		int ActiveFileIndex { get; set; }

		/// <summary>
		/// Gets the current document index in the specified view.
		/// </summary>
		/// <param name="view">The view in which the active file index will be retrieved.</param>
		/// <returns>Zero-based index of the active file.</returns>
		int GetActiveFileIndex(EditorView view);

		/// <summary>
		/// Switches to the file in the specified view.
		/// </summary>
		/// <param name="view">The view in which the file is to be switched.</param>
		/// <param name="index">Zero-based index of the view to switch to.</param>
		void SetActiveFileIndex(EditorView view, int index);

		/// <summary>
		/// Reloads the current file.
		/// <param name="withAlert">If true, Notepad++ will show an alert box will be displayed.</param>
		/// </summary>
		void ReloadFile(bool withAlert);

		/// <summary>
		/// Reloads a file given the file name.  If the file is not already open, then this has no effect.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be reloaded.</param>
		/// <param name="withAlert">If true, Notepad++ will show an alert box will be displayed.</param>
		void ReloadFile(string fileName, bool withAlert);

		/// <summary>
		/// Switches to a file given the file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be brought current.</param>
		/// <returns>True if the file could be switched to; false otherwise.</returns>
		bool SwitchToFile(string fileName);

		/// <summary>
		/// Saves the current document.
		/// </summary>
		/// <returns>True if successful, otherwise false.</returns>
		bool SaveFile();

		/// <summary>
		/// Saves the current document to another file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be written.</param>
		/// <returns>True if successful, otherwise false.</returns>
		bool SaveFileAs(string fileName);

		/// <summary>
		/// Saves a copy of the current document to another file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be written.</param>
		/// <returns>True if successful, otherwise false.</returns>
		bool SaveFileCopyAs(string fileName);

		/// <summary>
		/// Saves all open documents.
		/// </summary>
		/// <returns>True if successful, otherwise false.</returns>
		bool SaveAllFiles();

		/// <summary>
		/// Launches the 'Find in Files' dialog.
		/// </summary>
		/// <param name="dir">The directory to be searched.</param>
		/// <param name="filters">The file filters to be searched.</param>
		void LaunchFindInFiles(string dir, string filters);

		/// <summary>
		/// Triggers a menu item command.
		/// </summary>
		/// <param name="commandId">The ID of the command to be triggered.</param>
		void MenuCommand(int commandId);

		/// <summary>
		/// Gets the text for the specified line, optionally including line-end characters.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <param name="includeLineEndChars">If true, the returned string will contain the line-end characters.</param>
		/// <returns>The text for that line.</returns>
		string GetLineText(int line, bool includeLineEndChars);

		/// <summary>
		/// Gets the number of lines in the document.
		/// </summary>
		int LineCount { get; }

		/// <summary>
		/// Inserts text over the current selection.
		/// The caret is placed after the inserted text and scrolled into view.
		/// </summary>
		/// <param name="text">The text to be inserted.</param>
		void Insert(string text);

		/// <summary>
		/// Gets the text for the specified range.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="numChars">The number of characters to retrieve.</param>
		/// <returns>The text for the given range.</returns>
		string GetText(TextLocation start, int numChars);

		/// <summary>
		/// Gets the text for the specified range.
		/// </summary>
		/// <param name="start">The starting position</param>
		/// <param name="end">The ending position (exclusive)</param>
		/// <returns>The text for the given range.</returns>
		string GetText(TextLocation start, TextLocation end);

		/// <summary>
		/// Appends text to the end of the document without affecting the selection.
		/// </summary>
		/// <param name="text">The text to be appended.</param>
		void Append(string text);

		/// <summary>
		/// Clears all text out of the document.
		/// </summary>
		void ClearAll();

		/// <summary>
		/// Cuts the selected text to the clipboard.
		/// </summary>
		void Cut();

		/// <summary>
		/// Copies the selected text to the clipboard.
		/// </summary>
		void Copy();

		/// <summary>
		/// Copies the specified text range into the clipboard.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="numChars">The number of characters to be copied.</param>
		void Copy(TextLocation start, int numChars);

		/// <summary>
		/// Copies the specified text range into the clipboard.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="end">The ending position (exclusive).</param>
		void Copy(TextLocation start, TextLocation end);

		/// <summary>
		/// Copies the provided string into the clipboard.
		/// </summary>
		/// <param name="text">The text to be placed into to the clipboard.</param>
		void Copy(string text);

		/// <summary>
		/// Copies the selected text to the clipboard.
		/// If no text is selected, the current line is copied instead.
		/// </summary>
		void CopyAllowLine();

		/// <summary>
		/// Pastes the clipboard text over the selection.
		/// </summary>
		void Paste();

		/// <summary>
		/// Deletes the selected text.
		/// </summary>
		void Clear();

		/// <summary>
		/// Gets the length of the document.
		/// </summary>
		int Length { get; }

		/// <summary>
		/// Gets or sets the first visible line in the document.
		/// </summary>
		int FirstVisibleLine { get; set; }

		/// <summary>
		/// Gets the number of complete lines visible on the screen.
		/// </summary>
		int LinesOnScreen { get; }

		/// <summary>
		/// Gets a flag indicating if the document has been modified since the last save.
		/// </summary>
		bool FileModified { get; }

		/// <summary>
		/// Sets the selection range.
		/// (The caret is scrolled into view)
		/// </summary>
		/// <param name="anchorPos">The selection anchor position.</param>
		/// <param name="currentPos">The selection current position.</param>
		void SetSelection(TextLocation anchorPos, TextLocation currentPos);

		/// <summary>
		/// Goes to the specified position and ensures it is visible.
		/// </summary>
		/// <param name="pos">The position to go to.</param>
		void GoToPos(TextLocation pos);

		/// <summary>
		/// Goes to the specified line and ensures it is visible.
		/// </summary>
		/// <param name="line">The line to go to.</param>
		void GoToLine(int line);

		/// <summary>
		/// Gets or sets the current position.
		/// (The caret is not scrolled into view)
		/// </summary>
		TextLocation CurrentPos { get; set; }

		/// <summary>
		/// Gets or sets the current line.
		/// (The caret is not scrolled into view)
		/// </summary>
		int CurrentLine { get; set; }

		/// <summary>
		/// Gets or sets the selection anchor position.
		/// (The caret is not scrolled into view)
		/// </summary>
		TextLocation AnchorPos { get; set; }

		/// <summary>
		/// Gets or sets the selection start position.
		/// (The caret is not scrolled into view)
		/// </summary>
		TextLocation SelectionStart { get; set; }

		/// <summary>
		/// Gets or sets the selection end position.
		/// (The caret is not scrolled into view)
		/// </summary>
		TextLocation SelectionEnd { get; set; }

		/// <summary>
		/// Removes the selection and sets the caret at pos.
		/// (The caret is not scrolled into view)
		/// </summary>
		void SetEmptySelection(TextLocation pos);

		/// <summary>
		/// Selects all text in the document.
		/// (The caret is not scrolled into view)
		/// </summary>
		void SelectAll();

		/// <summary>
		/// Gets the line that contains the specified position.
		/// </summary>
		/// <param name="byteOffset">The zero-based position.</param>
		/// <returns>The one-based line number that contains the position.</returns>
		int GetLineFromPos(int byteOffset);

		/// <summary>
		/// Gets the starting position of the specified line.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The zero-based starting position of the line.</returns>
		int GetLineStartPos(int line);

		/// <summary>
		/// Gets the ending position of the specified line, before any line end characters.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The zero-based end position of the line.</returns>
		int GetLineEndPos(int line);

		/// <summary>
		/// Gets the length of the specified line.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The length of the line including any line end characters.</returns>
		int GetLineLength(int line);

		/// <summary>
		/// Gets or sets the selected text.
		/// When setting, the caret is placed after the inserted text and scrolled into view.
		/// </summary>
		string SelectedText { get; set; }

		/// <summary>
		/// Gets the selection mode.
		/// </summary>
		SelectionMode SelectionMode { get; }

		/// <summary>
		/// If the caret is outside the current view, it is moved to the nearest visible line.
		/// Any selection is lost.
		/// </summary>
		void MoveCaretInsideView();

		/// <summary>
		/// Gets the position at the end of the current word.
		/// </summary>
		/// <param name="pos">The starting position.</param>
		/// <param name="onlyWordChars">If true, only word characters will be jumped.
		/// If false, all characters will be jumped.</param>
		/// <returns>The position at the end of the word.</returns>
		TextLocation GetWordEndPos(TextLocation pos, bool onlyWordChars);

		/// <summary>
		/// Gets the position at the start of the current word.
		/// </summary>
		/// <param name="pos">The starting position.</param>
		/// <param name="onlyWordChars">If true, only word characters will be jumped.
		/// If false, all characters will be jumped.</param>
		/// <returns>The position at the start of the word.</returns>
		TextLocation GetWordStartPos(TextLocation pos, bool onlyWordChars);

		/// <summary>
		/// Gets the column number for the specified position.
		/// </summary>
		/// <param name="pos">The zero-based position.</param>
		/// <returns>The one-based column number.</returns>
		int GetColumn(int pos);

		/// <summary>
		/// Gets the position that corresponds to a line and column.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <param name="column">The one-based column number.</param>
		/// <returns>The zero-based position.</returns>
		int FindColumn(int line, int column);

		/// <summary>
		/// Finds the position closest to a point on the screen.
		/// </summary>
		/// <param name="pt">The client coordinates of the point to test.</param>
		/// <returns>The zero-based position of the closest character.</returns>
		int PointToPos(Point pt);

		/// <summary>
		/// Finds the position closest to a point on the screen.
		/// If no character is close, or the point it outside the window, it returns -1.
		/// </summary>
		/// <param name="pt">The client coordinates of the point to test.</param>
		/// <returns>-1 if not close to any character, otherwise the zero-based position of the
		/// closest character.</returns>
		int PointToPosClose(Point pt);

		/// <summary>
		/// Returns the x and y display location of the position.
		/// </summary>
		/// <param name="pos">The position to find.</param>
		/// <returns>The client coordinates of the text position.</returns>
		Point PosToPoint(int pos);

		/// <summary>
		/// Moves the selected lines up one line, shifting the line above the selection.
		/// </summary>
		void MoveSelectedLinesUp();

		/// <summary>
		/// Moves the selected lines down one line, shifting the line below the selection.
		/// </summary>
		void MoveSelectedLinesDown();

		/// <summary>
		/// Gets the offset for a text location.
		/// </summary>
		/// <param name="loc">The TextLocation to be converted.</param>
		/// <returns>The zero-based bytes offset for the location.</returns>
		int TextLocationToOffset(TextLocation loc);

		/// <summary>
		/// Gets the text location that refers to the byte offset.
		/// </summary>
		/// <param name="offset">The byte offset to be converted.</param>
		/// <returns>A TextLocation object.</returns>
		TextLocation OffsetToTextLocation(int offset);

		/// <summary>
		/// Increment/decrements an offset by a specified number of characters.
		/// </summary>
		/// <param name="offset">The originating byte-offset.</param>
		/// <param name="numChars">The number of characters to move.</param>
		/// <returns>The new byte-offset</returns>
		int MoveOffsetByChars(int offset, int numChars);

		/// <summary>
		/// Gets the lexer state of a line.
		/// </summary>
		/// <param name="line">The line number.</param>
		/// <returns>The state assigned by the lexer.</returns>
		int GetLineState(int line);

		/// <summary>
		/// Gets the buffer ID for the current document.
		/// </summary>
		uint CurrentBufferId { get; }
		#endregion

		#region Output Window
		/// <summary>
		/// Shows the output window.
		/// </summary>
		void ShowOutputWindow();

		/// <summary>
		/// Hides the output window.
		/// </summary>
		void HideOutputWindow();

		/// <summary>
		/// Gets a flag indicating if the output window is visible.
		/// </summary>
		bool OutputWindowVisible { get; }

		/// <summary>
		/// Writes text to the output window.
		/// </summary>
		/// <param name="text">The text to be written.</param>
		void WriteOutput(string text);

		/// <summary>
		/// Writes text followed by end-of-line to the output window.
		/// </summary>
		/// <param name="text">The text to be written.</param>
		void WriteOutputLine(string text);

		/// <summary>
		/// Gets or sets the output window text style.
		/// </summary>
		OutputStyle OutputStyle { get; set; }

		/// <summary>
		/// Sets the index of the 'Show Output Window' command.  This is required by Notepad++ to
		/// show the window the next time Notepad++ launches.
		/// </summary>
		/// <param name="cmdIndex">The zero-based index of the command.</param>
		void SetShowOutputWindowCommandIndex(int cmdIndex);

		/// <summary>
		/// Clears all text in the output window.
		/// </summary>
		void ClearOutputWindow();

		/// <summary>
		/// Scrolls the output window to the top position.
		/// </summary>
		void OutputWindowGoToTop();

		/// <summary>
		/// Scrolls the output window to the bottom position.
		/// </summary>
		void OutputWindowGoToBottom();

		/// <summary>
		/// Sets the text-style definition for an output style.
		/// </summary>
		/// <param name="osd">The text-style definition object.</param>
		/// <param name="defaultOsd">The fallback (normal) text-style definition.
		/// If null, hardcoded values will be used.</param>
		void SetOutputStyleDef(OutputStyleDef osd, OutputStyleDef defaultOsd);
		#endregion

		#region Dock Window
		/// <summary>
		/// Creates a docked window.
		/// </summary>
		/// <param name="window">The form/control handle.</param>
		/// <param name="title">The window title.</param>
		/// <param name="alignment">The alignment of the window.</param>
		/// <param name="tabIcon">The icon to appear when the window is tabbed because multiple docked windows exist on the same side.</param>
		/// <param name="id">An identifier for this docked window.
		/// Notepad++ tracks states for each window separately between sessions based on this ID.</param>
		IDockWindow CreateDockWindow(IWin32Window window, string title, DockWindowAlignment alignment, Icon tabIcon, int id);

		/// <summary>
		/// Gets the dock window object for the specified ID.
		/// </summary>
		/// <param name="id">The ID number for the dock window object.</param>
		/// <returns>If the dock window object could be found, the object is returned; otherwise null.</returns>
		IDockWindow GetDockWindow(int id);
		#endregion

		#region Lexers
		/// <summary>
		/// Adds a NppSharp lexer object.
		/// </summary>
		/// <param name="lexerType">The class type for the lexer.</param>
		/// <param name="name">The name of the lexer.</param>
		/// <param name="description">The description of the lexer to appear in the Notepad++ status bar.</param>
		/// <param name="blockCommentStart">The text that starts a block comment.</param>
		/// <param name="blockCommentEnd">The text that ends a block comment.</param>
		/// <param name="lineComment">The text that starts a line comment.</param>
		/// <returns>The zero-based index of the lexer object.</returns>
		int AddLexer(Type lexerType, string name, string description, string blockCommentStart, string blockCommentEnd, string lineComment);

		/// <summary>
		/// Refreshes the word-styles and folding on documents that use a custom lexer provided via NppSharp.
		/// </summary>
		void RefreshCustomLexers();
		#endregion

		#region AutoCompletion
		/// <summary>
		/// Shows the AutoCompletion list.
		/// </summary>
		/// <param name="lengthEntered">The number of characters already entered by the user.</param>
		/// <param name="list">The list of words.</param>
		/// <param name="ignoreCase">Ignore case?</param>
		void ShowAutoCompletion(int lengthEntered, IEnumerable<string> list, bool ignoreCase);

		/// <summary>
		/// Cancels any action auto-completion.
		/// </summary>
		void CancelAutoCompletion();

		/// <summary>
		/// Gets a value indicating if the autocompletion list is currently active.
		/// </summary>
		bool AutoCompletionIsActive { get; }

		/// <summary>
		/// Displays the function signature help UI.
		/// </summary>
		/// <param name="location">The location where the function signature box should appear (typically appears on the line below)</param>
		/// <param name="funcSignature">The function signature text to be displayed.</param>
		void ShowFunctionSignature(TextLocation location, string funcSignature);

		/// <summary>
		/// Sets the portion of the function signature text that will be highlighted.
		/// </summary>
		/// <param name="startIndex">The starting index (zero based)</param>
		/// <param name="length">Number of characters to highlight.</param>
		void SetFunctionSignatureHighlight(int startIndex, int length);

		/// <summary>
		/// Hides the function signature help UI.
		/// </summary>
		void CancelFunctionSignature();

		/// <summary>
		/// Gets a flag indicating if the function signature UI is currently visible.
		/// </summary>
		bool FunctionSignatureIsActive { get; }

		/// <summary>
		/// Gets the starting location for the function signature UI.
		/// </summary>
		/// <remarks>This is passed in as the 'location' argument in ShowFunctionSignature().</remarks>
		TextLocation FunctionSignatureLocation { get; }
		#endregion
	}

	#region Event Handlers
	/// <summary>
	/// Basic event handler.
	/// </summary>
	/// <param name="sender">The object that is sending the event.</param>
	/// <param name="e">Event arguments.</param>
	public delegate void NppEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Handler for the Execute Command event.
	/// </summary>
	/// <param name="sender">Object that sends the event.</param>
	/// <param name="e">Arguments provided for this event.</param>
	[System.Runtime.CompilerServices.CompilerGenerated]	// To stop it from appearing in the help file
	public delegate void ExecuteCommandEventHandler(object sender, ExecuteCommandEventArgs e);

	/// <summary>
	/// Arguments required for the ExecuteCommand event.
	/// </summary>
	[System.Runtime.CompilerServices.CompilerGenerated]	// To stop it from appearing in the help file
	public class ExecuteCommandEventArgs : EventArgs
	{
		private PluginCommand _cmd = null;

		/// <summary>
		/// Creates the event arguments object.
		/// </summary>
		/// <param name="cmd">The plugin command to be executed.</param>
		public ExecuteCommandEventArgs(PluginCommand cmd)
		{
			_cmd = cmd;
		}

		/// <summary>
		/// Gets the plugin command object.
		/// </summary>
		public PluginCommand Command
		{
			get { return _cmd; }
		}
	}

	/// <summary>
	/// Handler for file-related events.
	/// </summary>
	/// <param name="sender">Object that sends the event.</param>
	/// <param name="e">Arguments provided for this event.</param>
	public delegate void FileEventHandler(object sender, FileEventArgs e);

	/// <summary>
	/// Arguments required for file-related events.
	/// </summary>
	public class FileEventArgs : EventArgs
	{
		private string _fileName = "";
		private uint _bufferId = 0;

		/// <summary>
		/// Creates the event arguments object.
		/// </summary>
		/// <param name="bufferId">The Notepad++ buffer ID.</param>
		/// <param name="fileName">The name of the file affected.</param>
		public FileEventArgs(uint bufferId, string fileName)
		{
			_bufferId = bufferId;
			_fileName = fileName;
		}

		/// <summary>
		/// Gets the name of the affected file.
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
		}

		/// <summary>
		/// Gets the Notepad++ buffer ID.
		/// </summary>
		public uint BufferId
		{
			get { return _bufferId; }
		}
	}

	/// <summary>
	/// Handler for language-related events.
	/// </summary>
	/// <param name="sender">Object that sends the event.</param>
	/// <param name="e">Arguments provided for this event.</param>
	public delegate void LanguageTypeEventHandler(object sender, LanguageTypeEventArgs e);

	/// <summary>
	/// Arguments required for language-related events.
	/// </summary>
	public class LanguageTypeEventArgs : EventArgs
	{
		private string _fileName = "";
		private uint _bufferId = 0;
		private int _langId = 0;

		/// <summary>
		/// Creates the event arguments object.
		/// </summary>
		/// <param name="bufferId">The Notepad++ buffer ID.</param>
		/// <param name="fileName">The name of the file affected.</param>
		/// <param name="langId">The Notepad++ language ID.</param>
		public LanguageTypeEventArgs(uint bufferId, string fileName, int langId)
		{
			_bufferId = bufferId;
			_fileName = fileName;
			_langId = langId;
		}

		/// <summary>
		/// Gets the name of the affected file.
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
		}

		/// <summary>
		/// Gets the Notepad++ buffer ID.
		/// </summary>
		public uint BufferId
		{
			get { return _bufferId; }
		}

		/// <summary>
		/// Gets the Notepad++ language ID.
		/// </summary>
		public int LanguageId
		{
			get { return _langId; }
		}
	}

	/// <summary>
	/// Handler for char-added events.
	/// </summary>
	/// <param name="sender">Object that sends the event</param>
	/// <param name="e">Arguments provided for this event.</param>
	public delegate void CharAddedEventHandler(object sender, CharAddedEventArgs e);

	/// <summary>
	/// Arguments for char-added events.
	/// </summary>
	public class CharAddedEventArgs : EventArgs
	{
		/// <summary>
		/// Creates the event arguments object.
		/// </summary>
		/// <param name="ch">The character added.</param>
		public CharAddedEventArgs(char ch)
		{
			Character = ch;
		}

		/// <summary>
		/// Gets the character added.
		/// </summary>
		public char Character { get; private set; }
	}

	/// <summary>
	/// Handler for double-click events.
	/// </summary>
	/// <param name="sender">Object that sends the event</param>
	/// <param name="e">Arguments provided for this event.</param>
	public delegate void DoubleClickEventHandler(object sender, DoubleClickEventArgs e);

	/// <summary>
	/// Arguments for double-click events.
	/// </summary>
	public class DoubleClickEventArgs : EventArgs
	{
		/// <summary>
		/// Creates the event arguments object.
		/// </summary>
		/// <param name="pos">The position where the double-click occurred</param>
		/// <param name="ctrl">A flag indicating if the control key was pressed during the double-click.</param>
		/// <param name="alt">A flag indicating if the alt key was pressed during the double-click.</param>
		/// <param name="shift">A flag indicating if the shift key was pressed during the double-click.</param>
		public DoubleClickEventArgs(int pos, bool ctrl, bool alt, bool shift)
		{
			Position = pos;
			Control = ctrl;
			Alt = alt;
			Shift = shift;
		}

		/// <summary>
		/// The document position where the double-click occurred.
		/// </summary>
		public int Position { get; private set; }

		/// <summary>
		/// Gets a value indicating if the control key was pressed during the double-click.
		/// </summary>
		public bool Control { get; private set; }

		/// <summary>
		/// Gets a value indicating if the alt key was pressed during the double-click.
		/// </summary>
		public bool Alt { get; private set; }

		/// <summary>
		/// Gets a value indicating if the shift key was pressed during the double-click.
		/// </summary>
		public bool Shift { get; private set; }
	}

	/// <summary>
	/// A type of document modification performed.
	/// </summary>
	public enum ModificationType
	{
		/// <summary>
		/// Text was inserted into the document.
		/// </summary>
		Insert,

		/// <summary>
		/// Text was deleted from the document.
		/// </summary>
		Delete
	}

	/// <summary>
	/// Handler for modified events.
	/// </summary>
	/// <param name="sender">Object that sends the event.</param>
	/// <param name="e">Arguments provided for this event.</param>
	public delegate void ModifiedEventHandler(object sender, ModifiedEventArgs e);

	/// <summary>
	/// Arguments for modified events.
	/// </summary>
	public class ModifiedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the type of modification that occurred.
		/// </summary>
		public ModificationType ModificationType { get; private set; }

		/// <summary>
		/// Gets the text inserted or deleted.
		/// </summary>
		public TextPtr Text { get; private set; }

		/// <summary>
		/// Gets the location of the modification.
		/// </summary>
		public TextLocation Location { get; private set; }

		/// <summary>
		/// Gets a value indicating if this action was caused by the user.
		/// </summary>
		public bool UserAction { get; private set; }

		/// <summary>
		/// Gets a value indicating if this action is part of an undo process.
		/// </summary>
		public bool Undo { get; private set; }

		/// <summary>
		/// Gets a value indicating if this action is part of a redo process.
		/// </summary>
		public bool Redo { get; private set; }

		/// <summary>
		/// Gets the number of lines added by this modification.  For deletions, this will be a negative value.
		/// </summary>
		public int LinesAdded { get; private set; }

		/// <summary>
		/// Creates the event arguments object.
		/// </summary>
		/// <param name="type">The type of modification that occurred.</param>
		/// <param name="text">The text inserted or deleted.</param>
		/// <param name="location">The location of the modification.</param>
		/// <param name="userAction">A flag indicating if this action was caused by the user.</param>
		/// <param name="undo">A flag indicating if this action is part of an undo process.</param>
		/// <param name="redo">A flag indicating if this action is part of a redo process.</param>
		/// <param name="linesAdded">The number of lines added by this modification.</param>
		public ModifiedEventArgs(ModificationType type, TextPtr text, TextLocation location, bool userAction, bool undo, bool redo, int linesAdded)
		{
			ModificationType = type;
			Text = text;
			Location = location;
			UserAction = userAction;
			Undo = undo;
			Redo = redo;
			LinesAdded = linesAdded;
		}
	}
	#endregion

	/// <summary>
	/// Specifies the default docking window alignment.
	/// </summary>
	public enum DockWindowAlignment : uint
	{
		/// <summary>
		/// Default docking on left
		/// </summary>
		Left,

		/// <summary>
		/// Default docking on right
		/// </summary>
		Right,

		/// <summary>
		/// Default docking on top
		/// </summary>
		Top,

		/// <summary>
		/// Default docking on bottom
		/// </summary>
		Bottom,

		/// <summary>
		/// Default state is floating
		/// </summary>
		Floating
	}
}
