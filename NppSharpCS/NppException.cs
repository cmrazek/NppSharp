using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	public class NppException : Exception
	{
		public NppException(string message)
			: base(message)
		{
		}
	}
}
