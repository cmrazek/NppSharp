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
	/// Interface for a single line of text to be styled or folded.
	/// </summary>
	public interface ILexerLine
	{
		/// <summary>
		/// The line of text.
		/// </summary>
		string Text { get; }

		/// <summary>
		/// Sets the style for the next character.
		/// </summary>
		/// <param name="style">The style to be assigned.</param>
		void Style(LexerStyle style);

		/// <summary>
		/// Sets the style for the next several characters.
		/// </summary>
		/// <param name="style">The style to be assigned.</param>
		/// <param name="length">The number of characters to style.</param>
		void Style(LexerStyle style, int length);

		/// <summary>
		/// Sets the style for the remaining characters on the line.
		/// </summary>
		/// <param name="style">The style to be assigned.</param>
		void StyleRemainder(LexerStyle style);

		/// <summary>
		/// Sets the style for a range of characters.
		/// </summary>
		/// <param name="style">The style to be assigned.</param>
		/// <param name="startPos">The starting character position.</param>
		/// <param name="length">The number of characters to be styled.</param>
		/// <remarks>The current position is placed after the last character in this range.</remarks>
		void StyleRange(LexerStyle style, int startPos, int length);

		/// <summary>
		/// Gets the length of the text.
		/// </summary>
		int Length { get; }

		/// <summary>
		/// Gets the current position.
		/// </summary>
		int Position { get; set; }

		/// <summary>
		/// Gets a value indicating if the end of line has been reached.
		/// </summary>
		bool EOL { get; }

		/// <summary>
		/// Retrieves the next characters from the line without affecting the current position.
		/// </summary>
		/// <param name="length">The number of characters to be retrieved.</param>
		/// <returns>The next characters on the line.</returns>
		/// <remarks>If the end of the line has been reached, this function will return an empty string.</remarks>
		string Peek(int length);

		/// <summary>
		/// Retrieves the next character from the line without affecting the current position.
		/// </summary>
		/// <remarks>If the end of the line has been reached, will return a NUL '\0' character.</remarks>
		char NextChar { get; }

		/// <summary>
		/// Gets the output window writer object.
		/// </summary>
		OutputView Output { get; }
	}
}
