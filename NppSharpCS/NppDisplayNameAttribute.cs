using System;
using System.Collections.Generic;
using System.Text;

namespace NppSharp
{
	/// <summary>
	/// Controls the display name for the Notepad++ command or lexer class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class NppDisplayNameAttribute : Attribute
	{
		private string _displayName = string.Empty;

		/// <summary>
		/// Constructs the display name attribute.
		/// </summary>
		/// <param name="displayName">The name of the command, as it will be displayed on the menu item.</param>
		public NppDisplayNameAttribute(string displayName)
		{
			_displayName = displayName;
		}

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}
	}
}
