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

namespace NppSharp
{
	/// <summary>
	/// Defines the keyboard shortcut for a command.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class NppShortcutAttribute : Attribute
	{
		private NppShortcut _shortcut;

		/// <summary>
		/// Creates a new NppShortcut attribute.
		/// </summary>
		/// <param name="ctrl">Control key required?</param>
		/// <param name="alt">Alt key required?</param>
		/// <param name="shift">Shift key required?</param>
		/// <param name="keyCode">The virtual key code.</param>
		public NppShortcutAttribute(bool ctrl, bool alt, bool shift, Keys keyCode)
		{
			_shortcut = new NppShortcut(ctrl, alt, shift, keyCode);
		}

		/// <summary>
		/// Gets the shortcut object.
		/// </summary>
		public NppShortcut Shortcut
		{
			get { return _shortcut; }
		}
	}
}
