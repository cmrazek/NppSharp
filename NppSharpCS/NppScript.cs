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

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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
		/// Launches the 'Find in Files' dialog.
		/// </summary>
		/// <param name="dirs">The directories to be searched.</param>
		/// <param name="filters">The file filters to be searched.</param>
		public void LaunchFindInFiles(string dir, string filters)
		{
			Plugin.NppIntf.LaunchFindInFiles(dir, filters);
		}

		/// <summary>
		/// Triggers a menu item command.
		/// </summary>
		/// <param name="commandId">The ID of the command to be triggered.</param>
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
		/// This property is not available in the constructor.
		/// </summary>
		public string ScriptFileName
		{
			get { return _fileName; }
			internal set { _fileName = value; }
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
		/// Gets a complete list of file names for open files.
		/// </summary>
		public IEnumerable<string> FileNames
		{
			get { return Plugin.NppIntf.FileNames; }
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
		/// <param name="withAlert">If true, Notepad++ will show an alert box will be displayed.</param>
		/// </summary>
		public void ReloadFile(bool withAlert)
		{
			Plugin.NppIntf.ReloadFile(withAlert);
		}

		/// <summary>
		/// Reloads a file given the file name.  If the file is not already open, then this has no effect.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be reloaded.</param>
		/// <param name="withAlert">If true, Notepad++ will show an alert box will be displayed.</param>
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
		public bool SaveFile()
		{
			return Plugin.NppIntf.SaveFile();
		}

		/// <summary>
		/// Saves the current document to another file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be written.</param>
		public bool SaveFileAs(string fileName)
		{
			return Plugin.NppIntf.SaveFileAs(fileName);
		}

		/// <summary>
		/// Saves a copy of the current document to another file name.
		/// </summary>
		/// <param name="fileName">The pathname of the file to be written.</param>
		public bool SaveFileCopyAs(string fileName)
		{
			return Plugin.NppIntf.SaveFileCopyAs(fileName);
		}

		/// <summary>
		/// Saves all open documents.
		/// </summary>
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
		/// <param name="startPos">The 0-based starting position.</param>
		/// <param name="length">The length of text to retrieve.</param>
		/// <returns>The text for the given range.</returns>
		public string GetText(int startPos, int length)
		{
			return Plugin.NppIntf.GetText(startPos, length);
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
		/// <param name="startPos">The 0-based starting position.</param>
		/// <param name="length">The number of characters to be copied.</param>
		public void Copy(int startPos, int length)
		{
			Plugin.NppIntf.Copy(startPos, length);
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
		/// <param name="anchorPos">The selection anchor position.  If negative, the selection will
		/// be removed and start/end will be set to currentPos.</param>
		/// <param name="currentPos">The selection current position.  If negative, the selection
		/// will extend to the end of the document.</param>
		public void SetSelection(int anchorPos, int currentPos)
		{
			Plugin.NppIntf.SetSelection(anchorPos, currentPos);
		}

		/// <summary>
		/// Goes to the specified position and ensures it is visible.
		/// </summary>
		/// <param name="pos">The position to go to.</param>
		public void GoToPos(int pos)
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
		public int CurrentPos
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
		public int AnchorPos
		{
			get { return Plugin.NppIntf.AnchorPos; }
			set { Plugin.NppIntf.AnchorPos = value; }
		}

		/// <summary>
		/// Gets or sets the selection start position.
		/// (The caret is not scrolled into view)
		/// </summary>
		public int SelectionStart
		{
			get { return Plugin.NppIntf.SelectionStart; }
			set { Plugin.NppIntf.SelectionStart = value; }
		}

		/// <summary>
		/// Gets or sets the selection end position.
		/// (The caret is not scrolled into view)
		/// </summary>
		public int SelectionEnd
		{
			get { return Plugin.NppIntf.SelectionEnd; }
			set { Plugin.NppIntf.SelectionEnd = value; }
		}

		/// <summary>
		/// Removes the selection and sets the caret at pos.
		/// (The caret is not scrolled into view)
		/// </summary>
		public void SetEmptySelection(int pos)
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
		/// Gets the line that contains the specified position.
		/// </summary>
		/// <param name="pos">The zero-based position.</param>
		/// <returns>The one-based line number that contains the position.</returns>
		public int GetLineFromPos(int pos)
		{
			return Plugin.NppIntf.GetLineFromPos(pos);
		}

		/// <summary>
		/// Gets the starting position of the specified line.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The zero-based starting position of the line.</returns>
		public int GetLineStartPos(int line)
		{
			return Plugin.NppIntf.GetLineStartPos(line);
		}

		/// <summary>
		/// Gets the ending position of the specified line, before any line end characters.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The zero-based end position of the line.</returns>
		public int GetLineEndPos(int line)
		{
			return Plugin.NppIntf.GetLineEndPos(line);
		}

		/// <summary>
		/// Gets the length of the specified line.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <returns>The length of the line including any line end characters.</returns>
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
		public int GetWordEndPos(int pos, bool onlyWordChars)
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
		public int GetWordStartPos(int pos, bool onlyWordChars)
		{
			return Plugin.NppIntf.GetWordStartPos(pos, onlyWordChars);
		}

		/// <summary>
		/// Gets the column number for the specified position.
		/// </summary>
		/// <param name="pos">The zero-based position.</param>
		/// <returns>The one-based column number.</returns>
		public int GetColumn(int pos)
		{
			return Plugin.NppIntf.GetColumn(pos);
		}

		/// <summary>
		/// Gets the position that corresponds to a line and column.
		/// </summary>
		/// <param name="line">The one-based line number.</param>
		/// <param name="column">The one-based column number.</param>
		/// <returns>The zero-based position.</returns>
		public int FindColumn(int line, int column)
		{
			return Plugin.NppIntf.FindColumn(line, column);
		}

		/// <summary>
		/// Finds the position closest to a point on the screen.
		/// </summary>
		/// <param name="pt">The client coordinates of the point to test.</param>
		/// <returns>The zero-based position of the closest character.</returns>
		public int PointToPos(Point pt)
		{
			return Plugin.NppIntf.PointToPos(pt);
		}

		/// <summary>
		/// Finds the position closest to a point on the screen.
		/// If no character is close, or the point it outside the window, it returns -1.
		/// </summary>
		/// <param name="pt">The client coordinates of the point to test.</param>
		/// <returns>-1 if not close to any character, otherwise the zero-based position of the
		/// closest character.</returns>
		public int PointToPosClose(Point pt)
		{
			return Plugin.NppIntf.PointToPosClose(pt);
		}

		/// <summary>
		/// Returns the x and y display location of the position.
		/// </summary>
		/// <param name="pos">The position to find.</param>
		/// <returns>The client coordinates of the text position.</returns>
		public Point PosToPoint(int pos)
		{
			return Plugin.NppIntf.PosToPoint(pos);
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
		public event EventHandler Ready;

		/// <summary>
		/// Triggered when Notepad++ is about to shutdown.
		/// </summary>
		public event EventHandler Shutdown;

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
		public event EventHandler FileLoading;

		/// <summary>
		/// Triggered when a file could not be loaded due to an error.
		/// </summary>
		public event EventHandler FileLoadFailed;

		/// <summary>
		/// Triggered when the file tab-order has changed (user dragged a tab).
		/// </summary>
		public event FileEventHandler FileOrderChanged;

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
			EventHandler ev = Ready;
			if (ev != null) ev(this, e);
		}

		internal void OnShutdown(object sender, EventArgs e)
		{
			EventHandler ev = Shutdown;
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
			EventHandler ev = FileLoading;
			if (ev != null) ev(this, e);
		}

		internal void OnFileLoadFailed(object sender, EventArgs e)
		{
			EventHandler ev = FileLoadFailed;
			if (ev != null) ev(this, e);
		}

		internal void OnFileOrderChanged(object sender, FileEventArgs e)
		{
			FileEventHandler ev = FileOrderChanged;
			if (ev != null) ev(this, e);
		}
		#endregion
	}
}
