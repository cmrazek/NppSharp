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
		/// Sets the style for a range of characters.
		/// </summary>
		/// <param name="style">The lexer style index.</param>
		/// <param name="start">The zero based starting position of the first character to be styled.</param>
		/// <param name="length">The number of characters to be styled.</param>
		void SetStyle(int style, int start, int length);

		/// <summary>
		/// Sets the style for a single character.
		/// </summary>
		/// <param name="style">The lexer style index.</param>
		/// <param name="pos">The zero based character index to be styled.</param>
		void SetStyle(int style, int pos);

		/// <summary>
		/// Gets the style for a single character.
		/// </summary>
		/// <param name="pos">The zero based character index to retrieve the style number.</param>
		/// <returns>If the character position is valid, the style number for that character; otherwise -1.</returns>
		int GetStyle(int pos);
	}
}
