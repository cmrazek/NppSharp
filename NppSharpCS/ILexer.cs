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
		/// The file extensions to be automatically styled by this lexer.
		/// </summary>
		/// <remarks>
		/// These are default extensions suggested by the lexer.  Users may alter this list by
		/// using the Settings -> Style Configurator dialog.
		/// </remarks>
		IEnumerable<string> Extensions { get; }

		/// <summary>
		/// A list of word styles to be applied to text when styling.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When Notepad++ starts up, NppSharp will use this property to get a list of styles used by the lexer.
		/// It will use this information to create the XML configuration file at
		/// appdata\roaming\Notepad++\plugins\config\NppSharp.xml.
		/// On consecutive Notepad++ start-ups, this file will be re-loaded, and any user-customized
		/// information will be kept intact.
		/// </para>
		/// <para>
		/// Users may alter the appear of the styles by using the Settings -> Style Configurator dialog.
		/// </para>
		/// <para>
		/// Notepad++ imposes a restriction of 32 styles per language.
		/// </para>
		/// </remarks>
		IEnumerable<LexerStyle> Styles { get; }

		/// <summary>
		/// Called when Notepad++ requires the lexer to apply styles to a line.
		/// </summary>
		/// <param name="line">The object that contains the line information and provides styling/folding functionality.</param>
		/// <param name="previousLineState">The state value of the previous line.</param>
		/// <returns>The state value of this line.</returns>
		/// <remarks>
		/// <para>
		/// Styling is performed a line at a time, and uses a single integer to keep track of the
		/// state between lines.  For example, if a multi-line comment begins on line 1, and
		/// continues for several lines, lines 2 and after will somehow need to know that
		/// they are in the middle of a comment section.  The state variable is used to accomplish
		/// this.
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
