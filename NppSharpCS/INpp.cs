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
using System.Windows.Forms;
using System.Drawing;

namespace NppSharp
{
	public interface INpp
	{
		#region Notepad++ Core
		NativeWindow Window { get; }
		string NppDir { get; }
		string ConfigDir { get; }
		void AddCommand(PluginCommand cmd);
		int AllocateCommandId();
		void AddToolbarIcon(PluginCommand cmd);
		#endregion

		#region Events
		event EventHandler GetCommands;
		event EventHandler RegisterToolbarIcons;
		event ExecuteCommandEventHandler CommandExecuted;

		event EventHandler Ready;
		event EventHandler Shutdown;
		event FileEventHandler FileClosing;
		event FileEventHandler FileClosed;
		event FileEventHandler FileOpening;
		event FileEventHandler FileOpened;
		event FileEventHandler FileSaving;
		event FileEventHandler FileSaved;
		event FileEventHandler FileActivated;
		event LanguageTypeEventHandler LanguageChanged;
		event FileEventHandler StyleUpdate;
		event EventHandler FileLoading;
		event EventHandler FileLoadFailed;
		event FileEventHandler FileOrderChanged;
		#endregion

		#region Editor
		void OnCommandStart();
		void OnCommandEnd();

		EditorView CurrentView { get; }
		int LanguageId { get; set; }
		string LanguageName { get; }
		string GetLanguageName(int langId);
		string FileName { get; }
		int FileCount { get; }
		IEnumerable<string> FileNames { get; }
		bool OpenFile(string fileName);
		int ActiveFileIndex { get; set; }
		int GetActiveFileIndex(EditorView view);
		void SetActiveFileIndex(EditorView view, int index);
		void ReloadFile(bool withAlert);
		void ReloadFile(string fileName, bool withAlert);
		bool SwitchToFile(string fileName);
		bool SaveFile();
		bool SaveFileAs(string fileName);
		bool SaveFileCopyAs(string fileName);
		bool SaveAllFiles();
		void LaunchFindInFiles(string dirs, string filters);
		void MenuCommand(int commandId);
		string GetLineText(int line, bool includeLineEndChars);
		int LineCount { get; }
		void Insert(string text);
		string GetText(int startPos, int length);
		void Append(string text);
		void ClearAll();
		void Cut();
		void Copy();
		void Copy(int startPos, int length);
		void Copy(string text);
		void CopyAllowLine();
		void Paste();
		void Clear();
		int Length { get; }
		int FirstVisibleLine { get; set; }
		int LinesOnScreen { get; }
		bool FileModified { get; }
		void SetSelection(int anchorPos, int currentPos);
		void GoToPos(int pos);
		void GoToLine(int line);
		int CurrentPos { get; set; }
		int CurrentLine { get; set; }
		int AnchorPos { get; set; }
		int SelectionStart { get; set; }
		int SelectionEnd { get; set; }
		void SetEmptySelection(int pos);
		void SelectAll();
		int GetLineFromPos(int pos);
		int GetLineStartPos(int line);
		int GetLineEndPos(int line);
		int GetLineLength(int line);
		string SelectedText { get; set; }
		SelectionMode SelectionMode { get; }	//set; }
		void MoveCaretInsideView();
		int GetWordEndPos(int pos, bool onlyWordChars);
		int GetWordStartPos(int pos, bool onlyWordChars);
		int GetColumn(int pos);
		int FindColumn(int line, int column);
		int PointToPos(Point pt);
		int PointToPosClose(Point pt);
		Point PosToPoint(int pos);
		void MoveSelectedLinesUp();
		void MoveSelectedLinesDown();
		#endregion

		#region Output Window
		void ShowOutputWindow();
		void HideOutputWindow();
		bool OutputWindowVisible { get; }
		void WriteOutput(string text);
		void WriteOutputLine(string text);
		OutputStyle OutputStyle { get; set; }
		void SetShowOutputWindowCommandIndex(int cmdIndex);
		void ClearOutputWindow();
		void OutputWindowGoToTop();
		void OutputWindowGoToBottom();
		void SetOutputStyleDef(OutputStyleDef osd, OutputStyleDef defaultOsd);
		#endregion
	}

	#region Event Handlers
	public delegate void EventHandler(object sender, EventArgs e);

	public delegate void ExecuteCommandEventHandler(object sender, ExecuteCommandEventArgs e);
	public class ExecuteCommandEventArgs : EventArgs
	{
		private PluginCommand _cmd = null;

		public ExecuteCommandEventArgs(PluginCommand cmd)
		{
			_cmd = cmd;
		}

		public PluginCommand Command
		{
			get { return _cmd; }
		}
	}

	public delegate void FileEventHandler(object sender, FileEventArgs e);
	public class FileEventArgs : EventArgs
	{
		private string _fileName = "";
		private uint _bufferId = 0;

		public FileEventArgs(uint bufferId, string fileName)
		{
			_bufferId = bufferId;
			_fileName = fileName;
		}

		public string FileName
		{
			get { return _fileName; }
		}

		public uint BufferId
		{
			get { return _bufferId; }
		}
	}

	public delegate void LanguageTypeEventHandler(object sender, LanguageTypeEventArgs e);
	public class LanguageTypeEventArgs : EventArgs
	{
		private string _fileName = "";
		private uint _bufferId = 0;
		private int _langId = 0;

		public LanguageTypeEventArgs(uint bufferId, string fileName, int langId)
		{
			_bufferId = bufferId;
			_fileName = fileName;
			_langId = langId;
		}

		public string FileName
		{
			get { return _fileName; }
		}

		public uint BufferId
		{
			get { return _bufferId; }
		}

		public int LanguageId
		{
			get { return _langId; }
		}
	}
	#endregion
}
