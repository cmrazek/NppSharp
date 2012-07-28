using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Represents text stored using a native ptr.
	/// </summary>
	/// <remarks>This text will only be converted to a CLR string upon request, improving performance when its not needed.</remarks>
	public abstract class TextPtr
	{
		/// <summary>
		/// Gets the text at the pointer.
		/// </summary>
		public abstract string Text { get; }

		/// <summary>
		/// Gets the text at the pointer.
		/// </summary>
		/// <returns>The text at the pointer.</returns>
		public override string ToString()
		{
			return this.Text;
		}
	}
}
