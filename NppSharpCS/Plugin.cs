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
using System.IO;

namespace NppSharp
{
	/// <summary>
	/// Main class that manages the NppSharp plugin.
	/// </summary>
	[System.Runtime.CompilerServices.CompilerGenerated]	// To stop it from appearing in the help file
	public static class Plugin
	{
		#region Construction
		private static INpp _npp = null;

		/// <summary>
		/// Initializes the plugin.
		/// </summary>
		/// <param name="npp">The Notepad++ interface object.</param>
		public static void Initialize(INpp npp)
		{
			_npp = npp;
			_output = new OutputView();

			ScriptManager.Compile();
			OutputStyleDef.InitStyles();

			_npp.GetCommands += OnGetCommands;
			_npp.RegisterToolbarIcons += OnRegisterToolbarIcons;
			_npp.CommandExecuted += OnCommandExecuted;
		}

		internal static INpp NppIntf
		{
			get { return _npp; }
		}
		#endregion

		#region Error Handling
		/// <summary>
		/// Displays an error message to the user.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		public static void ShowError(string message)
		{
			try
			{
				ErrorForm form = new ErrorForm();
				form.Message = message;
				form.ShowDialog(_npp.Window);
			}
			catch (Exception)
			{ }
		}

		/// <summary>
		/// Displays an error caused by an exception to the user.
		/// </summary>
		/// <param name="ex">The exception that caused the error.</param>
		public static void ShowError(Exception ex)
		{
			try
			{
				ErrorForm form = new ErrorForm();
				form.Message = ex.Message;
				form.Details = ex.ToString();
				form.ShowDialog(_npp.Window);
			}
			catch (Exception)
			{ }
		}

		/// <summary>
		/// Displays an error caused by an exception to the user, with additional info.
		/// </summary>
		/// <param name="ex">The exception that caused the error.</param>
		/// <param name="message">Additional info text.</param>
		public static void ShowError(Exception ex, string message)
		{
			try
			{
				ErrorForm form = new ErrorForm();
				form.Message = message;
				form.Details = ex.ToString();
				form.ShowDialog(_npp.Window);
			}
			catch (Exception)
			{ }
		}
		#endregion

		#region Commands
		private static List<PluginCommand> _commands = new List<PluginCommand>();

		private static void OnGetCommands(object sender, EventArgs args)
		{
			InitCommands();
			foreach (PluginCommand cmd in _commands) _npp.AddCommand(cmd);
		}

		private static void InitCommands()
		{
			PluginCommand cmd = new PluginCommand("Show &Output Window");
			cmd.Execute += OnShowOutputWindow;
			cmd.SortOrder = -4;
			_npp.SetShowOutputWindowCommandIndex(AddCommand(cmd));

			cmd = new PluginCommand("NppSharp &Settings");
			cmd.Execute += OnSettings;
			cmd.SortOrder = -3;
			AddCommand(cmd);

			cmd = new PluginCommand("Show &Help File");
			cmd.Execute += OnShowHelpFile;
			cmd.SortOrder = -2;
			AddCommand(cmd);

			cmd = new PluginCommand("-");
			cmd.SortOrder = -1;
			AddCommand(cmd);

			ScriptManager.AddCommands();

			// Sort for the desired order as specified in the scripts.
			_commands.Sort(new PluginCommandComparer());

			// Add separators
			List<PluginCommand> cmdList = new List<PluginCommand>();
			bool first = true;
			foreach (PluginCommand c in _commands)
			{
				if (c.Separator && !first) cmdList.Add(new PluginCommand("-"));
				cmdList.Add(c);
				first = false;
			}
			_commands = cmdList;
		}

		private static void OnCommandExecuted(object sender, ExecuteCommandEventArgs e)
		{
			try
			{
				if (e.Command != null) e.Command.Fire();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		internal static int AddCommand(PluginCommand cmd)
		{
			_commands.Add(cmd);
			return _commands.Count - 1;
		}

		private class PluginCommandComparer : IComparer<PluginCommand>
		{
			public int Compare(PluginCommand a, PluginCommand b)
			{
				int ret = a.ClassSortOrder.CompareTo(b.ClassSortOrder);
				if (ret != 0) return ret;

				ret = a.SortOrder.CompareTo(b.SortOrder);
				if (ret != 0) return ret;

				return string.Compare(a.Name, b.Name, true);
			}
		}
		#endregion

		#region OutputWindow
		private static OutputView _output = null;
		private static int _showOutputWindowCommandIndex = 0;

		internal static void OnShowOutputWindow(object data)
		{
			try
			{
				_output.Show();
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}

		internal static int ShowOutputWindowCommandIndex
		{
			get { return _showOutputWindowCommandIndex; }
			private set { _showOutputWindowCommandIndex = value; }
		}

		/// <summary>
		/// Gets the output window object.
		/// </summary>
		public static OutputView Output
		{
			get { return _output; }
		}
		#endregion

		#region Toolbar
		private static void OnRegisterToolbarIcons(object sender, EventArgs e)
		{
			foreach (PluginCommand cmd in _commands)
			{
				if (cmd.ShowInToolbar)
				{
					Bitmap icon = cmd.ToolbarIcon;
					if (icon == null) icon = Res.DefaultToolbarIcon;

					_npp.AddToolbarIcon(cmd);
				}
			}
		}
		#endregion

		#region Settings
		private static void OnSettings(object data)
		{
			try
			{
				SettingsForm form = new SettingsForm();
				form.ShowDialog(_npp.Window);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		#endregion

		#region Help File
		private static void OnShowHelpFile(object data)
		{
			try
			{
				string helpFileName = string.Concat("file://", Path.Combine(_npp.NppDir, Res.HelpFileName));
				Help.ShowHelp(Control.FromHandle(_npp.Window.Handle), helpFileName);	// TODO: remove
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		#endregion

		#region Lexers
		internal static int AddLexer(Type lexerType, string name, string description,
			string blockCommentStart, string blockCommentEnd, string lineComment)
		{
			return _npp.AddLexer(lexerType, name, description, blockCommentStart, blockCommentEnd, lineComment);
		}
		#endregion
	}

	/// <summary>
	/// A view (editor) visible within Notepad++
	/// </summary>
	public enum EditorView
	{
		/// <summary>
		/// The main view.
		/// </summary>
		Main = 0,

		/// <summary>
		/// The alternate view.
		/// </summary>
		Sub = 1
	}

	/// <summary>
	/// Notepad++ selection mode.
	/// </summary>
	public enum SelectionMode
	{
		/// <summary>
		/// Normal text selection mode.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Rectangle selection mode.
		/// </summary>
		Rectangle = 1,

		/// <summary>
		/// Lines selection mode.
		/// </summary>
		Lines = 2,

		/// <summary>
		/// Thin selection mode.
		/// </summary>
		Thin = 3
	}
}
