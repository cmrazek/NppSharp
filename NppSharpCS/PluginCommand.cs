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
	/// Event handler for plugin commands.
	/// </summary>
	/// <param name="data">The user data associated with the command.</param>
	[System.Runtime.CompilerServices.CompilerGenerated]	// To stop it from appearing in the help file
	public delegate void PluginCommandEventHandler(object data);

	/// <summary>
	/// An instance of a command that resides in the NppSharp plugin menu, and can be triggered by the user.
	/// </summary>
	[System.Runtime.CompilerServices.CompilerGenerated]	// To stop it from appearing in the help file
	public class PluginCommand
	{
		private string _name = "";
		private object _tag = null;
		private NppShortcut _shortcut = null;
		private int _id = 0;
		private int _sort = 0;
		private int _classSort = 0;
		private bool _separator = false;
		private bool _showInToolbar = false;
		private Bitmap _toolbarIcon = null;
		private int _index = 0;

		internal event PluginCommandEventHandler Execute;

		/// <summary>
		/// Creates a new command object.
		/// </summary>
		/// <param name="name"></param>
		public PluginCommand(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Plugin command name cannot be blank.");
			_name = name;
		}

		/// <summary>
		/// Gets the name of the command.
		/// </summary>
		public string Name
		{
			get { return _name; }
			internal set { _name = value; }
		}

		/// <summary>
		/// Triggers the command.
		/// </summary>
		public void Fire()
		{
			PluginCommandEventHandler ev = Execute;
			if (ev != null) ev(_tag);
		}

		/// <summary>
		/// Gets the user-data associated with this command.
		/// </summary>
		public object Tag
		{
			get { return _tag; }
			internal set { _tag = value; }
		}

		/// <summary>
		/// Gets the shortcut key for this command.
		/// </summary>
		public NppShortcut Shortcut
		{
			get { return _shortcut; }
			internal set { _shortcut = value; }
		}

		/// <summary>
		/// Gets the Notepad++ command ID.
		/// </summary>
		public int Id
		{
			get { return _id; }
			internal set { _id = value; }
		}

		/// <summary>
		/// Gets the command sort order value.
		/// </summary>
		public int SortOrder
		{
			get { return _sort; }
			internal set { _sort = value; }
		}

		/// <summary>
		/// Gets the class sort order value.  Used to group commands together.
		/// </summary>
		public int ClassSortOrder
		{
			get { return _classSort; }
			internal set { _classSort = value; }
		}

		/// <summary>
		/// Gets a flag indicating if a separator appears before this menu item.
		/// </summary>
		public bool Separator
		{
			get { return _separator; }
			internal set { _separator = value; }
		}

		/// <summary>
		/// Gets a flag indicating if this command should be visible in the Notepad++ toolbar.
		/// </summary>
		public bool ShowInToolbar
		{
			get { return _showInToolbar; }
			internal set { _showInToolbar = value; }
		}

		/// <summary>
		/// Gets the icon for the toolbar button.  If null, a default icon will be used instead.
		/// </summary>
		public Bitmap ToolbarIcon
		{
			get { return _toolbarIcon; }
			internal set { _toolbarIcon = value; }
		}

		/// <summary>
		/// Gets or sets the menu item index.
		/// </summary>
		public int Index
		{
			get { return _index; }
			set { _index = value; }
		}
	}
}
