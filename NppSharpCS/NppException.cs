using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// An exception that can be thrown when an error occurs while interfacing with Notepad++.
	/// </summary>
	public class NppException : Exception
	{
		/// <summary>
		/// Creates the exception object.
		/// </summary>
		/// <param name="message">A description of the exception.</param>
		public NppException(string message)
			: base(message)
		{
		}
	}
}
