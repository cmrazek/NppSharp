using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// An exception caused by a problem with a lexer.
	/// </summary>
	public class LexerException : Exception
	{
		/// <summary>
		/// Creates the exception object.
		/// </summary>
		/// <param name="message">A message describing the error.</param>
		public LexerException(string message)
			: base(message)
		{
		}
	}
}
