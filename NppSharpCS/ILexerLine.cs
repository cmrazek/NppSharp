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
		#region Text Processing
		/// <summary>
		/// The line of text.
		/// </summary>
		string Text { get; }

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
		/// Reads characters from the line until the callback function returns false.
		/// </summary>
		/// <param name="readFunc">The callback function to control when reading stops.</param>
		/// <returns>A string containing the characters read from the line.</returns>
		string Peek(LexerReadDelegate readFunc);

		/// <summary>
		/// Retrieves a character from an offset relative to the current position.
		/// </summary>
		/// <param name="offset">The offset relative to the current position.</param>
		/// <returns>The character found.</returns>
		char PeekChar(int offset);

		/// <summary>
		/// Checks if the next characters on the line match the string provided.
		/// </summary>
		/// <param name="match">The string that the next characters will be compared to.</param>
		/// <returns>True if the characters match (case sensitive), otherwise false.</returns>
		bool Match(string match);

		/// <summary>
		/// Checks if the next characters on the line match the string provided.
		/// </summary>
		/// <param name="match">The string that the next characters will be compared to.</param>
		/// <param name="ignoreCase">Set to true to ignore case.</param>
		/// <returns>True if the characters match, otherwise false.</returns>
		bool Match(string match, bool ignoreCase);
		#endregion

		#region Styling
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
		/// Styles characters on the line until the callback function returns false.
		/// </summary>
		/// <param name="style">The style to be assigned.</param>
		/// <param name="readFunc">The callback function to control when styling stops.</param>
		void Style(LexerStyle style, LexerReadDelegate readFunc);
		#endregion

		#region Folding
		/// <summary>
		/// Indicates that a fold section should start on this line.
		/// </summary>
		void FoldStart();

		/// <summary>
		/// Indicates the end of a fold section.
		/// </summary>
		void FoldEnd();
		#endregion
	}

	/// <summary>
	/// Used to control the ILexerLine.Peek() function.
	/// </summary>
	/// <param name="ch">The next character to be read.</param>
	/// <returns>This function should return true if the character is to be included in the string returned by Read(), or false to stop reading.</returns>
	public delegate bool LexerReadDelegate(char ch);
}
