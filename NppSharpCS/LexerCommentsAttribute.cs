using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Sets the comments for the language.
	/// </summary>
	/// <remarks>This is used by Notepad++ to enable to comment menu items.</remarks>
	public class LexerCommentsAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the text used to start a block comment.
		/// </summary>
		/// <remarks>For a C-style language, this would be "/*".</remarks>
		public string BlockStart { get; set; }

		/// <summary>
		/// Gets or sets the text used to end a block comment.
		/// </summary>
		/// <remarks>For a C-style language, this would be "*/".</remarks>
		public string BlockEnd { get; set; }

		/// <summary>
		/// Gets or sets the text used to start a line comment.
		/// </summary>
		/// <remarks>For a C-style language, this would be "//".</remarks>
		public string Line { get; set; }
	}
}
