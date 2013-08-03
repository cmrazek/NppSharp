using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NppSharp
{
	/// <summary>
	/// Base class for all script objects.
	/// Any objects that wish to have their public methods made available as commands
	/// must inherit from this class.
	/// </summary>
	public abstract class NppScript
	{
		private string _fileName = "";

		#region Notepad++
		/// <summary>
		/// Gets the output window object.
		/// </summary>
		public OutputView Output
		{
			get { return Plugin.Output; }
		}

		/// <summary>
		/// Displays an error message.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		public void ShowError(string message)
		{
			Plugin.ShowError(message);
		}

		/// <summary>
		/// Displays an error message caused by an exception.
		/// </summary>
		/// <param name="ex">The exception that caused the error.</param>
		public void ShowError(Exception ex)
		{
			Plugin.ShowError(ex);
		}

		/// <summary>
		/// Displays an error message caused by an exception, with additional text.
		/// </summary>
		/// <param name="ex">The exception that caused the error (will be displayed in details tab).</param>
		/// <param name="message">Additional text to be displayed in message tab.</param>
		public void ShowError(Exception ex, string message)
		{
			Plugin.ShowError(ex, message);
		}

		/// <summary>
		/// Gets the name of a language, given the ID.
		/// </summary>
		/// <param name="langId">The language ID.</param>
		/// <returns>A string containing the language name.</returns>
		public string GetLanguageName(int langId)
		{
			return Plugin.NppIntf.GetLanguageName(langId);
		}
		
		/// <summary>
		/// Gets the directory where the Notepad++ executable resides.
		/// </summary>
		public string NppDirectory
		{
			get { return Plugin.NppIntf.NppDir; }
		}

		/// <summary>
		/// Gets the Notepad++ configuration directory in 'appdata'.
		/// </summary>
		public string ConfigDirectory
		{
			get { return Plugin.NppIntf.ConfigDir; }
		}

		/// <summary>
		/// Launches the 'Find in Files' dialog.
		/// </summary>
		/// <param name="dir">The directory to be searched.</param>
		/// <param name="filters">The file filters to be searched.</param>
		public void LaunchFindInFiles(string dir, string filters)
		{
			Plugin.NppIntf.LaunchFindInFiles(dir, filters);
		}

		/// <summary>
		/// Triggers a menu item command.
		/// </summary>
		/// <param name="command">The ID of the command to be triggered.</param>
		public void MenuCommand(MenuCommand command)
		{
			Plugin.NppIntf.MenuCommand((int)command);
		}

		/// <summary>
		/// Triggers a menu item command.
		/// </summary>
		/// <param name="commandId">The ID of the command to be triggered.</param>
		public void MenuCommand(int commandId)
		{
			Plugin.NppIntf.MenuCommand(commandId);
		}

		/// <summary>
		/// Gets the filename of this script or assembly.
		/// </summary>
		/// <remarks>This property is not available in the constructor as it must be set after the object has been instantiated.</remarks>
		public string ScriptFileName
		{
			get { return _fileName; }
			internal set { _fileName = value; }
		}

		/// <summary>
		/// Gets the directory in which this script resides.
		/// </summary>
		/// <remarks>This property is not available in the constructor as it must be set after the object has been instantiated.</remarks>
		public string ScriptDirectory
		{
			get { return Path.GetDirectoryName(_fileName); }
		}

		/// <summary>
		/// Gets the Notepad++ main window.
		/// </summary>
		public NativeWindow NppWindow
		{
			get { return Plugin.NppIntf.Window; }
		}

		/// <summary>
		/// Gets the first editor window.
		/// </summary>
		public NativeWindow EditorWindow1
		{
			get { return Plugin.NppIntf.EditorWindow1; }
		}

		/// <summary>
		/// Gets the second editor window.
		/// </summary>
		public NativeWindow EditorWindow2
		{
			get { return Plugin.NppIntf.EditorWindow2; }
		}

		/// <summary>
		/// Gets the current editor window.
		/// </summary>
		public NativeWindow EditorWindow
		{
			get { return Plugin.NppIntf.EditorWindow; }
		}

		/// <summary>
		/// Sets the focus to the current editor window.
		/// </summary>
		public void FocusEditor()
		{
			Plugin.NppIntf.FocusEditor();
		}
		#endregion

		#region File Handling
		/// <summary>
		/// Gets the number of open files.
		/// </summary>
		public int FileCount
		{
			get { return Plugin.NppIntf.FileCount; }
		}

		/// <summary>
		/// Gets a complete list of file names for open files in both views.
		/// </summary>
		/// <remarks>To get a list of files open in a single view, you can use the GetFileNames method.</remarks>
		public IEnumerable<string> FileNames
		{
			get { return Plugin.NppIntf.FileNames; }
		}

		/// <summary>
		/// Gets a list of open file names in the specified view.
		/// </summary>
		/// <param name="view">The editor view to retrieve the list of open file names.</param>
		/// <returns>A list of file names for the open files.</returns>
		/// <remarks>To get a list of all open file names in both views, you can use the 'FileNames' property.</remarks>
		public IEnumerable<string> GetFileNames(EditorView view)
		{
			return Plugin.NppIntf.GetFileNames(view);
		}

		/// <summary>
		/// Opens a new file.  If the file is already open, it will be made active.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be opened.</param>
		/// <returns>True if the file could be opened successfully; false otherwise.</returns>
		public bool OpenFile(string fileName)
		{
			return Plugin.NppIntf.OpenFile(fileName);
		}

		/// <summary>
		/// Gets the full file name of the current document.
		/// </summary>
		public string ActiveFileName
		{
			get { return Plugin.NppIntf.FileName; }
		}

		/// <summary>
		/// Gets or sets the current document index within the current view.
		/// </summary>
		public int ActiveFileIndex
		{
			get { return Plugin.NppIntf.ActiveFileIndex; }
			set { Plugin.NppIntf.ActiveFileIndex = value; }
		}

		/// <summary>
		/// Gets the current document index in the specified view.
		/// </summary>
		/// <param name="view">The view in which the active file index will be retrieved.</param>
		/// <returns>Zero-based index of the active file.</returns>
		public int GetActiveFileIndex(EditorView view)
		{
			return Plugin.NppIntf.GetActiveFileIndex(view);
		}

		/// <summary>
		/// Switches to the file in the specified view.
		/// </summary>
		/// <param name="view">The view in which the file is to be switched.</param>
		/// <param name="index">Zero-based index of the view to switch to.</param>
		public void SetActiveFileIndex(EditorView view, int index)
		{
			Plugin.NppIntf.SetActiveFileIndex(view, index);
		}

		/// <summary>
		/// Reloads the current file.
		/// </summary>
		/// <param name="withAlert">If true, Notepad++ will show an alert box to confirm.</param>
		public void ReloadFile(bool withAlert)
		{
			Plugin.NppIntf.ReloadFile(withAlert);
		}

		/// <summary>
		/// Reloads a file given the file name.  If the file is not already open, then this has no effect.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be reloaded.</param>
		/// <param name="withAlert">If true, Notepad++ will show an alert box to confirm.</param>
		public void ReloadFile(string fileName, bool withAlert)
		{
			Plugin.NppIntf.ReloadFile(fileName, withAlert);
		}

		/// <summary>
		/// Switches to a file given the file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be brought current.</param>
		/// <returns>True if the file could be switched to; false otherwise.</returns>
		public bool SwitchToFile(string fileName)
		{
			return Plugin.NppIntf.SwitchToFile(fileName);
		}

		/// <summary>
		/// Saves the current document.
		/// </summary>
		/// <returns>True if successful, otherwise false.</returns>
		public bool SaveFile()
		{
			return Plugin.NppIntf.SaveFile();
		}

		/// <summary>
		/// Saves the current document to another file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be written.</param>
		/// <returns>True if successful, otherwise false.</returns>
		public bool SaveFileAs(string fileName)
		{
			return Plugin.NppIntf.SaveFileAs(fileName);
		}

		/// <summary>
		/// Saves a copy of the current document to another file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be written.</param>
		/// <returns>True if successful, otherwise false.</returns>
		public bool SaveFileCopyAs(string fileName)
		{
			return Plugin.NppIntf.SaveFileCopyAs(fileName);
		}

		/// <summary>
		/// Saves all open documents.
		/// </summary>
		/// <returns>True if successful, otherwise false.</returns>
		public bool SaveAllFiles()
		{
			return Plugin.NppIntf.SaveAllFiles();
		}
		#endregion

		#region Editor
		/// <summary>
		/// Gets the current editor view (main or sub).
		/// </summary>
		public EditorView CurrentView
		{
			get { return Plugin.NppIntf.CurrentView; }
		}

		/// <summary>
		/// Gets or sets the current language ID.
		/// Note: languages for external lexers cannot be selected.
		/// </summary>
		public int LanguageId
		{
			get { return Plugin.NppIntf.LanguageId; }
			set { Plugin.NppIntf.LanguageId = value; }
		}

		/// <summary>
		/// Gets the name of the current language.
		/// </summary>
		public string LanguageName
		{
			get { return Plugin.NppIntf.LanguageName; }
		}

		/// <summary>
		/// Gets the text for the specified line.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The text for that line.  Blank if an invalid line number.</returns>
		public string GetLineText(int line)
		{
			return Plugin.NppIntf.GetLineText(line, false);
		}

		/// <summary>
		/// Gets the text for the specified line, optionally including line-end characters.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <param name="includeLineEndChars">If true, the returned string will contain the line-end characters.</param>
		/// <returns>The text for that line.</returns>
		public string GetLineText(int line, bool includeLineEndChars)
		{
			return Plugin.NppIntf.GetLineText(line, includeLineEndChars);
		}

		/// <summary>
		/// Gets the number of lines in the document.
		/// </summary>
		public int LineCount
		{
			get { return Plugin.NppIntf.LineCount; }
		}

		/// <summary>
		/// Gets the starting location of the document.
		/// </summary>
		/// <remarks>This will always be line 1, character position 1.</remarks>
		public TextLocation Start
		{
			get { return new TextLocation(); }
		}

		/// <summary>
		/// Gets the end location of the document.
		/// </summary>
		public TextLocation End
		{
			get { return TextLocation.FromByteOffset(Plugin.NppIntf.Length); }
		}

		/// <summary>
		/// Inserts text over the current selection.
		/// The caret is placed after the inserted text and scrolled into view.
		/// </summary>
		/// <param name="text">The text to be inserted.</param>
		public void Insert(string text)
		{
			Plugin.NppIntf.Insert(text);
		}

		/// <summary>
		/// Gets the text for the specified range.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="numChars">The number of characters to retrieve.</param>
		/// <returns>The text for the given range.</returns>
		public string GetText(TextLocation start, int numChars)
		{
			return Plugin.NppIntf.GetText(start, numChars);
		}

		/// <summary>
		/// Gets the text for the specified range.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="end">The ending position (exclusive).</param>
		/// <returns>The text for the given range.</returns>
		public string GetText(TextLocation start, TextLocation end)
		{
			return Plugin.NppIntf.GetText(start, end);
		}

		/// <summary>
		/// Appends text to the end of the document without affecting the selection.
		/// </summary>
		/// <param name="text">The text to be appended.</param>
		public void Append(string text)
		{
			Plugin.NppIntf.Append(text);
		}

		/// <summary>
		/// Clears all text out of the document.
		/// </summary>
		public void ClearAll()
		{
			Plugin.NppIntf.ClearAll();
		}

		/// <summary>
		/// Cuts the selected text to the clipboard.
		/// </summary>
		public void Cut()
		{
			Plugin.NppIntf.Cut();
		}

		/// <summary>
		/// Copies the selected text to the clipboard.
		/// </summary>
		public void Copy()
		{
			Plugin.NppIntf.Copy();
		}

		/// <summary>
		/// Copies the specified text range into the clipboard.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="numChars">The number of characters to be copied.</param>
		public void Copy(TextLocation start, int numChars)
		{
			Plugin.NppIntf.Copy(start, numChars);
		}

		/// <summary>
		/// Copies the specified text range into the clipboard.
		/// </summary>
		/// <param name="start">The starting position.</param>
		/// <param name="end">The ending position (exclusive).</param>
		public void Copy(TextLocation start, TextLocation end)
		{
			Plugin.NppIntf.Copy(start, end);
		}

		/// <summary>
		/// Copies the provided string into the clipboard.
		/// </summary>
		/// <param name="text">The text to be placed into to the clipboard.</param>
		public void Copy(string text)
		{
			Plugin.NppIntf.Copy(text);
		}

		/// <summary>
		/// Copies the selected text to the clipboard.
		/// If no text is selected, the current line is copied instead.
		/// </summary>
		public void CopyAllowLine()
		{
			Plugin.NppIntf.CopyAllowLine();
		}

		/// <summary>
		/// Pastes the clipboard text over the selection.
		/// </summary>
		public void Paste()
		{
			Plugin.NppIntf.Paste();
		}

		/// <summary>
		/// Deletes the selected text.
		/// </summary>
		public void Clear()
		{
			Plugin.NppIntf.Clear();
		}

		/// <summary>
		/// Gets the length of the document.
		/// </summary>
		public int Length
		{
			get { return Plugin.NppIntf.Length; }
		}

		/// <summary>
		/// Gets or sets the first visible line in the document.
		/// </summary>
		public int FirstVisibleLine
		{
			get { return Plugin.NppIntf.FirstVisibleLine; }
			set { Plugin.NppIntf.FirstVisibleLine = value; }
		}

		/// <summary>
		/// Gets the number of complete lines visible on the screen.
		/// </summary>
		public int LinesOnScreen
		{
			get { return Plugin.NppIntf.LinesOnScreen; }
		}

		/// <summary>
		/// Gets a flag indicating if the document has been modified since the last save.
		/// </summary>
		public bool Modified
		{
			get { return Plugin.NppIntf.FileModified; }
		}

		/// <summary>
		/// Sets the selection range.
		/// (The caret is scrolled into view)
		/// </summary>
		/// <param name="anchorPos">The selection anchor position.</param>
		/// <param name="currentPos">The selection current position.</param>
		public void SetSelection(TextLocation anchorPos, TextLocation currentPos)
		{
			Plugin.NppIntf.SetSelection(anchorPos, currentPos);
		}

		/// <summary>
		/// Goes to the specified position and ensures it is visible.
		/// </summary>
		/// <param name="pos">The position to go to.</param>
		public void GoTo(TextLocation pos)
		{
			Plugin.NppIntf.GoToPos(pos);
		}

		/// <summary>
		/// Goes to the specified line and ensures it is visible.
		/// </summary>
		/// <param name="line">The line to go to.</param>
		public void GoToLine(int line)
		{
			Plugin.NppIntf.GoToLine(line);
		}

		/// <summary>
		/// Gets or sets the current position.
		/// (The caret is not scrolled into view)
		/// </summary>
		public TextLocation CurrentLocation
		{
			get { return Plugin.NppIntf.CurrentPos; }
			set { Plugin.NppIntf.CurrentPos = value; }
		}

		/// <summary>
		/// Gets or sets the current line.
		/// (The caret is not scrolled into view)
		/// </summary>
		public int CurrentLine
		{
			get { return Plugin.NppIntf.CurrentLine; }
			set { Plugin.NppIntf.CurrentLine = value; }
		}

		/// <summary>
		/// Gets or sets the selection anchor position.
		/// (The caret is not scrolled into view)
		/// </summary>
		public TextLocation AnchorLocation
		{
			get { return Plugin.NppIntf.AnchorPos; }
			set { Plugin.NppIntf.AnchorPos = value; }
		}

		/// <summary>
		/// Gets or sets the selection start position.
		/// (The caret is not scrolled into view)
		/// </summary>
		public TextLocation SelectionStart
		{
			get { return Plugin.NppIntf.SelectionStart; }
			set { Plugin.NppIntf.SelectionStart = value; }
		}

		/// <summary>
		/// Gets or sets the selection end position.
		/// (The caret is not scrolled into view)
		/// </summary>
		public TextLocation SelectionEnd
		{
			get { return Plugin.NppIntf.SelectionEnd; }
			set { Plugin.NppIntf.SelectionEnd = value; }
		}

		/// <summary>
		/// Removes the selection and sets the caret at pos.
		/// (The caret is not scrolled into view)
		/// </summary>
		/// <param name="pos">The new start/end position for the selection.</param>
		public void SetEmptySelection(TextLocation pos)
		{
			Plugin.NppIntf.SetEmptySelection(pos);
		}

		/// <summary>
		/// Selects all text in the document.
		/// (The caret is not scrolled into view)
		/// </summary>
		public void SelectAll()
		{
			Plugin.NppIntf.SelectAll();
		}

		/// <summary>
		/// Gets the starting position of the specified line.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The starting position of the line.</returns>
		public TextLocation GetLineStartPos(int line)
		{
			if (line < 1) return Start;
			if (line > LineCount) return End;
			return new TextLocation(line, 1);
		}

		/// <summary>
		/// Gets the ending position of the specified line, before any line-end characters.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The end position of the line.</returns>
		public TextLocation GetLineEndPos(int line)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.GetLineEndPos(line));
		}

		/// <summary>
		/// Gets the length of the specified line (excluding line-end characters).
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The length of the line (excluding line-end characters)</returns>
		public int GetLineLength(int line)
		{
			return Plugin.NppIntf.GetLineLength(line);
		}

		/// <summary>
		/// Gets or sets the selected text.
		/// When setting, the caret is placed after the inserted text and scrolled into view.
		/// </summary>
		public string SelectedText
		{
			get { return Plugin.NppIntf.SelectedText; }
			set { Plugin.NppIntf.SelectedText = value; }
		}

		/// <summary>
		/// Gets the selection mode.
		/// </summary>
		public SelectionMode SelectionMode
		{
			get { return Plugin.NppIntf.SelectionMode; }
		}

		/// <summary>
		/// If the caret is outside the current view, it is moved to the nearest visible line.
		/// Any selection is lost.
		/// </summary>
		public void MoveCaretInsideView()
		{
			Plugin.NppIntf.MoveCaretInsideView();
		}

		/// <summary>
		/// Gets the position at the end of the current word.
		/// </summary>
		/// <param name="pos">The starting position.</param>
		/// <param name="onlyWordChars">If true, only word characters will be jumped.
		/// If false, all characters will be jumped.</param>
		/// <returns>The position at the end of the word.</returns>
		public TextLocation GetWordEndPos(TextLocation pos, bool onlyWordChars)
		{
			return Plugin.NppIntf.GetWordEndPos(pos, onlyWordChars);
		}

		/// <summary>
		/// Gets the position at the start of the current word.
		/// </summary>
		/// <param name="pos">The starting position.</param>
		/// <param name="onlyWordChars">If true, only word characters will be jumped.
		/// If false, all characters will be jumped.</param>
		/// <returns>The position at the start of the word.</returns>
		public TextLocation GetWordStartPos(TextLocation pos, bool onlyWordChars)
		{
			return Plugin.NppIntf.GetWordStartPos(pos, onlyWordChars);
		}

		/// <summary>
		/// Gets the position that corresponds to a line and column.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <param name="column">The one-based column number.</param>
		/// <returns>The zero-based position.</returns>
		public TextLocation FindColumn(int line, int column)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.FindColumn(line, column));
		}

		/// <summary>
		/// Finds the position closest to a point on the screen.
		/// </summary>
		/// <param name="pt">The client coordinates of the point to test.</param>
		/// <returns>The zero-based position of the closest character.</returns>
		public TextLocation PointToPos(Point pt)
		{
			return TextLocation.FromByteOffset(Plugin.NppIntf.PointToPos(pt));
		}

		/// <summary>
		/// Finds the position closest to a point on the screen.
		/// </summary>
		/// <param name="pt">The client coordinates of the point to test.</param>
		/// <param name="location">An out parameter to receive the found location.</param>
		/// <returns>
		/// If no character is close, or the point is outside the window, this function returns
		/// false, and the location parameter is set to the start of the document.
		/// If a location was found, this function returns true, and the location parameter is set
		/// to the close position.
		/// </returns>
		public bool PointToPosClose(Point pt, out TextLocation location)
		{
			int offset = Plugin.NppIntf.PointToPosClose(pt);
			if (offset < 0)
			{
				location = TextLocation.Start;
				return false;
			}
			location = TextLocation.FromByteOffset(offset);
			return true;
		}

		/// <summary>
		/// Returns the x and y display location of the position.
		/// </summary>
		/// <param name="pos">The position to find.</param>
		/// <returns>The client coordinates of the text position.</returns>
		public Point PosToPoint(TextLocation pos)
		{
			return Plugin.NppIntf.PosToPoint(pos.ByteOffset);
		}

		/// <summary>
		/// Moves the selected lines up one line, shifting the line above the selection.
		/// </summary>
		public void MoveSelectedLinesUp()
		{
			Plugin.NppIntf.MoveSelectedLinesUp();
		}

		/// <summary>
		/// Moves the selected lines down one line, shifting the line below the selection.
		/// </summary>
		public void MoveSelectedLinesDown()
		{
			Plugin.NppIntf.MoveSelectedLinesDown();
		}
		#endregion

		#region Events
		/// <summary>
		/// Triggered when Notepad++ has finished loading, and sends out the 'ready' notification to all plugins.
		/// </summary>
		public event NppEventHandler Ready;

		/// <summary>
		/// Triggered when Notepad++ is about to shutdown.
		/// </summary>
		public event NppEventHandler Shutdown;

		/// <summary>
		/// Triggered before a file has started closing.
		/// </summary>
		public event FileEventHandler FileClosing;

		/// <summary>
		/// Triggered after a file has closed.
		/// </summary>
		public event FileEventHandler FileClosed;

		/// <summary>
		/// Triggered before a file has started opening.
		/// </summary>
		public event FileEventHandler FileOpening;

		/// <summary>
		/// Triggered after a file has opened successfully.
		/// </summary>
		public event FileEventHandler FileOpened;

		/// <summary>
		/// Triggered before a file has started saving.
		/// </summary>
		public event FileEventHandler FileSaving;

		/// <summary>
		/// Triggered after a file has saved successfully.
		/// </summary>
		public event FileEventHandler FileSaved;

		/// <summary>
		/// Triggered when a different file is brought to the foreground.
		/// </summary>
		public event FileEventHandler FileActivated;

		/// <summary>
		/// Triggered when the language for a file changes.
		/// </summary>
		public event LanguageTypeEventHandler LanguageChanged;

		/// <summary>
		/// Triggered when the syntax styles change for the file in the foreground.
		/// </summary>
		public event FileEventHandler StyleUpdate;

		/// <summary>
		/// Triggered before a file has started loading.
		/// </summary>
		public event NppEventHandler FileLoading;

		/// <summary>
		/// Triggered when a file could not be loaded due to an error.
		/// </summary>
		public event NppEventHandler FileLoadFailed;

		/// <summary>
		/// Triggered when the file tab-order has changed (user dragged a tab).
		/// </summary>
		public event FileEventHandler FileOrderChanged;

		/// <summary>
		/// Triggered when a character is added to a document.
		/// </summary>
		public event CharAddedEventHandler CharAdded;

		/// <summary>
		/// Triggered when the user double-clicks inside a document.
		/// </summary>
		public event DoubleClickEventHandler DoubleClick;

		/// <summary>
		/// Triggered when the active document is modified.
		/// </summary>
		public event ModifiedEventHandler Modification;

        /// <summary>
        /// Triggered when the user changes the selection.
        /// </summary>
        public event NppEventHandler SelectionChanged;

        /// <summary>
        /// Triggered when the user scrolls the view vertically.
        /// </summary>
        public event NppEventHandler ScrolledVertically;

        /// <summary>
        /// Triggered when the user scrolls the view horizontally.
        /// </summary>
        public event NppEventHandler ScrolledHorizontally;

		internal void InitEvents()
		{
			INpp npp = Plugin.NppIntf;

			npp.Ready += OnReady;
			npp.Shutdown += OnShutdown;
			npp.FileClosing += OnFileClosing;
			npp.FileClosed += OnFileClosed;
			npp.FileOpening += OnFileOpening;
			npp.FileOpened += OnFileOpened;
			npp.FileSaving += OnFileSaving;
			npp.FileSaved += OnFileSaved;
			npp.FileActivated += OnFileActivated;
			npp.LanguageChanged += OnLanguageChanged;
			npp.StyleUpdate += OnStyleUpdate;
			npp.FileLoading += OnFileLoading;
			npp.FileLoadFailed += OnFileLoadFailed;
			npp.FileOrderChanged += OnFileOrderChanged;
			npp.CharAdded += OnCharAdded;
			npp.DoubleClick += OnDoubleClick;
			npp.Modification += OnModification;
            npp.SelectionChanged += OnSelectionChanged;
            npp.ScrolledVertically += OnScrolledVertically;
            npp.ScrolledHorizontally += OnScrolledHorizontally;
		}

		/// <summary>
		/// Called after the object has been created and initialized, but before the Notepad++ Ready notification.
		/// </summary>
		protected virtual void OnLoad()
		{
			// This function is meant to be overridden by script classes.
		}

		internal void OnReady(object sender, EventArgs e)
		{
			NppEventHandler ev = Ready;
			if (ev != null) ev(this, e);
		}

		internal void OnShutdown(object sender, EventArgs e)
		{
			NppEventHandler ev = Shutdown;
			if (ev != null) ev(this, e);
		}

		internal void OnFileClosing(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileClosing;
			if (ev != null) ev(this, e);
		}

		internal void OnFileClosed(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileClosed;
			if (ev != null) ev(this, e);
		}

		internal void OnFileOpening(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileOpening;
			if (ev != null) ev(this, e);
		}

		internal void OnFileOpened(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileOpened;
			if (ev != null) ev(this, e);
		}

		internal void OnFileSaving(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileSaving;
			if (ev != null) ev(this, e);
		}

		internal void OnFileSaved(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileSaved;
			if (ev != null) ev(this, e);
		}

		internal void OnFileActivated(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileActivated;
			if (ev != null) ev(this, e);
		}

		internal void OnLanguageChanged(object sender, LanguageTypeEventArgs e)
		{
			LanguageTypeEventHandler ev = LanguageChanged;
			if (ev != null) ev(this, e);
		}

		internal void OnStyleUpdate(object sender, FileEventArgs e)
		{
			FileEventHandler ev = StyleUpdate;
			if (ev != null) ev(this, e);
		}

		internal void OnFileLoading(object sender, EventArgs e)
		{
			NppEventHandler ev = FileLoading;
			if (ev != null) ev(this, e);
		}

		internal void OnFileLoadFailed(object sender, EventArgs e)
		{
			NppEventHandler ev = FileLoadFailed;
			if (ev != null) ev(this, e);
		}

		internal void OnFileOrderChanged(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileOrderChanged;
			if (ev != null) ev(this, e);
		}

		internal void OnCharAdded(object sender, CharAddedEventArgs e)
		{
			CharAddedEventHandler ev = CharAdded;
			if (ev != null) ev(this, e);
		}

		internal void OnDoubleClick(object sender, DoubleClickEventArgs e)
		{
			DoubleClickEventHandler ev = DoubleClick;
			if (ev != null) ev(this, e);
		}

		internal void OnModification(object sender, ModifiedEventArgs e)
		{
			ModifiedEventHandler ev = Modification;
			if (ev != null) ev(this, e);
		}

        internal void OnSelectionChanged(object sender, EventArgs e)
        {
            var ev = SelectionChanged;
            if (ev != null) ev(this, e);
        }

        internal void OnScrolledVertically(object sender, EventArgs e)
        {
            var ev = ScrolledVertically;
            if (ev != null) ev(this, e);
        }

        internal void OnScrolledHorizontally(object sender, EventArgs e)
        {
            var ev = ScrolledHorizontally;
            if (ev != null) ev(this, e);
        }
		#endregion

		#region Dock Window
		/// <summary>
		/// Creates a docked window.
		/// </summary>
		/// <param name="window">The form window that is to be docked.</param>
		/// <param name="title">The window title.</param>
		/// <param name="alignment">Window alignment, or floating.</param>
		/// <param name="id">An identifier for this docked window.
		/// The ID must be greater than zero.</param>
		/// <returns>An IDockWindow interface that can be used to hide or show the window.</returns>
		/// <remarks>
		/// <para>
		///	This window will not be restored the next time Notepad++ starts.
		///	The plugin must recreate the window during the 'Ready' event, if it wishes to have the window visible again.
		/// </para>
		/// <para>
		///	Notepad++ will use the ID to remember the state for this docked window between sessions.
		///	If another docked window with the same ID had been opened previously with a different alignment,
		///	or the user had dragged the window to another side of the screen, then the docked window
		///	will appear in the previous location rather than the one specified here.
		/// </para>
		/// </remarks>
		public IDockWindow DockWindow(IWin32Window window, string title, DockWindowAlignment alignment, int id)
		{
			// Notepad++ doesn't seem to support icons for docked windows, so it's excluded here.

			if (window == null) throw new ArgumentException(Res.err_InvalidWindowObj);
			if (id <= 0) throw new ArgumentException(Res.err_InvalidDockWindowId);

			return Plugin.NppIntf.CreateDockWindow(window, title, alignment, null, id);
		}

		/// <summary>
		/// Gets the dock window object for the specified ID.
		/// </summary>
		/// <param name="id">The ID number for the dock window object.</param>
		/// <returns>If the dock window object could be found, the object is returned; otherwise null.</returns>
		public IDockWindow GetDockWindow(int id)
		{
			if (id <= 0) throw new ArgumentException(Res.err_InvalidDockWindowId);

			return Plugin.NppIntf.GetDockWindow(id);
		}
		#endregion

		#region Lexers
		/// <summary>
		/// Refreshes the word-styles and folding on documents that use a custom lexer provided via NppSharp.
		/// </summary>
		/// <remarks>
		/// This function is useful if your lexers providing highlighting that can change,
		/// and need to be refreshed at certain events.
		/// </remarks>
		public void RefreshCustomLexers()
		{
			Plugin.NppIntf.RefreshCustomLexers();
		}
		#endregion

		#region AutoCompletion
		/// <summary>
		/// Shows the AutoCompletion list.
		/// </summary>
		/// <param name="lengthEntered">The number of characters already entered by the user.</param>
		/// <param name="list">The list of words.</param>
		/// <param name="ignoreCase">Ignore case?</param>
		public void ShowAutoCompletion(int lengthEntered, IEnumerable<string> list, bool ignoreCase)
		{
			Plugin.NppIntf.ShowAutoCompletion(lengthEntered, list, ignoreCase);
		}

		/// <summary>
		/// Shows the AutoCompletion list.
		/// </summary>
		/// <param name="lengthEntered">The number of characters already entered by the user.</param>
		/// <param name="list">The list of words.</param>
		public void ShowAutoCompletion(int lengthEntered, IEnumerable<string> list)
		{
			Plugin.NppIntf.ShowAutoCompletion(lengthEntered, list, false);
		}

		/// <summary>
		/// Cancels any active auto-completion.
		/// </summary>
		public void CancelAutoCompletion()
		{
			Plugin.NppIntf.CancelAutoCompletion();
		}
		#endregion
	}
}
