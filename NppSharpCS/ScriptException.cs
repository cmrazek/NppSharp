using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	internal class ScriptException : Exception
	{
		public ScriptException(string message)
			: base(message)
		{
		}
	}
}
