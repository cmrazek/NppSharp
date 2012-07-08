using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Specifies the description text for a lexer class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class NppDescriptionAttribute : Attribute
	{
		private string _desc = string.Empty;

		/// <summary>
		/// Sets the description text.
		/// </summary>
		/// <param name="desc">The description text.</param>
		public NppDescriptionAttribute(string desc)
		{
			_desc = desc;
		}

		/// <summary>
		/// Gets or sets the description text.
		/// </summary>
		public string Description
		{
			get { return _desc; }
			set { _desc = value; }
		}
	}
}
