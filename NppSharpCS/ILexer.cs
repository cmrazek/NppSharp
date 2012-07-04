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
	/// Interface for a lexer object.  Represents a single 'language'.
	/// </summary>
	public interface ILexer
	{
		/// <summary>
		/// The name of the language.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the description of the language.
		/// </summary>
		/// <remarks>This will appear in the status bar when the mouse is hovering over the language menu item.</remarks>
		string Description { get; }

		/// <summary>
		/// The file extensions to be automatically styled by this lexer.
		/// </summary>
		IEnumerable<string> Extensions { get; }

		/// <summary>
		/// A list of word styles to be applied to text when styling.
		/// </summary>
		/// <remarks>Notepad++ limits a language to a maximum of 32 styles.</remarks>
		IEnumerable<LexerStyle> Styles { get; }

		/// <summary>
		/// Called when Notepad++ requires the lexer to apply styles to a line.
		/// </summary>
		/// <param name="line">The object that contains the line information.</param>
		/// <param name="previousLineState">The state value of the previous line.</param>
		/// <returns>The state value of this line.</returns>
		/// <remarks>
		/// <para>
		/// Styling is performed a line at a time, and uses a single integer to keep track of the
		/// state between lines.  For example, if a multi-line comment begins on line 1, and
		/// continues for several lines, lines 2 through and after will somehow need to know that
		/// they are in the middle of a comment section.
		/// </para>
		/// <para>
		/// The return value indicates what the state of the current line should be.
		/// On the next line, the previousLineState argument will be set to the value returned from this line.
		/// You may use this integer any way you see fit.
		/// </para>
		/// <para>
		/// Notepad++ will not style the entire document at one time; only the sections that are
		/// visible will be styled as displayed.
		/// </para>
		/// </remarks>
		int StyleLine(ILexerLine line, int previousLineState);
	}
}
