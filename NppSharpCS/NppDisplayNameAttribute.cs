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

namespace NppSharp
{
	/// <summary>
	/// Controls the display name for the Notepad++ command or lexer class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class NppDisplayNameAttribute : Attribute
	{
		private string _displayName = string.Empty;

		/// <summary>
		/// Constructs the display name attribute.
		/// </summary>
		/// <param name="displayName">The name of the command, as it will be displayed on the menu item.</param>
		public NppDisplayNameAttribute(string displayName)
		{
			_displayName = displayName;
		}

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}
	}
}
