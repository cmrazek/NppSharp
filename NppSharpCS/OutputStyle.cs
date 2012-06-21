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
	/// A style for use in the NppSharp output window.
	/// </summary>
	public enum OutputStyle
	{
		/// <summary>
		/// The normal or default style for text.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// A style associated with warnings.
		/// </summary>
		Warning = 1,

		/// <summary>
		/// A style associated with errors.
		/// </summary>
		Error = 2,

		/// <summary>
		/// A style associated with comment text.
		/// </summary>
		Comment = 3,

		/// <summary>
		/// A style associated with important or emphasized text.
		/// </summary>
		Important = 4,

		/// <summary>
		/// A style associated with not-important text.
		/// </summary>
		NotImportant = 5
	}
}
