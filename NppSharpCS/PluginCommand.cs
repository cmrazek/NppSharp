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
	public delegate void PluginCommandEventHandler(object data);

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

		public PluginCommand(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Plugin command name cannot be blank.");
			_name = name;
		}

		public string Name
		{
			get { return _name; }
			internal set { _name = value; }
		}

		public void Fire()
		{
			PluginCommandEventHandler ev = Execute;
			if (ev != null) ev(_tag);
		}

		public object Tag
		{
			get { return _tag; }
			internal set { _tag = value; }
		}

		public NppShortcut Shortcut
		{
			get { return _shortcut; }
			internal set { _shortcut = value; }
		}

		public int Id
		{
			get { return _id; }
			internal set { _id = value; }
		}

		public int SortOrder
		{
			get { return _sort; }
			internal set { _sort = value; }
		}

		public int ClassSortOrder
		{
			get { return _classSort; }
			internal set { _classSort = value; }
		}

		public bool Separator
		{
			get { return _separator; }
			internal set { _separator = value; }
		}

		public bool ShowInToolbar
		{
			get { return _showInToolbar; }
			internal set { _showInToolbar = value; }
		}

		public Bitmap ToolbarIcon
		{
			get { return _toolbarIcon; }
			internal set { _toolbarIcon = value; }
		}

		public int Index
		{
			get { return _index; }
			set { _index = value; }
		}
	}
}
